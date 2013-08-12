'use strict';

/**
 * ssvFilters Module
 *
 * Description
 */
angular.module('ssvFilters', []).
filter('formatDate', function($filter) {
	return function(str) {
		if (str !== -1) {
			return $filter('date')(str, 'medium');
		} else {
			return 'Never';
		}
	};
});