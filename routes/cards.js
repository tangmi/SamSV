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
		var response = {
			"status": "success",
			"message": "rendered card",
			"response": data
		};
		res.send(response);
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
			"title": "",
			"type": "",
			"description": "",
			"sbuck": "",
			"heat": ""
		};

		fs.writeFileSync(infopath, JSON.stringify(defaults, null, 2));
	}

	fs.readFile(infopath, function(err, data) {
		var cardinfo = JSON.parse(data);

		var artstat = fs.statSync(path.join(dirsrc, cardname + '.psd'));
		var renderstat = fs.statSync(path.join(dirout, cardname + '.tif'));
		var infostat = fs.statSync(path.join(dirsrc, cardname + '.json'));

		callback({
			"name": cardname,
			"thumbnail": "/img/thumbs/" + cardname + "_thumb.png",
			"artmodified": artstat.mtime,
			"lastrendered": renderstat.mtime,
			"infomodified": infostat.mtime,
			"cardinfo": cardinfo
		});

	});
}