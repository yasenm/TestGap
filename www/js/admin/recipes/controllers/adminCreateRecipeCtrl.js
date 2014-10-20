'use strict';

app.controller('AdminCreateRecipeCtrl', function ($scope, adminRecipesData) {

    $scope.createRecipe = function (recipe, recipeForm) {
        if(!recipeForm.$valid){
            adminRecipesData.createRecipe(recipe,
                function (data) {
                    console.log('Successfully created recipe! : ' + data);
                    $location.path('/recipes');
                })
        }
        else{
            console.log('Recipe was not created!');
        }
    }

});