<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxgetUsers.aspx.cs" Inherits="admin_users_ajaxgetUsers" %>

  <script>
      $(function () {

          //   $('#ctl00_mainContentCP_PIN').val('None');
          var email = "<%=useremail %>";
          var lastname = "<%=Userfirstname %>";
          var firstname = "<%=Userlastname %>";

          $('#ctl00_mainContentCP_EmailAddress').val(email);
          $('#ctl00_mainContentCP_FirstName').val(firstname);
          $('#ctl00_mainContentCP_LastName').val(lastname);



          $('#ctl00_mainContentCP_EmailAddressHF').val(email);
          $('#ctl00_mainContentCP_FirstNameHF').val(firstname);
          $('#ctl00_mainContentCP_LastNameHF').val(lastname);

      });


 
    </script>

    <input type="hidden" runat="server" id="foundEmail" />
       <input type="hidden" runat="server" id="foundLastName" />
          <input type="hidden" runat="server" id="foundFirstName" />