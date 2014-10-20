'use strict';

app.factory('availableProductsData',
    function ($http, identity, authorization, baseUrl, notifier) {
        var productsApi = baseUrl + 'api/AvailabilityProducts',
            headers = {headers: authorization.getAuthorizationHeader()};

        return{
            getAllAvailableProducts: function (filters, success) {
                var searchFilters = '?$expand=Product&$expand=Product/Category';

                if (filters.orderBy == 'name') {
                    searchFilters += '&$orderby=Product/Name';
                }
                else if (filters.orderBy == 'timeleft') {
                    searchFilters += '&$orderby=ExpirationDate'
                }
                else if (filters.orderBy == 'category') {
                    searchFilters += '&$orderby=Product/Category/Name'
                }

//                if (filters.filter) {
//                    searchFilters += '&$filter=Name eq \'' + filters.filter + '\'';
//                }


                if (filters.categoryId) {
                    searchFilters += '&$filter=Product/CategoryId eq ' + filters.categoryId;
                }

                $http.get(productsApi + searchFilters, {headers: authorization.getAuthorizationHeader()})
                    .success(function (data) {
                        success(data);
                    })
                    .error(function (err) {
                        notifier.error('Could not get catalog products' + err);
                    });
            },
            getProductById: function (id, success) {
                var addon = '(' + id + ')/Product?$expand=Category';

                $http.get(productsApi + addon, {headers: authorization.getAuthorizationHeader()})
                    .success(function (data) {
                        success(data);
                    })
                    .error(function (err) {
                        notifier.error('Could not get catalog product by id');
                    })
            },
            addCatalogProductToAvailableProducts: function (catalogProduct, success) {

//                Id, ExpirationData(DateAdded + CatalogProduct.LifeTime), ProductId= CatalogProduct.Id
                var availableProduct = {};
                availableProduct.ProductId = catalogProduct.Id;
                availableProduct.DateAdded = new Date();
                availableProduct.ExpirationDate = new Date();
                availableProduct.ExpirationDate.setDate(availableProduct.DateAdded.getDate() + catalogProduct.LifetimeInDays);
                console.log(availableProduct);

                $http.post(productsApi, availableProduct, {headers: authorization.getAuthorizationHeader()})
                    .success(function (data) {
                        success(data);
                    })
                    .error(function (err) {
                        notifier.error('Could not add product to fridge: ' + err);
                    });
            },
            updateToUsed: function (product, success) {

                var newProduct = {
                    "Id": product.Id,
                    "DateAdded": product.DateAdded,
                    "ExpirationDate": product.ExpirationDate,
                    "IsFinished": true,
                    "ProductId": product.ProductId,
                    "UserId": product.UserId
                };

                var headers = authorization.getAuthorizationHeader();
                headers['Content-type'] = 'application/json';

                var addon = '(' + product + ')';
                $http.delete(productsApi + addon, {headers: headers})
                    .success(function (data) {
                        success(data);
                    })
                    .error(function (err) {
                        notifier.error('(*(*(*&(*&(*&(*&');
                    })
            }
        }
    });