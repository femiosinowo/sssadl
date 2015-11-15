<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="adhocresource.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Ad Hoc Resource Reports</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->


	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	  

           <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>Reports</li> <li>Ad Hoc Resource Reports</li>  
    </ul> 

   
    
      
</div>


	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
           <h3>Ad Hoc Resource Reports</h3>
	  <asp:Panel runat="server" ID="InvalidEmailPanel" Visible="false" >
      <div class="container-red">
	     
	  
	  
	  </div>
      
      </asp:Panel>

      <script>

          loadDefaultResources();
          function loadDefaultResources() {
              //load default resources
              url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=showall&rid=<%=Resources %>";
              $.get(url, function (data) {
                  $("#ResourcesDiv").html(data);

              });
          }


          $(document).ready(function () {


              $("#ctl00_mainContentCP_StartDate").datepicker();
              $("#ctl00_mainContentCP_EndDate").datepicker();

 



              $('#ctl00_mainContentCP_ShowDataforDD').change(function () {

                  var selectedValue = $(this).val();

                  switch (selectedValue) {
                      case "Applications in a Network":
                          $("#ResourcesDiv").html('');
                          $('#SelectedNetwork').show();
                          break;
                      default:
                          $('#SelectedNetwork').hide();
                          loadDefaultResources();
                          break;
                      
                  }
           


              });




              $('#ctl00_mainContentCP_AssociatedNetwork').change(function () {

                  var selectedValue = $(this).val();
                  url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=AssoicatedNetwork&value=" + selectedValue;   
               
                  $(document).ajaxStart(function () {
                      $("#loading").show();
                      //  $("#resultDiv").hide();
                  });
                  $(document).ajaxStop(function () {
                      $("#loading").hide();
                      //  $("#resultDiv").show();
                  });

                  $.get(url, function (data) {
                      $("#ResourcesDiv").html(data);

                  });


              });

              




              $('#ctl00_mainContentCP_DateRangeDD').change(function () {

                  var selectedValue = $(this).val();

                  switch (selectedValue) {
                      case "Custom Dates":

                          $("#CustomDates").show();
                          break;
                      default:
                          $("#CustomDates").hide();
                          break;

                  }



              });



          });


 </script>
 <asp:Panel runat="server" ID="editPanel" Visible="false" >
 
 <script>



   $(document).ready(function () {
       $('#SelectedNetwork').show();

       url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=AssoicatedNetwork&value=<%=AssociatedNetworkTxt %>&rid=<%=Resources %>";

         $(document).ajaxStart(function () {
             $("#loading").show();
             //  $("#resultDiv").hide();
         });
         $(document).ajaxStop(function () {
             $("#loading").hide();
             //  $("#resultDiv").show();
         });

         $.get(url, function (data) {
             $("#ResourcesDiv").html(data);

         });
     });

   
     
     </script>
 </asp:Panel>

    
      
	  <p>
      <label for="ctl00_mainContentCP_ShowDataforDD"  > Show Data for</label> <br/>
      
      <asp:DropDownList runat="server" ID="ShowDataforDD"   >
       <asp:ListItem Value="Applications" >Applications</asp:ListItem>
       <asp:ListItem Value="Applications in a Network" >Applications in a Network</asp:ListItem>
        <asp:ListItem Value="Applications by Type (M/D)" >Applications by Type (M/D)</asp:ListItem>
         
          <asp:ListItem Value="Networks" >Networks</asp:ListItem>
      </asp:DropDownList>
      </p>
      <p>
      <div id="SelectedNetwork" style="display:none" >
            
      
       <label  for="ctl00_mainContentCP_AssociatedNetwork">
		  Associated Network<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:DropDownList runat="server" ID="AssociatedNetwork"  CssClass="validate[required]" ToolTip="Select One Associated Network"   >
      <asp:ListItem   Text=" - Select One -" Value=""></asp:ListItem>
      <asp:ListItem Text="Internet" Value="Internet"></asp:ListItem>
      <asp:ListItem   Text="Intranet" Value="Intranet"></asp:ListItem>
      <asp:ListItem Text="Citrix" Value="Citrix"></asp:ListItem>
       <asp:ListItem Text="File" Value="File"></asp:ListItem>
     </asp:DropDownList>    
       
      </div>
      </p>
      <div id="loading" style="display:none" ></div>
     <div id="ResourcesDiv" >
     
     </div>


	  


      <p>
      <label for="ctl00_mainContentCP_DateRangeDD" > Date Range</label> <br/>
      
      <asp:DropDownList runat="server" ID="DateRangeDD"   >
       <asp:ListItem Value="Today" >Today</asp:ListItem>
       <asp:ListItem Value="Yesterday" >Yesterday</asp:ListItem>
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
      <div id="CustomDates" <%=customDatSDDisplay %> >
      
      <p>
 
  
<div class="date1">
Starts: <asp:TextBox runat="server" ID="StartDate"></asp:TextBox>    
</div>

<div class="date2">
Ends:  <asp:TextBox runat="server" ID="EndDate"></asp:TextBox>  
</div>


 

</p>
      </div>


        <p>
  <label for="ctl00_mainContentCP_DisplayDD" >Display</label> <br/>
