(function () {
    var app = angular.module("UnitApp", [])
        .controller("UnitCtrl", function ($scope, unitFactory) {

            //fiels and ..
            $scope.units = [];
            $scope.addMode = false;

            $scope.toggleAdd = function () {
                $scope.addMode = !$scope.addMode;
            };

            $scope.toggleEdit = function () {
                this.unit.editMode = !this.units.editMode;
            };
            //method
            //Insert
            $scope.AddUnit = function () {
                //call product Category Factory
                //Add
                unitFactory.AddUnit(this.newUnit)
                    .success(function (data) {
                        //do staff
                        alert("کالا مورد نظر درج شد");
                        $scope.addMode = false;
                        $scope.Units.push(data);
                    })
                    .error(function (data) {
                        //do staff
                        $scope.error = "An error has occured while Adding product !" + data.ExceptionInformation;

                    });
            };

            //Gets all data
            unitFactory.getUnit().success(function (data) {
                $scope.units = data;
                //$scope.loading = false;
            }).error(function (data) {
                $scope.error = "An Error has occured while loading posts! " + data.ExceptionInformation;
                $scope.loading = false;
            });

            //Save
            $scope.save = function () {
                var unitV = this.product;
                unitFactory.updateUnit(unitV)
                    .success(function () {
                        alert("درخواست انجام شد");
                        unitV.editMode = false;
                    })
                    .error(function (data) {
                        $scope.error = "خطا در عملیات ویرایش" + data.ExceptionInformation;
                    });
            };

            //Delete
            $scope.del = function () {
                var currentUnit = this.unit;
                unitFactory.delUnit(currentUnit)
                    .success(function (data) {
                        alert("انجام شد");
                        $.each($scope.unit, function (i) {
                            if ($scope.unit[i].Id == currentUnit.Id) {
                                $scope.unit.splice(i, 1);
                                return false;
                            }
                        });
                    })
                    .error(function (data) {
                        $scope.error = "خطا در حذف :" + data.ExceptionInformation;
                    });

            };
        })
        .factory("unitFactory", function ($http) {

            //fields
            var url = "../api/Units/";
            return {
                AddUnit: function (unit) {
                    return $http.post(url, unit);
                },
                getUnit: function (unit) {
                    return $http.get(url, unit);
                },
                updateUnit: function (unit) {
                    return $http.put(url + unit.Id, unit);
                },
                delUnit: function (unit) {
                    return $http.delete(url + unit.Id, unit);
                }
            };
        });
})();