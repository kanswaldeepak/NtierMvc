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

    $scope.LoadGoodsRecieptViewPopup = function (_GRno) {
        var _actionType = "VIEW"
        var GRno = _GRno;
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, GRno: GRno },
            datatype: "JSON",
            url: window.GoodsRecieptViewPopup,
            success: function (html) {
                SetModalTitle("View Goods Reciept")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1500px");
                ShowModal();
                $scope.GetGateControlNoDetails();                
                $('#formGoodsReciept input[type=radio],input[type=text], select, textarea').prop("disabled", true);
                $('.save_results').css('display', 'none');
                $('.cancel_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '2%'
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

    $scope.LoadGoodsRecieptEditPopup = function (_GRno) {
        var _actionType = "EDIT"
        var GRno = _GRno;
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, GRno: GRno },
            datatype: "JSON",
            url: window.GoodsRecieptViewPopup,
            success: function (html) {
                SetModalTitle("Edit Purchase Order")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1500px");
                ShowModal();
                $scope.GetGateControlNoDetails(); 

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '2%'
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

    $scope.DeleteGoodsReciept = function (id) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        ShowLoadder();
        $http({ url: window.DeleteGoodsRecieptDetails, method: 'POST', data: { id: id } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    $scope.FetchGoodsRecieptList();
                    HideLoadder();
                } else {
                    alert(res, 'E');
                    HideLoadder();
                }
            }
        ).error(function (res) { showHttpErr(res); HideLoadder(); });
    }

    $scope.GetGateControlNoDetails = function () {
        var GateControlNo = $('#GRGateControlNo option:selected').val();
        var GRno = $('#GRno').val();

        $.ajax({
            url: window.GetDetailsForGateControlNo,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ GateControlNo: GateControlNo, GRNo: GRno }),
            success: function (data) {

                if (data.length > 0) {

                    $('#tableSelected').val(data[0].PRCat);

                    $('#GRPODate').val(data[0].POdate);
                    $('#GRNo').val(data[0].GRNo);
                    $('#GRPRCat').val(data[0].PRCat);
                    $('#GRSupplierName').val(data[0].SupplierName);
                    $('#GRPoNo').val(data[0].PoNo);
                    $('#GRSupplierLocation').val(data[0].SupplierLocation);
                    $('#GRPoDate').val(data[0].PoDate);
                    $('#GRSupplyTerms').val(data[0].SupplyTerms);
                    $('#GRSupplierAmount').val(data[0].SupplierAmount);
                    $('#GRSupplierLotNo').val(data[0].SupplierLotNo);

                    $('#imgPreparedBy').attr("src", "/Images/Sign/" + data[0].PreparedBySign);
                    $('#imgStoresIncharge').attr("src", "/Images/Sign/" + data[0].StoresInchargeSign);

                    switch (data[0].PRCat) {
                        case 'RM':
                            $('#tableRM').show();
                            $('#tableRM tbody').empty();

                            $.each(data, function (i, item) {
                                let rowHtml = '<tr><td><span class="RMSN">${item.SN}</span></td><td><span class="RMdescription">${item.RMdescription}</span></td><td><span class="PRqty">${item.PRqty}</span></td><td><span class="UOM">${item.UOM}</span></td><td><span class="UnitPrice">${item.UnitPrice}</span></td><td><span class="TotalPrice">${item.TotalPrice}</span></td><td class="lotdetails"><span class="LotName">${item.LotName}</span></td><td class="lotdetails"><span class="LotDate">${item.LotDate}</span></td><td class="lotdetails"><span>${item.LotQty}</span></td><td><select class= "form-control requiredValidation GRStoresName" onfocusout = "return ValidateRequiredFieldsOnFocusOut(this)"></select></td><td><select class="form-control requiredValidation GRBayNo" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRLocation" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRDirection" name="Direction" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRStoreArea" name="StoreArea" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td></tr>';
                                
                                let $row = $(rowHtml)
                                // set value of the select within this row instance
                                $row.find('.RMSN').text(item.SN);
                                $row.find('.RMdescription').text(item.RMdescription);
                                $row.find('.PRqty').text(item.PRqty);
                                $row.find('.UOM').text(item.UOM);
                                $row.find('.UnitPrice').text(item.UnitPrice);
                                $row.find('.TotalPrice').text(item.TotalPrice);
                                $row.find('.LotName').text(item.LotName);
                                $row.find('.LotDate').text(item.LotDate);

                                $row.find('.GRStoresName').html($('#GRStoresName').html());
                                $row.find('.GRBayNo').html($('#GRBayNo').html());
                                $row.find('.GRLocation').html($('#GRLocation').html());
                                $row.find('.GRDirection').html($('#GRDirection').html());
                                $row.find('.GRStoreArea').html($('#GRStoreArea').html());

                                $row.find('select.GRStoresName').val(item.StoresName);
                                $row.find('select.GRBayNo').val(item.BayNo);
                                $row.find('select.GRLocation').val(item.Location);
                                $row.find('select.GRDirection').val(item.Direction);
                                $row.find('select.GRStoreArea').val(item.StoreArea);

                                // append updated object to DOM
                                $('#tableRM > tbody:last-child').append($row);

                                if ($('#GRSupplyTerms options:select').text() == 'Single')
                                    $('.lotdetails').hide();
                                else
                                    $('.lotdetails').hide();

                                //$('.GRStoresName').val(item.StoresName);
                                //$('.GRBayNo').val(item.BayNo);
                                //$('.GRLocation').val(item.Location);
                                //$('.GRDirection').val(item.Direction);
                                //$('.GRStoreArea').val(item.StoreArea);

                            })

                            
                            $('.tblLocation').hide();
                           
                            break;
                        case 'BOI':
                            $('.tableBOI').show();
                            break;
                        case 'JW':
                            $('.tableJW').show();
                            break;
                        case 'GI':
                            $('.tableGI').show();
                            break;
                        case 'C':
                            $('.tableC').show();
                            break;
                        case 'O':
                            $('.tableO').show();
                            break;
                        default:
                            break;
                    }


                }
            },
            error: function (res) {
                alert(res);
            }
        })
    }


})  

//EOF