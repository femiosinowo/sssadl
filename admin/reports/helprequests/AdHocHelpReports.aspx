<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="AdHocHelpReports.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 <script>

     $(document).ready(function () {


         $("#ctl00_mainContentCP_CustomDatesSubmittedDate_EndDate").datepicker();
         $("#ctl00_mainContentCP_CustomDatesSubmittedDate_StartDate").datepicker();

         $("#ctl00_mainContentCP_CustomDatesClosedDate_EndDate").datepicker();
         $("#ctl00_mainContentCP_CustomDatesClosedDate_StartDate").datepicker();


         $('#ctl00_mainContentCP_limitClosedDate').change(function () {

             if ($(this).is(':checked')) {
                 $("#ctl00_mainContentCP_RequestClosedDateDD").removeAttr('disabled');
             } else {
                 $("#ctl00_mainContentCP_RequestClosedDateDD").attr('disabled', true);
             }



         });


         $('#ctl00_mainContentCP_LimitSubmittedDateRange').change(function () {

             if ($(this).is(':checked')) {
                 $("#ctl00_mainContentCP_RequestSubmittedDateDD").removeAttr('disabled');
             } else {
                 $("#ctl00_mainContentCP_RequestSubmittedDateDD").attr('disabled', true);
             }



         });

        


         $('#ctl00_mainContentCP_RequestSubmittedDateDD').change(function () {

             var selectedValue = $(this).val();

             switch (selectedValue) {
                 case "Custom Dates":

                     $("#CustomDatesSubmittedDate").show();
                     break;
                 default:
                     $("#CustomDatesSubmittedDate").hide();
                     break;

             }



         });



         $('#ctl00_mainContentCP_RequestClosedDateDD').change(function () {

             var selectedValue = $(this).val();

             switch (selectedValue) {
                 case "Custom Dates":

                     $("#CustomDatesClosedDate").show();
                     break;
                 default:
                     $("#CustomDatesClosedDate").hide();
                     break;

             }



         });


     });


 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Ad Hoc Help Reports</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->


	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	   
	     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
          <li> Reports</li>
         <li> Ad Hoc Help Reports</li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
        <h3>Ad Hoc Help Reports</h3>
	  <asp:Panel runat="server" ID="InvalidEmailPanel" Visible="false" >
      <div class="container-red">
	     
	  
	  
	  </div>
      
      </asp:Panel>

      <p>
       <label for="ctl00_mainContentCP_ReportTypeDD"  > Help Request Form</label> <br/>
        <asp:ListBox runat="server" ID="RequestType"  DataTextField="Form" DataValueField="ID" SelectionMode="Multiple" CssClass="validate[required]" Height="120"   ToolTip="Select One or more Resource(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_RequestType')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_RequestType')">Select None</a>
    
      </p>


	  <p>
      <label for="ctl00_mainContentCP_RequestStatusDD"  > Request Status</label> <br/>
      
      <asp:DropDownList runat="server" ID="RequestStatusDD"   >
       <asp:ListItem Value="Any" >Any</asp:ListItem>
       <asp:ListItem Value="New" >New</asp:ListItem>
        <asp:ListItem Value="Open" >Open</asp:ListItem>
         <asp:ListItem Value="New or Open" >New or Open</asp:ListItem>
          <asp:ListItem Value="Closed" >Closed</asp:ListItem>
      </asp:DropDownList>
      </p>
	  


      <p>
      <label for="ctl00_mainContentCP_RequestSubmittedDateDD"  > Request Submitted Date</label> <br/>
      <asp:CheckBox runat="server" ID="LimitSubmittedDateRange"   />
      <asp:DropDownList runat="server" ID="RequestSubmittedDateDD"   >
       <asp:ListItem Value="Today" >Today</asp:ListItem>
       <asp:ListItem Value="This Week" >This Week</asp:ListItem>
        <asp:ListItem Value="Last Week" >Last Week</asp:ListItem>
         <asp:ListItem   Value="Last 30 Days" >Last 30 Days</asp:ListItem>
          <asp:ListItem Value="This Month" >This Month</asp:ListItem>


          <asp:ListItem Value="Last Month" >Last Month</asp:ListItem>
          <asp:ListItem Value="This Quarter" >This Quarter</asp:ListItem>
          <asp:ListItem Value="Last Quarter" >Last Quarter</asp:ListItem>
          <asp:ListItem Value="This Fiscal Year" >This Fiscal Year</asp:ListItem>
          <asp:ListItem Value="Last Fiscal Year" >Last Fiscal Year</asp:ListItem>
          <asp:ListItem Value="Custom Dates" >Custom Dates</asp:ListItem>
      </asp:DropDownList>
      </p>
      <div id="CustomDatesSubmittedDate" <%=customDatSDDisplay %> >
      
      <p>
 
