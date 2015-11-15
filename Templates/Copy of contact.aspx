<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="Copy of contact.aspx.cs" Inherits="Templates_Content" %>
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

    Ektron.ready(function (event, eventName) {
        Ektron.Controls.FormBlock.Utility.RedirectLink.init();
    });



    Ektron.ready(function (event, eventName) {
        var settings =
{ srcPath: "/WorkArea/ContentDesigner/"
, skinPath: "/WorkArea/csslib/ContentDesigner/"
, editorSkinPath: "/WorkArea/csslib/Editor/"
, langType: 1033
, localizedStrings:
{ stdOK: "OK"
, stdCancel: "Cancel"
, mnuInsAbv: "Insert Above"
, mnuInsBel: "Insert Below"
, mnuDupl: "Duplicate"
, mnuMvUp: "Move Up"
, mnuMvDn: "Move Down"
, mnuRem: "Remove"
, sFld: "Field"
, sInvFld: "At least one field is not valid. Please correct it and try again."
, sShow: "Show"
, sHide: "Hide"
}
};
        try {
            var sf = Ektron.SmartForm.create("ctl00_ctl00_cphMainContent_cphSecondaryMainContent_contactForm_content", settings);
            Ektron.SmartForm.instances["ctl00$ctl00$cphMainContent$cphSecondaryMainContent$contactForm"] = sf;
            if (sf) sf.onload();
        } catch (ex) {
            alert('Error initializing smart form: ' + ex.message);
        }
    });


$(function () {
 //   var url = "/Templates/contactAjax.aspx?formID=" + <%=defaultFormID %>;

 

  //  $.get(url, function (data) {
  //      $("#ajaxContact").html(data);

    //});

    //without htis second load the ektron validation doesnt work - i wonder why
   // $.get(url, function (data) {
   //     $("#ajaxContact").html(data);

  //  });



});



</script>

  <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />
 

	  
	  
	  <div class="form" id="contact_form">
	  	<label for="contact_subject" class="bold">
		    I need to contact the Digital Library about...<span class="fg-red" title="Required">*</span>
		</label>
          
    
		
 	 
		
			<!-- mmmmmmmmmmmmmmm Default option mmmmmmmmmmmmmmm-->
            
            
  
             
             
             <asp:DropDownList runat="server" AutoPostBack="true"  ID="contactSelect" OnSelectedIndexChanged="changeDisplayForm" >
        </asp:DropDownList>
 
		  <div id="ctl00_ctl00_cphMainContent_cphSecondaryMainContent_contactForm_content" class="design_content design_mode_entry" contenteditable="false" onmouseout="try{ Ektron.FormBlock.setState('__ekFormState_ctl00_ctl00_cphMainContent_cphSecondaryMainContent_contactForm', 'out') }catch(ex){}" onmouseover="try{ Ektron.FormBlock.setState('__ekFormState_ctl00_ctl00_cphMainContent_cphSecondaryMainContent_contactForm', 'in') }catch(ex){}">
 <cms:FormBlock runat="server" IncludeTags="false" ID="contactForm" />
		
	 <fieldset>
		<legend class="bold">This request is on behalf of:<span class="fg-red" title="Required">*</span></legend>
		<input type="radio" name="request" value="me" id="request_myself" checked>  <label for="request_myself" class="inline-label">Myself</label><br>
		<input type="radio" name="request" value="someone_else" id="request_someone"> <label for="request_someone" class="inline-label">Someone else</label>
		</fieldset>


        		<p class="margin-top">
		<input id="btn-submit" class="btn" type="submit"   value="Submit">
		</p>
        	
		<p class="bold">
		<span class="fg-red">*</span>
		Required Fields
	</p>
		
	  </div>
	  
	  
	  
	  
 
   </div>

    
	  
	  
	  
	  
	   
	  
        
          
                         
	  
	  
	  
 
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
