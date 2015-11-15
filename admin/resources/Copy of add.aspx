<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Copy of add.aspx.cs" Inherits="admin_users_Default" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">

<style>
 
label  {  padding-left: 5px; display: inline !important; }


h3 {
  border-bottom: 4px solid #cccccc;
  padding-bottom: 5px;
  padding-top: 5px;
}
input, select {
    height: auto;
}

.date1, .date2 {
    display: inline-block;
    width: 44%;
}

input[type="file"] {
   
    border: none;
    
}
label.error 
{
  display: table-cell !important; 
    padding-left: 5px;
    font-style: italic;
    font-size: small;
    color: Red;   
}
input.error , select.error , textarea.error
{
   border: 1px solid Red !important;
}

.required {
    color: #333 !important;
}


.block {
		display: block;
	}
	  
 
 




</style>
    <script>
        $(function () {

            //  $("#aspnetForm").validate();
            $("#ctl00_mainContentCP_NewFeatureEndDate").datepicker();
            $("#ctl00_mainContentCP_NewFeatureStartDate").datepicker();

            /////////////////////////////////////////////////////////// Start AccessTypeTaxonom
            $("#ctl00_mainContentCP_AccessTypeTaxonomy").change(function () {

                $("#ePassDiv").hide();
                $("#selfRegisterDiv").hide();
                $("#SharedLoginDiv").hide();


                strId = this.value;
                if (strId == 119) return; //public

                switch (strId) {

                    case "122":  //122 - Epassword
                        $("#ePassDiv").show();
                        break;

                    case "120":  //120 - Self Registered 
                        $("#selfRegisterDiv").show();
                        break;

                    case "121": //121 - Shared Login
                        $("#SharedLoginDiv").show();
                        break;

                }
                //122 - Epassword
                //120 - Self Registered 
                //121 - Shared Login
            });


            ////////////////////////////// ResourceTypeTaxonomy change



            $("#ctl00_mainContentCP_ResourceTypeTaxonomy").change(function () {
                //124 Databases
                //125 Journals
                //126 Articles
                //127 eBooks
                //128 Other Resources

                $("#LinktotheResourceDiv").hide();
                $("#uploadafileDiv").hide();

                var taxId = this.value;
                //  if (strId == 119) return; //public

                switch (taxId) {
                    case "124":
                    case "125":
                        $("#LinktotheResourceDiv").show();
                        break;

                    case "126":
                    case "127":
                    case "128":
                        $("#uploadafileDiv").show();
                        break;



                }

            });








            ///////////////////////////////////////// End ResourceTypeTaxonomy 

            $("#ctl00_mainContentCP_ApplicationIs_0").addClass("required");
            $("#ctl00_mainContentCP_ShowLogin_0").addClass("required");
            //////////////Resource File Validation
            $("#ctl00_mainContentCP_uploadFileRd").click(function () {
                $("#ctl00_mainContentCP_ResouceLink").removeClass("required");
                $("#ctl00_mainContentCP_ResourceFileUpload").removeClass("required");

                if ($('#ctl00_mainContentCP_uploadFileRd').is(':checked')) {
                    $("#ctl00_mainContentCP_ResourceFileUpload").addClass("required");
                }  
            });

            $("#ctl00_mainContentCP_ResourceLinkRd").click(function () {

                $("#ctl00_mainContentCP_ResouceLink").removeClass("required");
                $("#ctl00_mainContentCP_ResourceFileUpload").removeClass("required");

                if ($('#ctl00_mainContentCP_ResourceLinkRd').is(':checked')) {
                    $("#ctl00_mainContentCP_ResouceLink").addClass("required");
                }  
            });

            ///////////////// End Resource file validation

            ////////////////////////Epass limited password
            $("#ctl00_mainContentCP_limitedTo").addClass("required");

            $("#ctl00_mainContentCP_limitedTo").click(function () {
                $("#ctl00_mainContentCP_LimitedNumberPasswordsAvailable").removeClass("required");              

                if ($('#ctl00_mainContentCP_limitedTo').is(':checked')) {
                    $("#ctl00_mainContentCP_LimitedNumberPasswordsAvailable").addClass("required");
                }
            });

            $("#ctl00_mainContentCP_Unlimited").click(function () {

                $("#ctl00_mainContentCP_LimitedNumberPasswordsAvailable").removeClass("required");

                
            });

            ////////////////////////////////////////////////////Epass end


            /////////////////Select all checkboxes
//            $('#select_all').click(function () {
//                $('#countries option').prop('selected', true);
//            });

            


/////////////////////////////////////

            $('#tabs').tabs().addClass('ui-tabs-vertical ui-helper-clearfix');






            //add static tabs
            $("#addstatictab").dynatabs({
                tabBodyID: 'addstatictabbody',
                showCloseBtn: true,
                confirmDelete: true
            });





        });
        function Text1_onclick() {
            location.href = "/admin/users/";
        }

        function selectAll(whatSelect) {
        
            $(function () {
                $('select#' + whatSelect +' > option').prop('selected', 'selected');
            });

        }

        function selectNone(whatSelect) {

            $(function () {
                $('select#' + whatSelect + ' > option').prop('selected', false);
            });

        }

 
        jQuery(function () {
            // You can specify some validation options here but not rules and messages
            jQuery('#aspnetForm').validate();
            // Add a custom class to your name mangled input and add rules like this
            jQuery('.required').rules('add', {
                required: true,
                messages: {
                   // required: 'Some custom message for the username required field'
                }
            });

 

        });


        function addNewStaticTab() {
            $.addDynaTab({
                tabID: 'addstatictab',
                type: 'html',
                html: '<p>This HTML content is loaded statically</p>',
                params: {},
                tabTitle: 'New Static Tab'
            });
        }
      


    </script>


    	      <link rel="stylesheet" href="/admin/framework/css/tabs.css">
	<script type="text/javascript">

	    $(document).ready(function () {


	        //closable tabs
	        $("#closabletab1").dynatabs({
	            tabBodyID: "closabletabbody1",
	            showCloseBtn: true
	        });

	        //tabs with custom close confirmation message call backs
	        $("#closeconfirmtabs").dynatabs({
	            tabBodyID: "closeconfirmtabsbody",
	            showCloseBtn: true,
	            confirmDelete: true,
	            confirmMessage: 'Do you want to delete this tab?'
	        });


	        //add hidden tabs example
	        $("#adddivtab").dynatabs({
	            tabBodyID: 'adddivtabbody',
	            showCloseBtn: true,
	            confirmDelete: true
	        });


	    });



	    function addNewDivTab() {
	        $.addDynaTab({
	            tabID: 'adddivtab',
	            type: 'div',
	            divID: 'hdnDataTabDiv',
	            params: {},
	            tabTitle: 'New Div Tab'
	        });
	    }
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
<span id="AjaxResult" ></span>
<asp:HiddenField runat="server" ID="LastNameHF" />
<asp:HiddenField runat="server" ID="FirstNameHF" />
<asp:HiddenField runat="server" ID="EmailAddressHF" />
 
