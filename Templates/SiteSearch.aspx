<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    CodeFile="SiteSearch.aspx.cs" Inherits="Templates_Content" %>

<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux" TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
    <ux:PageTitle ID="uxPageTitle" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
    <ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
	<script> $('#searchDropdownBox').val('This Site');  </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">
    <cms:ContentBlock ID="mainContent" DynamicParameter="id" runat="server" />
    <div class="ssadl-int-search form" id="ssadl-site-search">
    <label for="site_search_field">
        <span>Search</span></label>
    <input type="search" id="site_search_field" class="int_search_field" value="<%=searchWord %>"  onkeypress="return gotoSearch2(event)" >
    
    <input type="button" class="submit"  id="int_search_button" value="Search" onclick="gotoSearch();"  />
    </div>
    <div class="results_content" id="pagerupper" runat="server">
        <p class="float-left">
            Results <asp:Label runat="server" ID="idResultsLabel" ></asp:Label></p>
        <p class="float-right">
            <label for="show_results" class="inline-label">
                Show:</label>
   
            <asp:DropDownList runat="server" AutoPostBack="true" ID="show_results" OnSelectedIndexChanged="Index_Changed" >
            <asp:ListItem Value="10">10</asp:ListItem>
             <asp:ListItem Value="25">25</asp:ListItem>
              <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="100">100</asp:ListItem>
                
            </asp:DropDownList>
        </p>
    </div>
    <asp:ListView ID="SearchResultLV" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="SearchResult_ItemDatabound">
        <EmptyDataTemplate>
            <h4>
                Search criteria did not match any records.
            </h4>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <p>
                <asp:HyperLink runat="server" ID="searchTitleLink"></asp:HyperLink>
                <asp:Literal runat="server" ID="myFav"></asp:Literal><br />
                <asp:Literal runat="server" ID="SearchTeaser"></asp:Literal>
                <%-- <cite>URL: <asp:Literal runat="server" ID="searchLink" ></asp:Literal>  </cite>--%>
            </p>
        </ItemTemplate>
    </asp:ListView>
    <div class="results_pagination">

        <asp:DataPager ID="DataPager1" PageSize="10" runat="server" PagedControlID="SearchResultLV"
            OnPreRender="Datapager_prender">
            <Fields>
                <asp:TemplatePagerField>
                    <PagerTemplate>
                        <div class="float-left">
                            <span class="pager_buttons" id="pager_prev">
                    </PagerTemplate>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField PreviousPageText="Prev" ButtonType="Link" ShowLastPageButton="false"
                    ShowNextPageButton="false" ButtonCssClass="btn" />
                <asp:TemplatePagerField>
                    <PagerTemplate>
                        </span> <span id="pager_next" class="pager_buttons">
                    </PagerTemplate>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField NextPageText="Next" ButtonType="Link" ShowFirstPageButton="false"
                    ShowNextPageButton="true" ButtonCssClass="btn" ShowPreviousPageButton="false" />
                <asp:TemplatePagerField>
                    <PagerTemplate>
                        </span> </div>
                       
                    </PagerTemplate>
                </asp:TemplatePagerField>
              
            </Fields>
        </asp:DataPager>
 

                          <div class="float-right" id="pagerbelow" runat="server">
                            <p>
                                <label for="pager_textbox" class="inline-label">
                                    Go to page</label>
                                <asp:TextBox runat="server" ID="pager_textbox" Width="40"  ></asp:TextBox>
                                 
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:Button runat="server" ID="goBtn" onclick="goBtnClick" CssClass="btn" Text="Go" />
                            </p>
                        </div>
    </div>
    <!--results_pagination-->
    <div class="clearfix">
    </div>
    <div class="container-blue search-message">
        <img src="framework/images/important-icon.png" alt="Important message">
      <p>            Not seeing what you need? 
    <a href="/searchsummon.aspx?s=<%=searchString %>">Search all library resources.</a>
</p>
    </div>
    <!-- END COLUMN -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
