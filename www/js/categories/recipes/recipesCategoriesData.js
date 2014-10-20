'use strict';

app.factory('recipesCategoriesData', function ($http, baseUrl, notifier) {
    var recipesCategoriesApi = baseUrl + '/api/RecipeCategories';

    return  {
        getAllRecipeCategories: function (success) {
            $http.get(recipesCategoriesApi)
                .success(function (data) {
                    success(data.value);
                })
                .error(function (err) {
                    notifier.error("Didn't get recipe categories!");
                })
        }
    }
});