<div class="date1">
Starts:  
<asp:TextBox runat="server" CssClass="validate[required,custom[date]]" ID="CustomDatesSubmittedDate_StartDate" Width="150" ></asp:TextBox>
</div>

<div class="date2">
Ends:   
<asp:TextBox runat="server" CssClass="validate[required,custom[date]]" ID="CustomDatesSubmittedDate_EndDate" Width="150" ></asp:TextBox>
</div>

</p>
      </div>

	 


 <p>
      <label for="ctl00_mainContentCP_RequestClosedDateDD">Request Closed Date  <i>(This does not apply to new or open requests.)</i></label> <br/>
      <asp:CheckBox runat="server" ID="limitClosedDate"   />
       <asp:DropDownList runat="server" Enabled="false" ID="RequestClosedDateDD" >
  <asp:ListItem Value="Today" >Today</asp:ListItem>
    <asp:ListItem Value="Yesterday" >Yesterday</asp:ListItem>
       <asp:ListItem Value="This Week" >This Week</asp:ListItem>
        <asp:ListItem Value="Last Week" >Last Week</asp:ListItem>
         <asp:ListItem  Value="Last 30 Days" >Last 30 Days</asp:ListItem>
          <asp:ListItem Value="This Month" >This Month</asp:ListItem>


          <asp:ListItem Value="Last Month" >Last Month</asp:ListItem>
          <asp:ListItem Value="This Quarter" >This Quarter</asp:ListItem>
          <asp:ListItem Value="Last Quarter" >Last Quarter</asp:ListItem>
          <asp:ListItem Value="This Fiscal Year" >This Fiscal Year</asp:ListItem>
          <asp:ListItem Value="Last Fiscal Year" >Last Fiscal Year</asp:ListItem>
          <asp:ListItem Value="Custom Dates" >Custom Dates</asp:ListItem>    
      </asp:DropDownList>
      </p>
      <div id="CustomDatesClosedDate" <%=customDatCDDisplay %> >
      
      <p>
 
<div class="date1">
Starts:  <asp:TextBox runat="server" ID="CustomDatesClosedDate_StartDate" CssClass="validate[required,custom[date]]" Width="150" ></asp:TextBox> 
 
</div>

<div class="date2">
Ends: <asp:TextBox runat="server" ID="CustomDatesClosedDate_EndDate" CssClass="validate[required,custom[date]]" Width="150" ></asp:TextBox> 
  
</div>

</p>
      </div>

	   <p>
      <label for="ctl00_mainContentCP_DisplayResutlsbyDD" >Display Results by</label> <br/>
     <asp:DropDownList runat="server" ID="DisplayResutlsbyDD"   >
       <asp:ListItem >Date</asp:ListItem>
      <asp:ListItem >Form</asp:ListItem>   
      </asp:DropDownList>
       
      </p>
       
     

       <div class="buttons">

       <asp:Button CssClass="btn" runat="server" ID="SaveReport" Text="Run Report" onclick="SaveReport_Click" 
                /> 
        <asp:Button CssClass="btn" runat="server" ID="RunReport" Text="Run Report" onclick="RunReport_Click" 
                /> 
                <asp:Button CssClass="btn" runat="server" ID="DownloadReport" Text="Download Report(XLSX)"   onclick="ExcelClick_Click"
               /> 

	  	 
	  	 <a runat="server" id="showRemove" visible="false"  onclick="javascript:confirmDelete();" href='#' class="float-right"  >Remove This Report From the Schedule</a>
         
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

