<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="admin_users_Default" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">

<style>
 
label  {  padding-left: 5px; display: inline !important; }
</style>
    <script>
        $(function () {

            $("#ctl00_mainContentCP_CORExpiresOn").datepicker();


            //////////////////Update user

            $('#updateuserBtn').click(function () {
                var PIN = $('#ctl00_mainContentCP_PIN').val();
                var url = "/admin/users/ajaxgetUsers.aspx?PIN=" + PIN;

                $.get(url, function (data) {
                    $("#AjaxResult").html(data);

                });

            });

            //////////////////End Update user

        });
  </script>
  
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
     <div id="title-bar">
    <h2>
      <span class="favorite-id">
         Edit User Profile</span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
<span id="AjaxResult" ></span>
<div class="grid" id="admin-tools-int-content">
	    <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
          <li><a href="/admin/users/">Users</a></li>
         <li>Edit User Profile</li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  

      <asp:Panel ID="successedit" runat="server" Visible="false"  >
      
      <div class="container-green">
	     <h4>Your changes has been saved succesfully.</h4>
		  
	  <p><a href='/admin/users/' > Back to Users List</a></p>
	  
	  </div>
      </asp:Panel>

        <asp:Panel ID="errorEdit" runat="server" Visible="false"  >
      
      <div class="container-red">
	     
		
	    <h4><%=ErrorHeader %></h4>
		<p><%=ErrorMessage %></p>
	  
	  </div>
      </asp:Panel>

 <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_PIN">
		    PIN<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="PIN" Enabled="false" ></asp:TextBox> <input type="button"   id="updateuserBtn" value="Update User" class="btn" style="width:150px" /> 
	  </p>

      <p>
      
       <label class="bold" for="ctl00_mainContentCP_EmailAddress">
		    Email Address<span title="Required" class="fg-red">*</span>
		</label><br />
      <asp:TextBox TextMode="Email" runat="server" Enabled="false" ID="EmailAddress" ></asp:TextBox> 
      </p>

       <p>
      
       <label class="bold" for="ctl00_mainContentCP_FirstName">
		   First Name<span title="Required" class="fg-red">*</span>
		</label><br />
      <asp:TextBox runat="server" ID="FirstName"  ></asp:TextBox>
      </p>



       <p>
    
       <label class="bold" for="ctl00_mainContentCP_LastName">Last Name<span title="Required" class="fg-red">*</span>
		</label><br />
      <asp:TextBox runat="server" ID="LastName" ></asp:TextBox>
      </p>




 <p>
      
      <label   for="ctl00_mainContentCP_AccessLevel">Access Level
		</label><br />

     <asp:DropDownList runat="server" ID="AccessLevel" ><asp:ListItem  Selected="True"  Text="Administrator" Value="Administrator"></asp:ListItem><asp:ListItem Text="Super Administrator" Value="Super Administrator"></asp:ListItem></asp:DropDownList>
        
      </p>




 <fieldset>
		<legend class=""> Contracting Officer's Representative (COR) </legend>
		<input type="radio" runat="server"   id="CORYes" value="Yes" name="COR">  <label class="inline-label" for="ctl00_mainContentCP_CORYes">Yes:</label>
      Expires On  <asp:TextBox runat="server" ID="CORExpiresOn"   Width="150" ></asp:TextBox>
        <br>
		<input type="radio" runat="server" id="CORNo" value="No" name="COR"> <label class="inline-label" for="ctl00_mainContentCP_CORNo">No</label>
		</fieldset>

      </p>



       <fieldset>
       <label   for="ctl00_mainContentCP_Active">Employee is
		</label><br />

      <asp:RadioButtonList ID="EmployeeActive"  runat="server"   RepeatLayout="flow" >
      
      <asp:ListItem   Text="Active" Value="Y"></asp:ListItem><asp:ListItem Text="Inactive" Value="N"></asp:ListItem>


      </asp:RadioButtonList>
     </fieldset>
   
      </p>

      <p>
       <asp:Button runat="server" ID="updateUser" Text="Save" class="btn" onclick="updateUser_Click" />  <input class="btn" onclick="history.back(-1)"  id="Text1" value="Cancel"  type="button" style="width:150px" /> 
       </p>

	   <p>
            &nbsp;<ux:AuditLog runat="server" ID="AuditLogUX" />
	  
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