<div class="grid" id="admin-tools-int-content">

	  
	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9" >
	  
	  <h3>Edit User Profile</h3>

   <%--   <asp:ValidationSummary ID="VSumm" runat="server" CssClass="container-red" />
      
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  SetFocusOnError="true" ControlToValidate="ResourceName" runat="server" Display="None" ErrorMessage="Resource Name cannot be blank" ></asp:RequiredFieldValidator>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  SetFocusOnError="true" ControlToValidate="Description" runat="server" Display="None" ErrorMessage="Description cannot be blank" ></asp:RequiredFieldValidator>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  SetFocusOnError="true" ControlToValidate="ResourceTypeTaxonomy" runat="server" Display="None" ErrorMessage="Please Select One Resource Type" ></asp:RequiredFieldValidator>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  SetFocusOnError="true" ControlToValidate="ResourceURLlink" runat="server" Display="None" ErrorMessage="Resource URL link cannot be blank" ></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator runat="server" Display="None" SetFocusOnError="true"  ControlToValidate="ResourceURLlink" ValidationExpression="^((http|https)://)?([\w-]+\.)+[\w]+(/[\w- ./?]*)?$"  ErrorMessage="Enter a valid URL" ></asp:RegularExpressionValidator>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  SetFocusOnError="true" ControlToValidate="SubjectAreasTaxonomy" runat="server" Display="None" ErrorMessage="Please Select One or More Subject Areas" ></asp:RequiredFieldValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator6"  SetFocusOnError="true" ControlToValidate="ApplicationIs" runat="server" Display="None" ErrorMessage="Please Select One Application Is field?" ></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"  SetFocusOnError="true" ControlToValidate="AssociatedNetwork" runat="server" Display="None" ErrorMessage="Please Select One Associated Network" ></asp:RequiredFieldValidator>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator8"  SetFocusOnError="true" ControlToValidate="AccessTypeTaxonomy" runat="server" Display="None" ErrorMessage="Please Select One Access Type " ></asp:RequiredFieldValidator>--%>
   
      <asp:Panel ID="successedit" runat="server" Visible="false"  >
      
      <div class="container-green">
	     <h4>Your changes has been saved succesfully.</h4>
		  
	  <p><a href='/admin/users/' > Back to Users List</a></p>
	  
	  </div>
      </asp:Panel>

        <asp:Panel ID="errorEdit" runat="server" Visible="false"  >
      
      <div class="container-red">
	     <h4>There was an error submitting your changes</h4>
		
	  
	  
	  </div>
      </asp:Panel>

 <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_PIN">
		    Resource Name<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="ResourceName" CssClass="required"   title="Resource Name is Required" ></asp:TextBox>  
	  </p>
               
       
       
      <p>
      
       <label class="bold" for="ctl00_mainContentCP_Description">
		   Description<span title="Required" class="fg-red">*</span>
		</label><br />
      <asp:TextBox TextMode="MultiLine"   runat="server"  ID="Description" CssClass="required" title="Description is Required" ></asp:TextBox> 
      </p>

       <p>
      
       <label class="bold" for="ctl00_mainContentCP_ResourceTypeTaxonomy">
		   Resource Type<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:DropDownList ToolTip="Please Select One Resource Type" runat="server" ID="ResourceTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID"  CssClass="required"  ></asp:DropDownList>

    

      </p>
      

      <div id="LinktotheResourceDiv" style="display:none;" >
       <p>
    
       <label class="bold" for="ctl00_mainContentCP_ResourceURLlink">Link to the Resource<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="ResourceURLlink"  CssClass="required" ToolTip="Resource Link is Required" ></asp:TextBox>
      <br />
     <asp:CheckBox runat="server" ID="ShowInNewWindow" Text="Show resource in a popup." />
      </p>

      </div>

      <div id="uploadafileDiv" style="display:none;" >
        <fieldset>
        <legend class="bold">Resource File <span title="Required" class="fg-red">*</span></legend>
         
		
		<input runat="server" class="required" type="radio"  id="uploadFileRd" value="uploadFile" name="ResourceFile" title="Select One Resource type">  <label class="inline-label" for="ctl00_mainContentCP_uploadFileRd">Upload</label> <asp:FileUpload ID="ResourceFileUpload" runat="server" />   
         Or <br />  
		<input runat="server" type="radio" id="ResourceLinkRd" value="ResourceLink" name="ResourceFile"> <label class="inline-label" for="ctl00_mainContentCP_ResourceLinkRd">Link</label>
        <asp:TextBox   runat="server" ID="ResouceLink" ></asp:TextBox>  <br /> <input runat="server" type="checkbox" id="ShowResourceinPopup" /> <label class="inline-label" for="ShowResourceinPopup">Show resource in a popup</label>
        <label for="ctl00$mainContentCP$ResourceFile" class="error" style="display:none !important;" >Please select one.</label>
          <br /> <br />
		</fieldset>

        
       </p>

      </div>
 <p>
      
      <label   for="ctl00_mainContentCP_AccessLevel">Admin URL link to resource
		</label><br />

     <asp:TextBox runat="server" ID="AdminResourceURL" ></asp:TextBox>   
      </p>




  <p>
      
      <label   for="ctl00_mainContentCP_AdminUsername">Admin Username
		</label><br />

     <asp:TextBox runat="server" ID="AdminUsername" ></asp:TextBox>   
      </p>



      
  <p>
      
      <label   for="ctl00_mainContentCP_AdminPassword">Admin Password
		</label><br />

     <asp:TextBox runat="server" ID="AdminPassword" ></asp:TextBox>   
      </p>


            
  <p>
      
        <asp:FileUpload ID="File1" runat="server" /> 
      </p>

       <p>
      
        <asp:FileUpload ID="File2" runat="server" /> 
      </p>



       <p>
      
        <asp:FileUpload ID="File3" runat="server" /> 
      </p>





        <p>
      
 

        <label class="bold" for="ctl00_mainContentCP_SubjectAreasTaxonomy">
		   Subject Areas<span title="Required" class="fg-red">*</span>
		</label><br />

     <asp:ListBox runat="server" ID="SubjectAreasTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" CssClass="required" ToolTip="Select One or more Subject Area(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_SubjectAreasTaxonomy')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_SubjectAreasTaxonomy')">Select None</a>
      </p>


            
  <p>
  <label   for="ctl00_mainContentCP_employeeis">Employee is
		</label><br />
        
         <asp:CheckBox runat="server" Checked="true" Text="Subject Areas" ID="ShowInSubjectAreas" /> <br />
           <asp:CheckBox runat="server" Checked="true" Text="Databases A-Z" ID="ShowInDatabases" /> <br />
             <asp:CheckBox runat="server" Checked="true" Text="Training Request Form" ID="ShowInTrainingRequestForm" /> <br />

  </p>


   <p>
  <label   for="ctl00_mainContentCP_ShowInAudienceToolsTaxonomy">Show in Audience Tool For
		</label><br />
        
        <asp:ListBox runat="server" ID="ShowInAudienceToolsTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" ></asp:ListBox> <br />
   <a href="javascript:selectAll('ctl00_mainContentCP_ShowInAudienceToolsTaxonomy')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ShowInAudienceToolsTaxonomy')">Select None</a>

         

  </p>
  
       <fieldset>
       <label class="bold"   for="ctl00_mainContentCP_ApplicationIs">Application is <span title="Required" class="fg-red">*</span>
		</label><br />

      <asp:RadioButtonList ID="ApplicationIs"   runat="server" ToolTip="Select what Application is."    RepeatLayout="flow"     >
      
      <asp:ListItem   Text="Mandatory" Value="Mandatory"  ></asp:ListItem>
      <asp:ListItem Text="Discretionary" Value="Discretionary" ></asp:ListItem>


      </asp:RadioButtonList>
      <label   class="error" for="ctl00$mainContentCP$ApplicationIs" style="display:none !important;"  >Select what Application is.</label>
     </fieldset>
   
      </p>




      <p>
      
       <label class="bold" for="ctl00_mainContentCP_AssociatedNetwork">
		  Associated Network<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:DropDownList runat="server" ID="AssociatedNetwork"  CssClass="required" ToolTip="Select One Associated Network"   >
      <asp:ListItem   Text=" - Select One -" Value=""></asp:ListItem>
      <asp:ListItem Text="Internet" Value="Internet"></asp:ListItem>
      <asp:ListItem   Text="Intranet" Value="Intranet"></asp:ListItem>
      <asp:ListItem Text="Citrix" Value="Citrix"></asp:ListItem>
       <asp:ListItem Text="File" Value="File"></asp:ListItem>
     </asp:DropDownList>    
      </p>
     
      <h3>Access/Passwords</h3>
       
       
      <p>
       <label class="bold" for="ctl00_mainContentCP_AccessTypeTaxonomy">
		 Access Type<span title="Required" class="fg-red">*</span>
		</label><br />
  
       <asp:DropDownList runat="server" ID="AccessTypeTaxonomy"  CssClass="required" ToolTip="Select One Access Type"
              DataTextField="TaxName" DataValueField="TaxID" 
                ></asp:DropDownList>    
                </p>

               <div id="selfRegisterDiv" style="display:none">
               <p>
     <label class="bold" for="ctl00_mainContentCP_selfRegister">
		  Resource Registration
