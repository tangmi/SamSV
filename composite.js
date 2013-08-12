var im = require('node-imagemagick');


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
	compositeConstructor.prototype.add = function(overlay, x, y, w, h) {
		var geometry = w + 'x' + h + '+' + x + '+' + y;
		var added = [overlay, '-geometry', geometry, '-composite'];
		this.params = this.params.concat(added);

		logger.info('Adding', overlay, 'to composite...');

		return this;
	};
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