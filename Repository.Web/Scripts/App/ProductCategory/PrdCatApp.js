(function () {
    var app = angular.module("PrdCatApp", [])
        .controller("PrdCatCtrl", function ($scope, prdCatFactory) {

            //fiels and ..
            $scope.products = [];
            $scope.addMode = false;

            $scope.toggleAdd = function () {
                $scope.addMode = !$scope.addMode;
            };

            $scope.toggleEdit = function () {
                this.product.editMode = !this.product.editMode;
            };
            //method
            //Insert
            $scope.AddPrdCat = function () {
                //call product Category Factory
                //Add
                prdCatFactory.AddPrd(this.newProduct)
                    .success(function (data) {
                        //do staff
                        alert("کالا مورد نظر درج شد");
                        $scope.addMode = false;
                        $scope.products.push(data);
                    })
                    .error(function (data) {
                        //do staff
                        $scope.error = "An error has occured while Adding product !" + data.ExceptionInformation;

                    });
            };

            //Gets all data
            prdCatFactory.getPrdCat().success(function (data) {
                $scope.products = data;
                //$scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while loading posts! " + data.ExceptionInformation;
                $scope.loading = false;
            });

            //Save
            $scope.save = function () {
                var prdCat = this.product;
                prdCatFactory.updatePrdCat(prdCat)
                    .success(function () {
                        alert("درخواست انجام شد");
                        prdCat.editMode = false;
                    })
                    .error(function (data) {
                        $scope.error = "خطا در عملیات ویرایش" + data.ExceptionInformation;
                    });
            };

            //Delete
            $scope.del = function () {
                var currentPrdCat = this.product;
                prdCatFactory.delPrdCat(currentPrdCat)
                    .success(function (data) {
                        alert("انجام شد");
                        $.each($scope.product, function (i) {
                            if ($scope.product[i].Id == currentPrdCat.Id) {
                                $scope.product.splice(i, 1);
                                return false;
                            }
                        });
                    })
                    .error(function (data) {
                        $scope.error = "خطا در حذف :" + data.ExceptionInformation;
                    });

            };
        })
        .factory("prdCatFactory", function ($http) {

            //fields
            var url = "../api/ProductCategories/";
            return {
                AddPrd: function (product) {
                    return $http.post(url, product);
                },
                getPrdCat: function (product) {
                    return $http.get(url, product);
                },
                updatePrdCat: function (product) {
                    return $http.put(url + product.Id, product);
                },
                delPrdCat: function (product) {
                    return $http.delete(url + product.Id, product);
                }
            };
        });
})();