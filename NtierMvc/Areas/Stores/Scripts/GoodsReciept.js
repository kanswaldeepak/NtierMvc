
function GetGRValuesFromSupplyType() {
    let SupplyType = $('#GRSupplyType').val();

    $.ajax({
        type: 'POST',
        url: window.GetGRValueFromSupplyType,
        data: JSON.stringify({ SupplyType: SupplyType }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#GRGateControlNo').empty();
            $.each(data, function (i, item) {
                $('#GRGateControlNo').append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            })
        }, error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function showRequestedSign(id) {
    if ($('#' + id).is(':checked')) {
        $('#img' + id).show();
    }
    else {
        $('#img' + id).hide();     
    }
}




function GetDetailForGateControlNo() {
    var GateControlNo = $('#GRGateControlNo option:selected').val();
    
    $.ajax({
        url: window.GetDetailsForGateControlNo,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ GateControlNo: GateControlNo }),
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

                $('#imgPreparedBy').attr("src", "/Images/Sign/" + data[0].PreparedBy);
                $('#imgStoresIncharge').attr("src", "/Images/Sign/" + data[0].StoresIncharge);

                switch (data[0].PRCat) {
                    case 'RM':
                        $('#tableRM').show();
                        $('#tableRM tbody').empty();

                        $.each(data, function (i, item) {
                            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.PRqty + '</span></td><td><span>' + item.UOM + '</span></td><td><span>' + item.UnitPrice + '</span></td><td><span>' + item.TotalPrice + '</span></td><td class="lotdetails"><span>' + item.LotName + '</span></td><td class="lotdetails"><span>' + item.LotDate + '</span></td><td class="lotdetails"><span>' + item.LotQty + '</span></td><td><select class= "form-control requiredValidation GRStoresName" name = "StoresName" onfocusout = "return ValidateRequiredFieldsOnFocusOut(this)"></select></td><td><select class="form-control requiredValidation GRBayNo" name="BayNo" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRLocation" name="Location" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRDirection" name="Direction" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td><td><select class="form-control requiredValidation GRStoreArea" name="StoreArea" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option></select></td></tr>');

                            //$('#GRSN').append('<option value="' + item.SN + '">' + item.SN + '</option>');

                            if ($('#GRSupplyTerms options:select').text() == 'Single')
                                $('.lotdetails').hide();
                            else
                                $('.lotdetails').hide();


                        })

                        $('.GRStoresName').html($('#GRStoresName').html());
                        $('.GRBayNo').html($('#GRBayNo').html());
                        $('.GRLocation').html($('#GRLocation').html());
                        $('.GRDirection').html($('#GRDirection').html());
                        $('.GRStoreArea').html($('#GRStoreArea').html());

                        $('.tblLocation').hide();
                        //$('.btnLoc').on("click", function (e) {
                        //    e.preventDefault();
                        //    $(this).parent().parent().next().toggle();
                        //});


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

function saveButtonGoodsRecieptDetails(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: window.SaveGoodsRecieptEntryDetails,
        data: data,
        success: function (result) {
            alert(result);
        },
        error: function () {
            alert(result)
        }
    });
}

function SaveGoodsReciept(e) {
    e.preventDefault();

    var arr = [];
    arr.length = 0;

    var frm = $("#formGoodsReciept");
    var formData = new FormData(frm[0]);


    var Status = true;
    Status = GetFormValidationStatus("#formGoodsReciept");

    let tableSelected = '#table' + $('#tableSelected').val();
    
    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        return;
    }
    else if ($('#StoresIncharge').prop("checked") == false) {
        alert("Kindly tick Stores Incharge Checkbox");
        return;
    }
    else {
        $.each($(tableSelected + " tbody tr"), function () {
            arr.push({
                //Id: $("#GRId").val(),
                SupplyType: $("#GRSupplyType").val(),
                GRno: $("#GRno").val(),
                GoodRecieptNo: $("#GRGoodRecieptNo").val(),
                GRDate: $("#GRDate").val(),
                PRCat: $("#GRPRCat").val(),
                PoNo: $("#GRPoNo").val(),
                POSetno: $("#GRPOSetno").val(),
                SupplyTerms: $("#GRSupplyTerms").val(),
                BatchNo: $("#GRBatchNo").val(),
                HeatNo: $("#GRHeatNo").val(),
                InspectionReportNo: $("#GRInspectionReportNo").val(),
                TestCertificationNo: $("#GRTestCertificationNo").val(),
                SupplierInvNo: $("#GRSupplierInvNo").val(),
                SupplierDate: $("#GRSupplierDate").val(),
                SupplierName: $("#GRSupplierName").val(),
                SupplierLocation: $("#GRSupplierLocation").val(),
                SupplierLotNo: $("#GRSupplierLotNo").val(),
                PreparedBy: $("#GRPreparedById").val(),

                SN: $(this).find('td:eq(0) span').text(),
                StoresName: $(this).find('td:eq(9) select').val(),
                BayNo: $(this).find('td:eq(10) select').val(),
                Location: $(this).find('td:eq(11) select').val(),
                Direction: $(this).find('td:eq(12) select').val(),
                StoreArea: $(this).find('td:eq(13) select').val()
            });
        });


        var data = JSON.stringify({
            StoreDetails: arr
        });

        $.when(saveButtonGoodsRecieptDetails(data)).then(function (response) {
            console.log(response);
        }).fail(function (err) {
            console.log(err);
        });
    }
};


