<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="users.aspx.cs" Inherits="admin_Default2" %>
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
			<th id="header1" scope="col" class="style1"> </th>
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
          
           <asp:ListBox runat="server" ID="ResourceTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" ></asp:ListBox>
          </td>		  
		</tr>


                	    <tr>
		  <td headers="header1" class="style1">URL link to the Resource<td headers="header2"><asp:TextBox runat="server" ID="ResourceURLlink" ></asp:TextBox></td>		  
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

