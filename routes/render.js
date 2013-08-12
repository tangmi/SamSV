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
						logger.info('output folder...')
						fs.stat(dirout, function(err, stats2) {
							if (!stats2) {
								fs.mkdirSync(dirout);
							}
							callback();
						});
					},
					function(callback) {
						//make the output directory if it doesn't exist
						logger.info('temp folder...')
						fs.stat(dirtmp, function(err, stats2) {
							if (!stats2) {
								fs.mkdirSync(dirtmp);
							}
							callback();
						});
					}
				], function() {
					logger.info('folders made.');
					done();
				})
			},
			function(done) {
				//cache the template
				renderTemplate(done);
			}
		]);

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
			// try {
			// 	fs.openSync(fileout, 'w');
			// } catch (e) {};

			composite(templatepath)
				.add(filetmp, 0, 0, 500, 500)
				.build(fileout, function() {
					done();
				});

		}
	], function() {
		res.send(200);
	});
};

function renderTemplate(done) {
	templatepath = path.join(dirtmp, config.template + '.tif');
	psdToPng(config.template, done);
}

function psdToPng(resourcename, callback) {
	var resourcein = path.join(dirsrc, resourcename + '.psd');
	var resourcetmp = path.join(dirtmp, resourcename + '.tif');
	im.convert([resourcein + '[0]', resourcetmp], function(err, stdout) {
		logger.info(resourcename + '.psd', 'cached to', resourcetmp);
		callback();
	});
}