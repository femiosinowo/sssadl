<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxResourceDD.aspx.cs" Inherits="admin_helprequests_ajaxResource" %>
 
 
    <asp:Panel runat="server" ID="ResourceSummaryPanel"   Visible="false" >
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
       <asp:Panel runat="server" ID="AdminLoginPanel" Wrap="false" Visible="false" >
    

 
 <h4>Administrative login details</h4>
      <div  >
    <a href="#"></a>
    <div>
      <label> Login at: <%=resourceAdminResourceURL%></label><br/><label>Username: <%=resourceAdminUsername %>  </label><br/>
      <label> Password: <%=resourceAdminPassword%></label>
   </div>
</div> <p></p>

       </asp:Panel> 

  