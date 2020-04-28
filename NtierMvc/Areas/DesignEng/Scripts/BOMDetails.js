
function SaveBOMDetails() {

    // Checking whether FormData is available in browser
    if (window.FormData !== undefined) {

        var fileUpload = $("#FileUploadBOM").get(0);
        var files = fileUpload.files;

        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        // Adding one more key to FormData object
        fileData.append('ProductName', $('#ProductNameBOM').val());
        fileData.append('ProductCode', $('#ProductCodeBOM').val());
        fileData.append('PL', $('#PLBOM').val());
        fileData.append('ProductNo', $('#ProductNoBOM').val());
        fileData.append('CasingSize', $('#CasingSizeBOM').val());
        fileData.append('CasingPPF', $('#CasingPPFBOM').val());
        fileData.append('Grade', $('#GradeBOM').val());
        fileData.append('OpenHoleSize', $('#OpenHoleSizeBOM').val());
        fileData.append('SN', $('#SNBOM').val());
        fileData.append('PartName', $('#PartNameBOM').val());
        fileData.append('CommodityNo', $('#CommodityNoBOM').val());
        fileData.append('COMMRevNo', $('#COMMRevNo').val());
        fileData.append('Qty', $('#QtyBOM').val());
        fileData.append('Length', $('#LengthBOM').val());
        fileData.append('OD', $('#ODBOM').val());
        fileData.append('WT', $('#WTBOM').val());
        fileData.append('UOM', $('#UOMBOM').val());
        fileData.append('RMTYPE', $('#RMTYPEBOM').val());

        $.ajax({
            url: window.SaveAndUploadBOMDetails,
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {
                alert(result);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    } else {
        alert("FormData is not supported.");
    }
}

function SearchForBOM() {
    var ProductName = $("#ProductNameBOM").val();
    var ProductCode = $("#ProductCodeBOM").val();
    var PL = $("#PLBOM").val();
    var ProductNo = $("#ProductNoBOM").val();
    var CasingSize = $("#CasingSizeBOM").val();
    var CasingPPF = $("#CasingPPFBOM").val();
    var Grade = $("#GradeBOM").val();
    var OpenHoleSize = $("#OpenHoleSizeBOM").val();

    $.ajax({
        type: 'POST',
        url: window.SearchBOM,
        data: JSON.stringify({ ProductName: ProductName, ProductCode: ProductCode, PL: PL, ProductNo: ProductNo, CasingSize: CasingSize, CasingPPF: CasingPPF, Grade: Grade, OpenHoleSize: OpenHoleSize }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {

            if (res.length > 0) {
                $.each(res, function (i, item) {
                    $('#tblBOMDetails > tbody:last-child').append('<tr><td>' + item.SN + '</td><td>' + item.PartName + '</td><td>' + item.CommodityNo + '</td><td>' + item.COMMRevNo + '</td><td>' + item.Qty + '</td><td>' + item.Length + '</td><td>' + item.OD + '</td><td>' + item.WT + '</td><td>' + item.UOM + '</td><td>' + item.RMTYPE + '</td></tr>');
                })
            }
            else {
                alert("No Record Found!");
            }

            //$("#SNBOM").val(data.SN);
            //$("#PartNameBOM").val(data.PartName);
            //$("#CommodityNoBOM").val(data.CommodityNo);
            //$("#CommodityReverseNoBOM").val(data.CommodityReverseNo);
            //$("#QtyBOM").val(data.Qty);
            //$("#ODBOM").val(data.OD);
            //$("#WTBOM").val(data.WT);
            //$("#BOMRMTYPE").val(data.BOMRMTYPE);
            //$("#UOMBOM").val(data.UOM);
            //$("#RMTYPEBOM").val(data.RMTYPE);
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}



function GetProductNameDetails() {
    var ProductName = $("#ProductNameBOM").val();
 
    $.ajax({
        type: 'POST',
        url: window.GetProductNoForProductName,
        data: JSON.stringify({ ProductNameId: ProductName }),
        contentType: 'application/json; charset=utf-8',
        success: function (res) {
            $('#ProductNoBOM').val(res);

        },
        error: function (x, e) {
            alert('Some Error comes up. Please Try Later.');
        }
    })
}