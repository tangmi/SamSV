/**
 * Module dependencies.
 */

var express = require('express'),
	cards = require('./routes/cards'),
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

app.get('/cards', cards.list);
app.get('/card/:name', cards.single);
app.post('/card/:name', cards.update);
app.get('/card/:name/render', cards.render);

http.createServer(app).listen(app.get('port'), function() {
	console.log("Express server listening on port " + app.get('port'));
});