'use strict';

app.factory('recipesData',
    function ($http, baseUrl, notifier) {
        var recipesApi = baseUrl + 'api/Recipes';

        return {
            getAllRecipes: function (filters, success) {
                var searchFilters = '?$expand=Category';

                if (filters.orderBy == "name") {
                    searchFilters += '&$orderby=Name';
                }else if (filters.orderBy == "category") {
                    searchFilters += '&$orderby=Category/Name';
                }

//                if (filters.filter) {
//                    searchFilters += '&$filter=Name eq \'' + filters.filter + '\'';
//                }

                if (filters.CategoryId) {
                    searchFilters += '&$filter=CategoryId eq ' + filters.CategoryId;
                }

                $http.get(recipesApi + searchFilters)
                    .success(function (data) {
                        success(data);
                    })
                    .error(function (err) {
                        notifier.error('Could not get recipes');
                    });
            },
            getRecipeById: function (id, success) {
                var addon = '(' + id + ')?$expand=Category,Ingredients/Product';

                $http.get(recipesApi + addon)
                    .success(function (data) {
                        success(data);
                    })
                    .error(function (err) {
                        notifier.error('Could not get catalog product by id');
                    })
            }
        }
    });