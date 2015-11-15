<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="confirm.aspx.cs" Inherits="admin_users_Default"  %>
<%@ Register Src="~/admin/helprequests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
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
           <%=whatInfo %> Help Request </span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


 
<div class="grid" id="admin-tools-int-content">
	    <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li><a href="/admin/helprequests/">Help Requests</a></li><li> <%=whatInfo %> Help Request</li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      
   
   

   
      <h3><%=RequestName %> Request for <%=Requestor%> </h3>  
        
        <asp:Panel runat="server" ID="successpanel" Visible="false">
         <p>
     This help request has been created and your message has been sent.
    </p>
           
      </asp:Panel>

              <asp:Panel runat="server" ID="closepanel" Visible="false">
         <p>
     This help request has been closed and your message has been sent.
    </p>
           
      </asp:Panel>
     
     
 

      
 
      </div></div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

