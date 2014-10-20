'use strict';

app.controller('AdminCreateRecipesCategoryCtrl', function ($scope, adminRecipesData) {

    $scope.createCategory = function (category, categoryForm) {
        if(!categoryForm.$valid){
            adminRecipesData.createRecipe(category,
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