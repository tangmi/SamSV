'use strict';

/**
 * ssvModules Module
 *
 * Description
 */
angular.module('ssvServices', []).
factory('Cards', function($http) {
	var Cards = {};

	Cards.query = function(successcallback, errorcallback) {
		$http({
			method: 'GET',
			url: '/cards'
		}).success(function(data, status, headers, config) {
			successcallback(data.response)
		}).error(function(data, status, headers, config) {
			errorcallback(404);
		});
	};

	Cards.get = function(cardname, successcallback, errorcallback) {
		$http({
			method: 'GET',
			url: '/card/' + cardname
		}).success(function(data, status, headers, config) {
			successcallback(data.response)
		}).error(function(data, status, headers, config) {
			errorcallback(404);
		});
	};

	Cards.render = function(cardname, successcallback, errorcallback) {
		$http({
			method: 'GET',
			url: '/card/' + cardname + '/render'
		}).success(function(data, status, headers, config) {
			successcallback(data.response)
		}).error(function(data, status, headers, config) {
			errorcallback(404);
		});
	};

	Cards.update = function(cardname, cardinfo, successcallback, errorcallback) {
		$http({
			method: 'POST',
			url: '/card/' + cardname,
			data: cardinfo
		}).success(function(data, status, headers, config) {
			successcallback(data.response)
		}).error(function(data, status, headers, config) {
			errorcallback(404);
		});
	};

	return Cards;
});