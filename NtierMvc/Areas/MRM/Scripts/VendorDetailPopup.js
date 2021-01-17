function  SaveVendor(e) {
    e.preventDefault();
    var Documents = $("#Documents").get(0).files;
    var frm = $("#formSaveVendorDetail");
    var formData = new FormData(frm[0]);
    formData.append("file", Documents)

    for (var i = 0; i < Documents.length; i++) {
        formData.append("fileInput", Documents[i]);
    }

    var Status = false;
     Status = GetFormValidationStatus("#formSaveVendorDetail");


    if (!Status) {
        alert("Kindly Fill all mandatory fields");
    }
    else {
        return $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: window.SaveVendorS,
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                if (result == 'Saved Successfully!' || result == 'Updated Successfully') {
                    if (window.LoadVendorList) {
                        window.LoadVendorList();
                    }
                    $("#ModalPopup").modal('hide');
                }
            },
            error: function () {
                alert(result)
            }
        });


    }


}

