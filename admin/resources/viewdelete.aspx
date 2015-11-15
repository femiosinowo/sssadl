<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewdelete.aspx.cs" Inherits="admin_resources_view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../framework/css/phoenix.css" rel="stylesheet" media="all" />
     <link href="../framework/css/digital.css" type="text/css" rel="stylesheet" media="all" /> 

         <style type="text/css">
        .style1
        {
            width: 30%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
		  <td headers="header2"><asp:TextBox runat="server" ID="ResourceName" Enabled="false" ></asp:TextBox></td>
 
		  
		</tr>

        	    <tr>
		  <td headers="header1" class="style1">Resource Description
		  <td headers="header2"><asp:TextBox TextMode="MultiLine" runat="server" ID="Description" Enabled="false" ></asp:TextBox></td>		  
		</tr>

                	    <tr>
		  <td headers="header1" class="style1">Resource Type  <td headers="header2"> 
          
          
           <asp:DropDownList Enabled="false"  runat="server" ID="ResourceTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID"  ></asp:DropDownList>
          </td>		  
		</tr>


                	    <tr>
		  <td headers="header1" class="style1">URL link to the Resource<td headers="header2"><asp:TextBox runat="server" ID="ResourceURLlink" Enabled="false" ></asp:TextBox></td>		  
		</tr>


                	    <tr>
		  <td headers="header1" class="style1">Show resource in new window
		  <td headers="header2">
          <asp:DropDownList Enabled="false"  runat="server" ID="ShowInNewWindow" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>

                	    <tr>
		  <td headers="header1" class="style1">Admin URL link to resource
		  <td headers="header2"><asp:TextBox runat="server" ID="AdminResourceURL" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Admin Username<td headers="header2"><asp:TextBox runat="server" ID="AdminUsername" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Admin Password<td headers="header2"><asp:TextBox runat="server" ID="AdminPassword" TextMode="Password" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">File 1<td headers="header2"> 
          <asp:Label runat="server" ID="file1Label" ></asp:Label>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">File 2<td headers="header2"> <asp:Label runat="server" ID="file2Label" ></asp:Label>  </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">File 3<td headers="header2"> <asp:Label runat="server" ID="file3Label" ></asp:Label> </td>		  
		</tr>

                	    <tr>
		  <td headers="header1" class="style1">Subject Areas  
		  <td headers="header2"><asp:Label runat="server" ID="SubjectAreasTaxonomyLabel" ></asp:Label> <asp:ListBox runat="server" Visible="false" ID="SubjectAreasTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" ></asp:ListBox>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in subject areas
		  <td headers="header2"> <asp:DropDownList Enabled="false"  runat="server" ID="ShowInSubjectAreas" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Databases A-Z
		  <td headers="header2"><asp:DropDownList Enabled="false"  runat="server" ID="ShowInDatabases" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Training Request Form
		  <td headers="header2"><asp:DropDownList Enabled="false"  runat="server" ID="ShowInTrainingRequestForm" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Audience Tools  
		  <td headers="header2"><asp:DropDownList Enabled="false"  runat="server" ID="ShowInAudienceToolsTaxonomy" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Mandatory
		  <td headers="header2"><asp:DropDownList Enabled="false"  runat="server" ID="Mandatory" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Associated Network
		  <td headers="header2"><asp:TextBox runat="server" ID="AssociatedNetwork" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Access Type <td headers="header2">
          
          <asp:Label runat="server" ID="AccessTypeTaxonomyLabel" ></asp:Label>
               <asp:ListBox Visible="false" runat="server" ID="AccessTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" ></asp:ListBox>
              </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Resource Registration Instructions
		  <td headers="header2"><asp:TextBox runat="server" ID="ResourceRegistrationInstructions" TextMode="MultiLine" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Shared Username
		  <td headers="header2"><asp:TextBox runat="server" ID="SharedUsername" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Shared Password<td headers="header2"><asp:TextBox runat="server" ID="SharedPassword" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show Login to<td headers="header2">
          <asp:DropDownList Enabled="false"  runat="server" ID="ShowLogin" ><asp:ListItem Text="All"  Value="All"></asp:ListItem>
          <asp:ListItem  Text="Only DDS employees" Value="OnlyDDSEmployees"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Limited number of passwords available<td headers="header2">
          <asp:DropDownList Enabled="false"  runat="server" ID="LimitedNumberPasswordsAvailable" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
 
                	    <tr>
		  <td headers="header1" class="style1">Number of passwords available
		  <td headers="header2"><asp:TextBox runat="server" ID="PasswordsAvailable" TextMode="Number" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Send e-password requests to
		  <td headers="header2">
          <asp:Label runat="server" ID="SendEpasswordToLabel" ></asp:Label>
          <asp:ListBox runat="server" Visible="false" ID="SendEpasswordTo"  DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120" ></asp:ListBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Password Requests restricted to managers<td headers="header2">
       <asp:DropDownList Enabled="false"  runat="server" ID="PasswordRequestsRestrictedToManagers" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Target users<td headers="header2"><asp:TextBox runat="server" ID="TargetUsers" Enabled="false" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Business Purpose of resource
		  <td headers="header2"><asp:TextBox runat="server" ID="BusinessPurposeOfResource" Enabled="false" ></asp:TextBox></td>		  
		</tr>
        
                	    <tr>
		  <td headers="header1" class="style1">Resource Display Status<td headers="header2">
          <asp:DropDownList Enabled="false"  runat="server" ID="ResourceDisplayStatus" >
          <asp:ListItem  Text="Enabled" Value="Enabled"></asp:ListItem>
          <asp:ListItem  Text="Inactive" Value="Inactive"></asp:ListItem>
          <asp:ListItem   Text="Temporarily Disabled" Value="Temporarily Disabled"></asp:ListItem>
          </asp:DropDownList>

          </td>		  
		</tr>
                	    
		

          <tr>
		  <td headers="header1" class="style1"><td headers="header2">
      

          </td>		  
		</tr>


	  </tbody>
	  </table>
    </form>
</body>
</html>
