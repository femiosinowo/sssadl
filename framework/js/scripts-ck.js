/*Search*/$(document).ready(function () { $("#nav-search-in-content").text("Library Resources"); $("#searchDropdownBox").change(function () { var e = $(this).val(); $("#nav-search-in-content").text(e) }) }); $(document).ready(function () { var e = $(".slidingDiv"), t = $(".show_text"), n = $(".hide_text"); $(".show_text").click(function (n) { n.preventDefault(); if (e.hasClass("hide")) { t.addClass("hide"); e.removeClass("hide"); e.addClass("show_div") } }); $(".hide_text").click(function (n) { n.preventDefault(); if (e.hasClass("show_div")) { t.removeClass("hide"); e.removeClass("show_div"); e.addClass("hide") } }) }); $(document).ready(function () { $(".show_desc_info_content").addClass("hide"); $(".show_desc_info").click(function (e) { e.preventDefault(); var t = $(this), n = t.parent().find(".show_desc_info_content"); if (n.hasClass("hide")) { t.html("> hide information"); n.removeClass("hide") } else { t.html("show information >"); n.addClass("hide") } }); var e = $(".show_desc_info_content"); $(".show_all_desc").click(function (t) { t.preventDefault(); $(".show_desc_info").html("> hide information"); e.removeClass("hide") }); $(".hide_all_desc").click(function (t) { t.preventDefault(); $(".show_desc_info").html("show information >"); e.addClass("hide") }) }); $(document).ready(function () { var e = $("#pager-alpha .nav a"); e.click(function (e) { e.preventDefault(); $(".page").removeClass("hide"); if (this.id == "all") { $(".show_desc_info_content").addClass("hide"); $(".show_desc_info").html("show information >") } else { $(".page").not(".database-" + this.id).addClass("hide"); $(".show_desc_info_content").removeClass("hide"); $(".show_desc_info").html("> hide information") } }) }); $(document).ready(function () { $(".login_info").addClass("hide"); $(".login_text").click(function (e) { e.preventDefault(); $(".login_text").removeClass("login_selected"); $(".login_info").addClass("hide"); $(".login_info").removeClass("show_login_info"); $(this).next(".login_info").addClass("show_login_info"); $(this).next(".login_info").removeClass("hide"); $(this).addClass("login_selected") }) }); $(document).ready(function () { $(".show_all_faq").click(function () { $("div.accordion a.icon").addClass("on"); $("div.accordion div").addClass("show") }); $(".hide_all_faq").click(function () { $("div.accordion a.icon").removeClass("on"); $("div.accordion div").removeClass("show") }) });
$(document).ready(function () {
    $(".someone_else_request").addClass("hide");
    $(".someone_else_request").find("input,select,textarea,button").attr("tabindex", "-1"); 
    $("input[value='someone_else']").change(function () {
        $(".someone_else_request").removeClass("hide");
        $(".someone_else_request").find("input,select,textarea,button").attr("tabindex", "0")
    });
    $("input[value='me']").change(function () {
        $(".someone_else_request").addClass("hide"); 
    $(".someone_else_request").find("input,select,textarea,button").attr("tabindex", "-1") })
});
$(document).ready(function () {
    var e = $("#pager-alpha"), t = $(".nav", e), n = $(".page", e), r = window.location.hash; $("a[name]", e).each(function () { $(this).attr("id", $(this).attr("name")) });
    if (r) { var i = $(r), s = i.closest("div.page"), o = s.index() - 1; s.show(); $("a", t).eq(o).addClass("active") } $("a", t).on("click", function (r) {
        r.preventDefault();
        var i = $(this).attr("href").replace("#", ""); $("a", t).removeAttr("class"); n.hide(); $(this).addClass("active"); $(".page:eq(" + i + ")", e).show();
        $(".page:visible a:first", e).focus()
    })
}); $(function () {
    $.fn.toggleAttr = function (e, t, n) { return this.each(function () { var r = $(this); r.attr(e) == t ? r.attr(e, n) : r.attr(e, t) }) };
   var e = $(".database-table"); e.find("thead tr").show(); e.find("tbody").show(); e.find("thead").click(function () { $(this).siblings().toggleClass("hide"); $(this).find("th").toggleClass("table_hide") }); e.find("thead").bind("keydown", function (e) { if (e.keyCode == 13) { e.stopPropagation(), e.preventDefault(); $(this).siblings().toggleClass("hide"); $(this).find("th").toggleClass("table_hide"); $(this).siblings().find("tr td a").toggleAttr("tabindex", "-1", "0") } }) }); $(document).ready(function () { $(".favorite-id").each(function (e) { var t = $(this).text(); $(".favorite-id-title").each(function (n) { e == n && $(this).html(t) }) }); var e = $(".favorite"); e.bind("keydown", function (e) { if (e.keyCode == 13) { e.stopPropagation(), e.preventDefault(); $(this).toggleClass("favorite_on"); var t = $(this).find(".favorite-id-title").text(); $(this).hasClass("favorite_on") ? $(this).attr("title", "Remove " + t + " from My Resources") : $(this).attr("title", "Add " + t + " to My Resources") } e.keyCode == 27 }); e.click(function (e) { e.stopPropagation(), e.preventDefault(); $(this).toggleClass("favorite_on"); var t = $(this).find(".favorite-id-title").text(); $(this).hasClass("favorite_on") ? $(this).attr("title", "Remove " + t + " from My Resources") : $(this).attr("title", "Add " + t + " to My Resources") }); $(".favorite").each(function (e) { $(this).hasClass("favorite_on") ? $(this).prepend("Remove") : $(this).prepend("Add") }) }); $(function () {
     $(".contact_option").addClass("hide"); 
    $("#option8").removeClass("hide"); $(".contact_option").find("input,select,textarea,button").attr("tabindex", "-1"); $("#option8").find("input,select,textarea,button").attr("tabindex", "0"); $("#contact_subject").change(function () {
        $(".contact_option").addClass("hide"); $("#" + $(this).val()).removeClass("hide"); $(".contact_option").find("input,select,textarea,button").attr("tabindex", "-1");
$("#"+$(this).val()).find("input,select,textarea,button").attr("tabindex","0")})});