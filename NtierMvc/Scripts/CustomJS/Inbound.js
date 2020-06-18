
function GetDetailForGateEntry() {
    var POSetno = $('#GEVendorPONO').val();

    $.ajax({
        url: window.GetDetailsForGateEntry,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ POSetno: POSetno }),
        success: function (data) {

            if (data.length > 0) {
                $("#HiddenPOSetno").val(data[0].POSetno);
                $('#RadioList' + data[0].PRCat).prop("checked", true);
                $('.RadioList').attr("disabled", true);
                $('#tableSelected').val(data[0].PRCat);

                switch (data[0].PRCat) {
                    case 'RM':
                        $('#tableRM').show();
                        $('#tableRM tbody').empty();

                        $.each(data, function (i, item) {
                            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.PRqty + '<span></td><td><span>' + item.UOM + '<span></td><td><span>' + item.UnitPrice + '<span></td><td><span>' + item.Discount + '%</span></td><td><span>' + item.TotalPrice + '</span></td></tr>');

                        })

                        $('#GEPODate').val(data[0].PODate);
                        $('#GEPODate').val(data[0].POdate);
                        $('#GEPOValidity').val(data[0].POValidity);
                        $('#GEWorkNo').val(data[0].WorkNo);
                        $('#GEDeliveryDate').val(data[0].DeliveryDate);
                        $('#GEPOValidity').val(data[0].POValidity);
                        $('#GEPORevNo').val(data[0].PORevNo);
                        $('#GEItemCategory').val(data[0].ItemCategory);
                        $('#GEModeOfTransport').val(data[0].ModeOfTransport);
                        $('#GEGateControlNo').val(data[0].GateControlNo);

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