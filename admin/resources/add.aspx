<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="add.aspx.cs" Inherits="admin_users_Default" %>
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

            $("#PeriodofPerformanceStart1").datepicker();
            $("#PeriodofPerformanceEnd1").datepicker();

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



            /////////////////////////////////////////////////////Validation
            $(document).ready(function () {
                $("span[id*=ctl00_mainContentCP_ApplicationIs] input").addClass("validate[required]");
                $("span[id*=ctl00_mainContentCP_ShowLogin] input").addClass("validate[required]");

                $("#aspnetForm").validationEngine('attach');
            });




            ///////////////////////////////////////// End ResourceTypeTaxonomy 

            $("#ctl00_mainContentCP_ApplicationIs_0").addClass("validate[required]");
            $("#ctl00_mainContentCP_ShowLogin_0").addClass("validate[required]");
            //////////////Resource File Validation
            $("#ctl00_mainContentCP_uploadFileRd").click(function () {
                $("#ctl00_mainContentCP_ResouceLink").removeClass("validate[required,custom[url]]");
                $("#ctl00_mainContentCP_ResourceFileUpload").removeClass("validate[required]");

                if ($('#ctl00_mainContentCP_uploadFileRd').is(':checked')) {
                    $("#ctl00_mainContentCP_ResourceFileUpload").addClass("validate[required]");
                }  
            });

            $("#ctl00_mainContentCP_ResourceLinkRd").click(function () {

                $("#ctl00_mainContentCP_ResouceLink").removeClass("validate[required,custom[url]]");
                $("#ctl00_mainContentCP_ResourceFileUpload").removeClass("validate[required]");

                if ($('#ctl00_mainContentCP_ResourceLinkRd').is(':checked')) {
                    $("#ctl00_mainContentCP_ResouceLink").addClass("validate[required,custom[url]]");
                }  
            });

            ///////////////// End Resource file validation

            ////////////////////////Epass limited password
            $("#ctl00_mainContentCP_limitedTo").addClass("required");

            $("#ctl00_mainContentCP_limitedTo").click(function () {
                $("#ctl00_mainContentCP_LimitedNumberPasswordsAvailable").removeClass("validate[required,custom[integer]]");              

                if ($('#ctl00_mainContentCP_limitedTo').is(':checked')) {
                    $("#ctl00_mainContentCP_LimitedNumberPasswordsAvailable").addClass("validate[required,custom[integer]]");
                }
            });

            $("#ctl00_mainContentCP_Unlimited").click(function () {

                $("#ctl00_mainContentCP_LimitedNumberPasswordsAvailable").removeClass("validate[required,custom[integer]]");

                
            });

            ////////////////////////////////////////////////////Epass end


            /////////////////Select all checkboxes
//            $('#select_all').click(function () {
//                $('#countries option').prop('selected', true);
//            });

            


