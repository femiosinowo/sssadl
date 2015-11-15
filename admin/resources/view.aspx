<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="admin_users_Default" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
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


            /////////////////////////////////////////////////////Validation
            $(document).ready(function () {
                $("span[id*=ctl00_mainContentCP_ApplicationIs] input").addClass("validate[required]");
                $("span[id*=ctl00_mainContentCP_ShowLogin] input").addClass("validate[required]");

                $("#aspnetForm").validationEngine('attach');
            });



            $("#ctl00_mainContentCP_ApplicationIs_0").addClass("validate[required]");
            $("#ctl00_mainContentCP_ShowLogin_0").addClass("validate[required]");
            //////////////Resource File Validation
            $("#ctl00_mainContentCP_uploadFileRd").click(function () {
                 
                $("#ctl00_mainContentCP_ResourceURLlink2").removeClass("validate[required,custom[url]]");
                $("#fileUpload11").removeClass("validate[required]");

                if ($('#ctl00_mainContentCP_uploadFileRd').is(':checked')) {
                    $("#fileUpload11").addClass("validate[required]");
                }
            });

            $("#ctl00_mainContentCP_ResourceLinkRd").click(function () {

                $("#ctl00_mainContentCP_ResouceLink").removeClass("validate[required,custom[url]]");
                $("#fileUpload11").removeClass("validate[required]");

                if ($('#ctl00_mainContentCP_ResourceLinkRd').is(':checked')) {
                    $("#ctl00_mainContentCP_ResourceURLlink2").addClass("validate[required,custom[url]]");
                }
            });

            ///////////////// End Resource file validation

            ////////////////////////Epass limited password
            $("#ctl00_mainContentCP_limitedTo").addClass("validate[required]");

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


            /////////////////////////////End validation


            $("#ctl00_mainContentCP_NewFeatureEndDate").datepicker();
            $("#ctl00_mainContentCP_NewFeatureStartDate").datepicker();

            $("#PeriodPerformanceStarts1").datepicker();
            $("#PeriodPerformanceEnds1").datepicker();

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
            location.href = "/admin/resources/default.aspx";
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
    	        htmlString += "&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='listInRG" + which + "' /> <label for='listInRG" + which + "'>List in Research Guide</label>";
    	        htmlString += "<input type='hidden' name='uploadURL" + which + "' id='uploadURL" + which + "' value='" + fullPath + "' /> ";
    	        htmlString += "<input type='hidden' name='fileModified" + which + "' id='fileModified" + which + "' value='true' />";
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

    	        $('#fileUpload' + which).replaceWith("<input type='file' class='validate[required]' id='fileUpload" + which + "' name='fileUpload" + which + "' /><input type='hidden' name='fileModified" + which + "' id='fileModified" + which + "' value='true' />"); ;
    	    }

    	    function deleteUpload(which) {
    	         
    	        $('#contractFile' + which).hide();
    	        $('#cfStatus' + which).replaceWith("<input type='hidden' name='deleteContractFile" + which + "' id='deleteContractFile" + which + "' value='true' />");
    	    }

	</script>
	 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
     <div id="title-bar">
    <h2>
      <span class="favorite-id">
          Edit a Resource</span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
    <asp:HiddenField ID="ResourceIDHF" runat="server" />
    <span id="AjaxResult" ></span>
 
 
<div class="grid" id="admin-tools-int-content">
     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
            <li><a href="/admin/resources/">Resources</a></li>
         <li>Edit A Resource</li>  
    </ul> 

   
    
      
