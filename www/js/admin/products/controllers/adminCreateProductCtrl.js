'use strict';

app.controller('AdminCreateProductCtrl', function ($scope, $location, identity, adminProductsData) {
//    if (!identity.isAdmin()) {
//        $location.path('/products');
//    }

    $scope.createProduct = function (product, productForm) {
        if (productForm.$valid) {
            var firstCommaIndex = product.ImageUrl.indexOf(',');
            var imageData = product.ImageUrl.substr(firstCommaIndex + 1);
            product.ImageUrl = imageData;

            adminProductsData.createProduct(product,
                function (data) {
                    console.log('Successfully created product! : ' + data);
                    $location.path('/products');
                })
        }
        else {
            console.log('Product was not created!');
        }
    };

});