'use strict';


angular.module('ssv', ['ssvServices']).
config(['$routeProvider',
	function($routeProvider) {
		$routeProvider.when('/', {
			templateUrl: 'partials/card-list.html',
			controller: 'CardListCtrl'
		});
		$routeProvider.when('/card/:cardname', {
			templateUrl: 'partials/card-view.html',
			controller: 'CardViewCtrl'
		});
		$routeProvider.otherwise({
			redirectTo: '/'
		});
	}
]);