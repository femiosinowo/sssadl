<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="contracts.aspx.cs" Inherits="admin_reports_Default"  %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Contract Information</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->


	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	       <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>Reports</li> <li>Contract Information</li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	 
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
       <h3>Contract Information</h3>
	  <asp:Panel runat="server" ID="InvalidEmailPanel" Visible="false" >
      <div class="container-red">
	     
	  
	  
	  </div>
      
      </asp:Panel>
	  <p>
      <label for="ctl00_mainContentCP_ReportTypeDD" class="bold"> Report Type*</label> <br/>
      
      <asp:DropDownList runat="server" ID="ReportTypeDD" CssClass="validate[required]" >
       <asp:ListItem Value="" > - Select One -</asp:ListItem>
       <asp:ListItem Value="Comparative Resource Pricing">Comparative Resource Pricing</asp:ListItem>
      <asp:ListItem Value="Pricing Over Time">Pricing Over Time</asp:ListItem>
       <asp:ListItem Value="Procurement Method" >Procurement Method</asp:ListItem>
        <asp:ListItem  Value="Contract Expiration" >Contract Expiration</asp:ListItem>
     
      </asp:DropDownList>
      </p>
	  
	 <script>

	     function loadDefaultResources() {
	         //load default resources
	         url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=showall";
	         $.get(url, function (data) {
	             $("#resultDiv").html(data);

	         });
	     }
	     function loadResouces(whattype, resourceIds) {
	         var whattypeSend;
	         switch (whattype) {
	             case "Resources by Procurement Method":
	                 whattypeSend = 'ResourcesbyProcurementMethod';
	                 break;
	             case "Resources by Type":
	                 whattypeSend = 'ResourcesbyType';
	                 break;
	             case "Resources":
	                 whattypeSend = 'showall';
	                 break;
	         }


	         url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=" + whattypeSend + "&rid=" + resourceIds + "&pmid=<%=ProcurementMethod %>&typeid=<%=ResourceTypeTaxonomy %>" + "&mathRandom=" + Math.random();
	         $.get(url, function (data) {
	             $("#resourceDiv").html(data);

	         });
	     }

	     function loadSelectedCompare(whattype, selectThis) {
	         $(document).ready(function () {
	             switch (whattype) {
	                 case "Resources by Procurement Method":
	                     url = "/admin/reports/resources/ajax/getDD.aspx?type=ResourcesbyProcurementMethod&selectThis=" + selectThis + "&mathRandom=" + Math.random();;
	                     break;
	                 case "Resources by Type":
	                     url = "/admin/reports/resources/ajax/getDD.aspx?type=ResourcesbyType&selectThis=" + selectThis + "&mathRandom=" + Math.random();;
	                     break;
	                 case "Resources":
	                     url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=showall&pmid=" + selectedValue + "&mathRandom=" + Math.random();;
	                     $("#resultDiv").html('');
	                     $("#resourceDiv").html('');
	                     break;
	                 //	                 case "Procurement Method":          
	                 //	                     url = "/admin/reports/resources/ajax/getDD.aspx?type=ProcurementMethod";          
	                 //	                     break;          


	             }
	             $(document).ajaxStart(function () {
	                 $("#loading").show();
	                 //  $("#resultDiv").hide();
	             });
	             $(document).ajaxStop(function () {
	                 $("#loading").hide();
	                 //  $("#resultDiv").show();
	             });

	             $.get(url, function (data) {
	                 $("#resultDiv").html(data);

	             });


	         });
	     }





	     $(document).ready(function () {


	         $("#ctl00_mainContentCP_StartDate").datepicker();
	         $("#ctl00_mainContentCP_EndDate").datepicker();



	         $('#ctl00_mainContentCP_ReportTypeDD').change(function () {

	             var selectedValue = $(this).val();
	             //alert(selectedValue);
	             switch (selectedValue) {
	                 case "Comparative Resource Pricing":

	                     $("#ctl00_mainContentCP_CompareDataAgainst option[value='None']").attr('disabled', true);
	                     $("#ctl00_mainContentCP_CompareDataAgainst option[value='Prior FY']").attr('selected', true);
	                     break;

	                 case "Procurement Method":
                     case "Contract Expiration":
	                   //  $("#ListResourcesDD").removeClass('validate[required]');
	                      
	                     break;
	                 default:
	                     $("#ctl00_mainContentCP_CompareDataAgainst option[value='None']").removeAttr('disabled');
	                     break;

	             }



	         });




	         $('#ctl00_mainContentCP_CompareDD').change(function () {

	             var selectedValue = $(this).val();

	             switch (selectedValue) {
	                 case "Resources by Procurement Method":
	                     url = "/admin/reports/resources/ajax/getDD.aspx?type=ResourcesbyProcurementMethod";
	                     break;
	                 case "Resources by Type":
	                     url = "/admin/reports/resources/ajax/getDD.aspx?type=ResourcesbyType";
	                     break;
	                 case "Resources":
	                     url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=showall&pmid=" + selectedValue + "&mathRandom=" + Math.random();
	                     $("#resultDiv").html('');
	                     $("#resourceDiv").html('');
	                     break;
	                 //	                 case "Procurement Method":      
	                 //	                     url = "/admin/reports/resources/ajax/getDD.aspx?type=ProcurementMethod";      
	                 //	                     break;      


	             }
	             $(document).ajaxStart(function () {
	                 $("#loading").show();
	                 //  $("#resultDiv").hide();
	             });
	             $(document).ajaxStop(function () {
	                 $("#loading").hide();
	                 //  $("#resultDiv").show();
	             });

	             $.get(url, function (data) {
	                 $("#resultDiv").html(data);

	             });


	         });





	         $('#ctl00_mainContentCP_DateRange').change(function () {

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
 <asp:Panel ID="EditPanel" runat="server" Visible="false" >
 
 
   <script>

       loadResouces('<%=selectedCompareData %>', '<%=Resources %>');
 </script>
 <div id="compareEditDiv" runat="server" visible="false" >
  <script>

      loadSelectedCompare('<%=selectedCompareData %>', '<%=SelectedcompareID %>');
 </script>

 <div id="resourcesEditDiv" runat="server" visible="false" >
 
 
 </div>
 </div>
 

 
 </asp:Panel>
	   <p>
      <label for="ctl00_mainContentCP_CompareDD" >Compare</label> <br/>
     <asp:DropDownList runat="server" ID="CompareDD" CssClass="validate[required]" >
       <asp:ListItem >Resources</asp:ListItem>
      <asp:ListItem >Resources by Procurement Method</asp:ListItem>
       <asp:ListItem >Resources by Type</asp:ListItem>
        
     
      </asp:DropDownList>
       <div id="loading"></div>
      <div id="resultDiv"></div>
       <div id="loadingResource"></div>
      <div id="resourceDiv" >
            </div>
      </p>

	    <p>
      <label for="ctl00_mainContentCP_DateRange">Date Range</label> <br/>
      <asp:DropDownList runat="server" ID="DateRange" >
       
      <asp:ListItem >Q1</asp:ListItem>  
      <asp:ListItem >Q2</asp:ListItem>  
      <asp:ListItem >Q3</asp:ListItem>  
      <asp:ListItem >Q4</asp:ListItem>  
       <asp:ListItem   >This Fiscal Year</asp:ListItem>
      <asp:ListItem >Last Fiscal Year</asp:ListItem>
       <asp:ListItem >Custom Dates</asp:ListItem>      
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
      <label for="ctl00_mainContentCP_CompareDataAgainst">Compare Data Against</label> <br/>
      <asp:DropDownList runat="server" ID="CompareDataAgainst" >
       <asp:ListItem  >None</asp:ListItem>
      <asp:ListItem >Prior FY</asp:ListItem>
       <asp:ListItem >All Prior FY on Record</asp:ListItem>      
      </asp:DropDownList>
      </p>


        
      

      </p>

       <div class="buttons">
       
       <asp:Button CssClass="btn" runat="server" ID="SaveReport" Text="Run Report" onclick="SaveReport_Click" Visible="false"
                /> 
        <asp:Button CssClass="btn" runat="server" ID="RunReport" Text="Run Report" onclick="RunReport_Click" 
                /> 
                <asp:LinkButton CssClass="btn" runat="server" ID="DownloadReport" 
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
	   <div runat="server" id="defaultResourceLoad" >
      <script>  loadDefaultResources();</script>
      </div>
	  </div>
	  <!-- END PAGE CONTENT -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

