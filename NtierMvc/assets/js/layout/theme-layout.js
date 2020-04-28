$(document).ready(function(){

	/* Tooltip */
	$('[data-toggle="tooltip"]').tooltip();
	
	/* Menu */
	
	$('.mk-sb-has-submenu').on('click', function () {
		$('.mk-sb-has-submenu').toggleClass('active');
	});
	
	$('.mk-menu-toggle').on('click', function () {
		$('body').toggleClass('mk-sb-minimized');
	});

	/* Horizontal Menu Toggle*/
	$('.mk-hm-has-submenu').on('click', function () {
		//$('.mk-hm-has-submenu').removeClass('active');
		//$(this).toggleClass('active');
		
	});
	
	
	/* Mobile view menu toogle */
	$('.mk-menu-toggle-mobile').on('click', function () {
		$('.mk-aside-left').css("left", "0px");
	});
	$('.mk-menu-close-mobile').on('click', function () {
		$('.mk-aside-left').css("left", "-255px");
	});
	
});