Instructions<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="selfRegister" TextMode="MultiLine" CssClass="required" ToolTip="Resource Registration
Instructions is required" ></asp:TextBox>
     </p>
     </div>
    

    <div id="SharedLoginDiv" style="display:none">
    <p>
     <label class="bold" for="ctl00_mainContentCP_ShareUsername">
		 Shared Username <span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="ShareUsername" CssClass="required" ToolTip="Shared Username is Required"   ></asp:TextBox>
     <br />
       <label class="bold" for="ctl00_mainContentCP_SharedPassword">
		 Shared Password <span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="SharedPassword" CssClass="required" ToolTip="Shared Password is Required"   ></asp:TextBox>
     </p>
      <fieldset>
       <label class="bold"   for="ctl00_mainContentCP_ShowLogin">Show Login to <span title="Required" class="fg-red">*</span>
		</label><br />

      <asp:RadioButtonList ID="ShowLogin"   runat="server" CssClass="required"  RepeatLayout="flow"  ToolTip="Please select Show login to"  >
      
      <asp:ListItem   Text="All Employees" Value="AllEmployees"></asp:ListItem>
      <asp:ListItem Text="Only DDS Employees" Value="OnlyDDSEmployees"></asp:ListItem>


      </asp:RadioButtonList>
        <label   class="error" for="ctl00$mainContentCP$ShowLogin" style="display:none !important;"  >Select what Application is.</label>
     </fieldset>
     
      
     
     </div>



     <div id="ePassDiv" style="display:none" >
        <fieldset>
        
		<legend> How many Passwords are available?<span title="Required" class="fg-red">*</span></legend>
		<input runat="server" type="radio"  id="limitedTo" value="limitedTo" name="PasswordsAvailable" />  <label class="inline-label" for="ctl00_mainContentCP_limitedTo">Limited To </label>  <asp:TextBox Width="50" runat="server" ID="LimitedNumberPasswordsAvailable" ></asp:TextBox> e-Passwords <br/>
		<input runat="server" type="radio" id="Unlimited" value="Unlimited" name="PasswordsAvailable" /> <label class="inline-label" for="ctl00_mainContentCP_Unlimited">Unlimited e-Passwords</label>
		<label class="error" for="ctl00$mainContentCP$PasswordsAvailable" ></label>
        </fieldset>
       </p> 
       <p>
         <label class="bold" for="ctl00_mainContentCP_SendEpasswordTo">
		  Send e-Passwords Requests to<span title="Required" class="fg-red">*</span>
		</label><br />
        <asp:ListBox runat="server" ID="SendEpasswordTo"  DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120" CssClass="required" ToolTip="Select who to send password requests to" ></asp:ListBox> <br />
         <a href="javascript:selectAll('ctl00_mainContentCP_SendEpasswordTo')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_SendEpasswordTo')">Select None</a>
         <br />
        <asp:CheckBox runat="server" ID="PasswordRequestsRestrictedToManagers"  />  <label   for="ctl00_mainContentCP_PasswordRequestsRestrictedToManagers">
		  Passwords Requests restricted to Managers 
		</label>
        </p>


     </div>
             
      
       


                  
      <h3>Business Justification </h3>
       
    

                  <p>
                  <label  for="ctl00_mainContentCP_TargetUsers">
		Who are the users of this resource?
		</label><br />
                  
                  <asp:TextBox runat="server" ID="TargetUsers" TextMode="MultiLine" ></asp:TextBox>
                  
                  
                  </p>


                  
                  <p>
                  <label   for="ctl00_mainContentCP_BusinessPurposeOfResource">
		How does the agency use this resource, and which workloads does it support?
		</label><br />
                  
                <asp:TextBox runat="server" ID="BusinessPurposeOfResource" TextMode="MultiLine" ></asp:TextBox>
                  
                  
                  </p>


                  <h3>Contract Information</h3>
 


                   
                 <div id="tabs"  > 
    <ul >
        <li>
            <a href="#a">Tab A</a>
        </li>
   <li>
            <a href="#b">Tab B</a>
        </li>

    </ul>
    <div id="a">
        Content of A
    </div>
     
  <div id="b">
        Content of b
    </div>
     