/////////////////////////////////////

            $('#tabs').tabs().addClass('ui-tabs-vertical ui-helper-clearfix');






            $('#btnAdd').click(function (e) {
                var nextTab = $('#tabs li').size() + 1;

                // create the tab
                $('<li><a href="#tab' + nextTab + '" data-toggle="tab">Tab ' + nextTab + '</a></li>').appendTo('#tabs');

                // create the tab content
                $('<div class="tab-pane" id="tab' + nextTab + '">tab' + nextTab + ' content</div>').appendTo('.tab-content');

                // make the new tab active
                $('#tabs a:last').tab('show');
            });



        });
        function Text1_onclick() {
            location.href = "/admin/users/";
        }



   
 



    	    $(document).ready(function () {

    	        //simple tab creation
    	        $("#simpletab1").dynatabs({
    	            tabBodyID: "simpletabbody1"
    	        });
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

    	        //add ajax tabs
    	        $("#addajaxtab").dynatabs({
    	            tabBodyID: "addajaxtabbody",
    	            showCloseBtn: true,
    	            confirmDelete: true
    	        });

    	        //add static tabs
    	        $("#addstatictab").dynatabs({
    	            tabBodyID: 'addstatictabbody',
    	            showCloseBtn: true,
    	            confirmDelete: true
    	        });

    	        //add hidden tabs example
    	        $("#adddivtab").dynatabs({
    	            tabBodyID: 'adddivtabbody',
    	            showCloseBtn: true,
    	            confirmDelete: true
    	        });

    	        //nested tab 1
    	        $("#nestedtab1").dynatabs({
    	            tabBodyID: 'nestedtabbody1',
    	            showCloseBtn: true,
    	            confirmDelete: true
    	        });
    	        //nested tab 2
    	        $("#nestedtab2").dynatabs({
    	            tabBodyID: 'nestedtabbody2',
    	            showCloseBtn: true,
    	            confirmDelete: true
    	        });
    	    });

    	    function addNewTab() {
    	        $.addDynaTab({
    	            tabID: 'addajaxtab',
    	            type: 'ajax',
    	            url: '/admin/resources/ajaxgetcontracts.aspx',
    	            method: 'get',
    	            dtype: 'html',
    	            params: {},
    	            tabTitle: 'New FY',
    	            showCloseBtn: false,
    	            confirmDelete: false
    	        });
    	    }

    	    function addNewStaticTab() {
    	        $.addDynaTab({
    	            tabID: 'addstatictab',
    	            type: 'html',
    	            html: '<p>This HTML content is loaded statically</p>',
    	            params: {},
    	            tabTitle: 'New Static Tab'
    	        });
    	    }

    	    function addNewDivTab() {
    	        $.addDynaTab({
    	            tabID: 'adddivtab',
    	            type: 'div',
    	            divID: 'hdnDataTabDiv',
    	            params: {},
    	            tabTitle: 'New Div Tab'
    	        });
    	    }

    	    function procureMentChg(id, which) {

    	        var aa = "ProcurementMethodOtherDiv" + which;
    	        var elem = document.getElementById(id).value;
    	        if (elem == "112") {
    	            $("#" + aa).show();
    	        } else {
    	            $("#" + aa).hide();
    	        }
    	    }


    	    function uploadMe(elm, which) {
    	        var fullPath = $(elm).val();

    	        //$(elm).hide();
    	        $('#fileDiv' + which).hide();
    	        var filename = fullPath.split(/(\\|\/)/g).pop();
    	        var filext = filename.split('.').pop();
    	        var researchGuideOption = "<option selected='true' >Research Guide</option >";
    	        htmlString = "<img src='/admin/framework/images/close.jpg' onclick='showUpload(" + which + ")' style='cursor:pointer' />" + filename + " &nbsp;&nbsp;&nbsp;&nbsp;     File type: <select disabled='true' style='width:70px'><option selected='true' > " + filext + "</option></select > ";
    	        htmlString += "&nbsp;&nbsp;&nbsp;&nbsp; Description : <select  style='width:170px'>" + researchGuideOption + "</select >";
    	        htmlString += "&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='listInRG" + which + "' name='listInRG" + which + "' /> <label for='listInRG" + which + "'>List in Research Guide</label>";
    	        htmlString += "<input type='hidden' name='uploadURL" + which + "' id='uploadURL" + which + "' value='" + fullPath + "' />";
    	        $('#fileResults' + which).html(htmlString);


    	    }

    	    function uploadMe2(elm, which) {
    	        var fullPath = $(elm).val();

    	        //$(elm).hide();
    	        $('#fileDiv' + which).hide();
    	        var filename = fullPath.split(/(\\|\/)/g).pop();
    	        var filext = filename.split('.').pop();
    	       
    	        htmlString = "<img src='/admin/framework/images/close.jpg' onclick='showUpload(" + which + ")' style='cursor:pointer' />" + filename + " &nbsp;&nbsp;&nbsp;&nbsp;     File type: <select disabled='true' style='width:70px'><option selected='true' > " + filext + "</option></select > ";
    	        htmlString += "<input type='hidden' name='uploadURL" + which + "' id='uploadURL" + which + "' value='" + fullPath + "' />";
    	        $('#fileResults' + which).html(htmlString);


    	    }

    	    function showUpload(which) {

    	        $('#fileResults' + which).html("");

    	        $('#fileDiv' + which).show();

    	        $('#fileUpload' + which).val("") //.replaceWith("<input type='file' id='fileUpload1" + which + "' name='fileUpload1" + which + "' />"); ;
            }

	</script>
    	      
	 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
 <div id="title-bar">
    <h2>
      <span class="favorite-id">
           Add A New Resource</span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
    <span id="AjaxResult" ></span>
 
 
