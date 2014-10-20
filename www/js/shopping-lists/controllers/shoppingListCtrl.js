'use strict';

app.controller('ShoppingListCtrl', function ($scope, $rootScope, shoppingListData, identity) {

    $scope.removeProduct = function (id) {
        shoppingListData.removeFromList(id);
        getShoppingList();
    };

    function hideTabs() {
        $rootScope.tab.isAvailabeProductsTab = false;
        $rootScope.tab.isCatalogProductsTab = false;
        $rootScope.tab.isRecipesTab = false;
    }

    hideTabs();

    function getShoppingList() {
        shoppingListData.getCurrentList(function (data) {
            console.log(data);
            $rootScope.currentShopList = data.listOfProducts;
        });
    }

    function createList() {
        shoppingListData.createList(function (list) {
            $rootScope.currentShopList = list;
            console.log(list);
        })
    }

    shoppingListData.getCurrentList(function (data) {
        if (!data) {
            createList();
        }
    });

    getShoppingList();

});