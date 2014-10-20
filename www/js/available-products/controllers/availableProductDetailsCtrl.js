'use strict';

app.controller('AvailableProductDetailsCtrl', function ($scope, $stateParams, availableProductsData, identity) {
    if (!identity.isAuthenticated()) {
        $location.path('/home');
        return;
    }

//    $scope.currentUserShoppingListId = identity.getCurrentUser().shoppingList;
  //  $scope.isLogged = identity.isAuthenticated();

    $scope.productId = $stateParams.id;

    $scope.quantity = 1000;

    availableProductsData.getProductById($scope.productId,
        function (data) {
            $scope.product = data;
        });

//    $scope.addToShoppingList = function (quantity) {
//        $scope.product.quantity = quantity;
//
//        shoppingListData.addProductToShoppingList($scope.currentUserShoppingListId, $scope.product,
//            function () {
//                notifier.success('Product added to shopping list');
//            });
//    };

//    $scope.addToFridge = function (quantity) {
//        $scope.product.quantity = quantity;
//
//        productsData.addProductToFridge(identity.currentUser()._id, $scope.product,
//            function () {
//                notifier.success('Product added to fridge');
//            });
//    };

});