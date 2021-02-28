(function($) {
"use strict";

/*------------------------------------------------------------------
[Table of contents]

XpeedStudio custom function
prleoader
isotope grid for 3 column
isotope grid for 4 column for portfolio
isotope grid for 1 column for faq section
mega menu init
number counter and skill bar animation
welcome skill bar
cause matters skill bar
welcome number percentages
skill bar and number counter
countdown timer
back to top init
owl testimonial slider
owl single content slider
owl sync slider init
video popup init
image popup init
parallax background init
map window opener add class
contact form init
easy pie chart init
input number increase
insta feed
wow animation init
show last position back to top init
fixed class add scroll and corrent section
XpeedStudio multipile Maps
XpeedStudio Maps

-------------------------------------------------------------------*/

/*==========================================================
			XpeedStudio custom function
=======================================================================*/

function xs_custom_function(argument) {
	
	var w_height =	$(window),
		xs_welcome = $('.xs-screen-height .xs-welcome-content'),
		xs_welcome_wraper = $('.xs-welcome-wraper'),
		h_height = $('.xs-header-height'),
		xs_footer = $('.xs-fixed-footer'),
		footer_height = xs_footer.height(),
		xs_main_content_wraper = $('.xs-all-content-wrapper'),
		xs_main_content_height = xs_main_content_wraper.height(),
		sync_slider_img = $('.xs-sync-slider-preview-content img'),
		sync_slider_iframe = $('.xs-sync-slider-preview-content iframe');

		// console.log(sync_slider_iframe);

		if (sync_slider_iframe.length > 0) {
			sync_slider_iframe.css('height', sync_slider_img.outerHeight());
		}

		xs_footer.css({
			width: '100%',
			left: '0',
			position: 'fixed',
			zIndex: '1',
		});

		xs_main_content_wraper.css({
			marginBottom: footer_height,
			zIndex: '100',
			position: 'relative',
			backgroundColor: '#fff',
		});

		xs_welcome_wraper.css('marginTop', h_height.height()+'px');

		if (w_height.height() >= 750 && w_height.width() <= 991 && w_height.width() >= 1400) {
			xs_welcome.css('height', w_height.height());
		} else {
			xs_welcome.css('height', '700'+'px');
		}

		/*=============================================================
							parallax title perfect center
		=========================================================================*/ 

		$('.parallax-title').each(function(index, el) {
			var parallax_title_width = $(this).innerWidth();
			$(this).css('marginLeft', -(parallax_title_width / 2)+'px');
		});

}




//  email patern 
function email_pattern(email) {
	var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;

	return regex.test(email);
}

//  text parallax init 
function initparallax() {
	var a = {
		Android: function() {
			return navigator.userAgent.match(/Android/i);
		},
		BlackBerry: function() {
			return navigator.userAgent.match(/BlackBerry/i);
		},
		iOS: function() {
			return navigator.userAgent.match(/iPhone|iPad|iPod/i);
		},
		Opera: function() {
			return navigator.userAgent.match(/Opera Mini/i);
		},
		Windows: function() {
			return navigator.userAgent.match(/IEMobile/i);
		},
		any: function() {
			return a.Android() || a.BlackBerry() || a.iOS() || a.Opera() || a.Windows();
		}
	};	
		var trueMobile = a.any();
		if (null == trueMobile) {
			var b = new Scrollax();
			b.reload();
			b.init();
	}
}

/*==========================================================
			easy pie chart init
======================================================================*/

function pie_chart_init() {
	var chart = $(".xs-pie-chart");
	if(chart.length > 0){
		chart.each(function () {
			$(this).easyPieChart({
				easing: 'easeOutBounce',
				barColor:'#4CC899',
				trackColor: '#E5E5E5',
				lineWidth: 6,
				scaleColor: 'transparent',
				size: 120,
				lineCap: 'round',
				duration: 4500, 
				enabled: true,
				onStep: function(from, to, percent) {
					$(this.el).find('.xs-pie-chart-percent').text(Math.round(percent));
				}
			});
		});
	} // End exists

	var chart = $(".xs-pie-chart-v2");
	if(chart.length > 0){
		chart.each(function () {
			$(this).easyPieChart({
				easing: 'easeOutBounce',
				barColor:'#1B70F0',
				trackColor: '#F4F4F4',
				lineWidth: 6,
				scaleColor: 'transparent',
				size: 80,
				lineCap: 'round',
				duration: 5000, 
				enabled: true,
				onStep: function(from, to, percent) {
					$(this.el).find('.xs-pie-chart-percent').text(Math.round(percent));
				}
			});
		});
	} // End exists

	var chart = $(".xs-pie-chart-v3");
	if(chart.length > 0){
		chart.each(function () {
			$(this).easyPieChart({
				easing: 'easeOutBounce',
				barColor:'#011b58',
				trackColor: '#F4F4F4',
				lineWidth: 12,
				scaleColor: 'transparent',
				size: 120,
				lineCap: 'square',
				duration: 5000, 
				enabled: true,
				onStep: function(from, to, percent) {
					$(this.el).find('.xs-pie-chart-percent').text(Math.round(percent));
				}
			});
		});
	} // End exists
}

$(window).on('load', function() {

	// xs custom function
	xs_custom_function();

	// init text parallax
	initparallax();

	// prleoader
	setTimeout(function() {
		$("#preloader").fadeOut();
		$('.fundpress-animate').addClass('load');
		pie_chart_init();
	}, 500);


	/*==========================================================
			isotope grid for 3 column
	=======================================================================*/

	if ($('.xs-col-3-isotope-grid').length > 0) {
		var $container = $('.xs-col-3-isotope-grid'),
		colWidth = function() {
			var w = $container.width(),
				columnNum = 1,
				columnWidth = 0;
			if (w > 1200) {
				columnNum = 3;
			} else if (w > 900) {
				columnNum = 3;
			} else if (w > 600) {
				columnNum = 2;
			} else if (w > 450) {
				columnNum = 2;
			} else if (w > 385) {
				columnNum = 1;
			}
			columnWidth = Math.floor(w / columnNum);
			$container.find('.xs-col-3-isotope-grid-item').each(function() {
				var $item = $(this),
					multiplier_w = $item.attr('class').match(/xs-col-3-isotope-grid-item-w(\d)/),
					multiplier_h = $item.attr('class').match(/xs-col-3-isotope-grid-item-h(\d)/),
					width = multiplier_w ? columnWidth * multiplier_w[1] : columnWidth,
					height = multiplier_h ? columnWidth * multiplier_h[1] * 0.4 - 12 : columnWidth * 0.5;
				$item.css({
					width: width
					//height: height
				});
			});
			return columnWidth;
		},
		isotope = function() {
			$container.isotope({
				resizable: false,
				itemSelector: '.xs-col-3-isotope-grid-item',
				masonry: {
					columnWidth: colWidth(),
					gutterWidth: 3
				}
			});
		};
		isotope();
		$(window).on('resize', isotope);
		var $optionSets = $('.xs-isotope-nav .option-set'),
			$optionLinks = $optionSets.find('a');
		$optionLinks.on('click', function() {
			var $this = $(this);
			var $optionSet = $this.parents('.option-set');
			$optionSet.find('.selected').removeClass('selected');
			$this.addClass('selected');

			// make option object dynamically, i.e. { filter: '.my-filter-class' }
			var options = {},
				key = $optionSet.attr('data-option-key'),
				value = $this.attr('data-option-value');
			// parse 'false' as false boolean
			value = value === 'false' ? false : value;
			options[key] = value;
			if (key === 'layoutMode' && typeof changeLayoutMode === 'function') {
				// changes in layout modes need extra logic
				changeLayoutMode($this, options)
			} else {
				// creativewise, apply new options
				$container.isotope(options);
			}
			return false;
		});
	}

	/*==========================================================
			isotope grid for 4 column for portfolio
	=======================================================================*/

	if ($('.xs-portfolio-isotope-grid').length > 0) {
		var $container = $('.xs-portfolio-isotope-grid'),
		colWidth = function() {
			var w = $container.width(),
				columnNum = 1,
				columnWidth = 0;
			if (w > 1200) {
				columnNum = 3;
			} else if (w > 900) {
				columnNum = 3;
			} else if (w > 600) {
				columnNum = 1;
			} else if (w > 450) {
				columnNum = 1;
			} else if (w > 385) {
				columnNum = 1;
			}
			columnWidth = Math.floor(w / columnNum);
			$container.find('.xs-portfolio-isotope-grid-item').each(function() {
				var $item = $(this),
					multiplier_w = $item.attr('class').match(/xs-portfolio-isotope-grid-item-w(\d)/),
					multiplier_h = $item.attr('class').match(/xs-portfolio-isotope-grid-item-h(\d)/),
					width = multiplier_w ? columnWidth * multiplier_w[1] : columnWidth,
					height = multiplier_h ? columnWidth * multiplier_h[1] * 0.4 - 12 : columnWidth * 0.5;
				$item.css({
					width: width
					//height: height
				});
			});
			return columnWidth;
		},
		isotope = function() {
			$container.isotope({
				resizable: false,
				itemSelector: '.xs-portfolio-isotope-grid-item',
				masonry: {
					columnWidth: colWidth(),
					gutterWidth: 3
				}
			});
		};
		isotope();
		$(window).on('resize', isotope);
	}

	/*==========================================================
			isotope grid for 1 column for faq section
	=======================================================================*/

	if($('.xs-col-1-isotope-grid').length > 0) {

		var $container = $('.xs-col-1-isotope-grid'),
			$filterLinks = $('#filters li a');

		$container.isotope({
			itemSelector: '.xs-col-1-isotope-grid-item',
			filter: '.art'
		});

		$filterLinks.on('click', function(e){
			e.preventDefault();
			var $this = $(this);

		// don't proceed if already selected
		if ( $this.hasClass('selected') ) {
			return;
		}

		$filterLinks.filter('.selected').removeClass('selected');
		$this.addClass('selected');

		// get selector from data-filter attribute
		var selector = $this.data('filter');

		$container.isotope({
			filter: selector
		});


		});


		var $clubxgallerycontainer = $('.xs-col-1-isotope-grid'),
		colWidth = function () {
			var w = $clubxgallerycontainer.width(), 
				columnNum = 1,
				columnWidth = 0;
			if (w > 1200) {
				columnNum  = 1;
			} else if (w > 900) {
				columnNum  = 1;
			} else if (w > 600) {
				columnNum  = 1;
			} else if (w > 450) {
				columnNum  = 1;
			} else if (w > 385) {
				columnNum  = 1;
			}
			columnWidth = Math.floor(w/columnNum);
			$clubxgallerycontainer.find('.xs-col-1-isotope-grid-item').each(function() {
				var $item = $(this),
				multiplier_w = $item.attr('class').match(/xs-col-1-isotope-grid-item-w(\d)/),
				multiplier_h = $item.attr('class').match(/xs-col-1-isotope-grid-item-h(\d)/),
				width = multiplier_w ? columnWidth*multiplier_w[1] : columnWidth,
				height = multiplier_h ? columnWidth*multiplier_h[1]*0.4-12 : columnWidth*0.5;
				$item.css({
				width: width
				//height: height
			});
		});
		return columnWidth;
		},
		isotope = function () {
				$clubxgallerycontainer.isotope({
					resizable: false,
					itemSelector: '.xs-col-1-isotope-grid-item',
					masonry: {
					columnWidth: colWidth(),
					gutterWidth: 3
				}
			});
		};
		isotope();
			$(window).on('resize', isotope);
	} // end clubx player list grid

}); // end on.load event

$(document).ready(function() {

	// xs custom function
	xs_custom_function();

	// init text parallax
	initparallax();


	/*==========================================================
			mega menu init
	=======================================================================*/

	if ($('.xs-menus').length > 0) {
		$('.xs-menus').xs_nav({
			mobileBreakpoint: 992,
		});
	}

	/*==========================================================
			number counter and skill bar animation
	=======================================================================*/

	var number_percentage = $(".number-percentage");
	function animateProgressBar(){
		number_percentage.each(function() {
		$(this).animateNumbers($(this).attr("data-value"), true, parseInt($(this).attr("data-animation-duration"), 10));
				var value = $(this).attr("data-value");
				var duration = $(this).attr("data-animation-duration");
		$(this).closest('.xs-skill-bar').find('.xs-skill-track').animate({
			width: value + '%'
			}, 4500);
		});
	}


	if ($('.waypoint-tigger').length > 0) {
		var waypoint = new Waypoint({
			element: document.getElementsByClassName('waypoint-tigger'),
			handler: function(direction) {
				animateProgressBar();
			}
		});
	}

	/*==========================================================
			welcome skill bar
	=======================================================================*/
	if ($('.xs-skill-bar-v2').length > 0) {
		$('.xs-skill-bar-v2').each(function(){
			$(this).find('.xs-skill-track').animate({
				width:$(this).attr('data-percent')
			},4500);
		});
	}

	/*==========================================================
			cause matters skill bar
	=======================================================================*/
	if ($('.xs-skill-bar-v3').length > 0) {
		$('.xs-skill-bar-v3').each(function(){
			$(this).find('.xs-skill-track').css({
				width:$(this).attr('data-percent'),
			});
		});
	}
	/*==========================================================
			welcome number percentages
	=======================================================================*/
	if ($('.number-percentages').length > 0) {
		$('.number-percentages').each(function () {
			var $this = $(this);

			$({ Counter: 0 }).animate({ Counter: $this.text() }, {
				duration: 4500,
				easing: 'swing',
				step: function () {
					$this.text(Math.ceil(this.Counter));
				}
			});
		});
	}

	/*==========================================================
			skill bar and number counter
	=======================================================================*/

	$.fn.animateNumbers = function(stop, commas, duration, ease) {
		return this.each(function() {
			var $this = $(this);
			var start = parseInt($this.text().replace(/,/g, ""), 10);
			commas = (commas === undefined) ? true : commas;
			$({
				value: start
			}).animate({
				value: stop
			}, {
				duration: duration == undefined ? 500 : duration,
				easing: ease == undefined ? "swing" : ease,
				step: function() {
					$this.text(Math.floor(this.value));
					if (commas) {
						$this.text($this.text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
					}
				},
				complete: function() {
					if (parseInt($this.text(), 10) !== stop) {
						$this.text(stop);
						if (commas) {
							$this.text($this.text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
						}
					}
				}
			});
		});
	};


	/*==========================================================
			countdown timer
	======================================================================*/

	$('.xs-countdown-timer[data-countdown]').each(function() {
		var $this = $(this), 
			finalDate = $(this).data('countdown');

			$this.countdown(finalDate, function(event) {
			var $this = $(this).html(event.strftime(' ' 
			+ '<span class="timer-count">%-D <span class="timer-text">Dias</span></span>  ' 
			+ '<span class="timer-count">%H <span class="timer-text">Horas</span></span> ' 
			+ '<span class="timer-count">%M <span class="timer-text">Minutos</span></span> ' 
			+ '<span class="timer-count">%S <span class="timer-text">Segundos</span></span>'));
		});
	});

	$('.xs-countdown-timer-v2[data-countdown]').each(function() {
		var $this = $(this), 
			finalDate = $(this).data('countdown');

			$this.countdown(finalDate, function(event) {
			var $this = $(this).html(event.strftime(' ' 
			+ '<div class="xs-timer-container"><span class="timer-count">%-D </span><span class="timer-text">Dias</span></div>'
			+ '<div class="xs-timer-container"><span class="timer-count">%H </span><span class="timer-text">Horas</span></div>' 
			+ '<div class="xs-timer-container"><span class="timer-count">%M </span><span class="timer-text">Minutos</span></div>' 
			+ '<div class="xs-timer-container"><span class="timer-count">%S </span><span class="timer-text">Segundos</span></div>'));
		});
	});

	/*==========================================================
			back to top init
	======================================================================*/
	$(document).on('click', '.xs-back-to-top', function(event) {
		event.preventDefault();
		/* Act on the event */

		$('html, body').animate({
			scrollTop: 0,
		}, 1000)
	});

	/*==========================================================
			owl testimonial slider
	======================================================================*/
	if ($('.xs-testimonial-slider.slider-double-item').length > 0) {
		var owl1 = $(".xs-testimonial-slider.slider-double-item");
		owl1.owlCarousel({
			items: 2,
			loop: true,
			mouseDrag: true,
			touchDrag: true,
			dots: true,
			nav: false,
			autoplay: true,
			autoplayTimeout: 5000,
			autoplayHoverPause: true,
			responsive : {
				// breakpoint from 0 up
				0 : {
					items: 1,
				},
				// breakpoint from 480 up
				480 : {
					items: 1,
				},
				// breakpoint from 768 up
				768 : {
					items: 2,
				}
			}
		});
	}

	/*==========================================================
			owl single content slider
	======================================================================*/
	if ($('.xs-single-content-slider').length > 0) {
		var owl2 = $(".xs-single-content-slider");
		owl2.owlCarousel({
			items: 1,
			loop: true,
			mouseDrag: true,
			touchDrag: true,
			dots: false,
			nav: true,
			autoplay: true,
			navText: ["<i class='fa fa-angle-left xs-owl-round-nav'></i>","<i class='fa fa-angle-right xs-owl-round-nav'></i>"],
			autoplayTimeout: 5000,
			autoplayHoverPause: true,
		});
	}

	/*==========================================================
			owl single content slider
	======================================================================*/
	if ($('.xs-banner-slider').length > 0) {
		var owl3 = $(".xs-banner-slider");
		owl3.owlCarousel({
			items: 1,
			loop: true,
			mouseDrag: true,
			touchDrag: true,
			dots: false,
			nav: true,
			autoplay: true,
			navText: ["<i class='fa fa-angle-left xs-owl-round-nav v2'></i>","<i class='fa fa-angle-right xs-owl-round-nav v2'></i>"],
			autoplayTimeout:5000,
			autoplaySpeed: true,
			autoplayHoverPause: true,
			responsive : {
				// breakpoint from 0 up
				0 : {
					nav: false,
				},
				// breakpoint from 480 up
				480 : {
					nav: false,
				},
				// breakpoint from 768 up
				768 : {
					nav: true,
				}
			}
		});
	}

	/*==========================================================
			owl sync slider init
	======================================================================*/

	if (($('.xs-sync-slider-preview').length > 0) && ($('.xs-sync-slider-thumb').length > 0)) {

		var sync1 = $(".xs-sync-slider-preview"),
			sync2 = $(".xs-sync-slider-thumb"),
			slidesPerPage = 4,
			syncedSecondary = true;

		sync1.owlCarousel({
			items : 1,
			slideSpeed : 2000,
			nav: false,
			autoplay: true,
			autoplayHoverPause: true,
			dots: false,
			loop: true,
			responsiveRefreshRate : 200,
			navText: [''],
		}).on('changed.owl.carousel', syncPosition);

		sync2
		.on('initialized.owl.carousel', function () {
			sync2.find(".owl-item").eq(0).addClass("current");
		})
		.owlCarousel({
			items : slidesPerPage,
			dots: false,
			nav: false,
			smartSpeed: 200,
			slideSpeed : 500,
			slideBy: slidesPerPage, //alternatively you can slide by 1, this way the active slide will stick to the first item in the second carousel
			responsiveRefreshRate : 100
		}).on('changed.owl.carousel', syncPosition2);

		function syncPosition(el) {
			//if you set loop to false, you have to restore this next line
			//var current = el.item.index;

			//if you disable loop you have to comment this block
			var count = el.item.count-1;
			var current = Math.round(el.item.index - (el.item.count/2) - .5);

		if(current < 0) {
			current = count;
		}
		if(current > count) {
			current = 0;
		}

		//end block

		sync2
			.find(".owl-item")
			.removeClass("current")
			.eq(current)
			.addClass("current");
			var onscreen = sync2.find('.owl-item.active').length - 1;
			var start = sync2.find('.owl-item.active').first().index();
			var end = sync2.find('.owl-item.active').last().index();

			if (current > end) {
				sync2.data('owl.carousel').to(current, 100, true);
			}
			if (current < start) {
				sync2.data('owl.carousel').to(current - onscreen, 100, true);
			}
		}

			function syncPosition2(el) {
				if(syncedSecondary) {
				var number = el.item.index;
				sync1.data('owl.carousel').to(number, 100, true);
			}
		}

		sync2.on("click", ".owl-item", function(e){
			e.preventDefault();
			var number = $(this).index();
			sync1.data('owl.carousel').to(number, 300, true);
		});
	}
	
	/*==========================================================
			video popup init
	======================================================================*/
	if ($('.xs-video-popup').length > 0) {
		$('.xs-video-popup').magnificPopup({
			disableOn: 700,
			type: 'iframe',
			mainClass: 'mfp-fade',
			removalDelay: 160,
			preloader: false,

			fixedContentPos: false
		});
	}

	/*==========================================================
			image popup init
	======================================================================*/

	if ($('.xs-image-popup').length > 0) {
		$('.xs-image-popup').magnificPopup({
			type: 'image',
			removalDelay: 500, //delay removal by X to allow out-animation
			callbacks: {
				beforeOpen: function () {
					// just a hack that adds mfp-anim class to markup
					this.st.image.markup = this.st.image.markup.replace('mfp-figure', 'mfp-figure mfp-with-anim');
					this.st.mainClass = 'mfp-zoom-in';
				}
			},
			closeOnContentClick: true,
			midClick: true,
			gallery: {
				enabled: true,
			},
		});
	}

	/*==========================================================
			parallax background init
	======================================================================*/
	$('.parallax-window').parallax();


	/*==========================================================
			map window opener add class
	======================================================================*/
	$(document).on('click', '.fundpress-window-opener', function() {
		// body...
		event.preventDefault();

		var main_wraper = $('.fundpress-widnow-wraper'),
			active_class= 'active';

		if ($(this).parent().parent().hasClass(active_class)) {
			$(this).parent().parent().removeClass(active_class);
		} else {
			$(this).parent().parent().addClass(active_class);
		}
	});


	/*==========================================================
			contact form init
	======================================================================*/

	$(document).on('submit', '#xs-contact-form', function(event) {
		event.preventDefault();
		/* Act on the event */

		var xs_contact_name = $('#xs_contact_name'),
			xs_contact_email = $('#xs_contact_email'),
			xs_contact_subject = $('#xs_contact_subject'),
			x_contact_massage = $('#x_contact_massage'),
			xs_contact_submit = $('#xs_contact_submit'),
			xs_contact_error = false;

		$('.xpeedStudio_success_message').remove();

		if (xs_contact_name.val() === '') {
			xs_contact_name.addClass('invaild');
			xs_contact_error = true;
			xs_contact_name.focus();
		} else {
			xs_contact_name.removeClass('invaild');
		}
		if (xs_contact_email.val() === '') {
			xs_contact_email.addClass('invaild');
			xs_contact_error = true;
			xs_contact_email.focus();
		} else if (!email_pattern(xs_contact_email.val().toLowerCase())){
			xs_contact_email.addClass('invaild');
			xs_contact_error = true;
			xs_contact_email.focus();
		} else {
			xs_contact_email.removeClass('invaild');
		}
		if (xs_contact_subject.val() === '') {
			xs_contact_subject.addClass('invaild');
			xs_contact_error = true;
			xs_contact_subject.focus();
		} else {
			xs_contact_subject.removeClass('invaild');
		}
		if (x_contact_massage.val() === '') {
			x_contact_massage.addClass('invaild');
			xs_contact_error = true;
			x_contact_massage.focus();
		} else {
			x_contact_massage.removeClass('invaild');
		}

		if (xs_contact_error === false) {
			xs_contact_submit.before().hide().fadeIn();
			$.ajax({
					type: "POST",
					url: "assets/php/contact-form.php",
					data: {
					'xs_contact_name' : xs_contact_name.val(),
					'xs_contact_email' : xs_contact_email.val(),
					'xs_contact_subject' : xs_contact_subject.val(),
					'x_contact_massage' : x_contact_massage.val(),
				},
				success: function(result){
					xs_contact_submit.after('<span class="xpeedStudio_success_message">' + result + '</span>').hide().fadeIn();

					// $(".xpeedStudio_loader").fadeOut("normal", function() {
					// 	$(this).remove();
					// });

					$('#xs-contact-form')[0].reset();
				}
			});
		}

	});


	/*=============================================================
					input number increase
	=========================================================================*/

	var thiss = $('.xs_input_number');

		thiss.append('<span class="sub"><img src="/Content/images/minus-icon.png" alt="" /></span>');
		thiss.append('<span class="add"><img src="/Content/images/plus-icon.png" alt="" /></span>');

	$('.xs_input_number').each(function() {
		
		var spinner = $(this),
			input = spinner.find('input[type="number"]'),
			add = spinner.find('.add'),
			sub = spinner.find('.sub');

			input.parent().on('click', '.sub', function(event) {
				
				event.preventDefault();
				/* Act on the event */
				if (input.val() > parseInt(input.attr('min')))
					input.val( function(i, oldval) { return --oldval; });

			});

			input.parent().on('click', '.add', function () {
				event.preventDefault();

				if (input.val() < parseInt(input.attr('max')))
					input.val( function(i, oldval) { return ++oldval; });
			});

	});

	/*=============================================================
					insta feed
	=========================================================================*/
	if ($('.xs-demoFeed').length > 0) {
		$.fn.spectragram.accessData = {
			accessToken: '1764162550.ba4c844.679392a432894982bf6a31ec20d8acb0',
			clientID: '289a98508b934dd49a68144b79209813'
		};
		$('.xs-demoFeed').spectragram('getUserFeed',{
			query: 'natgeo',
			max: 9,
			size: 'small',
		});
	}

	
	/*=============================================================
					wow animation init
	=========================================================================*/
	$(function(){
		var wow = new WOW({
			boxClass: 'wow',
			animateClass: 'animated',
			offset: 0,
			mobile: false,
			live: true,
			scrollContainer: null,
		});
		wow.init();
	});



	// campaign-form js
	if ($('#addMore-btn').length > 0) {
		$('#addMore-btn').on('click', function(event) {
			event.preventDefault();
			$(".xs-reward-input-filed:last").clone(true).insertBefore(this).addClass('clone');
			var lengt = $('.xs-reward-input-filed').length;
			if (lengt > 1) {
				$('.xs-reward-input-filed:first').addClass('clone');
			}
			$('.xs-reward-input-filed:last input, .xs-reward-input-filed:last textarea').each(function(){
				if ($(this).val().length > 0)
				  $(this).val('');
			  }); 
		});
	}

	if ($('#remove-btn').length > 0) {
		$('#remove-btn').on('click', function() {
			$(this).closest('.xs-reward-input-filed').remove();
			var lengt = $('.xs-reward-input-filed').length;
			if (lengt == '1'){
				$('.xs-reward-input-filed').removeClass('clone');
			}
		});
	}

	if ($('.custom-file input').length > 0) {
		$('.custom-file input').on('change', function(e) {
			var getValue = $(this).val(),
				fileName = getValue.replace(/C:\\fakepath\\/i, '');
			$(this).closest('.custom-file').find('.file-name').html(fileName);
		});
	}
	
	/*if ($('#customFile').length > 0) {
		$('#customFile').on('change', function(e) {
			var getValue = $(this).val(),
				fileName = getValue.replace(/C:\\fakepath\\/i, '');
			$(this).closest('.custom-file').find('.file-name').html(fileName);
		});
	}*/
	
	// dashboard form
	if ($('.formEdit').length > 0 && $('.formCancel').length > 0) {
		$('.formEdit').on('click', function(e) {
			e.preventDefault();
			$(this).parent().parent().parent().addClass('isActive');
		});
		$('.formCancel').on('click', function(e) {
			e.preventDefault();
			$(this).parent().parent().parent().removeClass('isActive');
		});
	}

	// campaign form
	if($('#campaign_form').length > 0) {
		$('#campaign_form').on('submit', function(e) {
			e.preventDefault();
			var error = false;
			$('#campaign_goal, #campaign_date, #campaign_end_date, #campaign_title').each(function(index){
				var input = $(this);
				if (input.val() === '') {
					input.addClass('error');
					error = true;
				} else {
					input.removeClass('error')
				}
			});
			$('#customCheck3').each(function(index) {
				var input = $(this);
				if ($(this).is(':checked')){
					input.removeClass('error');
				} else {
					input.addClass('error');
					error = true;
				}
			});
		});
	}
})


$(window).on('scroll', function() {
	
	/*==========================================================
			show last position back to top init
	======================================================================*/
	
	if ($('.show-last-pos').length > 0) {
		var w_height = $(window).height(),
			d_height = $(document).height(),
			height_calc = d_height - w_height,
			last_pos = $('.show-last-pos');

			if ($(this).scrollTop() >= height_calc) {
				last_pos.addClass('active');
			} else {
				last_pos.removeClass('active');
			}
	};

	/*==========================================================
			fixed class add scroll and corrent section
	======================================================================*/
	
	if ($('.xs-fixed-footer').length > 0) {
		var footer_content = $('.xs-fixed-footer'),
			xs_all_wrap_content = $('.xs-all-content-wrapper'),
			pos = footer_content.position(),
			windowpos = $(window).scrollTop();

			console.log(windowpos);
			console.log(pos);

		if (windowpos == pos.top && windowpos <= xs_all_wrap_content.height()) {
			footer_content.removeClass('xs_footer_sticky');	
		} else {
			footer_content.addClass('xs_footer_sticky');
		}
	};

}); // END Scroll Function 

$(window).on('resize', function() {

	// xs custom function
	xs_custom_function();

}); // End Resize

/*==========================================================
			XpeedStudio multipile Maps
======================================================================*/

if (($('#xs-multiple-map-1').length > 0) && ($('#xs-multiple-map-2').length > 0) && ($('#xs-multiple-map-3').length > 0)) {

	var latlng = new google.maps.LatLng(28.561287,-81.444465),
		latlng2 = new google.maps.LatLng(28.507561,-81.482359),
		latlng3 = new google.maps.LatLng(29.125285,-82.048823);

	var myOptions = {
		zoom: 3,
		center: latlng,
		scrollwheel: false,
		navigationControl: false,
		mapTypeControl: true,
		scaleControl: false,
		draggable: true,
		disableDefaultUI: true,
		mapTypeId: google.maps.MapTypeId.ROADMAP,
	};

	var myOptions2 = {
		zoom: 3,
		center: latlng,
		scrollwheel: false,
		navigationControl: false,
		mapTypeControl: true,
		scaleControl: false,
		draggable: true,
		disableDefaultUI: true,
		mapTypeId: google.maps.MapTypeId.ROADMAP,
	};

	var myOptions3 = {
		zoom: 3,
		center: latlng,
		scrollwheel: false,
		navigationControl: false,
		mapTypeControl: true,
		scaleControl: false,
		draggable: true,
		disableDefaultUI: true,
		mapTypeId: google.maps.MapTypeId.ROADMAP,
	};


	var map = new google.maps.Map(document.getElementById("xs-multiple-map-1"), myOptions),
		map2 = new google.maps.Map(document.getElementById("xs-multiple-map-2"), myOptions2),
		map3 = new google.maps.Map(document.getElementById("xs-multiple-map-3"), myOptions3);

	var myMarker = new google.maps.Marker({
		position: latlng,
		map: map,
		title:"Barnett Park"
	});

	var myMarker2 = new google.maps.Marker({
		position: latlng2,
		map: map2,
		title:"Bill Fredrick Park at Turkey Lake"
	});

	var myMarker3 = new google.maps.Marker( {
		position: latlng3,
		map: map3,
		title:"Dogwood Park"
	});
}

/*==========================================================
			XpeedStudio Maps
======================================================================*/

if ($('#xs-maps').length > 0) {
	// When the window has finished loading create our google map below
	google.maps.event.addDomListener(window, 'load', init);

		function init() {
		// Basic options for a simple Google Map
		// For more options see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions
		var mapOptions = {
			// How zoomed in you want the map to start at (always required)
			zoom: 11,
			scrollwheel: false,
			navigationControl: false,
			mapTypeControl: true,
			scaleControl: false,
			draggable: true,
			disableDefaultUI: true,

			// The latitude and longitude to center the map (always required)
			center: new google.maps.LatLng(40.6700, -73.9400), // New York

			// How you would like to style the map. 
			// This is where you would paste any style found on Snazzy Maps.
			styles: [{"featureType":"water","elementType":"geometry","stylers":[{"color":"#a0d6d1"},{"lightness":17}]},{"featureType":"landscape","elementType":"geometry","stylers":[{"color":"#ffffff"},{"lightness":20}]},{"featureType":"road.highway","elementType":"geometry.fill","stylers":[{"color":"#dedede"},{"lightness":17}]},{"featureType":"road.highway","elementType":"geometry.stroke","stylers":[{"color":"#dedede"},{"lightness":29},{"weight":0.2}]},{"featureType":"road.arterial","elementType":"geometry","stylers":[{"color":"#dedede"},{"lightness":18}]},{"featureType":"road.local","elementType":"geometry","stylers":[{"color":"#ffffff"},{"lightness":16}]},{"featureType":"poi","elementType":"geometry","stylers":[{"color":"#f1f1f1"},{"lightness":21}]},{"elementType":"labels.text.stroke","stylers":[{"visibility":"on"},{"color":"#ffffff"},{"lightness":16}]},{"elementType":"labels.text.fill","stylers":[{"saturation":36},{"color":"#333333"},{"lightness":40}]},{"elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"geometry","stylers":[{"color":"#f2f2f2"},{"lightness":19}]},{"featureType":"administrative","elementType":"geometry.fill","stylers":[{"color":"#fefefe"},{"lightness":20}]},{"featureType":"administrative","elementType":"geometry.stroke","stylers":[{"color":"#fefefe"},{"lightness":17},{"weight":1.2}]}]
			};

			// Get the HTML DOM element that will contain your map 
			// We are using a div with id="map" seen below in the <body>
			var mapElement = document.getElementById('xs-maps');

			// Create the Google Map using our element and options defined above
			var map = new google.maps.Map(mapElement, mapOptions);

			// Let's also add a marker while we're at it
			var marker = new google.maps.Marker({
			position: new google.maps.LatLng(40.6700, -73.9400),
			map: map,
			title: 'XspeedStudio',
			icon: 'assets/images/map-marker.png',
		});
	}
}
})(jQuery);