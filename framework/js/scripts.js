//$(document).ready(function () { $("input").attr("autocomplete", "off"); }); 
//$(document).ready(function () { $("textarea").attr("autocomplete", "off"); }); 
 
/*Search*/
$(document).ready(function () {
	
    $("#nav-search-in-content").text("Library Resources");

    $("#searchDropdownBox").change(function () {
        var Search_Str = $(this).val();
        //replace search str in span value
        $("#nav-search-in-content").text(Search_Str);
    });
});

//Show / Hide Text
$(document).ready(function () {

    var slideDiv = $(".slidingDiv");
    var showMore = $(".show_text");
    var hideMore = $(".hide_text");

    /*Show Text*/
    $(".show_text").click(function (e) {
        e.preventDefault();

        if (slideDiv.hasClass("hide")) {
            //showMore.html("> show less");
            showMore.addClass('hide');
            //slideDiv.slideUp(0,function(){
            //					slideDiv.removeClass('hide')
            //					slideDiv.addClass('show_div')
            //				   .slideDown('fast');
            //				});
            slideDiv.removeClass('hide');
            slideDiv.addClass('show_div');

        }

    });

    /*Hide Text*/
    $(".hide_text").click(function (e) {
        e.preventDefault();

        if (slideDiv.hasClass("show_div")) {
            //showMore.html("> show less");
            showMore.removeClass('hide');
            // slideDiv.slideDown(0,function(){
            slideDiv.removeClass('show_div')
            slideDiv.addClass('hide')
            //   .slideUp('slow');
            //	});

        }

    });


});

//Show / Hide Description Text
$(document).ready(function () {

    $(".show_desc_info_content").addClass("hide");
    //Show/Hide Links

    $(".show_desc_info").click(function (e) {
        e.preventDefault();
        var $this = $(this); //capture clicked link

        var show_desc_info_content = $this.parent().find(".show_desc_info_content"); //get the div parent of the clicked .show_hide link

        if (show_desc_info_content.hasClass("hide")) {
            $this.html("> hide information");
            //  show_desc_info_content.slideUp(0,function(){
            show_desc_info_content.removeClass('hide')
            //	   .slideDown('fast');
            //	});

        } else {
            $this.html("show information >");
            //	  show_desc_info_content.slideUp('fast',function(){
            show_desc_info_content.addClass('hide')
            //		 .slideDown(0);
            // 		});
        }

    });


    var show_desc_info_content = $('.show_desc_info_content');

    $('.show_all_desc').click(function (e) {
        e.preventDefault();
        $('.show_desc_info').html("> hide information");
        //show_desc_info_content.slideUp(0,function(){
        show_desc_info_content.removeClass('hide')
        //  .slideDown('fast');
        //});

    });

    $('.hide_all_desc').click(function (e) {
        e.preventDefault();
        $('.show_desc_info').html("show information >");
        //show_desc_info_content.slideUp(0,function(){
        show_desc_info_content.addClass('hide')
        //	 .slideDown('fast');
        //	});
    });




});

//Show / Hide database tables

$(document).ready(function () {

    var alpha_id = $('#pager-alpha .nav a');

    alpha_id.click(function (e) {
        e.preventDefault();

        $('.page').removeClass('hide');

        if (this.id == "all") {
            $('.show_desc_info_content').addClass('hide');
            $('.show_desc_info').html("show information >");

        } else {
            $('.page').not('.database-' + this.id).addClass('hide');
            $('.show_desc_info_content').removeClass('hide');
            $('.show_desc_info').html("> hide information");
        }



    });

});



//Show / Hide Login Information

$(document).ready(function () {

    $(".login_info").addClass('hide');


    $('.login_text').click(function (e) {
        e.preventDefault();
        $('.login_text').removeClass('login_selected');
        $(".login_info").addClass('hide');
        $(".login_info").removeClass('show_login_info');
        $(this).next('.login_info').addClass('show_login_info');
        $(this).next('.login_info').removeClass('hide');

        $(this).addClass('login_selected');
    });



});

