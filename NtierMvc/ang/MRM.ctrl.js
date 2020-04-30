angular.module('App').controller("MRMController", function ($scope, $http, $timeout, $compile) {
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

    $scope.FetchPRDetailsList = function () {
        $http.get(window.FetchPRDetailsList+"?pageIndex=" + $scope.PRPageIndex + "&pageSize=" + $scope.PRPageSize + "&SearchTypeId=" + $scope.SearchTypeId + "&SearchQuoteNo=" + $scope.SearchQuoteNo + "&SearchSONo=" + $scope.SearchSONo + "&SearchVendorId=" + $scope.SearchVendorId + "&SearchVendorName=" + $scope.SearchVendorName + "&SearchProductGroup=" + $scope.SearchProductGroup).success(function (response) {
            $scope.PRPList = response.lstPREntity;
            $scope.PRPTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.FetchPRDetailsList();

    $scope.PRPageChanged = function () {
        $scope.FetchPRDetailsList();
    }

    $scope.PRChangePageSize = function () {
        $scope.PRPageIndex = 1;
        $scope.FetchPRDetailsList();
    }

    $scope.PRChangePageSize = function () {
        $scope.PRPageIndex = 1;
        $scope.FetchPRDetailsList();
    }

    $scope.BindPRDetailsPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.PRDetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Purchase Requisition")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1500px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '0%'
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

    $scope.LoadPRDetailsViewPopup = function (_PRSetno) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, PRSetno: _PRSetno },
            datatype: "JSON",
            url: window.PRDetailsPopup,
            success: function (res) {
                //Don't Copy this for View 

                //SetModalTitle("View Product Realisation Details")
                //SetModalBody(res);
                //HideLoadder();
                var html = $compile(res)($scope);
                SetModalTitle("Purchase Requisition")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1500px");
                ShowModal();

                $('#formPRDetails input[type=radio],input[type=text], select').prop("disabled", true);
                $('#save_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                
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

})    
//EOF