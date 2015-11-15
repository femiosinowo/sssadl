<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="emailReport.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->
<div id="title-bar"><h2>Email a Report</h2></div>

	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">

      <h3>Email "<%=ReportName%>" Report</h3>
	  <asp:Panel runat="server" ID="InvalidEmailPanel" Visible="false" >
      <div class="container-red">
	     <h4>These emails are not valid emails</h4>
		
	  <%=invalidEmailsFound %>
	  
	  </div>
      
      </asp:Panel>
	  
	  
	 
	   <p>
      <label for="ctl00_mainContentCP_emails" class="bold">Email addresses: <br /> (Seperate multiple addresses with commas.)</label> <br/>
      <asp:TextBox runat="server" ID="emails" TextMode="MultiLine" ></asp:TextBox>
      </p>

 
	  
       
	   
	   <p>
      

      </p>

       <div class="buttons">
        <asp:LinkButton CssClass="btn" runat="server" ID="Send" Text="Send" 
               onclick="Send_Click" /> 
	  	<a href="#" class="btn">Cancel</a>
	  	 
        
	  </div>

      <p>
      

      </p><p>
      

      </p>
        
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