//Show / Hide - FAQs All Answers
$(document).ready(function () {

    $('.show_all_faq').click(function () {
        $('div.accordion a.icon').addClass('on');
        $('div.accordion div').addClass('show');
    });

    $('.hide_all_faq').click(function () {
        $('div.accordion a.icon').removeClass('on');
        $('div.accordion div').removeClass('show');
    });



});




//*****************************/

//* ALPHA PAGER SCRIPT        */

//*****************************/

$(document).ready(function () {

    var 
	$alphaPager = $("#pager-alpha"),
	$alphaPagerNav = $(".nav", $alphaPager),
	$alphaPagerPages = $(".page", $alphaPager),
	$pageAnchor = window.location.hash;

    $("a[name]", $alphaPager).each(function () {

        $(this).attr("id", $(this).attr("name"));

    });

    if ($pageAnchor) {

        var 
		$elemID = $($pageAnchor),
		$elemContainer = $elemID.closest("div.page"),
		$elemIndex = $elemContainer.index() - 1;
        $elemContainer.show();

        $("a", $alphaPagerNav).eq($elemIndex).addClass("active");

    }


    $("a", $alphaPagerNav).on("click", function (e) {
        e.preventDefault();

        var alphaID = $(this).attr("href").replace("#", "");

        $("a", $alphaPagerNav).removeAttr("class");

        $alphaPagerPages.hide();

        $(this).addClass("active");

        $(".page:eq(" + alphaID + ")", $alphaPager).show();

        $(".page:visible a:first", $alphaPager).focus();

    })

});


//Database Table Accordion
$(function () {

    //New function Toggle Attribute
    //https://gist.github.com/mathiasbynens/298591
    $.fn.toggleAttr = function (attr, attr1, attr2) {
        return this.each(function () {
            var self = $(this);
            if (self.attr(attr) == attr1)
                self.attr(attr, attr2);
            else
                self.attr(attr, attr1);
        });
    };

    var $database = $('.database-table');
    $database.find("thead tr").show();
    $database.find("tbody").show();

    $database.find("thead").click(function () {

        $(this).siblings().toggleClass('hide');
        $(this).find("th").toggleClass('table_hide');
    })

    $database.find("thead").bind("keydown", function (e) {
        if (e.keyCode == 13) {
            e.stopPropagation(), e.preventDefault();
            $(this).siblings().toggleClass('hide');
            $(this).find("th").toggleClass('table_hide');

            $(this).siblings().find("tr td a").toggleAttr('tabindex', '-1', '0');

        };
    });

});

//Favorite - Add and Remove to My Resources
//Script is needed & used on all pages with a favorite folder

$(document).ready(function () {


    //Get resource name and populate inline for accessibility 
    $(".favorite-id").each(function (index) {

        var favorite_id = $(this).text();

        $(".favorite-id-title").each(function (i) {
            if (index == i) {
                $(this).html(favorite_id);
            }
        });


    });


    //Add and Remove favorites to and from My Resources
    var favorite = $('.favorite');

    //By Keyboard
  /**  favorite.bind("keydown", function (e) {
        if (e.keyCode == 13) {
            e.stopPropagation(), e.preventDefault();
            $(this).toggleClass('favorite_on');

            var favorite_id = $(this).find('.favorite-id-title').text();

            if ($(this).hasClass('favorite_on')) {
                $(this).attr('title', 'Remove ' + favorite_id + ' from My Resources');
            } else {
                $(this).attr('title', 'Add ' + favorite_id + ' to My Resources');
            };
        };

        if (e.keyCode == 27) {
            //return if ESC is pressed
        };
    });
    **/
    //By Click
    //	  favorite.click(function(e){
    //		  e.stopPropagation(), e.preventDefault();
    //		  $(this).toggleClass('favorite_on');  
    //		  
    //		  var favorite_id = $(this).find('.favorite-id-title').text();
    //		  
    //		  if ($(this).hasClass('favorite_on')){ 
    //			$(this).attr('title', 'Remove ' + favorite_id + ' from My Resources');
    //		  } else{
    //			$(this).attr('title', 'Add ' + favorite_id + ' to My Resources');
    //		  };
    //	  });


    //Populate Add or Remove inline depending on the state of the favorite
    $('.favorite').each(function (index) {

        if ($(this).hasClass('favorite_on')) {
            $(this).prepend("Remove");

        } else {
            $(this).prepend("Add");

        }
    });



});



