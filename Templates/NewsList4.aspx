<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="NewsList4.aspx.cs" Inherits="Templates_Default4" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryPageTitle" Runat="Server">
<ux:PageTitle ID="uxPageTitle"  runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSecondaryBreadcrumb" Runat="Server">
<ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryMainContent" Runat="Server">
<CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />

 <h3>News Archives for 2014</h3>
	  
	  <p><label for="news_archive_year" class="inline-label">View Archives for:</label>
	  <select id="news_archive_year">
		  <option>- Select a year -</option>
		  <option>2014</option>
		  <option>2013</option>
		  <option>2012</option>
	  </select>
      
      
   

  
	  
	 
      <asp:Button runat="server" CssClass="btn" Text="Go" />
	  </p>


 <ul class="divider no-margin">
      <asp:ListView     ID="DL_newslist"   runat="server" OnItemDataBound="DL_newslist_ItemDatabound" >
               
 
               <ItemTemplate>
 <li>
			<div class="byline"> <asp:Literal runat="server" ID="NewsDate" ></asp:Literal></div>
			 
            <asp:HyperLink runat="server" CssClass="post"  ID="NewsTitle" ></asp:HyperLink>
		</li>

   
                  <!--end multimedia_block -->


 


               </ItemTemplate>
               </asp:ListView>
	   
        </ul>
         <asp:DataPager ID="DataPager1"  PageSize="2" runat="server" PagedControlID="DL_newslist" OnPreRender="Datapager_prender">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="false" ShowNextPageButton="false"
                                    ButtonCssClass="arrow prev" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowNextPageButton="true"
                                    ButtonCssClass="arrow next" ShowPreviousPageButton="false" />
                            </Fields>
                        </asp:DataPager>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphSideContent" Runat="Server">
<ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryJSCode" Runat="Server">
</asp:Content>

