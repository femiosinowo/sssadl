<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="declinedRequest.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/requests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
<style>

.div1, .div2 {
    display: inline-block;
    width: 10%;
}

 
 
</style><style>
 

  .accordion > p a::before {
    
    
    float:right;
}
 
 
 


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
 
 <asp:HiddenField ID="ResourceNameHF" Value="false" runat="server" />
 <asp:HiddenField ID="RequestorNameHF" Value="false" runat="server" />

 
  <asp:HiddenField ID="RequestorEmailHF"   runat="server" />
 <asp:HiddenField ID="fromEmail"   runat="server" />
<div class="grid" id="admin-tools-int-content">
	    
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      
   
   
  
    
   <h4>ePassword Request for <%=requestorName %> to <%=resourceName %>   </h4>
   <asp:HiddenField runat="server" ID="RequestorPIN" />
  

       <h3>Messaging</h3>

         <p>
      <label  for="Subject" >Subject</label> <br/>
       <asp:TextBox runat="server" ID="Subject"   ></asp:TextBox>
       </p>


         <p>
      <label  for="BodyText" >Body Text</label> <br/>
       <asp:TextBox runat="server" ID="BodyText" CssClass="html" TextMode="MultiLine" ></asp:TextBox>
       </p>

         
       <p>
       <label>Attach a File</label> <br/>
       <asp:FileUpload runat="server" ID="attachment" />
       </p>
       <p></p>
       <p>
       
       
       <asp:Button CssClass="btn" runat="server" ID="SendBtn" Text="Send" onclick="SendBtn_Click" 
                ></asp:Button>
       <asp:Button CssClass="btn" runat="server" ID="Cancel" Text="Cancel" 
               onclick="Cancel_Click" ></asp:Button>
       </p>

  <ux:AuditLog runat="server" ID="AuditLogUX" />
      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

