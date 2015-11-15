<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="newRequest.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/requests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
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

         $("#ctl00_mainContentCP_ApproveBtn").removeAttr("disabled", "disabled");
         if (id != "undefined") {
             var url = "/admin/controls/ajaxResourceDD.aspx?rid=" + id + "&mathRandom=" + Math.random();;

             $.get(url, function (data) {
                 $("#results").html(data);

             });
         }
     }


$(document).ready(function () {

    userPIN = $("#ctl00_mainContentCP_RequestorPIN").val();
    if (userPIN != "") {
        userEmail = $("#ctl00_mainContentCP_RequestorEmail").val();

        var url = "/admin/requests/ajaxgetuserdetails.aspx?action=find&pin=" + userPIN + "&email=" + userEmail + "&mathRandom=" + Math.random();

        //    var aa = '#addFavDiv' + resourceID;
        //    var bb = '#removeFavDiv' + resourceID;
        //    $(aa).hide();
        //    $(bb).show();

        $.get(url, function (data) {
            $("#getInfo").html(data);

        });


        var rid = $("#ctl00_mainContentCP_ResourceDD").val();
        if (rid != "undefined" || rid !="") {
            var url = "/admin/controls/ajaxResourceDD.aspx?rid=" + rid;

            $.get(url, function (data) {
                $("#results").html(data);

            });
        }
    }


    $('#ctl00_mainContentCP_RequestorPIN').change(function (e) {

        //btn - get - information
        userPIN = $("#ctl00_mainContentCP_RequestorPIN").val();
        userEmail = $("#ctl00_mainContentCP_RequestorEmail").val();

        var url = "/admin/requests/ajaxgetuserdetails.aspx?action=find&pin=" + userPIN + "&email=" + userEmail + "&mathRandom=" + Math.random();

        //    var aa = '#addFavDiv' + resourceID;
        //    var bb = '#removeFavDiv' + resourceID;
        //    $(aa).hide();
        //    $(bb).show();

        $.get(url, function (data) {
            $("#getInfo").html(data);

        });


    });





    $("#ctl00_mainContentCP_RequestedDate").datepicker();
    $("#aspnetForm").validationEngine('attach');

});

 
    
     
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
<span id="getInfo" ></span>
<div id="title-bar">
    <h2>
      <span class="favorite-id">
          New  e-Passwords Requests</span>  
                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">