</div>
	  
	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-12 print-full-width" >
	  
	 

         <asp:Panel ID="successedit" runat="server" Visible="false"  >
      
      <div class="container-green">
	     <h4>Your changes has been saved succesfully.</h4>
		  
	  <p><a href='/admin/users/default.aspx' > Back to Users List</a></p>
	  
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
	   <asp:TextBox runat="server" ID="ResourceName" CssClass="validate[required]"   title="Resource Name is Required"   ></asp:TextBox>  
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
      

      <div id="LinktotheResourceDiv" <%=LinktotheResourceDivStyle %>  >
       <p>
    
       <label class="bold" for="ctl00_mainContentCP_ResourceURLlink">Link to the Resource<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="ResourceURLlink"  data-errormessage-value-missing="Invalid URL!" 
    data-errormessage-custom-error="must begin with either http:// or https:// " 
    data-errormessage="This is the fall-back error message."  CssClass="validate[required,custom[url]]" ToolTip="Resource Link is Required" ></asp:TextBox>
      <br />
     
      </p>

      </div>
     
     
      <div id="uploadafileDiv" <%=uploadafileDivStyle %>   >
        <fieldset>
        <legend class="bold">Resource File <span title="Required" class="fg-red">*</span></legend>
         <div class="column-5">
		
		<input runat="server" class="validate[required]" type="radio"  id="uploadFileRd" value="uploadFile" name="ResourceFile" title="Select One Resource type"> 
         <label class="inline-label" for="ctl00_mainContentCP_uploadFileRd">Upload</label> 
          
  <%=file11Label %> 
        </div> <div class="column-5">
		<input runat="server" type="radio" id="ResourceLinkRd" value="ResourceLink" name="ResourceFile"> <label class="inline-label" for="ctl00_mainContentCP_ResourceLinkRd">Link</label>
        <asp:TextBox   runat="server" ID="ResourceURLlink2" ></asp:TextBox>  <br /> 
        <label for="ctl00$mainContentCP$ResourceFile" class="error" style="display:none !important;" >Please select one.</label>
         </div>  <br /> <br />
		</fieldset>
        </div>
        
        
 <p>
      <asp:CheckBox runat="server" ID="ShowResourceinPopup" Text="Show resource in a popup." />
      </p> 
     
 <p>
      
      <label   for="ctl00_mainContentCP_AccessLevel">Admin Link to resource
		</label><br />

     <asp:TextBox runat="server" ID="AdminResourceURL" CssClass="validate[custom[url]]"  ></asp:TextBox>   
      </p>




  <p>
      
      <label   for="ctl00_mainContentCP_AdminUsername">Admin Username
		</label><br />

     <asp:TextBox runat="server" ID="AdminUsername"   ></asp:TextBox>   
      </p>



      
  <p>
      
      <label   for="ctl00_mainContentCP_AdminPassword">Admin Password
		</label><br />

     <asp:TextBox runat="server" ID="AdminPassword"    ></asp:TextBox>   
      </p>

            <label class="bold">Associated Files <span title="Required" class="fg-red">*</span></label>
  <p>
  <%=file1Label %> 
         
      </p>

       <p>    <%=file2Label%>
         
    </p>



       <p>
             <%=file3Label%> 
        
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
        
         <asp:CheckBox runat="server"   Text="Subject Areas" ID="ShowInSubjectAreas" /> <br />
           <asp:CheckBox runat="server"   Text="Databases A-Z" ID="ShowInDatabases" /> <br />
             <asp:CheckBox runat="server"   Text="Training Request Form" ID="ShowInTrainingRequestForm" /> <br />

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

               <div id="selfRegisterDiv" <%=selfRegisterDivStyle %>  >
               <p>
     <label class="bold" for="ctl00_mainContentCP_ResourceRegistrationInstructions">
		  Resource Registration
