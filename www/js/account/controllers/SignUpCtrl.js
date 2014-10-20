'use strict';

app.controller('SignUpCtrl',
    function ($scope, $location, auth, identity) {

        $scope.isLogged = identity.isAuthenticated();

        $scope.signup = function (user, signUpForm) {
            console.log(user);
            if (signUpForm.$valid) {
                var newUser = {
                    "Email": user.Email,
                    "Username": user.Username,
                    "Password": user.Password,
                    "ConfirmPassword": user.Password
                };

                auth.signup(newUser).then(function (success) {
                    if (success) {
                        $location.path('/login');
                    }
                    else {
                        $scope.error = 'Username/Password combination is not valid!';
                    }
                });
            }
            else {
                console.log('Username and password are required fields!')
            }
        }
    });