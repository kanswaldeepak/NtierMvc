function makeDescQuery(item) {
    console.log($(item).find('option:selected').text());

    if (item.name == 'FieldName1')
        $('#finalDescQuery').val($('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text() + 'Field' + ' ProductName , \r\n ');
    else if (item.name == 'Pos1')
        $('#finalDescQuery').val($(item).find('option:selected').text());
    else if (item.name.indexOf('FieldName') != -1)
        $('#finalDescQuery').val($('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text() + 'Field, \r\n');
    else
        $('#finalDescQuery').val($('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text());

    //let queryString = $('#finalDescQuery').val() + ' ' + $(item).find('option:selected').text();

}

function GetSubProductGrps(itemType) {
    var MainProdGrp = '';

    if (itemType == 'QuotePrep') {
        MainProdGrp = $("#MainProdGrp").val();
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
                $("#SubProdGrp").empty();
                if (res.length > 0) {
                    $.each(res, function (i, item) {
                        $("#SubProdGrp").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
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
        MainProdGrp = $("#MainProdGrp").val();
        SubProdGrp = $("#SubProdGrp").val();
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
                $("#ProductName").empty();
                if (res.length > 0) {
                    $.each(res, function (i, item) {
                        $("#ProductName").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
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
    var ProductName = $("#ProductName").val();
    $.ajax({
        type: 'POST',
        url: window.GetProductNumber,
        data: JSON.stringify({ productNameId: ProductName }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#ProductNo").val(res);
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
    var ProductNameID = $("#ProductName").val();
    var CasingSize = $("#CasingSize").val();
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
    var ProductNameID = $("#ProductName").val();
    var CasingSize = $("#CasingSize").val();
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

                        if (item.FieldName1 == '')
                            finalString = finalString.replace(item.FieldName1 + 'Field', '');
                        else
                            finalString = finalString.replace(item.FieldName1+'Field', $('#' + item.FieldName1 + ' option:selected').text());

                        finalString = finalString.replace('ProductName', $('#ProductName option:selected').text());

                        if (item.FieldName2 == '')
                            finalString = finalString.replace(item.FieldName2 + 'Field', '');
                        else
                            finalString = finalString.replace(item.FieldName2 + 'Field', $('#' + item.FieldName2 + ' option:selected').text());

                        if (item.FieldName3 == '')
                            finalString = finalString.replace(item.FieldName3 + 'Field', '');
                        else
                            finalString = finalString.replace(item.FieldName3 + 'Field', $('#' + item.FieldName3 + ' option:selected').text());

                        if (item.FieldName4 == '')
                            finalString = finalString.replace(item.FieldName4 + 'Field', '');
                        else
                            finalString = finalString.replace(item.FieldName4 + 'Field', $('#' + item.FieldName4 + ' option:selected').text());

                        if (item.FieldName5 == '')
                            finalString = finalString.replace(item.FieldName5 + 'Field', '');
                        else
                            finalString = finalString.replace(item.FieldName5 + 'Field', $('#' + item.FieldName5 + ' option:selected').text());

                        if (item.FieldName6 == '')
                            finalString = finalString.replace(item.FieldName6 + 'Field', '');
                        else
                            finalString = finalString.replace(item.FieldName6 + 'Field', $('#' + item.FieldName6 + ' option:selected').text());

                        //switch (item.DES) {
                        //    case 'DES1.1':
                        //        finalString = item.Pos1 + ' ' + $("#CasingSize option:selected").text() + ' ' + $("#ProductName option:selected").text() + ', \r\n ' + item.Pos2 + ' ' + $("#OpenHoleSize option:selected").text()
                        //            + ', \r\n' + item.Pos3 + ' ' + $("#QuotePrepODSize").val()
                        //            + ', \r\n ' + item.Pos4 + $("#QuotePrepTotalBows").val();
                        //        break;
                        //    case 'DES1.2':
                        //        finalString = item.Pos1 + ' ' + $("#CasingSize option:selected").text() + ' ' + $("#ProductName option:selected").text() + ', \r\n ' + item.Pos2 + ' ' + $("#OpenHoleSize option:selected").text();
                        //        break;
                        //    case 'DES1.3':
                        //        finalString = item.Pos1 + ' ' + $("#CasingSize option:selected").text() + ' ' + $("#ProductName option:selected").text();
                        //        break;
                        //    case 'DES2.1':
                        //        finalString = item.Pos1 + ' ' + $("#CasingSize option:selected").text() + ' ' + $("#ProductName option:selected").text() + ', \r\n ' + item.Pos2 + ' ' + $("#CasingPpf option:selected").text()
                        //            + ' lbs/ft, \r\n' + item.Pos3 + ' ' + $("#Connection option:selected").text()
                        //            + ', \r\n ' + item.Pos4 + $("#MaterialGrade option:selected").text();
                        //        break;
                        //    case 'DES2.2':
                        //        finalString = item.Pos1 + ' ' + $("#CasingSize option:selected").text() + ' ' + $("#ProductName option:selected").text() + ', \r\n ' + item.Pos2 + ' ' + $("#CasingPpf option:selected").text()
                        //            + ' lbs/ft, \r\n' + item.Pos3 + ' ' + $("#WallThickness option:selected").text()
                        //            + ', \r\n ' + item.Pos4 + $("#MaterialGrade option:selected").text();
                        //        break;
                        //    //case 'DES2.3':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ', ' + item.Pos3 + ', '
                        //    //        + item.Pos4 + item.Pos5;
                        //    //    break;
                        //    //case 'DES2.4':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ', ' + item.Pos3 + ', '
                        //    //        + item.Pos4 + item.Pos5;// + ' ' + item.subProductDetails;
                        //    //    break;
                        //    //case 'DES2.5':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + $("#CasingPpf option:selected").text() + ' ' + item.Pos4
                        //    //        + ', ' + $("#Connection option:selected").text() + item.Pos5 + ', ' + $("#BallSize option:selected").text();
                        //    //    break;
                        //    //case 'DES2.6':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ' ' + $("#MaterialGrade option:selected").text() + ', ' + $("#CasingPpf option:selected").text() + item.Pos4
                        //    //        + ', ' + $("#Connection option:selected").text()
                        //    //        + item.Pos5; // + ' ' + item.subProductDetails;
                        //    //    break;
                        //    //case 'DES2.7':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    //        + ' ' + $("#MaterialGrade option:selected").text() + $("#CasingPpf option:selected").text()
                        //    //        + ', ' + item.Pos4 + ', ' + $("#Connection option:selected").text()
                        //    //        + item.Pos5;
                        //    //    break;
                        //    //case 'DES3.1':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ',x ' + item.Pos3 + ' '
                        //    //        + ', ' + item.Pos4 + $("#CasingSize option:selected").text()
                        //    //        + item.Pos5 + ', ' + $("#OpenHoleSize option:selected").text() + ', ' + item.Pos6;
                        //    //    break;
                        //    //case 'DES3.2':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    //        + item.Pos4 + $("#CasingSize option:selected").text()
                        //    //        + item.Pos5 + ' ' + $("#CasingPpf option:selected").text() + ' ' + item.Pos6;
                        //    //    break;
                        //    //case 'DES3.3':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    //        + item.Pos4 + $("#CasingSize option:selected").text()
                        //    //        + item.Pos5 + ' ' + item.Pos6;
                        //    //    break;
                        //    //case 'DES3.4':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    //        + ', ' + item.Pos4 + ', '
                        //    //        + item.Pos5 + $("#CasingSize option:selected").text() + ', ' + item.Pos6;
                        //    //    break;
                        //    //case 'DES3.5':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos4 + ', '
                        //    //        + item.Pos5 + ', ' + item.Pos6;
                        //    //    break;
                        //    //case 'DES3.6':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ', '
                        //    //        + $("#CasingSize option:selected").text()
                        //    //        + ', ' + $("#MaterialGrade option:selected").text() + ', ' + $("#CasingPpf option:selected").text() + ', '
                        //    //        + item.Pos4 + ', ' + $("#CasingSize option:selected").text() + ', ' + $("#Connection option:selected").text()
                        //    //        + item.Pos5 + ', ' + item.Pos6;
                        //    //    break;
                        //    //case 'DES3.7':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + $("#OpenHoleSize option:selected").text() + ', ' + item.Pos3
                        //    //        + ', ' + item.Pos4 + $("#CasingSize option:selected").text() + item.Pos5 + ', ' + $("#OpenHoleSize option:selected").text() + ' ' + item.Pos6;
                        //    //    break;
                        //    //case 'DES4.1':
                        //    //    finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    //        + $("#CasingSize option:selected").text() + ', ' + item.Pos4 + $("#CasingPpf option:selected").text()
                        //    //        + item.Pos5
                        //    //        + ' ' + $("#MaterialGrade option:selected").text() + ' ' + $("#CasingWT option:selected").text() + ' ' + item.Pos6
                        //    //        + ', ' + $("#Connection option:selected").text()
                        //    //        + item.Pos7;
                        //    //    break;
                        //    default:
                        //        finalString = item.Pos1 + ' ' + $("#CasingSize option:selected").text() + ',' + item.Pos2 + ' \r\n '
                        //            + ', ' + item.Pos3 + ' '
                        //            + $("#CasingPpf option:selected").text() + 'lbs/ft,  \r\n ' + item.Pos4
                        //            + $("#Connection option:selected").text() + ' \r\n ' + item.Pos5
                        //            + $("#MaterialGrade option:selected").text();
                        //        break;
                        //}
                        $('#ViewPos1').val(item.Pos1);
                        $('#ViewPos2').val(item.Pos2);
                        $('#ViewPos3').val(item.Pos3);
                        $('#ViewPos4').val(item.Pos4);
                        $('#ViewPos5').val(item.Pos5);
                        //$('#ViewProductNo').text($('#ProductNo').val());
                        //$('.ViewCasingSize').text($("#CasingSize option:selected").text());
                        //$('#ViewCasingPpf').text($("#CasingPpf option:selected").text());
                        //$('#ViewMaterialGrade').text($("#MaterialGrade option:selected").text());
                        //$('#ViewConnection').text($("#Connection option:selected").text());

                        //finalString = item.DESQuery;
                        //finalString = finalString + ' ' + item.subProductDetails;
                        //finalString = item.Pos1 + ' ' + $('#ProductNo').val() + ', \r\n ' + item.Pos2 + ' '
                        //    + $("#CasingSize option:selected").text() + ', ' + item.Pos3 + ' '
                        //    + $("#CasingSize option:selected").text() + ', ' + item.Pos4 + $("#CasingPpf option:selected").text()
                        //    + ' ' + $("#MaterialGrade option:selected").text() + ', ' + $("#Connection option:selected").text()
                        //    + item.Pos5 + ' ' + item.subProductDetails;
                        $('#ViewProductDetails').val(finalString);
                        $('#ViewEnqSrNo').text($("#EnqSrNo").val());
                        $('#ViewItemNo').text($('#ItemNo').val());
                        $('#ViewQuantity').text($('#Qty').val());

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

                        //$('#ViewProductId').val(item.Id);
                        //$('#ViewProductName').val(item.ProductName);
                        //$('#ViewProductCode').val(item.ProductCode);
                        //$('#ViewPL').val(item.PL);
                        //$('#ViewProductNo').val(item.ProductNo);
                        //$('#ViewPos1').val(item.Pos1);
                        //$('#ViewPos2').val(item.Pos2);
                        //$('#ViewPos3').val(item.Pos3);
                        //$('#ViewPos4').val(item.Pos4);
                        //$('#ViewPos5').val(item.Pos5);
                        //$('#ViewPos6').val(item.Pos6);
                        //$('#ViewPos7').val(item.Pos7);
                        //$('#ViewPos8').val(item.Pos8);
                        //$('#ViewPos9').val(item.Pos9);
                        //$('#ViewPos10').val(item.Pos10);
                        //$('#ViewDES').val(item.DES);

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
    var ItemNo = $("#ItemNo").val();

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
                $("#EnqSrNo").val(data.EnqSrNo);
                $("#ProductName").val(data.ProductName);
                $("#ProductNo").val(data.ProductNo);
                $("#CasingSize").val(data.CasingSize);
                $("#CasingPpf").val(data.CasingPpf);
                $("#MaterialGrade").val(data.MaterialGrade);
                $("#Connection").val(data.Connection);
                $("#Qty").val(data.Qty);
                $("#Uom").val(data.Uom);
                $("#UnitPrice").val(data.UnitPrice);
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
            $("#Currency").val(res.DataValueField2);
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

