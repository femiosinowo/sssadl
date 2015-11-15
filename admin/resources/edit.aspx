<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="admin_resources_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <%=ResourceNameDB%>
    <asp:HiddenField  ID="ResourceID"   runat="server" />

    <asp:HiddenField ID="ResourceNameHF"    runat="server" />

    <asp:HiddenField ID="DescriptionHF"   runat="server" />

    <asp:HiddenField ID="ResourceTypeTaxonomyHF"     runat="server" />

    <asp:HiddenField ID="ShowInNewWindowHF"   runat="server" />

    <asp:HiddenField ID="AdminUsernameHF"   runat="server" />

    <asp:HiddenField ID="AdminResourceURLHF"     runat="server" />

    <asp:HiddenField ID="ResourceURLlinkHF"   runat="server" />

    <asp:HiddenField ID="AdminPasswordHF"     runat="server" />

    <asp:HiddenField ID="File1HF"     runat="server" />

    <asp:HiddenField ID="File2HF"    runat="server" />

    <asp:HiddenField ID="File3HF"   runat="server" />

    <asp:HiddenField ID="SubjectAreasTaxonomyHF"     runat="server" />

    <asp:HiddenField ID="ShowInSubjectAreasHF"     runat="server" />

    <asp:HiddenField ID="ShowInDatabasesHF"    runat="server" />

    <asp:HiddenField ID="ShowInTrainingRequestFormHF"    runat="server" />

    <asp:HiddenField ID="ShowInAudienceToolsTaxonomyHF"     runat="server" />

    <asp:HiddenField ID="MandatoryHF"    runat="server" />

    <asp:HiddenField ID="AssociatedNetworkHF"     runat="server" />

    <asp:HiddenField ID="AccessTypeTaxonomyHF"     runat="server" />

    <asp:HiddenField ID="ResourceRegistrationInstructionsHF"     runat="server" />

    <asp:HiddenField ID="SharedUsernameHF"    runat="server" />

    <asp:HiddenField ID="SharedPasswordHF"     runat="server" />

    <asp:HiddenField ID="ShowLoginHF"     runat="server" />

    <asp:HiddenField ID="LimitedNumberPasswordsAvailableHF"     runat="server" />

    <asp:HiddenField ID="PasswordsAvailableHF"     runat="server" />

    <asp:HiddenField ID="SendEpasswordToHF"    runat="server" />

    <asp:HiddenField ID="PasswordRequestsRestrictedToManagersHF"   runat="server" />

    <asp:HiddenField ID="TargetUsersHF"    runat="server" />

    <asp:HiddenField ID="BusinessPurposeOfResourceHF"   runat="server" />

    <asp:HiddenField ID="ResourceDisplayStatusHF"     runat="server" />

    <table class="table table-bordered table-striped">
	  <caption><h3>Editing <%=ResourceNameS%></h3></caption>
	  
	  <thead>
	    <tr>
			<th id="header1" scope="col" class="style1"><asp:Literal runat="server" ID="ltlMessage" ></asp:Literal></th>
			<th id="header2" scope="col"> </th>
 
			
		</tr>
	  </thead>
	  
	  <tbody>
	    <tr>
		  <td headers="header1" class="style1">Resource Name
		  <td headers="header2"><asp:TextBox runat="server" ID="ResourceName" OnTextChanged="TrackChanges"  ></asp:TextBox></td>
 
		  
		</tr>

        	    <tr>
		  <td headers="header1" class="style1">Resource Description
		  <td headers="header2"><asp:TextBox TextMode="MultiLine" runat="server" ID="Description" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>

                	    <tr>
		  <td headers="header1" class="style1">Resource Type  <td headers="header2"> 
          
          
           <asp:DropDownList runat="server" ID="ResourceTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID"  OnTextChanged="TrackChanges2" ></asp:DropDownList>
          </td>		  
		</tr>


                	    <tr>
		  <td headers="header1" class="style1">URL link to the Resource<td headers="header2"><asp:TextBox runat="server" ID="ResourceURLlink" OnTextChanged="TrackChanges" ></asp:TextBox></td>		  
		</tr>


                	    <tr>
		  <td headers="header1" class="style1">Show resource in new window
		  <td headers="header2">
          <asp:DropDownList runat="server" ID="ShowInNewWindow" OnTextChanged="TrackChanges2"   ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>

                	    <tr>
		  <td headers="header1" class="style1">Admin URL link to resource
		  <td headers="header2"><asp:TextBox runat="server" ID="AdminResourceURL" OnTextChanged="TrackChanges" ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Admin Username<td headers="header2"><asp:TextBox runat="server" ID="AdminUsername" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Admin Password<td headers="header2"><asp:TextBox runat="server" ID="AdminPassword"  OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
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
		  <td headers="header1" class="style1">Subject Areas  
		  <td headers="header2"> <asp:ListBox runat="server" ID="SubjectAreasTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120"  OnTextChanged="TrackChanges3" ></asp:ListBox>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in subject areas
		  <td headers="header2"> <asp:DropDownList runat="server" ID="ShowInSubjectAreas" OnTextChanged="TrackChanges2" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Databases A-Z
		  <td headers="header2"><asp:DropDownList runat="server" ID="ShowInDatabases" OnTextChanged="TrackChanges2" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Training Request Form
		  <td headers="header2"><asp:DropDownList runat="server" ID="ShowInTrainingRequestForm" OnTextChanged="TrackChanges2" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show in Audience Tools 
		  <td headers="header2"><asp:DropDownList runat="server" ID="ShowInAudienceToolsTaxonomy" OnTextChanged="TrackChanges2"><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Mandatory
		  <td headers="header2"><asp:DropDownList runat="server" ID="Mandatory" OnTextChanged="TrackChanges2"><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Associated Network
		  <td headers="header2"><asp:TextBox runat="server" ID="AssociatedNetwork" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Access Type  <td headers="header2">
               <asp:ListBox runat="server" ID="AccessTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120"  OnTextChanged="TrackChanges3" ></asp:ListBox>
              </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Resource Registration Instructions
		  <td headers="header2"><asp:TextBox runat="server" ID="ResourceRegistrationInstructions" TextMode="MultiLine" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Shared Username
		  <td headers="header2"><asp:TextBox runat="server" ID="SharedUsername" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Shared Password<td headers="header2"><asp:TextBox runat="server" ID="SharedPassword" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Show Login to<td headers="header2">
          <asp:DropDownList runat="server" ID="ShowLogin" OnTextChanged="TrackChanges2" ><asp:ListItem Text="All"  Value="All"></asp:ListItem>
          <asp:ListItem  Text="Only DDS employees" Value="OnlyDDSEmployees"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Limited number of passwords available<td headers="header2">
          <asp:DropDownList runat="server" ID="LimitedNumberPasswordsAvailable" OnTextChanged="TrackChanges2"><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
 
                	    <tr>
		  <td headers="header1" class="style1">Number of passwords available
		  <td headers="header2"><asp:TextBox runat="server" ID="PasswordsAvailable" TextMode="Number" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Send e-password requests to
		  <td headers="header2"><asp:ListBox runat="server" ID="SendEpasswordTo"  DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120" OnTextChanged="TrackChanges3" ></asp:ListBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Password Requests restricted to managers<td headers="header2">
       <asp:DropDownList runat="server" ID="PasswordRequestsRestrictedToManagers" OnTextChanged="TrackChanges2"><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Target users<td headers="header2"><asp:TextBox runat="server" ID="TargetUsers" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>
                	    <tr>
		  <td headers="header1" class="style1">Business Purpose of resource
		  <td headers="header2"><asp:TextBox runat="server" ID="BusinessPurposeOfResource" OnTextChanged="TrackChanges"  ></asp:TextBox></td>		  
		</tr>
        
                	    <tr>
		  <td headers="header1" class="style1">Resource Display Status<td headers="header2">
          <asp:DropDownList OnTextChanged="TrackChanges2" runat="server" ID="ResourceDisplayStatus" >
          <asp:ListItem  Text="Enabled" Value="Enabled"></asp:ListItem>
          <asp:ListItem  Text="Inactive" Value="Inactive"></asp:ListItem>
          <asp:ListItem   Text="Temporarily Disabled" Value="Temporarily Disabled"></asp:ListItem>
          </asp:DropDownList>

          </td>		  
		</tr>
                	    
		

          <tr>
		  <td headers="header1" class="style1">  <asp:Button runat="server" ID="cancel" Text="Cancel" 
                   /> <td headers="header2">
      
        <asp:Button runat="server" ID="editResource" Text="Edit Resource" 
                  onclick="createResource_Click" />
          </td>		  
		</tr>


	  </tbody>
	  </table>
    </form>
</body>
</html>
