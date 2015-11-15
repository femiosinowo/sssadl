<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="confirm.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/requests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
<style>

.div1, .div2 {
    display: inline-block;
    width: 10%;
}

 
 
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">

        <div id="title-bar">
    <h2>
      <span class="favorite-id">
          E-Password Requests</span> 
 

                    
                  


                </h2>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


 
<div class="grid" id="admin-tools-int-content">
	     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
         <li><a href="/admin/requests/">E-Password Requests</a></li>
         <li> ePassword Request for <%=requestorName %> to <%=resourceName %></li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      
   
   
   <h3>ePassword Request for <%=requestorName %> to <%=resourceName %>   </h3>
   <asp:Panel runat="server" ID="approvedConfirmedPanel" Visible="false" > 
   <p>This request has been approved and marked as closed , and your message has been sent.</p>
 </asp:Panel>
    <asp:Panel runat="server" ID="newRequestPanel" Visible="false" > 
   <p>This request has been created , and your message has been sent.</p>
 </asp:Panel>
 
 <asp:Panel runat="server" ID="declinedPanel" Visible="false" > 
 <p>This request has been declined and marked as closed , and your message has been sent.</p>
 </asp:Panel>

     <asp:Panel runat="server" ID="newrequestskipmessagePanel" Visible="false" > 
   <p>This request has been opened ,but your message has not been sent to the person making the request.</p>
 </asp:Panel>
 
      </div></div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

