

function ShowHideStatus() {
    var InspectionValue = $('#Inspection option:selected').text();

    if (InspectionValue == 'In-House')
        $('#divStatus').hide();
    else
        $('#divStatus').show();
}

function ShowHideReasonForRegret() {
    var EoqValue = $('#Eoq option:selected').text();

    if (EoqValue == 'Quote')
        $('#divReasonForRegret').hide();
    else
        $('#divReasonForRegret').show();
}

function GetQuoteNoForQuoteType() {
    let QuoteType = $('#QuoteType').val();

    $.ajax({
        type: 'POST',
        url: window.GetPrepQuoteNo,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#QuoteNo").empty();
            if (res.length > 0) {
                $.each(res, function (i, item) {
                    $("#QuoteNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            $('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            $('#spn-Sucess-Failure').addClass("important red");
            $('#Sucess-Failure').modal('show');
        }
    })
}


function GetVendorAndEnquiryDetails() {
    var VendorId = $("#QRCustomerId").val();
    $.ajax({
        type: 'POST',
        url: window.VendorDetail,
        data: JSON.stringify({ vendorId: VendorId }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#QRCustomerName").val(res.quoteEntity.CustomerName);
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
            GetEnquiryDetails();
        },
        error: function (x, e) {
            $('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            $('#spn-Sucess-Failure').addClass("important red");
            $('#Sucess-Failure').modal('show');
        }
    })
}

function GetEnquiryDetails() {
    var EnqNo = $("#EnqNo").val();
    $.ajax({
        type: 'POST',
        url: window.GetEnquiryDetailsForQuote,
        data: JSON.stringify({ enquiryId: EnqNo }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.DataValueField1.length > 0) {
                $('#QuoteEnqDt').val(res.DataValueField1.split(' ')[0]);
                $('#QuoteEnqFor').val(res.DataValueField2);
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please contact support.' + e);
        }
    })
}

function GetQuoteNumbersForRevised() {
    var QuoteType = $("#QuoteTypeRevised").val();

    $.ajax({
        type: 'POST',
        url: window.GetPrepQuoteNo,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#RevisedFormQuoteNo").empty();
            $("#CustomerNameRevised").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#RevisedFormQuoteNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                    //$("#VendorName").append($('<option></option>').val(item.DataStringValueField).html(item.DataAltValueField));
                    //$("#QuotePrepVendorName").val(item.DataStringValueField);
                })
            }
        },
        error: function (x, e) {
            $('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            $('#spn-Sucess-Failure').addClass("important red");
            $('#Sucess-Failure').modal('show');
        }
    })
}

function GetQuoteNumbers() {
    //var QuoteType = $("#QuoteFormType option:selected").text();
    var QuoteType = $("#QuoteFormType").val();
    var QuoteTypeText = $("#QuoteFormType option:selected").text();

    $.ajax({
        type: 'POST',
        url: window.GetDeliveryItems,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#DeliveryTerms").empty();
            $('#QuoteFormNo').val('');
            $('#QuoteTypeNo').val('');


            if (res.lstQuoteTypeEntity.length > 0) {
                $.each(res.lstQuoteTypeEntity, function (i, item) {
                    $("#DeliveryTerms").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
            $('#QuoteFormNo').val(res.QuoteNo);

            var currentDate = new Date();
            if (currentDate.getMonth() >= 3)
                $('#QuoteTypeNo').val('QTMOT' + (currentDate.getFullYear()).toString().substr(currentDate.getFullYear().toString().length - 2) + (currentDate.getFullYear() + 1).toString().substr((currentDate.getFullYear() + 1).toString().length - 2) + '0' + res.QuoteNo);
            else
                $('#QuoteTypeNo').val('QTMOT' + (currentDate.getFullYear() - 1).toString().substr((currentDate.getFullYear() + 1).toString().length - 2) + currentDate.getFullYear().toString().substr(currentDate.getFullYear().toString().length - 2) + '0' + res.QuoteNo);


            //$('#QuoteTypeNo').val('QTMOT-' + res.QuoteNo);

            //if (QuoteTypeText == 'Domestic')
            //    $('#QuoteTypeNo').val('DQ-' + res.QuoteNo);
            //else if (QuoteTypeText == 'Export')
            //    $('#QuoteTypeNo').val('EQ-' + res.QuoteNo);
            //else if (QuoteTypeText == 'Service')
            //    $('#QuoteTypeNo').val('SE-' + res.QuoteNo);

            $("#QRCustomerId").empty();
            if (res.lstVendors.length > 0) {
                $.each(res.lstVendors, function (i, item) {
                    $("#QRCustomerId").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }

        },
        error: function (x, e) {
            $('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            $('#spn-Sucess-Failure').addClass("important red");
            $('#Sucess-Failure').modal('show');
        }
    })
}

function ChangeTransport() {

    var QuoteType = $('#QuoteFormType option:selected').text();

    if (QuoteType == 'Domestic') {
        $("#ModeOfDespatch").find("option:contains(Road)").show();
        $("#ModeOfDespatch").find("option:contains(Hand)").show();
        $("#ModeOfDespatch").find("option:contains(Sea)").hide();

        $('#divPortOfDischarge').hide();
    }
    else {
        $("#ModeOfDespatch").find("option:contains(Road)").hide();
        $("#ModeOfDespatch").find("option:contains(Hand)").hide();
        $("#ModeOfDespatch").find("option:contains(Sea)").show();

        $('#divPortOfDischarge').show();
    }
}


function LoadDescDetails() {

    $("#tblDescDetails").DataTable().destroy();
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
            }
        },
        "ajax": {
            "url": "/Technical/LoadDescDetail",
            "type": "POST",
            "datatype": "json",
            "data": {}
        },
        'order': [[1, "asc"]],
        columns: [
            { title: "SN", "data": "SNo", "name": "SNo", "autoWidth": true, "visible": true },
            { title: "Product Line", "data": "MainPL", "name": "MainPL", "autoWidth": true, "visible": true },
            { title: "Sub Product Line", "data": "SubPL", "name": "SubPL", "autoWidth": true, "visible": true },
            { title: "Product Name", "data": "ProductName", "name": "ProductName", "autoWidth": true },
            { title: "Product No", "data": "ProductNo", "name": "ProductNo", "autoWidth": true },
            { title: "Description", "data": "DESQuery", "name": "DESQuery", "autoWidth": true },
            {
                title: "Action", "data": "", orderable: false, width: "auto",
                "render": function (data, type, full, meta) {
                    var columnVal = "";
                    columnVal = '<div><button type = "button" onclick=DeleteUsingId("' + full.Id + '") class="btn btn-primary btn-smdf"> Delete </button></div>';
                    return columnVal;
                }
            }
        ],
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
        },
        "drawCallback": function (settings) {
        },
        "footerCallback": function (row, data, start, end, display) {

        }
    }

    $("#tblDescDetails").DataTable(req);
    $("#tblDescDetails tbody").show();

}


function DeleteUsingId(Id) {

    DeleteUsingIdFromTable("Master.Product", "Id", Id);
    LoadDescDetails();

}



