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

module.exports.init = function() {
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

module.exports.renderTemplate = function(req, res) {
	renderTemplate(function() {});
};

module.exports.render = function(req, res, callback) {
	var cardname = req.params.name;

	logger.info('Render card:', cardname);

	var stime = +new Date,
		etime,
		dtime;

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

			var infopath = path.join(dirsrc, cardname + '.json');
			fs.readFile(infopath, function(err, data) {
				var cardinfo = JSON.parse(data);

				composite(templatepath)
					.art(filetmp)
					.text('title', cardinfo.title)
					.text('type', cardinfo.type)
					.text('description', cardinfo.description)
					.text('sbuck', cardinfo.sbuck)
					.text('heat', cardinfo.heat)
					.build(fileout, function() {
						//clean temp file
						try {
							fs.unlinkSync(filetmp);
						} catch (e) {};

						renderThumbnail(cardname, done);
					});
			});
		}
	], function() {
		logger.info('Finished building ' + cardname + '!');
		etime = +new Date;
		dtime = etime - stime;
		callback({
			"name": cardname,
			"elapsed": dtime
		});
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
		'-resize', config.thumbnailsize,
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

module.exports.init();