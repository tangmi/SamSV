'use strict';

var path = require('path'),
	fs = require('fs'),
	async = require('async');

var render = require('./render');

var config = require('../configuration');
var dirsrc = config.dirs.src;
var dirout = config.dirs.out;

var templatename = require('../template.json').name;

var logger = require('winston');

module.exports.list = function(req, res) {
	res.setHeader('Content-Type', 'application/json');

	var list = [];

	fs.readdir(dirsrc, function(err, files) {
		async.each(files.filter(function(file) {
			return file.indexOf('.psd') !== -1 && file.indexOf(templatename) !== 0;
		}), function(item, callback) {

			var cardname = path.basename(item, '.psd');

			getCardInfo(cardname, function(info) {
				list.push(info);
				callback();
			});

		}, function() {
			var response = {
				"status": "success",
				"message": "got card list",
				"response": list
			};
			res.send(response);
		});


	});
};

module.exports.single = function(req, res) {
	res.setHeader('Content-Type', 'application/json');

	var cardname = req.params.name;

	getCardInfo(cardname, function(cardinfo) {
		var response = {
			"status": "success",
			"message": "got info for card",
			"response": cardinfo
		};

		res.send(response);
	});

};

module.exports.update = function(req, res) {
	res.setHeader('Content-Type', 'application/json');

	var cardname = req.params.name;
	var infopath = path.join(dirsrc, cardname + '.json');

	try {
		fs.unlinkSync(infopath);
	} catch (e) {};

	var data = {
		"title": req.body.title,
		"type": req.body.type,
		"description": req.body.description,
		"sbuck": req.body.sbuck,
		"heat": req.body.heat
	};

	fs.writeFileSync(infopath, JSON.stringify(data, null, 2));

	getCardInfo(cardname, function(cardinfo) {
		var response = {
			"status": "success",
			"message": "updated card",
			"response": cardinfo
		};
		res.send(response);
	});
};

module.exports.render = function(req, res) {
	render.render(req, res, function(data) {
		getCardInfo(data.name, function(info) {
			info.elapsed = data.elapsed;

			var response = {
				"status": "success",
				"message": "rendered card",
				"response": info
			};
			res.send(response);
		});
	});
};

function getCardInfo(cardname, callback) {
	if (!fs.existsSync(path.join(dirsrc, cardname + '.psd'))) {
		res.send({
			"status": "error",
			"message": "art does not exist for " + cardname + "!"
		});
		return;
	}

	var infopath = path.join(dirsrc, cardname + '.json');

	if (!fs.existsSync(infopath)) {
		fs.openSync(infopath, 'w');

		var defaults = {
			"title": cardname,
			"type": "type",
			"description": "description",
			"sbuck": "0",
			"heat": "0"
		};

		fs.writeFileSync(infopath, JSON.stringify(defaults, null, 2));
	}

	var artstat = -1;
	var renderstat = -1;
	var infostat = -1;
	try {
		artstat = fs.statSync(path.join(dirsrc, cardname + '.psd')).mtime;
	} catch (e) {}
	try {
		renderstat = fs.statSync(path.join(dirout, cardname + '.tif')).mtime;
	} catch (e) {}
	try {
		infostat = fs.statSync(path.join(dirsrc, cardname + '.json')).mtime;
	} catch (e) {}

	fs.readFile(infopath, function(err, data) {
		var cardinfo = JSON.parse(data);

		callback({
			"name": cardname,
			"thumbnail": "/img/thumbs/" + cardname + "_thumb.png",
			"artmodified": artstat,
			"lastrendered": renderstat,
			"infomodified": infostat,
			"cardinfo": cardinfo
		});

	});
}