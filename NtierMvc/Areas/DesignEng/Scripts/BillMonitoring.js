
function getVendorIDs() {
    var VendorNatureId = $('#BMVendorNatureId').val();

    $.ajax({
        type: 'POST',
        url: window.GetVendorIds,
        data: JSON.stringify({ VendorNatureId: VendorNatureId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#BMVendorId").empty();
            $.each(data, function (i, item) {
                $("#BMVendorId").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            })
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })

}


function getVendorDetails() {
    var VendorId = $('#BMVendorId').val();

    $.ajax({
        type: 'POST',
        url: window.GetVendorDetails,
        data: JSON.stringify({ VendorId: VendorId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#BMVendorName').val(data.DataValueField1);
            $('#BMCity').val(data.DataValueField2);
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function changeEndUseNo() {
    var EndUse = $('#BMEndUse option:selected').text();

    $.ajax({
        type: 'POST',
        url: window.ChangeEndUseNo,
        data: JSON.stringify({ EndUse: EndUse }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#BMEndUseNo').empty();
            $.each(data, function (i, item) {
                $("#BMEndUseNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            })

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}


function getDdlDetailsForList(selectObject) {

    var SelectedId = selectObject;
    var SelectedVal = $('#' + selectObject).val();
    $.ajax({
        type: 'POST',
        url: window.GetDdlDetailsForList,
        data: JSON.stringify({ SelectedVal: SelectedVal, SelectedId: SelectedId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            if (SelectedId == 'TypeList') {
                $('#BMVendorNatureList').empty();
                $.each(data, function (i, item) {
                    $("#BMVendorNatureList").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
            else if (SelectedId == 'VendorNatureList') {
                $('#BMVendorNameList').empty();
                $.each(data, function (i, item) {
                    $("#BMVendorNameList").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
            else if (SelectedId == 'VendorNameList') {
                $('#BMBillNoList').empty();
                $.each(data, function (i, item) {
                    $("#BMBillNoList").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }


        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}