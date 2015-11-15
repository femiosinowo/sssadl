<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UtilityNav.ascx.cs" Inherits="UserControls_UtilityNav" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
 

<div class="bg-dark-gray accessibility">
    <div class="content-wrapper no-padding">
        <a class="accessibility-mode" id="accessibility-mode" href="#">Accessibility Mode <span>Off</span></a>
        <a class="skip-navigation" id="skip-navigation" href="#home_search_box_header">Skip to content</a>
    </div>
</div>
<!-- RESOURCES BLOCK -->
<div class="block-resources hide-phone">
    <div class="content-wrapper no-padding">
        <div class="bg-dark-gray">
            <!--<a class="icon icon-wheelchair" href="/accessibility/"><svg><use xlink:href="#icon-wheelchair"/></svg> Accessibility</a> &#183; -->
           <a id="MyResourceslink" href="/resources/myresources.aspx" title="My Resources(<%=myResourcesCount %>)">My Resources (<span id="myResourcesCount"><%=myResourcesCount %></span>)</a>  
                        <a href="/about/faqs.aspx" title="Frequently Asked Questions">FAQs</a>   
                        <a href="/about/contact.aspx" title="Contact">Contact</a>
                        <asp:Panel runat="server" ID="adminPanelLinks" Visible="false" >
                        <style>
                        .bg-dark-gray { width: 374px;}
                        </style>
                         <a href="http://digitallibraryadmin.ba.ad.ssa.gov/admin/default.aspx" title="Admin Area">Admin Site</a>   
                        <a target="_blank" href="http://digitallibraryadmin.ba.ad.ssa.gov/WorkArea/workarea.aspx?page=content.aspx&action=ViewContentByCategory&folder_id=0&LangType=1033" title="WCMS">WCMS</a>
                        </asp:Panel>
        </div>
    </div>
</div>

<header class="banner" id="banner" role="banner">
    <div class="grid">
        <div class="row-12">
            <div class="column-6 logo" id="logo" title="Social Security Logo">
                <a href="/default.aspx">
                    <asp:Literal ID="ltrLogoText" runat="server"></asp:Literal>  
                       <cms:ContentBlock runat="server" DefaultContentID="34" />               
                </a>
            </div>
            <!-- END COLUMN -->
            <div class="column-6 align-right hide-phone">
                <div class="search-box" id="search-box" role="search">
                    <input type="hidden" name="affiliate" value="ssa" />
                    <label class="hide" for="q">Search Terms</label>
                    <input class="usagov-search-autocomplete" type="search" value="<%=searchWord %>" name="query" onkeypress="return gotoTopSearch(event)" id="q" autocomplete="off" placeholder="Search..." alt="Search" />
                    <span class=" nav-facade-active" id="nav-search-in">
                        <span class="nav-down-arrow nav-sprite"></span>
                        <select title="Search menu" class="searchSelect" id="searchDropdownBox" name="search_category">
                            <option value="This Site" title="This Site">This Site</option>
                            <option value="Library Resources" title="Library Resources" selected="selected">Library Resources</option>
                            <option value="SSA Reports" title="SSA Reports">SSA Reports</option>
                        </select>
                    </span>
                </div>
            </div>
            <!-- END COLUMN -->
        </div>
        <!-- END ROW -->
    </div>
    <!-- END WRAPPER -->
</header>


