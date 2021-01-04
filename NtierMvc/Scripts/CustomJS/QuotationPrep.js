function makeDescQuery(item) {
    //console.log($(item).find('option:selected').text());

    if (item.name == 'FieldName1')
        $('#finalDescQuery').val($('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text() + 'Field' + ' ProductName \n');
    else if (item.name == 'Pos1')
        $('#finalDescQuery').val($(item).find('option:selected').text() +' : ');
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
    var QuoteNumber = $("#QuotePrepFormNo").val();
    var QuoteTypeText = $("#QuotePrepFormType option:selected").text();

    if (DownloadType == '' || QuoteType == '' || QuoteNumber == '') {
        alert("Kindly Select DownloadType, QuoteType and QuoteNumber");
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.CreateDownloadDocument,
        data: JSON.stringify({ downloadTypeId: DownloadType, quoteTypeId: QuoteType, quoteNumberId: QuoteNumber, quoteTypeText: QuoteTypeText }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //alert("Dowloaded Successfully" + data);
            if (data.fileName != "") {
                //use window.location.href for redirect to download action for download the file
                //window.location.href = '@Url.RouteUrl(new { Controller = "Technical", Action = "Download" })/?fileName=' + data.fileName;
            }
            else {
                alert(data.errorMessage);
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function ValidateItemNo() {
    var QuoteType = $("#QuotePrepFormType").val();
    var QuoteNo = $("#QuotePrepFormNo").val();
    var ItemNo = $("#QuotePrepItemNo").val();

    if (ItemNo == '' || ItemNo == undefined) {
        alert('Please Enter Item No');
        return;
    }
    $.ajax({
        type: 'POST',
        url: window.CheckDuplicateItemNo,
        data: JSON.stringify({ itemNoId: ItemNo, quoteType: QuoteType, quoteNo: QuoteNo }),
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
    var QuoteType = $("#QuotePrepFormType").val();
    var QuoteNo = $("#QuotePrepFormNo").val();
    $.ajax({
        type: 'POST',
        url: window.VendorDetailsForQuote,
        data: JSON.stringify({ quoteNo: QuoteNo, quoteType: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#QuotePrepVendorName").val(res.DataValueField1);
            $("#QuotePrepCurrency").val(res.DataValueField2);
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

