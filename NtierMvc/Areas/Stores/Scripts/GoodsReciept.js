
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

function GetDetailForGateControlNo() {
    var GateControlNo = $('#GRGateControlNo').val();

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
                            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.PRqty + '</span></td><td><span>' + item.UOM + '</span></td><td><span>' + item.UnitPrice + '</span></td><td><span>' + item.TotalPrice + '</span></td><td><span>' + item.LotName + '</span></td><td><span>' + item.LotDate + '</span></td><td><span>' + item.LotQty + '</span></td><td><button class="btn btn-info btnLoc"><i class="fa fa-plus"></i></button></td></tr><tr class="tblLocation"><td colspan="9"><div class="row"><div class="col-md-12"><h3>Inventory Allocations</h3><h3></h3></div><div class="col-md-2"><div class="form-group"><label class="control-label">Stores Name<span class="required">*</span></label><select class= "form-control requiredValidation" id = "GRStoresName" name = "StoresName" onfocusout = "return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option><option value="508">AssyTool</option><option value="509">BoughtOutItems</option><option value="510">Elastomers</option><option value="511">FinishedGoods</option><option value="512">Gauges</option><option value="513">GeneralItems</option><option value="514">EnggItems</option><option value="515">PackingGoods</option><option value="516">RMYARD</option><option value="517">StationeryItems</option></select ></div></div><div class="col-md-2"><div class="form-group"><label class="control-label">BAY NO<span class="required">*</span></label><select class="form-control requiredValidation" id="GRBayNo" name="BayNo" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option><option value = "528">B1</option ><option value="529">B2</option><option value="530">B3</option><option value="531">B4</option><option value="532">B5</option><option value="533">B6</option><option value="534">B7</option><option value="535">B8</option><option value="536">B9</option><option value="537">B10</option></select ></div></div><div class="col-md-2"><div class="form-group"><label class="control-label">LOCATION<span class="required">*</span></label><select class="form-control requiredValidation" id="GRLocation" name="Location" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option><option value="538">A</option><option value="539">B</option><option value="540">C</option><option value="541">D</option><option value="542">E</option><option value="543">F</option><option value="544">G</option><option value="545">H</option><option value="546">I</option><option value="547">J</option></select></div></div><div class="col-md-2"><div class="form-group"><label class="control-label">POSTION<span class="required">*</span></label><select class= "form-control requiredValidation" id = "GRPosition" name = "Position" onfocusout = "return ValidateRequiredFieldsOnFocusOut(this)" > <option value="">Select</option><option value="568">LR</option><option value="569">RL</option><option value="570">TB</option><option value="571">BT</option><option value="572">FR</option><option value="573">BK</option><option value="574">ND</option></select ></div></div><div class="col-md-2"><div class="form-group"><label class="control-label">DIRECTION<span class="required">*</span></label><select class="form-control requiredValidation" id="GRPostion" name="Direction" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option><option value = "568">LR</option><option value="569">RL</option><option value="570">TB</option><option value="571">BT</option><option value="572">FR</option><option value="573">BK</option><option value="574">ND</option></select></div></div><div class="col-md-2"><div class="form-group"><label class="control-label">STOREAREA<span class="required">*</span></label><select class="form-control requiredValidation" id="GRStoreArea" name="StoreArea" onfocusout="return ValidateRequiredFieldsOnFocusOut(this)"><option value="">Select</option>< option value = "575" > OP</option ><option value="576">MF</option><option value="577">GF</option><option value="578">FF</option><option value="579">SF</option><option value="580">TF</option><option value="581">MN</option></select></div></div></div><td></tr>');

                            $('.tblLocation').hide();
                            $('.btnLoc').on('click', function () {
                                $(this).parent().parent().next('tr.showhide');
                            })
                            //$('#GRSN').append('<option value="' + item.SN + '">' + item.SN + '</option>');

                            

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

