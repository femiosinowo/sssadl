<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="emailReport.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<%@ Register Src="~/admin/controls/auditLog.ascx" TagPrefix="ux" TagName="AuditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Email a Report</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->


	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
          <li> Reports</li>
         <li> Email a Report </li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">

      <h3>Email "<%=ReportName%>" Report</h3>
	  <asp:Panel runat="server" ID="InvalidEmailPanel" Visible="false" >
      <div class="container-red">
	     <h4>These emails are not valid emails</h4>
		
	  <%=invalidEmailsFound %>
	  
	  </div>
      
      </asp:Panel>
	  	  <asp:Panel runat="server" ID="EmailSent" Visible="false" >
      <div class="container-blue">
	     <h4>Report link sent.</h4>
		
	  <%=invalidEmailsFound %>
	  
	  </div>
      
      </asp:Panel>
	  
	 
	   <p>
      <label for="ctl00_mainContentCP_emails" class="bold">Email addresses: <br /> (Seperate multiple addresses with commas.)</label> <br/>
      <asp:TextBox runat="server" ID="emails" CssClass="validate[required]" TextMode="MultiLine" ></asp:TextBox>
      </p>

 
	  
       
	   
	   <p>
      

      </p>

       <div class="buttons">
        <asp:Button CssClass="btn" runat="server" ID="Send" Text="Send" 
               onclick="Send_Click" /> 
	  	 
				 
				  <input id="ctl00_mainContentCP_Cancel" class="btn" onclick="javascript: history.back()" name="ctl00$mainContentCP$Cancel" type="button" value="Cancel" />
	  	 <asp:HiddenField runat="server" ID="WhereFrom" />
        
	  </div>

      <p>
      

      </p><p>
      

      </p>
        
	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
	  <!-- END GRID -->
	  
	  </div>
	  <!-- END PAGE CONTENT -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

