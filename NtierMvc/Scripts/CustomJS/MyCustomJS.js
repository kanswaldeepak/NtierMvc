function show_ajax_loader(msg) {
    msg = msg ? msg : 'Loading';
    var el = '<div class="bodycover"></div><div class="ajaxloader"><i class="fa fa-spin fa-spinner"></i> ' + msg + '</div>';
    $(".bodycover,.ajaxloader").remove();
    $("body").prepend(el);
}

function hide_ajax_loader() {
    $(".bodycover,.ajaxloader").remove();
}