'use strict';

app.controller('CatalogProductDetailsCtrl', function ($scope, $stateParams, $location, catalogProductsData, availableProductsData, shoppingListData) {
    $scope.data = 'CatalogProductDetailsCtrl';
    $scope.catalogProductId = $stateParams.id;

    function GetCurrentCatalogProductById(id) {
        catalogProductsData.getCatalogProductById(id,
            function (data) {
                console.log(data);
                $scope.catalogProduct = data;
            })
    }

    $scope.addToFridge = function () {
        availableProductsData.addCatalogProductToAvailableProducts($scope.catalogProduct,
            function (data) {
                console.log($location.path());
                $location.path('/available-products');
                console.log($location.path());
            });
    };


    $scope.addToList = function () {
        shoppingListData.addToList($scope.catalogProduct);
    };

    GetCurrentCatalogProductById($scope.catalogProductId);
});