<div class="grid" id="admin-tools-int-content">
	    <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
         <li><a href="/admin/requests/">E-Password Requests</a></li>
         <li>   New  e-Passwords Requests</li>  
    </ul> 

   </div>
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      <div class="container-yellow" <%=notifyStle %>>
        

      <div <%=divExistsApprovedDiv %>>
      <h4>Existing Assigned Password</h4>
      <p>There is an existing password assigned to this user for this resource on <%=approvedDate %> by <%=approvedByDetails %>.</p>
      </div>
      </div>
      
   <div class="column-5" >
   <h3>e-Password Request for  </h3>
  
 
   <p><label for="RequestorPIN">PIN</label>
   
     <asp:TextBox ID="RequestorPIN"  runat="server"  CssClass="validate[required]" ToolTip="Please enter a valid PIN" ></asp:TextBox>
   
   </p>
     <p> <label for="RequestorName">Name </label>  
      <asp:TextBox ID="RequestorName" runat="server" ></asp:TextBox>
      </p>

         <p> <label for="RequestorTitle">Title</label>  
      <asp:TextBox ID="RequestorTitle" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="RequestorComponent">Component </label>  
      <asp:TextBox ID="RequestorComponent" runat="server" ></asp:TextBox>
      </p>

          <p> <label for="RequestorOfficeCode">    Office Code </label>  
      <asp:TextBox ID="RequestorOfficeCode" runat="server" ></asp:TextBox>
      </p>
  

         <p> <label for="RequestorEmail">Email </label>  
      <asp:TextBox ID="RequestorEmail" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="RequestorPhone">Phone</label>  
      <asp:TextBox ID="RequestorPhone" runat="server" ></asp:TextBox>
      </p>


       <input type="hidden" name="RequestorLastName"  id="RequestorLastName"  runat="server" />
            <input type="hidden" name="RequestorFirstName"  id="RequestorFirstName"   runat="server"   />
            
            <input type="hidden" name="RequestorServer"  id="RequestorServer"  runat="server"  />
            <input type="hidden" name="RequestorUserDomain"  id="RequestorUserDomain"  runat="server"  />
    

      </div>
      <div class="column-5" >
       <h3>Request Submitted by</h3>

            <p> <label for="SubmittedByPIN">PIN </label>  
      <asp:TextBox ID="SubmittedByPIN" runat="server" Enabled="false" ></asp:TextBox>
      </p>


     <p> <label for="SubmittedByName">Name </label>  
      <asp:TextBox ID="SubmittedByName" runat="server" ></asp:TextBox>
      </p>

         <p> <label for="SubmittedByTitle">Title</label>  
      <asp:TextBox ID="SubmittedByTitle" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="SubmittedByComponent">Component </label>  
      <asp:TextBox ID="SubmittedByComponent" runat="server" ></asp:TextBox>
      </p>

          <p> <label for="SubmittedByOfficeCode">    Office Code </label>  
      <asp:TextBox ID="SubmittedByOfficeCode" runat="server" ></asp:TextBox>
      </p>
  

         <p> <label for="SubmittedByEmail">Email </label>  
      <asp:TextBox ID="SubmittedByEmail" runat="server" ></asp:TextBox>
      </p>


         <p> <label for="SubmittedByPhone">Phone</label>  
      <asp:TextBox ID="SubmittedByPhone" runat="server" ></asp:TextBox>
      </p>
      </div>
       
          <h3>Request Details</h3>
          <p>
          <label class="bold" for="">Resource Requesting Access To</label>
       <asp:DropDownList runat="server" DataTextField="ResourceName" DataValueField="ID" AutoPostBack="false"   CssClass="validate[required]" ToolTip="Please select one Resource"
                  ID="ResourceDD" onchange="changeInfo(this.value);" ></asp:DropDownList>
       </p>
<div id="results"></div>

       <asp:Panel runat="server" ID="AdminLoginPanel" Visible="false" >
       <p>
       <h3>Administrative Login Information</h3>
      <label> Login at: <%=resourceAdminResourceURL%></label><br/><label>Username: <%=resourceAdminUsername %>  </label><br/>
      <label> Password: <%=resourceAdminPassword%></label></p>
       </asp:Panel>
       <p>
      <label class="bold" for="whyNeedAccess" >Why do you need access?*</label><br/>
       <asp:TextBox runat="server" ID="whyNeedAccess" TextMode="MultiLine"   CssClass="validate[required]" ToolTip="Why do you need access?"></asp:TextBox>
       </p>
         <p>
      <label  for="InternalNotes" >Internal Notes</label><br/>
       <asp:TextBox runat="server" ID="InternalNotes"  TextMode="MultiLine" ></asp:TextBox>
       </p>

       <p>
       <div class="container-yellow" <%=notifyStle %>>
        

      <div <%=divExistsApprovedDiv %>>
      <h4>Existing Assigned Password</h4>
      <p>There is an existing password assigned to this user for this resource on <%=approvedDate %> by <%=approvedByDetails %>.</p>
      </div>
      </div>
             
       <asp:Button CssClass="btn" runat="server" ID="DeclineBtn" Text="Decline" 
               onclick="DeclineBtn_Click" ></asp:Button>
       
       <asp:Button CssClass="btn" runat="server" ID="ApproveBtn" Text="Approve" 
               onclick="Approve_Click" ></asp:Button>
       <asp:Button CssClass="btn" runat="server" ID="SaveForLaterBtn" Text="Save For Later" 
               onclick="SaveForLater_Click" ></asp:Button>
       </p>
      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

