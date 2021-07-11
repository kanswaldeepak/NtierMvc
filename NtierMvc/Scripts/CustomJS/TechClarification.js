

function Upload() {

    // Checking whether FormData is available in browser
    if (window.FormData !== undefined) {

        var fileUpload = $("#QuoteFileUpload").get(0);
        var files = fileUpload.files;

        //Validate File Type
        var numb = $('#QuoteFileUpload')[0].files[0].size / 1024 / 1024; //count file size
        var resultid = $('#QuoteFileUpload').val().split(".");
        var gettypeup = resultid[resultid.length - 1]; // take file type uploaded file
        var filetype = $('#QuoteFileUpload').attr('data-file_types'); // take allowed files from input
        var allowedfiles = filetype.replace(/\|/g, ', '); // string allowed file
        var tolovercase = gettypeup.toLowerCase();
        var filesize = 25; //25MB
        var onlist = $('#QuoteFileUpload').attr('data-file_types').indexOf(tolovercase) > -1;
        var checkinputfile = $('#QuoteFileUpload').attr('type');
        numb = numb.toFixed(2);

        if (onlist && numb <= filesize) {
            $('#alert').html('The file is ready to upload').removeAttr('class').addClass('xd2'); //file OK

            let QuoteType = $('#QuoteClarificationFormType').val();
            let QuoteNo = $('#QuoteClarificationFormNo').val();

            if ((QuoteType == undefined || QuoteType == '') || (QuoteNo == undefined || QuoteNo == '')) {
                alert('Kindly Select Quote Type and Quote No');
                return;
            }
            else {
                // Create FormData object
                var fileData = new FormData();

                // Looping over all files and add it to FormData object
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                // Adding one more key to FormData object
                fileData.append('quoteType', QuoteType);
                fileData.append('quoteNo', QuoteNo);


                $.ajax({
                    url: window.ClarificationFilesUpload,
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
            }
        }
        else {
            if (numb >= filesize && onlist) {
                $('#QuoteFileUpload').val(''); //remove uploaded file
                $('#alert').html('Added file is too big \(' + numb + ' MB\) - max file size ' + filesize + ' MB').removeAttr('class').addClass('xd'); //alert that file is too big, but type file is ok
            } else if (numb < filesize && !onlist) {
                $('#QuoteFileUpload').val(''); //remove uploaded file
                $('#alert').html('An not allowed file format has been added \(' + gettypeup + ') - allowed formats: ' + allowedfiles).removeAttr('class').addClass('xd'); //wrong type file
            } else if (!onlist) {
                $('#QuoteFileUpload').val(''); //remove uploaded file
                $('#alert').html('An not allowed file format has been added \(' + gettypeup + ') - allowed formats: ' + allowedfiles).removeAttr('class').addClass('xd'); //wrong type file
            }
        }

    } else {
        alert("FormData is not supported.");
    }
}

function GetQuoteNosForClarification() {
    var QuoteType = $("#QuoteClarificationFormType").val();
    $.ajax({
        type: 'POST',
        url: window.GetPrepQuoteNo,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#QuoteClarificationFormNo").empty();
            $("#VendorName").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#QuoteClarificationFormNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                    $("#VendorName").append($('<option></option>').val(item.DataStringValueField).html(item.DataAltValueField));
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

function GetSoNoDetialsForClarification() {
    var QuoteType = $("#QuoteClarificationFormType").val();
    var QuoteNo = $("#QuoteClarificationFormNo").val();
    $.ajax({
        type: 'POST',
        url: window.GetSoNoForClarification,
        data: JSON.stringify({ quotetypeId: QuoteType, quoteNoId: QuoteNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#SoNoClarification").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#SoNoClarification").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
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

function GetClarificationMail() {

    var QuoteNo = $("#QuoteClarificationFormNo").val();
    var QuoteType = $("#QuoteClarificationFormType").val();

    if ((QuoteNo == undefined || QuoteNo == '') && (QuoteType == undefined || QuoteType == '')) {
        alert('Please Select Quote No and Quote Type.');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetClarificationMails,
        data: JSON.stringify({ quoteNo: QuoteNo, quoteType: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.length > 0) {
                $("#MailList").empty();
                $.each(data, function (i, item) {
                    if (item.RequiredColumn1 != "")
                        $("#MailList").append("<li><input type='checkbox' value='" + item.RequiredColumn1 + "'> <a target='_blank' href='/Documents/MailUploads/" + item.RequiredColumn2 + "'><img height='25px' src='~/Images/pdfIcon.png' /><label>" + item.RequiredColumn2 + "</label></a></li>");
                })
            }
            else {
                alert("No Records Found");
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

function DeleteClarificationMail() {

    var categories = new Array();

    // iterate all checkboxes and obtain their checked values, unchecked values are not pushed into array
    $('input[type=checkbox]').each(function () {
        this.checked ? categories.push($(this).val()) : null;
    });

    // assume urldata is your web method to delete multiple records
    $.ajax({
        type: 'POST',
        url: window.DeleteClarificationMails,
        data: JSON.stringify({ Id: categories }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //$('#spn-Sucess-Failure').text(data);
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
            alert(data);
            GetClarificationMails();
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })

}

function GetClarificationNote() {
    var QuoteNo = $("#QuoteClarificationFormNo").val();
    var QuoteType = $("#QuoteClarificationFormType").val();

    if ((QuoteNo == undefined || QuoteNo == '') && (QuoteType == undefined || QuoteType == '')) {
        alert('Please Select Quote No and Quote Type.');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetClarificationNotes,
        data: JSON.stringify({ quoteNo: QuoteNo, quoteType: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.length > 0) {
                $("#txtClarificationNotes").val(data);
            }
            else {
                alert("Notes Not Found for Quote");
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

function SaveNotes() {
    var QuoteNo = $("#QuoteClarificationFormNo").val();
    var QuoteType = $("#QuoteClarificationFormType").val();
    var Notes = $("#txtClarificationNotes").val();

    if ((QuoteNo == undefined || QuoteNo == '') && (QuoteType == undefined || QuoteType == '')) {
        alert('Please Select Quote No and Quote Type.');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.SaveQuoteNotes,
        data: JSON.stringify({ quoteNo: QuoteNo, quoteType: QuoteType, notes: Notes }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert(data);
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })
}


function CallOrderFileUpload() {

    // Checking whether FormData is available in browser
    if (window.FormData !== undefined) {

        var fileUpload = $("#OrderFileUpload").get(0);
        var files = fileUpload.files;

        //Validate File Type
        var numb = $('#OrderFileUpload')[0].files[0].size / 1024 / 1024; //count file size
        var resultid = $('#OrderFileUpload').val().split(".");
        var gettypeup = resultid[resultid.length - 1]; // take file type uploaded file
        var filetype = $('#OrderFileUpload').attr('data-file_types'); // take allowed files from input
        var allowedfiles = filetype.replace(/\|/g, ', '); // string allowed file
        var tolovercase = gettypeup.toLowerCase();
        var filesize = 25; //25MB
        var onlist = $('#OrderFileUpload').attr('data-file_types').indexOf(tolovercase) > -1;
        var checkinputfile = $('#OrderFileUpload').attr('type');
        numb = numb.toFixed(2);

        if (onlist && numb <= filesize) {
            $('#alertOrder').html('The file is ready to upload').removeAttr('class').addClass('xd2'); //file OK

            let soNo = $('#SoNoClarification').val();

            if (soNo == undefined || soNo == '') {
                alert('Kindly Select SoNo');
                return;
            }
            else {
                // Create FormData object
                var fileData = new FormData();

                // Looping over all files and add it to FormData object
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                // Adding one more key to FormData object
                fileData.append('SoNo', $('#SoNoClarification').val());

                $.ajax({
                    url: window.ClarificationOrderFileUpload,
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
            }
        }
        else {
            if (numb >= filesize && onlist) {
                $('#OrderFileUpload').val(''); //remove uploaded file
                $('#alertOrder').html('Added file is too big \(' + numb + ' MB\) - max file size ' + filesize + ' MB').removeAttr('class').addClass('xd'); //alert that file is too big, but type file is ok
            } else if (numb < filesize && !onlist) {
                $('#OrderFileUpload').val(''); //remove uploaded file
                $('#alertOrder').html('An not allowed file format has been added \(' + gettypeup + ') - allowed formats: ' + allowedfiles).removeAttr('class').addClass('xd'); //wrong type file
            } else if (!onlist) {
                $('#OrderFileUpload').val(''); //remove uploaded file
                $('#alertOrder').html('An not allowed file format has been added \(' + gettypeup + ') - allowed formats: ' + allowedfiles).removeAttr('class').addClass('xd'); //wrong type file
            }
        }
    } else {
        alert("FormData is not supported.");
    }
}


function GetOrderClarification() {
    let SoNo = $("#SoNoClarification").val();

    if ((SoNo == undefined || SoNo == '')) {
        alert('Please Select So No.');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetOrderClarifications,
        data: JSON.stringify({ soNo: SoNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.length > 0) {
                $("#OrderList").empty();
                $.each(data, function (i, item) {
                    if (item.RequiredColumn1 != "")
                        $("#OrderList").append("<li><input type='checkbox' value='" + item.RequiredColumn1 + "'> <a target='_blank' href='/Documents/OrderUpload/" + item.RequiredColumn2 + "'><img height='25px' src='~/Images/pdfIcon.png' /><label>" + item.RequiredColumn2 + "</label></a></li>");
                })
            }
            else {
                alert("Details Not Found for Selected SoNo");
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

function DeleteOrderClarification() {

    var categories = new Array();

    // iterate all checkboxes and obtain their checked values, unchecked values are not pushed into array
    $('#OrderList input[type=checkbox]').each(function () {
        this.checked ? categories.push($(this).val()) : null;
    });

    // assume urldata is your web method to delete multiple records
    $.ajax({
        type: 'POST',
        url: window.DeleteOrderClarifications,
        data: JSON.stringify({ Id: categories }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(data);
            //$('#spn-Sucess-Failure').text(data);
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
            GetOrderClarifications();
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })

}

function GetOrderNotes() {
    var SoNo = $("#SoNoClarification").val();

    if ((SoNo == undefined || SoNo == '')) {
        alert('Please Select So No.');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.GetOrderNote,
        data: JSON.stringify({ soNo: SoNo }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.length > 0) {
                $("#txtOrderNotes").val(data);
            }
            else {
                alert("Notes Not Found for Order");
            }
        },
        error: function (x, e) {
            alert('Some error is occurred. Kindly contact Support.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })
}

function SaveOrderNotes() {
    var SoNo = $("#SoNoClarification").val();
    var Notes = $("#txtOrderNotes").val();

    if ((SoNo == undefined || SoNo == '')) {
        alert('Please Select So No.');
        return;
    }

    $.ajax({
        type: 'POST',
        url: window.SaveOrderNote,
        data: JSON.stringify({ soNo: SoNo, notes: Notes }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert(data);
        },
        error: function (x, e) {
            alert('Some error is occurred. Kindly contact Support.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })
}
