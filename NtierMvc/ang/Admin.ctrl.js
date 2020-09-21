angular.module('App').controller("AdminController", function ($scope, $http, $timeout, $compile) {
    $scope.VendorId = "";
    //For Pagination
    //$scope.maxsize = 5;

    //$scope.AdminTotalCount = 0;
    //$scope.AdminPageIndex = 1;
    //$scope.AdminPageSize = "50";
    //$scope.SearchTypeId = "";
    

    //$scope.GetRoleURLDetails = function () {
    //    $http.get(window.GetRoleURLDetails + "?pageIndex=" + $scope.AdminPageIndex + "&pageSize=" + $scope.AdminPageSize).success(function (response) {
    //        $scope.GRList = response.lstGREntity;
    //        $scope.GRTotalCount = response.totalcount;
    //    }, function (error) {
    //        alert('failed');
    //    });
    //}

    //$scope.FetchGoodsRecieptList();

    //$scope.GRPageChanged = function () {
    //    $scope.FetchGoodsRecieptList();
    //}

    //$scope.GRChangePageSize = function () {
    //    $scope.GRPageIndex = 1;
    //    $scope.FetchGoodsRecieptList();
    //}


    
    
})  

//EOF