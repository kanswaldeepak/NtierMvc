angular.module('App').controller("DesignEngController", function ($scope, $http, $timeout, $compile) {
    $scope.VendorId = "";
    //For Pagination
    $scope.maxsize = 5;

    //Product Realisation Starts
    $scope.PRPTotalCount = 0;
    $scope.PRPageIndex = 1;
    $scope.PRPageSize = "50";
    $scope.SearchTypeId = "";
    $scope.SearchQuoteNo = "";
    $scope.SearchSONo = "";
    $scope.SearchVendorId = "";
    $scope.SearchVendorName = "";
    $scope.SearchProductGroup = "";

    $scope.FetchProductRealisationList = function () {
        $http.get(window.FetchProductRealisationList+"?pageindex=" + $scope.PRPageIndex + "&pagesize=" + $scope.PRPageSize + "&SearchTypeId=" + $scope.SearchTypeId + "&SearchQuoteNo=" + $scope.SearchQuoteNo + "&SearchSONo=" + $scope.SearchSONo + "&SearchVendorId=" + $scope.SearchVendorId + "&SearchVendorName=" + $scope.SearchVendorName + "&SearchProductGroup=" + $scope.SearchProductGroup).success(function (response) {
            $scope.PRPList = response.ListPR;
            $scope.PRPTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.FetchProductRealisationList();

    $scope.PRPageChanged = function () {
        $scope.FetchProductRealisationList();
    }

    $scope.PRChangePageSize = function () {
        $scope.PRPageIndex = 1;
        $scope.FetchProductRealisationList();
    }

    $scope.BindBOMPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.BOMPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Master BOM Detail")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        //});
    }

    $scope.BindPRPPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.PRPPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Product Realisation Plan")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        //});
    }

    $scope.LoadPRPViewPopup = function (_SoNo) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, SoNo: _SoNo },
            datatype: "JSON",
            url: window.PRPPopup,
            success: function (res) {
                //Don't Copy this for View 

                //SetModalTitle("View Product Realisation Details")
                //SetModalBody(res);
                //HideLoadder();
                var html = $compile(res)($scope);
                SetModalTitle("View Product Realisation Details")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1200px");
                ShowModal();

                //$('#formPRPDetail input[type=radio],input[type=text], select').prop("disabled", true);
                $('#save_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1200px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function () {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }

    $scope.LoadPRPEditPopup = function (_PRPId) {
        var _actionType = "EDIT";
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, PRPId: _PRPId },
            datatype: "JSON",
            url: window.PRPPopup,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Product Realisation Details")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1200px");
                ShowModal();


                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function () {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }

    $scope.DeletePRP = function (id) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        $http({ url: window.DeletePRPDetail, method: 'POST', data: { id: id } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    $scope.FetchProductRealisationList();
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
    }

    //BOM Starts
    $scope.SaveProductRealisationDetails = function () {
        var frm = $("#formPRPDetail");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formPRPDetail");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveProductRealisationDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                        $scope.FetchProductRealisationList();

                        $(".clearFields").val('');
                        $("#tblPRPDetails > tbody").empty();
                        //$("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });

        }

    }

    //BOM Starts
    $scope.SaveBOMDetails = function () {
        var frm = $("#formBOMDetails");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formBOMDetails");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveBOM, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                        //$scope.FetchProductRealisationList();
                        //$("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });

        }

    }

    
    //Bill Monitoring List Starts
    $scope.BMTotalCount = 0;
    $scope.BMPageIndex = 1;
    $scope.BMPageSize = "50";
    $scope.SearchType = "";
    $scope.SearchVendorNature = "";
    $scope.SearchVendorName = "";
    $scope.SearchBillNo = "";
    $scope.SearchBillDate = "";
    $scope.SearchItemDescription = "";
    $scope.SearchCurrency = "";
    $scope.SearchApprovalStatus = "";

    $scope.FetchBillMonitoringList = function () {
        $http.get(window.FetchBillMonitoringList+"?pageindex=" + $scope.BMPageIndex + "&pageSize=" + $scope.BMPageSize + "&SearchType=" + $scope.SearchType + "&SearchVendorNature=" + $scope.SearchVendorNature + "&SearchVendorName=" + $scope.SearchVendorName + "&SearchBillNo=" + $scope.SearchBillNo + "&SearchBillDate=" + $scope.SearchBillDate + "&SearchItemDescription=" + $scope.SearchItemDescription + "&SearchCurrency=" + $scope.SearchCurrency + "&SearchApprovalStatus=" + $scope.SearchApprovalStatus).success(function (response) {
            $scope.BOMList = response.lstVBM;
            $scope.BMTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.FetchBillMonitoringList();

    $scope.BMPageChanged = function () {
        $scope.FetchBillMonitoringList();
    }

    $scope.BMChangePageSize = function () {
        $scope.BMPageIndex = 1;
        $scope.FetchBillMonitoringList();
    }

    $scope.BindVendorsMasterBillPopUp = function () {
        var _actionType = "ADD"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.BillMonitoringPopUp,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Material Entry")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function () {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }


    $scope.LoadBillMonitoringViewPopup = function (_BillId) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, BillId: _BillId },
            datatype: "JSON",
            url: window.BillMonitoringPopUp,
            success: function (html) {
                SetModalTitle("Material Entry")
                SetModalBody(html);
                HideLoadder();
                $('#formBMDetail input[type=radio],input[type=text], select').prop("disabled", true);
                $('#save_results').css('display', 'none');
                $('#cancel_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1200px");
                ShowModal();


                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function () {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }

    $scope.LoadBillMonitoringEditPopup = function (_BillId) {
        var _actionType = "EDIT"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, BillId: _BillId },
            datatype: "JSON",
            url: window.BillMonitoringPopUp,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Material Entry")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1200px");
                setReadOnly("#formBillMonitoring");
                ShowModal();


                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function () {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        
    }


    $scope.SaveBillMonitoringDetails = function () {
        var frm = $("#formBillMonitoring");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formBillMonitoring");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveBillMonitoringDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        $scope.FetchProductRealisationList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res);
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }
    }



    //Reports Starts
    $scope.BindReportsPopup = function () {

        var _actionType = "ADD";
        $.ajax({
            type: "GET",
            data: { },
            datatype: "JSON",
            url: window.ReportsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetParamModalPanelBody('ReportsPanelBody', html);
                HideLoadder();
            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        //});
    };

})  

//EOF