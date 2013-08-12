'use strict';

function SuperCtrl($scope, $rootScope, Cards) {

	$rootScope.render = function(card) {
		card.rendering = true;
		Cards.render(card.name, function(data) {
			card.lastrendered = data.lastrendered;
			card.rendering = false;
			card.thumbnail = data.thumbnail + '#' + (+new Date);
		}, function() {
			card.rendering = false;
		});
	};

	$rootScope.update = function(card) {
		card.updating = true;
		Cards.update(card.name, card.cardinfo, function(data) {
			card.infomodified = data.infomodified;
			$rootScope.render(card);
			card.updating = false;
		}, function() {
			card.updating = false;
		});
	};
}

function CardListCtrl($scope, Cards) {
	$scope.cards = [];

	Cards.query(function(data) {
		$scope.cards = data;
	});

}

function CardViewCtrl($scope, Cards, $routeParams) {
	$scope.card = {};

	Cards.get($routeParams.cardname, function(data) {
		$scope.card = data;
	});



}