<div class="grid" id="admin-tools-int-content">
     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
            <li><a href="/admin/resources/">Resources</a></li>
         <li> Add A New Resource</li>  
    </ul> 

   
    
      
</div>
	  
	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-12 print-full-width" >
	  
	  

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
	   <asp:TextBox runat="server" ID="ResourceName" CssClass="validate[required]"   title="Resource Name is Required"  ></asp:TextBox>  
	  </p>
               
       
       
      <p>
      
       <label class="bold" for="ctl00_mainContentCP_Description">
		   Description<span title="Required" class="fg-red">*</span>
		</label><br />
      <asp:TextBox TextMode="MultiLine"   runat="server"  ID="Description" CssClass="validate[required]" title="Description is Required"   ></asp:TextBox> 
      </p>

       <p>
      
       <label class="bold" for="ctl00_mainContentCP_ResourceTypeTaxonomy">
		   Resource Type<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:DropDownList ToolTip="Please Select One Resource Type" runat="server" ID="ResourceTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID"  CssClass="validate[required]"  ></asp:DropDownList>

    

      </p>
      

      <div id="LinktotheResourceDiv" style="display:none;" >
       <p>
    
       <label class="bold" for="ctl00_mainContentCP_ResourceURLlink">Link to the Resource<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="ResourceURLlink"  data-errormessage-value-missing="Invalid URL!" 
    data-errormessage-custom-error="must begin with either http:// or https:// " 
    data-errormessage="This is the fall-back error message."   CssClass="validate[required,custom[url]]" ToolTip="Resource Link is Required" ></asp:TextBox>
      <br />
     
      </p>

      </div>
     
       <p>
      <div id="uploadafileDiv" style="display:none;" > <legend class="bold">Resource File <span title="Required" class="fg-red">*</span></legend>
        <fieldset><div class="column-5">
       
         
		
		<input runat="server" class="validate[required]" type="radio"  id="uploadFileRd" value="uploadFile" name="ResourceFile" title="Select One Resource type">  
        <label class="inline-label" for="ctl00_mainContentCP_uploadFileRd">Upload</label>
          

            <div id='fileDiv11'>
      <input type="file" id="fileUpload11" name="fileUpload11"  onchange="uploadMe2(this , 11)" />
      </div>
        <div id="fileResults11" ></div>

           <%--<asp:FileUpload ID="ResourceFileUpload" runat="server" />--%>   
        </div> <div class="column-5">
		<input runat="server" type="radio" id="ResourceLinkRd" value="ResourceLink" name="ResourceFile"> <label class="inline-label" for="ctl00_mainContentCP_ResourceLinkRd">Link</label>
        <asp:TextBox   runat="server" ID="ResourceURLlink2" ></asp:TextBox>  <br /> 
        <label for="ctl00$mainContentCP$ResourceFile" class="error" style="display:none !important;" >Please select one.</label>
          </div> 
		</fieldset>

        
      

      </div> </p>  <p>  
      <asp:CheckBox runat="server" ID="ShowResourceinPopup" Text="Show Resource in Popup"   />
      </p>
 <p>
      
      <label   for="ctl00_mainContentCP_AccessLevel">Admin Link to resource
		</label><br />

     <asp:TextBox runat="server" ID="AdminResourceURL" CssClass="validate[custom[url]]"   ></asp:TextBox>   
      </p>




  <p>
      
      <label   for="ctl00_mainContentCP_AdminUsername">Admin Username
		</label><br />

     <asp:TextBox runat="server" ID="AdminUsername"  ></asp:TextBox>   
      </p>



      
  <p>
      
      <label   for="ctl00_mainContentCP_AdminPassword">Admin Password
		</label><br />

     <asp:TextBox runat="server" ID="AdminPassword"    ></asp:TextBox>   
      </p>

            <label class="bold">Associated Files  </label>
  <p>
  <div id='fileDiv1'>
      <input type="file" id="fileUpload1" name="fileUpload1"  onchange="uploadMe(this , 1)" />
      </div>
        <div id="fileResults1" ></div>

         
      </p>

       <p>  <div id='fileDiv2'>
      <input type="file" id="fileUpload2" name="fileUpload2" onchange="uploadMe(this , 2)" />
      </div>
        <div id="fileResults2" ></div>
         
      </p>



       <p>
         <div id='fileDiv3'>
         <input type="file" id="fileUpload3" name="fileUpload3"  onchange="uploadMe(this , 3)" />
         </div>
        <div id="fileResults3" ></div>
        
      </p>





        <p>
      
 

        <label class="bold" for="ctl00_mainContentCP_SubjectAreasTaxonomy">
		   Subject Areas<span title="Required" class="fg-red">*</span>
		</label><br />

     <asp:ListBox runat="server" ID="SubjectAreasTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120" CssClass="validate[required]" ToolTip="Select One or more Subject Area(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_SubjectAreasTaxonomy')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_SubjectAreasTaxonomy')">Select None</a>
      </p>


            
  <p>
  <label   for="ctl00_mainContentCP_employeeis">Show Resource In
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
      
      <asp:ListItem   Text="Mandatory" Value="Y"  ></asp:ListItem>
      <asp:ListItem Text="Discretionary" Value="N" ></asp:ListItem>


      </asp:RadioButtonList>
      <label   class="error" for="ctl00$mainContentCP$ApplicationIs" style="display:none !important;"  >Select what Application is.</label>
     </fieldset>
   
     




      <p>
      
       <label class="bold" for="ctl00_mainContentCP_AssociatedNetwork">
		  Associated Network<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:DropDownList runat="server" ID="AssociatedNetwork"  CssClass="validate[required]" ToolTip="Select One Associated Network"   >
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
  
       <asp:DropDownList runat="server" ID="AccessTypeTaxonomy"  CssClass="validate[required]" ToolTip="Select One Access Type"
              DataTextField="TaxName" DataValueField="TaxID" 
                ></asp:DropDownList>    
                </p>

               <div id="selfRegisterDiv" style="display:none">
               <p>
     <label class="bold" for="ctl00_mainContentCP_ResourceRegistrationInstructions">
		  Resource Registration
