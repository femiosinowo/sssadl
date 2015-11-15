<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="assignees.aspx.cs" Inherits="admin_users_Default"  %>
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
           Send Help Requests To </span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


 
<div class="grid" id="admin-tools-int-content">
	     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
         <li><a href="/admin/helprequests/">Help Requests</a></li>
         <li>  Send Help Requests To </li>  
    </ul> 

   </div>
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     <asp:Panel ID="AreaShow" runat="server" Visible="false" >
      <div>
      
   
   

   
      <h3><%=FormTitle %></h3>  
        
         <p>
      <label class="bold" for="ctl00_mainContentCP_SendReqeustsTo" >Send Requests To</label> <br/>
    <asp:ListBox runat="server" ID="SendReqeustsTo"  DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120" CssClass="required" ToolTip="Select who to send password requests to" ></asp:ListBox> <br />
         <a href="javascript:selectAll('ctl00_mainContentCP_SendReqeustsTo')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_SendReqeustsTo')">Select None</a>
         <br />
    </p>
           
      


     
     
     
     <p>
     
     <asp:Button ID="SaveBtn" class="btn" runat="server" Text="Save" onclick="SaveBtn_Click" />   
         <asp:Button ID="CancelBtn"  class="btn" runat="server" Text="Cancel" 
             onclick="CancelBtn_Click" /> 
     </p>

        <ux:AuditLog runat="server" ID="AuditLogUX" />


      
 
      </div>
      </asp:Panel></div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

