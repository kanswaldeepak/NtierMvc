function SetQuoteNoView() {
    $('#QuoteNoViewOrder').val($('#QuoteNoOrder option:selected').text());
}

function RQDShowHideStatus() {
    var InspectionValue = $('#RQDInspection option:selected').text();

    if (InspectionValue == 'In-House')
        $('#RQDdivStatus').hide();
    else
        $('#RQDdivStatus').show();
}


function GetQuoteNoForQuoteType() {
    let QuoteType = $('#QuoteType').val();

    $.ajax({
        type: 'POST',
        url: window.GetPrepQuoteNo,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#QuoteNo").empty();
            if (res.length > 0) {
                $.each(res, function (i, item) {
                    $("#QuoteNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}


function GetVendorAndEnquiryDetails() {
    var VendorId = $("#QRCustomerId").val();
    $.ajax({
        type: 'POST',
        url: window.VendorDetail,
        data: JSON.stringify({ vendorId: VendorId }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#QRCustomerName").val(res.quoteEntity.CustomerName);
            $("#Country").val(res.quoteEntity.Country);
            $("#CountryId").val(res.quoteEntity.CountryId);
            $("#GeoArea").val(res.quoteEntity.GeoArea);
            $("#GeoCode").val(res.quoteEntity.GeoCode);
            $("#EnqNo").empty();
            if (res.lstDdEntity.length > 0) {
                $.each(res.lstDdEntity, function (i, item) {
                    $("#EnqNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
            GetEnquiryDetails();
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function GetEnquiryDetails() {
    var EnqNo = $("#EnqNo").val();
    $.ajax({
        type: 'POST',
        url: window.GetEnquiryDetailsForQuote,
        data: JSON.stringify({ enquiryId: EnqNo }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            if (res.DataValueField1.length > 0) {
                $('#QuoteEnqDt').val(res.DataValueField1.split(' ')[0]);
                $('#QuoteEnqFor').val(res.DataValueField2);
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please contact support.' + e);
        }
    })
}

function RQDGetQuoteNumbers() {
    let QuoteType = $("#RQDQuoteFormType").val();
    let finYear = $("#RQDFinancialYear").val();

    $.ajax({
        type: 'POST',
        url: window.GetRevAndOriginalQuotes,
        data: JSON.stringify({ quotetypeId: QuoteType, financialYr: finYear }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#RQDQuoteNo").empty();
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#RQDQuoteNo").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}

function RQDChangeTransport() {

    var QuoteType = $('#RQDQuoteFormType option:selected').text();

    if (QuoteType == 'Domestic') {
        $("#RQDModeOfDespatch").find("option:contains(Road)").show();
        $("#RQDModeOfDespatch").find("option:contains(Hand)").show();
        $("#RQDModeOfDespatch").find("option:contains(Sea)").hide();

        $('#RQDdivPortOfDischarge').hide();
    }
    else {
        $("#RQDModeOfDespatch").find("option:contains(Road)").hide();
        $("#RQDModeOfDespatch").find("option:contains(Hand)").hide();
        $("#RQDModeOfDespatch").find("option:contains(Sea)").show();

        $('#RQDdivPortOfDischarge').show();
    }
}


function GetCustomerDetails() {
    var CustomerId = $("#CustomerIdRevised").val();
    $.ajax({
        type: 'POST',
        url: window.CustomerDetail,
        data: JSON.stringify({ vendorId: CustomerId }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#CustomerNameRevised").val(res.quoteEntity.CustomerName);
            $("#Country").val(res.quoteEntity.Country);
            $("#CountryId").val(res.quoteEntity.CountryId);
        },
        error: function (x, e) {
            $('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            $('#spn-Sucess-Failure').addClass("important red");
            $('#Sucess-Failure').modal('show');
        }
    })
}

function GetRevisedQuoteNos() {
    var QuoteTypeId = $("#QuoteTypeRevised").val();
    var QuoteNo = $("#RevisedFormQuoteNo").val();
    $.ajax({
        type: 'POST',
        url: window.GetRevisedQuoteNo,
        data: JSON.stringify({ quoteTypeId: QuoteTypeId, quoteNumberId: QuoteNo }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            $("#RevisedQuoteNo").val(res.DataValueField1);
            $("#QuoteDateRevised").val(res.DataValueField2);
            $("#QuoteValidityRevised").val(res.DataValueField4);
        },
        error: function (x, e) {
            $("#RevisedQuoteNo").val('');
        }
    })
}

function RQGetQuoteNoDetails() {
    let QuoteType = $("#RQDQuoteFormType").val();
    let finYear = $("#RQDFinancialYear").val();
    let QuoteNo = $("#RQDQuoteNo option:selected").text();

    $.ajax({
        type: 'POST',
        url: window.GetQuoteNoDetailsforRevisedQuote,
        data: JSON.stringify({ quoteNoId: QuoteNo, quotetypeId: QuoteType, financialYr: finYear }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(".clearFields").val('');
            $("#RQDUserInitial").val(data.UserInitial);
            //$("#RQDFinancialYear").val(data.FinancialYear);
            //$("#RQDQuoteFormType").val(data.QuoteType);
            //$("#RQDQuoteNo").val(data.QuoteNo);
            //$("#RQDRevisedQuoteNo").val(data.RevisedQuoteNo);
            $("#RQDQuoteNoView").val(data.QuoteNoView);
            $("#RQDQuoteDate").val(data.QuoteDate);
            $("#RQDCustomerId").val(data.CustomerId);
            $("#RQDCustomerName").val(data.CustomerName);
            $("#RQDCountry").val(data.Country);
            $("#RQDCountryId").val(data.CountryId);
            $("#RQDGeoArea").val(data.GeoArea);
            $("#RQDFileNo").val(data.FileNo);
            $("#RQDEnqNo").val(data.EnqNo);
            $("#RQDQuoteEnqDt").val(data.EnqDt);
            $("#RQDQuoteEnqFor").val(data.EnqFor);
            $("#RQDQuoteSentOn").val(data.QuoteSentOn);
            $("#RQDQuoteValidity").val(data.QuoteValidity);
            $("#RQDInspection").val(data.Inspection);
            $("#RQDStatus").val(data.Status);
            $("#RQDQuoteSupplyTerms").val(data.SupplyTerms);
            $("#RQDLeadTime").val(data.LeadTime);
            $("#RQDLeadTimeDuration").val(data.LeadTimeDuration);
            $("#RQDModeOfDespatch").val(data.ModeOfDespatch);
            $("#RQDDeliveryTerms").val(data.DeliveryTerms);
            $("#RQDPortOfDischarge").val(data.PortOfDischarge);
            $("#RQDQuoteFormCurrency").val(data.Currency);
            $("#RQDPaymentTerms").val(data.PaymentTerms);
            $("#RQDSalesPerson").val(data.SalesPerson);
            $("#RQDSubject").val(data.Subject);
            $("#RQDRemarks").val(data.Remarks);
            $("#RQDEnqRef").val(data.EnqRef);
            $("#RGDGeoCode").val(data.GeoCode);

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
}
