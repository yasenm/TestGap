'use strict';

app.factory('auth', function ($http, $q, $location, identity, authorization, baseUrl) {
    var usersApi = baseUrl + 'api/account';

    return {
        signup: function (user) {
            var deferred = $q.defer();

            $http.post(usersApi + '/register', user)
                .success(function () {
                    deferred.resolve();
                }, function (response) {
                    deferred.reject(response);
                });

            return deferred.promise;
        },
        login: function (user) {
            var deferred = $q.defer();
            user['grant_type'] = 'password';
            $http.post(baseUrl + 'token', 'username=' + user.Username + '&password=' + user.Password + '&grant_type=password',
                {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                }).success(function (response) {
                    if (response["access_token"]) {
                        identity.setCurrentUser(response);
                        deferred.resolve(response);
                    }
                    else {
                        deferred.reject();
                    }
                });

            return deferred.promise;
        },
        logout: function () {
            var deferred = $q.defer();

            var headers = authorization.getAuthorizationHeader();

            $http.post(usersApi + '/logout', {}, { headers: headers })
                .success(function () {
                    identity.setCurrentUser(undefined);
                    deferred.resolve();
                });

            return deferred.promise;
        },
        isAuthenticated: function () {
            if (identity.isAuthenticated()) {
                return true;
            }
            else {
                return $q.reject('not authorized');
            }
        }
    }
});