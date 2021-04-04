angular.module('App').controller("ReportController", function ($scope, $http, $timeout, $compile, $filter, $window) {

    $('.modal-dialog').draggable({
        handle: ".modal-body"
    });
    $scope.date = '';
    $scope.date1 = '';
    $scope.SoNo = '';

    $scope.HideFileds = function () {

        if ($scope.ReportType == "EnquiryReport") {
            $scope.ISTure = false;
        } else
            $scope.ISTure = true;
    }


    $scope.GenerateReport = function (Type) {
        //var DownloadType = dwnldtype;
        ShowLoadder();
        $.ajax({
            type: "Post",
            url: window.GenerateReport,
            data: JSON.stringify({ ReportType: Type, pageindex: "1", pagesize: "50", SearchCustomerName: "", SearchCustomerID: "", SearchCustomerIsActive: "" }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //alert("Dowloaded Successfully" + data);
                if (data != "") {
                    //use window.location.href for redirect to download action for download the file
                    window.location.href = window.DownloadDoc + '?fileName=' + data;
                    HideLoadder();
                }
                else {
                    alert(data.errorMessage);
                    HideLoadder();
                }
            },
            error: function (x, e) {
                alert('Some error is occurred, Please try after some time.');
                HideLoadder();
            }
        })
    }

    if ($scope.ReportType == 'Enquiry Report') {
        $scope.ISTure = false;
    }
    else
        $scope.ISTure = true;


    $scope.CreateWAAuthReport = function () {
        //var DownloadType = dwnldtype;
        var fromdate = '';
        var Todate = '';
        ShowLoadder();
        if ($scope.date != '') {
            if (new Date($scope.date).getMonth() + 1 == 4 && new Date($scope.date1).getMonth() + 1 == 3) {
                if ((new Date($scope.date1).getFullYear() - new Date($scope.date).getFullYear()) == 1) {
                    fromdate = $scope.date;
                    Todate = $scope.date1;
                    ShowLoadder();
                    $.ajax({
                        type: "Post",
                        url: window.CreateWAAuthReport,
                        data: JSON.stringify({ SoNo: $scope.SoNo, FromDate: fromdate, ToDate: Todate, ReportType: $scope.ReportType }),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            //alert("Dowloaded Successfully" + data);
                            if (data == 'No Records Found For Selected Item') {
                                HideLoadder();
                                alert(data);
                            }
                            else if (data != "") {
                                window.location.href = window.DownloadDoc + '?fileName=' + data;
                                HideLoadder();
                            }
                            else {
                                HideLoadder();
                                alert(data.errorMessage);
                            }
                        },
                        error: function (x, e) {
                            alert('Some error is occurred, Please try after some time.');
                            HideLoadder();
                        }
                    })
                }
                else
                    alert("Select Financial Year")
                HideLoadder();
            }

            else
                alert("Select Financial Year")
            HideLoadder();
        }
        else {
            if ($scope.SoNo != null || $scope.SoNo != '') {
                let SoNoView = $('#SoNoCRM option:selected').text();
                $.ajax({
                    type: "Post",
                    url: window.CreateWAAuthReport,
                    data: JSON.stringify({ SoNo: SoNoView, FromDate: fromdate, ToDate: Todate, ReportType: $scope.ReportType }),
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert("Dowloaded Successfully" + data);
                        if (data == 'No Records Found For Selected Item') {
                            HideLoadder();
                            alert(data);
                        }
                        else if (data != "") {
                            window.location.href = window.DownloadDoc + '?fileName=' + data;
                            HideLoadder();
                        }
                        else {
                            HideLoadder();
                            alert(data.errorMessage);
                        }
                    },
                    error: function (x, e) {
                        alert('Some error is occurred, Please try after some time.');
                        HideLoadder();
                    }
                })
            }
            else {
                alert("Select AW No")
            }

        }



    }
    $scope.GetSONoDetails = function () {

        $scope.HideFileds();
        $http.get(window.TecnicalMaster).success(function (response) {
            $scope.SonoList = "";
            if (response.length > 0) {
                $scope.SonoList = response;

            }
        }, function (error) {
            alert('failed');
        });
    }

    $scope.CreateConsolidateReport = function () {
        var fromdate = '';
        var Todate = '';
        ShowLoadder();

        if ($scope.ConReportType != null || $scope.ConReportType != '') {
            $.ajax({
                type: "Post",
                url: window.CreateWAAuthReport,
                data: JSON.stringify({ SoNo: "", FromDate: $scope.ConsolidateDateFrom, ToDate: $scope.ConsolidateDateTo, ReportType: $scope.ConReportType }),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data == 'No Records Found For Selected Item') {
                        HideLoadder();
                        alert(data);
                    }
                    else if (data != "") {
                        window.location.href = window.DownloadDoc + '?fileName=' + data;
                        HideLoadder();
                    }
                    else {
                        HideLoadder();
                        alert(data.errorMessage);
                    }
                },
                error: function (x, e) {
                    alert('Some error is occurred, Please try after some time.');
                    HideLoadder();
                }
            })
        }
        else {
            alert("Select Report Type")
        }


    }

});


//EOF