<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Total Help Requests Report</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->


	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	    <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>Reports</li> <li>Total Help Requests Report</li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3>Total Help Requests Report</h3>
	  
	  <ul>
	    <li><%=RunMEssage %></li>
		 
	  </ul>
	  
	  <p><strong>Scheduling:</strong> <%=SchedulingMessage%>.<br/>
	  <a href="/admin/reports/scheduledreport.aspx?reportid=3&scheduledreportid=<%=scheduledreportid %>"><%=SchedulingMessageTag%> the schedule and report recipients.</a></p>
	  
	  <div class="buttons">
	  		<a href="/admin/reports/emailReport.aspx?reportid=3" class="btn">Email This Report</a>
	   
        <asp:LinkButton runat="server"  CssClass="float-right"  ID="ExcelClick" 
              Text="Download Report to Excel" onclick="ExcelClick_Click"   ></asp:LinkButton>
	  </div>
	  
	  <table class="table table-bordered table-striped">
	  <caption><h3></h3></caption>
	  
	  <thead>
	    <tr>
			<th id="header1" scope="col">Request Type</th>
			<th id="header2" scope="col">FY<%=lastYearDisplay%>(Full)</th>
			<th id="header3" scope="col">FY<%=lastYearDisplay%>(to Date)</th>
			<th id="header4" scope="col">FY<%=thisYearDisplay %>(to Date)</th>
			<th id="header5" scope="col">% Difference to Date</th>
			
		</tr>
	  </thead>
	  
	  <tbody>
	    
		 
		<asp:ListView ID="resultsLV"   runat="server" ItemPlaceholderID="phLV"  >
         
        <EmptyDataTemplate>
		 
            
        </EmptyDataTemplate>
        
        <LayoutTemplate>
         
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
		  <td headers="header1"><%# Eval("RequestType")%></th>
		  <td headers="header2"><%# Eval("LastYearFull")%></td>
		  <td headers="header3"><%# Eval("LastYeartoDate") %></td>
		  <td headers="header4"><%# Eval("thisYeartoDate") %></td>
		  <td headers="header5"><%# Eval("percentage")%></td>
		  
		</tr>
        </ItemTemplate>
    </asp:ListView>
	  </tbody>
	  </table>
	  
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

