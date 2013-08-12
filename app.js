/**
 * Module dependencies.
 */

var express = require('express'),
	render = require('./routes/render'),
	http = require('http'),
	path = require('path');

var app = express();

app.configure(function() {
	app.set('port', process.env.PORT || 3000);
	app.use(express.logger('dev'));
	app.use(express.bodyParser());
	app.use(express.methodOverride());
	app.use(app.router);
	app.use(express.static(path.join(__dirname, 'app')));
	app.use('img/', express.static(path.join(__dirname, 'app/img')));
});

app.configure('development', function() {
	app.use(express.errorHandler());
});

render.init();

app.get('/render/:name', render.index);

http.createServer(app).listen(app.get('port'), function() {
	console.log("Express server listening on port " + app.get('port'));
});