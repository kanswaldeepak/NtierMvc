var Staff = function () {
    
    var bindForm = function (id) {
        $('.overlap_container').show();
        $.ajax({
            type: 'GET',
            url: window.GetForm,
            async: false,
            //data: { buildingSpaceDetailsId: id },
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                SetModalWidth(800);
                SetModalTitle("Add Personal detail of profile")
                SetModalBody(response);
                HideLoadder();
                ShowModal();
            },
            error: function (data) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        });
    }
    var BindCasteDropDown = function (e) {
        var categoryId = $(e).val();
        $.ajax({
            url: window.GetCasteByCategoryId,
            type: "GET",
            data: { categoryId: categoryId },
            success: function (result) {
                var options = '';
                $(result).each(function (index, obj) {
                    options = options + '<option value="' + obj.DataValueField + '">' + obj.DataTextField + '</option>';
                });
                $("#ddlCaste").html(options);
            },
            error: function (e) {
                var options = '<option value="-1">Select</option>';
                $("#ddlCaste").html(options);
                alertNotification(e.statusText);
            }
        });

        //var options = '<option value="-1">Select</option>';
        //$("#ddlCategory").html(options);
    }
    var BindFacultyDropDown = function (e) {
        var staffTypeId = $(e).val();
        $.ajax({
            url: window.GetStaffByStaffTypeId,
            type: "GET",
            data: { staffTypeId: staffTypeId },
            success: function (result) {
                var options = '';
                $(result).each(function (index, obj) {
                    options = options + '<option value="' + obj.DataValueField + '">' + obj.DataTextField + '</option>';
                });
                $("#ddlStaffWiseFaculty").html(options);
            },
            error: function (e) {
                var options = '<option value="-1">Select</option>';
                $("#ddlStaffWiseFaculty").html(options);
                alertNotification(e.statusText);
            }
        });

        //var options = '<option value="-1">Select</option>';
        //$("#ddlCategory").html(options);
    }
    var bindDetailForm = function (selectedStaffTypeId, selectedFacultyId, selectedPageTypeId) {
        $('.overlap_container').show();
        if (selectedPageTypeId == "1") {
            $.ajax({
                type: 'GET',
                url: window.GetQualificationForm,
                async: false,
                data: { selectedStaffTypeId: selectedStaffTypeId, selectedFacultyId: selectedFacultyId, selectedPageTypeId: selectedPageTypeId },
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (response) {
                    //$("#btnSubmit").show();
                    $('.overlap_container').hide();
                    $("#div-Qualification-details").html(response);
                    $("#addQualificationDetail").modal('show');
                    //alert("Hello");

                    //calculateTotalArea();
                },
                error: function (data) {
                    //$("#addPersonalDetail").modal('hide');
                    $('.overlap_container').hide();
                }
            });
        }
        else if (selectedPageTypeId == "2") {
            alert("Category");
        }
    }
    var bindQualificationForm = function (id, name) {
        $('.overlap_container').show();
        $.ajax({
            type: 'GET',
            url: window.GetQualificationForm,
            async: false,
            data: { id: id, name: name},
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                SetModalWidth(800);
                SetModalTitle("Add Qualification detail of profile")
                SetModalBody(response);
                HideLoadder();
                ShowModal();
            },
            error: function (data) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        });
    }
    var bindQualificationEditForm = function (QualificationRowId, StaffProfileId, name) {
        $('.overlap_container').show();
        $.ajax({
            type: 'GET',
            url: window.GetQualificationEditForm,
            async: false,
            data: { profileId: StaffProfileId, name: name,  QualificationId : QualificationRowId },
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                //$("#btnSubmit").show();
                $('.overlap_container').hide();
                $("#div-editQualification-details").html(response);
                $("#addeditQualificationDetail").modal('show');
                //alert("Hello");

                //calculateTotalArea();
            },
            error: function (data) {
                //$("#addPersonalDetail").modal('hide');
                $('.overlap_container').hide();
            }
        });
    }
    var bindQualificationListForm = function (id, name) {
        $('.overlap_container').show();
        $.ajax({
            type: 'GET',
            url: window.GetQualificationListForm,
            async: false,
            data: { id: id, name: name },
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (response) {
                SetModalTitle("Qualification Details of Staff")
                SetModalBody(response);
                HideLoadder();
                ShowModal();
            },
            error: function (data) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        });
    }

    return {
        Load: function () {
            $(document).on("blur", "#PhoneNumber", function () {
                //alert("Hell0");
                var stdCode = $('#STDCode').val();
                var phonenumberlength = $('#PhoneNumber').val();
                var totalLength = stdCode + phonenumberlength;
                var length = totalLength.length;
                if ($('#PhoneNumber').val() != '') {
                    if (totalLength.length != 11) {
                        alert("Combination of STD and Phone number should be 11 digits.");
                        //alertNotification("Combination of STD and Phone number should be 11 digits.");
                        $('#PhoneNumber').val('');
                    }
                }
            });
            //STD CODE VALIDATION
            $(document).on("blur", "#STDCode", function () {
                if ($('#STDCode').val() != '') {
                    if ($('#STDCode').val().length > 7 || $('#STDCode').val().length < 1) {
                        alertNotification("Enter Valid STD Code");
                        $('#STDCode').val('');
                    }
                    else {
                        var stdCode = $('#STDCode').val();
                        var phonenumberlength = $('#PhoneNumber').val();
                        var totalLength = stdCode + phonenumberlength;
                        if ($('#PhoneNumber').val() != '') {
                            if (totalLength.length != 11) {
                                alertNotification("Combination of STD and Phone number should be 11 digits.");
                                $('#PhoneNumber').val('');
                            }
                        }
                    }
                }
            });
            //$('#addPersonalDetail').hide();
            $('input[type="radio"]').on('click', function () {
                if ($(this).val() == 'True') {
                    $("#AddStaffDetail").show();
                    $("#ExistingStaffDetail").hide();
                };
                if ($(this).val() == 'False') {
                    $("#ExistingStaffDetail").show();
                    $("#AddStaffDetail").hide();
                };
            })

            $(document).on("click", "#btnAdd", function () {
                $('.overlap_container').show();
                bindForm(-1);
            });
            $(document).on("click", ".btn-Add-Qualification", function () {
                var id = $(this).closest('tr').attr('data-keyvalue');
                var name = $(this).closest('tr').find('td:eq(1)').text(); 
                bindQualificationForm(id, name);
            });
            $(document).on("click", ".btn-view-Qualification", function () {
                var id = $(this).closest('tr').attr('data-keyvalue');
                var name = $(this).closest('tr').find('td:eq(1)').text();
                bindQualificationListForm(id, name);
            });
            $(document).on("click", ".ClsEditQualification", function () {
                
                var QualificationRowId = $(this).closest('tr').attr('data-keyvalue');
                var name = $(this).closest('tr').find('td:eq(1)').text();
                var StaffProfileId = $(this).closest('tr').find("TD:eq(8) .hdnStaffProfileId").val();
                bindQualificationEditForm(QualificationRowId, StaffProfileId, name);
            });

            $(document).on("change", "#ddlCategory", function () {
                BindCasteDropDown(this);
            });
            $(document).on("change", "#ddlStaffType", function () {
                BindFacultyDropDown(this);
            });
            $(document).on("change", "#ddlGender", function () {
                var selectedLandOwnership = $("#ddlGender").find("option:selected").text();
                $("#personalEntity_Gender").val(selectedLandOwnership);
            });
            if ($("#LocalIPAddressAddFaculty").val()) {
                GetInfraNormIP = $("#LocalIPAddressAddFaculty").val();
            } else {
                GetInfraNormIP = "NF";
            }
        },

    }

}();