Instructions<span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="ResourceRegistrationInstructions" TextMode="MultiLine" CssClass="validate[required]" ToolTip="Resource Registration
Instructions is required" ></asp:TextBox>
     </p>
     </div>
    

    <div id="SharedLoginDiv" <%=SharedLoginDivStyle %>>
    <p>
     <label class="bold" for="ctl00_mainContentCP_ShareUsername">
		 Shared Username <span title="Required" class="fg-red">*</span>
		</label><br />
     <asp:TextBox runat="server" ID="ShareUsername" CssClass="validate[required]" ToolTip="Shared Username is Required"  ></asp:TextBox>
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



     <div id="ePassDiv" <%=ePassDivStyle %> >
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
  

     <asp:ListView ID="ContractTabsLV" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="ContractTabsLV_ItemDatabound">
        <EmptyDataTemplate>
            <h4>
                Search criteria did not match any records.
            </h4>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
         	<li><%--<a class="on" href="#tabview1">FY <%=DateTime.Now.Year %></a>--%>
             <asp:HyperLink runat="server" ID="tabViewLinkTitle"></asp:HyperLink></li>
            
        </ItemTemplate>
    </asp:ListView>

   
        
        
 		  
          
 	</ul>
     <a class="floatNew onFig" href="javascript:addNewTab();" onclick="javascript:addNewTab();return false;" style="clear: both; float: left; padding: 16px;">Add New FY</a>

 	<!-- Tab Headers End -->
 	<!-- Tab Body Start -->
 	<div   id="addajaxtabbody">
      <asp:ListView ID="ContractContentLV" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="ContractContentLV_ItemDatabound">
        <EmptyDataTemplate>
            <h4>
                Search criteria did not match any records.
            </h4>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
       
 		<div  <asp:Literal runat="server" ID="idview" ></asp:Literal>  >
        <input type="hidden" id="contractCounts" name="contractCounts" value="<%# Eval("ID") %>" />
          <input type="hidden" id="AddContract<%# Eval("ID") %>" name="AddContract<%# Eval("ID") %>" value="False" />
<p>FY: <select id="FiscalYear<%# Eval("ID") %>" name="FiscalYear<%# Eval("ID") %>" >
<asp:Literal runat="server" ID="fiscalYearList" ></asp:Literal>
</select>
<p>
<label>Period of Performance</label>
<br />
<div class="date1">
Starts:   <input type="text" id="PeriodofPerformanceStart<%# Eval("ID") %>" value="<%# Eval("PeriodofPerformanceStart").ToString().Trim() %>"  name="PeriodofPerformanceStart<%# Eval("ID") %>" />
</div>

<div class="date2">
Ends:  <input  type="text" id="PeriodofPerformanceEnd<%# Eval("ID") %>" value="<%# Eval("PeriodofPerformanceEnd").ToString().Trim() %>" name="PeriodofPerformanceEnd<%# Eval("ID") %>" />
</div>

</p>

<p>
<label>Requisition Number</label>
<br /> <input   type="text" id="RequisitionNumber<%# Eval("ID") %>" value="<%# Eval("RequisitionNumber").ToString().Trim() %>" name="RequisitionNumber<%# Eval("ID") %>" />

</p>

<p>
<label>Contract Number</label>
<br /> <input   type="text" id="ContractNumber<%# Eval("ID") %>" value="<%# Eval("ContractNumber").ToString().Trim() %>" name="ContractNumber<%# Eval("ID") %>" />

</p>


<fieldset>
        
		<legend> How many licenses do we own?</legend>

 
        	<input checked="checked" <asp:Literal runat="server" ID="noneLicenseChecked" ></asp:Literal>  type="radio" id="noneLicense<%# Eval("ID") %>" value="None" name="HowManyLicenses<%# Eval("ID") %>" /> <label class="inline-label" for="noneLicense">None</label> <br />
		<input  type="radio" <asp:Literal runat="server" ID="limitedToLicensesChecked" ></asp:Literal>  id="limitedToLicenses<%# Eval("ID") %>" value="limitedTo" name="HowManyLicenses<%# Eval("ID") %>" />  <label class="inline-label" for="limitedToLicensesCount<%# Eval("ID") %>">Limited To </label> 
       <input   style="width:50px;" type="text" id="limitedToLicensesCount<%# Eval("ID") %>"  value="<%# Eval("LicensesCount").ToString().Trim() %>" name="limitedToLicensesCount<%# Eval("ID") %>" />   Licenses <br/>
	 
        <input <asp:Literal runat="server" ID="UnlimitedLicensesChecked" ></asp:Literal>  type="radio" id="UnlimitedLicenses<%# Eval("ID") %>" value="Unlimited" name="HowManyLicenses<%# Eval("ID") %>" /> <label class="inline-label" for="UnlimitedLicenses<%# Eval("ID") %>">Unlimited</label> 
 
        </fieldset>


