'use strict';

var fs = require('fs');

function ensureDirectoryExists(path, cb) {
	var callback = typeof cb === 'function' ? cb : function() {};

	fs.exists(path, function(exists) {
		if (exists) {
			fs.stat(path, function(err, stats) {
				if (stats.isDirectory()) {
					callback();
				} else {
					callback(path + ' already exists and is not a directory!');
				}
			})
		} else {
			fs.mkdir(path, 0777, function() {
				callback();
			});
		}
	})

}