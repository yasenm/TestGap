'use strict';

app.controller('RecipesCtrl',
    function ($scope, $rootScope, $location, recipesData, identity, recipesCategoriesData) {
        if (!identity.isAuthenticated()) {
            $location.path('/home');
            return;
        }
        $rootScope.tab = $rootScope.tab || {};
        $scope.isLogged = identity.isAuthenticated();

        function showRecipesTab() {
            $rootScope.tab.isAvailabeProductsTab = false;
            $rootScope.tab.isCatalogProductsTab = false;
            $rootScope.tab.isRecipesTab = true;

        }

        $rootScope.recipeFilter = {};

        $rootScope.recipeNameFilter = "";

        function getRecipes() {
            recipesData.getAllRecipes(
                $rootScope.recipeFilter,
                function (data) {
                    $rootScope.recipes = data.value;
                });
        }

        function getCategories() {
            recipesCategoriesData.getAllRecipeCategories(
                function (data) {
                    $rootScope.categories = data;

                })
        }

        $scope.sort = function () {
            recipesData.getAllRecipes(
                $rootScope.recipeFilter,
                function (data) {
                    $rootScope.recipes = data.value;
                });
        };

        getRecipes();
        getCategories();
        showRecipesTab();
    });