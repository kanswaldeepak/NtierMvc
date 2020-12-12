

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
            $('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            $('#spn-Sucess-Failure').addClass("important red");
            $('#Sucess-Failure').modal('show');
        }
    })
}