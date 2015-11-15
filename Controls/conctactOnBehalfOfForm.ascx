<%@ Control Language="C#" AutoEventWireup="true" CodeFile="conctactOnBehalfOfForm.ascx.cs" Inherits="Controls_conctactOnBehalfOfForm" %>


    <script>
        $(function () {


            $('#IndividualName').prop("disabled", true);
            $('#IndividualTitle').prop("disabled", true);
            $('#IndividualComponent').prop("disabled", true);
            $('#IndividualOfficeCode').prop("disabled", true);
            $('#IndividualPhone').prop("disabled", true);
            //  $('#IndividualEmail').prop("disabled", false);
            // $('#IndividualsPIN').prop("disabled", false);

 


            $('#IndividualName').val('<%=myLastName %>');
            $('#IndividualTitle').val('<%=myTitle %>');
            $('#IndividualComponent').val('<%=myComponent %>');
            $('#IndividualOfficeCode').val('<%=myOffice %>');
            $('#IndividualPhone').val('<%=myPhone %>');
            $('#SubmittedForPIN').val('<%=myPin %>');
            $('#SubmittedForLastName').val('<%=myLastName %>');
            $('#SubmittedForFirstName').val('<%=myFirstName %>');
            $('#SubmittedForEMail').val('<%=myEmail %>');
            $('#SubmittedForOffice').val('<%=myOffice %>');
            $('#SubmittedForServer').val('<%=myServer %>');
            $('#SubmittedForUserDomain').val('<%=myUserDomain %>');


            $('#request_myself').click(function (e) {
                //  e.preventDefault();
                $('#IndividualName').val('<%=myLastName %>');
                $('#IndividualTitle').val('<%=myTitle %>');
                $('#IndividualComponent').val('<%=myComponent %>');
                $('#IndividualOfficeCode').val('<%=myOffice %>');
                $('#IndividualPhone').val('<%=myPhone %>');
                $('#SubmittedForPIN').val('<%=myPin %>');
                $('#SubmittedForLastName').val('<%=myLastName %>');
                $('#SubmittedForFirstName').val('<%=myFirstName %>');
                $('#SubmittedForEMail').val('<%=myEmail %>');
                $('#SubmittedForOffice').val('<%=myOffice %>');
                $('#SubmittedForServer').val('<%=myServer %>');
                $('#SubmittedForUserDomain').val('<%=myUserDomain %>');

                $('#btn-submit').prop("disabled", false);
            });


            $('#request_someone').click(function (e) {
                $('#IndividualName').val('');
                $('#IndividualTitle').val('');
                $('#IndividualComponent').val('');
                $('#IndividualOfficeCode').val('');
                $('#IndividualPhone').val('');
                $('#SubmittedForPIN').val('');
                $('#SubmittedForLastName').val('');
                $('#SubmittedForFirstName').val('');
                $('#SubmittedForEMail').val('');
                $('#SubmittedForOffice').val('');
                $('#SubmittedForServer').val('');
                $('#SubmittedForUserDomain').val('');

                $('#btn-submit').prop("disabled", true);
     
            });


        });


 
    </script>


    	<fieldset>
		<legend class="bold">This request is on behalf of:<span class="fg-red" title="Required">*</span></legend>
		<input type="radio" name="request" value="me" id="request_myself" checked>  <label for="request_myself" class="inline-label">Myself</label><br>
		<input type="radio" name="request" value="someone_else" id="request_someone"> <label for="request_someone" class="inline-label">Someone else</label>
		</fieldset>
		

        		<div class="someone_else_request container-yellow">
			 
			<h4>Who is this request for?</h4>
			
			<div class="request_get_information_wrapper">
				<div class="request_individual_pin_wrapper">
				<label for="request_individual_pin">Individual's PIN</label>
				<input type="text" name="IndividualsPIN" ektdesignns_nodetype="element" id="IndividualsPIN" onblur="design_validate_re(/\S+/,this,'Cannot be blank');" ektdesignns_caption="Individuals PIN" ektdesignns_name="IndividualsPIN" title="Individuals PIN" size="24" class="design_textfield" />
				</div>
				<span>OR</span>
				<div class="request_individual_email_wrapper">
				<label for="request_individual_email">Individual's Email Address</label>
				<input type="text" name="IndividualEmail" ektdesignns_nodetype="element" id="IndividualEmail" onblur="design_validate_re(/\S+/,this,'Cannot be blank');" ektdesignns_caption="Individual Email" ektdesignns_name="IndividualEmail" title="Individual Email" size="24" class="design_textfield" />
				</div>
			
				<input id="btn-get-information" onclick="getInformation()" class="btn" type="button" name="submit" value="Get Information"><span class="fg-red" title="Required">*</span>
			</div>
			
			<label for="request_individual_name">Individual's Name<span class="fg-red" title="Required">*</span> </label>
			<input type="text" name="IndividualName" ektdesignns_nodetype="element" id="IndividualName" ektdesignns_caption="Individual Name" ektdesignns_name="IndividualName" title="Individual Name" ektdesignns_validation="string-req" ektdesignns_datatype="string" ektdesignns_basetype="text" ektdesignns_schema="&lt;xs:minLength value=&quot;1&quot;&gt;&lt;/xs:minLength&gt;" ektdesignns_validate="re:/\S+/" ektdesignns_invalidmsg="Cannot be blank"  size="24" class="design_textfield" />
			
			<label for="request_individual_title">Individual's Title<span class="fg-red" title="Required">*</span> </label>
			<input type="text" name="IndividualTitle" ektdesignns_nodetype="element" id="IndividualTitle" ektdesignns_caption="Individual Title" ektdesignns_name="IndividualTitle" title="Individual Title" ektdesignns_validation="string-req" ektdesignns_datatype="string" ektdesignns_basetype="text" ektdesignns_schema="&lt;xs:minLength value=&quot;1&quot;&gt;&lt;/xs:minLength&gt;" ektdesignns_validate="re:/\S+/" ektdesignns_invalidmsg="Cannot be blank"  size="24" class="design_textfield" />
			
			<label for="request_individual_component">Individual's Component<span class="fg-red" title="Required">*</span> </label>
			<input type="text" name="IndividualComponent" ektdesignns_nodetype="element" id="IndividualComponent" ektdesignns_caption="Individual Component" ektdesignns_name="IndividualComponent" title="Individual Component" ektdesignns_validation="string-req" ektdesignns_datatype="string" ektdesignns_basetype="text" ektdesignns_schema="&lt;xs:minLength value=&quot;1&quot;&gt;&lt;/xs:minLength&gt;" ektdesignns_validate="re:/\S+/" ektdesignns_invalidmsg="Cannot be blank"  size="24" class="design_textfield" />
			
			<label for="request_individual_office_code">Individual's Office Code<span class="fg-red" title="Required">*</span> </label>
			<input type="text" name="IndividualOfficeCode" ektdesignns_nodetype="element" id="IndividualOfficeCode" ektdesignns_caption="Individual Office Code" ektdesignns_name="IndividualOfficeCode" title="Individual Office Code &#xA;Cannot be blank" ektdesignns_validation="string-req" ektdesignns_datatype="string" ektdesignns_basetype="text" ektdesignns_schema="&lt;xs:minLength value=&quot;1&quot;&gt;&lt;/xs:minLength&gt;" ektdesignns_validate="re:/\S+/" ektdesignns_invalidmsg="Cannot be blank"  size="24" class="design_textfield" />
			
			<label for="request_individual_phone">Individual's Phone<span class="fg-red" title="Required">*</span> </label>
			<input type="text" name="IndividualPhone" ektdesignns_nodetype="element" id="IndividualPhone" ektdesignns_caption="Individual Phone" ektdesignns_name="IndividualPhone" title="Individual Phone" ektdesignns_validation="string-req" ektdesignns_datatype="string" ektdesignns_basetype="text" ektdesignns_schema="&lt;xs:minLength value=&quot;1&quot;&gt;&lt;/xs:minLength&gt;" ektdesignns_validate="re:/\S+/" ektdesignns_invalidmsg="Cannot be blank"  size="24" class="design_textfield" />
			


            <input type="hidden" name="SubmittedForPIN" ektdesignns_nodetype="element" id="SubmittedForPIN" ektdesignns_caption="SubmittedForPIN" ektdesignns_name="SubmittedForPIN" title="SubmittedForPIN"  class="design_textfield" />

              <input type="hidden" name="SubmittedForLastName" ektdesignns_nodetype="element" id="SubmittedForLastName" ektdesignns_caption="SubmittedForLastName" ektdesignns_name="SubmittedForLastName" title="SubmittedForLastName"  class="design_textfield" />
            <input type="hidden" name="SubmittedForFirstName" ektdesignns_nodetype="element" id="SubmittedForFirstName" ektdesignns_caption="SubmittedForFirstName" ektdesignns_name="SubmittedForFirstName" title="SubmittedForFirstName"  class="design_textfield" />
            <input type="hidden" name="SubmittedForEMail" ektdesignns_nodetype="element" id="SubmittedForEMail" ektdesignns_caption="SubmittedForEMail" ektdesignns_name="SubmittedForEMail" title="SubmittedForEMail"  class="design_textfield" />
            <input type="hidden" name="SubmittedForOffice" ektdesignns_nodetype="element" id="SubmittedForOffice" ektdesignns_caption="SubmittedForOffice" ektdesignns_name="SubmittedForOffice" title="SubmittedForOffice"  class="design_textfield" />
            <input type="hidden" name="SubmittedForServer" ektdesignns_nodetype="element" id="SubmittedForServer" ektdesignns_caption="SubmittedForServer" ektdesignns_name="SubmittedForServer" title="SubmittedForServer"  class="design_textfield" />
            <input type="hidden" name="SubmittedForUserDomain" ektdesignns_nodetype="element" id="SubmittedForUserDomain" ektdesignns_caption="SubmittedForUserDomain" ektdesignns_name="SubmittedForUserDomain" title="SubmittedForUserDomain"  class="design_textfield" />
    
		</div> <span id="getInfo" ></span>
		
 
		
		<p class="margin-top">
		<input id="btn-submit" class="btn" type="submit" name="submit" value="Submit">
		</p>