

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

                //$('#tableSelected').val(data[0].PRCat);
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
                $('#MRMCostCentre').val(data[0].CostCentre);
                
                
                //switch (data[0].PRCat) {
                //    case 'RM':
                //        $('#tableRM').show();
                //        $('#tableRM tbody').empty();

                //        $.each(data, function (i, item) {
                //            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.PRqty + '</span></td><td><span>' + item.UOM + '</span></td><td><span>' + item.UnitPrice + '</span></td><td><span>' + item.TotalPrice + '</span></td><td class="lotdetails"><span>' + item.LotName + '</span></td><td class="lotdetails"><span>' + item.LotDate + '</span></td><td class="lotdetails"><span>' + item.LotQty + '</span></td><td><select class= "form-control requiredValidation GRStoresName" name = "StoresName" onfocusout = "return ValidateRequiredFieldsOnFocusOut(this)"></select></td><td><select class="form-control requiredValidation GRBayNo" name="BayNo" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRLocation" name="Location" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRDirection" name="Direction" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRStoreArea" name="StoreArea" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td></tr>');

                //            if ($('#GRSupplyTerms options:select').text() == 'Single')
                //                $('.lotdetails').hide();
                //            else
                //                $('.lotdetails').hide();

                //        })

                //        $('.GRStoresName').html($('#GRStoresName').html());
                //        $('.GRBayNo').html($('#GRBayNo').html());
                //        $('.GRLocation').html($('#GRLocation').html());
                //        $('.GRDirection').html($('#GRDirection').html());
                //        $('.GRStoreArea').html($('#GRStoreArea').html());

                //        $('.tblLocation').hide();


                //        break;
                //    case 'BOI':
                //        $('.tableBOI').show();
                //        break;
                //    case 'JW':
                //        $('.tableJW').show();
                //        break;
                //    case 'GI':
                //        $('.tableGI').show();
                //        break;
                //    case 'C':
                //        $('.tableC').show();
                //        break;
                //    case 'O':
                //        $('.tableO').show();
                //        break;
                //    default:
                //        break;
                //}


            }
        },
        error: function (res) {
            alert(res);
        }
    })
}
