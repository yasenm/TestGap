'use strict';

app.factory('authorization',['identity', function(identity) {



    return {
        getAuthorizationHeader: function() {
            if(identity.getCurrentUser()) {
                return {
                    'Authorization': 'Bearer ' + identity.getCurrentUser()['access_token']
                }
            }
            else{
                return {
                    'Authorization': ''
                }
            }
        }
    }
}]);