if(jQuery){var $html=$("html");$html.addClass("not-visible"),function(e){function n(e){return"string"==typeof e}function a(e){var n=C.call(arguments,1);return function(){return e.apply(this,n.concat(C.call(arguments)))}}function t(e){return e.replace(b,"$2")}function i(e){return e.replace(/(?:^[^?#]*\?([^#]*).*$)?.*/,"$1")}function o(a,t,i,o,r){var s,d,u,$,v;return o!==l?(u=i.match(a?b:/^([^#?]*)\??([^#]*)(#?.*)/),v=u[3]||"",2===r&&n(o)?d=o.replace(a?p:N,""):($=f(u[2]),o=n(o)?f[a?D:T](o):o,d=2===r?o:1===r?e.extend({},o,$):e.extend({},$,o),d=c(d),a&&(d=d.replace(g,k))),s=u[1]+(a?y:d||!u[1]?"?":"")+d+v):s=t(i!==l?i:location.href),s}function r(e,a,t){return a===l||"boolean"==typeof a?(t=a,a=w[e?D:T]()):a=n(a)?a.replace(e?p:N,""):a,f(a,t)}function s(a,t,i,o){return n(i)||"object"==typeof i||(o=i,i=t,t=l),this.each(function(){var n=e(this),r=t||h()[(this.nodeName||"").toLowerCase()]||"",s=r&&n.attr(r)||"";n.attr(r,w[a](s,i,o))})}var l,c,d,f,u,$,v,h,p,b,g,m,y,C=Array.prototype.slice,k=decodeURIComponent,w=e.param,q=e.bbq=e.bbq||{},x=e.event.special,S="hashchange",T="querystring",D="fragment",L="elemUrlAttr",P="href",A="src",N=/^.*\?|#.*$/g,j={};w[T]=a(o,0,i),w[D]=d=a(o,1,t),w.sorted=c=function(n,a){var t=[],i={};return e.each(w(n,a).split("&"),function(e,n){var a=n.replace(/(?:%5B|=).*$/,""),o=i[a];o||(o=i[a]=[],t.push(a)),o.push(n)}),e.map(t.sort(),function(e){return i[e]}).join("&")},d.noEscape=function(n){n=n||"";var a=e.map(n.split(""),encodeURIComponent);g=RegExp(a.join("|"),"g")},d.noEscape(",/"),d.ajaxCrawlable=function(e){return e!==l&&(e?(p=/^.*(?:#!|#)/,b=/^([^#]*)(?:#!|#)?(.*)$/,y="#!"):(p=/^.*#/,b=/^([^#]*)#?(.*)$/,y="#"),m=!!e),m},d.ajaxCrawlable(0),e.deparam=f=function(n,a){var t={},i={"true":!0,"false":!1,"null":null};return e.each(n.replace(/\+/g," ").split("&"),function(n,o){var r,s=o.split("="),c=k(s[0]),d=t,f=0,u=c.split("]["),$=u.length-1;if(/\[/.test(u[0])&&/\]$/.test(u[$])?(u[$]=u[$].replace(/\]$/,""),u=u.shift().split("[").concat(u),$=u.length-1):$=0,2===s.length)if(r=k(s[1]),a&&(r=r&&!isNaN(r)?+r:"undefined"===r?l:i[r]!==l?i[r]:r),$)for(;$>=f;f++)c=""===u[f]?d.length:u[f],d=d[c]=$>f?d[c]||(u[f+1]&&isNaN(u[f+1])?{}:[]):r;else e.isArray(t[c])?t[c].push(r):t[c]=t[c]!==l?[t[c],r]:r;else c&&(t[c]=a?l:"")}),t},f[T]=a(r,0),f[D]=u=a(r,1),e[L]||(e[L]=function(n){return e.extend(j,n)})({a:P,base:P,iframe:A,img:A,input:A,form:"action",link:P,script:A}),h=e[L],e.fn[T]=a(s,T),e.fn[D]=a(s,D),q.pushState=$=function(e,a){n(e)&&/^#/.test(e)&&a===l&&(a=2);var t=e!==l,i=d(location.href,t?e:{},t?a:2);location.href=i},q.getState=v=function(e,n){return e===l||"boolean"==typeof e?u(e):u(n)[e]},q.removeState=function(n){var a={};n!==l&&(a=v(),e.each(e.isArray(n)?n:arguments,function(e,n){delete a[n]})),$(a,2)},x[S]=e.extend(x[S],{add:function(n){function a(e){var n=e[D]=d();e.getState=function(e,a){return e===l||"boolean"==typeof e?f(n,e):f(n,a)[e]},t.apply(this,arguments)}var t;return e.isFunction(n)?(t=n,a):(t=n.handler,n.handler=a,void 0)}})}(jQuery,this),function(e,n,a){"$:nomunge";function t(e){return e=e||location.href,"#"+e.replace(/^[^#]*#?(.*)$/,"$1")}var i,o="hashchange",r=document,s=e.event.special,l=r.documentMode,c="on"+o in n&&(l===a||l>7);e.fn[o]=function(e){return e?this.bind(o,e):this.trigger(o)},e.fn[o].delay=50,s[o]=e.extend(s[o],{setup:function(){return c?!1:(e(i.start),a)},teardown:function(){return c?!1:(e(i.stop),a)}}),i=function(){function i(){var a=t(),r=$(d);a!==d?(u(d=a,r),e(n).trigger(o)):r!==d&&(location.href=location.href.replace(/#.*/,"")+r),s=setTimeout(i,e.fn[o].delay)}var s,l={},d=t(),f=function(e){return e},u=f,$=f;return l.start=function(){s||i()},l.stop=function(){s&&clearTimeout(s),s=a},!c&&function(){var n,a;l.start=function(){n||(a=e.fn[o].src,a=a&&a+t(),n=e('<iframe tabindex="-1" title="empty"/>').hide().one("load",function(){a||u(t()),i()}).attr("src",a||"javascript:0").insertAfter("body")[0].contentWindow,r.onpropertychange=function(){try{"title"===event.propertyName&&(n.document.title=r.title)}catch(e){}})},l.stop=f,$=function(){return t(n.location.href)},u=function(a,t){var i=n.document,s=e.fn[o].domain;a!==t&&(i.title=r.title,i.open(),s&&i.write('<script>document.domain="'+s+'"</script>'),i.close(),n.location.hash=a)}}(),l}()}(jQuery,this),function(e){"function"==typeof define&&define.amd&&define.amd.jQuery?define(["jquery"],e):e(jQuery)}(function(e){function n(e){return e}function a(e){return decodeURIComponent(e.replace(i," "))}function t(e){0===e.indexOf('"')&&(e=e.slice(1,-1).replace(/\\"/g,'"').replace(/\\\\/g,"\\"));try{return o.json?JSON.parse(e):e}catch(n){}}var i=/\+/g,o=e.cookie=function(i,r,s){if(void 0!==r){if(s=e.extend({},o.defaults,s),"number"==typeof s.expires){var l=s.expires,c=s.expires=new Date;c.setDate(c.getDate()+l)}return r=o.json?JSON.stringify(r):r+"",document.cookie=[encodeURIComponent(i),"=",o.raw?r:encodeURIComponent(r),s.expires?"; expires="+s.expires.toUTCString():"",s.path?"; path="+s.path:"",s.domain?"; domain="+s.domain:"",s.secure?"; secure":""].join("")}for(var d=o.raw?n:a,f=document.cookie.split("; "),u=i?void 0:{},$=0,v=f.length;v>$;$++){var h=f[$].split("="),p=d(h.shift()),b=d(h.join("="));if(i&&i===p){u=t(b);break}i||(u[p]=t(b))}return u};o.defaults={},e.removeCookie=function(n,a){return void 0!==e.cookie(n)?(e.cookie(n,"",e.extend(a,{expires:-1})),!0):!1}}),function(e){function n(n){function t(){n?r.removeData(n):u&&delete a[u]}function o(){l.id=setTimeout(function(){l.fn()},$)}var r,s=this,l={},c=n?e.fn:e,d=arguments,f=4,u=d[1],$=d[2],v=d[3];if("string"!=typeof u&&(f--,u=n=0,$=d[1],v=d[2]),n?(r=s.eq(0),r.data(n,l=r.data(n)||{})):u&&(l=a[u]||(a[u]={})),l.id&&clearTimeout(l.id),delete l.id,v)l.fn=function(e){"string"==typeof v&&(v=c[v]),v.apply(s,i.call(d,f))!==!0||e?t():o()},o();else{if(l.fn)return void 0===$?t():l.fn($===!1),!0;t()}}var a={},t="doTimeout",i=Array.prototype.slice;e[t]=function(){return n.apply(window,[0].concat(i.call(arguments)))},e.fn[t]=function(){var e=i.call(arguments),a=n.apply(this,[t+e[0]].concat(e));return"number"==typeof e[0]||"number"==typeof e[1]?this:a}}(jQuery),$.extend($.easing,{def:"easeInOutQuad",swing:function(e,n,a,t,i){return jQuery.easing[jQuery.easing.def](e,n,a,t,i)},easeInOutQuad:function(e,n,a,t,i){return 1>(n/=i/2)?t/2*n*n+a:-t/2*(--n*(n-2)-1)+a},easeInOutExpo:function(e,n,a,t,i){return 0==n?a:n==i?a+t:1>(n/=i/2)?t/2*Math.pow(2,10*(n-1))+a:t/2*(-Math.pow(2,-10*--n)+2)+a}}),$.support.pdf=function(){try{if(!Modernizr.csstransitions){var e=null;try{e=new ActiveXObject("AcroPDF.PDF")}catch(n){}return e?!0:!1}if(null!=navigator.plugins)for(var a in navigator.plugins){if("Adobe Acrobat"==a)return!0;if(navigator.plugins[a].name&&("Adobe Acrobat"==navigator.plugins[a].name||"Chrome PDF Viewer"==navigator.plugins[a].name))return!0}}catch(n){}return!0}();var $document=$(document),$window=$(window),$body=$("body"),$htmlAndBody=$("html, body"),$noOpacity=$html.hasClass("no-opacity"),$noTransitions=$html.hasClass("no-csstransitions");$noOpacity&&$(".overlay").css("opacity",".7");var $keyboard,bbqState={},$page=$("#page"),$banner=$("#banner"),$titleBar=$("#title-bar"),$content=$("#content"),$language=$html.attr("lang"),$englishPage=$body.attr("data-english-file")||"/",$spanishPage=$body.attr("data-spanish-file")||"/espanol/",$languageLink=$("#language").find("a");"es"===$language?$languageLink.attr("href",$englishPage):$languageLink.attr("href",$spanishPage);var $pdfLink=$content.find("a[href$='.pdf']"),$pdfPlugin="&#160;<a href='http://get.adobe.com/reader/'><img alt='Get Adobe Acrobat' src='http://www.socialsecurity.gov/framework/images/icons/pdf.gif' /></a>";$pdfLink.length&&($.support.pdf||$pdfLink.eq(0).addClass("no-icon").after($pdfPlugin));var $skipNav=$("#skip-navigation");$skipNav.on("focus",function(){$keyboard=!0,$html.addClass("keyboard")}),$skipNav.on("click",function(e){e.preventDefault(),$titleBar.length?$titleBar.attr("tabindex","-1").focus():$content.attr("tabindex","-1").focus()});var $search=$banner.find("#search-box"),$searchField=$search.find("#q"),placeholderSupport="placeholder"in document.createElement("input");if(!placeholderSupport)switch($language){case"es":$searchField.val("Búsqueda..."),$searchField.focus(function(){"Búsqueda..."===$(this).val()&&$(this).val("")}),$searchField.blur(function(){""===$(this).val()&&$(this).val("Búsqueda...")});break;default:$searchField.val("Search..."),$searchField.focus(function(){"Search..."===$(this).val()&&$(this).val("")}),$searchField.blur(function(){""===$(this).val()&&$(this).val("Search...")})}var $dialog=$("#dialog"),$dialogContent=$dialog.find("> div"),$dialogLink=$("a.dialog-info, a.disclaimer"),ajaxFile,dialog={load:function(){$dialog.on("keydown",function(e){27===e.keyCode&&($body.removeClass("dialog-open"),$page.attr("aria-hidden","false"),$dialogContent.empty(),$("a.focus").filter(":first").focus().removeClass("focus"))}),$dialogLink.attr("aria-haspopup","true").on("click",function(e){e.preventDefault();var n=$(this),a=this.href,t=n.hasClass("disclaimer");if(n.addClass("focus"),$body.addClass("dialog-open"),t)switch($language){case"es":ajaxFile="/framework/ajax/dialog/disclaimer/spanish.html";break;default:ajaxFile="/framework/ajax/dialog/disclaimer/english.html"}else ajaxFile=a;$.ajax({context:$dialog,url:ajaxFile,success:function(e){$page.attr("aria-hidden","true"),$dialogContent.empty().html(e).find(".dialog-close").on("click",function(e){e.preventDefault(),$body.removeClass("dialog-open"),$page.attr("aria-hidden","false"),$("a.focus").filter(":first").focus().removeClass("focus")}),t&&$dialog.find("#btn-ok").on("click",function(e){e.preventDefault(),window.location=a}),$dialog.find("a:first").on("keydown",function(e){e.shiftKey&&9===e.keyCode&&e.preventDefault()}).end().find("a:last").on("keydown",function(e){9===e.keyCode&&(e.shiftKey||e.preventDefault())}),$dialog.find("h4").filter(":first").focus()}})})}},$accordion=$content.find("div.accordion"),$accordionLink=$accordion.find("> p a"),$accordionContent=$accordion.find("> div"),$accordionContentAnchor=$accordionContent.find("a[href^='#']").not("a[href='#']"),accordion={load:function(){accordion.generateID(),accordion.activate(),$accordion.css("visibility","visible")},generateID:function(){$accordion.each(function(e){$(this).attr("id","a"+e)})},change:function(e,n){if(e>=0){var n="#"+n,a=$(n),t=a.find("> div"),i=a.find("> p a");i.removeClass("on"),t.removeClass("show"),i.eq(e).addClass("on"),t.eq(e).addClass("show"),$keyboard&&t.eq(e).focus()}},activate:function(){$accordionContentAnchor.length&&accordion.anchorLinks(),$accordionLink.attr("href","#").removeAttr("id").addClass("icon"),$accordionLink.on("keydown",function(){$accordionContent.attr("tabindex","-1"),$keyboard=!0}),$accordionLink.on("click",function(e){e.preventDefault();var n=$(this),a=n.parent().next("div"),t=n.closest(".accordion"),i=t.attr("id"),o=$(this).parent().prevAll("p").length;n.hasClass("on")?(n.removeClass("on"),a.removeClass("show"),bbqState[i]="-1",$.bbq.pushState(bbqState)):(bbqState[i]=o,$.bbq.pushState(bbqState))}),$window.on("hashchange",function(){$accordion.each(function(){var e=this.id,n=$.bbq.getState(e);if(n){var a=$("> div",$(this));a.eq(n).is(":visible")||accordion.change(n,e)}})}),$window.trigger("hashchange")},anchorLinks:function(){var e=window.location.hash.replace("#","");if(e.indexOf("=")<0){var n=$accordion.find("a[name="+e+"]");if(n.length){var a=n.closest("div.accordion"),t=a.attr("id"),i=n.parents().prev("p"),o=a.find("> p").index(i);bbqState[t]=o,$.bbq.removeState(e),$.bbq.pushState(bbqState)}}$accordionContentAnchor.on("click",function(e){e.preventDefault();var n=$(this),a=n.attr("href").replace("#",""),t=$accordion.find("a[name="+a+"]"),i=t.closest("div.accordion"),o=i.attr("id"),r=t.closest("div").prev("p"),s=i.find("> p").index(r);bbqState[o]=s,$.bbq.pushState(bbqState),$htmlAndBody.stop().animate({scrollTop:$(t).offset().top},1500)})}},$siteNav=$("#nav-site"),$sideBarNav=$content.find("#nav-sidebar");$sideBarNav.length&&$body.addClass("nav-sidebar");var menus={global:function(){function e(e){27===e.keyCode&&$siteNav.find("a.on").focus(),t.removeClass("show"),$siteNav.find("a.on").removeClass("on")}var n=$siteNav.find("> ul > li"),a=n.find("> a"),t=n.find("> div"),i=t.find("a");a.on("keydown",function(){$keyboard=!0}),$document.on("click",function(){a.removeClass("on"),t.removeClass("show")}),a.on("mouseenter",function(){var e=$(this),n=e.next("div");$.doTimeout("menu",300,function(){a.removeClass("on"),t.removeClass("show"),e.addClass("on"),n.addClass("show")})}),$siteNav.on("mouseleave",function(){$.doTimeout("menu",300,function(){a.removeClass("on"),t.removeClass("show")})}),a.on("click",function(e){e.stopPropagation(),e.preventDefault();var n=$(this),i=n.attr("href"),o=n.next("div");"#"!==i?window.location=i:n.hasClass("on")?(n.removeClass("on"),o.removeClass("show")):(a.removeClass("on"),t.removeClass("show"),n.addClass("on"),o.addClass("show"),$keyboard&&o.find("a").filter(":first").focus())}),i.on("keydown",function(n){27===n.keyCode&&e(n)}),t.find("a:first").on("keydown",function(n){n.shiftKey&&9===n.keyCode&&e(n)}),t.find("a:last").on("keydown",function(n){9===n.keyCode&&(n.shiftKey||e(n))})},sidebar:function(){var e=$sideBarNav.find("nav > ul > li"),n=e.find("a.sub"),a=$sideBarNav.find("ul ul");n.on("keydown",function(){$keyboard=!0,a.find("a").on("keydown",function(e){if(27===e.keyCode){e.preventDefault();var n=$(this),a=n.closest("ul"),t=a.closest("li").find("a").filter(":first");t.removeClass("on").focus(),bbqState.sb="-1",$.bbq.pushState(bbqState)}})}),a.find("a").each(function(){var e,n,a=$(this),t=a.attr("href"),i=a.parents("li:eq(1)").index(),o="";if("#"!==t&&t.indexOf("/")<0)if(e=t.split("#"),n=e.length,n>1){for(var r=0;++r<n;)o+=e[r];o+="&sb="+i,a.attr("href",e[0]+"#"+o)}else a.attr("href",e[0]+"#sb="+i)}),n.on("click",function(e){e.preventDefault();var n=$(this),a=n.next("ul");n.hasClass("on")?(bbqState.sb="-1",$.bbq.pushState(bbqState)):(bbqState.sb=n.parent("li").index(),$.bbq.pushState(bbqState),$keyboard&&setTimeout(function(){a.find("a").filter(":first").focus()},10))}),$window.on("hashchange",function(){var t=$.bbq.getState("sb");if(n.removeClass("on"),$noOpacity&&a.hide(),t>=0){var i=e.eq(t),o=i.find("a.sub"),r=o.next("ul");o.addClass("on"),$noOpacity&&r.show()}}),$window.trigger("hashchange")}},$hTabs=$content.find("div.tabs"),$hTabsLink=$hTabs.find("> ul a"),$hTabsContent=$hTabs.find("> div"),$vTabs=$content.find("#vertical-tabs"),$vTabsLink=$vTabs.find("> ul a"),$vTabsContent=$vTabs.find("> div"),tabs={change:function(e,n){"horizontal"===n?($hTabsLink.removeClass("on"),$hTabsLink.eq(e).addClass("on"),$hTabsContent.removeClass("show"),$hTabsContent.eq(e).addClass("show")):($vTabsLink.removeClass("on"),$vTabsLink.eq(e).addClass("on"),$vTabsContent.removeClass("show"),$vTabsContent.eq(e).addClass("show"))},horizontal:function(){$hTabsLink.on("keydown",function(){$hTabsContent.attr("tabindex","-1"),$keyboard=!0}),$hTabsLink.attr("href","#"),$hTabsLink.on("click",function(e){e.preventDefault();var n=$(this),a=n.parent("li").index();tabs.change(a,"horizontal"),bbqState.ht=a,$.bbq.pushState(bbqState),$keyboard&&$hTabsContent.eq(a).focus()}),$window.on("hashchange",function(){var e=$.bbq.getState("ht",!0)||0;tabs.change(e,"horizontal")}),$window.trigger("hashchange"),$hTabs.css("visibility","visible")},vertical:function(){$vTabsLink.on("keydown",function(){$vTabsContent.attr("tabindex","-1"),$keyboard=!0}),$vTabsLink.on("click",function(e){e.preventDefault();var n=$(this),a=n.parent("li").index();tabs.change(a,"vertical"),bbqState.vt=a,$.bbq.pushState(bbqState),$keyboard&&$vTabsContent.eq(a).focus()}),$window.on("hashchange",function(){var e=$.bbq.getState("vt",!0)||0;tabs.change(e,"vertical")}),$window.trigger("hashchange"),$vTabs.css("visibility","visible")}},$carousel=$content.find("#carousel, #carousel-fade, #carousel-slider"),carousel={load:function(){function e(){c=setInterval(function(){l=b.filter(".on").prevAll().length,r=l===s-1?!0:!1,l=l===s-1?0:l+1,n(l,r)},h)}function n(e,n){if(n&&(clearInterval(c),$carousel.off("mouseleave")),$noTransitions&&"fade"===d&&(t.filter(".on").find(".caption").animate({opacity:"0"},100,"linear",function(){$(this).parent().animate({opacity:"0"},500,"linear")}),t.eq(e).animate({opacity:"1"},500,"linear",function(){a.eq(e).animate({opacity:".8"},500,"linear")})),"slider"===d){if($noTransitions){var o=e;o="-"+100*o+"%",i.animate({marginLeft:o},500)}i.removeAttr("class").addClass("slide-"+(e+1))}t.filter(".on").removeClass("on").end().attr("tabindex","-1").eq(e).addClass("on").attr("tabindex","0"),b.filter(".on").removeClass("on").end().eq(e).addClass("on"),$keyboard&&("slider"===d?setTimeout(function(){t.filter(".on").focus()},750):t.filter(".on").focus())}var a,t,i,o,r,s,l,c,d,f=$carousel.attr("id"),u=$("<nav />"),v="",h=1e3*$carousel.attr("data-timer")||1e4;"carousel-fade"===f?(t=$carousel.find("> div a"),a=t.find("> div"),d="fade"):"carousel-slider"===f?(o=$carousel.find("> div"),i=o.find("> div"),t=i.find("a"),a=t.find("> div"),d="slider"):(t=$carousel.find("> div > a"),d="basic"),s=t.length;for(var p=-1;++p<s;)v+="<a href='#'>"+(p+1)+"</a>";u.append(v),$carousel.append(u);var b=u.find("a");b.eq(0).addClass("on"),t.not(":first").attr("tabindex","-1"),t.eq(0).addClass("on").attr("tabindex","0"),$noOpacity&&("slider"===d?a.css("opacity",".8"):"fade"===d&&(t.not(":first").css("opacity","0"),a.css("opacity",".8"))),$carousel.on("mouseenter",function(){clearInterval(c)}),$carousel.on("mouseleave",function(){e()}),t.on("focus",function(){clearInterval(c)}),b.on("keydown",function(){$keyboard=!0}),b.on("click",function(e){e.preventDefault(),l=$(this).prevAll().length,n(l,!0)}),e(),$carousel.css("visibility","visible")}},$definitionPanel=$("<div id='definition-panel' role='dialog'></div>"),$definitionPanelClose=$("<a href='#'>X</a>"),$definitionLink=$content.find("a.definition"),definitionPanel={load:function(){$body.append($definitionPanel),"es"===$language?$definitionPanelClose.attr("title","Cerrar panel de definiciones"):$definitionPanelClose.attr("title","Close Definition Panel");var e={load:function(e){$.ajax({context:$definitionPanel,url:e,success:function(e){var n;n=$(e).find("dl").html(),$definitionPanel.html(n).wrapInner("<dl></dl>"),$definitionPanel.find("dt").append($definitionPanelClose),$definitionPanel.find("dt").find("a").on("click",function(e){e.preventDefault(),$definitionPanel.removeClass("show").empty(),$keyboard&&$("a.focus").filter(":first").focus().removeClass("focus")}),$definitionPanel.addClass("show"),$keyboard&&$definitionPanel.find("dt").attr("tabindex","-1").focus()}})}};$definitionPanel.on("keydown",function(e){27===e.keyCode&&($definitionPanel.removeClass("show").empty(),$("a.focus, button.focus").filter(":first").focus().removeClass("focus"))}),$definitionLink.attr("aria-haspopup","true").on("click",function(n){n.preventDefault();var a=$(this),t=a.attr("href"),i=a.offset().left-10,o=a.offset().top+30;$keyboard&&a.addClass("focus"),$definitionPanel.css("top",o).css("left",i),$definitionPanel.empty(),e.load(t)}),$document.on("click",function(){$definitionPanel.removeClass("show")})}},$govDeliveryContainer=$("<div></div>"),$govDeliveryLink=$("<a href='#'></a>"),$govDeliveryTopic=$body.attr("data-gov-delivery"),govDeliveryEnglishAddress="https://public.govdelivery.com/accounts/USSSA/subscriber/new?topic_id="+$govDeliveryTopic,govDeliverySpanishAddress="https://public.govdelivery.com/accounts/USSSAESP/subscriber/new?topic_id="+$govDeliveryTopic;switch($language){case"es":$govDeliveryLink.attr("href",govDeliverySpanishAddress).text("Reciba actualizaciones");break;default:$govDeliveryLink.attr("href",govDeliveryEnglishAddress).text("Get updates")}$govDeliveryTopic&&($govDeliveryContainer.append($govDeliveryLink),$titleBar.append($govDeliveryContainer).addClass("gov-delivery"));var $toggle=$content.find("div.toggle-link"),$toggleLink=$toggle.find("> a:first").removeAttr("class"),$toggleContent=$toggle.find("> div").removeAttr("class"),toggle={load:function(){$toggleLink.on("click",function(e){e.preventDefault();var n=$(this),a=n.next("div");n.toggleClass("on"),a.toggleClass("show")})}},$bodyClass=$body.attr("class")||"",$legacyNav=$("#nav"),$legacySidebar=$content.find(".sidebar-nav"),urlPath=window.location.pathname,urlFile=urlPath.substring(urlPath.lastIndexOf("/")+1);if($bodyClass.indexOf("tab-")>=0){var className=$bodyClass.match(/tab-+\w*./).toString();className=className.replace("tab-","").replace(/^\s\s*/,"").replace(/\s\s*$/,""),"home"!==className?$legacyNav.find("a[href*='"+className+"']").closest("li").addClass("active"):$legacyNav.find("a").eq(0).closest("li").addClass("active")}$legacySidebar&&(urlFile?$legacySidebar.find("a[href$='"+urlPath+"']").addClass("active"):$legacySidebar.find("a").eq(0).addClass("active")),$siteNav&&menus.global(),$sideBarNav.hasClass("menu")&&menus.sidebar(),$accordion.length&&accordion.load(),$carousel.length&&carousel.load(),$dialog.length&&dialog.load(),$definitionLink.length&&definitionPanel.load(),$toggle.length&&toggle.load(),$hTabs.length&&tabs.horizontal(),$vTabs.length&&tabs.vertical(),$html.removeClass("not-visible")}