Instructions<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="ResourceRegistrationInstructions" TextMode="MultiLine" CssClass="validate[required]" ToolTip="Resource Registration
Instructions is required" ></asp:TextBox>
     </p>
     </div>
    

    <div id="SharedLoginDiv" style="display:none">
    <p>
     <label class="bold" for="ctl00_mainContentCP_ShareUsername">
		 Shared Username <span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="ShareUsername" CssClass="validate[required]" ToolTip="Shared Username is Required"   ></asp:TextBox>
     <br />
       <label class="bold" for="ctl00_mainContentCP_SharedPassword">
		 Shared Password <span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="SharedPassword" CssClass="validate[required]" ToolTip="Shared Password is Required"      ></asp:TextBox>
     </p>
      <fieldset>
       <label class="bold"   for="ctl00_mainContentCP_ShowLogin">Show Login to <span title="Required" class="fg-red">*</span>
		</label><br />

      <asp:RadioButtonList ID="ShowLogin"   runat="server"   RepeatLayout="flow"  ToolTip="Please select Show login to"  >
      
      <asp:ListItem   Text="All Employees" Value="AllEmployees"></asp:ListItem>
      <asp:ListItem Text="Only DDS Employees" Value="OnlyDDSEmployees"></asp:ListItem>


      </asp:RadioButtonList>
        <label   class="error" for="ctl00$mainContentCP$ShowLogin" style="display:none !important;"  >Select what Application is.</label>
     </fieldset>
     
      
     
     </div>



     <div id="ePassDiv" style="display:none" >
        <fieldset>
        
		<legend> How many Passwords are available?<span title="Required" class="fg-red">*</span></legend>
		<input runat="server" type="radio"  id="limitedTo" value="limitedTo" name="PasswordsAvailable" />  <label class="inline-label" for="ctl00_mainContentCP_limitedTo">Limited To </label>  <asp:TextBox Width="50"   runat="server" ID="LimitedNumberPasswordsAvailable" ></asp:TextBox> e-Passwords <br/>
		<input runat="server" type="radio" id="Unlimited" value="Unlimited" name="PasswordsAvailable" /> <label class="inline-label" for="ctl00_mainContentCP_Unlimited">Unlimited e-Passwords</label>
		<label class="error" for="ctl00$mainContentCP$PasswordsAvailable" ></label>
        </fieldset>
       </p> 
       <p>
         <label class="bold" for="ctl00_mainContentCP_SendEpasswordTo">
		  Send e-Passwords Requests to<span title="Required" class="fg-red">*</span>
		</label><br />
        <asp:ListBox runat="server" ID="SendEpasswordTo"  DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120" CssClass="validate[required]" ToolTip="Select who to send password requests to" ></asp:ListBox> <br />
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
 <label class="bold" >Contracts by Fiscal Year(Oct 1-Sept 30)</label> 
 
                     <div id="vertical-tabs">
 	<!-- Tab Headers Start -->
 	<ul  id="addajaxtab" >
 	 
    	<li><a class="on" href="#tabview1">FY <%=DateTime.Now.Year %></a></li>
        
        
 		  
          
 	</ul>
    <a class="floatNew onFig" href="javascript:addNewTab();" onclick="javascript:addNewTab();return false;" style="clear: both; float: left; padding: 16px;">Add New FY</a>
 	<!-- Tab Headers End -->
 	<!-- Tab Body Start -->
 	<div   id="addajaxtabbody">
 		<div id="tabview1" style="display:block;" >
         <input type="hidden" id="contractCounts" name="contractCounts" value="1" />
