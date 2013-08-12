var im = require('node-imagemagick');
var template = require('./template.json');
var geo = template.geometry;

var logger = require('winston');

module.exports = (function() {
	function composite() {
		return new compositeConstructor(arguments);
	}

	function compositeConstructor(background) {
		this.params = Array.prototype.slice.call(background);

		logger.info('Building a composite from', background);
		return this;
	}

	compositeConstructor.prototype._add = function(overlay, options) {
		var geometry = options.w + 'x' + options.h + '+' + options.x + '+' + options.y;
		var added = [
			overlay,
			'-geometry', geometry,
			'-composite'
		];
		this.params = this.params.concat(added);

		logger.info('Adding', overlay, 'to composite...');

		return this;
	};

	compositeConstructor.prototype.art = function(overlay) {
		return this._add(overlay, {
			x: geo.art.x,
			y: geo.art.y,
			w: geo.art.w,
			h: geo.art.h
		});
	};

	compositeConstructor.prototype._addText = function(text, options) {
		var font = 'Arial';

		var size = options.w + 'x' + options.h;
		var geometry = options.w + 'x' + options.h + '+' + options.x + '+' + options.y;
		var added = [
			'-font', font,
			'-fill', 'blue',
			'-size', size,
			'-gravity', 'West',
			'caption:' + text,
			// 'label:' + text,
			'-geometry', geometry,
			'-composite'
		];
		this.params = this.params.concat(added);

		logger.info('Adding text to composite... (' + text.substring(0, 100) + ')');

		return this;
	};

	compositeConstructor.prototype.title = function(text) {
		return this._addText(text, {
			x: geo.title.x,
			y: geo.title.y,
			w: geo.title.w,
			h: geo.title.h
		});
	}

	compositeConstructor.prototype.build = function(output, callback) {
		this.params[this.params.length] = output;

		var _this = this;
		im.convert(this.params, function(err, stdout) {
			logger.info('Rendered composite to:', output);
			callback();
		});
	};

	return composite;
})();