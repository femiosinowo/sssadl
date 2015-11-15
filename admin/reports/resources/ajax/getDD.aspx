<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getDD.aspx.cs" Inherits="admin_reports_resources_ajax_getDD" %>


 
 	  
	 <script>

	     $(document).ready(function () {



	         $('#ResourceTypeTaxonomy').change(function () {

	             var selectedValue = $(this).val();

	             url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=ResourcesbyType&typeid=" + selectedValue;
	             $(document).ajaxStart(function () {
	                 $("#loadingResource").show();
	                 $("#resourceDiv").hide();
	             });
	             $(document).ajaxStop(function () {
	                 $("#loadingResource").hide();
	                 $("#resourceDiv").show();
	             });

	             $.get(url, function (data) {
	                 $("#resourceDiv").html(data);

	             });


	         });




	         $('#ProcurementMethod').change(function () {

	             var selectedValue = $(this).val();

	             url = "/admin/reports/resources/ajax/getResourceDD.aspx?type=ResourcesbyProcurementMethod&pmid=" + selectedValue + "&mathRandom=" + Math.random();
	             $(document).ajaxStart(function () {
	                 $("#loadingResource").show();
	                 $("#resourceDiv").hide();
	             });
	             $(document).ajaxStop(function () {
	                 $("#loadingResource").hide();
	                 $("#resourceDiv").show();
	             });

	             $.get(url, function (data) {
	                 $("#resourceDiv").html(data);

	             });


	         });









	     });


 </script>
 
    
     
    
    <div id="PanelResourceType" visible="false" runat="server"> 
    
  <p>
      
       <label   for="ResourceTypeTaxonomy">
		   Resource Type<span title="Required" class="fg-red">*</span>
		</label><br />
      

    <select id="ResourceTypeTaxonomy" name="ResourceTypeTaxonomy" ToolTip="Please Select One Resource Type"  Class="validate[required]" >
    <%=optionResourceTypeTaxonomy%>
    </select>

      </p></div>

      
    <div   runat="server" ID="PanelProcurementMethod" Visible="false" runat="server"> 
  <p>
      
       <label   for="ProcurementMethod">
		  Procurement Method 
		</label><br />
     
      <select id="ProcurementMethod" name="ProcurementMethod" ToolTip="Please Select One Procurement Method"  Class="validate[required]" >
     <%=optionProcurementMethod%>
    </select>
    

      </p></div>
    