<p>FY: <select id="FiscalYear1" name="FiscalYear1" >
<%=optionsFYYear %>
</select>
<p>
<label>Period of Performance</label>
<br />
<div class="date1">
Starts:   <input type="text" id="PeriodofPerformanceStart1"  name="PeriodofPerformanceStart1" />
</div>

<div class="date2">
Ends:  <input  type="text" id="PeriodofPerformanceEnd1" name="PeriodofPerformanceEnd1" />
</div>

</p>

<p>
<label>Requisition Number</label>
<br /> <input   type="text" id="RequisitionNumber1"  name="RequisitionNumber1" />

</p>

<p>
<label>Contract Number</label>
<br /> <input   type="text" id="ContractNumber1" name="ContractNumber1" />

</p>


<fieldset>
        
		<legend> How many licenses do we own?</legend>

 
        	<input checked="checked"  type="radio" id="noneLicense1" value="None" name="HowManyLicenses1" /> <label class="inline-label" for="noneLicense1">None</label> <br />
		<input  type="radio"  id="limitedToLicenses1" value="limitedTo" name="HowManyLicenses1" />  <label class="inline-label" for="limitedToLicensesCount">Limited To </label> 
       <input   style="width:50px;" type="text" id="limitedToLicensesCount1"  name="limitedToLicensesCount1" />   Licenses <br/>
	 
        <input   type="radio" id="UnlimitedLicenses1" value="Unlimited" name="HowManyLicenses1" /> <label class="inline-label" for="UnlimitedLicenses1">Unlimited</label> 
 
        </fieldset>


