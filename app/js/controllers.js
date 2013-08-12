'use strict';


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