//CONTACT form

$(function () {

    $('.contact_option').addClass("hide");
    $('#option8').removeClass("hide");

    $('.contact_option').find('input,select,textarea,button').attr('tabindex', '-1');
    $('#option8').find('input,select,textarea,button').attr('tabindex', '0');

    //$('.contact_option').children().attr('tabindex', '-1');
    //$('#option8').children().attr('tabindex', '0');

    //    $('#contact_subject').change(function () {
    //        $('.contact_option').addClass("hide");
    //        $('#' + $(this).val()).removeClass("hide");

    //        $('.contact_option').find('input,select,textarea,button').attr('tabindex', '-1');
    //        $('#' + $(this).val()).find('input,select,textarea,button').attr('tabindex', '0');

    //    });







   
    $('#contact_subject').change(function () {
        location.href = "/about/contact.aspx?formID=" + $(this).val() ;
        //var url = "/Templates/contactAjax.aspx?formID=" + $(this).val() + "&mathRandom=" + Math.random();

        //$(document).ajaxStart(function () {
        //    $("#loading").show();
        //    $("#ajaxContact").hide();
        //});
        //$(document).ajaxStop(function () {
        //    $("#loading").hide();
        //    $("#ajaxContact").show();
        //});

        //$.get(url, function (data) {
		//	 $('.aspNetHidden', this).remove(); 
        //    $("#ajaxContact").html(data);
        //    $("#loading").hide();
        //});

      
    });

    $("#loading").hide();







});


///////////////// Onclick for contact forms

function getInformation() {

    //btn - get - information
    userPIN = $("#IndividualsPIN").val();
    userEmail = $("#IndividualEmail").val();
    if (userPIN != "" || userEmail != "") {
        var url = "/Templates/getInformation.aspx?action=find&pin=" + userPIN + "&email=" + userEmail + "&mathRandom=" + Math.random(); ;

        //    var aa = '#addFavDiv' + resourceID;
        //    var bb = '#removeFavDiv' + resourceID;
        //    $(aa).hide();
        //    $(bb).show();

        $.get(url, function (data) {
            $("#getInfo").html(data);
 $('.aspNetHidden', this).remove(); 
        });
    } else {

    alert('Please enter either a PIN or Email');
    }


}

function hideAlert() {
    $("#alertPaneldiv").hide();
    var url = "/Templates/hideAlert.aspx?mathRandom=" + Math.random();
    $.get(url, function (data) {
       // $("#myResourcesCount").html(data);

    });
}

function addToFavorite(resourceID, resourceTypeID, collection , Title , Url  ,  Date  ,  Author) {

    var url = "/Templates/resourceAjax.aspx?action=add&rid=" + resourceID + "&rstid=" + resourceTypeID + "&collection=" + collection + "&Url=" + Url + "&Date=" + Date + "&Author=" + Author + "&Title=" + Title + "&mathRandom=" + Math.random();
    var aa = '#addFavDiv' + resourceID;
    var bb = '#removeFavDiv' + resourceID;
    $(aa).hide();
    $(bb).show();

    $.get(url, function (data) {
        $("#myResourcesCount").html(data);
        document.getElementById('MyResourceslink').title = 'My Resources(' + data +')';
    });
   

}
function deleteFavorite(resourceID, resourceTypeID, collection) {

    var url = "/Templates/resourceAjax.aspx?action=delete&rid=" + resourceID + "&rstid=" + resourceTypeID + "&collection=" + collection + "&mathRandom=" + Math.random();


    var aa = '#addFavDiv' + resourceID;
    var bb = '#removeFavDiv' + resourceID;
    $(aa).show();
    $(bb).hide();

    $.get(url, function (data) {
        $("#myResourcesCount").html(data);

    });

}

