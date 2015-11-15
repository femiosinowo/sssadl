<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getInformation.aspx.cs"  Inherits="Templates_getInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
   <script>
       

    

           $('#IndividualName').val('<%=IndividualName %>')  ;
             $('#IndividualTitle').val('<%=IndividualTitle %>')  ;
               $('#IndividualComponent').val('<%=IndividualComponent %>')  ;
                 $('#IndividualOfficeCode').val('<%=IndividualOfficeCode %>')  ;
                   $('#IndividualPhone').val('<%=IndividualPhone %>')  ;
                       $('#IndividualEmail').val('<%=IndividualEmail %>')  ;
$('#IndividualsPIN').val('<%=IndividualsPIN %>')  ;

           $('#SubmittedForPIN').val('<%=SubmittedForPIN %>')  ;
             $('#SubmittedForLastName').val('<%=SubmittedForLastName %>')  ;
               $('#SubmittedForFirstName').val('<%=SubmittedForFirstName %>')  ;
                 $('#SubmittedForEMail').val('<%=SubmittedForEMail %>')  ;
                   $('#SubmittedForOffice').val('<%=SubmittedForOffice %>')  ;
                              $('#SubmittedForServer').val('<%=SubmittedForServer %>')  ;
             $('#SubmittedForUserDomain').val('<%=SubmittedForUserDomain %>')  ;


             var pin = $('#SubmittedForPIN').val();
             if (pin != '') {
                 $('#btn-submit').prop("disabled", false);
             }
   
   </script>
  
    </form>
</body>
</html>
