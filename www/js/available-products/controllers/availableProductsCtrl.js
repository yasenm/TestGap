'use strict';

app.controller('AvailableProductsCtrl', function ($scope, $rootScope, $location, availableProductsData, identity, productsCategoriesData) {
    if (!identity.isAuthenticated()) {
        $location.path('/home');
        return;
    }

    var one_day = 1000 * 60 * 60 * 24;
    $rootScope.availableProductsFilters = {};
    $rootScope.tab = $rootScope.tab || {};

    $scope.identity = identity;
    $scope.isLogged = identity.isAuthenticated();

    $scope.showActions = true;

    $scope.swipeRight = function (id) {
        deleteById(id);
    };

    function showAvailableProductsTab() {
        $rootScope.tab.isAvailabeProductsTab = true;
        $rootScope.tab.isCatalogProductsTab = false;
        $rootScope.tab.isRecipesTab = false;
    }

    $scope.numColumns = [];
    $scope.numColumns.length = 4;

    function getProducts(filters) {
        availableProductsData.getAllAvailableProducts(filters,
            function (data) {
                $rootScope.availableProducts = [];
                for (var i = 0; i < data.value.length; i += 1) {
                    var product = data.value[i];

                    if (product.IsFinished) {
                        continue;
                    }

                    var dateExpiring = new Date(product.ExpirationDate);
                    var dateAdded = new Date(product.DateAdded);
                    var currentTime = new Date();

                    // Calculate the difference in milliseconds
                    var timeDiff = currentTime.getTime() - dateAdded.getTime();
                    var timeDiffLifetimeInDays = dateExpiring.getTime() - dateAdded.getTime();

                    // Convert back to days and return
                    var lifetimeInDays = Math.round(timeDiffLifetimeInDays / one_day);
                    var freshness = lifetimeInDays - Math.round(timeDiff / one_day);
                    var percent = freshness / lifetimeInDays * 100;

                    product.freshness = freshness;
                    product.lifetimeInDays = lifetimeInDays;
                    product.lifetimePercent = 100 - percent;

                    if (freshness < 0) {
                        deleteById(product.Id);
                    }

                    console.log('Freshness -> ' + freshness);
                    console.log('Lifetime in days -> ' + lifetimeInDays);
                    console.log('Life time percent -> ' + product.lifetimePercent);
                    console.log('---------------------------------------------');

                    $rootScope.availableProducts.push(product);
                }
            });
    }

    function getCategories() {
        productsCategoriesData.getAllProductCategories(
            function (data) {
                $rootScope.availableProductsCategories = data;
            })
    }

    function deleteById(id) {
        availableProductsData.getProductById(id,
            function (product) {
                availableProductsData.updateToUsed(id,
                    function (data) {
                        getProducts($rootScope.availableProductsFilters);
                    });
            });
    }

    $scope.sort = function () {
        getProducts($rootScope.availableProductsFilters);
    };

    getProducts($rootScope.availableProductsFilters);
    getCategories();
    showAvailableProductsTab();
});