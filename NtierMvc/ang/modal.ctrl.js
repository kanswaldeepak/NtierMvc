angular.module('App').controller("ModalController", function ($scope, $http, $timeout, $compile) {
    $scope.sdata1 = {};

    //Customer Modal Starts
    $scope.SaveCustomer = function () {
        var frm = $("#formSaveCustomerDetail");
        var formData = new FormData(frm[0]);

        var Tel1 = $('tel1').length;
        var Tel2 = $('tel2').length;

        var Status = false;
        Status = GetFormValidationStatus("#formSaveCustomerDetail");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else if (Tel1 > 0 && Tel1 < 10)
            alert("Telephone 1 should be more than 10 and less than 25");
        else if (Tel2 > 0 && Tel2 < 10)
            alert("Telephone 2 should be more than 10 and less than 25");
        else {
            $http({ url: window.SaveCustomer, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        $scope.FetchCustomerList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });

        }
        
    }

    $scope.IsValid = function () {
        return true;
    }

    //Enquiry Modal Starts
    $scope.SaveEnquiry = function () {
        var frm = $("#formSaveEnquiryDetail");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveEnquiryDetail");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveEnquiry, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        $scope.FetchEnquiryList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }
    }

    //Quotation Modal Starts
    $scope.SaveQuotation = function () {
        var frm = $("#formSaveQuotationDetail");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveQuotationDetail");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
            //alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveQuotation, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        $scope.FetchQuotationList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }
        
    }

    //QuotePreparation Modal Starts
    $scope.SaveQuotePreparation = function () {
        if (!confirm("Are you sure you want to Save?")) {
            return;
        }

        var frm = $("#formSaveQuotationPrepDetail");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveQuotationPrepDetail");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");            
        }
        else {
            $http({
                url: window.SaveQuotePreparation, method: 'POST', data: formData, headers: { 'Content-Type': undefined }
            }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                        ClearAllFields("#formSaveQuotationPrepDetail");
                        $('#divProductDetails').hide();
                        $('.ShowHideFields').hide();
                        
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }
        
    }

    //Order Modal Starts
    $scope.SaveOrderDetails = function () {

        var frm = $("#formSaveOrderDetails");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveOrderDetails");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveOrder, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        $scope.FetchOrdersList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }

    }

    //Item Modal Starts
    $scope.SaveItemDetails = function () {
        var frm = $("#formSaveItemDetails");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveItemDetails");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveItemDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                        //$scope.FetchQuotationList();
                        //$("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }

    }

    //Quotation Modal Starts
    $scope.SaveRevisedQuotation = function () {
        var frm = $("#formSaveRevisedQuotationDetail");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveRevisedQuotationDetail");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
            //alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveRevisedQuotation, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        $scope.FetchQuotationList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });
        }

    }

    //Vendor Master Bill Starts
    $scope.SaveInboundGateEntry = function () {
        var frm = $("#formInbound");
        var formData = new FormData(frm[0]);

        //var BillAmount = $('#BillAmount').val();
        //var EndUse = $('#EndUse option:selected').text();
        //if (BillAmount > 1000 && EndUse == 'Non-PO') {
        //    alert('Value exceeds the sanctioned value. Please raise PO');
        //    return;
        //}

        var Status = false;
        Status = GetFormValidationStatus("#formInbound");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveGateEntryDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                        $scope.FetchInboundList();
                        $("#ModalPopup").modal('hide');
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });

        }

    }

    $scope.SaveNewDescDetails = function () {
        var frm = $("#formNewSaveDescDetails");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formNewSaveDescDetails");

        var ProductNo = $('#DescProductNo').val();
        var MainPL = $('#DescMainPL').val();

        if (MainPL != ProductNo.slice(0,1)) {
            alert("Product No is not valid");
        }
        else if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveNewDescDetail, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                        $scope.PosNo = 1;
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });

        }

    }


    $scope.SaveRevisedOrderDetail = function () {
        var frm = $("#formSaveRevisedOrderDetails");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveRevisedOrderDetails");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveRevisedOrderDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });

        }

    }

    $scope.SaveCRListings = function () {
        var frm = $("#formSaveContractReview");
        var formData = new FormData(frm[0]);

        var Status = false;
        Status = GetFormValidationStatus("#formSaveContractReview");

        if (!Status) {
            alert("Kindly Fill all mandatory fields");
        }
        else {
            $http({ url: window.SaveContractReviewDetails, method: 'POST', data: formData, headers: { 'Content-Type': undefined } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });

        }

    }

    $scope.SavePLDetail = function (Type) {

        if ($scope.PLDMainPLText == '') {
            alert("Kindly Fill Main PL");
        }
        else {
            $http({ url: window.SavePLDetails, method: 'POST', data: JSON.stringify({ type: Type, mainPLName: $scope.PLDMainPLNameText, mainPLNo: $scope.PLDMainPLNoText, mainPLDdl: $scope.PLDMainPLDdl, subPLtext: $scope.PLDSubPLText }), headers: { 'Content-Type': "application/json; charset=utf-8" } }).success(
                function (res) {
                    if (res == 'Saved Successfully!') {
                        alert(res);
                    }
                    else {
                        alert(res)
                    }
                }
            ).error(function (res) { showHttpErr(res); });

        }

    }

});

//EOF