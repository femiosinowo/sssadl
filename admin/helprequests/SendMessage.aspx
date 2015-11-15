<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/requests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
<style>

.div1, .div2 {
    display: inline-block;
    width: 10%;
}

 
 
</style><style>
 

  .accordion > p a::before {
    
    
    float:right;
}
 
 
 


        </style>
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
<asp:HiddenField ID="sortOrderbyHF" Value=" order by lastname asc" runat="server" />
<asp:HiddenField ID="ShowInactive" Value="False" runat="server" />
<asp:HiddenField ID="RequestID" runat="server" />
<asp:HiddenField ID="ResourceIDHF" runat="server" />
<asp:HiddenField ID="RecordExistsHF" Value="false" runat="server" />
 <asp:HiddenField ID="fromEmail"   runat="server" />
 <asp:HiddenField ID="ResourceNameHF" Value="false" runat="server" />
 <asp:HiddenField ID="RequestorNameHF" Value="false" runat="server" />
 
  <asp:HiddenField ID="RequestorEmailHF"  runat="server" />
<div class="grid" id="admin-tools-int-content">
	    <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li><a href="/admin/helprequests/">Help Requests</a></li><li> New Help Request</li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	 
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
     
      <div>
      
   
   
  
    
   <h4>  <%=requestName %>  Request for <%=requestorName %>  </h4>
   <asp:HiddenField runat="server" ID="RequestorPIN" />
  

       <h3>Request <%=WhatMessage %>: Message to Requestor</h3>

         <p>
      <label  for="Subject" >Subject</label> <br/>
       <asp:TextBox runat="server" ID="Subject"   ></asp:TextBox>
       </p>


         <p>
      <label  for="BodyText" >Body Text</label> <br/>
       <asp:TextBox runat="server" ID="BodyText" CssClass="html" TextMode="MultiLine" ></asp:TextBox>
       </p>

         
       <p>
       <label>Attach a File</label> <br/>
       <asp:FileUpload runat="server" ID="attachment" />
       </p>
       <p></p>
       <p>
       
       
       <asp:Button CssClass="btn" runat="server" ID="SendBtn" Text="Send" onclick="SendBtn_Click" 
                ></asp:Button>   
           <asp:Button CssClass="btn" runat="server" 
               ID="SaveForLater" Text="Save For Later" onclick="SaveForLater_Click1"  
                ></asp:Button>
                <asp:Button runat="server" CssClass="btn" ID="SkipMessage"  Text="Skip Message" 
               Visible="false" onclick="SkipMessage_Click" />
       <input id="ctl00_mainContentCP_Cancel" class="btn" onclick="javascript: location.href = '/admin/helprequests/'" name="ctl00$mainContentCP$Cancel" type="button" value="Cancel" />
       </p>

        
      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

