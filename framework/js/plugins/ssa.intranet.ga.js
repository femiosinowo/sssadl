/* E-NOR'S GOOGLE ANALYTICS SCRIPTS (INTRANET VERSION) */
hostname = document.location.hostname;
hostname = hostname.match(/(([^.\/]+\.[^.\/]{2,3}\.[^.\/]{2})|(([^.\/]+\.)[^.\/]{2,4}))(\/.*)?$/)[1];
hostname = hostname.toLowerCase() ;
var 
_gaq = _gaq || [];
_gaq.push(['_setAccount', 'UA-25977386-20']);
/* _gaq.push(['_setDomainName', hostname]); */
_gaq.push(['_setDomainName', 'none']);
_gaq.push(['_setAllowLinker', true]);
_gaq.push(['_trackPageview']);
(function() {
var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();