
function getContractReviewDetails() {
    
    var Customer = $("#CRCustomer").val();
    var ENQNo = $("#CRENQNo").val();
    var ItemNo = $("#CRItemNo").val();

    let itemNo = '';
    var y = document.getElementById("CRItemNo");
    for (var i = 0; i < y.options.length; i++) {
        if (y.options[i].selected == true) {
            //alert(x.options[i].value);
            itemNo = itemNo + y.options[i].value + ',';
        }
    }

    itemNo = itemNo.substring(0, itemNo.length - 1);

    ShowLoadder();

    $.ajax({
        type: 'POST',
        url: window.GetExcelForContractReview,
        data: JSON.stringify({ customerId: Customer, enqNo: ENQNo, itemNo: itemNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != "") {
                window.location.href = window.DownloadDoc + '?fileName=' + data.fileName+'&path='+data.path;
                HideLoadder();
            }
            else {
                alert(data.errorMessage);
                HideLoadder();
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            HideLoadder();
        }
    })
}