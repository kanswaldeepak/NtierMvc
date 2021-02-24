angular.module('App').controller("ReportController", function ($scope, $http, $timeout, $compile, $filter, $window) {

    $('.modal-dialog').draggable({
        handle: ".modal-body"
    });
    $scope.date = '';
    $scope.date1='';
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


    $scope.GenerateWAuthReport = function () {
        //var DownloadType = dwnldtype;
        var fromdate = '';
        var Todate = '';
        ShowLoadder();
        if ($scope.date != '') {
            if (new Date($scope.date).getMonth() + 1 == 4 && new Date($scope.date1).getMonth() + 1 == 3) {
                if ((new Date($scope.date1).getFullYear() - new Date($scope.date).getFullYear()) == 1) {
                    fromdate = $scope.date;
                    Todate = $scope.date1;
                    $.ajax({
                        type: "Post",
                        url: window.GenerateWAAuthReport,
                        data: JSON.stringify({ SoNo: $scope.SoNo, FromDate: fromdate, ToDate: Todate, ReportType: $scope.ReportType }),
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
                $.ajax({
                    type: "Post",
                    url: window.GenerateWAAuthReport,
                    data: JSON.stringify({ SoNo: $scope.SoNo, FromDate: fromdate, ToDate: Todate, ReportType: $scope.ReportType }),
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
    //function GetSONoDetails() {
    //    var SoNo = $("#SoNoOrder").val();
    //    if (SoNo == undefined || SoNo == '') {
    //        alert('Please Select So No');
    //        return;
    //    }

    //    $.ajax({
    //        type: 'Get',
    //        url: window.TecnicalMaster,
    //        data: {},
    //        contentType: "application/json; charset=utf-8",
    //        success: function (data) {

    //            //$('#QuoteNoOrder').val(data.QuoteNo);
    //            //$('#VendorIdOrder').val(data.VendorId);
    //            //$('#VendorNameOrder').val(data.VendorName);
    //            //$('#FileNo').val(data.FileNo);
    //            //$('#ProdGrp').val(data.ProductGroup);
    //            //$('#POEntity').val(data.PoEntity);
    //            //$('#POLocation').val(data.PoLocation);
    //            //$('#PONo').val(data.PoNo);
    //            //$('#PODate').val(data.PoDate);
    //            //$('#PODor').val(data.PoDor);
    //            //$('#Curr').val(data.Curr);
    //            //$('#ExWorkValue').val(data.ExWorkValue);
    //            //$('#PODeliveryDate').val(data.PoDeliveryDate);
    //            //$('#DeliveryTerms').val(data.DeliveryTerms);
    //            //$('#SupplyTerms').val(data.SupplyTerms);
    //            //$('#ConsigneeName').val(data.ConsigneeName);
    //            //$('#ConsigneeLocation').val(data.ConsigneeLocation);
    //            //$('#ModeOfShipment').val(data.ModeOfShipment);
    //            //$('#PaymentTerms').val(data.PaymentTerms);
    //            //$('#Inspection').val(data.Inspection);
    //            //$('#EndUser').val(data.EndUser);

    //        },
    //        error: function (x, e) {
    //            alert('Some error is occurred, Please try after some time.');
    //        }
    //    })
    //}

});


//EOF