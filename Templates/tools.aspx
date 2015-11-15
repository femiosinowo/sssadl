<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="tools.aspx.cs" Inherits="Templates_Content" %>
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

  <%=SADescription %>
  <hr />
	  <!-- END COLUMN -->
	  <asp:ListView     ID="DL_Toollist"   runat="server" OnItemDataBound="DL_Toollist_ItemDatabound" >
               
 <EmptyDataTemplate>
 <table class="table table-bordered table-striped database-table">
				    
					<thead tabindex="0" role="button">
						<tr>
						   <th>No Tools found</th>
						</tr>	
					 </thead>
                     </table>
 </EmptyDataTemplate>
               <ItemTemplate>
  


          <p>
           <asp:HyperLink runat="server" CssClass="post"  ID="toolTitle" ></asp:HyperLink>
          
          <%--<a href="#" class="icon icon-book ga-event"><span class="favorite-id">AMA ePhysician Profile</span>
	  <svg>
		<use xlink:href="#icon-book">
	  </svg></a>--%>
<asp:Literal runat="server" ID="myfavIcon" ></asp:Literal>
	  <br/>
	  <asp:Literal runat="server" ID="Description" ></asp:Literal>
       
   
      </p> 

   
                  <!--end multimedia_block -->


 


               </ItemTemplate>
               </asp:ListView>
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
