<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="scheduledreport.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
<script>


    $(document).ready(function () {
     $("#ctl00_mainContentCP_startDate").datepicker();
      
     
});

function confirmDelete() {

    if (confirm('Are you sure you want to remove <%=ReportNameData %>')) {
        // do things if OK
        location.href = '/admin/reports/scheduledreportdelete.aspx?deleteReportID=<%=ScheduleReportID %>';
    }
}
 </script>
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
	  <asp:Panel runat="server" ID="InvalidEmailPanel" Visible="false" >
      <div class="container-red">
	     <h4>These emails are not valid emails</h4>
		
	  <%=invalidEmailsFound %>
	  
	  </div>
      
      </asp:Panel>
	  <p>
      <label for="ctl00_mainContentCP_ReportName" class="bold"> Report Name*</label> <br/>
      <asp:TextBox runat="server" ID="ReportName" CssClass="validate[required]" ></asp:TextBox>
      </p>
	  
	 
	   <p>
      <label for="ctl00_mainContentCP_emails" >Notify these email addresses: <br /> (Seperate multiple addresses with commas.)</label> <br/>
      <asp:TextBox runat="server" ID="emails" TextMode="MultiLine" ></asp:TextBox>
      </p>

	    <p>
      <label for="ctl00_mainContentCP_frequency"  >Notify these email addresses: <br /> (Seperate multiple addresses with commas.)</label> <br/>
      <asp:DropDownList runat="server" ID="frequency" >
       <asp:ListItem >Daily</asp:ListItem>
      <asp:ListItem >Weekly</asp:ListItem>
       <asp:ListItem >Bi-Weekly</asp:ListItem>
        <asp:ListItem   >Monthly</asp:ListItem>
         <asp:ListItem >Quarterly</asp:ListItem>
         <asp:ListItem >Annually</asp:ListItem>
     
      </asp:DropDownList>
      </p>
	  

        <p>
      <label for="ctl00_mainContentCP_startDate" class="bold" >Start Date*: </label> <br/>
      <asp:TextBox runat="server" ID="startDate" CssClass="validate[required]" Width="150"  ></asp:TextBox>
      </p>
	   
	   <p>
      

      </p>

       <div class="buttons">
        <asp:LinkButton CssClass="btn" runat="server" ID="Save" Text="Save" Visible="false"
               onclick="Save_Click" /> 
                <asp:LinkButton CssClass="btn" runat="server" ID="Send" Text="Send"  Visible="false"
               onclick="Send_Click" /> 

	  	<a href="/admin/reports/resources/" class="btn">Cancel</a>
	  	 <a runat="server" id="showRemove" visible="false"  onclick="javascript:confirmDelete();" href='#' class="float-right"  >Remove This Report From the Schedule</a>
         
	  </div>

      <p>
      

      </p><p>
      

      </p>
         <ux:AuditLog runat="server" ID="AuditLogUX" />
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

