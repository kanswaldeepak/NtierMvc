function makeDescQuery(item) {
    //console.log($(item).find('option:selected').text());

    if (item.name == 'FieldName1')
        $('#finalDescQuery').val($('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text() + 'Field' + ' ProductName \n');
    else if (item.name == 'Pos1')
        $('#finalDescQuery').val($(item).find('option:selected').text() + ' : ');
    else if (item.name.indexOf('FieldName') != -1)
        $('#finalDescQuery').val($('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text() + 'Field \n');
    else
        $('#finalDescQuery').val($('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text() + ' : ');

    //let queryString = $('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text();

}

function GetSubProductGrps(itemType) {
    var MainProdGrp = '';

    if (itemType == 'QuotePrep') {
        MainProdGrp = $("#QuotePrepMainProdGrp").val();
    }
    else {
        MainProdGrp = $("#DescMainPL").val();
    }

    $.ajax({
        type: 'POST',
        url: window.GetSubProdGrp,
        data: JSON.stringify({ MainProdGrpId: MainProdGrp }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {

            if (itemType == 'QuotePrep') {
                $("#QuotePrepSubProdGrp").empty();
                if (res.length > 0) {
                    $.each(res, function (i, item) {
                        $("#QuotePrepSubProdGrp").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                    })
                }
            }
            else {
                $("#DescSubPL").empty();
                if (res.length > 0) {
                    $.each(res, function (i, item) {
                        $("#DescSubPL").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                    })
                }
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

function GetProdName(itemType) {
    var MainProdGrp = '';
    var SubProdGrp = '';

    if (itemType == 'QuotePrep') {
        MainProdGrp = $("#QuotePrepMainProdGrp").val();
        SubProdGrp = $("#QuotePrepSubProdGrp").val();
    }
    else {
        MainProdGrp = $("#DescMainPL").val();
        SubProdGrp = $("#DescSubPL").val();
    }

    $.ajax({
        type: 'POST',
        url: window.GetProdNames,
        data: JSON.stringify({ MainProdGrpId: MainProdGrp, SubProdGrpId: SubProdGrp }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {

            if (itemType == 'QuotePrep') {
                $("#QuotePrepProductName").empty();
                if (res.length > 0) {
                    $.each(res, function (i, item) {
                        $("#QuotePrepProductName").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                    })
                }
            }
            else {
                $("#DescProductName").empty();
                if (res.length > 0) {
                    $.each(res, function (i, item) {
                        $("#DescProductName").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                    })
                }
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


function GetProductNumberByName() {
    var ProductName = $("#QuotePrepProductName").val();
    $.ajax({
        type: 'POST',
        url: window.GetProductNumber,
        data: JSON.stringify({ productNameId: ProductName }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#QuotePrepProductNo").val(res);
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })
}

function QPGetQuoteNos() {
    var QuoteType = $("#QuotePrepFormType").val();

    $.ajax({
        type: 'POST',
        url: window.GetPrepQuoteNo,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#QuotePrepFormNo").empty();
            $("#QuotePrepVendorName").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#QuotePrepFormNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some Error Occurred. Please try again later.');
        }
    })
}

function QPGetQuoteNosFromType() {
    var QuoteType = $("#SearchQuoteTypeQuotePrep").val();
    var finYear = $("#SearchQuoteTypeFinancialYear").val();

    $.ajax({
        type: 'POST',
        url: window.GetPrepQuoteNo,
        data: JSON.stringify({ quotetypeId: QuoteType, financialYr: finYear }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#SearchQuoteNoQuotePrep").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#SearchQuoteNoQuotePrep").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            $alert('Some error is occurred, Please try after some time.');
        }
    })
}


function GetItemNosForQuoteNo() {
    var QuoteNo = $("#SearchQuoteNoQuotePrep option:selected").text();

    $.ajax({
        type: 'POST',
        url: window.GetItemNosForQuoteNos,
        data: JSON.stringify({ quoteNo: QuoteNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#SearchItemNoQuotePrep").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#SearchItemNoQuotePrep").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            $alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetProductTypes() {
    var QuoteType = $("#QuotePrepFormType").val();
    $.ajax({
        type: 'POST',
        url: window.GetProductTypes,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#QuotePrepVendorName").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#QuotePrepVendorName").val(item.DataStringValueField);
                    //$("#VendorName").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
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

function GetDisplayFieldsForQuotePrep() {
    var ProductNameID = $("#QuotePrepProductName").val();
    var CasingSize = $("#QuotePrepCasingSize").val();
    $('.ShowHideFields').css('display', 'none');

    $.ajax({
        type: 'POST',
        url: window.GetDisplayFieldsForQuoteP,
        data: JSON.stringify({ productId: ProductNameID, casingSize: CasingSize, type: 'NewQuotePrep' }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.length > 0) {
                for (var i = 1; i <= 10; i++) {
                    if (data[0]['FieldName' + i] != '') {
                        $('#div' + data[0]['FieldName' + i]).css('display', 'block');
                    }
                }

            }
        }
    })

}

function GetProductDetails() {
    var ProductNameID = $("#QuotePrepProductName").val();
    var CasingSize = $("#QuotePrepCasingSize").val();
    var Status = true;
    //Status = GetFormValidationStatus("#formSaveQuotationPrepDetail");
    if (!Status) {
        alert("Kindly Fill all mandatory fields first");
    }
    else {
        $.ajax({
            type: 'POST',
            url: window.GetPrepProductNames,
            data: JSON.stringify({ productId: ProductNameID, casingSize: CasingSize, type: 'NewQuotePrep' }),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.length > 0) {
                    $('#divProductDetails').show();
                    $.each(data, function (i, item) {
                        var finalString = item.DESQuery;

                        finalString = finalString.replace('ProductName', $('#QuotePrepProductName option:selected').text());

                        for (var i = 1; i <= 6; i++) {
                            if (item['FieldName' + i] == '')
                                finalString = finalString.replace(item['FieldName' + i] + 'Field', '');
                            else if (item['FieldName' + i].indexOf('Ppf') != -1)
                                finalString = finalString.replace(item['FieldName' + i] + 'Field', $('#QuotePrep' + item['FieldName' + i] + ' option:selected').text() + ' lbs/ft');
                            else {
                                if ($('#QuotePrep' + item['FieldName' + i]).is('select'))
                                    finalString = finalString.replace(item['FieldName' + i] + 'Field', $('#QuotePrep' + item['FieldName' + i] + ' option:selected').text());
                                else if ($('#QuotePrep' + item['FieldName' + i]).is('input:text'))
                                    finalString = finalString.replace(item['FieldName' + i] + 'Field', $('#QuotePrep' + item['FieldName' + i]).val());
                                else
                                    alert('Other than select or input found');
                            }
                        }

                        $('#ViewPos1').val(item.Pos1);
                        $('#ViewPos2').val(item.Pos2);
                        $('#ViewPos3').val(item.Pos3);
                        $('#ViewPos4').val(item.Pos4);
                        $('#ViewPos5').val(item.Pos5);

                        $('#ViewProductDetails').val(finalString);
                        $('#ViewEnqSrNo').text($("#QuotePrepEnqSrNo").val());
                        $('#ViewItemNo').text($('#QuotePrepItemNo').val());
                        $('#ViewQuantity').text($('#QuotePrepQty').val());

                        $('#ViewNetWeight').val(item.NetWeight);
                        $('#ViewGrossWeight').val(item.GrossWeight);

                        if ($("#EnqSrNo").val() == "") {
                            $("#EnqSrNoHeader").hide();
                            $("#EnqSrNoBody").hide();
                        }
                        else {
                            $("#EnqSrNoHeader").show();
                            $("#EnqSrNoBody").show();
                        }

                    })
                }
                else {
                    alert('No Relevant Data Found in Product');
                }
            },
            error: function (x, e) {
                alert('Some error is occurred, Please try after some time.');
            }
        })
    }

}

function CreateDocument(dwnldtype) {
    var DownloadType = dwnldtype;
    var QuoteType = $("#QuotePrepFormType").val();
    var QuoteNumber = $("#QuotePrepFormNo option:selected").text();
    var QuoteTypeText = $("#QuotePrepFormType option:selected").text();
    var QuoteNoForFileName = $("#QuotePrepFormNo option:selected").text();

    if (DownloadType == '' || QuoteType == '' || QuoteNumber == '') {
        alert("Kindly Select DownloadType, QuoteType and QuoteNumber");
        return;
    }

    ShowLoadder();

    $.ajax({
        type: 'POST',
        url: window.CreateDownloadDocument,
        data: JSON.stringify({ downloadTypeId: DownloadType, quoteTypeId: QuoteType, quoteNumberId: QuoteNumber, quoteTypeText: QuoteTypeText, quoteNoForFileName: QuoteNoForFileName }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //alert("Dowloaded Successfully" + data);
            if (data == 'No Records Found For Selected Quote') {
                HideLoadder();
                alert(data);
            }   
            else if (data != "") {
                //use window.location.href for redirect to download action for download the file
                window.location.href = window.DownloadDoc + '?fileName=' + data;
                HideLoadder();
            }
            else {
                HideLoadder();
                alert(data.errorMessage);
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            HideLoadder();
        }
    })
}

function ValidateItemNo() {
    var FinancialYear = $("#QuotePrepFinancialYear").val();
    var QuoteType = $("#QuotePrepFormType").val();
    var QuoteNo = $("#QuotePrepFormNo option:selected").text();
    var ItemNo = $("#QuotePrepItemNo").val();

    if (ItemNo == '' || ItemNo == undefined) {
        alert('Please Enter Item No');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.CheckDuplicateItemNo,
        data: JSON.stringify({ itemNoId: ItemNo, quoteType: QuoteType, quoteNo: QuoteNo, financialYear: FinancialYear }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.RecordCount > 0) {

                if (data.EnqSrNo != '')
                    $("#QuotePrepEnqSrNo").val(data.EnqSrNo);
                if (data.CasingSize != '')
                    $("#QuotePrepCasingSize").val(data.CasingSize);
                if (data.CasingPpf != '')
                    $("#QuotePrepCasingPpf").val(data.CasingPpf);
                if (data.MaterialGrade != '')
                    $("#QuotePrepMaterialGrade").val(data.MaterialGrade);
                if (data.Connection != '')
                    $("#QuotePrepConnection").val(data.Connection);
                if (data.Qty != '')
                    $("#QuotePrepQty").val(data.Qty);
                if (data.Uom != '')
                    $("#QuotePrepUom").val(data.Uom);
                if (data.UnitPrice != '')
                    $("#QuotePrepUnitPrice").val(data.UnitPrice);

                GetDisplayFieldsForQuotePrep();
                GetProductDetails();

                if (data.ViewProductDetails != '')
                    $("#ViewProductDetails").val(data.ViewProductDetails);

                if (data.OpenHoleSize != '')
                    $("#QuotePrepOpenHoleSize").val(data.OpenHoleSize);

                if (data.ViewBallSize != '')
                    $("#QuotePrepBallSize").val(data.BallSize);

                if (data.ViewWallThickness != '')
                    $("#QuotePrepWallThickness").val(data.WallThickness);

            }
            else
                alert("Item No not Found in Records");
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetProductNameForProductType(ProductType) {
    var ProductType = $("#ProductType").val();
    $.ajax({
        type: 'POST',
        url: window.GetProductNameForProductType,
        data: JSON.stringify({ productTypeId: ProductType }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res > 0) {
                $scope.ProductNameList = res;
            }
            else
                alert("Item No not Found in Records");
        },
        error: function (x, e) {
            $('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            $('#spn-Sucess-Failure').addClass("important red");
            $('#Sucess-Failure').modal('show');
        }
    })
}

function GetVendorDetailsForQuote() {
    var financialYr = $("#QuotePrepFinancialYear").val();
    var QuoteType = $("#QuotePrepFormType").val();
    var QuoteNo = $("#QuotePrepFormNo option:selected").text();
    $('#QPQuoteNoView').val($('#QuotePrepFormNo option:selected').text());

    $.ajax({
        type: 'POST',
        url: window.VendorDetailsForQuote,
        data: JSON.stringify({ quoteNoId: QuoteNo, quotetypeId: QuoteType, financialYr: financialYr }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            //$("#QuotePrepVendorName").val(res.DataValueField1);
            //$("#QuotePrepCurrency").val(res.DataValueField2);
            $("#QuotePrepCustomerName").val(res.CustomerName);
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function CheckSupplyTermsAndLeadTime() {
    //let st = $('#QuotePrepSupplyTerms option:selected').text();

    var QuoteType = $("#QuotePrepFormType").val();
    var QuoteNo = $("#QuotePrepFormNo option:selected").text();

    $.ajax({
        type: 'POST',
        url: window.LeadDetailsForQuote,
        data: JSON.stringify({ quoteNo: QuoteNo, quoteType: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $('#QuotePrepSupplyTerms').val(res[0].RequiredColumn3);
            let mySupp = $('#QuotePrepSupplyTerms option:selected').text();
            if (mySupp == 'Single') {
                $('#QuotePrepLeadTime').val(res[0].RequiredColumn1);
                $('#QuotePrepLeadTimeDuration').val(res[0].RequiredColumn2);
            }
            else {
                $('#QuotePrepLeadTime').val('');
                $('#QuotePrepLeadTimeDuration option:selected').val('-1');
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
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
            "url": window.LoadDescDetail,
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
                    columnVal = '<div><button type = "button" onclick=DeleteUsingId("' + full.Id + '") class="btn btn-primary btn-sm"> Delete </button></div>';
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

function LoadMasterPLAndSubPL() {

    $("#tblMainAndSubPL").DataTable().destroy();
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
            "url": window.LoadMasterPLlist,
            "type": "POST",
            "datatype": "json",
            "data": {}
        },
        'order': [[2, "desc"]],
        columns: [
            { title: "SN", "data": "SNo", "name": "SNo", "autoWidth": true, "visible": true },
            { title: "Id", "data": "Id", "name": "Id", "autoWidth": true, "visible": false },
            { title: "Main PL", "data": "MainPL", "name": "MainPL", "autoWidth": true, "visible": true },
            { title: "Main PL Name", "data": "MainPLName", "name": "MainPLName", "autoWidth": true, "visible": true },
            { title: "Sub PL", "data": "SubPL", "name": "SubPL", "autoWidth": true, "visible": true }

        ],
        "fnCreatedRow": function (nRow, aData, iDataIndex) {
        },
        "drawCallback": function (settings) {
        },
        "footerCallback": function (row, data, start, end, display) {

        }
    }

    $("#tblMainAndSubPL").DataTable(req);
    $("#tblMainAndSubPL tbody").show();

}

//function LoadMasterPLAndSubPL() {

//    $("#tblMainAndSubPL").DataTable().destroy();
//    var req = {
//        "processing": true,
//        "language": {
//            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
//        },
//        "serverSide": true,
//        "paging": true,
//        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
//        "pageLength": 5,
//        "searching": true,
//        "filter": true,
//        "language": {
//            "paginate": {
//                "next": '&#8594;',
//                "previous": '&#8592;'
//            }
//        },
//        "ajax": {
//            "url": "/Technical/LoadMasterPLlist",
//            "type": "POST",
//            "datatype": "json",
//            "data": {}
//        },
//        'order': [[1, "asc"]],
//        columns: [
//            { title: "SN", "data": "SNo", "name": "SNo", "autoWidth": true, "visible": true },
//            { title: "Id", "data": "Id", "name": "Id", "autoWidth": true, "visible": false },
//            { title: "Main PL", "data": "MainPL", "name": "MainPL", "autoWidth": true, "visible": true },
//            { title: "Main PL Name", "data": "MainPLName", "name": "MainPLName", "autoWidth": true, "visible": true },
//            { title: "Sub PL", "data": "SubPL", "name": "SubPL", "autoWidth": true, "visible": true }

//        ],
//        "fnCreatedRow": function (nRow, aData, iDataIndex) {
//        },
//        "drawCallback": function (settings) {
//        },
//        "footerCallback": function (row, data, start, end, display) {

//        }
//    }

//    $("#tblMainAndSubPL").DataTable(req);
//    $("#tblMainAndSubPL tbody").show();

//}


function LoadQuotePrepListDetail() {

    $("#tblQuotePrep").DataTable().destroy();
    let FinancialYear = $('#SearchQuoteTypeFinancialYear').val();
    let QuoteType = $('#SearchQuoteTypeQuotePrep').val();
    let QuoteNo = $('#SearchQuoteNoQuotePrep option:selected').text();
    let ItemNo = $('#SearchItemNoQuotePrep').val();

    var req = {
        "processing": true,
        "language": {
            processing: '<i class="fa fa-spinner fa-spin fa-3x fa-fw"></i><span class="sr-only">Loading...</span> '
        },
        "serverSide": true,
        "paging": true,
        "lengthMenu": [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
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
            "url": window.LoadQuotePrepListDetails,
            "type": "POST",
            "datatype": "json",
            "data": { quoteType: QuoteType, quoteNo: QuoteNo, itemNo: ItemNo, financialYear: FinancialYear }
        },
        'order': [[0, "desc"]],
        columns: [
            { title: "SN", "data": "SNo", "name": "SNo", "autoWidth": true, "visible": true },
            { title: "Customer Name", "data": "CustomerName", "name": "CustomerName", "autoWidth": true, "visible": true },
            { title: "Quote Type", "data": "QuoteType", "name": "QuoteType", "autoWidth": true, "visible": true },
            { title: "Quote No", "data": "QuoteNo", "name": "QuoteNo", "autoWidth": true, "visible": true },
            { title: "Main Prod Grp", "data": "MainProdGrp", "name": "MainProdGrp", "autoWidth": true, "visible": true },
            { title: "Sub Prod Grp", "data": "SubProdGrp", "name": "SubProdGrp", "autoWidth": true, "visible": true },
            { title: "Item No", "data": "ItemNo", "name": "ItemNo", "autoWidth": true, "visible": true },
            { title: "Product Name", "data": "ProductName", "name": "ProductName", "autoWidth": true, "visible": true },
            { title: "Product No", "data": "ProductNo", "name": "ProductNo", "autoWidth": true, "visible": true },
            { title: "Casing Size", "data": "CasingSize", "name": "CasingSize", "autoWidth": true, "visible": false },
            { title: "Casing Ppf", "data": "CasingPpf", "name": "CasingPpf", "autoWidth": true, "visible": false },
            { title: "Material Grade", "data": "MaterialGrade", "name": "MaterialGrade", "autoWidth": true, "visible": false },
            { title: "Connection", "data": "Connection", "name": "Connection", "autoWidth": true, "visible": false },
            { title: "Qty", "data": "Qty", "name": "Qty", "autoWidth": true, "visible": true },
            { title: "Uom", "data": "Uom", "name": "Uom", "autoWidth": true, "visible": true },
            { title: "Unit Price", "data": "UnitPrice", "name": "UnitPrice", "autoWidth": true, "visible": true },
            { title: "Open Hole Size", "data": "OpenHoleSize", "name": "OpenHoleSize", "autoWidth": true, "visible": false },
            { title: "Ball Size", "data": "BallSize", "name": "BallSize", "autoWidth": true, "visible": false },
            { title: "Wall Thickness", "data": "WallThickness", "name": "WallThickness", "autoWidth": true, "visible": false },
            { title: "View Product Details", "data": "ViewProductDetails", "name": "ViewProductDetails", "autoWidth": true, "visible": true },
            { title: "OD Size", "data": "ODSize", "name": "ODSize", "autoWidth": true, "visible": false },
            { title: "Total Bows", "data": "TotalBows", "name": "TotalBows", "autoWidth": true, "visible": false },
            { title: "PDC Drillable", "data": "PDCDrillable", "name": "PDCDrillable", "autoWidth": true, "visible": false },
            { title: "Id", "data": "Id", "name": "Id", "autoWidth": true, "visible": false },
            {
                title: "Action", "data": "", orderable: false, width: "1%",
                "render": function (data, type, full, meta) {
                    var columnVal = "";
                    columnVal = '<div><button type = "button" onclick=EditQuotePrep("' + full.Id + '") class="btn btn-info btn-sm"> Edit </button><button type = "button" onclick=DeleteQuotePrep("' + full.Id + '") class="btn btn-danger btn-sm"> Delete </button></div>';
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

    $("#tblQuotePrep").DataTable(req);
    $("#tblQuotePrep tbody").show();

}


function DeleteQuotePrep(Id) {    
    DeleteUsingIdFromTable("QuotePreparationTbl", "Id", Id);
    LoadQuotePrepListDetail();

}

function ClearQuotePrepSearch() {
    $('#SearchQuoteTypeQuotePrep').val('');
    $('#SearchQuoteNoQuotePrep').val('');
    $('#SearchItemNoQuotePrep').val('');

    LoadQuotePrepListDetail();
}

function EditQuotePrep(QuoteId) {

    let id = QuoteId;

    $.ajax({
        type: 'POST',
        url: window.EditQuotePreps,
        data: JSON.stringify({ Id: QuoteId }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.QuoteType != null) {
                $('#QuotePrepId').val(QuoteId);
                $('#QuotePrepFormType').val(data.QuoteType);
                //$('#QuotePrepFormNo option:selected').text(data.QuoteNoView);
                $("#QuotePrepFormNo option").each(function () {
                    if ($(this).text() == data.QuoteNoView) {
                        $(this).attr('selected', 'selected');
                    }
                });
                $('#QPQuoteNoView').val(data.QuoteNoView);
                $('#QuotePrepCustomerName').val(data.CustomerName);
                $('#QuotePrepSupplyTerms').val(data.SupplyTerms);
                $('#QuotePrepLeadTime').val(data.LeadTime);
                $('#QuotePrepLeadTimeDuration').val(data.LeadTimeDuration);
                $('#QuotePrepMainProdGrp').val(data.MainProdGrp);
                $('#QuotePrepSubProdGrp').val(data.SubProdGrp);
                $('#QuotePrepProductName').val(data.ProductName);
                $('#QuotePrepProductNo').val(data.ProductNo);
                $('#QuotePrepItemNo').val(data.ItemNo);
                $('#QuotePrepFinancialYear').val(data.FinancialYear);

                if (data.EnqSrNo != '')
                    $("#QuotePrepEnqSrNo").val(data.EnqSrNo);
                if (data.CasingSize != '')
                    $("#QuotePrepCasingSize").val(data.CasingSize);
                if (data.CasingPpf != '')
                    $("#QuotePrepCasingPpf").val(data.CasingPpf);
                if (data.MaterialGrade != '')
                    $("#QuotePrepMaterialGrade").val(data.MaterialGrade);
                if (data.Connection != '')
                    $("#QuotePrepConnection").val(data.Connection);
                if (data.Qty != '')
                    $("#QuotePrepQty").val(data.Qty);
                if (data.Uom != '')
                    $("#QuotePrepUom").val(data.Uom);
                if (data.UnitPrice != '')
                    $("#QuotePrepUnitPrice").val(data.UnitPrice);

                GetDisplayFieldsForQuotePrep();
                $('#divProductDetails').show();

                if (data.ViewProductDetails != '')
                    $("#ViewProductDetails").val(data.ViewProductDetails);

                if (data.OpenHoleSize != '')
                    $("#QuotePrepOpenHoleSize").val(data.OpenHoleSize);

                if (data.ViewBallSize != '')
                    $("#QuotePrepBallSize").val(data.BallSize);

                if (data.ViewWallThickness != '')
                    $("#QuotePrepWallThickness").val(data.WallThickness);

            }
            else
                alert("Item No not Found in Records");
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })

}

function GetQuoteNoFromFinYearAndQuote() {
    var FinYear = $("#QuotePrepFinancialYear").val();
    var QuoteType = $("#QuotePrepFormType").val();

    if (FinYear == '' || FinYear == undefined)
        return;

    $.ajax({
        type: 'POST',
        url: window.QPGetQuoteNoFromFinYears,
        data: JSON.stringify({ finYear: FinYear, quoteType: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#QuotePrepFormNo").empty();
            $("#QuotePrepVendorName").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#QuotePrepFormNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some Error Occurred. Please try again later.');
        }
    })
}