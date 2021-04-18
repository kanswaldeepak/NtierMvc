
function GetSoNosForFinancialYear() {
    let finYear = $('#ItemFinancialYear').val();
    let QuoteType = $('#OrderFormQuoteType').val();

    $.ajax({
        type: 'POST',
        url: window.GetSoNosForFinancialYears,
        data: JSON.stringify({ FinYear: finYear, quoteType: QuoteType}),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#ItemSoNo").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#ItemSoNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

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

            var SoNoItem = $("#ItemSoNo option:selected").text();
            var quoteTypeId = $("#OrderFormQuoteType").val();

            $.ajax({
                type: 'POST',
                url: window.GetOrderDetailsFromSO,
                data: JSON.stringify({ SoNoView: SoNoItem, quoteTypeId: quoteTypeId }),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('.OrderPercentClass').html('Order Percentage ' + data.iEntity.POPercent + ' %');
                    LoadtblItemWiseOrder();
                },
                error: function (x, e) {
                    alert('Some error is occurred, Please try after some time. Please check SoNo Selected is Correct or Not ');
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
    var SoNoItem = $("#ItemSoNo option:selected").text();
    var quoteTypeId = $("#OrderFormQuoteType").val();
    $('#ItemSoNoView').val(SoNoItem);

    if (quoteTypeId == undefined || quoteTypeId == '') {
        alert('Kindly Select Order Type.');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetOrderDetailsFromSO,
        data: JSON.stringify({ SoNoView: SoNoItem, quoteTypeId: quoteTypeId }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#ItemCustomerName').val(data.iEntity.CustomerName);
            $('#ItemCustomerId').val(data.iEntity.CustomerId);
            $('#PONo').val(data.iEntity.PoNo);
            $('#PODate').val(data.iEntity.PoDate);
            $('#PODeliveryDate').val(data.iEntity.PoDeliveryDate);
            $('#SupplyTerms').val(data.iEntity.SupplyTerms);
            $('#QuoteItemFormNo').val(data.iEntity.QuoteNo);
            $('#ExWorkValue').val(data.iEntity.ExWorkValue);
            $('#ItemQPFinancialYear').val(data.iEntity.QPFinancialYear);
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

            $('#QuoteItemSlNo').empty();
            if (data.lstQuoteItemSlNo.length > 0) {
                $.each(data.lstQuoteItemSlNo, function (i, item) {
                    $("#QuoteItemSlNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
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
    var ItemNo = $("#QuoteItemSlNo").val();
    var itemString = "";
    $.ajax({
        type: 'POST',
        url: window.GetItemQuoteDetails,
        data: JSON.stringify({ itemNo: ItemNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#POSlNo').val($('#QuoteItemSlNo option:selected').text());
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
    var QuoteNo = $("#QuoteItemFormNo option:selected").val();
    var FinYear = $("#ItemQPFinancialYear").val();

    $('#ItemQuoteNoView').val($("#QuoteItemFormNo option:selected").text());

    if ((QuoteType == undefined || QuoteType == '') || (QuoteNo == undefined || QuoteNo == '') || (FinYear == undefined || FinYear == '')) {
        alert('Please Select Order Type, Quote No and Financial Year');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetQuoteItemSlNos,
        data: JSON.stringify({ quoteType: QuoteType, quoteNo: QuoteNo, finYear: FinYear }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#QuoteItemSlNo').empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#QuoteItemSlNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
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
        url: window.GetOrderQuoteNo,
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
    var SoNo = $("#SoNoOrder option:selected").text();
    if (SoNo == undefined || SoNo == '') {
        alert('Please Select So No');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetSoNoDetails,
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

var myData = [];
function LoadtblItemWiseOrder() {

    $("#tblItemWiseOrder").DataTable().destroy();
    var req = {
        "processing": true,
        "language": {
            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
        },
        "serverSide": true,
        "paging": true,
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
        "pageLength": 5,
        "searching": true,
        "filter": true,
        "language": {
            "paginate": {
                "next": '&#8594;',
                "previous": '&#8592;'
            },
            "search": "Common Search:"
        },
        "ajax": {
            "url": window.LoadItemWiseOrders,
            "type": "POST",
            "datatype": "json",
            "data": {},
            "dataSrc": function (json) {
                myData = json.data;
                return json.data;
            }
        },
        'order': [[0, "asc"]],
        columns: [
            { title: "SNo", "data": "SNo", "name": "SNo", "autoWidth": true, "visible": true },
            { title: "Id", "data": "Id", "name": "Id", "autoWidth": true, "visible": false },
            { title: "Financial Year", "data": "FinancialYearText", "name": "FinancialYearText", "autoWidth": true, "visible": true },
            { title: "WA No", "data": "SoNoView", "name": "SoNoView", "autoWidth": true, "visible": true },
            { title: "Customer Id", "data": "CustomerId", "name": "CustomerId", "autoWidth": true, "visible": true },
            { title: "Customer Name", "data": "CustomerName", "name": "CustomerName", "autoWidth": true, "visible": true },
            { title: "PO No", "data": "PoNo", "name": "PONo", "autoWidth": true, "visible": true },
            { title: "PO Date", "data": "PoDate", "name": "PODate", "autoWidth": true, "visible": true },
            { title: "PO Delivery Date", "data": "PoDeliveryDate", "name": "PODeliveryDate", "autoWidth": true, "visible": false },
            { title: "Supply", "data": "SupplyTerms", "name": "SupplyTerms", "autoWidth": true, "visible": false },
            { title: "Supply Terms", "data": "SupplyTermsText", "name": "SupplyTermsText", "autoWidth": true, "visible": false },
            { title: "QuoteNos", "data": "QuoteNo", "name": "QuoteNo", "autoWidth": true, "visible": false },
            { title: "QuoteItems", "data": "QuoteItemSlNo", "name": "QuoteItemSlNo", "autoWidth": true, "visible": false },
            { title: "Quote No", "data": "QuoteNoView", "name": "QuoteNoView", "autoWidth": true, "visible": false },
            { title: "Quote Item Sl No", "data": "QuoteItemSlNoText", "name": "QuoteItemSlNoText", "autoWidth": true, "visible": false },
            { title: "PO Sl No", "data": "PoSLNo", "name": "PoSLNo", "autoWidth": true, "visible": true },
            { title: "PO Qty", "data": "PoQty", "name": "PoQty", "autoWidth": true, "visible": true },
            { title: "Unit Price", "data": "UnitPrice", "name": "UnitPrice", "autoWidth": true, "visible": true },
            { title: "Product Details", "data": "ViewProductDetails", "name": "ViewProductDetails", "autoWidth": true, "visible": true },
            {
                title: "Action", "data": "", orderable: false, width: "auto",
                "render": function (data, type, full, meta) {
                    var columnVal = "";
                    columnVal = '<div><button type = "button" onclick=DeleteUsingItemId("' + full.Id + '") class="btn btn-danger btn-sm"> Delete </button></div>';
                    return columnVal;
                }
            },
            //{
            //    title: "Action", "data": "", orderable: false, width: "auto",
            //    "render": function (data, type, full, meta) {
            //        var columnVal = "";
            //        columnVal = '<div><button type = "button" onclick=EditUsingItemId("' + full.Id + '") class="btn btn-info btn-sm"> Edit </button></div>';
            //        return columnVal;
            //    }
            //}
            //{
            //    data: 'id',
            //    title: "Action", "data": "", orderable: false, width: "auto",
            //    render: value => `<div><button type="button" class="btn btn-info btn-sm editItemBtn" data-id="${value}">Edit</button> <button type="button" class="btn btn-danger btn-sm deleteItemBtn" data-id="${value}">Delete</button></div>`
            //}
        ],
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
        },
        "drawCallback": function (settings) {
        },
        "footerCallback": function (row, data, start, end, display) {

        }
    }

    $("#tblItemWiseOrder").DataTable(req);
    $("#tblItemWiseOrder tbody").show();
}



function DeleteUsingItemId(Id) {

    DeleteUsingIdFromTable("Items", "Id", Id);
    LoadtblItemWiseOrder();

}

function EditUsingItemId(Id) {
        
   current_row= myData.filter((row) => {
        if (row.abc = Id) return row;
    })
}

//function  EditOrderItems(thisObj) {

//    thisObj.parent().parent().children().each(function () {
//        alert("childs");
//    });

//}

//var table = $("#tblItemWiseOrder").DataTable(req);

//table.on('click', '.editItemBtn', function () {
//    debugger;
//    const id = this.getAttribute('data-id');
//    const data = myData.find(d => d.id == Id);
//    const data1 = myData.find(d => d.id == SoNoView);
//    console.log(data+' '+data1);

//})

//table.on('click', '.deleteItemBtn', function () {
//    const id = this.getAttribute('data-id');
//    const data = myData.find(d => d.id == id);
//})
