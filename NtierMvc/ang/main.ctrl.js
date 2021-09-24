angular.module('App').controller("MainController", function ($scope, $http, $timeout, $compile, $filter, $window) {

    $('.modal-dialog').draggable({
        handle: ".modal-body"
    });

    //For Pagination
    $scope.maxsize = 5;
    $scope.custTotalCount = 0;
    $scope.custPageIndex = 1;
    $scope.custPageSize = "1000";

    //Customer Starts
    $scope.DefaultCustomerList = function () {
        $scope.SearchCountry = "";
        $scope.SearchCustomerName = "";
        $scope.SearchCustomerID = "";
        $scope.SearchCustomerIsActive = "";
    }

    $scope.FetchCustomerList = function () {
        $http.get(window.FetchCustomerList + "?pageindex=" + $scope.custPageIndex + "&pagesize=" + $scope.custPageSize + "&SearchCountry=" + $scope.SearchCountry + "&SearchCustomerID=" + $scope.SearchCustomerID + "&SearchCustomerIsActive=" + $scope.SearchCustomerIsActive).success(function (response) {
            $scope.AvailableCustomerList = response.LstCusEnt;
            $scope.custTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.CustPageChanged = function () {
        $scope.FetchCustomerList();
    }

    $scope.CustChangePageSize = function () {
        $scope.custPageIndex = 1;
        $scope.FetchCustomerList();
    }

    $scope.BindCustomerPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.OtherDetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Add New Customer")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1400px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '1%'
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

    $scope.LoadCustomerViewPopup = function (_CustomerDetailsId) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, CustomerId: _CustomerDetailsId },
            datatype: "JSON",
            url: window.OtherDetailsPopup,
            success: function (html) {
                SetModalTitle("View Customer Details")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1400px");
                $('#formSaveCustomerDetail input[type=radio],input[type=text], select').prop("disabled", true);
                $('#saveCust').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '1%'
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

    $scope.LoadCustomerEditPopup = function (_CustomerDetailsId) {
        var _actionType = "EDIT";
        $scope.SubmitCustomer = 'Save';
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, CustomerId: _CustomerDetailsId },
            datatype: "JSON",
            url: window.OtherDetailsPopup,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Customer Details")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1400px");
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '15%',
                        left: '1%'
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

    $scope.DeleteCustomer = function (id) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        $http({ url: window.DeleteCustomerDetail, method: 'POST', data: { id: id } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    $scope.FetchCustomerList();
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
    }

    $scope.GenerateReport = function (Type) {
        //var DownloadType = dwnldtype;
        ShowLoadder();

        $http.get(window.GenerateReport + "?ReportType=" + Type + "&pageindex=" + $scope.custPageIndex + "&pagesize=" + $scope.custPageSize + "&SearchCountry=" + $scope.SearchCountry + "&SearchCustomerID=" + $scope.SearchCustomerID + "&SearchCustomerIsActive=" + $scope.SearchCustomerIsActive).success(function (data) {
                if (data != "") {
                    //use window.location.href for redirect to download action for download the file
                    window.location.href = window.DownloadDoc + '?fileName=' + data;
                    HideLoadder();
                }
                else {
                    alert(data.errorMessage);
                    HideLoadder();
                }
            }, function (error) {
                alert('failed');
            });

        //$.ajax({
        //    type: "Post",
        //    url: window.GenerateReport,
        //    data: JSON.stringify({ ReportType: Type, pageindex: $scope.custPageIndex, pagesize: $scope.custPageSize, SearchCountry: $scope.SearchCountry, SearchCustomerID: $scope.SearchCustomerID, SearchCustomerIsActive:$scope.SearchCustomerIsActive}),
        //    contentType: "application/json; charset=utf-8",
        //    success: function (data) {
        //        //alert("Dowloaded Successfully" + data);
        //        if (data != "") {
        //            //use window.location.href for redirect to download action for download the file
        //            window.location.href = window.DownloadDoc + '?fileName=' + data;
        //            HideLoadder();
        //        }
        //        else {
        //            alert(data.errorMessage);
        //            HideLoadder();
        //        }
        //    },
        //    error: function (x, e) {
        //        alert('Some error is occurred, Please try after some time.');
        //        HideLoadder();
        //    }
        //})
    }
    $scope.GenerateCustReport = function (Type) {
        //var DownloadType = dwnldtype;
        ShowLoadder();

        $.ajax({
            type: "POST",
            url: window.GenerateReport,
            data: json.stringify({ ReportType: Type}),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //alert("Dowloaded Successfully" + data);
                if (data != "") {
                    //use window.location.href for redirect to download action for download the file
                    window.location.href = window.DownloadDoc + '?fileName=' + data;
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


    $scope.enqTotalCount = 0;
    $scope.enqPageIndex = 1;
    $scope.enqPageSize = "1000";

    //Enquiry Starts
    $scope.DefaultEnquiryList = function () {
        $scope.SearchEQEnqType = "";
        $scope.SearchCustomerName = "";
        $scope.SearchEnqFor = "";
        $scope.SearchDueDate = "";
        $scope.SearchEOQ = "";
    }


    $scope.FetchEnquiryList = function () {
        var x = document.getElementById("EQDueDate");
        var DueDate = '';
        if (x != '') {
            for (var i = 0; i < x.options.length; i++) {
                if (x.options[i].selected == true) {
                    //alert(x.options[i].value);
                    DueDate = DueDate + formatDate(x.options[i].value, 'yyyy-MM-dd') + ',';
                }
            }

            DueDate = DueDate.substring(0, DueDate.length - 1);
        }
        
        $http.get(window.FetchEnquiryList + "?pageindex=" + $scope.enqPageIndex + "&pagesize=" + $scope.enqPageSize + "&SearchEQEnqType=" + $scope.SearchEQEnqType + "&SearchCustomerName=" + $scope.SearchCustomerName + "&SearchEnqFor=" + $scope.SearchEnqFor + "&SearchEQDueDate=" + DueDate + "&SearchEOQ=" + $scope.SearchEOQ).success(function (response) {
            $scope.AvailableEnquiryList = response.lstEnqEntity;
            $scope.enqTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.EnqPageChanged = function () {
        $scope.FetchEnquiryList();
    }

    $scope.EnqChangePageSize = function () {
        $scope.enqPageIndex = 1;
        $scope.FetchEnquiryList();
    }

    //$scope.FetchEnquiryList();

    $scope.BindEnquiryPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.EnquiryDetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Add New Enquiry")
                SetModalBody(html);
                HideLoadder();
                ShowModal();
                SetModalWidth("1400px");

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '1%'
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

    $scope.LoadEnquiryViewPopup = function (_EnquiryDetailsId) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, EnquiryId: _EnquiryDetailsId },
            datatype: "JSON",
            url: window.EnquiryDetailsPopup,
            success: function (html) {
                SetModalTitle("View Enquiry Details")
                SetModalBody(html);
                HideLoadder();
                $('#formSaveEnquiryDetail input[type=radio],input[type=text], select').prop("disabled", true);
                $('#save_results').css('display', 'none');
                $('#cancel_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();
                SetModalWidth("1400px");

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '1%'
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

    $scope.LoadEnquiryEditPopup = function (_EnquiryDetailsId) {
        var _actionType = "EDIT"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, EnquiryId: _EnquiryDetailsId },
            datatype: "JSON",
            url: window.EnquiryDetailsPopup,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Enquiry Details")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();
                SetModalWidth("1400px");

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '1%'
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

    $scope.DeleteEnquiry = function (id) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        $http({ url: window.DeleteEnquiryDetail, method: 'POST', data: { id: id } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    $scope.FetchEnquiryList();
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
    }

    $scope.quotTotalCount = 0;
    $scope.quotPageIndex = 1;
    $scope.quotPageSize = "1000";

    //Quotation Starts
    $scope.DefaultQuotationList = function () {
        $scope.SearchQuoteType = "";
        $scope.SearchQuoteCustomerID = "";
        $scope.SearchSubject = "";
        $scope.SearchDeliveryTerms = "";
    }


    $scope.FetchQuotationList = function () {
        $http.get(window.FetchQuotationList + "?pageIndex=" + $scope.quotPageIndex + "&pageSize=" + $scope.quotPageSize + "&SearchQuoteType=" + $scope.SearchQuoteType + "&SearchQuoteCustomerID=" + $scope.SearchQuoteCustomerID + "&SearchSubject=" + $scope.SearchSubject + "&SearchDeliveryTerms=" + $scope.SearchDeliveryTerms).success(function (response) {
            $scope.AvailableQuotationList = response.lstQuoteEntity;
            $scope.quotTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    //$scope.FetchQuotationList();

    $scope.QuotPageChanged = function () {
        $scope.FetchQuotationList();
    }

    $scope.QuotChangePageSize = function () {
        $scope.quotPageIndex = 1;
        $scope.FetchQuotationList();
    }

    $scope.BindQuotationPopup = function () {
        //$(".btn-Add-QuotationDetails").on("click", function (e) {

        $scope.VendorName = "";
        $scope.Country = "";
        $scope.CountryId = "";
        $scope.GeoArea = "";
        $scope.GeoCode = "";
        $scope.QuoteType = "";

        var _actionType = "ADD"
        //var _QuotationDetailsId = $(this).parents("tr:first").find("#QuotationDetailsId").val();
        //var _staffProfileName = $(this).parents("tr:first").find("#StaffFirstName").val();
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.QuotationDetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Add New Quotation")
                SetModalBody(html);
                HideLoadder();
                ShowModal();
                SetModalWidth("1400px");

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '1%'
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

    $scope.LoadQuotationViewPopup = function (_QuotationDetailsId) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, QuotationId: _QuotationDetailsId },
            datatype: "JSON",
            url: window.QuotationDetailsPopup,
            success: function (html) {
                SetModalTitle("View Quotation Details")
                SetModalBody(html);
                HideLoadder();
                $('#formSaveQuotationDetail input[type=radio],input[type=text], select, textarea').prop("disabled", true);
                $('#save_results').css('display', 'none');
                $('#cancel_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();
                SetModalWidth("1400px");

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '1%'
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

    $scope.LoadQuotationEditPopup = function (_QuotationDetailsId, _QuoteNoView) {
        var _actionType = "EDIT";
        let _QuotationUrl = "";

        if (_QuoteNoView.indexOf('Rev') != -1 || _QuoteNoView.indexOf('FRPO') != -1)
            _QuotationUrl = window.RevisedQuotationDetailsPopup;
        else
            _QuotationUrl = window.QuotationDetailsPopup;

        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, QuotationId: _QuotationDetailsId},
            datatype: "JSON",
            url: _QuotationUrl,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Quotation Details")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();
                SetModalWidth("1400px");
                RQGetQuoteNoItemDetail();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '1%'
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

    $scope.DeleteQuotation = function (id) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        $http({ url: window.DeleteQuotationDetail, method: 'POST', data: { id: id } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    $scope.FetchQuotationList();
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
    }

    //Quotation Registration Starts
    $scope.quotRegTotalCount = 0;
    $scope.quotRegPageIndex = 1;
    $scope.quotRegPageSize = "1000";
    $scope.SearchQuotRegVendorID = "";
    $scope.SearchQuotRegVendorName = "";
    $scope.SearchQuotRegQuoteNo = "";
    $scope.SearchQuotRegProductGrp = "";
    $scope.SearchQuotRegEnqFor = "";
    $scope.SearchQuotRegQuoteType = "";
    $scope.FetchQuoteRegList = function () {
        $http.get(window.FetchQuoteRegList + "?pageindex=" + $scope.quotPageIndex + "&pagesize=" + $scope.quotPageSize + "&SearchQuotRegVendorID=" + $scope.SearchQuotRegVendorID + "&SearchQuotRegVendorName=" + $scope.SearchQuotRegVendorName + "&SearchQuotRegQuoteNo=" + $scope.SearchQuotRegQuoteNo + "&SearchQuotRegProductGrp=" + $scope.SearchQuotRegProductGrp + "&SearchQuotRegEnqFor=" + $scope.SearchQuotRegEnqFor + "&SearchQuotRegQuoteType=" + $scope.SearchQuotRegQuoteType).success(function (response) {
            $scope.AvailableQuoteRegList = response.lstQuoteRegEntity;
            $scope.quotTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.QuotRegPageChanged = function () {
        $scope.FetchQuoteRegList();
    }

    $scope.QuotRegChangePageSize = function () {
        $scope.quotPageIndex = 1;
        $scope.FetchQuoteRegList();
    }

    //Quotation Preparation Starts
    $scope.BindQuotePrepPopup = function () {
        //$(".btn-Add-QuotationDetails").on("click", function (e) {
        ShowLoadder();
        var _actionType = "ADD"
        //var _QuotationDetailsId = $(this).parents("tr:first").find("#QuotationDetailsId").val();
        //var _staffProfileName = $(this).parents("tr:first").find("#StaffFirstName").val();
        $.ajax({
            type: "GET",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.QuotePrepPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalPanelBody(html);
                $('.ShowHideFields').hide();
                HideLoadder();
            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        //});
    }

    $scope.GetProductLineDetails = function (ProductLine) {
        $http({ url: window.GetProductLineList, method: 'POST', data: { productLine: ProductLine } }).success(
            function (res) {
                $scope.ProductNameList = res.ProductNameListList;
            }
        ).error(function (res) { showHttpErr(res); });
    }

    $scope.GetProductNumber = function (ProductName) {
        var ProductType = $scope.ProductType;
        $http({ url: window.GetProductNumber, method: 'POST', data: { productNameId: ProductName, productType: ProductType } }).success(
            function (res) {
                $scope.ProductNo = res.ProductNo;
            }
        ).error(function (res) { showHttpErr(res); });
    }

    //For Pdf
    var getLayout = function () {
        return {
            hLineWidth: function () {
                return 0.1;
            },
            vLineWidth: function () {
                return 0.1;
            },
            hLineColor: function () {
                return 'gray';
            },
            vLineColor: function () {
                return 'gray';
            }
        };
    };

    var getTable = function () {
        return [
            { text: 'PDF Print Test', fontSize: 20, bold: false, alignment: 'center', style: ['lineSpacing', 'headingColor'] },
            { canvas: [{ type: 'line', x1: 0, y1: 5, x2: 595 - 2 * 40, y2: 5, lineWidth: 1, lineColor: '#990033', style: ['lineSpacing'] }] },
            { text: '', style: ['doublelineSpacing'] },
            {
                table: {
                    widths: ['auto', 'auto', 'auto', 'auto'],
                    headerRows: 1,
                    // keepWithHeaderRows: 1,
                    body: [
                        ['Project', { text: 'Technology', style: 'tableHeader' }, 'Language', 'Database'],
                        ['Intranet', 'Microsoft', { text: 'Sharepoint', colSpan: 2 }, {}],
                        ['Online Jobs', 'Microsoft', 'Asp.Net', 'SQL Server']

                    ]
                }, layout: getLayout()
            }

        ];
    };

    $scope.GenerateQuoteReport = function () {
        var docDefinition = {
            pageMargins: [72, 80, 40, 60],
            layout: 'headerLineOnly',
            pageSize: 'A4',
            header: function () {

                return {
                    columns: [
                        {
                            text: 'Csharp',
                            width: 200,
                            margin: [50, 20, 5, 5]
                        },
                        {
                            stack: [
                                { text: 'Project Details', alignment: 'right', fontSize: 12, margin: [0, 30, 50, 0] }
                            ]
                        }
                    ]
                }
            },

            footer: function (currentPage, pageCount) {
                return {
                    stack: [{ canvas: [{ type: 'line', x1: 0, y1: 5, x2: 595, y2: 5, lineWidth: 1, lineColor: '#ffff00', style: ['lineSpacing'] }] },
                    { text: '', margin: [0, 0, 0, 5] },
                    {
                        columns: [
                            {},
                            { text: currentPage.toString(), alignment: 'center' },
                            { text: moment(new Date()).format("DD-MMM-YYYY"), alignment: 'right', margin: [0, 0, 20, 0] }
                        ]
                    }]

                };
            },
            content: [
                { stack: getTable() }
            ],
            styles: {
                'lineSpacing': {
                    margin: [0, 0, 0, 6]
                },
                'doublelineSpacing': {
                    margin: [0, 0, 0, 12]
                },
                'headingColor':
                {
                    color: '#999966'
                },
                tableHeader: {
                    bold: true,
                    fontSize: 13,
                    color: '#669999'
                }
            }
        }

        //  pdfMake.createPdf(docDefinition).open();
        var date = new Date();
        date = moment(date).format('DD_MMM_YYYY_HH_mm_ss');
        pdfMake.createPdf(docDefinition).download('PDF_' + date + '.pdf');

    };
    //For Pdf

    $scope.orderTotalCount = 0;
    $scope.orderPageIndex = 1;
    $scope.orderPageSize = "1000";

    //Order Starts
    $scope.DefaultOrdersList = function () {
        $scope.SearchOrderQuoteType = "";
        $scope.SearchOrderCustomerId = "";
        $scope.SearchOrderProductGroup = "";
        $scope.SearchOrderDeliveryTerms = "";
        $scope.SearchPODeliveryDate = "";
    }

    $scope.FetchOrdersList = function () {

        var PODeliveryDate = '';
        var x = document.getElementById("ORPODeliveryDate");

        if (x.length > 0) {
            for (var i = 0; i < x.options.length; i++) {
                if (x.options[i].selected == true) {
                    PODeliveryDate = PODeliveryDate + convertDateFormat(x.options[i].value,'dd-MM-yyyy', 'MM-dd-yyyy') + ',';
                }
            }
        }

        PODeliveryDate = PODeliveryDate.substring(0, PODeliveryDate.length - 1);

        $http.get(window.FetchOrdersList + "?pageindex=" + $scope.orderPageIndex + "&pagesize=" + $scope.orderPageSize + "&SearchQuoteType=" + $scope.SearchOrderQuoteType + "&SearchCustomerID=" + $scope.SearchOrderCustomerId + "&SearchProductGroup=" + $scope.SearchOrderProductGroup + "&SearchDeliveryTerms=" + $scope.SearchOrderDeliveryTerms + "&SearchPODeliveryDate=" + PODeliveryDate).success(function (response) {
            $scope.AvailableOrdersList = response.lstOrderEntity;
            $scope.orderTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }
    $scope.GeneratePOReport = function (Type) {
        //var DownloadType = dwnldtype;
        ShowLoadder();

        var PODeliveryDate = '';
        var x = document.getElementById("ORPODeliveryDate");

        if (x.length > 0) {
            for (var i = 0; i < x.options.length; i++) {
                if (x.options[i].selected == true) {
                    PODeliveryDate = PODeliveryDate + convertDateFormat(x.options[i].value, 'dd-MM-yyyy', 'MM-dd-yyyy') + ',';
                }
            }
        }

        PODeliveryDate = PODeliveryDate.substring(0, PODeliveryDate.length - 1);

        $http.get(window.GeneratePOReport + "?ReportType=" + Type+"&pageindex=" + $scope.orderPageIndex + "&pagesize=" + $scope.orderPageSize + "&SearchQuoteType=" + $scope.SearchOrderQuoteType + "&SearchCustomerID=" + $scope.SearchOrderCustomerId + "&SearchProductGroup=" + $scope.SearchOrderProductGroup + "&SearchDeliveryTerms=" + $scope.SearchOrderDeliveryTerms + "&SearchPODeliveryDate=" + PODeliveryDate).success(function (data) {
            if (data != "") {
                //use window.location.href for redirect to download action for download the file
                window.location.href = window.DownloadDoc + '?fileName=' + data;
                HideLoadder();
            }
            else {
                alert(data.errorMessage);
                HideLoadder();
            }
        }, function (error) {
            alert('failed');
        });

        //$http.get(window.GeneratePOReport + "?ReportType=" + Type + "&pageindex=" + $scope.custPageIndex + "&pagesize=" + $scope.custPageSize + "&SearchCountry=" + $scope.SearchCountry + "&SearchCustomerID=" + $scope.SearchCustomerID + "&SearchCustomerIsActive=" + $scope.SearchCustomerIsActive).success(function (data) {
        //    if (data != "") {
        //        //use window.location.href for redirect to download action for download the file
        //        window.location.href = window.DownloadDoc + '?fileName=' + data;
        //        HideLoadder();
        //    }
        //    else {
        //        alert(data.errorMessage);
        //        HideLoadder();
        //    }
        //}, function (error) {
        //    alert('failed');
        //});

        //$.ajax({
        //    type: "Post",
        //    url: window.GeneratePOReport,
        //    data: JSON.stringify({ ReportType: Type, pageindex: $scope.orderPageIndex, pagesize: $scope.orderPageSize, SearchOrderQuoteType: $scope.SearchOrderQuoteType, SearchVendorID: $scope.SearchOrderVendorID, SearchProductGroup: $scope.SearchOrderProductGroup, SearchDeliveryTerms: $scope.SearchOrderDeliveryTerms, SearchPODeliveryDate: $scope.SearchPODeliveryDate }),
        //    contentType: "application/json; charset=utf-8",
        //    success: function (data) {
        //        //alert("Dowloaded Successfully" + data);
        //        if (data != "") {
        //            //use window.location.href for redirect to download action for download the file
        //            window.location.href = window.DownloadDoc + '?fileName=' + data;
        //            HideLoadder();
        //        }
        //        else {
        //            alert(data.errorMessage);
        //            HideLoadder();
        //        }
        //    },
        //    error: function (x, e) {
        //        alert('Some error is occurred, Please try after some time.');
        //        HideLoadder();
        //    }
        //})
    }

    $scope.BindOrderPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.OrderDetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Add New Order")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                ShowModal();
                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '15%',
                        left: '10%'
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

    $scope.LoadOrderViewPopup = function (_OrderId) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, Id: _OrderId },
            datatype: "JSON",
            url: window.OrderDetailsPopup,
            success: function (html) {
                SetModalTitle("View Order Details")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                $('#formOrderEnquiryDetail input[type=radio],input[type=text], select').prop("disabled", true);
                $('#save_results').css('display', 'none');
                $('#cancel_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '15%',
                        left: '10%'
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

    $scope.LoadOrderEditPopup = function (_OrderId) {
        var _actionType = "EDIT"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, Id: _OrderId },
            datatype: "JSON",
            url: window.OrderDetailsPopup,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Order Details")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1200px");
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '15%',
                        left: '10%'
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

    $scope.DeleteOrder = function (id) {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        $http({ url: window.DeleteOrderDetail, method: 'POST', data: { id: id } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    $scope.FetchOrdersList();
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
    }

    $scope.BindRevisedOrderPopup = function () {

        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.RevisedOrderDetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Add New Revised Order")
                SetModalBody(html);
                SetModalWidth("1200px");
                HideLoadder();
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

    //For Item
    $scope.BindItemPopup = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.ItemDetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Add Item Wise Entry")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1400px");
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

    //For Quote Revised Form
    $scope.BindRevisedQuotationPopup = function () {

        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.RevisedQuotationDetailsPopup,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Quote Add-ON")
                SetModalBody(html);
                SetModalWidth("1400px");
                HideLoadder();
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
    
    //Clarification Starts
    $scope.BindClarificationPopup = function () {

        var _actionType = "ADD";
        $.ajax({
            type: "GET",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.Clarification,
            success: function (html) {
                html = $compile(html)($scope);
                SetParamModalPanelBody('ClarificationPanelBody', html);
                HideLoadder();
            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        //});
    }


    $scope.BindInboundPopUp = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "POST",
            data: { actionType: _actionType },
            datatype: "JSON",
            url: window.InboundPopUp,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Gate Entry")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1400px");
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '3%'
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

    //Gate Entry List Starts
    $scope.INBTotalCount = 0;
    $scope.INBPageIndex = 1;
    $scope.INBPageSize = "1000";
    $scope.SearchType = "";
    $scope.SearchVendorNature = "";
    $scope.SearchVendorName = "";
    $scope.SearchPONo = "";

    $scope.FetchInboundList = function () {
        $http.get(window.FetchInboundsList + "?pageIndex=" + $scope.INBPageIndex + "&pageSize=" + $scope.INBPageSize + "&SearchType=" + $scope.SearchType + "&SearchVendorNature=" + $scope.SearchVendorNature + "&SearchVendorName=" + $scope.SearchVendorName + "&SearchPONo=" + $scope.SearchPONo).success(function (response) {
            $scope.InboundList = response.lstVBM;
            $scope.INBTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.INBPageChanged = function () {
        $scope.FetchInboundList();
    }

    $scope.INBChangePageSize = function () {
        $scope.INBPageIndex = 1;
        $scope.FetchInboundList();
    }

    //$scope.FetchInboundList();


    $scope.LoadInboundViewPopup = function (_GateNo) {
        var _actionType = "VIEW"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, GateNo: _GateNo },
            datatype: "JSON",
            url: window.InboundPopUp,
            success: function (html) {
                SetModalTitle("View Inbound Details")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1400px");
                ShowModal();
                //$scope.GetPODetailsFromSupplyType();

                $scope.GetPOTableDetailsForGateEntry();
                $('#formInbound input[type=checkbox],input[type=radio],input[type=text], select').prop("disabled", true);
                $('.save_results').css('display', 'none');
                $('.cancel_results').css('display', 'none');

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '0%'
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

    $scope.LoadInboundEditPopup = function (_GateNo) {
        var _actionType = "EDIT"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, GateNo: _GateNo },
            datatype: "JSON",
            url: window.InboundPopUp,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Inbound Details")
                SetModalBody(html);
                HideLoadder();
                SetModalWidth("1400px");
                ShowModal();
                //$scope.GetPODetailsFromSupplyType();
                $scope.GetPOTableDetailsForGateEntry();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '0%'
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

    $scope.GetPOTableDetailsForGateEntry = function () {
        var POSetno = $('#GEVendorPONO').val();
        var GateNo = $('#GateNo').val();

        $.ajax({
            url: window.GetPOTableDetailsForGateEntry,
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ POSetno: POSetno, GateNo: GateNo }),
            success: function (data) {

                if (data.length > 0) {
                    $('#tableSelected').val(data[0].PRCat);
                    $("#HiddenPOSetno").val(data[0].POSetno);
                    $('#GEPRCat').val(data[0].PRCat);
                    $('#GEVendorPONO').val(POSetno);
                    $('#GEPRCat').attr("disabled", true);

                    $('#GEPODate').val(data[0].POdate);
                    $('#GEPOValidity').val(data[0].POValidity);
                    $('#GEWorkNo').val(data[0].WorkNo);
                    $('#GEDeliveryDate').val(data[0].DeliveryDate);
                    $('#GEPOValidity').val(data[0].POValidity);
                    $('#GEPORevNo').val(data[0].PORevNo);
                    $('#GEItemCategory').val(data[0].ItemCategory);
                    $('#GEModeOfTransport').val(data[0].ModeOfTransport);
                    $('#GEGateControlNo').val(data[0].GateControlNo);
                    $('#GESupplyTerms').val(data[0].SupplyTerms);
                    $('#GateNo').val(data[0].GateNo);

                    switch (data[0].PRCat) {
                        case 'RM':
                            $('#tableRM').show();
                            $('#tableRM tbody').empty();

                            $.each(data, function (i, item) {
                                $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.PRqty + '<span></td><td><span>' + item.UOM + '<span></td><td><span>' + item.UnitPrice + '<span></td><td><span>' + item.Discount + '</span>%</td><td><span>' + item.TotalPrice + '</span></td><td><input type="text" name="LotQty" class="form-control" placeholder="Enter Lot Name" /></td><td><input type="text" name="LotDate" class="form-control NoEndDate" placeholder="Enter Lot Date" /></td><td><input type="text" name="LotQty" class="form-control" placeholder="Enter Lot Qty" /></td></tr>');
                            })

                            break;
                        case 'BOI':
                            $('.tableBOI').show();
                            break;
                        case 'JW':
                            $('.tableJW').show();
                            break;
                        case 'GI':
                            $('.tableGI').show();
                            break;
                        case 'C':
                            $('.tableC').show();
                            break;
                        case 'O':
                            $('.tableO').show();
                            break;
                        default:
                            break;
                    }


                }
            },
            error: function (res) {
                alert(res);
            }
        })
    }

    $scope.BindDescDetails = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "GET",
            data: ({ actionType: _actionType }),
            datatype: "JSON",
            url: window.BindDescDetail,
            success: function (html) {
                event.preventDefault();
                html = $compile(html)($scope);
                SetModalTitle("Add Master Item")
                SetModalBody(html);
                SetModalWidth("1200px");
                ShowModal();
                $scope.PosNo = 1;

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '3%'
                    });
                }

                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                LoadDescDetails();
                //$http.get('/Technical/GetDescLists').then(function (response) {
                //    $scope.DescMailPLList = response.data.DescMailPL;
                //    $scope.DescSubPLList = response.data.DescSubPL;
                //    $scope.DescPosList = response.data.DescPosList;
                //    $scope.FieldNameList = response.data.FieldNameList;
                //});

                //$scope.DescMailPL = "";

                HideLoadder();

            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        //});
    }

    $scope.BindPLDetail = function () {
        var _actionType = "ADD"
        $.ajax({
            type: "GET",
            data: ({ actionType: _actionType }),
            datatype: "JSON",
            url: window.BindPLDetails,
            success: function (html) {
                html = $compile(html)($scope);
                SetModalTitle("Add PL")
                SetModalBody(html);
                SetModalWidth("1200px");
                ShowModal();
                $scope.PosNo = 1;

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '3%'
                    });
                }

                $('#ModalPopup').modal({
                    backdrop: false,
                    show: true
                });

                LoadMasterPLAndSubPL();

                HideLoadder();

            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
    }

    $scope.PosNo = 1;
    $scope.addDescElement = function () {
        $scope.PosNo = parseInt($scope.PosNo) + 1;
        $('#Pos' + $scope.PosNo).show();
    }

    //$scope.finalDesQuery = "";
    //$scope.makeDescQuery = function () {
    //    var currentSelected = $filter('filter')($scope.DescMailPLList, { id: $scope.DescMailPL })[0]
    //    $window.alert("Selected Value: " + currentSelected.DataStringValueField + "\nSelected Text: " + currentSelected.DataTextField);
    //}

    $scope.DeleteQuotePrepItem = function () {
        if (!confirm("Are you sure to delete?")) {
            return;
        }
        //show_loader();
        let QuoteType = $("#QuotePrepFormType").val();
        let QuoteNumber = $("#QuotePrepFormNo").val();
        let ItemNo = $("#QuotePrepItemNo").val();

        if (ItemNo == '' || QuoteType == '' || QuoteNumber == '') {
            alert("Kindly Select ItemNo, QuoteType and QuoteNumber");
            return;
        }

        $http({ url: window.DeleteQuotationPrepDetail, method: 'POST', data: { ItemNo: ItemNo, QuoteType: QuoteType, QuoteNumber: QuoteNumber  } }).success(
            function (res) {
                if (res == 'Deleted Successfully!') {
                    alert(res);
                } else {
                    alert(res, 'E');
                }
            }
        ).error(function (res) { showHttpErr(res); });
    }

    $scope.BindReportPopup = function () {

        $.ajax({
            type: "GET",
            data: {  },
            datatype: "JSON",
            url: window.ReportsPopUp,
            success: function (html) {
                html = $compile(html)($scope);
                SetParamModalPanelBody('ReportsPanelBody', html);
                HideLoadder();
            },
            error: function (r) {
                HideLoadder();
                alert(window.ErrorMsg);
            }
        })
        //});
    }


});

//EOF