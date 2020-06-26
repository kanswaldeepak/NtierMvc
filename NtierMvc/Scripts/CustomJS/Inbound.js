﻿
function GetDetailForGateEntry() {
    var POSetno = $('#GEVendorPONO').val();

    $.ajax({
        url: window.GetDetailsForGateEntry,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ POSetno: POSetno }),
        success: function (data) {

            if (data.length > 0) {
                $("#HiddenPOSetno").val(data[0].POSetno);
                $('#GEPRCat').val(data[0].PRCat);
                $('#GEPRCat').attr("disabled", true);
                $('#tableSelected').val(data[0].PRCat);

                switch (data[0].PRCat) {
                    case 'RM':
                        $('#tableRM').show();
                        $('#tableRM tbody').empty();

                        $.each(data, function (i, item) {
                            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.PRqty + '<span></td><td><span>' + item.UOM + '<span></td><td><span>' + item.UnitPrice + '<span></td><td><span>' + item.Discount + '</span>%</td><td><span>' + item.TotalPrice + '</span></td><td><input type="text" name="LotQty" class="form-control" placeholder="Enter Lot Name" /></td><td><input type="text" name="LotDate" class="form-control requiredValidation NoEndDate" placeholder="Enter Lot Date" /></td><td><input type="text" name="LotQty" class="form-control" placeholder="Enter Lot Qty" /></td></tr>');
                        })

                        $('#GEPODate').val(data[0].POdate);
                        $('#GEPOValidity').val(data[0].POValidity);
                        $('#GEWorkNo').val(data[0].WorkNo);
                        $('#GEDeliveryDate').val(data[0].DeliveryDate);
                        $('#GEPOValidity').val(data[0].POValidity);
                        $('#GEPORevNo').val(data[0].PORevNo);
                        $('#GEItemCategory').val(data[0].ItemCategory);
                        $('#GEModeOfTransport').val(data[0].ModeOfTransport);
                        $('#GEGateControlNo').val(data[0].GateControlNo);

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

function SaveInboundDetails(e) {
    e.preventDefault();

    var arr = [];
    arr.length = 0;

    var frm = $("#formInbound");
    var formData = new FormData(frm[0]);

    var Status = false;
    Status = GetFormValidationStatus("#formInbound");

    let tableSelected = '#table' + $('#GEPRCat').val();
    
    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        return;
    }
    else {

        $.each($(tableSelected + " tbody tr"), function () {
            arr.push({
                VendorPONO: $("#GEVendorPONO").val(),
                GateControlNo: $("#GEGateControlNo").val(),
                VehicleNo: $("#GEVehicleNo").val(),
                DriverName: $("#GEDriverName").val(),
                DriverContactNo: $("#GEDriverContactNo").val(),
                TimeIn: $("#GETimeIn").val(),
                TimeOut: $("#GETimeOut").val(),
                VehicleReleased: $("#GEVehicleReleased").val(),
                PRCat: $("#GEPRCat").val(),

                SN: $(this).find('td:eq(0) span').text(),
                RMdescription: $(this).find('td:eq(1) span').text(),
                PRqty: $(this).find('td:eq(2) span').text(),
                UOM: $(this).find('td:eq(3) span').text(),
                UnitPrice: $(this).find('td:eq(4) span').text(),
                Discount: $(this).find('td:eq(5) span').text(),
                FinalPrice: $(this).find('td:eq(6) span').text(),
                LotName: $(this).find('td:eq(7) input').val(),
                LotDate: $(this).find('td:eq(8) input').val(),
                LotQty: $(this).find('td:eq(9) input').val()
                
            });
        });

        var data = JSON.stringify({
            arrGE: arr
        });

        $.when(saveButtonGateEntry(data)).then(function (response) {
            console.log(response);
        }).fail(function (err) {
            console.log(err);
        });
    }
};

function saveButtonGateEntry(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: window.SaveGateEntryDetails,
        data: data,
        success: function (result) {
            alert(result);
        },
        error: function () {
            alert(result)
        }
    });
}