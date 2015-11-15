<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="adduser.aspx.cs" Inherits="admin_users_Default" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">

<div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  <ux:SideNav runat="server" />
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3>Create New User</h3>
 
	  
	   
	  
	  <table class="table table-bordered table-striped">
	  
	  <thead>
	    <tr>
			<th id="header1" scope="col" class="style1"> </th>
			<th id="header2" scope="col"> </th>
 
			
		</tr>
	  </thead>
	  
	  <tbody>
	    <tr>
		  <td headers="header1" class="style1">PIN
		  <td headers="header2"><asp:TextBox runat="server" ID="PIN" ></asp:TextBox></td>
 
		  
		</tr>

        	    <tr>
		  <td headers="header1" class="style1">Email Address
		  <td headers="header2"><asp:TextBox TextMode="Email" runat="server" ID="EmailAddress" ></asp:TextBox></td>		  
		</tr>

                	   


                	    <tr>
		  <td headers="header1" class="style1">First Name<td headers="header2"><asp:TextBox runat="server" ID="FirstName" ></asp:TextBox></td>		  
		</tr>


              

                	    <tr>
		  <td headers="header1" class="style1">Last Name
		  <td headers="header2"><asp:TextBox runat="server" ID="LastName" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Access Level<td headers="header2">
            <asp:DropDownList runat="server" ID="AccessLevel" ><asp:ListItem  Selected="True"  Text="Admin" Value="Admin"></asp:ListItem><asp:ListItem Text="Super" Value="Super"></asp:ListItem></asp:DropDownList>
          
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">COR<td headers="header2">
          <asp:DropDownList runat="server" ID="COR" ><asp:ListItem  Selected="True"  Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
 
                	    

                         <tr>
		  <td headers="header1" class="style1"> COR Expire Date <td headers="header2">
         <asp:TextBox runat="server" ID="CORExpireDate" TextMode="Date" ></asp:TextBox>

          </td>		  
		</tr>


        	    <tr>
		  <td headers="header1" class="style1">Active<td headers="header2">
          <asp:DropDownList runat="server" ID="Active" ><asp:ListItem  Selected="True"  Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>

		

          <tr>
		  <td headers="header1" class="style1"><td headers="header2">
         <asp:Button runat="server" ID="createUser" Text="Create User" onclick="createUser_Click" 
                    />

          </td>		  
		</tr>


	  </tbody>
	  </table>
	  
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

