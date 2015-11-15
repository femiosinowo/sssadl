<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="sitemaintenance.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">

    <script>
        $(function () {
            $("#ctl00_mainContentCP_AlertStartOn").timepicki({ increase_direction: 'up' });
            $("#ctl00_mainContentCP_AlertEndOn").timepicki({ increase_direction: 'up' });
            
        });
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
     <div id="title-bar">
    <h2>
      <span class="favorite-id">
           System Maintenance Settings</span> 
 

                    
                  


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
         <li> System Maintenance Settings </li>  
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
		  <td headers="header1" class="style1"><label for="ctl00_mainContentCP_AlertStartOn">Start On</label> <td headers="header2"><asp:TextBox runat="server" ID="AlertStartOn" ></asp:TextBox></td>		  
		</tr>


           

                	    <tr>
		  <td headers="header1" class="style1"><label for="ctl00_mainContentCP_AlertEndOn"> End On</label>
		  <td headers="header2"><asp:TextBox runat="server" ID="AlertEndOn" ></asp:TextBox></td>		  
		</tr>


	    <tr>
		  <td headers="header1" class="style1"> <label for="ctl00_mainContentCP_SMTitle">Title</label> 
		  <td headers="header2"><asp:TextBox TextMode="MultiLine" Columns="170" Rows="7" runat="server" CssClass="" ID="SMTitle" ></asp:TextBox></td>
 
		  
		</tr>

        	    <tr>
		  <td headers="header1" class="style1"><label for="ctl00_mainContentCP_SMMessage">Message</label> 
		  <td headers="header2"><asp:TextBox TextMode="MultiLine" Columns="170" Rows="7" CssClass="" runat="server" ID="SMMessage" ></asp:TextBox></td>		  
		</tr>

                	    
                         
 
 
                	        <tr>
		  <td headers="header1" class="style1"><span aria-labelledby="ctl00_mainContentCP_daysOfWeeksCB_0"  > Days of Week</span></td><td headers="header2">

<asp:CheckBoxList runat="server" ID="daysOfWeeksCB" RepeatColumns="4" RepeatDirection="Horizontal"> 
<asp:ListItem Value="0" >Sun</asp:ListItem>
<asp:ListItem Value="1" >Mon</asp:ListItem>
<asp:ListItem Value="2" >Tue </asp:ListItem>
<asp:ListItem Value="3" >Wed </asp:ListItem>
<asp:ListItem Value="4" >Thur </asp:ListItem>
<asp:ListItem Value="5" >Fri</asp:ListItem>
<asp:ListItem Value="6" >Sat </asp:ListItem>

</asp:CheckBoxList>

          </td>		  
		</tr>
		 
                    	        <tr>
		  <td headers="header1" class="style1"><label for="ctl00_mainContentCP_AlertActive">Active</label> </td><td headers="header2">
       <asp:DropDownList runat="server" ID="AlertActive" ><asp:ListItem Text="Yes" Value="Y"></asp:ListItem><asp:ListItem  Text="No" Value="N"></asp:ListItem></asp:DropDownList>
          </td>		  
		</tr>
		


         
       
         
	  </tbody>
	  </table>
  </fieldset>


       
	 <fieldset>
    
 


      <asp:Button runat="server" ID="Button1" CssClass="btn" Text="Save Settings" onclick="Button1_Click" 
                   />
                      <asp:LinkButton CssClass="btn" runat="server" ID="LinkButton1" Text="Cancel" onclick="LinkButton1_Click" 
                 /> 
	  	 <asp:HiddenField runat="server" ID="WhereFrom" />
  </fieldset>





	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

