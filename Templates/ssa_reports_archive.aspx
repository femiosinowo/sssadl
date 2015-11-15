<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="ssa_reports_archive.aspx.cs" Inherits="Templates_Content" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>


 <asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
 <ux:PageTitle ID="uxPageTitle"  runat="server" />
 
 <script> $('#searchDropdownBox').val('SSA Reports');  </script>
</asp:Content>

 <asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
<ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">

  <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />

  <asp:Panel runat="server" ID="simpleSearch" >
    <div class="ssadl-int-search form" id="ssadl-site-search">
    <label for="site_search_field">
        <span>Search</span></label>
    <input type="search" id="site_ssareportsearch_field" class="int_search_field" value="<%=searchWord %>"  onkeypress="return gotoSSAReportsSearch2(event)" />
    
    <input type="button" class="submit"  id="int_search_button" value="Search" onclick="gotoSSAReportsSearch();"  />
 
    
            	 <a href="?show=advance">Advanced Search</a> 
    </div>

</asp:Panel>
<asp:Panel runat="server" ID="AdvanceSearch" Visible="false" >


	  <h3>Advanced Search | <a href="?show=simple" id="simple_search_link">return to simple search</a></h3>
	  
	  <p>You must fill out at least one of these fields to use advanced search.  If you fill out more than one field, we will use the fields together (an "AND" search). For example, if you fill out an Author and a Year Published, we'll show the titles published by the author during that year, only.</p>
	 
	 <form class="form">
	 	<label for="archive_keyword">Keyword</label>
		 
		       <asp:TextBox runat="server" ID="KeywordTxt" ></asp:TextBox>
		<label for="archive_author">Author</label>
		 
		       <asp:TextBox runat="server" ID="AuthorTxt" ></asp:TextBox>
		<label for="archive_title">Title</label>
 
		       <asp:TextBox runat="server" ID="TitleTxt" ></asp:TextBox>
		<label for="archive_year_pub">Year Published</label>
	 
		       <asp:TextBox runat="server" ID="YearTxt" ></asp:TextBox>
		<label for="archive_subject">Subject</label>
 
        <asp:TextBox runat="server" ID="SubjectTxt" ></asp:TextBox>
		<br/>
		<br/>
 
        <asp:Button runat="server" ID="SearchAdvance" Text="Search" CssClass="btn" 
            onclick="SearchAdvance_Click" />

    </asp:Panel>



    <div class="results_content" runat="server" id="pagertop" visible="false">
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
                <asp:HyperLink runat="server" ID="TitleLink"></asp:HyperLink>
                <asp:Literal runat="server" ID="myFav"></asp:Literal> 
                <asp:Literal runat="server" ID="Teaser"></asp:Literal>
                <%-- <cite>URL: <asp:Literal runat="server" ID="searchLink" ></asp:Literal>  </cite>--%>
            </p>
        </ItemTemplate>
    </asp:ListView>
    <div class="results_pagination">

        <asp:DataPager ID="DataPager1" PageSize="10" runat="server" PagedControlID="SearchResultLV"
            OnPreRender="Datapager_prender" >
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
 

                          <div class="float-right" runat="server" id="pagerbelow" visible="false">
                            <p>
                                <label for="pager_textbox" class="inline-label">
                                    Go to page</label>
                                <asp:TextBox runat="server" ID="pager_textbox" Width="40" Visible="false"  ></asp:TextBox>
                                  <asp:DropDownList runat="server" ID="pager_dropdown" AutoPostBack="true"  Width="55" OnSelectedIndexChanged="pager_dropdown_SelectedIndexChanged" ></asp:DropDownList>
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:Button runat="server" ID="goBtn" onclick="goBtnClick" CssClass="btn" Visible="false" Text="Go" />
                            </p>
                        </div>
    </div>
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
