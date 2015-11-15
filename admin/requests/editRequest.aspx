<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="editRequest.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/requests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
<style>

.div1, .div2 {
    display: inline-block;
    width: 10%;
}

 .column-5 {
    width: 46.667%;
}
input, textarea, select {
    max-width: 90%;
    width: 90%;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


<asp:HiddenField ID="searchStringHF" runat="server" />
<asp:HiddenField ID="sortOrderbyHF" Value=" order by lastname asc" runat="server" />
<asp:HiddenField ID="RequestID" runat="server" />
<asp:HiddenField ID="AssignmentIDHF" runat="server" />
<asp:HiddenField ID="AccesIDHF" runat="server" />
<div class="grid" id="admin-tools-int-content">
	    
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      
     <div class="container-yellow" <%=notifyStle %>>
         <div <%=divApprovedDiv %> >
		 <p>This e-Password request was approved on <%=notifyDate %> by <%=notifyByUsername %>, but the requestor has not yet been notified. To notify the requestor and close this request, click approve again and send a message.</p>
	  </div>
      <div <%=divDeclinedDiv %> >
      <p>This e-Password request was declined on <%=notifyDate %> by <%=notifyByUsername %>, but the requestor has not yet been notified. To notify the requestor and close this request, click declined again and send a message.</p>
	
      </div>

      <div <%=divExistsApprovedDiv %>>
      <h4>Existing Assigned Password</h4>
      <p>There is an existing password assigned to this user for this resource on <%=approvedDate %> by <%=approvedByDetails %>.</p>
      </div>
	  
	  </div>
   <div class="column-5" >
   <h3>e-Password Request for  </h3>
  
   <asp:HiddenField runat="server" ID="RequestorPIN" />
     <p> <label for="RequestorName">Name </label>  
      <asp:TextBox ID="RequestorName" runat="server" ></asp:TextBox>
      </p>

         <p> <label for="RequestorTitle">Title</label>  
      <asp:TextBox ID="RequestorTitle" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="RequestorComponent">Component </label>  
      <asp:TextBox ID="RequestorComponent" runat="server" ></asp:TextBox>
      </p>

          <p> <label for="RequestorOfficeCode">    Office Code </label>  
      <asp:TextBox ID="RequestorOfficeCode" runat="server" ></asp:TextBox>
      </p>
  

         <p> <label for="RequestorEmail">Email </label>  
      <asp:TextBox ID="RequestorEmail" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="RequestorPhone">Phone</label>  
      <asp:TextBox ID="RequestorPhone" runat="server" ></asp:TextBox>
      </p>

      </div>
      <div class="column-5" >
       <h3>Request Submitted by</h3>
          <asp:HiddenField runat="server" ID="SubmittedByPIN" />
     <p> <label for="SubmittedByName">Name </label>  
      <asp:TextBox ID="SubmittedByName" runat="server" ></asp:TextBox>
      </p>

         <p> <label for="SubmittedByTitle">Title</label>  
      <asp:TextBox ID="SubmittedByTitle" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="SubmittedByComponent">Component </label>  
      <asp:TextBox ID="SubmittedByComponent" runat="server" ></asp:TextBox>
      </p>

          <p> <label for="SubmittedByOfficeCode">    Office Code </label>  
      <asp:TextBox ID="SubmittedByOfficeCode" runat="server" ></asp:TextBox>
      </p>
  

         <p> <label for="SubmittedByEmail">Email </label>  
      <asp:TextBox ID="SubmittedByEmail" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="SubmittedByPhone">Phone</label>  
      <asp:TextBox ID="SubmittedByPhone" runat="server" ></asp:TextBox>
      </p>
      </div>
       
          <h3>Request Details</h3>
          <p>
          <label class="bold" for="">Resource Requesting Access To</label>
       <asp:DropDownList runat="server" DataTextField="ResourceName" DataValueField="ID" ID="ResourceDD" ></asp:DropDownList>
       </p>

       <p>
       <label class="bold" >Resource Summary</label><br/>
       <label><%=resourceDescription%></label><br/>
          <asp:Panel runat="server" ID="LicensePanel"   >
      <label>  Total Licenses: <%=resourcePasswordsAvailable%>  </label> <br/>
      <label>Assigned Licenses: <%=AssignedLicenses%></label>  <%=ListUsersWithAccess %> <br/>
       <label class="bold">Available Licenses: <%=AvailableLicenses%></label> <br/>
       </asp:Panel>
       <asp:Literal runat="server" ID="ResourceDetails" ></asp:Literal>
       
       </p>

       <asp:Panel runat="server" ID="AdminLoginPanel" Visible="false" >
       <p>
       <h3>Administrative Login Information</h3>
      <label> Login at: <%=resourceAdminResourceURL%></label><br/><label>Username: <%=resourceAdminUsername %>  </label><br/>
      <label> Password: <%=resourceAdminPassword%></label></p>
       </asp:Panel>
       <p>
      <label class="bold" for="whyNeedAccess" >Why do you need access?*</label><br/>
       <asp:TextBox runat="server" ID="whyNeedAccess" TextMode="MultiLine" ></asp:TextBox>
       </p>
         <p>
      <label  for="InternalNotes" >Internal Notes</label><br/>
       <asp:TextBox runat="server" ID="InternalNotes" TextMode="MultiLine" ></asp:TextBox>
       </p>

       <p>
            <div class="container-yellow" <%=notifyStle %>>
         <div <%=divApprovedDiv %> >
		 <p>This e-Password request was approved on <%=notifyDate %> by <%=notifyByUsername %>, but the requestor has not yet been notified. To notify the requestor and close this request, click approve again and send a message.</p>
	  </div>
      <div <%=divDeclinedDiv %> >
      <p>This e-Password request was declined on <%=notifyDate %> by <%=notifyByUsername %>, but the requestor has not yet been notified. To notify the requestor and close this request, click declined again and send a message.</p>
	 </div>
      </div>
       <asp:Button CssClass="btn" runat="server" ID="DeclineBtn" Text="Decline" 
               onclick="DeclineBtn_Click" ></asp:Button>
       
       <asp:Button CssClass="btn" runat="server" ID="ApproveBtn" Text="Approve" 
               onclick="Approve_Click" ></asp:Button>
       <asp:Button CssClass="btn" runat="server" ID="SaveForLaterBtn" Text="Save For Later" 
               onclick="SaveForLater_Click" ></asp:Button>
       </p>

       <ux:AuditLog runat="server" ID="AuditLogUX" />


      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

