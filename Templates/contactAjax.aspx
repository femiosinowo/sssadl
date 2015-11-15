<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contactAjax.aspx.cs" Inherits="Templates_contactAjax"  %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/contactControls.ascx" TagPrefix="ux" TagName="contact" %>
<%@ Register Src="~/Controls/conctactOnBehalfOfForm.ascx" TagPrefix="ux" TagName="OnBehalfOf" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
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

</head>
<body>
    <form id="form1" runat="server"  >
    

       <div id="formBlk_content" class="design_content design_mode_entry" contenteditable="false" onmouseover="try{ Ektron.FormBlock.setState('__ekFormState_formBlk', 'in') }catch(ex){}" onmouseout="try{ Ektron.FormBlock.setState('__ekFormState_formBlk', 'out') }catch(ex){}">


          <ux:contact ID="contactForm" runat="server" />

     <cms:FormBlock IncludeTags="false" DefaultFormID="113" runat="server" ID="formBlk" />
    
     
     <ux:OnBehalfOf runat="server" ID="onbehalfForm" />
    </div>
   
     
    </form>
</body>
</html>
