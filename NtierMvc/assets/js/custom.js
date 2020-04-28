$(document).ready(function(){
	
	//$(".mk-aside-left").niceScroll({cursorborder:"",background:"rgba(20,20,20,0.3)",cursorcolor:"rgb(131, 131, 131)", touchbehavior:true });
	
	$('[data-toggle="tooltip"]').tooltip();
	
	$('.datepicker').datepicker({
		startDate: '-3d'
	});
	
	//Counter
	$('.counter').each(function() {
	  var $this = $(this),
		  countTo = $this.attr('data-count');
	  
	  $({ countNum: $this.text()}).animate({
		countNum: countTo
	  },

	  {

		duration: 5000,
		easing:'linear',
		step: function() {
		  $this.text(Math.floor(this.countNum));
		},
		complete: function() {
		  $this.text(this.countNum);
		  //alert('finished');
		}

	  });  
	  
	  

	});
	
	
});