
function GetPoSLNoDetailList() {
    var POSlNoPRP = $("#POSlNoPRP").val();
    $.ajax({
        type: 'POST',
        url: window.GetPoSLNoDetails,
        data: JSON.stringify({ POSlNo: POSlNoPRP }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.length <= 0) {
                alert('No Records Found');
            }

            $('#POQtyPRP').val(res[0].POQty);
            $('#POItemDescPRP').val(res[0].POItemDesc);
            $('#OpnCodePRP').val(res[0].OpnCode);
            $('#ProductNoPRP').val(res[0].ProductNo);
            $('#ProductNoViewPRP').val(res[0].ProductNoView);
            $('#ProductNamePRP').val(res[0].ProductName);
            $('#PLPRP').val(res[0].PL);
            $('#CasingSizePRP').val(res[0].CasingSize);
            $('#CasingPPFPRP').val(res[0].CasingPPF);
            $('#GradePRP').val(res[0].Grade);
            $('#OpenHoleSizePRP').val(res[0].OpenHoleSize);
            //$("a#UploadFileId").attr("href", "/BOMUpload/" + item.UploadFile)

            $("#tblPRPDetails > tbody").empty();
            if (res.length > 0) {
                $.each(res, function (i, item) {
                    if (item.UploadFile != '') {
                        $('#tblPRPDetails > tbody:last-child').append('<tr><td>' + item.SN + '</td><td>' + item.PartName + '</td><td>' + item.CommNo + '</td><td>' + item.COMMRevNo + '</td><td>' + item.Qty + '</td><td>' + item.Length + '</td><td>' + item.OD + '</td><td>' + item.WT + '</td><td>' + item.UOM + '</td><td>' + item.RMTYPE + '</td><td><a target="_blank" href="/Documents/BOMUpload/' + item.UploadFile + '" id="UploadFileId"><img height="25px" src="../../Images/pdfIcon.png" /><label> </label></a></td></tr>');
                    }
                    else {
                        $('#tblPRPDetails > tbody:last-child').append('<tr><td>' + item.SN + '</td><td>' + item.PartName + '</td><td>' + item.CommNo + '</td><td>' + item.COMMRevNo + '</td><td>' + item.Qty + '</td><td>' + item.Length + '</td><td>' + item.OD + '</td><td>' + item.WT + '</td><td>' + item.UOM + '</td><td>' + item.RMTYPE + '</td><td> NA </td></tr>');
                    }
                })
            }
            else {
                alert("No BOM Details Found For Selected Product Name and Number!");
            }
        }
    });
}

function GetOrderDetailsProductNo() {
    var ProductName = $("#ProductNamePRP").val();
    var ProductNo = $("#ProductNoPRP").val();

    if (ProductName == '' || ProductName == undefined) {
        alert('Please Select Product Name');
        return;
    }
    

    $.ajax({
        type: 'POST',
        url: window.GetProductDetails,
        data: JSON.stringify({ ProductName: ProductName, ProductNo: ProductNo }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
                $('#ProductCodePRP').val(res.DataValueField1);
                $('#PLPRP').val(res.DataValueField2);
                $('#CasingSizePRP').val(res.DataValueField3);
                $("a#UploadFileId").attr("href", "/BOMUpload/" + res.DataValueField7)
                $('#CasingPPFPRP').val(res.DataValueField4);
                $('#GradePRP').val(res.DataValueField5);
                $('#OpenHoleSizePRP').val(res.DataValueField6);

                $.ajax({
                    type: 'POST',
                    url: window.SearchBOM,
                    data: JSON.stringify({ ProductName: ProductName, ProductNo: ProductNo }),
                    contentType: "application/json; charset=utf-8",
                    success: function (res) {
                        $("#tblPRPDetails > tbody").empty();
                        if (res.length > 0) {
                            $.each(res, function (i, item) {
                                $('#tblPRPDetails > tbody:last-child').append('<tr><td>' + item.SN + '</td><td>' + item.PartName + '</td><td>' + item.CommodityNo + '</td><td>' + item.COMMRevNo + '</td><td>' + item.Qty + '</td><td>' + item.Length + '</td><td>' + item.OD + '</td><td>' + item.WT + '</td><td>' + item.UOM + '</td><td>' + item.RMTYPE + '</td></tr>');
                            })
                        }
                        else {
                            alert("No BOM Details Found For Selected Product Name and Number!");
                        }
                    },
                    error: function (x, e) {
                        alert('Some error is occurred, Please try after some time.');
                    }
                })
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
    
}


function GetSONoDetails() {
    var SONo = $("#SONoPRP option:selected").val();

    $.ajax({
        type: 'POST',
        url: window.GetOrderDetailsFromSO,
        data: JSON.stringify({ SoNo: SONo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#PONoPRP").val(data.iEntity.PoNo);
            $("#PODatePRP").val(data.iEntity.PoDate);
            $("#ProductGroupPRP").val(data.iEntity.ProductGroup);
            //$("#POSlNoPRP").val(data.iEntity.PoSLNo);
            //$("#POQtyPRP").val(data.iEntity.PoQty);
            //$("#POItemDescPRP").val(data.iEntity.PoItemDescription);
        },
        error: function (x, e) {
            alert('Some error occurred, Please try after some time.');
        }
    })
}


function GetPlSoNoDetails() {
    var SONo = $("#SONoPRP option:selected").val();

    $.ajax({
        type: 'POST',
        url: window.GetPlSoNoDetails,
        data: JSON.stringify({ SoNo: SONo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#POSlNoPRP").empty();
            $.each(data, function (i, item) {
                $("#POSlNoPRP").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            });
        },
        error: function (x, e) {
            alert('Some error occurred, Please try after some time.');
        }
    })
}

function GetQuoteNos() {
    var QuoteType = $("#QuoteTypePRP").val();
    $.ajax({
        type: 'POST',
        url: window.GetPrepQuoteNo,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#QuoteNoPRP").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#QuoteNoPRP").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

var count = 0;
function GetQuoteDetails() {
    var QuoteType = $("#QuoteTypePRP").val();
    var QuoteNo = $("#QuoteNoPRP").val();

    $.ajax({
        type: 'POST',
        url: window.GetQuoteOrderDetailsForPRP,
        data: JSON.stringify({ quoteType: QuoteType, quoteNoId: QuoteNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#VendorIdPRP').val(data.VendorId);
            $('#VendorNamePRP').val(data.VendorName);
            $('#SONoPRP').val(data.SoNo);
            $("#PONoPRP").val(data.PoNo);
            $("#PODatePRP").val(data.PoDate);
            $("#ProductGroupPRP").val(data.ProductGroup);

            //if (data.soNo.length > 0) {
            //    $('#SONoPRP').empty();
            //    $.each(data.soNo, function (i, item) {
            //        $("#SONoPRP").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            //    })
            //}
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

$('.NoEndDate').datepicker({
    format: 'dd/mm/yyyy',
    autoclose: true,
    changeMonth: true,
    changeYear: true,
    endDate: '',
});
$('.CurrentEndDate').datepicker({

    format: 'dd/mm/yyyy',
    autoclose: true,
    changeMonth: true,
    changeYear: true,
    endDate: 'today',
});