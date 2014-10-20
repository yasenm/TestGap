app.controller('NavCtrl', function ($scope, $ionicSideMenuDelegate, identity) {

    $scope.isLogged = function () {
        return  identity.isAuthenticated();
    };

    $scope.showMenu = function () {
        $ionicSideMenuDelegate.toggleLeft();
    };
    $scope.showRightMenu = function () {
        $ionicSideMenuDelegate.toggleRight();
    };

});