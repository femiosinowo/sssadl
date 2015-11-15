<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="add1.aspx.cs" Inherits="admin_Default2" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>



<asp:Content ID="Content4" ContentPlaceHolderID="pageTitleCP" Runat="Server">
    <div id="title-bar"><h2>Unique Visitors and Total Hits</h2></div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 181px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentCP" Runat="Server">

    <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  <ux:SideNav runat="server" />
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3>Create New Resource</h3>
 
	  
	   
	  
	  <table class="table table-bordered table-striped">
	  <caption><h3></h3></caption>
	  
	  <thead>
	    <tr>
			<th id="header1" scope="col" class="style1"><asp:Literal runat="server" ID="ltlMessage" ></asp:Literal></th>
			<th id="header2" scope="col"> </th>
 
			
		</tr>
	  </thead>
	  
	  <tbody>
	    <tr>
		  <td headers="header1" class="style1">Resource Name
		  <td headers="header2"><asp:TextBox runat="server" ID="ResourceName" ></asp:TextBox></td>
 
		  
		</tr>

        	    <tr>
		  <td headers="header1" class="style1">Resource Description
		  <td headers="header2"><asp:TextBox TextMode="MultiLine" runat="server" ID="Description" ></asp:TextBox></td>		  
		</tr>

                	    <tr>
		  <td headers="header1" class="style1">Resource Type (from Ektron Taxonomy)<td headers="header2"> 
          
          
           <asp:DropDownList runat="server" ID="ResourceTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" ></asp:DropDownList>
          </td>		  
		</tr>


                	    <tr>
		  <td headers="header1" class="style1">URL link to the Resource<td headers="header2"><asp:TextBox runat="server" ID="ResourceURLlink" ></asp:TextBox></td>		  
		</tr>


                	    <tr>
		  <td headers="header1" class="style1">Show resource in new window
		  <td headers="header2">
          <asp:DropDownList runat="server" ID="ShowInNewWindow" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>

                	    <tr>
		  <td headers="header1" class="style1">Admin URL link to resource
		  <td headers="header2"><asp:TextBox runat="server" ID="AdminResourceURL" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Admin Username<td headers="header2"><asp:TextBox runat="server" ID="AdminUsername" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Admin Password<td headers="header2"><asp:TextBox runat="server" ID="AdminPassword" TextMode="Password" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">File 1<td headers="header2"> 
          <asp:FileUpload ID="File1" runat="server" />
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">File 2<td headers="header2"> <asp:FileUpload ID="File2" runat="server" /></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">File 3<td headers="header2"> <asp:FileUpload ID="File3" runat="server" /></td>		  
		</tr>

                	    <tr>
		  <td headers="header1" class="style1">Subject Areas (from Ektron Smart Form)
		  <td headers="header2"> <asp:ListBox runat="server" ID="SubjectAreasTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" ></asp:ListBox>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in subject areas
		  <td headers="header2"> <asp:DropDownList runat="server" ID="ShowInSubjectAreas" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Databases A-Z
		  <td headers="header2"><asp:DropDownList runat="server" ID="ShowInDatabases" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Training Request Form
		  <td headers="header2"><asp:DropDownList runat="server" ID="ShowInTrainingRequestForm" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Audience Tools (select from list of 
              audience terms defined in taxonomy)
		  <td headers="header2"><asp:DropDownList runat="server" ID="ShowInAudienceToolsTaxonomy" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Mandatory
		  <td headers="header2"><asp:DropDownList runat="server" ID="Mandatory" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Associated Network
		  <td headers="header2"><asp:TextBox runat="server" ID="AssociatedNetwork" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Access Type (from Resource Access Type 
              taxonomy)<td headers="header2">
               <asp:ListBox runat="server" ID="AccessTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" ></asp:ListBox>
              </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Resource Registration Instructions
		  <td headers="header2"><asp:TextBox runat="server" ID="ResourceRegistrationInstructions" TextMode="MultiLine" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Shared Username
		  <td headers="header2"><asp:TextBox runat="server" ID="SharedUsername" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Shared Password<td headers="header2"><asp:TextBox runat="server" ID="SharedPassword" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show Login to<td headers="header2">
          <asp:DropDownList runat="server" ID="ShowLogin" ><asp:ListItem Text="All" Selected="True" Value="All"></asp:ListItem>
          <asp:ListItem  Text="Only DDS employees" Value="OnlyDDSEmployees"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Limited number of passwords available<td headers="header2">
          <asp:DropDownList runat="server" ID="LimitedNumberPasswordsAvailable" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
 
                	    <tr>
		  <td headers="header1" class="style1">Number of passwords available
		  <td headers="header2"><asp:TextBox runat="server" ID="PasswordsAvailable" TextMode="Number" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Send e-password requests to
		  <td headers="header2"><asp:ListBox runat="server" ID="SendEpasswordTo"  DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120" ></asp:ListBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Password Requests restricted to managers<td headers="header2">
       <asp:DropDownList runat="server" ID="PasswordRequestsRestrictedToManagers" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem Selected="True" Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Target users<td headers="header2"><asp:TextBox runat="server" ID="TargetUsers" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Business Purpose of resource
		  <td headers="header2"><asp:TextBox runat="server" ID="BusinessPurposeOfResource" ></asp:TextBox></td>		  
		</tr>
        
                	    <tr>
		  <td headers="header1" class="style1">Resource Display Status<td headers="header2">
          <asp:DropDownList runat="server" ID="ResourceDisplayStatus" >
          <asp:ListItem Selected="True" Text="Enabled" Value="Enabled"></asp:ListItem>
          <asp:ListItem  Text="Inactive" Value="Inactive"></asp:ListItem>
          <asp:ListItem   Text="Temporarily Disabled" Value="Temporarily Disabled"></asp:ListItem>
          </asp:DropDownList>

          </td>		  
		</tr>
                	    
		

          <tr>
		  <td headers="header1" class="style1"><td headers="header2">
         <asp:Button runat="server" ID="createResource" Text="Create Resource" 
                  onclick="createResource_Click" />

          </td>		  
		</tr>


	  </tbody>
	  </table>
	  
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
	  <!-- END GRID -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

