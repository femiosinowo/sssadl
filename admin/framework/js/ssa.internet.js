
var $html=$("html");$html.addClass("not-visible");var ua=navigator.userAgent,oldFireFox=!1;ua.match(/FireFox\/3.6/i)&&(oldFireFox=!0,$html.removeClass("svg"),$html.addClass("no-svg"));
!function(e){function t(e){return"string"==typeof e}function n(e){var t=w.call(arguments,1);return function(){return e.apply(this,t.concat(w.call(arguments)))}}function i(e){return e.replace(g,"$2")}function r(e){return e.replace(/(?:^[^?#]*\?([^#]*).*$)?.*/,"$1")}function o(n,i,r,o,s){var a,l,d,h,p;return o!==c?(d=r.match(n?g:/^([^#?]*)\??([^#]*)(#?.*)/),p=d[3]||"",2===s&&t(o)?l=o.replace(n?v:E,""):(h=f(d[2]),o=t(o)?f[n?F:A](o):o,l=2===s?o:1===s?e.extend({},o,h):e.extend({},h,o),l=u(l),n&&(l=l.replace(y,q))),a=d[1]+(n?b:l||!d[1]?"?":"")+l+p):a=i(r!==c?r:location.href),a}function s(e,n,i){return n===c||"boolean"==typeof n?(i=n,n=j[e?F:A]()):n=t(n)?n.replace(e?v:E,""):n,f(n,i)}function a(n,i,r,o){return t(r)||"object"==typeof r||(o=r,r=i,i=c),this.each(function(){var t=e(this),s=i||m()[(this.nodeName||"").toLowerCase()]||"",a=s&&t.attr(s)||"";t.attr(s,j[n](a,r,o))})}var c,u,l,f,d,h,p,m,v,g,y,x,b,w=Array.prototype.slice,q=decodeURIComponent,j=e.param,C=e.bbq=e.bbq||{},S=e.event.special,k="hashchange",A="querystring",F="fragment",I="elemUrlAttr",$="href",N="src",E=/^.*\?|#.*$/g,T={};j[A]=n(o,0,r),j[F]=l=n(o,1,i),j.sorted=u=function(t,n){var i=[],r={};return e.each(j(t,n).split("&"),function(e,t){var n=t.replace(/(?:%5B|=).*$/,""),o=r[n];o||(o=r[n]=[],i.push(n)),o.push(t)}),e.map(i.sort(),function(e){return r[e]}).join("&")},l.noEscape=function(t){t=t||"";var n=e.map(t.split(""),encodeURIComponent);y=RegExp(n.join("|"),"g")},l.noEscape(",/"),l.ajaxCrawlable=function(e){return e!==c&&(e?(v=/^.*(?:#!|#)/,g=/^([^#]*)(?:#!|#)?(.*)$/,b="#!"):(v=/^.*#/,g=/^([^#]*)#?(.*)$/,b="#"),x=!!e),x},l.ajaxCrawlable(0),e.deparam=f=function(t,n){var i={},r={"true":!0,"false":!1,"null":null};return e.each(t.replace(/\+/g," ").split("&"),function(t,o){var s,a=o.split("="),u=q(a[0]),l=i,f=0,d=u.split("]["),h=d.length-1;if(/\[/.test(d[0])&&/\]$/.test(d[h])?(d[h]=d[h].replace(/\]$/,""),d=d.shift().split("[").concat(d),h=d.length-1):h=0,2===a.length)if(s=q(a[1]),n&&(s=s&&!isNaN(s)?+s:"undefined"===s?c:r[s]!==c?r[s]:s),h)for(;h>=f;f++)u=""===d[f]?l.length:d[f],l=l[u]=h>f?l[u]||(d[f+1]&&isNaN(d[f+1])?{}:[]):s;else e.isArray(i[u])?i[u].push(s):i[u]=i[u]!==c?[i[u],s]:s;else u&&(i[u]=n?c:"")}),i},f[A]=n(s,0),f[F]=d=n(s,1),e[I]||(e[I]=function(t){return e.extend(T,t)})({a:$,base:$,iframe:N,img:N,input:N,form:"action",link:$,script:N}),m=e[I],e.fn[A]=n(a,A),e.fn[F]=n(a,F),C.pushState=h=function(e,n){t(e)&&/^#/.test(e)&&n===c&&(n=2);var i=e!==c,r=l(location.href,i?e:{},i?n:2);location.href=r},C.getState=p=function(e,t){return e===c||"boolean"==typeof e?d(e):d(t)[e]},C.removeState=function(t){var n={};t!==c&&(n=p(),e.each(e.isArray(t)?t:arguments,function(e,t){delete n[t]})),h(n,2)},S[k]=e.extend(S[k],{add:function(t){function n(e){var t=e[F]=l();e.getState=function(e,n){return e===c||"boolean"==typeof e?f(t,e):f(t,n)[e]},i.apply(this,arguments)}var i;return e.isFunction(t)?(i=t,n):(i=t.handler,t.handler=n,void 0)}})}(jQuery,this),!function(e){"function"==typeof define&&define.amd?define(["jquery"],e):e(jQuery)}(function(e){function t(e){return a.raw?e:encodeURIComponent(e)}function n(e){return a.raw?e:decodeURIComponent(e)}function i(e){return t(a.json?JSON.stringify(e):e+"")}function r(e){0===e.indexOf('"')&&(e=e.slice(1,-1).replace(/\\"/g,'"').replace(/\\\\/g,"\\"));try{return e=decodeURIComponent(e.replace(s," ")),a.json?JSON.parse(e):e}catch(t){}}function o(t,n){var i=a.raw?t:r(t);return e.isFunction(n)?n(i):i}var s=/\+/g,a=e.cookie=function(r,s,c){if(void 0!==s&&!e.isFunction(s)){if(c=e.extend({},a.defaults,c),"number"==typeof c.expires){var u=c.expires,l=c.expires=new Date;l.setTime(+l+864e5*u)}return document.cookie=t(r)+"="+i(s)+(c.expires?"; expires="+c.expires.toUTCString():"")+(c.path?"; path="+c.path:"")+(c.domain?"; domain="+c.domain:"")+(c.secure?"; secure":"")}for(var f=r?void 0:{},d=document.cookie?document.cookie.split("; "):[],h=0,p=d.length;p>h;h++){var m=d[h].split("="),v=n(m.shift()),g=m.join("=");if(r&&r===v){f=o(g,s);break}r||void 0===(g=o(g))||(f[v]=g)}return f};a.defaults={},e.removeCookie=function(t,n){return void 0===e.cookie(t)?!1:(e.cookie(t,"",e.extend({},n,{expires:-1})),!e.cookie(t))}}),function(e){function t(t){function i(){t?s.removeData(t):d&&delete n[d]}function o(){c.id=setTimeout(function(){c.fn()},h)}var s,a=this,c={},u=t?e.fn:e,l=arguments,f=4,d=l[1],h=l[2],p=l[3];if("string"!=typeof d&&(f--,d=t=0,h=l[1],p=l[2]),t?(s=a.eq(0),s.data(t,c=s.data(t)||{})):d&&(c=n[d]||(n[d]={})),c.id&&clearTimeout(c.id),delete c.id,p)c.fn=function(e){"string"==typeof p&&(p=u[p]),p.apply(a,r.call(l,f))!==!0||e?i():o()},o();else{if(c.fn)return void 0===h?i():c.fn(h===!1),!0;i()}}var n={},i="doTimeout",r=Array.prototype.slice;e[i]=function(){return t.apply(window,[0].concat(r.call(arguments)))},e.fn[i]=function(){var e=r.call(arguments),n=t.apply(this,[i+e[0]].concat(e));return"number"==typeof e[0]||"number"==typeof e[1]?this:n}}(jQuery),oldFireFox||(window.matchMedia||(window.matchMedia=function(e){var t=e.document,n=t.documentElement,i=[],r=0,o="",s={},a=/\s*(only|not)?\s*(screen|print|[a-z\-]+)\s*(and)?\s*/i,c=/^\s*\(\s*(-[a-z]+-)?(min-|max-)?([a-z\-]+)\s*(:?\s*([0-9]+(\.[0-9]+)?|portrait|landscape)(px|em|dppx|dpcm|rem|%|in|cm|mm|ex|pt|pc|\/([0-9]+(\.[0-9]+)?))?)?\s*\)\s*$/,u=0,l=function(e){var t=-1!==e.indexOf(",")&&e.split(",")||[e],n=t.length-1,i=n,r=null,u=null,l="",f=0,d=!1,h="",p="",r=null,u=0,p=null,m="",v="",g="",y="",x="",m=!1;if(""===e)return!0;do if(r=t[i-n],d=!1,(u=r.match(a))&&(l=u[0],f=u.index),!u||-1===r.substring(0,f).indexOf("(")&&(f||!u[3]&&l!==u.input))m=!1;else{if(p=r,d="not"===u[1],f||(h=u[2],p=r.substring(l.length)),m=h===o||"all"===h||""===h,r=-1!==p.indexOf(" and ")&&p.split(" and ")||[p],u=r.length-1,m&&u>=0&&""!==p)do{if(p=r[u].match(c),!p||!s[p[3]]){m=!1;break}if(m=p[2],y=v=p[5],g=p[7],x=s[p[3]],g&&(y="px"===g?Number(v):"em"===g||"rem"===g?16*v:p[8]?(v/p[8]).toFixed(2):"dppx"===g?96*v:"dpcm"===g?.3937*v:Number(v)),m="min-"===m&&y?x>=y:"max-"===m&&y?y>=x:y?x===y:!!x,!m)break}while(u--);if(m)break}while(n--);return d?!m:m},f=function(){var t=e.innerWidth||n.clientWidth,i=e.innerHeight||n.clientHeight,r=e.screen.width,o=e.screen.height,a=e.screen.colorDepth,c=e.devicePixelRatio;s.width=t,s.height=i,s["aspect-ratio"]=(t/i).toFixed(2),s["device-width"]=r,s["device-height"]=o,s["device-aspect-ratio"]=(r/o).toFixed(2),s.color=a,s["color-index"]=Math.pow(2,a),s.orientation=i>=t?"portrait":"landscape",s.resolution=c&&96*c||e.screen.deviceXDPI||96,s["device-pixel-ratio"]=c||1},d=function(){clearTimeout(u),u=setTimeout(function(){var t=null,n=r-1,o=n,s=!1;if(n>=0){f();do if((t=i[o-n])&&((s=l(t.mql.media))&&!t.mql.matches||!s&&t.mql.matches)&&(t.mql.matches=s,t.listeners))for(var s=0,a=t.listeners.length;a>s;s++)t.listeners[s]&&t.listeners[s].call(e,t.mql);while(n--)}},10)},h=t.getElementsByTagName("head")[0],t=t.createElement("style"),p=null,m="screen print speech projection handheld tv braille embossed tty".split(" "),v=0,g=m.length,y="#mediamatchjs { position: relative; z-index: 0; }",x="",b=e.addEventListener||(x="on")&&e.attachEvent;for(t.type="text/css",t.id="mediamatchjs",h.appendChild(t),p=e.getComputedStyle&&e.getComputedStyle(t)||t.currentStyle;g>v;v++)y+="@media "+m[v]+" { #mediamatchjs { position: relative; z-index: "+v+" } }";return t.styleSheet?t.styleSheet.cssText=y:t.textContent=y,o=m[1*p.zIndex||0],h.removeChild(t),f(),b(x+"resize",d),b(x+"orientationchange",d),function(e){var t=r,n={matches:!1,media:e,addListener:function(e){i[t].listeners||(i[t].listeners=[]),e&&i[t].listeners.push(e)},removeListener:function(e){var n=i[t],r=0,o=0;if(n)for(o=n.listeners.length;o>r;r++)n.listeners[r]===e&&n.listeners.splice(r,1)}};return""===e?(n.matches=!0,n):(n.matches=l(e),r=i.push({mql:n,listeners:null}),n)}}(window)),function(e,t,n){var i=t.matchMedia;"undefined"!=typeof module&&module.exports?module.exports=n(i):"function"==typeof define&&define.amd?define(function(){return t[e]=n(i)}):t[e]=n(i)}("enquire",this,function(e){"use strict";function t(e,t){var n,i=0,r=e.length;for(i;r>i&&(n=t(e[i],i),n!==!1);i++);}function n(e){return"[object Array]"===Object.prototype.toString.apply(e)}function i(e){return"function"==typeof e}function r(e){this.options=e,!e.deferSetup&&this.setup()}function o(t,n){this.query=t,this.isUnconditional=n,this.handlers=[],this.mql=e(t);var i=this;this.listener=function(e){i.mql=e,i.assess()},this.mql.addListener(this.listener)}function s(){if(!e)throw Error("matchMedia not present, legacy browsers require a polyfill");this.queries={},this.browserIsIncapable=!e("only all").matches}return r.prototype={setup:function(){this.options.setup&&this.options.setup(),this.initialised=!0},on:function(){!this.initialised&&this.setup(),this.options.match&&this.options.match()},off:function(){this.options.unmatch&&this.options.unmatch()},destroy:function(){this.options.destroy?this.options.destroy():this.off()},equals:function(e){return this.options===e||this.options.match===e}},o.prototype={addHandler:function(e){var t=new r(e);this.handlers.push(t),this.matches()&&t.on()},removeHandler:function(e){var n=this.handlers;t(n,function(t,i){return t.equals(e)?(t.destroy(),!n.splice(i,1)):void 0})},matches:function(){return this.mql.matches||this.isUnconditional},clear:function(){t(this.handlers,function(e){e.destroy()}),this.mql.removeListener(this.listener),this.handlers.length=0},assess:function(){var e=this.matches()?"on":"off";t(this.handlers,function(t){t[e]()})}},s.prototype={register:function(e,r,s){var a=this.queries,c=s&&this.browserIsIncapable;return a[e]||(a[e]=new o(e,c)),i(r)&&(r={match:r}),n(r)||(r=[r]),t(r,function(t){a[e].addHandler(t)}),this},unregister:function(e,t){var n=this.queries[e];return n&&(t?n.removeHandler(t):(n.clear(),delete this.queries[e])),this}},new s})),$.support.pdf=function(){try{if(!Modernizr.csstransitions){var e=null;try{e=new ActiveXObject("AcroPDF.PDF")}catch(t){}return e?!0:!1}if(null!=navigator.plugins)for(var n in navigator.plugins){if("Adobe Acrobat"==n)return!0;if(navigator.plugins[n].name&&("Adobe Acrobat"==navigator.plugins[n].name||"Chrome PDF Viewer"==navigator.plugins[n].name))return!0}}catch(t){}return!0}();
var $document=$(document),$window=$(window),$body=$("body"),$htmlAndBody=$("html, body"),$noOpacity=$html.hasClass("no-opacity"),$noTransitions=$html.hasClass("no-csstransitions");$noOpacity&&$(".overlay").css("opacity",".8");var bbqState={},$page=$("#page"),$banner=$("#banner"),$titleBar=$("#title-bar"),$content=$("#content"),$gaEventLinks=$content.find("a.ga-event");$gaEventLinks.on("click",function(){var t=$(this),a=t.attr("data-event")||"Internal Link",n=t.attr("data-event-action")||"eService Click",e=t.text()||t.find("img").attr("alt");_gaq&&_gaq.push(["_trackEvent",a,n,e])});var $language=$html.attr("lang"),$englishPage=$body.attr("data-english-file")||"/",$spanishPage=$body.attr("data-spanish-file")||"/espanol/",$languageLink=$(".language");"es"===$language?$languageLink.attr("href",$englishPage):$languageLink.attr("href",$spanishPage);var $pdfLink=$content.find("a[href$='.pdf']").filter(function(){return $(this).closest("div").is(":not(.menu)")}),$pdfPlugin="&#160;<a href='http://get.adobe.com/reader/'><img alt='Get Adobe Acrobat' src='http://www.socialsecurity.gov/framework/images/icons/pdf.gif' /></a>";$pdfLink.length&&($.support.pdf||$pdfLink.eq(0).addClass("no-icon").after($pdfPlugin));var $btnTop=$(".btn-top");$btnTop.on("click",function(t){t.preventDefault(),$htmlAndBody.scrollTop(0)});var $skipNav=$("#skip-navigation");$skipNav.on("click",function(t){t.preventDefault(),$titleBar.length?$titleBar.attr("tabindex","-1").focus():$content.attr("tabindex","-1").focus()});
var $keyboard,$accessibilityCookie=$.cookie("accessibility")||"off",$accessibilityMode=$("#accessibility-mode"),$accessibilityModeState=$accessibilityMode.find("span"),strOn,strOff,accessibility={load:function(){switch($language){case"es":strOn="activado",strOff="desactivado";break;default:strOn="On",strOff="Off"}switch($accessibilityCookie){case"on":$html.addClass("accessibility-on"),$accessibilityModeState.text(strOff),$keyboard=!0,accessibility.widgets();break;default:$html.removeClass("accessibility-on"),$accessibilityModeState.text(strOn),$keyboard=!1}$accessibilityMode.on("click",function(i){i.preventDefault(),$html.hasClass("accessibility-on")?($html.removeClass("accessibility-on"),$accessibilityModeState.text(strOn),$keyboard=!1,$.cookie("accessibility","off",{path:"/"})):($html.addClass("accessibility-on"),$accessibilityModeState.text(strOff),$keyboard=!0,$.cookie("accessibility","on",{path:"/"}),accessibility.widgets())})},widgets:function(){accordion.accessibility(),tabs.accessibility()}};
var $siteBtnMenu=$("#btn-top-menu"),$siteNav=$("#nav-top-menu"),$siteNavList=$siteNav.find("> ul > li"),$siteNavLink=$siteNavList.find("> a"),$siteNavMenu=$siteNavList.find("> div"),$siteNavMenuHeaders=$siteNavList.find("a.nav-header"),$siteNavMenuLink=$siteNavMenu.find("a"),$sideBarNav=$content.find("#nav-sidebar"),$sideBarNavList=$sideBarNav.find("nav > ul > li"),$sideBarNavLinks=$sideBarNavList.find("a.sub"),$sideBarNavMenus=$sideBarNav.find("ul ul"),menus={global:function(){function e(e){27===e.keyCode&&$siteNav.find("a.on:first").focus(),$siteNavMenu.removeClass("show"),$siteNav.find("a.on").removeClass("on")}$siteBtnMenu.on("click",function(e){e.stopPropagation(),e.preventDefault();var n=$(this);n.toggleClass("on")}),$siteNavMenuHeaders.on("click",function(e){e.stopPropagation(),e.preventDefault();var n=$(this);n.toggleClass("on")}),$siteNavLink.on("click",function(e){e.stopPropagation(),e.preventDefault();var n=$(this),a=n.attr("href"),s=n.next("div");"#"!==a?window.location=a:n.hasClass("on")?(n.removeClass("on"),s.removeClass("show"),$html.removeClass("menu-open")):($siteNavLink.removeClass("on"),$siteNavMenu.removeClass("show"),n.addClass("on"),s.addClass("show"),$html.addClass("menu-open"),$keyboard&&s.find("a").filter(":first").focus())}),$siteNavMenuLink.on("keydown",function(n){27===n.keyCode&&e(n)}),$siteNavMenu.find("a:first").on("keydown",function(n){n.shiftKey&&9===n.keyCode&&e(n)}),$siteNavMenu.find("a:last").on("keydown",function(n){9===n.keyCode&&(n.shiftKey||e(n))}),$document.on("click",function(){$siteBtnMenu.removeClass("on"),$siteNavLink.removeClass("on"),$siteNavMenu.removeClass("show")})},sidebar:function(){$sideBarNavLinks.on("keydown",function(){$keyboard=!0,$sideBarNavMenus.find("a").on("keydown",function(e){if(27===e.keyCode){e.preventDefault();var n=$(this),a=n.closest("ul"),s=a.closest("li").find("a").filter(":first");s.removeClass("on").focus(),bbqState.sb="-1",$.bbq.pushState(bbqState)}})}),$sideBarNavMenus.find("a").each(function(){var e,n,a=$(this),s=a.attr("href"),i=a.parents("li:eq(1)").index(),t="";if("#"!==s&&s.indexOf("/")<=0)if(e=s.split("#"),n=e.length,n>1){for(var o=0;++o<n;)t+=e[o];t+="&sb="+i,a.attr("href",e[0]+"#"+t)}else a.attr("href",e[0]+"#sb="+i)}),$sideBarNavLinks.on("click",function(e){e.preventDefault();var n=$(this),a=n.next("ul");n.hasClass("on")?(bbqState.sb="-1",$.bbq.pushState(bbqState)):(bbqState.sb=n.parent("li").index(),$.bbq.pushState(bbqState),$keyboard&&$.doTimeout("sidebar-open",10,function(){a.find("a").filter(":first").focus()}))}),$window.on("hashchange",function(){var e=$.bbq.getState("sb");if($sideBarNavLinks.removeClass("on"),$noOpacity&&$sideBarNavMenus.hide(),e>=0){var n=$sideBarNavList.eq(e),a=n.find("a.sub"),s=a.next("ul");a.addClass("on"),$noOpacity&&s.show()}}),$window.trigger("hashchange")}};
var $accordion=$content.find("div.accordion"),$accordionLink=$accordion.find("> p a"),$accordionContent=$accordion.find("> div"),$accordionContentAnchor=$accordionContent.find("a[href^='#']").not("a[href='#']"),accordion={load:function(){accordion.generateID(),accordion.activate(),$accordion.css("visibility","visible")},accessibility:function(){$accordionContent.attr("tabindex","-1")},generateID:function(){$accordion.each(function(o){$(this).attr("id","a"+o)})},change:function(o,n){if(o>=0){var n="#"+n,t=$(n),a=t.find("> div"),i=t.find("> p a");i.removeClass("on"),a.removeClass("show"),i.eq(o).addClass("on"),a.eq(o).addClass("show"),$keyboard&&a.eq(o).focus()}},activate:function(){$accordionContentAnchor.length&&accordion.anchorLinks(),$accordionLink.attr("href","#").removeAttr("id").addClass("icon"),$accordionLink.on("click",function(o){o.preventDefault();var n=$(this),t=n.parent().next("div"),a=n.closest(".accordion"),i=a.attr("id"),c=$(this).parent().prevAll("p").length;n.hasClass("on")?(n.removeClass("on"),t.removeClass("show"),bbqState[i]="-1",$.bbq.pushState(bbqState)):(bbqState[i]=c,$.bbq.pushState(bbqState))}),$window.on("hashchange",function(){$accordion.each(function(){var o=this.id,n=$.bbq.getState(o);if(n){var t=$("> div",$(this));t.eq(n).is(":visible")||accordion.change(n,o)}})}),$window.trigger("hashchange")},anchorLinks:function(){var o=window.location.hash.replace("#","");if(o&&o.indexOf("=")<0){var n=$accordion.find("a[name="+o+"]");if(n.length){var t=n.closest("div.accordion"),a=t.attr("id"),i=n.parent().parent("div").prevAll("p").length-1;bbqState[a]=i,$.bbq.removeState(o),$.bbq.pushState(bbqState),$.doTimeout("accordion-scroll",100,function(){$htmlAndBody.scrollTop($(n).offset().top)})}}$accordionContentAnchor.on("click",function(o){o.preventDefault();var n=$(this),t=n.attr("href").replace("#",""),a=$accordion.find("a[name="+t+"]"),i=a.closest("div.accordion"),c=i.attr("id"),e=a.closest("div").prev("p"),r=i.find("> p").index(e);bbqState[c]=r,$.bbq.pushState(bbqState),$.doTimeout("accordion-scroll",100,function(){$htmlAndBody.scrollTop($(a).offset().top)})})}};
var $hTabs = $content.find("div.tabs"), $hTabsLink = $hTabs.find("> ul a"), $hTabsContent = $hTabs.find("> div"), $vTabs = $content.find("#vertical-tabs"), $vTabsLink = $vTabs.find("> ul a"), $vTabsContent = $vTabs.find("> div > div"), tabs = { accessibility: function () { $hTabsContent.attr("tabindex", "-1"), $vTabsContent.attr("tabindex", "-1") }, change: function (a, t) { "horizontal" === t ? ($hTabsLink.removeClass("onFig"), $hTabsLink.eq(a).addClass("onFig"), $hTabsContent.removeClass("showFig"), $hTabsContent.eq(a).addClass("showFig")) : ($vTabsLink.removeClass("onFig"), $vTabsLink.eq(a).addClass("onFig"), $vTabsContent.removeClass("showFig"), $vTabsContent.eq(a).addClass("showFig")) }, horizontal: function () { $hTabsLink.attr("href", "#"), $hTabsLink.on("click", function (a) { a.preventDefault(); var t = $(this), n = t.parent("li").index(); tabs.change(n, "horizontal"), bbqState.ht = n, $.bbq.pushState(bbqState), $keyboard && $hTabsContent.eq(n).focus() }), $window.on("hashchange", function () { var a = $.bbq.getState("ht", !0) || 0; tabs.change(a, "horizontal") }), $window.trigger("hashchange"), $hTabs.css("visibility", "visible") }, vertical: function () { $vTabsLink.on("click", function (a) { a.preventDefault(); var t = $(this), n = t.parent("li").index(); tabs.change(n, "vertical"), bbqState.vt = n, $.bbq.pushState(bbqState), $keyboard && $vTabsContent.eq(n).focus() }), $window.on("hashchange", function () { var a = $.bbq.getState("vt", !0) || 0; tabs.change(a, "vertical") }), $window.trigger("hashchange"), $vTabs.css("visibility", "visible") } };
var $toggle = $content.find("div.toggle-link,div.toggle-block"),
$toggleLink = $toggle.find("> a:first"),
$toggleContent = $toggle.find("> div"),
toggleLink = { load: function () {
    $toggleLink.on("click", function (t) {
        t.preventDefault(); var n = $(this); $elemContent = n.next("div"), n.toggleClass("on"), $noOpacity && $elemContent.toggleClass("show")
    })
} 
};

var $disclaimerDialog=$("#disclaimer-dialog"),$disclaimerDialogBtnOK=$disclaimerDialog.find("#btn-ok"),$disclaimerDialogBtnCancel=$disclaimerDialog.find("#btn-cancel"),$disclaimerLink=$("a.disclaimer"),disclaimer={mobile:function(){$disclaimerLink.on("click",function(i){i.preventDefault();this.href;switch($language){case"es":window.location="/espanol/agencia/disclaimer-sp.html?link="+this.href;break;default:window.location="/agency/disclaimer.html?link="+this.href}})},desktop:function(){$disclaimerDialog.on("keydown",function(i){27===i.keyCode&&($html.removeClass("dialog-open"),$page.attr("aria-hidden","false"),$("a.focus").filter(":first").focus().removeClass("focus"))}),$disclaimerLink.on("click",function(i){i.preventDefault();var e=$(this),a=this.href;e.addClass("focus"),$html.addClass("dialog-open"),$page.attr("aria-hidden","true"),$disclaimerDialogBtnOK.attr("href",a),$.doTimeout("disclaimer",100,function(){$disclaimerDialog.find("h4").focus()})}),$disclaimerDialog.find("a:first").on("keydown",function(i){i.shiftKey&&9===i.keyCode&&i.preventDefault()}).end().find("a:last").on("keydown",function(i){9===i.keyCode&&(i.shiftKey||i.preventDefault())})}};$disclaimerDialogBtnCancel.on("click",function(i){i.preventDefault(),$html.removeClass("dialog-open"),$page.attr("aria-hidden","false"),$("a.focus").filter(":first").focus().removeClass("focus")});
var $utilityBar=$("#utility-bar"),$utilityBarTopList=$utilityBar.find("> ul > li"),$utilityBarTopListLinks=$utilityBar.find("> ul > li > a"),$utilityBarMenu=$utilityBar.find(".menu"),$utilityBarMenuLinks=$utilityBarMenu.find("a"),utilityBar={accessibility:function(){function i(i,t){27===i.keyCode&&t.focus(),$utilityBar.removeClass("on"),$utilityBarTopList.removeClass("open")}$utilityBarMenuLinks.on("keydown",function(t){if(27===t.keyCode){var e=$(this),a=e.closest("li").find("a").filter(":first");i(t,a)}}),$utilityBarMenu.find("a:first").on("keydown",function(t){t.shiftKey&&9===t.keyCode&&i(t)}),$utilityBarMenu.find("a:last").on("keydown",function(t){9===t.keyCode&&(t.shiftKey||i(t))})},load:function(){$utilityBarTopListLinks.on("click",function(i){i.preventDefault();var t=$(this);$elemParent=t.closest("li"),$elemMenu=t.attr("data-menu"),$elemParent.hasClass("open")?($utilityBar.removeClass("on"),$elemParent.removeClass("open")):($utilityBarTopList.removeClass("open"),$utilityBar.addClass("on"),$elemParent.addClass("open"),$html.hasClass("accessibility-on")&&(utilityBar.accessibility(),$("#menu-"+$elemMenu).find("a").filter(":first").focus()))})}},$govDeliveryTopic=$body.attr("data-gov-delivery")||!1,$govDelivery=$utilityBar.find("#menu-gov-delivery"),govDelivery={load:function(){var i=$govDelivery.find("a"),t="https://public.govdelivery.com/accounts/USSSA/subscriber/new?topic_id="+$govDeliveryTopic,e="https://public.govdelivery.com/accounts/USSSAESP/subscriber/new?topic_id="+$govDeliveryTopic;switch($html.addClass("has-gov-delivery"),$language){case"es":i.attr("href",e);break;default:i.attr("href",t)}}};
var $definitionPanel=$("#definition-panel"),$definitionPanelClose=$("<a href='#'>X</a>"),$definitionLink=$content.find("a.definition"),definitionPanel={load:function(){"es"===$language?$definitionPanelClose.attr("title","Cerrar panel de definiciones"):$definitionPanelClose.attr("title","Close Definition Panel");var n={load:function(n){$.ajax({context:$definitionPanel,url:n,success:function(n){$html.addClass("definition-open");var i;i=$(n).find("dl").html(),$definitionPanel.html(i).wrapInner("<dl></dl>"),$definitionPanel.find("dt").append($definitionPanelClose),$definitionPanel.find("dt").find("a").on("click",function(n){n.preventDefault(),$html.removeClass("definition-open"),$definitionPanel.empty(),$keyboard&&$("a.focus").filter(":first").focus().removeClass("focus")}),$keyboard&&$definitionPanel.find("dt").attr("tabindex","-1").focus()}})}};$definitionPanel.on("keydown",function(n){27===n.keyCode&&($html.removeClass("definition-open"),$definitionPanel.empty(),$("a.focus, button.focus").filter(":first").focus().removeClass("focus"))}),$definitionLink.attr("aria-haspopup","true").on("click",function(i){i.preventDefault();var e=$(this),t=e.attr("href"),o=e.offset().left-10,a=e.offset().top+30;$keyboard&&e.addClass("focus"),$definitionPanel.css("top",a).css("left",o),$definitionPanel.empty(),n.load(t)}),$document.on("click",function(){$html.removeClass("definition-open")})}};
function mediaQueryWidgets(e){"desktop"===e?($siteNavMenuHeaders.attr("tabindex","-1"),$disclaimerLink.off(),disclaimer.desktop()):($siteNavMenuHeaders.attr("tabindex","0"),$disclaimerLink.off(),disclaimer.mobile())}var queryPhone="(max-width: 37.5em)",queryTablet="(min-width: 37.5em) and (max-width: 50em)",queryDesktop="(min-width: 50em)";oldFireFox?mediaQueryWidgets("desktop"):(enquire.register(queryDesktop,{match:function(){mediaQueryWidgets("desktop")}},!0),enquire.register(queryTablet,{match:function(){mediaQueryWidgets("tablet")}},!0),enquire.register(queryPhone,{match:function(){mediaQueryWidgets("phone")}},!0));
window.jQuery&&(accessibility.load(),$siteNav&&menus.global(),$sideBarNav.hasClass("menu")&&menus.sidebar(),utilityBar.load(),$accordion.length&&accordion.load(),$definitionLink.length&&definitionPanel.load(),$toggle.length&&toggleLink.load(),$hTabs.length&&tabs.horizontal(),$vTabs.length&&tabs.vertical(),$govDeliveryTopic&&govDelivery.load());
$html.removeClass("not-visible");