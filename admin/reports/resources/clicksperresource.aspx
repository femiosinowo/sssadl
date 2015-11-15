<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="clicksperresource.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Clicks per Resource</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->


	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	          <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>Reports</li> <li>Clicks per Resource </li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" ID="SideNav" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3>Clicks Per Resource Report</h3>
	  
	  <ul>
	    <li><%=RunMEssage %> </li>
		 
	  </ul>
	  
	   <p><strong>Scheduling:</strong> <%=SchedulingMessage%>.<br/>
	  <a href="/admin/reports/scheduledreport.aspx?reportid=2&scheduledreportid=<%=scheduledreportid%>"><%=SchedulingMessageTag%> the schedule and report recipients.</a></p>
	  
	  <div class="buttons">
	  	<a href="/admin/reports/emailReport.aspx?reportid=2" class="btn">Email This Report</a>
	  	<asp:LinkButton runat="server"  CssClass="float-right"  ID="ExcelClick" 
              Text="Download Report to Excel" onclick="ExcelClick_Click"   ></asp:LinkButton>
	  </div>
	  
	  <table class="table table-bordered table-striped">
	  <caption><h3></h3></caption>
	  
	  <thead>
	    <tr>
			<th id="header1" scope="col">Resource Name</th>
			<th id="header2" scope="col">FY<%=lastYearDisplay%>(to date) # of Unique Visitors</th>
			<th id="header3" scope="col">FY<%=thisYearDisplay%>(to date) # of Unique Visitors</th>
			<th id="header4" scope="col">% Difference in Unique Visitors to Date</th>
			<th id="header5" scope="col">FY<%=lastYearDisplay%> (to Date) Number of Hits</th>
			<th id="header6" scope="col">FY<%=thisYearDisplay%> (to date) Number  of Hits</th>
            <th id="header7" scope="col">% Difference in Number of Hits to Date</th>
		</tr>
	  </thead>
	  
	  <tbody>


                  <asp:ListView ID="resourcesLV"  runat="server" ItemPlaceholderID="phLV" >
 
    
        <ItemTemplate>
<%--           <tr <asp:Literal runat="server" ID="rowClick" ></asp:Literal> >
		  <td headers="header1">  <asp:HyperLink runat="server" ID="ResourceName" ></asp:HyperLink>  </td>
		  <td headers="header2"><asp:Literal runat="server" ID="SubjectAreasTaxonomyLit" ></asp:Literal> </td>
		  <td headers="header3"> <asp:Literal runat="server" ID="ePass" ></asp:Literal> </td>

		   <td headers="header4"><asp:Literal runat="server" ID="Active" ></asp:Literal></td>

           
		</tr>--%>

              <tr>
		  <td headers="header1"><%# Eval("ResourceName") %></th>
		  <td headers="header2"><%# Eval("LastFYUniqueVisitors") %></td>
		  <td headers="header3"><%# Eval("CurrentFYUniqueVisitors") %></td>
		  <td headers="header4"><%# Eval("DiffUniqueVisitors") %></td>
		  <td headers="header5"><%# Eval("LastFYNumberofHits") %></td>
		  <td headers="header6"><%# Eval("CurrentFYNumberofHits") %></td>
          <td headers="header7"><%# Eval("DiffNumberofHits") %></td>
		</tr>

        </ItemTemplate>
    </asp:ListView>

	  <%--  <tr>
		  <td headers="header1">Unique Visitors</th>
		  <td headers="header2"><%=long.Parse(uniqueVistor_LastYearFull).ToString("N0")%></td>
		  <td headers="header3"><%=long.Parse(uniqueVistor_LastYeartoDate).ToString("N0")%></td>
		  <td headers="header4"><%=long.Parse(uniqueVistor_thisYeartoDate).ToString("N0")%></td>
		  <td headers="header5"><%=percentDetails_uniqueVisitor%></td>
		  <td headers="header6"><%=percentDetails_uniqueVisitor%></td>
              <td headers="header7"><%=percentDetails_uniqueVisitor%></td>
		</tr>--%>
		<%--<tr>
		  <td headers="header1">Number of Hits</th>
		  <td headers="header2"><%=long.Parse(NumberofHits_LastYearFull).ToString("N0")%></td>
		  <td headers="header3"><%=long.Parse(NumberofHits_LastYeartoDate).ToString("N0")%></td>
		  <td headers="header4"><%=long.Parse(NumberofHits_thisYeartoDate).ToString("N0")%></td>
		  <td headers="header5"><%=percentDetails_NumberofHits%></td>
		  
		</tr>--%>
		
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

