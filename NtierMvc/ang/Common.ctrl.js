angular.module('App').controller("CommonController", function ($scope, $http, $timeout, $compile) {

    $scope.SaveCountryDetails = function () {
        var frm = $("#formSaveCountryData");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveCountryData");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveCountryDetail, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }
    }

    $scope.SaveStateDetails = function () {
        var frm = $("#formSaveStateData");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveStateData");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveStateDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }
    }

    $scope.DeleteTableData = function (id, Type) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        $http({ url: window.DeleteTableDatas, method: 'POST', data: { id: id, type: Type } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    alert(res);
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
    }

})  

//EOF