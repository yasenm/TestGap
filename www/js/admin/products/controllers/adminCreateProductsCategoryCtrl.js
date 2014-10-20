'use strict';

app.controller('AdminCreateProductsCategoryCtrl', function ($scope, adminProductsData) {
//    if (!identity.isAdmin()) {
//        $location.path('/products');
//    }

    $scope.createCategory = function (category, categoryForm) {
        if (categoryForm.$valid) {
            adminProductsData.createCategory(category,
                function (data) {
                    console.log('Successfully created category! : ' + data);
                    $location.path('/create-product');
                })
        }
        else {
            console.log('Product was not created!');
        }
    }

});