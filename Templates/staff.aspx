<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="staff.aspx.cs" Inherits="Templates_Content" %>
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

	   
	  <table class="table" id="library_staff_table">
	  <caption><h3>Library Staff</h3></caption>
	  
	  <thead>
	    <tr>
			<th id="header1" scope="col"></th>
			<th id="header2" scope="col">Name</th>
			<th id="header3" scope="col" style="width:33%;">Title/Role</th>
			<th id="header4" scope="col">Location</th>
			
		</tr>
	  </thead>
	  
	  <tbody>
	    <%=ouptput %>
		 
		 
		 
		 
		 
	  </tbody>
	  </table>
	   

        
	  
	  
	  
 
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
