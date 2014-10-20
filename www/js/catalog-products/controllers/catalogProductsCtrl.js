'use strict';

app.controller('CatalogProductsCtrl', function ($scope, $rootScope, catalogProductsData, productsCategoriesData, notifier) {

    $rootScope.catalogProductsFilters = {};
    $rootScope.tab = $rootScope.tab || {};

    $scope.showActions = true;

    $scope.swipeRight = function () {
//        alert('KJHASDASKJLDASDKHJL');
    };

    function showAvailableProductsTab() {
        $rootScope.tab.isAvailabeProductsTab = false;
        $rootScope.tab.isCatalogProductsTab = true;
        $rootScope.tab.isRecipesTab = false;
    }

    function getCatalogProducts(filters) {
        catalogProductsData.getAllCatalogProducts(filters,
            function (data) {
                $rootScope.catalogProducts = data.value;
            })
    }


    function getCategories() {
        productsCategoriesData.getAllProductCategories(
            function (data) {
                $scope.categories = data;
            })
    }

    $scope.sort = function () {
        getCatalogProducts($rootScope.catalogProductsFilters);
    };

    getCatalogProducts($rootScope.catalogProductsFilters);

    getCategories();
    showAvailableProductsTab();
});