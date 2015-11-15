<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="admin_users_Default" %>
<%@ Register Src="~/admin/autoreply/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
     <div id="title-bar">
    <h2>
      <span class="favorite-id">
           Auto-Reply Messages</span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

<div class="grid" id="admin-tools-int-content">
	         <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
          <li> Settings</li>
         <li> Auto-Reply Messages </li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
 
         
         
       
        
       


      </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

