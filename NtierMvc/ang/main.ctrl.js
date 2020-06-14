angular.module('App').controller("MainController", function ($scope, $http, $timeout, $compile) {
    $scope.VendorId = "";
    //For Pagination
    $scope.maxsize = 5;
    
    //Customer Starts
    $scope.custTotalCount = 0;
    $scope.custPageIndex = 1;
    $scope.custPageSize = "50";
    $scope.SearchCustName = "";
    $scope.SearchCustVendorID = "";
    $scope.SearchCustVendorType = "";
    $scope.SearchCustVendorNature = "";
    $scope.SearchCustFunctionalArea = "";

    $scope.FetchCustomerList = function () {
        $http.get(window.FetchCustomerList + "?pageindex=" + $scope.custPageIndex + "&pagesize=" + $scope.custPageSize + "&SearchCustName=" + $scope.SearchCustName + "&SearchCustVendorType=" + $scope.SearchCustVendorType + "&SearchCustVendorID=" + $scope.SearchCustVendorID + "&SearchCustVendorNature=" + $scope.SearchCustVendorNature + "&SearchCustFunctionalArea=" + $scope.SearchCustFunctionalArea).success(function (response) {
            $scope.AvailableCustomerList = response.LstCusEnt;
            $scope.custTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }
    
    $scope.FetchCustomerList();
    
    $scope.CustPageChanged = function () {
        $scope.FetchCustomerList();
    }

    $scope.CustChangePageSize = function () {
        $scope.custPageIndex = 1;
        $scope.FetchCustomerList();
    }

    $scope.BindCustomerPopup = function () {
        //$(".btn-Add-CustomerDetails").on("click", function (e) {
        var _actionType = "ADD"
        //var _CustomerDetailsId = $(this).parents("tr:first").find("#CustomerDetailsId").val();
        //var _staffProfileName = $(this).parents("tr:first").find("#StaffFirstName").val();
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
                $('#formSaveCustomerDetail input[type=radio],input[type=text], select').prop("disabled", true);
                $('#save_results').css('display', 'none');
                $('#cancel_results').css('display', 'none');
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '15%',
                        left: '15%'
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
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '15%',
                        left: '15%'
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

    //Enquiry Starts
    $scope.enqTotalCount = 0;
    $scope.enqPageIndex = 1;
    $scope.enqPageSize = "50";
    $scope.SearchEnquiryString = "";
    $scope.SearchEnqName = "";
    $scope.SearchEnqVendorID = "";
    $scope.SearchProductGroup = "";
    $scope.SearchEOQ = "";
    $scope.SearchMonth = "";

    $scope.FetchEnquiryList = function () {
        $http.get(window.FetchEnquiryList+"?pageindex=" + $scope.enqPageIndex + "&pagesize=" + $scope.enqPageSize + "&SearchEnqName=" + $scope.SearchEnqName + "&SearchEnqVendorID=" + $scope.SearchEnqVendorID + "&SearchProductGroup=" + $scope.SearchProductGroup + "&SearchMonth=" + $scope.SearchMonth + "&SearchEOQ=" + $scope.SearchEOQ).success(function (response) {
            $scope.AvailableEnquiryList = response.lstEnqEntity;
            $scope.enqTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }
    //$scope.FetchEnquiryList = function () {
    //    $http({ url: '/Enquiry/FetchEnquiryList', method: 'GET', params: $scope.sdata1 }).success(
    //        function (res) {
    //            $scope.AvailableEnquiryList = res;
    //        }
    //    ).error(function (res) { showHttpErr(res); });
    //}

    $scope.FetchEnquiryList();

    $scope.EnqPageChanged = function () {
        $scope.FetchEnquiryList();
    }

    $scope.EnqChangePageSize = function () {
        $scope.enqPageIndex = 1;
        $scope.FetchEnquiryList();
    }

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

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '25%'
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

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
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

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
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

    //Quotation Starts
    $scope.quotTotalCount = 0;
    $scope.quotPageIndex = 1;
    $scope.quotPageSize = "50";
    $scope.SearchQuoteVendorID = "";
    $scope.SearchQuoteVendorName = "";
    $scope.SearchQuoteNo = "";
    $scope.SearchQuoteProductGroup = "";
    $scope.SearchQuoteEnqFor = "";
    $scope.SearchQuoteType = "";

    $scope.FetchQuotationList = function () {
        $http.get(window.FetchQuotationList+"?pageIndex=" + $scope.quotPageIndex + "&pageSize=" + $scope.quotPageSize + "&SearchQuoteType=" + $scope.SearchQuoteType + "&SearchQuoteNo=" + $scope.SearchQuoteNo + "&SearchQuoteVendorID=" + $scope.SearchQuoteVendorID + "&SearchQuoteVendorName=" + $scope.SearchQuoteVendorName + "&SearchQuoteProductGroup=" + $scope.SearchQuoteProductGroup + "&SearchQuoteEnqFor=" + $scope.SearchQuoteEnqFor).success(function (response) {
            $scope.AvailableQuotationList = response.lstQuoteEntity;
            $scope.quotTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
    }

    $scope.FetchQuotationList();

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

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '25%'
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

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '25%'
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

    $scope.LoadQuotationEditPopup = function (_QuotationDetailsId) {
        var _actionType = "EDIT"
        //var ID = e.target.id;
        $.ajax({
            type: "POST",
            data: { actionType: _actionType, QuotationId: _QuotationDetailsId },
            datatype: "JSON",
            url: window.QuotationDetailsPopup,
            success: function (res) {
                var html = $compile(res)($scope);
                SetModalTitle("Edit Quotation Details")
                SetModalBody(html);
                HideLoadder();
                $('.bs-tooltip-top').css('display', 'none');
                ShowModal();

                if (!($('.modal.in').length)) {
                    $('.modal-dialog').css({
                        top: '5%',
                        left: '25%'
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
    $scope.quotRegPageSize = "50";
    $scope.SearchQuotRegVendorID = "";
    $scope.SearchQuotRegVendorName = "";
    $scope.SearchQuotRegQuoteNo = "";
    $scope.SearchQuotRegProductGrp = "";
    $scope.SearchQuotRegEnqFor = "";
    $scope.SearchQuotRegQuoteType = "";
    $scope.FetchQuoteRegList = function () {
        $http.get(window.FetchQuoteRegList+"?pageindex=" + $scope.quotPageIndex + "&pagesize=" + $scope.quotPageSize + "&SearchQuotRegVendorID=" + $scope.SearchQuotRegVendorID + "&SearchQuotRegVendorName=" + $scope.SearchQuotRegVendorName + "&SearchQuotRegQuoteNo=" + $scope.SearchQuotRegQuoteNo + "&SearchQuotRegProductGrp=" + $scope.SearchQuotRegProductGrp + "&SearchQuotRegEnqFor=" + $scope.SearchQuotRegEnqFor + "&SearchQuotRegQuoteType=" + $scope.SearchQuotRegQuoteType).success(function (response) {
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

    //For Order

    $scope.FetchOrdersList = function () {
        $http.get(window.FetchOrdersList+"?pageindex=" + $scope.custPageIndex + "&pagesize=" + $scope.custPageSize + "&SearcOrderVendorID=" + $scope.SearcOrderVendorID + "&SearchOrderVendorName=" + $scope.SearchOrderVendorName + "&SearchOrderQuoteType=" + $scope.SearchOrderQuoteType + "&SearchOrderQuoteNo=" + $scope.SearchOrderQuoteNo + "&SearchOrderProductGroup=" + $scope.SearchOrderProductGroup + "&SearchOrderEnqFor=" + $scope.SearchOrderEnqFor).success(function (response) {
            $scope.AvailableOrdersList = response.lstOrderEntity;
            $scope.orderTotalCount = response.totalcount;
        }, function (error) {
            alert('failed');
        });
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
                SetModalTitle("Add New Revised Quotation")
                SetModalBody(html);
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
                SetParamModalPanelBody('ClarificationPanelBody',html);
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
                SetModalTitle("Material Entry")
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
    
    //Vendor Master List Starts
    $scope.INBTotalCount = 0;
    $scope.INBPageIndex = 1;
    $scope.INBPageSize = "50";
    $scope.SearchType = "";
    $scope.SearchVendorNature = "";
    $scope.SearchVendorName = "";
    $scope.SearchBillNo = "";
    $scope.SearchBillDate = "";
    $scope.SearchItemDescription = "";
    $scope.SearchCurrency = "";
    $scope.SearchApprovalStatus = "";

    //$scope.FetchVendorsMasterList = function () {
    //    $http.get(window.FetchInboundList+"?pageindex=" + $scope.INBPageIndex + "&pageSize=" + $scope.INBPageSize + "&SearchType=" + $scope.SearchType + "&SearchVendorNature=" + $scope.SearchVendorNature + "&SearchVendorName=" + $scope.SearchVendorName + "&SearchBillNo=" + $scope.SearchBillNo + "&SearchBillDate=" + $scope.SearchBillDate + "&SearchItemDescription=" + $scope.SearchItemDescription + "&SearchCurrency=" + $scope.SearchCurrency + "&SearchApprovalStatus=" + $scope.SearchApprovalStatus).success(function (response) {
    //        $scope.InboundList = response.lstVBM;
    //        $scope.INBTotalCount = response.totalcount;
    //    }, function (error) {
    //        alert('failed');
    //    });
    //}

    //$scope.FetchVendorsMasterList();

    $scope.INBPageChanged = function () {
        $scope.FetchVendorsMasterList();
    }

    $scope.INBChangePageSize = function () {
        $scope.INBPageIndex = 1;
        $scope.FetchVendorsMasterList();
    }




});

//EOF