<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxgetuserdetails.aspx.cs" Inherits="admin_requests_ajaxGetUsersDetails" %>
 <script>
     $(function () {


        
         //  e.preventDefault();
         $('#ctl00_mainContentCP_RequestorName').val('<%=fullname %>');
         $('#ctl00_mainContentCP_RequestorTitle').val('<%=userTitle %>');
         $('#ctl00_mainContentCP_RequestorComponent').val('<%=userComponent %>');
         $('#ctl00_mainContentCP_RequestorOfficeCode').val('<%=userofficecode %>');
         $('#ctl00_mainContentCP_RequestorEmail').val('<%=useremail %>');
         $('#ctl00_mainContentCP_RequestorPhone').val('<%=userphone %>');
         $('#ctl00_mainContentCP_RequestorPIN').val('<%=PINN %>');
         
         $('#ctl00_mainContentCP_RequestorLastName').val('<%=Userlastname %>');
         $('#ctl00_mainContentCP_RequestorFirstName').val('<%=Userfirstname %>');
         $('#ctl00_mainContentCP_RequestorServer').val('<%=UserServer %>');
         $('#ctl00_mainContentCP_RequestorUserDomain').val('<%=UserDomain %>');


         $('#ctl00_mainContentCP_RequestorName').prop("disabled", true);
         $('#ctl00_mainContentCP_RequestorTitle').prop("disabled", true);
         $('#ctl00_mainContentCP_RequestorComponent').prop("disabled", true);
         $('#ctl00_mainContentCP_RequestorOfficeCode').prop("disabled", true);
         $('#ctl00_mainContentCP_RequestorEmail').prop("disabled", true);
         $('#ctl00_mainContentCP_RequestorPhone').prop("disabled", true);


         $('#ctl00_mainContentCP_RequestorLastName').prop("disabled", true);
         $('#ctl00_mainContentCP_RequestorFirstName').prop("disabled", true);
         $('#ctl00_mainContentCP_RequestorServer').prop("disabled", true);
         $('#ctl00_mainContentCP_RequestorUserDomain').prop("disabled", true);
        // $('#btn-submit').prop("disabled", false);

     });


 
    </script>