function addToFavoriteKP(e , resourceID, resourceTypeID, collection, Title, Url, Date, Author) {
    var code = (e.keyCode ? e.keyCode : e.which);
   // alert(code);
    if (code == 13) {
        var url = "/Templates/resourceAjax.aspx?action=add&rid=" + resourceID + "&rstid=" + resourceTypeID + "&collection=" + collection + "&Url=" + Url + "&Date=" + Date + "&Author=" + Author + "&Title=" + Title + "&mathRandom=" + Math.random();
        var aa = '#addFavDiv' + resourceID;
        var bb = '#removeFavDiv' + resourceID;
        $(aa).hide();
        $(bb).show();

        $.get(url, function (data) {
            $("#myResourcesCount").html(data);

        });
    }

}
function deleteFavoriteKP(e ,resourceID, resourceTypeID, collection) {
    var code = (e.keyCode ? e.keyCode : e.which);
    //alert(code);
    if (code == 13) {
        var url = "/Templates/resourceAjax.aspx?action=delete&rid=" + resourceID + "&rstid=" + resourceTypeID + "&collection=" + collection + "&mathRandom=" + Math.random();


        var aa = '#addFavDiv' + resourceID;
        var bb = '#removeFavDiv' + resourceID;
        $(aa).show();
        $(bb).hide();

        $.get(url, function (data) {
            $("#myResourcesCount").html(data);

        });
    }
}
//var url = "/Templates/resourceAjax.aspx";

//$.get(url, function (data) {
//    $("#myResourcesCount").html(data);

//});





///////////////////////////////////////   Search stuff ///////////////////////////////////////////////////////////////
/**$(document).ready(function () {

        $('#q').click(function () {
			var searchTerm =   $('#q').val();  
            if (searchTerm != "") {
              //alert(searchTerm);
            }
			
			if( ! $( event.target).is('input') ) {
           alert('background was clicked')

      }
	  
        });
		
		
		
});
**/


function gotoSearch2(e) {
    var code = (e.keyCode ? e.keyCode : e.which);
    //alert(code);
    if (code == 13) {
        var searchTerm = document.getElementById('site_search_field').value;
        if (searchTerm != "") {

            location.href = "/sitesearch.aspx?s=" + searchTerm;
        }
        return false;

    }


}

function gotoSearch() {

    var searchTerm = document.getElementById('site_search_field').value;
    if (searchTerm != "") {

        location.href = "/sitesearch.aspx?s=" + searchTerm;
    }
}

////SSA Reports resources/ssareportsarchive.aspx
function gotoSSAReportsSearch2(e) {
    var code = (e.keyCode ? e.keyCode : e.which);
    //alert(code);
    if (code == 13) {
        var searchTerm = document.getElementById('site_ssareportsearch_field').value;
        if (searchTerm != "") {

            location.href = "/resources/ssareportsarchive.aspx?show=simple&s=" + searchTerm;
        }
        return false;

    }


}

function gotoSSAReportsSearch() {

    var searchTerm = document.getElementById('site_ssareportsearch_field').value;
    if (searchTerm != "") {

        location.href = "/resources/ssareportsarchive.aspx?show=simple&s=" + searchTerm;
    }
}

$(document).on('keyup keydown', '#searchDropdownBox', function (e) {
    var code = (e.keyCode ? e.keyCode : e.which);
    if (code == 13) {
        var searchTerm = document.getElementById('q').value;
		 var where = document.getElementById('searchDropdownBox').value;
       searchAndGoto(where , searchTerm);
     }
});




function gotoTopSearch(e) {
    var code = (e.keyCode ? e.keyCode : e.which);
    // alert(code);
    if (code == 13) {
        var searchTerm = document.getElementById('q').value;
		 var where = document.getElementById('searchDropdownBox').value;
       searchAndGoto(where , searchTerm);
        return false;

    }


}
function searchAndGoto(where , searchTerm){
	
	 if (searchTerm != "") {

           

            if (where == "This Site") {

                location.href = "/sitesearch.aspx?s=" + searchTerm;
            }

            if (where == "SSA Reports") {

                location.href = "/resources/ssareportsarchive.aspx?show=simple&s=" + searchTerm;
            }
            if (where == "Library Resources") {

                location.href = "/searchsummon.aspx?show=simple&s=" + searchTerm;
            }


        }

}

