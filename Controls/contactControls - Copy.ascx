<%@ Control Language="C#" AutoEventWireup="true" CodeFile="contactControls - Copy.ascx.cs"
    Inherits="Controls_contactControls" %>
<asp:Panel runat="server" ID="TrainingRequest" Visible="false">
    <h4>
    <input id="whichResources" value="TrainingRequest" type="hidden" />
        <span id="contact_option6_topic">Topic of Training<span class="fg-red" title="Required">*</span><br /></h4>
            (Select either a <span id="contact_option6_subject">subject</span> or a <span id="contact_option6_resource">
                resource</span>, or both.)</span></h4>
<%--    <p>
        <select name="SubjectArea" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
            onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
            ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="SubjectArea"
            ektdesignns_caption="Subject Area" ektdesignns_name="SubjectArea" title="Subject Area" required>
            <option selected="selected" value="">- Select a Subject -</option>
            <%=SubJectAreaDD%>
        </select></p>
    <p>
    </p>
    <select name="Resource" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
        onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
        ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="Resource"
        ektdesignns_caption="Resource" ektdesignns_name="Resource" title="Resource" required>
        <option selected="selected" value="">- Select a Resource - </option>
        <%=resourceDD%>
    </select>--%>
</asp:Panel>
<asp:Panel runat="server" ID="ReportingAProblem" Visible="false">
    <script>
        $(function () {
            $("#spanResource").hide();
            $("#spanWebsite").hide();

            $('#AResource').click(function (e) {
                //  e.preventDefault();
                $("#spanResource").show();
                $("#spanWebsite").hide();
                $("#pageTrouble").val("None");
                $("#ProblemResource option:contains(0)").attr('selected', true);

            });


            $('#TheWebsite').click(function (e) {
                // e.preventDefault();
                $("#spanResource").hide();
                $("#spanWebsite").show();
                $("#pageTrouble").val("");
               // $("#ProblemResource").val("");
                $("#ProblemResource").val("0");
               // $("#ProblemResource option:contains(-1)").attr('selected', true);
            });

			//$('#Number_ofAttendees').addClass("validate[custom[integer]]");
        });


 
    </script>
    <fieldset>
        <ektdesignns_choices title="The problem is with" ektdesignns_nodetype="element" id="The_problem_is_with"
            name="The_problem_is_with" ektdesignns_caption="The problem is with" ektdesignns_name="The_problem_is_with">
				<legend class="bold">The problem is with: <span class="required">*</span></legend>
				<input type="radio" ektdesignns_nodetype="item" name="The_problem_is_with" id="AResource" value="A Resource" required="required"  />
				 
				    <label for="AResource" class="inline-label">A resource (e.g. a database or journal).</label><br>
                    <input type="radio" ektdesignns_nodetype="item" name="The_problem_is_with" id="TheWebsite" value="The Website" required="required"   />
				 
				    <label for="TheWebsite" class="inline-label">The SSA DL website.</label>
                     </ektdesignns_choices>
    </fieldset>

    <span id="spanResource">
        <label for="ProblemResource" class="bold">
            Which resource are you having trouble with? <span class="fg-red" title="Required">*</span>
        </label>
        <select  name="ProblemResource" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
            onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
            ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="ProblemResource"
            ektdesignns_caption="Resource" ektdesignns_name="Resource" title="Resource"  >
            <option selected="selected" value="">- Select a Resource - </option>
            <%=resourceDD%>
             <option disabled="disabled" value="0"></option>
        </select>
    </span>
    
    
    <span id="spanWebsite">
        <label for="pageTrouble" class="bold">
            Which page are you having trouble with? <span class="fg-red" title="Required">*</span>
        </label>
      


            <input type="text" name="pageTrouble" ektdesignns_nodetype="element" id="pageTrouble" ektdesignns_caption="Which page are you having trouble with"
             ektdesignns_name="pageTrouble" title="Which page are you having trouble with" class="design_textfield"  
             ektdesignns_validation="string-req" ektdesignns_datatype="string" ektdesignns_basetype="text" 
             ektdesignns_schema="&lt;xs:minLength value=&quot;1&quot;&gt;&lt;/xs:minLength&gt;" ektdesignns_validate="re:/\S+/" 
            ektdesignns_invalidmsg="Cannot be blank" onblur="design_validate_re(/\S+/,this,'Cannot be blank');" size="24"   />
    </span>
    

    <p>
    </p>