</div> 

                   
                  <p>
                  <label   for="ctl00_mainContentCP_CriticalNotes">
		Critical Notes
		</label><br />
                  
                <asp:TextBox runat="server" ID="CriticalNotes" TextMode="MultiLine" ></asp:TextBox>
                  
                  
                  </p>



                             <p>
                  <label   for="ctl00_mainContentCP_HowManyDays">
		How many days in advance do you want an alert for the expiration of the contract?
		</label><br />
                  
                <asp:TextBox runat="server" ID="HowManyDays" Width="50" ></asp:TextBox>
                  
                  
                  </p>





                  <p>
                  
                  <label   for="ctl00_mainContentCP_LibraryCOR">
		Library Contracting Officer's Representative (COR)?
		</label><br />
                  
                   <asp:ListBox runat="server" ID="LibraryCOR"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" ></asp:ListBox> <br />
   <a href="javascript:selectAll('ctl00_mainContentCP_LibraryCOR')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_LibraryCOR')">Select None</a>
                  </p>



<h3>Vendor Account Represenative</h3>

<p> 
	  
      <label class="bold" for="ctl00_mainContentCP_VendorName">
		    Vendor Name<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorName"  CssClass="required" ToolTip="Vendor Name is Required"    ></asp:TextBox>  
	  </p>


      <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_RepresentatvieName">
		    Representative Name<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="RepresentatvieName"  CssClass="required" ToolTip="Vendor Representative Name is Required"  ></asp:TextBox>  
	  </p>


      <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_VendorEmail">
		    Email<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorEmail"  CssClass="required" ToolTip="Vendor Email is Required"   ></asp:TextBox>  
	  </p>



      <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_VendorPhone">
		    Phone<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorPhone"  CssClass="required" ToolTip="Vendor Phone is Required"  ></asp:TextBox>  
	  </p>





      <h3>Vendor Technical Assistance Contact</h3>

      <p> 
	  
      <label  for="ctl00_mainContentCP_VendorTechnicalAssistanceName">
		      Name 
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorTechnicalAssistanceName"   ></asp:TextBox>  
	  </p>


 


      <p> 
	  
      <label   for="ctl00_mainContentCP_VendorTechnicalAssistanceEmail">
		    Email 
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorTechnicalAssistanceEmail"   ></asp:TextBox>  
	  </p>



      <p> 
	  
      <label for="ctl00_mainContentCP_VendorTechnicalAssistancePhone">
		    Phone 
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorTechnicalAssistancePhone"   ></asp:TextBox>  
	  </p>



      <h3>New Features</h3>

       <p> 
	  
      <label for="ctl00_mainContentCP_NewFeaturesDescription">
		    Description of New Features 
		</label> <br />
	   <asp:TextBox runat="server" ID="NewFeaturesDescription" TextMode="MultiLine"   ></asp:TextBox>  
	  </p>
      


        <p> 
	     <div class="date1">
      <label for="ctl00_mainContentCP_NewFeatureStartDate">
		    Start Date
		</label> <br />
	   <asp:TextBox runat="server" ID="NewFeatureStartDate"    ></asp:TextBox>  
       </div>
       <div class="date2">
            <label for="ctl00_mainContentCP_NewFeatureEndDate">
		    End Date
		</label> <br />
	   <asp:TextBox runat="server" ID="NewFeatureEndDate"    ></asp:TextBox>  </div>
	  </p>

      <h3>Resource Display Status</h3>
      <p>
      
      <asp:RadioButtonList ID="ResourceDisplayStatus" runat="server" >
      <asp:ListItem Selected="True" Text="Enable and Show on the website" Value="Enabled"></asp:ListItem>
         
          <asp:ListItem   Text="Temporarily Disable this resource, but still show it on the website" Value="Temporarily Disabled"></asp:ListItem>
       <asp:ListItem  Text="Make this resource Inactive and hide it on the website" Value="Inactive"></asp:ListItem>
      
      
      </asp:RadioButtonList>
      
      </p>
      <p></p> <p></p>
      <p>
       <asp:Button runat="server" ID="updateUser" Text="Save" onclick="createUser_Click" />    <input   id="Text1" value="Cancel"  type="button"  style="width:150px" onclick="return Text1_onclick(); return false;" /> 
       </p>

	   
	  
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
      </span>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

