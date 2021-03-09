
function GetContractReview() {

    $.ajax({
        type: 'POST',
        url: window.GetContractReviews,
        data: JSON.stringify({ }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#ContractReviewList").empty();
            if (data.length > 0) {
                $('#noCRRecord').text('');
                $.each(data, function (i, item) {
                    if (item.RequiredColumn1 != "")
                        $("#ContractReviewList").append("<li><input type='checkbox' value='" + item.RequiredColumn3 + "'> <a target='_blank' href='/Documents/ContractReviewUploads/" + item.RequiredColumn3 + "'><img height='25px' src='/Images/excel.png' /><label>" + item.RequiredColumn2 + "</label></a></li>");
                })
            }
            else {
                $('#noCRRecord').text('No Records Found For Contract Review');
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })

}

function DeleteContReviews() {

    var categories = new Array();

    $('input[type=checkbox]').each(function () {
        this.checked ? categories.push($(this).val()) : null;
    });

    $.ajax({
        type: 'POST',
        url: window.DeleteContractReviews,
        data: JSON.stringify({ EnqNoExcels: categories }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(data);
            GetContractReview();
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })

}

function CRUpload() {

    if (window.FormData !== undefined) {

        var fileUpload = $("#CRFileUpload").get(0);
        var files = fileUpload.files;

        //Validate File Type
        var numb = $('#CRFileUpload')[0].files[0].size / 1024 / 1024; //count file size
        var resultid = $('#CRFileUpload').val().split(".");
        var gettypeup = resultid[resultid.length - 1]; // take file type uploaded file
        var filetype = $('#CRFileUpload').attr('data-file_types'); // take allowed files from input
        var allowedfiles = filetype.replace(/\|/g, ', '); // string allowed file
        var tolovercase = gettypeup.toLowerCase();
        var filesize = 25; //25MB
        var onlist = $('#CRFileUpload').attr('data-file_types').indexOf(tolovercase) > -1;
        var checkinputfile = $('#CRFileUpload').attr('type');
        numb = numb.toFixed(2);

        if (onlist && numb <= filesize) {
            $('#alert').html('The file is ready to upload').removeAttr('class').addClass('xd2'); //file OK

            // Create FormData object
            var fileData = new FormData();

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object
            //fileData.append('quoteType', QuoteType);
            //fileData.append('quoteNo', QuoteNo);


            $.ajax({
                url: window.CRFilesUpload,
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData,
                success: function (result) {
                    alert(result);
                    GetContractReview();
                },
                error: function (err) {
                    alert(err.statusText);
                    GetContractReview();
                }
            });
        }
        else {
            if (numb >= filesize && onlist) {
                $('#CRFileUpload').val(''); //remove uploaded file
                $('#alert').html('Added file is too big \(' + numb + ' MB\) - max file size ' + filesize + ' MB').removeAttr('class').addClass('xd'); //alert that file is too big, but type file is ok
            } else if (numb < filesize && !onlist) {
                $('#CRFileUpload').val(''); //remove uploaded file
                $('#alert').html('An not allowed file format has been added \(' + gettypeup + ') - allowed formats: ' + allowedfiles).removeAttr('class').addClass('xd'); //wrong type file
            } else if (!onlist) {
                $('#CRFileUpload').val(''); //remove uploaded file
                $('#alert').html('An not allowed file format has been added \(' + gettypeup + ') - allowed formats: ' + allowedfiles).removeAttr('class').addClass('xd'); //wrong type file
            }
        }

    } else {
        alert("FormData is not supported.");
    }
}


function DeleteContractReview() {

    var categories = new Array();

    $('input[type=checkbox]').each(function () {
        this.checked ? categories.push($(this).val()) : null;
    });

    $.ajax({
        type: 'POST',
        url: window.DeleteClarificationMails,
        data: JSON.stringify({ Id: categories }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(data);
            GetClarificationMails();
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })

}

function getContractReviewDetails() {

    let Customer = $("#CRCustomer").val();
    let ENQNo = $("#CRENQNo").val();
    let ItemNo = $("#CRItemNo").val();
    let FileName = $("#CRENQNo").val(); //$("#CRENQNo option:selected").text();

    //let itemNo = '';
    //let y = document.getElementById("CRItemNo");
    //for (var i = 0; i < y.options.length; i++) {
    //    if (y.options[i].selected == true) {
    //        //alert(x.options[i].value);
    //        itemNo = itemNo + y.options[i].value + ',';
    //    }
    //}

    //itemNo = itemNo.substring(0, itemNo.length - 1);

    ShowLoadder();

    $.ajax({
        type: 'POST',
        url: window.GetExcelForContractReview,
        data: JSON.stringify({ customerId: Customer, enqNo: ENQNo, fileName: FileName }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == 'No Records Found For Selected Item') {
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

            //if (data != "") {
            //    window.location.href = window.DownloadDoc + '?fileName=' + data.fileName + '&path=' + data.path;
            //    HideLoadder();
            //}
            //else {
            //    alert(data.errorMessage);
            //    HideLoadder();
            //}
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            HideLoadder();
        }
    })
}