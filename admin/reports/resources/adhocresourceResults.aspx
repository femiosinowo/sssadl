<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="adhocresourceResults.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Ad Hoc  Resource</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->


	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" ID="SideNav" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3>Ad Hoc Resource Report</h3>
	  
	  <ul>
	    <li><%=RunMEssage %> </li>
		<li>Standard parameters:<br>
		   <%=DisplayParameters %></li>
            <asp:HyperLink runat="server" Text="Change the report parameters" ID="ChangeParameters" ></asp:HyperLink> 
	  </ul>
 <p><strong>Scheduling:</strong> <%=SchedulingMessage%>.<br/>
	  <a href="/admin/reports/scheduledreport.aspx?reportid=<%=reportID %>&scheduledreportid=<%=scheduledreportid %>"><%=SchedulingMessageTag%> the schedule and report recipients.</a></p>

	  <div class="buttons">
	  	<a href="/admin/reports/emailReport.aspx?reportid=<%=reportID %>" class="btn">Email This Report</a>
        <asp:HyperLink runat="server"  CssClass="btn"  ID="RunSchudule" Visible="false" 
              Text="Run on a Schedule" onclick="ExcelClick_Click"   ></asp:HyperLink>
	  	<asp:LinkButton runat="server"  CssClass="float-right"  ID="ExcelClick" 
              Text="Download Report to Excel" onclick="ExcelClick_Click"   ></asp:LinkButton>
	  </div>
	  
   

   <caption><h3></h3></caption>
       

     <asp:GridView CssClass="table table-bordered table-striped" OnRowDataBound="grd_RowDataBound" runat="server" ID="ResultsGridView" AutoGenerateColumns="true" EmptyDataText="No records Found" >
      
     </asp:GridView>




      



 
	  
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

