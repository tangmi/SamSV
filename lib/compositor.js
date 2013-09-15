'use strict';

var path = require('path');

var im = require('node-imagemagick');
var templatedir = require('../configuration').dirs.template;
var template = require('../configuration').template;

var geo = require(path.resolve(path.join(templatedir, template + '.json'))).geometry;

var logger = require('./logger');

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
		text = text.toString();

		var font = options.font;

		var size = options.w + 'x' + options.h;
		var geometry = options.w + 'x' + options.h + '+' + options.x + '+' + options.y;
		var added = [
			'-gravity', options.alignment, //sets the alignment of the text
			'-background', 'none',
			'-font', font,
			'-fill', options.color,
			'-size', size,
			options.type + ':' + text,
			'-gravity', 'Northwest', //resets the alignment so -geometry will work from the top-left
			'-geometry', geometry,
			'-composite'
		];
		this.params = this.params.concat(added);

		logger.info('Adding text to composite... (' + text.substring(0, 100) + ')');

		return this;
	};

	compositeConstructor.prototype.text = function(textType, text) {
		if (!geo[textType]) {
			logger.error('Text type', '"' + textType + '"', 'doesn\'t exist!');
			return this;
		} else {
			return this._addText(text, {
				font: geo[textType].font || 'Arial',
				type: geo[textType].type || 'label',
				alignment: geo[textType].alignment || 'Northwest',
				color: geo[textType].color || 'black',
				x: geo[textType].x,
				y: geo[textType].y,
				w: geo[textType].w,
				h: geo[textType].h
			});
		}
	};

	compositeConstructor.prototype.build = function(output, callback) {
		this.params[this.params.length] = output;
		logger.info('Rendering composite...');

		var _this = this;
		im.convert(this.params, function(err, stdout) {
			logger.info('Rendered composite to:', output);
			callback();
		});
	};

	return composite;
})();