</p>
<p>

<label>Annual Contract Cost</label>
<br />
$ <input  type="text" id="AnnualContractCost<%# Eval("ID") %>"  value="<%# Eval("AnnualContractCost").ToString().Trim() %>"  name="AnnualContractCost<%# Eval("ID") %>"  style="width:150px;" />
</p>
<p>
<label>Procurement Method</label><br />
<select  onchange="procureMentChg('ProcurementMethod<%# Eval("ID") %>' , <%# Eval("ID") %>)"  id="ProcurementMethod<%# Eval("ID") %>" name="ProcurementMethod<%# Eval("ID") %>" >
 <asp:Literal runat="server" ID="ProcurementMethodListOptions" ></asp:Literal> 
</select>
</p>
<span id="ProcurementMethodOtherDiv<%# Eval("ID") %>"   <asp:Literal runat="server" ID="ProcurementMethodOtherDivLit" ></asp:Literal>  >
<p>Please specify other Procurement Method <br />  <input   type="text" id="ProcurementMethodOther<%# Eval("ID") %>" value="<%# Eval("ProcurementMethodOther").ToString().Trim() %>"   name="ProcurementMethodOther<%# Eval("ID") %>"  /> </p>
</span>

<p>
<label>Contract PDF</label><br />
<input type="file" id="ContractFileName<%# Eval("ID") %>" name="ContractFileName<%# Eval("ID") %>" onchange="deleteUpload(<%# Eval("ID") %>);" />
 <asp:Literal runat="server" ID="fileUploadDetails" ></asp:Literal> 
 
 <div id="cfStatus<%# Eval("ID") %>" >
 <input type='hidden' name='deleteContractFile<%# Eval("ID") %>' id='deleteContractFile<%# Eval("ID") %>' value='false' />
 </div>
</p>

</div>
<script>
    $("#PeriodofPerformanceStart<%# Eval("ID") %>").datepicker();
    $("#PeriodofPerformanceEnd<%# Eval("ID") %>").datepicker();

</script>
 		   </ItemTemplate>
    </asp:ListView>

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
	   <asp:TextBox runat="server" ID="VendorEmail"  CssClass="validate[required]" ToolTip="Vendor Email is Required"   ></asp:TextBox>  
	  </p>



      <p> 
	  
      <label class="bold" for="ctl00_mainContentCP_VendorPhone">
		    Phone<span title="Required" class="fg-red">*</span>
		</label> <br />
	   <asp:TextBox runat="server" ID="VendorPhone"  CssClass="validate[required]" ToolTip="Vendor Phone is Required"  ></asp:TextBox>  
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
         
          <asp:ListItem   Text="Temporarily Disable this resource, but still show it on the website" Value="Disabled"></asp:ListItem>
       <asp:ListItem  Text="Make this resource Inactive and hide it on the website" Value="Inactive"></asp:ListItem>
      
      
      </asp:RadioButtonList>
      
      </p>
      <p></p> <p></p>
      <p>
       <asp:Button runat="server" ID="AddResource" class="btn" Text="Save" onclick="AddResource_Click" />    <input  class="btn"  id="Text1" value="Cancel"  type="button"  style="width:150px" onclick="return Text1_onclick(); return false;" /> 
       </p>

	   <ux:AuditLog runat="server" ID="AuditLogUX" />
	  
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	   </div>
	  </div>
      </span>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

