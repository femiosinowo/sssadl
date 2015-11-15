<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="epasswords.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false"  %>
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
            Auto-Reply Messages for e-Passwords Requests</span> 
 

                    
                  


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
         <li>Auto-Reply Messages for e-Passwords Requests</li>  
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
       <asp:TextBox runat="server" ID="fromEmailNR" CssClass="validate[custom[multiemail]]"  ></asp:TextBox>
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




   <h3>Approved Request Message </h3>
   <p>
    <asp:CheckBox runat="server" Checked="true" ID="TurnOnRRMessages" Text="Turn on Approved Request Messages" /></p>
    <p>
      <label class="bold" for="fromEmailRR" >Email Address</label> <br/>
       <asp:TextBox runat="server" ID="fromEmailRR" CssClass="validate[custom[multiemail]]"  ></asp:TextBox>
    </p>
  <p>
      <label class="bold" for="SubjectRR" >Subject</label> <br/>
       <asp:TextBox runat="server" ID="SubjectRR"   ></asp:TextBox>
       </p>
         <p>
      <label  for="BodyTextRR" class="bold" >Body Text</label> 
       <asp:TextBox runat="server" ID="BodyTextRR"  CssClass="html" TextMode="MultiLine" Width="500" Height="250"   ></asp:TextBox>
       </p>

         <h3>Request Declined Messages</h3>   <p>
         <asp:CheckBox runat="server" Checked="true" ID="TurnOnRCMessages" Text="Turn on Request Declined Messages" /></p>
         <p>
      <label class="bold" for="fromEmailRC" >Email Address</label> <br/>
       <asp:TextBox runat="server" ID="fromEmailRC" CssClass="validate[custom[multiemail]]"  ></asp:TextBox>
    </p>
          <p>
      <label class="bold" for="SubjectRC" >Subject</label> <br/>
       <asp:TextBox runat="server" ID="SubjectRC"   ></asp:TextBox>
       </p>

         <p>
      <label class="bold"  for="BodyTextRC" >Body Text</label>  
       <asp:TextBox runat="server" ID="BodyTextRC"  CssClass="html" TextMode="MultiLine" Height="250"  ></asp:TextBox>
       </p>

     
     
     
     
     

     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     
     <p>
     
     <asp:Button ID="SaveBtn"  CssClass="btn" runat="server" Text="Save" onclick="SaveBtn_Click" />   
         <asp:Button  CssClass="btn" ID="CancelBtn" runat="server" Text="Cancel" 
             onclick="CancelBtn_Click" /> 
     </p>

        <ux:AuditLog runat="server" ID="AuditLogUX" />


      
 
      </div></div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

