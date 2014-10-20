'use strict';

app.controller('RecipeDetailsCtrl',
    function ($scope, $stateParams, recipesData) {
//        if(!identity.isAuthenticated()) {
//            $location.path('/login');
//            return;
//        }
//        $scope.isLogged = identity.isAuthenticated();

        $scope.recipeId = $stateParams.id;

        recipesData.getRecipeById($scope.recipeId,
            function (data) {
                console.log(data)
                $scope.recipe = data;
            })
    });