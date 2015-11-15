<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="addEpassword.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/requests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 
<style>

.div1, .div2 {
    display: inline-block;
    width: 10%;
}

 
 
</style><style>
 

 
 
 
 


        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


<asp:HiddenField ID="searchStringHF" runat="server" />
<asp:HiddenField ID="sortOrderbyHF" Value=" order by lastname asc" runat="server" />
<asp:HiddenField ID="ShowInactive" Value="False" runat="server" />
<asp:HiddenField ID="RequestID" runat="server" />
<asp:HiddenField ID="ResourceIDHF" runat="server" />
<asp:HiddenField ID="RecordExistsHF" Value="false" runat="server" />
 <asp:HiddenField ID="AssignmentID"   runat="server" />
 <asp:HiddenField ID="ResourceNameHF" Value="false" runat="server" />
 <asp:HiddenField ID="RequestorNameHF" Value="false" runat="server" />

<div class="grid" id="admin-tools-int-content">
	    
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      
   
   
   <h3>Access to <%=resourceName %> for  </h3>
   <asp:HiddenField runat="server" ID="RequestorPIN" />
     <p> <label for="RequestorName">Name </label> <br/>  
      <asp:TextBox ID="RequestorName" runat="server" ></asp:TextBox>
      </p>

         <p> <label for="RequestorTitle">Title</label> <br/>  
      <asp:TextBox ID="RequestorTitle" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="RequestorComponent">Component </label> <br/>  
      <asp:TextBox ID="RequestorComponent" runat="server" ></asp:TextBox>
      </p>

          <p> <label for="RequestorOfficeCode">    Office Code </label> <br/>  
      <asp:TextBox ID="RequestorOfficeCode" runat="server" ></asp:TextBox>
      </p>
  

         <p> <label for="RequestorEmail">Email </label> <br/>  
      <asp:TextBox ID="RequestorEmail" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="RequestorPhone">Phone</label> <br/>  
      <asp:TextBox ID="RequestorPhone" runat="server" ></asp:TextBox>
      </p>

 
       
       
    

       <p>
       <label class="bold" >Resource Summary</label> <br/>
       <label><%=resourceDescription%></label> <br/>
       <asp:Panel runat="server" ID="LicensePanel"   >
      <label>  Total Licenses: <%=resourcePasswordsAvailable%>  </label> <%=ListUsersWithAccess %> <br/>
      <label>Assigned Licenses: <%=AssignedLicenses%></label> <br/>
       <label class="bold">Available Licenses: <%=AvailableLicenses%></label> <br/>
       </asp:Panel>
       <asp:Literal runat="server" ID="ResourceDetails" ></asp:Literal>
       
       </p>

         

       <asp:Panel runat="server" ID="AdminLoginPanel" Visible="false" >
       <p>
       <h3>Administrative Login Information</h3>
      <label> Login at: <%=resourceAdminResourceURL%></label> <br/><label>Username: <%=resourceAdminUsername %>  </label> <br/>
      <label> Password: <%=resourceAdminPassword%></label> <br/></p>
       </asp:Panel>
       <p>
       <p>
       <h3>Access Status & Login Information</h3>
      <label class="bold"> Status* </label> <br/>
      <asp:DropDownList runat="server" ID="StatusDD" >
      <asp:ListItem >Active</asp:ListItem>
      <asp:ListItem >Inactive</asp:ListItem>
      </asp:DropDownList>
      </p>
     
       <p>
      <label class="bold" for="Username" >Username*</label> <br/>
       <asp:TextBox runat="server" ID="Username"   ></asp:TextBox>
       </p>
         <p>
      <label  for="Password" class="bold" >Password*</label> <br/>
       <asp:TextBox runat="server" ID="Password"   ></asp:TextBox>
       </p>


         <p>
      <label  for="InternalNotes" >Notes</label> <br/>
       <asp:TextBox runat="server" ID="InternalNotes" TextMode="MultiLine" ></asp:TextBox>
       </p>
       <asp:Panel ID="MessagingPanel" runat="server" Visible="false" >
       <h3>Messaging</h3>

         <p><asp:HiddenField runat="server" ID="RequestApprovedEmailMessageFromAddress" />
      <label  for="RequestApprovedEmailMessageSubject" >Subject</label> <br/>
       <asp:TextBox runat="server" ID="RequestApprovedEmailMessageSubject"   ></asp:TextBox>
       </p>


         <p>
      <label  for="RequestApprovedEmailMessageText" >Body Text</label> <br/>
       <asp:TextBox runat="server" ID="RequestApprovedEmailMessageText" TextMode="MultiLine" ></asp:TextBox>
       </p>

         <p>
      
       <asp:CheckBox runat="server" ID="SendEMailWhenDone" Text="Send This Email When I Save" />
       </p>
       <p>
       <label>Attach a File</label> <br/>
       <asp:FileUpload runat="server" ID="attachment" />
       </p>
       </asp:Panel>
       <p></p>
       <p>
       <asp:Button CssClass="btn" runat="server" ID="ApproveAccessBtn" 
                 onclick="ApproveAccess_Click" ></asp:Button>
       
       
       <asp:Button CssClass="btn" runat="server" ID="SaveForLater" Text="Save For Later" 
               onclick="SaveForLater_Click" ></asp:Button>
       <asp:Button CssClass="btn" runat="server" ID="Cancel" Text="Cancel" 
               onclick="Cancel_Click" ></asp:Button>
       </p>
      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