function summonSearch(e) {
    var code = (e.keyCode ? e.keyCode : e.which);
    // alert(code);
    if (code == 13) {
        var searchTerm = document.getElementById('home_search').value;

        if (searchTerm != "") {
            location.href = "/searchsummon.aspx?s=" + searchTerm;

        }
        return false;

    }

}

function summonSearch2(e) {
    
        var searchTerm = document.getElementById('home_search').value;

        if (searchTerm != "") {
            location.href = "/searchsummon.aspx?s=" + searchTerm;

        }
        return false;


    }

    function summonSearchList(e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        // alert(code);
        if (code == 13) {
            var searchTerm = document.getElementById('home_search').value;

            if (searchTerm != "") {
                location.href = "/searchsummonlist.aspx?s=" + searchTerm;

            }
            return false;

        }

    }

    function summonSearchList2(e) {

        var searchTerm = document.getElementById('home_search').value;

        if (searchTerm != "") {
            location.href = "/searchsummonlist.aspx?s=" + searchTerm;

        }
        return false;


    }
/////////////////////////////////////// End Search stuff ///////////////////////////////////////////////////////////////


/////////////////////////////////////// External Link stuff ///////////////////////////////////////////////////////////////
 /***  $(document).ready(function () {
	var currentDomain = document.location.protocol + '//' + document.location.hostname;
$('a[href^="http"]:not([href*="' + currentDomain + '"])').on('click', function (e) {
    e.preventDefault();

    // track GA event for outbound links
   // if (typeof _gaq == "undefined")
     //   return;
  var link = $(this).attr('href');
	if(link.indexOf("ssa.gov") > -1) {
	location.href = link;
	} else {
  
            $('.dialog').css('visibility', 'visible');
            $('.dialog').css('opacity', '1');
            $('.overlay').fadeTo("slow", 0.7);
            $('.overlay').show();


            $('#btn-ok').click(function () {
                window.open(link);
                $('.overlay').hide();
                $('.dialog').css('visibility', 'hidden');
                $('.dialog').css('opacity', '0');
            });

            $('#btn-cancel').click(function () {


                $('.dialog').css('visibility', 'hidden');
                $('.dialog').css('opacity', '0');
                $('.overlay').fadeTo("slow", 0);
                //  $('.overlay').hide();
            });

            $('.overlay').click(function () {

                $('.overlay').hide();
                $('.dialog').css('visibility', 'hidden');
                $('.dialog').css('opacity', '0');
            });
	}
    //_gaq.push(["_trackEvent", "outbound", this.href, document.location.pathname + document.location.search]);
});

         
    });
	***/
	

    
/////////////////////////////////////// End External Link stuff ///////////////////////////////////////////////////////////////

/////////////////////////////////////// Link Tracking Stuff ///////////////////////////////////////////////////////////////

    $(document).ready(function () {


        $('.outboundResource').click(function () {
            var link = $(this).attr('href');
            var resourceid = $(this).attr('resourceid');
           // alert(resourceid);
            var url = "/Templates/Ajax/clicktracks.aspx?rid=" + resourceid + "&link=" + link + "&mathRandom=" + Math.random();

            $.get(url, function (data) {
                //   $("#myResourcesCount").html(data);
                cache: false
            });
            return;

        });


        $('a').click(function () {
            if (!$(this).hasClass("outboundResource")) {
                var link = $(this).attr('href');
                var url = "/Templates/Ajax/clicktracks.aspx?link=" + link + "&mathRandom=" + Math.random();
                if (link != "" || link != "#") {
                    $.get(url, function (data) {
                        //   $("#myResourcesCount").html(data);
                        cache: false

                    });
                }
            }
        });






    });




/////////////////////////////////////// Link Tracking  Stuff ///////////////////////////////////////////////////////////////