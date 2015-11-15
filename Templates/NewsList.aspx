<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="NewsList.aspx.cs" Inherits="Templates_Content" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>


 <asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
     <ux:PageTitle ID="uxPageTitle"  runat="server" />
</asp:Content>

 <asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
     <ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">

  <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />

	   	   
          <asp:Literal runat="server" ID="archiveH3" ></asp:Literal>
	  
	  <p><label for="news_archive_year" class="inline-label">View Archives for:</label>
 
      <asp:DropDownList runat="server" Width="180" ID="yearArchieve" >
      
      </asp:DropDownList>
      
   

   <asp:Button ID="searchYearArchieves" runat="server" CssClass="btn" Text="Go" onclick="searchYearArchieves_Click" 
                />
	  
	  
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
 
	 
	      <div class="results_pagination" id="pagerDiv" runat="server">

        <asp:DataPager ID="DataPager1" PageSize="10" runat="server" PagedControlID="DL_newslist"
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
 

                        
    </div>
 
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