</asp:Panel>




<asp:Panel runat="server" ID="PasswordAssistancePanel" Visible="false">
    <h4>
        <span id="contact_option6_topic">For which Resource do you need password assistance?<span class="fg-red" title="Required">*</span><br /></h4>
            
    <select name="Resource" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
        onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
        ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="Select2"
        ektdesignns_caption="Resource" ektdesignns_name="Resource" title="Resource" required>
        <option <%=selectedText %> value="">- Select a Resource - </option>
        <%=resourceDD%>
    </select>
</asp:Panel>

<asp:Panel runat="server" ID="RequestAccessPanel" Visible="false">
    <h4>
        <span id="contact_option6_topic">Select the Database you wish to access<span class="fg-red" title="Required">*</span><br /></h4>
            
    <select name="Resource" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
        onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
        ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="Select1"
        ektdesignns_caption="Resource" ektdesignns_name="Resource" title="Resource">
        <option <%=selectedText %> value="">- Select a Resource - </option>
        <%=resourceDD%>
    </select>
</asp:Panel>


<asp:Panel runat="server" ID="ResearchAssistancePanel" Visible="false">

<script>


    $(document).ready(function () {

        $('#SubjectArea').change(function () {
            // alert('aaa');
            var whichOne = $('#whichResources').val();
            var url = "/Templates/Ajax/findResource.aspx?subjectID=" + $(this).val() + "&loadResFor=" + whichOne;

            $(document).ajaxStart(function () {
            //    $("#loading").show();
                //  $("#resourceDiv").hide();
            });
            $(document).ajaxStop(function () {
              //  $("#loading").hide();
                //$("#ajaxContact").show();
            });

            $.get(url, function (data) {
                $("#resourceDiv").html(data);

            });


        });


        $('#Resource').change(function () {
            // alert('aaa');
            if ($(this).val() != '') {
                $("#SubjectArea").removeAttr('onblur');
            } else {
                //onblur = "design_validate_select(1, this, 'First item is not a valid selection.')"
                $("#SubjectArea").attr('onblur', "design_validate_select(1, this, 'First item is not a valid selection.')");
            }
            //var whichOne = $('#whichResources').val();



        });

    });


</script>

<asp:Panel runat="server" ID="IntroPanel" >
<input id="whichResources" value="ResearchAssistance" type="hidden" />
    <h4>
        <span id="contact_option6_topic">What do you need help with?: <span class="fg-red" title="Required">*</span><br /></h4>
            (Select either a <span id="Span2">subject</span> or a <span id="Span3">
                resource</span>, or both.)</span></h4>
                </asp:Panel>
    <p>
     
        <select name="SubjectArea" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
            onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
            ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="SubjectArea"
            ektdesignns_caption="Subject Area" ektdesignns_name="SubjectArea" title="Subject Area"   >
            <option selected="selected" value="">- Select a Subject -</option>
            <%=SubJectAreaDD%>
        </select></p>
    <p>
    </p>
    
    <div id="resourceDiv">
    <select name="Resource" ektdesignns_minoccurs="1" ektdesignns_validation="select-req"
        onblur="design_validate_select(1, this, 'First item is not a valid selection.')"
        ektdesignns_maxoccurs="1" size="1" ektdesignns_nodetype="element" id="Resource"
        ektdesignns_caption="Resource" ektdesignns_name="Resource" title="Resource" >
        <option selected="selected" value="">- Select a Resource - </option>
        <%=resourceDD%>
    </select>
    </div>
</asp:Panel>