</p>
<p>

<label>Annual Contract Cost</label>
<br />
$ <input  type="text" id="AnnualContractCost1"   name="AnnualContractCost1"  style="width:150px;" />
</p>
<p>
<label>Procurement Method</label><br />
<select  onchange="procureMentChg('ProcurementMethod' , 1)"  id="ProcurementMethod1" name="ProcurementMethod1" >
<%=ProcurementMethodListOptions%>
</select>
</p>
<span id="ProcurementMethodOtherDiv1" style="display:none">
<p>Please specify other Procurement Method <br />  <input  type="text" id="ProcurementMethodOther1"   name="ProcurementMethodOther1"  style="width:150px;" /> </p>
</span>

<p>
<label>Contract PDF</label><br />
<input type="file" id="ContractFileName1" name="ContractFileName1" />

</p>

</div>
 		 
 	</div>
 	<!-- Tab Body End --> 	
 </div>

 

                   
                  <p>
                  <label   for="ctl00_mainContentCP_CriticalNotes">
		Critical Notes
		</label><br />
                  
                <asp:TextBox runat="server" ID="CriticalNotes" TextMode="MultiLine" ></asp:TextBox>
                  
                  
                  </p>



                             <p>
                  <label   for="ctl00_mainContentCP_NotifyOfExpirationThisManyDaysInAdvance">
		How many days in advance do you want an alert for the expiration of the contract?
		</label><br />
                  
                <asp:TextBox runat="server" ID="NotifyOfExpirationThisManyDaysInAdvance" Width="50" ></asp:TextBox>
                  
                  
                  </p>





                  <p>
                  
                  <label   for="ctl00_mainContentCP_LibraryCOR">
		Library Contracting Officer's Representative (COR)?
		</label><br />
                  
                   <asp:ListBox runat="server" ID="LibraryCOR"   DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120" ></asp:ListBox> <br />
   <a href="javascript:selectAll('ctl00_mainContentCP_LibraryCOR')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_LibraryCOR')">Select None</a>
                  </p>



<h3>Vendor Account Represenative</h3>

<p> 
	  
      <label class="bold" for="ctl00_mainContentCP_VendorName">
		    Vendor Name<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorName"  CssClass="validate[required]" ToolTip="Vendor Name is Required"    ></asp:TextBox>  
	  </p>


      <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_RepresentativeName">
		    Representative Name<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="RepresentativeName"  CssClass="validate[required]" ToolTip="Vendor Representative Name is Required"  ></asp:TextBox>  
	  </p>


      <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_VendorEmail">
		    Email<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorEmail"  CssClass="validate[required,custom[email]]" ToolTip="Vendor Email is Required"   ></asp:TextBox>  
	  </p>



      <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_VendorPhone">
		    Phone<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorPhone"   ToolTip="Vendor Phone is Required"  ></asp:TextBox>  
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
	   <asp:TextBox runat="server" ID="VendorTechnicalAssistanceEmail" CssClass="validate[custom[email]]"  ></asp:TextBox>  
	  </p>



      <p> 
	  
      <label for="ctl00_mainContentCP_VendorTechnicalAssistancePhone">
		    Phone 
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorTechnicalAssistancePhone"    ></asp:TextBox>  
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
         
          <asp:ListItem   Text="Temporarily Disable this resource, but still show it on the website" Value="Disabled"></asp:ListItem>
       <asp:ListItem  Text="Make this resource Inactive and hide it on the website" Value="Inactive"></asp:ListItem>
      
      
      </asp:RadioButtonList>
      
      </p>
      <p></p> <p></p>
      <p>
       <asp:Button runat="server" CssClass="btn" ID="AddResource" Text="Save" onclick="AddResource_Click" />    <input   id="Text1" value="Cancel"  type="button"   style="height: 36px;width:150px; position: relative;
    top: 4px;" onclick="return Text1_onclick(); return false;" /> 
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

