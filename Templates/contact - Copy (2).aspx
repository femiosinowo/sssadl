<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"  CodeFile="contact - Copy (2).aspx.cs" Inherits="Templates_Content" %>
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
<style>

input[type="submit"] {
    height: auto !important;
    width: auto !important;
}
</style>

<script>



$(function () {
    var url = "/Templates/contactAjax.aspx?formID=" + <%=defaultFormID %>;

 
            $(document).ajaxStart(function () {
                $("#loading").show();
                //  $("#resourceDiv").hide();
            });
            $(document).ajaxStop(function () {
                $("#loading").hide();
                //$("#ajaxContact").show();
            });

    $.get(url, function (data) {
        $("#ajaxContact").html(data);
         $("#loading").hide();
    });

  


    
});





</script>
 

  <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />
 
	  
      
	  <p>Do you have a question, comment, or request for the Digital Library? If so, please tell us about it below. We'll be sure to respond within 24 hours.</p>
	  
	  
	  <div class="form" id="contact_form">
	  	<label for="contact_subject" class="bold">
		    I need to contact the Digital Library about...<span class="fg-red" title="Required">*</span>
		</label>
          
    
		
 		<select id="contact_subject" required="required" >
			 
			<%=selectOutput %>
			 
		</select> 
		
			<!-- mmmmmmmmmmmmmmm Default option mmmmmmmmmmmmmmm-->
            
            <div id="ajaxContact" ></div>
             <div id="loading" style="display:none" >
                 <img src="../images/loading.gif" /></div> 
	 
		
		<p class="bold">
		<span class="fg-red">*</span>
		Required Fields
	</p>
		
	  </div>
	  
	  
	  
	  
 
   

    
	  
	  
	  
	  
	   
	  
        
          
                         
	  
	  
	  
 
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
