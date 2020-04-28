angular.module('App').controller("HrController", function ($scope, $http, $timeout, $compile) {
    $scope.VendorId = "";
    //For Pagination
    $scope.maxsize = 5;

    //Employee Starts
    $scope.EmpTotalCount = 0;
    $scope.EmpPageIndex = 1;
    $scope.EmpPageSize = "50";
    $scope.SearchEmployeeNameId = "";
    $scope.SearchDesignation = "";
    $scope.SearchDepartment = "";

    $scope.FetchEmployeeList = function () {
        $http.get(window.FetchEmployeeList + "?pageindex=" + $scope.EmpPageIndex + "&pagesize=" + $scope.EmpPageSize + "&SearchEmployeeNameId=" + $scope.SearchEmployeeNameId + "&SearchDesignation=" + $scope.SearchDesignation + "&SearchDepartment=" + $scope.SearchDepartment).success(function (response) {
            $scope.AvailableEmployeeList = response.ListEmployeeEnt;
            $scope.EmpTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.FetchEmployeeList();

    $scope.EmpPageChanged = function () {
        $scope.FetchEmployeeList();
    }

    $scope.EmpChangePageSize = function () {
        $scope.EmpPageIndex = 1;
        $scope.FetchEmployeeList();
    }

    $scope.SelectedFileForUpload = null;

    // THIS IS REQUIRED AS File Control is not supported 2 way binding features of Angular
    // ------------------------------------------------------------------------------------
    //File Validation
    $scope.ChechFileValid = function (file) {
        var isValid = false;
        if ($scope.SelectedFileForUpload != null) {
            if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif') && file.size <= (512 * 1024)) {
                $scope.FileInvalidMessage = "";
                isValid = true;
            }
            else {
                $scope.FileInvalidMessage = "Selected file is Invalid. (only file type png, jpeg and gif and 512 kb size allowed)";
            }
        }
        else {
            $scope.FileInvalidMessage = "Image required!";
        }
        $scope.IsFileValid = isValid;
    };

    //File Select event 
    $scope.selectFileforUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
    }
    //----------------------------------------------------------------------------------------


    $scope.SaveEmployee = function () {

        var Status = false;
        Status = GetFormValidationStatus();

        $scope.ChechFileValid($scope.SelectedFileForUpload);
        var frm = $("#formSaveEmployeeDetail");
        var formData = new FormData(frm[0]);
        formData.append("file", $scope.SelectedFileForUpload);

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveEmployee, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        $scope.FetchEmployeeList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }


    }

    $scope.BindHREmpPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.HREmpPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Employee Detail")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        //});
    }

    $scope.LoadEmployeeViewPopup = function (_EmployeeDetailsId) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, HREmpId: _EmployeeDetailsId },
            datatype: "JSON",
            url: window.HREmpPopup,
            success: function (html) {
                SetModalTitle("View Employee Details")
                SetModalBody(html);
                HideLoadder();
                $('#formSaveEmployeeDetail input[type=radio],input[type=text], select').prop("disabled", true);
                $('#save_results').css('display', 'none');
                $('#cancel_results').css('display', 'none');
                $('#EmpImageUpload').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1200px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function () {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }

    $scope.LoadEmployeeEditPopup = function (_EmployeeDetailsId) {
        var _actionType = "EDIT";
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, HREmpId: _EmployeeDetailsId },
            datatype: "JSON",
            url: window.HREmpPopup,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Employee Details")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                SetModalWidth("1200px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function () {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }

    $scope.DeleteEmployee = function (id) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        $http({ url: window.DeleteEmployeeDetail, method: 'POST', data: { id: id } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    $scope.FetchEmployeeList();
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
    }

    $scope.GetState = function (Country) {
        $http({ url: window.StateDetail, method: 'POST', data: { countryId: Country } }).success(
            function (res) {
                $scope.ListState = res;
                //$('#VendorName').val(res);
            }
        ).error(function (res) { showHttpErr(res); });
    }

    //Quotation Preparation Starts
    $scope.BindPayroll = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "GET",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.PayrollTab,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalPanelBody(html);
                HideLoadder();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }

    //QuotePreparation Modal Starts
    $scope.SavePayrollDetails = function () {
        var frm = $("#formSavePayrollDetail");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus();

        if (!Status) {
            //$scope.ValidationMessage = "Kindly Fill all mandatory fields";
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({
                url: window.SavePayrollDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined }
            }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                        ClearAllFields();
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }

    }


    //$scope.GetEmpPayrollData = function (EmpId) {
    //    $http({ url: '/Hr/GetEmpPayrollData', method: 'POST', data: { EmpId: EmpId } }).success(
    //        function (res) {
    //            //alert(res);
    //            $scope.UserInitial = res.UserInitial;
    //            $scope.UnitNo = res.UnitNo;
    //            $scope.EmpCode = res.EmpCode;
    //            $scope.BasicPay = res.BasicPay;
    //            $scope.HRA = res.HRA;
    //            $scope.Conv = res.Conv;
    //            $scope.OtherAllow = res.OtherAllow;
    //            $scope.CarAllow = res.CarAllow;
    //            $scope.EPF = res.EPF;
    //            $scope.PPF = res.PPF;
    //            $scope.ESI = res.ESI;
    //            $scope.TDSAMT = res.TDSAMT;
    //            $scope.leaveAdj = res.leaveAdj;
    //            $scope.Absent = res.Absent;
    //            $scope.LoanAmt = res.LoanAmt;
    //            $scope.Adv = res.Adv;
    //            $scope.NetPay = res.NetPay
    //        }
    //    ).error(function (res) { showHttpErr(res); });
    //}

    $scope.GetEmpNameForDept = function (Dept) {
        $http({ url: window.GetEmpDetailsForPayroll, method: 'POST', data: { Data: Dept, Type: 'EmpName' } }).success(
            function (res) {
                //alert(res);
                $scope.ListEmpName = res;
            }
        ).error(function (res) { showHttpErr(res); });
    }

    $scope.GetEmpCodeForName = function (EmpName) {
        $http({ url: window.GetEmpDetailsForPayroll, method: 'POST', data: { Data: EmpName, Type: 'EmpCode' } }).success(
            function (res) {
                //alert(res);
                $scope.ListEmpCode = res;
            }
        ).error(function (res) { showHttpErr(res); });
    }

    //$scope.EmpId = '';
    //$scope.Year = '';


    $scope.GetMonthlyEmpPayrollData = function (Month) {
        var EmpId = $scope.EmpId;
        var Year = $scope.Year;

        $http({ url: window.GetEmpPayrollData, method: 'POST', data: { EmpId: EmpId, Yr: Year, mnth:Month } }).success(
            function (res) {
                //alert(res);
                $scope.UserInitial = res.UserInitial;
                $scope.UnitNo = res.UnitNo;
                $scope.EmpCode = res.EmpCode;
                $scope.BasicPay = res.BasicPay;
                $scope.HRA = res.HRA;
                $scope.Conv = res.Conv;
                $scope.OtherAllow = res.OtherAllow;
                $scope.CarAllow = res.CarAllow;
                $scope.EPF = res.EPF;
                $scope.PPF = res.PPF;
                $scope.ESI = res.ESI;
                $scope.TDSAMT = res.TDSAMT;
                $scope.leaveAdj = res.leaveAdj;
                $scope.Absent = res.Absent;
                $scope.LoanAmt = res.LoanAmt;
                $scope.Adv = res.Adv;
                $scope.NetPay = res.NetPay
            }
        ).error(function (res) { showHttpErr(res); });
    }

    //Leave Management Starts
    $scope.BindLeaveManagement = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "GET",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.LeaveManagementTab,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalPanelBody(html);
                HideLoadder();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '5%'
                    });
                }
                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                $('.modal-dialog').draggable({
                    handle: ".modal-body"
                });

            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }

    $scope.SaveEmpLeaveDetails = function () {
        var frm = $("#formSaveLeaveDetail");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus();

        if (!Status) {
            //$scope.ValidationMessage = "Kindly Fill all mandatory fields";
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({
                url: window.SaveEmpLeaveDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined }
            }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                        ClearAllFields();
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }

    }

    $scope.GetEmpLeaveDetails = function (EmpId) {
        $http({ url: window.GetEmpLeaveDetails, method: 'POST', data: { empId: EmpId }  }).success(
            function (res) {
                $scope.AvailableEmpLeavesList = res.ListLeaveMgmt;
            }
        ).error(function (res) { showHttpErr(res); });
    }


});

//EOF