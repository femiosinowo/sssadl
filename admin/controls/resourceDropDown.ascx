<%@ Control Language="C#" AutoEventWireup="true" CodeFile="resourceDropDown.ascx.cs" Inherits="admin_controls_resourceDropDown" %>
   <asp:Panel runat="server" ID="ResourceSummaryPanel" Visible="false" >
       <p>

       <label class="bold" >Resource Summary</label><br/>
       <label><%=resourceDescription%></label><br/>
          <asp:Panel runat="server" ID="LicensePanel"   >
      <label>  Total Licenses: <%=resourcePasswordsAvailable%>  </label> <br/>
      <label>Assigned Licenses: <%=AssignedLicenses%></label>  <%=ListUsersWithAccess %> <br/>
       <label class="bold">Available Licenses: <%=AvailableLicenses%></label> <br/>
       </asp:Panel>
       <asp:Literal runat="server" ID="ResourceDetails" ></asp:Literal>
       
       </p></asp:Panel>
       <asp:Panel runat="server" ID="AdminLoginPanel" Visible="false" >
    

      <div class="toggle-block" >
    <a href="#">Admin Login Information</a>
    <div>
      <label> Login at: <%=resourceAdminResourceURL%></label><br/><label>Username: <%=resourceAdminUsername %>  </label><br/>
      <label> Password: <%=resourceAdminPassword%></label>
   </div>
</div>

       </asp:Panel>