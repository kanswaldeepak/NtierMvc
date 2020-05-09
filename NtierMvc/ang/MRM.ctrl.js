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
        $http.get(window.FetchPRDetailsList + "?pageIndex=" + $scope.PRPageIndex + "&pageSize=" + $scope.PRPageSize + "&SearchTypeId=" + $scope.SearchTypeId + "&SearchQuoteNo=" + $scope.SearchQuoteNo + "&SearchSONo=" + $scope.SearchSONo + "&SearchVendorId=" + $scope.SearchVendorId + "&SearchVendorName=" + $scope.SearchVendorName + "&SearchProductGroup=" + $scope.SearchProductGroup).success(function (response) {
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
                            $('#RadioList' + data[0].PRcat).prop("checked", true);
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

})
//EOF