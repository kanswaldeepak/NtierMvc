

function saveButtonMRMBillMonitoring(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: window.SaveButtonMRMBillMonitoringDetails,
        data: data,
        success: function (result) {
            alert(result);
        },
        error: function () {
            alert(result)
        }
    });
}

function SaveMRMBillMonitoringDetails(e) {
    e.preventDefault();
    ShowLoadder();
    var arr = [];
    arr.length = 0;

    var frm = $("#formMRMBillMonitoring");
    var formData = new FormData(frm[0]);


    var Status = true;
    Status = GetFormValidationStatus("#formMRMBillMonitoring");

    let tableSelected = '#table' + $('#tableSelected').val();
    let TotalAmount = 0;

    $.each($(tableSelected + " tbody tr"), function () {
        TotalAmount = TotalAmount + Math.round($(this).find('td:eq(8) input').val());
    });


    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        HideLoadder();
        return;
    }
    else {
        $.each($(tableSelected + " tbody tr"), function () {
            arr.push({
                BMno: $("#BMno").val(),
                SupplyType: $("#MRMSupplyType").val(),
                VendorId: $("#MRMVendorId").val(),
                GateNo: $("#MRMGateNo").val(),
                GateControlNo: $("#MRMGateControlNo").val(),                
                SupplierInvNo: $("#MRMSupplierInvNo").val(),
                SupplierInvDate: $("#MRMSupplierInvDate").val(),
                Currency: $("#MRMCurrency").val(),
                SupplierInvAmount: $("#MRMSupplierInvAmount").val(),
                PaymentDueDate: $("#MRMPaymentDueDate").val(),
                VerifiedBy: $("#MRMVerifiedBy").val(),
                CostCenter: $("#MRMCostCenter").val(),
                ApprovedStatus: $("#MRMApprovedStatus").val(),
                ForwardedTo: $('#MRMForwardedTo').val(),

                SN: $(this).find('td:eq(0) span').text(),
                RMDescription: $(this).find('td:eq(1) span').text(),
                Qty: $(this).find('td:eq(2) span').text(),
                UOM: $(this).find('td:eq(3) span').text(),
                UnitPrice: $(this).find('td:eq(4) span').text(),
                TotalPrice: $(this).find('td:eq(5) span').text(),
                SACNo: $(this).find('td:eq(6) input').val(),
                GSTPercent: $(this).find('td:eq(7) input').val(),
                GSTAmount: $(this).find('td:eq(8) input').val(),
                TotalAmount: TotalAmount
                
            });
        });


        var data = JSON.stringify({
            MRMBillDetails: arr
        });

        $.when(saveButtonMRMBillMonitoring(data)).then(function (response) {
            console.log(response);
            HideLoadder();
        }).fail(function (err) {
            console.log(err);
        });
    }
};

function showApproverSign(id) {
    if ($('#' + id).is(':checked')) {
        $('#img' + id).show();
    }
    else {
        $('#img' + id).hide();
    }
}

function showHideRejectReason() {
    var selectedVal = $('#MRMApprovedStatus option:selected').text();

    if (selectedVal == 'Rejected') {
        $('#divRejectReason').show();
        $('#divPendingReason').hide();
    }
    else if (selectedVal == 'Pending') {
        $('#divPendingReason').show();
        $('#divRejectReason').hide();
    }
    else {
        $('#divRejectReason').hide();
        $('#divPendingReason').hide();
    }
}

function CalcTotal(ob) {

    let trob = $(ob).closest('tr');
    let totPrice = parseFloat($.trim(trob.find(".MRMTPrice").text()));
    if (!totPrice || isNaN(totPrice)) {
        totPrice = 0;
    }
    let gstpercent = parseFloat($.trim(trob.find(".MRMGSTPercent").val()));
    if (!gstpercent || isNaN(gstpercent)) {
        gstpercent = 0;
    }

    let gstVal = ((totPrice / 100) * gstpercent).toFixed(2);
    trob.find(".MRMGSTAmount").val((Math.round(((totPrice - gstVal) * 1000) / 10) / 100).toFixed(2));
}

function GetMRMValuesFromSupplyType() {
    let SupplyType = $('#MRMSupplyType').val();

    $.ajax({
        type: 'POST',
        url: window.GetGRValueFromSupplyType,
        data: JSON.stringify({ SupplyType: SupplyType }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#MRMGateControlNo').empty();
            $.each(data, function (i, item) {
                $('#MRMGateControlNo').append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            })
        }, error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetMRMGateControlNoDetails() {
    var GateControlNo = $('#MRMGateControlNo option:selected').val();

    $.ajax({
        url: window.MRMGateControlNoDetails,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ GateControlNo: GateControlNo }),
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
                $('#MRMGateNo').val(data[0].GateNo);
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
                            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.PRqty + '</span></td><td><span>' + item.UOM + '</span></td><td><span>' + item.UnitPrice + '</span></td><td><span class="MRMTPrice">' + item.TotalPrice + '</span></td><td><input type="text" class="MRMSacNo form-control"/></td><td><input type="text" onkeyup="CalcTotal(this)" onkeypress="return AllowNumbers(event);" class="MRMGSTPercent form-control"/></td><td><input type="text" onkeyup="CalcTotal(this);" onkeypress="return AllowNumbers(event);" class="MRMGSTAmount form-control" readonly="readonly" /></td></tr>');

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
