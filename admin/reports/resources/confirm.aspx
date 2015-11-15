<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="confirm.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->
<div id="title-bar"><h2>Scheduled Report</h2></div>

	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
      <div class="container-blue">
	     <h4>Report schedule created/saved successfully</h4>
		
	 
	  
	  </div>
      
     
	 
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
	  <!-- END GRID -->
	  
	  </div>
	  <!-- END PAGE CONTENT -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

