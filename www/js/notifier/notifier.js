'use strict';

app.factory('notifier', function () {

    return {
        success: function (message) {
            document.addEventListener('deviceready', function () {
                navigator.notification.vibrate(2000);
            });
        },
        error: function (message) {
            document.addEventListener('deviceready', function () {
                navigator.notification.beep(1);
            });
        }
    }

});