<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="autoreplyforms.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false"  %>
<%@ Register Src="~/admin/autoreply/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 

<style>

.div1, .div2 {
    display: inline-block;
    width: 10%;
}

 
 
</style> 

<script>

    $(document).ready(function () {
                $("#aspnetForm").validationEngine('attach');
    });


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
    <div id="title-bar">
    <h2>
      <span class="favorite-id">
            Auto-Reply Messages for <%=pageTitle %></span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


 
<div class="grid" id="admin-tools-int-content">
	         <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
          <li>Settings</li> 
         <li><a href="/admin/autoreply/">Auto Reply</a></li>
          <li> Auto-Reply Messages for <%=pageTitle %></li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      
   <p>The following messages will be used as defaults when an event is triggered for this request type.</p>
   

   
      <h3>New Request Received Message </h3>   <p>
         <asp:CheckBox runat="server" Checked="true" ID="TurnOnNR" Text="Turn on New Request Received Messages" /></p>
         <p>
      <label class="bold" for="fromEmailNR" >Email Address</label> <br/>
       <asp:TextBox runat="server" ID="fromEmailNR" CssClass="validate[custom[multiemail]]"   ></asp:TextBox>
    </p>
          <p>
      <label class="bold" for="SubjectNR" >Subject</label> <br/>
       <asp:TextBox runat="server" ID="SubjectNR"   ></asp:TextBox>
       </p>

         <p>
      <label class="bold"  for="BodyTextNR" >Body Text</label>  
       <asp:TextBox runat="server" ID="BodyTextNR" TextMode="MultiLine" CssClass="html" Height="250"  ></asp:TextBox>
       </p>


             <h3>Closed Request Received Message </h3>   <p>
         <asp:CheckBox runat="server" Checked="true" ID="TurnOnCRR" Text="Turn on Closed Request Received Messages" /></p>
         <p>
      <label class="bold" for="fromEmailCRR" >Email Address</label> <br/>
       <asp:TextBox runat="server" ID="fromEmailCRR" CssClass="validate[custom[multiemail]]"  ></asp:TextBox>
    </p>
          <p>
      <label class="bold" for="SubjectCRR" >Subject</label> <br/>
       <asp:TextBox runat="server" ID="SubjectCRR"   ></asp:TextBox>
       </p>

         <p>
      <label class="bold"  for="BodyTextCRR" >Body Text</label>  
       <asp:TextBox runat="server" ID="BodyTextCRR" TextMode="MultiLine" Height="250"  CssClass="html"  ></asp:TextBox>
       </p>

 
     
     <p>
     
     <asp:Button ID="SaveBtn" CssClass="btn" runat="server" Text="Save" onclick="SaveBtn_Click" />   
             <asp:LinkButton  CssClass="btn" runat="server" ID="LinkButton1" Text="Cancel" onclick="LinkButton1_Click" 
                 /> 
	  	 <asp:HiddenField runat="server" ID="WhereFrom" />
     </p>

        <ux:AuditLog runat="server" ID="AuditLogUX" />


      
 
      </div></div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