<div class="date1">

   <asp:DropDownList runat="server" ID="DisplayDD"   >
       <asp:ListItem   Value="All" >All</asp:ListItem>
       <asp:ListItem Value="All repeat users" >All repeat users</asp:ListItem>
       <asp:ListItem Value="The top 100 percent" >The top 100 percent</asp:ListItem>
        <asp:ListItem Value="The top 50 percent" >The top 50 percent</asp:ListItem>
         <asp:ListItem  Value="The top 500" >The top 500</asp:ListItem>
          <asp:ListItem Value="The top 250" >The top 250</asp:ListItem>
          <asp:ListItem Value="The top 100" >The top 100</asp:ListItem>
          
      </asp:DropDownList>
</div>

<div class="date2">

   <asp:DropDownList runat="server" ID="Display2DD"   >
       <asp:ListItem  Value="Visitors" >Visitors</asp:ListItem>
       <asp:ListItem Value="Hits" >Hits</asp:ListItem>
       
          
      </asp:DropDownList>
</div>

</p>


	  


 <p>
      <label for="ctl00_mainContentCP_DisplayByDD" >by</label> <br/>
      
      <asp:DropDownList runat="server" ID="DisplayByDD"   >
       <asp:ListItem    Value="Date" >Date</asp:ListItem>
       <asp:ListItem Value="Month/Year" >Month/Year</asp:ListItem>
       <asp:ListItem Value="Application" >Application</asp:ListItem>
        <asp:ListItem Value="Mandatory/Discretionary" >Mandatory/Discretionary</asp:ListItem>
         <asp:ListItem Value="Server" >Server</asp:ListItem>
             <asp:ListItem Value="Domain" >Domain</asp:ListItem>
               <asp:ListItem Value="Email" >Email</asp:ListItem>
              <asp:ListItem Value="PIN" >PIN</asp:ListItem>
                <asp:ListItem Value="LastName" >Last Name</asp:ListItem>
                  <asp:ListItem Value="FirstName" >First Name</asp:ListItem>
       
      </asp:DropDownList>
      </p>


      <div class="toggle-link">
    <a href="#">Show Advanced Field</a>
   <div  >


       <p>
      <label for="ctl00_mainContentCP_BreakOutDataByDD">Break out data by</label> <br/>      
       <asp:DropDownList runat="server"   ID="BreakOutDataByDD" >
  <asp:ListItem Value="Day" >Day</asp:ListItem>      
      </asp:DropDownList>
      </p>



      <p>
       <label for="ctl00_mainContentCP_ListBox_AccessType"  > Access Type</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_AccessType"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more Access Type(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_AccessType')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_AccessType')">Select None</a>
    
      </p>



         <p>
       <label for="ctl00_mainContentCP_ListBox_OfficeCode"  >Office Code</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_OfficeCode"  DataTextField="value" DataValueField="value" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more Office Code(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_OfficeCode')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_OfficeCode')">Select None</a>
    
      </p>

      
         <p>
       <label for="ctl00_mainContentCP_ListBox_UserDomain"  >User Domain</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_UserDomain"  DataTextField="value" DataValueField="value" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more User Domain(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_UserDomain')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_UserDomain')">Select None</a>
    
      </p>



              <p>
       <label for="ctl00_mainContentCP_ListBox_CoreServicesOffice"  >Core Services - Office</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_CoreServicesOffice"  DataTextField="value" DataValueField="value" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more User Core Services Office(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_CoreServicesOffice')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_CoreServicesOffice')">Select None</a>
    
      </p>

               <p>
       <label for="ctl00_mainContentCP_ListBox_PIN"  >Core Services - PIN</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_PIN"  DataTextField="value" DataValueField="value" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more User Core Services Office(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_PIN')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_PIN')">Select None</a>
    
      </p>


                   <p>
       <label for="ctl00_mainContentCP_ListBox_Email"  >Core Services - Email</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_Email"  DataTextField="value" DataValueField="value" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more User Core Email(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_Email')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_Email')">Select None</a>
    
      </p>


                     <p>
       <label for="ctl00_mainContentCP_ListBox_FirstName"  >Core Services - First Name</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_FirstName"  DataTextField="value" DataValueField="value" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more User Core FirstName(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_FirstName')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_FirstName')">Select None</a>
    
      </p>



      
                     <p>
       <label for="ctl00_mainContentCP_ListBox_LastName"  >Core Services -Last Name</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_LastName"  DataTextField="value" DataValueField="value" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more User Core LastName(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_LastName')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_LastName')">Select None</a>
    
      </p>
      
                     <p>
       <label for="ctl00_mainContentCP_ListBox_Server"  >Core Services -Server</label> <br/>
        <asp:ListBox runat="server" ID="ListBox_Server"  DataTextField="value" DataValueField="value" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more User Core Server(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ListBox_Server')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ListBox_Server')">Select None</a>
    
      </p>


       </div>
</div>


       





 
     

       <div class="buttons">
         <asp:Button CssClass="btn" runat="server" ID="SaveReport" Text="Run Report" onclick="SaveReport_Click" 
                /> 
        <asp:Button CssClass="btn" runat="server" ID="RunReport" Text="Run Report" onclick="RunReport_Click" 
                /> 
                <asp:Button CssClass="btn" runat="server" ID="DownloadReport" 
               Text="Download Report(XLSX)" onclick="DownloadReport_Click"   
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

