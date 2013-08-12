'use strict';

// render method

var path = require('path'),
	fs = require('fs'),
	async = require('async'),
	im = require('node-imagemagick');

var composite = require('../composite');

var logger = require('winston');

var config = require('../configuration');
var dirs = config.dirs;
var dirsrc = path.resolve(dirs.src);
var dirtmp = path.resolve(dirs.tmp);
var dirout = path.resolve(dirs.out);
var dirthumbs = path.resolve(dirs.thumbs);

var templatepath = '';

exports.init = function() {
	fs.stat(dirsrc, function(err, stats) {
		if (!stats) {
			logger.info('Source directory doesn\'t exist! (' + dirsrc + ')');
			process.exit(1);
		}

		async.series([
			function(done) {
				//prepare folders for output
				logger.info('Preparing folders...');
				async.parallel([
					function(callback) {
						//make the output directory if it doesn't exist
						fs.stat(dirout, function(err, stats2) {
							if (!stats2) {
								fs.mkdirSync(dirout);
							}
							callback();
						});
					},
					function(callback) {
						//make the output directory if it doesn't exist
						fs.stat(dirtmp, function(err, stats2) {
							if (!stats2) {
								fs.mkdirSync(dirtmp);
							}
							callback();
						});
					}
				], function() {
					done();
				})
			},
			function(done) {
				//cache the template
				logger.info('Caching template...');
				renderTemplate(done);
			}
		], function() {
			logger.info('Initialization done!');
		});

	});
};

exports.renderTemplate = function(req, res) {
	renderTemplate(function() {});
};

exports.index = function(req, res) {
	var cardname = req.params.name;

	logger.info('Render card:', cardname);

	async.series([
		function(done) {
			psdToPng(cardname, done);
		},
		function(done) {
			var filetmp = path.join(dirtmp, cardname + '.tif');
			var fileout = path.join(dirout, cardname + '.tif');

			//clean
			try {
				fs.unlinkSync(fileout);
			} catch (e) {};

			composite(templatepath)
				.art(filetmp)
				.text('title', 'sam warner')
				.text('type', 'human')
				.text('description', 'this is a description this is a description this is a description this is a description this is a description this is a description this is a description this is a description this is a description this is a description ')
				.text('sbuck', 5)
				.text('heat', 0)
				.build(fileout, function() {
					renderThumbnail(cardname, done);
				});
		}
	], function() {
		logger.info('Finished building ' + cardname + '!');
		res.send(200);
	});
};

function renderTemplate(done) {
	templatepath = path.join(dirtmp, require('../template.json').name + '.tif');
	psdToPng(require('../template.json').name, done);
}

function renderThumbnail(resourcename, callback) {
	var resourcein = path.join(dirout, resourcename + '.tif');
	var thumbnail = path.join(dirthumbs, resourcename + '_thumb.png');

	try {
		fs.unlinkSync(thumbnail);
	} catch (e) {};

	im.convert([
		resourcein,
		'-resize', '250x250',
		thumbnail
	], function(err, stdout) {
		logger.info('Created thumbnail of card', resourcename);
		callback();
	});
}

function psdToPng(resourcename, callback) {
	var resourcein = path.join(dirsrc, resourcename + '.psd');
	var resourcetmp = path.join(dirtmp, resourcename + '.tif');
	im.convert([
		resourcein + '[0]',
		resourcetmp
	], function(err, stdout) {
		logger.info(resourcename + '.psd', 'rendered to', resourcetmp);
		callback();
	});
}