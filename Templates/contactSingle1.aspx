<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="contactSingle1.aspx.cs" Inherits="Templates_Content" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>
<%@ Register Src="~/Controls/conctactOnBehalfOfForm.ascx" TagPrefix="ux" TagName="OnBehalfOf" %>
<%@ Register Src="~/Controls/contactControls.ascx" TagPrefix="ux" TagName="contact" %>
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

   <script  type="text/javascript">
        

       //Contact Show form
       $(document).ready(function () {
           $(".someone_else_request").addClass('hide');
           $('.someone_else_request').find('input,select,textarea,button').attr('tabindex', '-1');

           $("input[value='someone_else']").change(function () {
               $(".someone_else_request").removeClass('hide');
               $('.someone_else_request').find('input,select,textarea,button').attr('tabindex', '0');
           });

           $("input[value='me']").change(function () {
               $(".someone_else_request").addClass('hide');
               $('.someone_else_request').find('input,select,textarea,button').attr('tabindex', '-1');
           });



       });


</script>
  <div id="ctl00_ctl00_cphMainContent_cphSecondaryMainContent_mainForm_content" class="design_content design_mode_entry" contentEditable="false" onmouseover="try{ Ektron.FormBlock.setState('__ekFormState_ctl00_ctl00_cphMainContent_cphSecondaryMainContent_mainForm', 'in') }catch(ex){}" onmouseout="try{ Ektron.FormBlock.setState('__ekFormState_ctl00_ctl00_cphMainContent_cphSecondaryMainContent_mainForm', 'out') }catch(ex){}">
 <%=TopDescription %>
 <ux:contact ID="contactForm" runat="server" />
   <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />
 
  <cms:FormBlock ID="mainForm"  runat="server" />
 
	 
      
	    
            
	     <asp:Panel ID="uploadFile" runat="server" Visible="false" >
        <p></p>
        Screen shots of the problem (optional): <br />
     
    <input type="file" id="screenshot" name="screenshot"  />
    <p></p>

  </asp:Panel>


   <ux:OnBehalfOf runat="server" ID="onbehalfForm" />
		</div>
 
	  
	  
	  
	  
 
   

    
	  
	  
	  
	  
	   
	  
        
          
                         
	  
	  
	  
 
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
