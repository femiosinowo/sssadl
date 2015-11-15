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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


 
<div class="grid" id="admin-tools-int-content">
	    
	  <div class="row-12">
 
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      
   
   
   
   <asp:Panel runat="server" ID="newPanel" Visible="false" > 
   <h3>Resource <%=ResourceName %> has been created   </h3>
   
 </asp:Panel>
    <asp:Panel runat="server" ID="editPanel" Visible="false" > 
    <h3>Resource <%=ResourceName %> has been modified successfully   </h3>
    
 </asp:Panel>
 
 <asp:Panel runat="server" ID="declinedPanel" Visible="false" > 
  
 </asp:Panel>

     <asp:Panel runat="server" ID="newrequestskipmessagePanel" Visible="false" > 
   
 </asp:Panel>
 
      </div></div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

