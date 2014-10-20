'use strict';

app.factory('cameraData', function ($http, notifier, authorization, baseUrl) {

    return {
        postImage: function (image, success) {

            var headers = {headers: authorization.getAuthorizationHeader()};
            var imageApi = baseUrl + 'api/ReceiptScanner';

            $http.post(imageApi, {ImageData: image}, headers)
                .success(function (data) {
                    success(data);
                })
                .error(function (err) {
                    alert('Picture not sent! ' + err.message);
                })
        }
    }

});