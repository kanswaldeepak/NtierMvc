angular.module('App').controller("StoresController", function ($scope, $http, $timeout, $compile) {
    $scope.VendorId = "";
    //For Pagination
    $scope.maxsize = 5;

    //Goods Reciept Starts
    $scope.GRTotalCount = 0;
    $scope.GRPageIndex = 1;
    $scope.GRPageSize = "50";
    $scope.SearchTypeId = "";
    $scope.SearchQuoteNo = "";
    $scope.SearchSONo = "";
    $scope.SearchVendorId = "";
    $scope.SearchVendorName = "";
    $scope.SearchProductGroup = "";

    $scope.FetchGoodsRecieptList = function () {
        $http.get(window.FetchGoodsRecieptList+"?pageindex=" + $scope.GRPageIndex + "&pagesize=" + $scope.GRPageSize).success(function (response) {
            $scope.GRList = response.lstGREntity;
            $scope.GRTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.FetchGoodsRecieptList();

    $scope.GRPageChanged = function () {
        $scope.FetchGoodsRecieptList();
    }

    $scope.GRChangePageSize = function () {
        $scope.GRPageIndex = 1;
        $scope.FetchGoodsRecieptList();
    }

    $scope.BindGoodsRecieptPopUp = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.GRPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Goods Reciept Form")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1400px");
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

    //Quotation Modal Starts
    $scope.SaveGoodsReciept = function () {
        var frm = $("#formGoodsReciept");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formGoodsReciept");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
            //alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveGoodsRecieptEntryDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        $scope.FetchGoodsRecieptList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }

    }
    
})  

//EOF