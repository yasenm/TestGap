'use strict';

app.controller('MainCtrl', function ($scope, $rootScope, $location, identity, cameraData, notifier) {

    $scope.takePictureForShoppingList = function () {
        document.addEventListener('deviceready', function () {
            var picConfig = {
                destinationType: Camera.DestinationType.DATA_URL,
                targetHeight: 400,
                targetWidth: 400
            };

            navigator.camera.getPicture(function (imageData) {
                var image = imageData;

                cameraData.postImage(image, function (data) {
                    notifier.success('Successfully photo added!');
                    $location.path('/');
                });
            }, function (err) {
                notifier.error('Picture upload fail!');
            }, picConfig);
        });
    };

    $rootScope.isLogged = function () {
        return  identity.isAuthenticated();
    };

});