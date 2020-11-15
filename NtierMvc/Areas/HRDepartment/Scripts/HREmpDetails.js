

function GetCertificateDetails() {

    var EmpId = $('#EmpId').val();

    $.ajax({
        type: 'POST',
        url: window.GetEmpCertificates,
        data: JSON.stringify({ EmpId: EmpId }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.length > 0) {
                $("#CertificateList").empty();
                $.each(data, function (i, item) {
                    if (item.RequiredColumn1 != "")
                        $("#CertificateList").append("<li><a target='_blank' href='/Images/Certificates/Employee/EmployeeCertificates/" + item.RequiredColumn2 + "'><label>" + item.RequiredColumn2 + "</label></a> <button type='button' class='btn btn -outline-danger btn-sm' onclick='DeleteCertificate(" + item.RequiredColumn1 + ")' data-toggle='tooltip' data-placement='bottom' title='DELETE' ><i class='fa fa-trash'></i></button></li>");
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

function DeleteCertificate(CertId) {

    $.ajax({
        type: 'POST',
        url: window.DeleteCertificates,
        data: JSON.stringify({ CertId: CertId }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            GetCertificateDetails();
            alert(data);
        },
        error: function (data) {
            alert(data);
        }
    })
}


function addNewExp(e) {
    e.preventDefault();
    var $tableBody = $("#tableExp");
    var $trLast = $tableBody.find("tr:last");

    var $trNew = $trLast.clone();
    var size = $('#tableExp >tbody >tr').length + 1;

    $trNew.find('.ExpSN').html(size);
    var suffix = $trNew.find(':input:first').attr('class').match(/\d+/);

    $trNew.find("#HREmployer").attr('class', 'requiredValidation form-control RMdescription');
    $trNew.find("#HRDesignation").attr('class', 'form-control');
    $trNew.find("#HRPeriodFrom").attr('class', 'form-control');
    $trNew.find("#HRPeriodTo").attr('class', 'form-control');

    $trNew.find("td:last").html('<a href="#" class="remove">Remove</a>');
    $.each($trNew.find(':input'), function (i, val) {
        // Replaced Name
        var oldN = $(this).attr('class');
        var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
        $(this).attr('class', newN);
        //Replaced value

        $(this).val('');

        //var type = $(this).attr('type');
        //debugger;
        //if (type.toLowerCase() == "text") {
        //    $(this).attr('value', '');
        //}
        //if (type.toLowerCase() == "select") {
        //    $(this).attr('value', '');
        //}

        // If you have another Type then replace with default value
        $(this).removeClass("input-validation-error");
        //$(this).addClass("requiredValidation");

    });
    $trLast.after($trNew);


    // Re-assign Validation
    //var form = $("form")
    //    .removeData("validator")
    //    .removeData("unobtrusiveValidation");
    //$.validator.unobtrusive.parse(form);


    // 2. Remove
    $('.remove').on("click", function (e) {
        e.preventDefault();
        $(this).parent().parent().remove();
    });

    $('.NoEndDate').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        changeMonth: true,
        changeYear: true,
        endDate: '',
    });

    $('.CalenderTillTodayDate').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        changeMonth: true,
        changeYear: true,
        endDate: 'today',
    });

};


function SaveExpDetails(EmpId) {
    //e.preventDefault();

    var arr = [];
    arr.length = 0;

    var frm = $("#formSaveEmployeeDetail");
    var formData = new FormData(frm[0]);

    var Status = false;
    Status = GetFormValidationStatus("#formSaveEmployeeDetail");

    let tableSelected = '#tableExp';

    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        return;
    }
    else {

        $.each($(tableSelected + " tbody tr"), function () {
            arr.push({
                //Id: $("#Id").val(),
                
                EmpId: EmpId,
                SN: $(this).find('td:eq(0) label').text(),
                Employer: $(this).find('td:eq(1) input').val(),
                Designation: $(this).find('td:eq(2) input').val(),
                PeriodFrom: $(this).find('td:eq(3) input').val(),
                PeriodTo: $(this).find('td:eq(4) select').val()

            });
        });

        var data = JSON.stringify({
            ExpDetails: arr
        });

        $.when(saveExperienceDetail(data)).then(function (response) {
            console.log(response);
        }).fail(function (err) {
            console.log(err);
        });
    }
};

function saveExperienceDetail(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: window.SaveExperienceDetails,
        data: data,
        success: function (result) {
            alert(result);
            HideLoadder();
        },
        error: function () {
            alert(result);
            HideLoadder();
        }
    });
}

function SaveCertificates() {
    var fileTech = $("#fileTech").get(0).files;
    var empId = $("#EmpId").val();
    //var fileEduc = $("#fileEduc").get(0).files;
    //var filePostGrad = $("#filePostGrad").get(0).files;

    ShowLoadder();
    var fileData = new FormData();
    fileData.append("fileTech", fileTech);
    fileData.append("EmpId", empId);
    //fileData.append("fileEduc", fileEduc);
    //fileData.append("filePostGrad", filePostGrad);
    //fileData.append("fileProfQual", fileProfQual);


    for (var i = 0; i < fileTech.length; i++) {
        fileData.append("fileInput", fileTech[i]);
    }

    $.ajax({
        type: "POST",
        url: window.SaveEmpCertificates,
        dataType: "json",
        contentType: false, // Not to set any content header
        processData: false, // Not to process data
        data: fileData,
        success: function (result, status, xhr) {
            HideLoadder();
            $("#ModalPopup").modal('hide');
            alert(result);
        },
        error: function (xhr, status, error) {
            HideLoadder();
            alert(status);
        }
    });
};

$(document).ajaxStart(function () {
    $("#loadingImg").show();
    $("#fileButton").prop('disabled', true);
});

$(document).ajaxStop(function () {
    $("#loadingImg").hide();
    $("#fileButton").prop('disabled', false);
    $("#fileInput").val("");
});