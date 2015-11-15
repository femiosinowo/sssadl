<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Copy of view.aspx.cs" Inherits="admin_users_Default" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
<span id="AjaxResult" ></span>
<div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3>Edit User Profile</h3>
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




 <p>
   
 <fieldset>
		<legend class=""> Contracting Officer's Representative (COR) </legend>
		<input type="radio" runat="server" checked="" id="CORYes" value="Yes" name="COR">  <label class="inline-label" for="ctl00_mainContentCP_CORYes">Yes:</label>
      Expires On  <asp:TextBox runat="server" ID="CORExpiresOn" TextMode="Date" Width="150" ></asp:TextBox>
        <br>
		<input type="radio" runat="server" id="CORNo" value="No" name="COR"> <label class="inline-label" for="ctl00_mainContentCP_CORNo">No</label>
		</fieldset>

      </p>



 <p>
       <fieldset>
       <label   for="ctl00_mainContentCP_Active">Employee is
		</label><br />

      <asp:RadioButtonList ID="Active"  runat="server" CssClass="inline-label" RepeatLayout="flow" >
      
      <asp:ListItem  Selected="True"  Text="Active" Value="Active"></asp:ListItem><asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>


      </asp:RadioButtonList>
     </fieldset>
   
      </p>

      <p>
       <asp:Button runat="server" ID="createUser" Text="Save" onclick="createUser_Click" />  <input   id="Text1" value="Cancel"  type="submit" style="width:150px" /> 
       </p>

	   
	  
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

