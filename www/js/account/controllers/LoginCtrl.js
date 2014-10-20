'use strict';

app.controller('LoginCtrl',
    function ($scope, $location, $rootScope, identity, auth, shoppingListData) {
        $scope.identity = identity;
        $scope.loginActive = true;

        $scope.isAdmin = function () {
            return identity.isAdmin();
        };

        $scope.login = function (user, loginForm) {
            if (loginForm.$valid) {
                auth.login(user).then(function (success) {
                    if (success) {
                        $location.path('/available-products');
                    }
                    else {
                        $scope.error = 'Username/Password combination is not valid!';
                    }
                });
            }
            else {
                console.log('Username and password are required fields!')
            }
        };

        $scope.logout = function () {
            auth.logout().then(function () {
                console.log('Successful logout!');
                if ($scope.user) {
                    $scope.user.email = '';
                    $scope.user.username = '';
                    $scope.user.password = '';
                }

                $location.path('/home');
            })
        };

        $scope.changeToSignUp = function () {
            $scope.loginActive = false;
        };
    });