

//After Click Save Button Pass All Data View To Controller For Save Database
function saveButton(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: window.SaveItemDetailList,
        data: data,
        success: function (result) {
            alert(result);

            var SoNoItem = $("#ItemSoNo").val();
            var quoteTypeId = $("#OrderFormQuoteType").val();

            $.ajax({
                type: 'POST',
                url: window.GetOrderDetailsFromSO,
                data: JSON.stringify({ SoNo: SoNoItem, quoteTypeId: quoteTypeId }),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('.OrderPercentClass').html('Order Percentage ' + data.iEntity.POPercent + ' %');

                },
                error: function (x, e) {
                    alert('Some error is occurred, Please try after some time.');
                    //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
                    //$('#spn-Sucess-Failure').addClass("important red");
                    //$('#Sucess-Failure').modal('show');
                }
            })

        },
        error: function () {
            alert(result)
        }
    });
}


function GetVendorDetails() {
    var VendorId = $("#VendorFormId").val();
    $.ajax({
        type: 'POST',
        url: window.VendorDetail,
        data: JSON.stringify({ vendorId: VendorId }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#VendorNameOrder").val(res.quoteEntity.VendorName);
            $("#Country").val(res.quoteEntity.Country);
            $("#CountryId").val(res.quoteEntity.CountryId);
            $("#GeoArea").val(res.quoteEntity.GeoArea);
            $("#GeoCode").val(res.quoteEntity.GeoCode);
            $("#EnqNo").empty();
            if (res.lstDdEntity.length > 0) {
                $.each(res.lstDdEntity, function (i, item) {
                    $("#EnqNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetQuoteNos() {
    var QuoteType = $("#OrderFormQuoteType").val();
    debugger;
    $.ajax({
        type: 'POST',
        url: window.GetPrepQuoteNo,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#QuoteOrderFormNo").empty();
            if (data.lstQuoteNo.length > 0) {
                $.each(data, function (i, item) {
                    $("#QuoteOrderFormNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetItemOrderDetailsFromSO() {
    var SoNoItem = $("#ItemSoNo").val();
    var quoteTypeId = $("#OrderFormQuoteType").val();

    if (quoteTypeId == undefined || quoteTypeId == '') {
        alert('Kindly Select Order Type.');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetOrderDetailsFromSO,
        data: JSON.stringify({ SoNo: SoNoItem, quoteTypeId: quoteTypeId }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#VendorFormName').val(data.iEntity.VendorName);
            $('#VendorFormId').val(data.iEntity.VendorId);
            $('#PONo').val(data.iEntity.PoNo);
            $('#PODate').val(data.iEntity.PoDate);
            $('#PODeliveryDate').val(data.iEntity.PoDeliveryDate);
            $('#SupplyTerms').val(data.iEntity.SupplyTerms);
            $('#QuoteItemFormNo').val(data.iEntity.QuoteNo);
            $('#ExWorkValue').val(data.iEntity.ExWorkValue);
            $('.OrderPercentClass').html('Order Percentage ' + data.iEntity.POPercent + ' %');

            //$("#SupplyTerms").attr('disabled', true);

            if (data.iEntity.SupplyTermsText == 'Single') {
                $(".forSingle").prop('disabled', true);
                $('.forSingle').removeClass('requiredValidation')
            }
            else {
                $(".forSingle").prop('disabled', false);
                $('.forSingle').addClass('requiredValidation');
            }

            $('#QuoteItemFormNo').empty();
            if (data.lstQuoteNo.length > 0) {
                $.each(data.lstQuoteNo, function (i, item) {
                    $("#QuoteItemFormNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }

            $('#QuotePrepId').empty();
            if (data.lstQuoteItemSlNo.length > 0) {
                $.each(data.lstQuoteItemSlNo, function (i, item) {
                    $("#QuotePrepId").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })
}


function GetQuoteItemDetails() {
    var ItemNo = $("#QuotePrepId").val();
    var itemString = "";
    $.ajax({
        type: 'POST',
        url: window.GetItemQuoteDetails,
        data: JSON.stringify({ itemNo: ItemNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#POSlNo').val($('#QuotePrepId option:selected').text());
            $("#ItemDescription").val(data.DataValueField1);
            $('#itemPOQty').val(data.DataValueField2);
            $('#itemUnitPrice').val(data.DataValueField3);
            $("#divQuoteDetails").show();

            //if (data.length > 0) {
            //    $.each(data, function (i, item) {
            //        itemString = itemString + item.DataColumnValueField + '\n\n';
            //        //$("#QuotePrepFormNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            //        $('#itemPOQty').val(item.DataColumnValueField1);
            //        $('#itemUnitPrice').val(item.DataColumnValueField2);
            //    })
            //    $("#ItemDescription").val(itemString);

            //}
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetQuoteDetails() {
    var QuoteType = $("#OrderFormQuoteType").val();
    var QuoteNo = $("#QuoteItemFormNo").val();

    if ((QuoteType == undefined || QuoteType == '') && (QuoteNo == undefined || QuoteNo == '')) {
        alert('Please Select Order Type and Quote No');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetQuoteItemPODetails,
        data: JSON.stringify({ quoteType: QuoteType, quoteNo: QuoteNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('#itemPOQty').val(data.DataValueField2);
            $('#itemUnitPrice').val(data.DataValueField1);
            $("#itemUnitPrice").attr('readonly', true);
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetQuoteOrderItemSlNos() {
    var QuoteType = $("#OrderFormQuoteType").val();
    var QuoteNo = $("#QuoteItemFormNo").val();

    if ((QuoteType == undefined || QuoteType == '') && (QuoteNo == undefined || QuoteNo == '')) {
        alert('Please Select Order Type and Quote No');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetQuoteItemSlNos ,
        data: JSON.stringify({ quoteType: QuoteType, quoteNo: QuoteNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#QuotePrepId').empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#QuotePrepId").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetQuoteNoDetails() {
    var QuoteType = $("#OrderFormQuoteType").val();
    if (QuoteType == undefined || QuoteType == '') {
        alert('Please Select Quote Type');
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/Technical/GetOrderQuoteNo',
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //$('#QuoteItemFormNo').empty();
            //if (data.lstQuoteNo.length > 0) {
            //    $.each(data.lstQuoteNo, function (i, item) {
            //        $("#QuoteItemFormNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            //    })
            //}

            $('#ItemSoNo').empty();
            if (data.lstSoNo.length > 0) {
                $.each(data.lstSoNo, function (i, item) {
                    $("#ItemSoNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetSONoDetails() {
    var SoNo = $("#SoNoOrder").val();
    if (SoNo == undefined || SoNo == '') {
        alert('Please Select So No');
        return;
    }

    $.ajax({
        type: 'POST',
        url: '/Technical/GetSoNoDetails',
        data: JSON.stringify({ soNo: SoNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('#QuoteNoOrder').val(data.QuoteNo);
            $('#VendorIdOrder').val(data.VendorId);
            $('#VendorNameOrder').val(data.VendorName);
            $('#FileNo').val(data.FileNo);
            $('#ProdGrp').val(data.ProductGroup);
            $('#POEntity').val(data.PoEntity);
            $('#POLocation').val(data.PoLocation);
            $('#PONo').val(data.PoNo);
            $('#PODate').val(data.PoDate);
            $('#PODor').val(data.PoDor);
            $('#Curr').val(data.Curr);
            $('#ExWorkValue').val(data.ExWorkValue);
            $('#PODeliveryDate').val(data.PoDeliveryDate);
            $('#DeliveryTerms').val(data.DeliveryTerms);
            $('#SupplyTerms').val(data.SupplyTerms);
            $('#ConsigneeName').val(data.ConsigneeName);
            $('#ConsigneeLocation').val(data.ConsigneeLocation);
            $('#ModeOfShipment').val(data.ModeOfShipment);
            $('#PaymentTerms').val(data.PaymentTerms);
            $('#Inspection').val(data.Inspection);
            $('#EndUser').val(data.EndUser);

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}
