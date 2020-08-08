angular.module('App').controller("MRMController", function ($scope, $http, $timeout, $compile) {
    $scope.VendorId = "";
    //For Pagination
    $scope.maxsize = 5;

    //Product Realisation Starts
    $scope.PRPTotalCount = 0;
    $scope.PRPageIndex = 1;
    $scope.PRPageSize = "50";
    $scope.PRSearchVendorTypeId = "";
    $scope.PRSearchSupplierId = "";
    $scope.PRSearchRMCategory = "";
    $scope.PRSearchDeliveryDateFrom = "";
    $scope.PRSearchDeliveryDateTo = "";

    $scope.FetchPRDetailsList = function () {
        $http.get(window.FetchPRDetailsList + "?pageIndex=" + $scope.PRPageIndex + "&pageSize=" + $scope.PRPageSize + "&SearchVendorTypeId=" + $scope.PRSearchVendorTypeId + "&SearchSupplierId=" + $scope.PRSearchSupplierId + "&SearchRMCategory=" + $scope.PRSearchRMCategory + "&SearchDeliveryDateFrom=" + $scope.PRSearchDeliveryDateFrom + "&SearchDeliveryDateTo=" + $scope.PRSearchDeliveryDateTo).success(function (response) {
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
                SetModalTitle("Add Purchase Requisition");
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

    $scope.LoadPRDetailsEditPopup = function (_PRSetno) {
        var _actionType = "EDIT";
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, PRSetno: _PRSetno },
            datatype: "JSON",
            url: window.PRDetailsPopup,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Purchase Requisition");
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1500px");
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();


                $.ajax({
                    type: "POST",
                    data: { PRSetno: _PRSetno },
                    datatype: "JSON",
                    url: window.GetPRTableDetails,
                    success: function (data) {

                        if (data.length > 0) {

                            $('#tableSelected').val(data[0].PRcat);
                            $("#HiddenPRSetno").val(data[0].PRSetno);
                            $('#table' + data[0].PRcat + ' tbody tr:first').remove();
                            $('#RadioList' + data[0].PRcat).prop("checked", true);

                            switch (data[0].PRcat) {
                                case 'RM':
                                    $('#tableRM').show();
                                    $('#tableRM tbody').empty();

                                    $.each(data, function (i, item) {
                                        $('#tableRM > tbody:last-child').append('<tr><td><label class="RMSN">' + item.SN + '</label></td><td><input name="RMdescription" type="text" class="form-control RMdescription" value="' + item.RMdescription + '" /></td><td><input name="RMgrade" type="text" class="form-control RMgrade" value="' + item.RMgrade + '" /></td><td><input name="RMHardness" type="text" class="form-control RMHardness" value="' + item.RMHardness + '" /></td><td><select name="PSLlevel" class="form-control RMPSLlevel"><option value = "1">I</option><option value="2">II</option></select ></td><td><input name="OD" type="text" class="form-control RMOD" onkeypress="return validateDecimalNumbers(this, event);" value="' + item.OD + '" /></td><td><input name="WT" type="text" class="form-control RMWT" onkeypress="return validateDecimalNumbers(this, event);" value="' + item.WT + '" /></td><td><input name="Len" type="text" class="form-control RMLen" onkeypress="return validateDecimalNumbers(this, event);" value="' + item.Len + '" /></td><td><input name="QtyReqd" type="text" class="form-control RMQtyReqd" onkeypress="return AllowNumbers(event);" value="' + item.QtyReqd + '" /></td><td><input name="QtyStock" type="text" class="form-control RMQtyStock" onkeypress="return AllowNumbers(event);" value="' + item.QtyStock + '"/></td><td><input name="PRqty" type="text" class="form-control RMPRqty" onkeypress="return AllowNumbers(event);" onkeyup="CalcTotal(this)" value="' + item.PRqty + '" /></td><td><input name="UnitPrice" type="text" class="form-control RMUnitPrice" onkeyup="CalcTotal(this);" onkeypress="return validateDecimalNumbers(this, event);" value="' + item.UnitPrice + '" /></td><td><input name="TotalPrice" type="text" readonly="readonly" class="form-control RMTotalPrice" value="' + item.TotalPrice + '" /></td></tr>');


                                        $('#tableRM > tbody tr:last-child .RMPSLlevel').val(item.PSLlevel);
                                    })

                                    $('#tableRM > tbody tr:last-child').append('<td><a href = "#" id = "addNewRM" class= "btn btn-info btn-sm" onclick="addNewRM(event)" > Add Row</a></td>');

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
                    }
                })


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
            error: function () {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
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

                var html = $compile(res)($scope);
                SetModalTitle("View Purchase Requisition")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1500px");
                ShowModal();

                $.ajax({
                    type: "POST",
                    data: { PRSetno: _PRSetno },
                    datatype: "JSON",
                    url: window.GetPRTableDetails,
                    success: function (data) {

                        if (data.length > 0) {
                            $("#HiddenPRSetno").val(data[0].PRSetno);
                            //$('#RadioList' + data[0].PRcat).prop("checked", true);
                            $('#PRCatPRDetails').val(data[0].PRcat);
                            $('#PRCatPRDetails').attr("disabled", true);
                            $('#table' + data[0].PRcat + ' tbody tr:first').remove();

                            switch (data[0].PRcat) {
                                case 'RM':
                                    $('#tableRM').show();
                                    $.each(data, function (i, item) {
                                        $('#tableRM > tbody:last-child').append('<tr><td>' + item.SN + '</td><td>' + item.RMdescription + '</td><td>' + item.RMgrade + '</td><td>' + item.RMHardness + '</td><td>' + item.PSLlevel + '</td><td>' + item.OD + '</td><td>' + item.WT + '</td><td>' + item.Len + '</td><td>' + item.QtyReqd + '</td><td>' + item.QtyStock + '</td><td>' + item.PRqty + '</td><td>' + item.UnitPrice + '</td><td>' + item.TotalPrice + '</td></tr>');

                                    })
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
                    }
                })

                $('#formPRDetails input[type=radio],input[type=text], select').prop("disabled", true);
                $('#SavePRDetailButton').css('display', 'none');


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


    //Purchase Order Starts
    $scope.POTotalCount = 0;
    $scope.POPageIndex = 1;
    $scope.POPageSize = "50";
    $scope.POSearchVendorTypeId = "";
    $scope.POSearchSupplierId = "";
    $scope.POSearchRMCategory = "";
    $scope.POSearchDeliveryDateFrom = "";
    $scope.POSearchDeliveryDateTo = "";

    $scope.FetchPODetailsList = function () {
        $http.get(window.FetchPODetailsList + "?pageIndex=" + $scope.POPageIndex + "&pageSize=" + $scope.POPageSize + "&SearchVendorTypeId=" + $scope.POSearchVendorTypeId + "&SearchSupplierId=" + $scope.POSearchSupplierId + "&SearchRMCategory=" + $scope.POSearchRMCategory + "&SearchDeliveryDateFrom=" + $scope.POSearchDeliveryDateFrom + "&SearchDeliveryDateTo=" + $scope.POSearchDeliveryDateTo).success(function (response) {
            $scope.POList = response.lstPOEntity;
            $scope.POTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.FetchPODetailsList();

    $scope.POPageChanged = function () {
        $scope.FetchPODetailsList();
    }

    $scope.POChangePageSize = function () {
        $scope.POPageIndex = 1;
        $scope.FetchPODetailsList();
    }

    $scope.POChangePageSize = function () {
        $scope.POPageIndex = 1;
        $scope.FetchPODetailsList();
    }

    $scope.BindPODetailsPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.PODetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Purchase Order");
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

    $scope.LoadVendorPOViewPopup = function (_POSetno) {
        var _actionType = "VIEW"
        var POSetNo = _POSetno;

        $.ajax({
            type: "POST",
            data: { actionType: _actionType, POSetNo: POSetNo },
            datatype: "JSON",
            url: window.PODetailsPopup,
            success: function (html) {
                SetModalTitle("View Purchase Order")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1500px");
                $('#formPODetails input[type=radio],input[type=text], select, textarea').prop("disabled", true);
                $('.save_results').css('display', 'none');
                $('.cancel_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();


                $.ajax({
                    type: "GET",
                    data: { POSetno: POSetNo },
                    datatype: "JSON",
                    url: window.POTableDetails,
                    success: function (data) {

                        if (data.length > 0) {
                            $("#HiddenPRSetno").val(data[0].PRSetno);
                            $('#POCatPRDetails').val(data[0].PRcat);
                            $('#POCatPRDetails').attr("disabled", true);
                            $('#tableSelected').val(data[0].PRcat);
                            //$('#table' + data[0].PRcat + ' tbody tr:first').remove();

                            switch (data[0].PRcat) {
                                case 'RM':
                                    $('#tableRM').show();
                                    $('#tableRM tbody').empty();

                                    $.each(data, function (i, item) {
                                        let rowHtml = '<tr sn=${item.SN}> <td><span class="RMSN">${item.SN}</span></td><td><span class="RMdescription">${item.RMdescription}</span></td><td hidden><span class="RMgrade">${item.RMgrade}<span></td><td hidden><span class="RMHardness">${item.RMHardness}<span></td><td hidden><span class="RMPSLlevel">${item.PSLlevel}<span></td><td hidden><span class="RMOD">${item.OD}<span></td><td hidden><span class="RMWT">${item.WT}</span></td><td hidden><span class="RMLen">${item.Len}</span></td><td hidden><span class="RMQtyReqd">${item.QtyReqd}</span></td><td hidden><span class="RMQtyStock">${item.QtyStock}</span></td><td><span class="RMPRqty">${item.PRqty}</span></td><td><select disabled class="RMLotName form-control lotColumns"><option value="">Select</option><option value="Lot1">Lot1</option><option value="Lot2">Lot2</option><option value="Lot3">Lot3</option></select></td><td><input type="text" disabled class="form-control lotColumns RMLotQty" value="${item.LotQty}" /></td><td><input type="text" disabled class="form-control lotColumns NoEndDate RMLotDate" value="${item.LotDate}" /></td><td><span class="RMUnitPrice">${item.UnitPrice}</span></td><td><span class="PORMTPrice RMPrice">${item.TotalPrice}</span></td><td><input disabled type="text" class="PORMDiscount form-control col-md-6" value="${item.Discount}" /></td><td><input type="text" disabled class="form-control PORMFinalPrice RMTotalPrice" value="${item.TotalPrice}" /></td><td hidden><span class="RMDesc1">${item.Desc1}</span></td></tr>';
                                        // create object from html string
                                        let $row = $(rowHtml)
                                        // set value of the select within this row instance
                                        $row.find('.RMSN').text(item.SN);
                                        $row.find('.RMdescription').text(item.RMdescription);
                                        $row.find('.RMgrade').text(item.RMgrade);
                                        $row.find('.RMHardness').text(item.RMHardness);
                                        $row.find('.RMPSLlevel').text(item.PSLlevel);
                                        $row.find('.RMOD').text(item.OD);
                                        $row.find('.RMWT').text(item.WT);
                                        $row.find('.RMLen').text(item.Len);
                                        $row.find('.RMQtyReqd').text(item.QtyReqd);
                                        $row.find('.RMPRqty').text(item.PRqty);
                                        $row.find('select.lotColumns').val(item.LotName);
                                        $row.find('.RMLotQty').val(item.LotQty);
                                        $row.find('.RMLotDate').val(item.LotDate);
                                        $row.find('.RMUnitPrice').text(item.UnitPrice);
                                        $row.find('.PORMTPrice').text(item.TotalPrice);
                                        $row.find('.PORMDiscount').val(item.Discount);
                                        $row.find('.RMTotalPrice').val(item.TotalPrice);
                                        $row.find('.PORMFinalPrice').val(item.TotalPrice);
                                        $row.find('.RMDesc1').text(item.Desc1);
                                        // append updated object to DOM
                                        $('#tableRM > tbody:last-child').append($row);

                                    })

                                    $('#imgRequestedBy').attr("src", "/Images/Sign/" + data[0].EntryPersonSign);
                                    $('#imgStoreEx').attr("src", "/Images/Sign/" + data[0].ApprovePerson1Sign);
                                    $('#imgApproverSign').attr("src", "/Images/Sign/" + data[0].ApprovePerson2Sign);

                                    $('.NoEndDate').datepicker({

                                        format: 'dd-mm-yyyy',
                                        autoclose: true,
                                        changeMonth: true,
                                        changeYear: true,
                                        endDate: ''
                                    });

                                    $('.removeRMPO').on("click", function (e) {
                                        e.preventDefault();
                                        $(this).parent().parent().remove();
                                    });

                                    $('.addLotPO').on("click", function (e) {
                                        e.preventDefault();
                                        //var $tableBody = $("#tableRM");
                                        //var $trLast = $tableBody.find("tr:last");
                                        var $trLast = $(this).parent().parent();
                                        var $trNew = $trLast.clone();

                                        //$trNew.find("td:nth-last-child(2)").html('');

                                        $trNew.find("td:nth-last-child(2)").html('');
                                        $trNew.find("td:nth-last-child(3)").html('');
                                        $trNew.find("td:nth-last-child(4)").html('');
                                        $trNew.find("td:nth-last-child(5)").html('');
                                        $trNew.find("td:nth-last-child(6)").html('');
                                        $trNew.find("td:nth-last-child(7)").html('');

                                        //var suffix = $trNew.find(':input:first').attr('name').match(/\d+/);

                                        //$trNew.find("td:last").html('<a href="#" class="remove">Remove</a>');
                                        $.each($trNew.find(':input'), function (i, val) {
                                            // Replaced Name
                                            //var oldN = $(this).attr('name');
                                            //var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
                                            //$(this).attr('name', newN);
                                            //Replaced value
                                            var type = $(this).attr('type');
                                            if (type != undefined && type.toLowerCase() == "text") {
                                                $(this).attr('value', '');
                                            }

                                            // If you have another Type then replace with default value
                                            $(this).removeClass("input-validation-error");
                                            //$(this).addClass("requiredValidation");

                                        });
                                        $trLast.after($trNew);

                                        // 2. Remove
                                        $('.removeRMPO').on("click", function (e) {
                                            e.preventDefault();
                                            $(this).parent().parent().remove();
                                        });

                                        $('.NoEndDate').datepicker({
                                            format: 'dd-mm-yyyy',
                                            autoclose: true,
                                            changeMonth: true,
                                            changeYear: true,
                                            endDate: '',
                                        });

                                    });

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

                    }
                })

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

    $scope.LoadVendorPOEditPopup = function (_POSetno) {
        var _actionType = "EDIT"
        var POSetNo = _POSetno;
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, POSetNo: POSetNo },
            datatype: "JSON",
            url: window.PODetailsPopup,
            success: function (html) {
                SetModalTitle("Edit Purchase Order")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1500px");
                ShowModal();

                $.ajax({
                    type: "GET",
                    data: { POSetno: POSetNo },
                    datatype: "JSON",
                    url: window.POTableDetails,
                    success: function (data) {

                        if (data.length > 0) {
                            $("#HiddenPRSetno").val(data[0].PRSetno);
                            $('#POCatPRDetails').val(data[0].PRcat);
                            $('#POCatPRDetails').attr("disabled", true);
                            $('#tableSelected').val(data[0].PRcat);
                            //$('#table' + data[0].PRcat + ' tbody tr:first').remove();

                            switch (data[0].PRcat) {
                                case 'RM':
                                    $('#tableRM').show();
                                    $('#tableRM tbody').empty();

                                    $.each(data, function (i, item) {
                                        let rowHtml = '<tr sn="' + item.SN + '"> <td><span class="RMSN">${item.SN}</span></td><td><span class="RMdescription">${item.RMdescription}</span></td><td hidden><span class="RMgrade">${item.RMgrade}<span></td><td hidden><span class="RMHardness">${item.RMHardness}<span></td><td hidden><span class="RMPSLlevel">${item.PSLlevel}<span></td><td hidden><span class="RMOD">${item.OD}<span></td><td hidden><span class="RMWT">${item.WT}</span></td><td hidden><span class="RMLen">${item.Len}</span></td><td hidden><span class="RMQtyReqd">${item.QtyReqd}</span></td><td hidden><span class="RMQtyStock">${item.QtyStock}</span></td><td><span class="RMPRqty">${item.PRqty}</span></td><td><select disabled class="RMLotName form-control lotColumns"><option value="">Select</option><option value="Lot1">Lot1</option><option value="Lot2">Lot2</option><option value="Lot3">Lot3</option></select></td><td><input type="text" disabled class="form-control lotColumns RMLotQty" value="${item.LotQty}" /></td><td><input type="text" disabled class="form-control lotColumns NoEndDate RMLotDate" value="${item.LotDate}" /></td><td><span class="RMUnitPrice">${item.UnitPrice}</span></td><td><span class="PORMTPrice RMPrice">${item.TotalPrice}</span></td><td><input type="text" class="PORMDiscount form-control col-md-6" value="${item.Discount}" /></td><td><input type="text" class="form-control PORMFinalPrice RMTotalPrice" value="${item.TotalPrice}" /></td><td hidden><span class="RMDesc1">${item.Desc1}</span></td><td><a href="#" class="addLotPO">Add Lot Details</a></td><td><a href="#" class="removeRMPO">Remove</a></td></tr>';
                                        // create object from html string
                                        let $row = $(rowHtml)
                                        // set value of the select within this row instance

                                        $row.find('.RMSN').text(item.SN);
                                        $row.find('.RMdescription').text(item.RMdescription);
                                        $row.find('.RMgrade').text(item.RMgrade);
                                        $row.find('.RMHardness').text(item.RMHardness);
                                        $row.find('.RMPSLlevel').text(item.PSLlevel);
                                        $row.find('.RMOD').text(item.OD);
                                        $row.find('.RMWT').text(item.WT);
                                        $row.find('.RMLen').text(item.Len);
                                        $row.find('.RMQtyReqd').text(item.QtyReqd);
                                        $row.find('.RMPRqty').text(item.PRqty);
                                        $row.find('select.lotColumns').val(item.LotName);
                                        $row.find('.RMLotQty').val(item.LotQty);
                                        $row.find('.RMLotDate').val(item.LotDate);
                                        $row.find('.RMUnitPrice').text(item.UnitPrice);
                                        $row.find('.PORMTPrice').text(item.TotalPrice);
                                        $row.find('.PORMDiscount').val(item.Discount);
                                        $row.find('.RMTotalPrice').val(item.TotalPrice);
                                        $row.find('.PORMFinalPrice').val(item.FinalPrice);
                                        $row.find('.RMDesc1').text(item.Desc1);
                                        // append updated object to DOM
                                        $('#tableRM > tbody:last-child').append($row);

                                    })

                                    $('#imgRequestedBy').attr("src", "/Images/Sign/" + data[0].EntryPersonSign);
                                    $('#imgStoreEx').attr("src", "/Images/Sign/" + data[0].ApprovePerson1Sign);
                                    $('#imgApproverSign').attr("src", "/Images/Sign/" + data[0].ApprovePerson2Sign);

                                    $('.NoEndDate').datepicker({

                                        format: 'dd-mm-yyyy',
                                        autoclose: true,
                                        changeMonth: true,
                                        changeYear: true,
                                        endDate: ''
                                    });

                                    $('.removeRMPO').on("click", function (e) {
                                        e.preventDefault();
                                        $(this).parent().parent().remove();
                                    });

                                    $('.addLotPO').on("click", function (e) {
                                        e.preventDefault();
                                        //var $tableBody = $("#tableRM");
                                        //var $trLast = $tableBody.find("tr:last");
                                        var $trLast = $(this).parent().parent();
                                        var $trNew = $trLast.clone();

                                        //$trNew.find("td:nth-last-child(2)").html('');

                                        $trNew.find("td:nth-last-child(2)").html('');
                                        $trNew.find("td:nth-last-child(3)").html('');
                                        $trNew.find("td:nth-last-child(4)").html('');
                                        $trNew.find("td:nth-last-child(5)").html('');
                                        $trNew.find("td:nth-last-child(6)").html('');
                                        $trNew.find("td:nth-last-child(7)").html('');

                                        //var suffix = $trNew.find(':input:first').attr('name').match(/\d+/);

                                        //$trNew.find("td:last").html('<a href="#" class="remove">Remove</a>');
                                        $.each($trNew.find(':input'), function (i, val) {
                                            // Replaced Name
                                            //var oldN = $(this).attr('name');
                                            //var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
                                            //$(this).attr('name', newN);
                                            //Replaced value
                                            var type = $(this).attr('type');
                                            if (type != undefined && type.toLowerCase() == "text") {
                                                $(this).attr('value', '');
                                            }

                                            // If you have another Type then replace with default value
                                            $(this).removeClass("input-validation-error");
                                            //$(this).addClass("requiredValidation");

                                        });
                                        $trLast.after($trNew);

                                        // 2. Remove
                                        $('.removeRMPO').on("click", function (e) {
                                            e.preventDefault();
                                            $(this).parent().parent().remove();
                                        });

                                        $('.NoEndDate').datepicker({
                                            format: 'dd-mm-yyyy',
                                            autoclose: true,
                                            changeMonth: true,
                                            changeYear: true,
                                            endDate: '',
                                        });

                                    });

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

                    }
                })


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

    $scope.DeleteVendorPODetails = function (id) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        $http({ url: window.DeleteVendorPODetails, method: 'POST', data: { id: id } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    $scope.FetchPODetailsList();
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
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

    //Bill Monitoring List Starts
    $scope.BMTotalCount = 0;
    $scope.BMPageIndex = 1;
    $scope.BMPageSize = "50";
    $scope.MRMSearchVendorTypeId = "";
    $scope.MRMSearchSupplierId = "";
    $scope.MRMSearchSupplierName = "";
    $scope.MRMSearchApprovedDate = "";
    $scope.MRMSearchTotalAmount = "";

    $scope.FetchBMList = function () {
        $http.get(window.FetchBillMonitoringList + "?pageindex=" + $scope.BMPageIndex + "&pageSize=" + $scope.BMPageSize + "&SearchVendorTypeId=" + $scope.MRMSearchVendorTypeId + "&SearchSupplierId=" + $scope.MRMSearchSupplierId + "&MRMSearchSupplierName=" + $scope.MRMSearchSupplierName + "&SearchApprovedDate=" + $scope.SearchApprovedDate + "&SearchGSTAmount=" + $scope.SearchGSTAmount).success(function (response) {
            $scope.BOMList = response.lstMRMEntity;
            $scope.BMTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }


    $scope.BMPageChanged = function () {
        $scope.FetchBillMonitoringList();
    }

    $scope.BMChangePageSize = function () {
        $scope.BMPageIndex = 1;
        $scope.FetchBillMonitoringList();
    }

    $scope.MRMBindVendorsMasterBillPopUp = function () {
        var _actionType = "ADD"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, BillId: '' },
            datatype: "JSON",
            url: window.MRMBillMonitoringPopUp,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Bill Monitoring")
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


    $scope.LoadBillMonitoringViewPopup = function (_BMno) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, BMno: _BMno },
            datatype: "JSON",
            url: window.MRMBillMonitoringPopUp,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("View Bill Monitoring")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                ShowModal();
                $scope.GetMRMGateControlNoDetails();

                $('#formMRMBillMonitoring input[type=radio],input[type=text], select, textarea').prop("disabled", true);
                $('.save_results').css('display', 'none');
                $('.cancel_results').css('display', 'none');
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
    $scope.LoadBillMonitoringEditPopup = function (_BMno) {
        var _actionType = "EDIT"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, BMno: _BMno },
            datatype: "JSON",
            url: window.MRMBillMonitoringPopUp,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Edit Bill Monitoring")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                ShowModal();
                $scope.GetMRMGateControlNoDetails();
         
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


    $scope.GetMRMGateControlNoDetails = function() {
        var GateControlNo = $('#MRMGateControlNo option:selected').val();
        var BMno = $('#BMno').val();

        $.ajax({
            url: window.MRMGateControlNoDetails,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ GateControlNo: GateControlNo, BMno: BMno }),
            success: function (data) {

                if (data.length > 0) {

                    $('#tableSelected').val(data[0].PRCat);
                    $('#MRMVendorNatureId').val(data[0].VendorNatureId);
                    $('#MRMVendorId').val(data[0].VendorId);
                    $('#MRMVendorName').val(data[0].VendorName);
                    $('#MRMCity').val(data[0].City);
                    $('#MRMEndUse').val(data[0].EndUse);
                    $('#MRMEndUseNo').val(data[0].EndUseNo);
                    $('#MRMFunctionalAreaId').val(data[0].FunctionalAreaId);
                    $('#MRMVendorPONO').val(data[0].VendorPONO);
                    $('#MRMVendorPODate').val(data[0].VendorPODate);
                    $('#MRMVehicleNo').val(data[0].VehicleNo);
                    $('#MRMDriverName').val(data[0].DriverName);
                    $('#MRMDriverContactNo').val(data[0].DriverContactNo);
                    $('#MRMTimeIn').val(data[0].TimeIn);
                    $('#MRMTimeOut').val(data[0].TimeOut);
                    $('#MRMVehicleReleased').val(data[0].VehicleReleased);
                    $('#MRMGRNo').val(data[0].GRNo);
                    $('#MRMGRDate').val(data[0].GRDate);
                    $('#MRMCostCenter').val(data[0].CostCenter);
                    $('#MRMSupplyTerms').val(data[0].SupplyTerms);
                    $('#MRMSupplierInvNo').val(data[0].SupplierInvNo);
                    $('#MRMSupplierInvDate').val(data[0].SupplierInvDate);
                    $('#MRMCurrency').val(data[0].Currency);
                    $('#MRMSupplierInvAmount').val(data[0].SupplierInvAmount);


                    switch (data[0].PRCat) {
                        case 'RM':
                            $('#tableRM').show();
                            $('#tableRM tbody').empty();

                            $.each(data, function (i, item) {
                                $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.PRqty + '</span></td><td><span>' + item.UOM + '</span></td><td><span>' + item.UnitPrice + '</span></td><td><span class="MRMTPrice">' + item.TotalPrice + '</span></td><td><input type="text" class="MRMSacNo form-control" value="' + item.SACNo + '" /></td><td><input type="text" onkeyup="CalcTotal(this)" onkeypress="return AllowNumbers(event);" class="MRMGSTPercent form-control" value="' + item.GSTPercent + '" /></td><td><input type="text" onkeyup="CalcTotal(this);" onkeypress="return AllowNumbers(event);" class="MRMGSTAmount form-control" readonly="readonly"  value="' + item.GSTAmount +'"/></td></tr>');

                                if ($('#MRMSupplyTerms options:select').text() == 'Single')
                                    $('.lotdetails').hide();
                                else
                                    $('.lotdetails').hide();

                            })

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