<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="newRequest.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/requests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<%@ Register Src="~/admin/helprequests/resource.ascx" TagPrefix="ux" TagName="Resource" %>
<%@ Register Src="~/admin/helprequests/subject.ascx" TagPrefix="ux" TagName="Subject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
    <style>

.div1, .div2 {
    display: inline-block;
    width: 10%;
}

 .column-5 {
    width: 46.667%;
}
input, textarea, select {
    max-width: 90%;
    width: 90%;
}
</style>

    <script>


        function changeInfo(id) {
            if (id != "undefined") {
                var url = "/admin/controls/ajaxResourceDD.aspx?rid=" + id;

                $.get(url, function (data) {
                    $("#results").html(data);

                });

                if (id != "") {
                    $('#SubjectAreaData').removeClass("validate[required]");
                }
            }
        }

        function changeResourceDD(id) {
            if (id != "undefined") {
                var url = "/admin/controls/ajaxResourceDD.aspx?rid=" + id;

                $.get(url, function (data) {
                    $("#results").html(data);

                });
            }
        }

        $(document).ready(function () {

            $('#SubjectAreaData').change(function () {
                var whichOne = $('#whichResources').val();
                var url = "/admin/helprequests/findResource.aspx?subjectID=" + $(this).val() + "&loadResFor=" + whichOne;

                $(document).ajaxStart(function () {
                    $("#loading").show();
                    //  $("#resourceDiv").hide();
                });
                $(document).ajaxStop(function () {
                    $("#loading").hide();
                    //$("#ajaxContact").show();
                });

                $.get(url, function (data) {
                    $("#resourceDiv").html(data);

                });
                if ($(this).val() != "") {
                    $('#resourceData').removeClass("validate[required]");
                }


            });

        });


 </script>

   <script>
       $(document).ready(function () {
           $("#ctl00_mainContentCP_RequestedDate").datepicker();
           $("#aspnetForm").validationEngine('attach');
       });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
    <div id="title-bar">
    <h2>
      <span class="favorite-id">
          New Help Request</span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


<asp:HiddenField ID="searchStringHF" runat="server" />
 
<asp:HiddenField ID="RequestID" runat="server" />
<asp:HiddenField ID="AssignmentIDHF" runat="server" />
<asp:HiddenField ID="AccesIDHF" runat="server" />
<div class="grid" id="admin-tools-int-content">
	     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
         <li><a href="/admin/helprequests/">Help Requests</a></li>
         <li>   New  Help Request</li>  
    </ul> 

   </div>
	  <div class="row-12">
	 <div class="column-9">
	  
	  <!-- END COLUMN -->
	  
 
 <p>
 <asp:DropDownList runat="server" ID="RequestTypeDD" DataTextField="FormName" class="validate[required]"
         DataValueField="ID" AutoPostBack="true"
         onselectedindexchanged="RequestTypeDD_SelectedIndexChanged"   ></asp:DropDownList>
 </p>
       
          <h3>Request Details</h3>
          
          <asp:Panel runat="server" ID="PasswordAssistanceFormPanel" Visible="false" >
          <h4>
        <span id="contact_option6_topic">For which Resource do you need password assistance?<span title="Required" class="fg-red">*</span><br></span></h4>
            <%=resourceDropDownData%>

       
       <p>
          <label for="Additionalnotesordetails" >Additional Notes Or Details</label>
          <asp:TextBox runat="server" ID="Additionalnotesordetails" TextMode="MultiLine" ></asp:TextBox>
          </p>
</asp:Panel>

<asp:Panel runat="server" ID="SuggestingNewResourceFormPanel" Visible="false" >

  <p>
      <label class="bold"  for="NameOfResource" >Name Of Resource <span title="Required" class="fg-red">*</span></label><br/>
       <asp:TextBox runat="server" CssClass="validate[required]"  ID="NameOfResource"  ></asp:TextBox>
       </p>


  <p>
      <label class="bold" for="WhatIsTheBusinessNeedForThisResource" >What is the business need for this resource? <span title="Required" class="fg-red">*</span></label><br/>
       <asp:TextBox runat="server" CssClass="validate[required]"  ID="WhatIsTheBusinessNeedForThisResource" TextMode="MultiLine" ></asp:TextBox>
       </p>


         <p>
      <label  for="AdditionalInformation" >Additional information about this resource (so we can learn more):</label><br/>
       <asp:TextBox runat="server" ID="AdditionalInformation" TextMode="MultiLine" ></asp:TextBox>
       </p>

                <p>
      <label  for="ApprovingSupervisor" >Approving Supervisor :</label><br/>
       <asp:TextBox runat="server" ID="ApprovingSupervisor" TextMode="MultiLine" ></asp:TextBox>
       </p>










