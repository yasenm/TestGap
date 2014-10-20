'use strict';

app.factory('adminRecipesData', function ($http, baseUrl, authorization) {
    var recipesAdminUrl = baseUrl;
    var headers = authorization.getAuthorizationHeader();

    return {
        createRecipe: function (recipe, success) {
            $http.post(recipesAdminUrl, recipe, {headers: headers})
                .success(function (data) {
                    success(data);
                })
                .error(function (err) {
                    console.log('Recipe was not created: ' + err);
                })
        },
        createCategory: function (category, success) {
            $http.post(recipesAdminUrl, category, {headers: headers})
                .success(function (data) {
                    success(data);
                })
                .error(function (err) {
                    console.log('Recipe category was not created! ' + err);
                })
        }
    }
});