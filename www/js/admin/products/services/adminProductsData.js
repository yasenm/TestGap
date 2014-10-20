'use strict';

app.factory('adminProductsData', function ($http, baseUrl, authorization) {
    var adminProductsUrl = baseUrl + 'api/Products';
    var headers = authorization.getAuthorizationHeader();

    return {
        getAllProducts: function (success) {
            $http.get(adminProductsUrl, {headers: headers})
                .success(function (data) {
                    success(data);
                })
                .error(function (err) {
                    console.log('Could not get catalog products');
                });
        },
        createProduct: function (product, success) {
            $http.post(adminProductsUrl, product,
                {
                    transformRequest: function (obj) {
                        var str = [];
                        for (var p in obj)
                            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                        return str.join("&");
                    },
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }})
                .success(function (data) {
                    success(data);
                })
                .error(function (error) {
                    console.log('Could not add product: ' + error);
                })
        },
        updateProduct: function (productId, success) {
            $http.put(adminProductsUrl, productId, {headers: headers})
                .success(function (data) {
                    success(data);
                })
                .error(function (error) {
                    console.log('Could not update product: ' + error);
                })
        },
        deleteProduct: function (productId, success) {
            $http.put(adminProductsUrl, productId, {headers: headers})
                .success(function (data) {
                    success(data);
                })
                .error(function (error) {
                    console.log('Could not update product: ' + error);
                })
        },
        createCategory: function (category, success) {
            $http.post(adminProductsUrl, category, {headers: headers})
                .success(function (data) {
                    success(data);
                })
                .error(function (err) {
                    console.log('Category was not created: ' + err);
                })
        }
    }
});