</asp:Panel>



<asp:Panel runat="server" ID="ResearchAssistanceFormPanel" Visible="false" >

<label class="bold">What do you need help with? <span title="Required" class="fg-red">*</span> </label><br />
(Select either a subject or a resource, or both.) 
  <%=subjectAreadDropDownData%>
   <div id="loading"></div>
   <br />
    <div id="resourceDiv">
 
<%=resourceDropDownData%>
  <p></p>
</div>

 <p>
          <label for="HowCanWeHelp" class="bold" >How can we help?<span title="Required" class="fg-red">*</span></label>
          <asp:TextBox runat="server"   ID="HowCanWeHelp" TextMode="MultiLine" class="validate[required]" ToolTip="How can we help?" ></asp:TextBox>
          </p>
</asp:Panel>


<asp:Panel runat="server" ID="TrainingRequestFormPanel" Visible="false" >

<label class="bold">Topic of Training <span title="Required" class="fg-red">*</span> </label>
  <%=subjectAreadDropDownData%>
   <div id="Div1"></div>
   <br />
    <div id="Div2">
 
<%=resourceDropDownData%>
  <p></p>
</div>

 <p>
          <label for="NumberOfAttendees" class="bold" >Number of Attendees <span title="Required" class="fg-red">*</span></label>
          <asp:TextBox runat="server" CssClass="validate[required]"  ID="NumberOfAttendees"  ></asp:TextBox>
          </p>
 <p>
          <label for="RequestedDate"   >Requested Date</label>
          <asp:TextBox runat="server" ID="RequestedDate"  ></asp:TextBox>
          </p>


      

             <p>
          <label for="Location"   >Requested Time of Day</label>
      
          <asp:DropDownList runat="server" ID="RequestedTime">
          <asp:ListItem Text="- Select a Time of Day - " Value="None"  > </asp:ListItem>
            <asp:ListItem Text="Morning" Value="Morning" ></asp:ListItem>
              <asp:ListItem Text="Afternoon" Value="Afternoon" ></asp:ListItem>
               <asp:ListItem Text="Either Morning or Afternoon" Value="Either Morning or Afternoon" ></asp:ListItem>

          </asp:DropDownList>
      
          </p>


           <p>
          <label for="Location"   >Location</label><br />
          <asp:TextBox runat="server" ID="Location"    ></asp:TextBox>
          </p>



 <p>
          <label for="OtherInformation"   >Other Information</label>
          <asp:TextBox runat="server" ID="OtherInformation" TextMode="MultiLine"  ></asp:TextBox>
          </p>











</asp:Panel>



