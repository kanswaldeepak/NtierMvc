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

    $scope.PRChangePageSize = function () {
        $scope.PRPageIndex = 1;
        $scope.FetchProductRealisationList();
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

                $('#formPRPDetail input[type=radio],input[type=text], select').prop("disabled", true);
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

})    
//EOF