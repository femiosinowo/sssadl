<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="systemsettings.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">

    <script>
        $(function () {
            $("#ctl00_mainContentCP_AlertStartOn").datepicker();
            $("#ctl00_mainContentCP_AlertEndOn").datepicker();
            
        });
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
     <div id="title-bar">
    <h2>
      <span class="favorite-id">
           System Alert Settings</span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">

<div class="grid" id="admin-tools-int-content">
	  	  <div class="grid" id="admin-tools-int-content">
	         <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
          <li> Settings</li>
         <li> System Alert Settings </li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	   
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  
 
	     <asp:Panel runat="server" ID="SaveConfirm" Visible="false" >
	    <div class="container-blue">
		<h4>Settings Saved</h4>
		</div>
	   </asp:Panel>
	   
	  
<fieldset>
     
<table class="table table-bordered table-striped">
	 
	  
	  <tbody>
	    <tr>
		  <td headers="header1" class="style1">Alert Title
		  <td headers="header2"><asp:TextBox runat="server" ID="AlertTitle" ></asp:TextBox></td>
 
		  
		</tr>

        	    <tr>
		  <td headers="header1" class="style1">Alert Description
		  <td headers="header2"><asp:TextBox TextMode="MultiLine" runat="server" ID="AlertDescription" ></asp:TextBox></td>		  
		</tr>

                	    


                	    <tr>
		  <td headers="header1" class="style1">Start On<td headers="header2"><asp:TextBox runat="server" ID="AlertStartOn" ></asp:TextBox></td>		  
		</tr>


           

                	    <tr>
		  <td headers="header1" class="style1">End On
		  <td headers="header2"><asp:TextBox runat="server" ID="AlertEndOn" ></asp:TextBox></td>		  
		</tr>
 
                	        <tr>
		  <td headers="header1" class="style1">Active</td><td headers="header2">
       <asp:DropDownList runat="server" ID="AlertActive" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem   Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
		











	  </tbody>
	  </table>
  </fieldset>
         
  
	 
	    


       
	 <fieldset>
    
<table class="table table-bordered table-striped">
	   
 
	  
	  <tbody>
 



         
          <tr align="right">
		  <td headers="header2" align="right">
         <asp:Button runat="server" CssClass="btn" ID="Button1" Text="Save Settings" onclick="Button1_Click" 
                   />
                 <asp:LinkButton CssClass="btn" runat="server" ID="LinkButton1" Text="Cancel" onclick="LinkButton1_Click" 
                 /> 
	  	 <asp:HiddenField runat="server" ID="WhereFrom" />

          </td>		  
		</tr>
         
	  </tbody>
	  </table>
  </fieldset>





	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