<asp:Panel runat="server" ID="ReportingProblemFormPanel" Visible="false" >

  <script>

            $(function () {

            $("table[id*=ctl00_mainContentCP_ProblemWith] input").addClass("validate[required]");

                $("#spanResource").hide();
                    $("#spanWebsite").hide();
                <%=HideMe %>
                $('#ctl00_mainContentCP_ProblemWith_0').click(function (e) {
                    //  e.preventDefault();
                    $("#spanResource").show();
                    $("#spanWebsite").hide();
                     $("#pageTrouble").val("None");
                     $("#ProblemResource option:contains(0)").attr('selected', true);

                });


                $('#ctl00_mainContentCP_ProblemWith_1').click(function (e) {  //website
                    // e.preventDefault();
                    $("#spanResource").hide();
                    $("#spanWebsite").show();
                      $("#pageTrouble").val("");
                     $("#ProblemResource").val("");
                      $("#ProblemResource").val("0");
                  //  $("#ProblemResource option:contains(-1)").attr('selected', true);
                });


            });


 
    </script>


     <p>
     <span class="bold">The problem is with:  <span class="fg-red" title="Required">*</span></span>
     <asp:RadioButtonList runat="server" ID="ProblemWith" >
     <asp:ListItem Text="A resource (e.g. a database or journal)."  Value="A Resource" ></asp:ListItem>
     <asp:ListItem Text="The SSA DL website" Value="The Website"></asp:ListItem>
     </asp:RadioButtonList>

     
        
    
    </p> <p>
    <div id="spanResource">
        <label for="ProblemResource" class="bold">
            Which resource are you having trouble with? <span class="fg-red" title="Required">*</span>
        </label>  
   <%=resourceDropDownData%>
         
       
       

    </div>
         
      
             <div ID="spanWebsite">
             <label class="bold" for="pageTrouble">
             Which page are you having trouble with? <span class="fg-red" title="Required">*</span>
             </label>
             <asp:TextBox ID="pageTrouble" CssClass="validate[required,custom[url]]"  runat="server"></asp:TextBox>
             </div>
             <p>
             </p>
             <p>
                 <label class="bold" for="ProblemDescription">
                 Please describe the problem you&#39;re having <span class="fg-red" title="Required">*</span> </label><br/>
                 <asp:TextBox ID="ProblemDescription" CssClass="validate[required]" runat="server" TextMode="MultiLine"></asp:TextBox>
             </p>
             <p>
                 <label   for="Screenshotimage">
                 Screen shot image</label><br/>
               <asp:FileUpload runat="server" ID="ScreenShotImageFile" />
             </p>  

         </asp:Panel>

<asp:Panel runat="server" ID="RequestForAnArticlePanel" Visible="false" >
<p>
<asp:Label  CssClass="bold"  runat="server" >Article Title or Keyword <span title="Required" class="fg-red">*</span> </asp:Label><br />
<asp:TextBox runat="server" CssClass="validate[required]" ToolTip="Article Title is required" ID="ArticleTitleorKeyword" ></asp:TextBox>
</p>

<p>
<asp:Label  CssClass="bold"  ID="Label1" runat="server" >Author(s) <span title="Required" class="fg-red">*</span></asp:Label><br />
<asp:TextBox runat="server" CssClass="validate[required]"  ID="Author" ></asp:TextBox>
</p>


<p>
<asp:Label  CssClass="bold"  ID="Label2" runat="server" >Journal Title <span title="Required" class="fg-red">*</span></asp:Label><br />
<asp:TextBox runat="server" CssClass="validate[required]"  ID="JournalTitle" ></asp:TextBox>
</p>


<p>
<asp:Label ID="Label3" runat="server" >Issue Volume Page </asp:Label><br />
<asp:TextBox runat="server" ID="IssueVolumePage" ></asp:TextBox>
</p>



<p>
<asp:Label ID="Label4" runat="server" >Year Published</asp:Label><br />
<asp:TextBox runat="server" ID="YearPublished" ></asp:TextBox>
</p>


<p>
<asp:Label ID="Label5" CssClass="bold" runat="server" >Why Do You Need This Article? <span title="Required" class="fg-red">*</span></asp:Label><br />
<asp:TextBox runat="server" CssClass="validate[required]"  ID="WhyDoYouNeedThisArticle" TextMode="MultiLine" ></asp:TextBox>
</p>












</asp:Panel>

<asp:Panel runat="server" ID="OtherFormPanel" Visible="false" >

  <p>
      <label  for="comments" class="bold" >Please describe your question or comment  <span title="Required" class="fg-red">*</span></label><br/>
       <asp:TextBox runat="server" ID="comments" CssClass="validate[required]" TextMode="MultiLine" ></asp:TextBox>
       </p>
</asp:Panel>
 

         <p>
      <label  for="InternalNotes" >Internal Notes</label><br/>
       <asp:TextBox runat="server" ID="InternalNotes" TextMode="MultiLine" ></asp:TextBox>
       </p>

       <p>
             
       <asp:Button CssClass="btn" runat="server" ID="SaveForLaterBtn" Text="Save For Later" 
               onclick="SaveForLater_Click" ></asp:Button>
       
       <asp:Button CssClass="btn" runat="server" ID="MarkAsClosedBtn" 
               Text="Mark As Closed" onclick="MarkAsClosedBtn_Click" 
                ></asp:Button>
           <input id="ctl00_mainContentCP_Cancel" class="btn" onclick="javascript: location.href = '/admin/helprequests/'" name="ctl00$mainContentCP$Cancel" type="button" value="Cancel" /></p>

    


      </div></div> </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

