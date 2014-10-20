'use strict';

app.factory('productsCategoriesData', function ($http, baseUrl, authorization, notifier) {
    var productsCategoriesApi = baseUrl + '/api/ProductCategories',
        headers = authorization.getAuthorizationHeader();

    return  {
        getAllProductCategories: function (success) {
            $http.get(productsCategoriesApi, {headers: headers})
                .success(function (data) {
                    success(data.value);
                })
                .error(function (err) {
                    notifier.error("Didn't get categories! :" + err);
                })
        }
    }
});