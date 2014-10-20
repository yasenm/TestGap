'use strict';

app.controller('NavCtrl', function ($scope, $rootScope, $ionicSideMenuDelegate, identity) {

    $rootScope.isUserLogged = function () {
        return identity.isAuthenticated();
    };

    $scope.showMenu = function () {
        $ionicSideMenuDelegate.toggleLeft();
    };
    $scope.showRightMenu = function () {
        $ionicSideMenuDelegate.toggleRight();
    };

});