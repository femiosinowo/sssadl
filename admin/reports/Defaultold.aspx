<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Defaultold.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->
<div id="title-bar"><h2>Unique Visitors and Total Hits</h2></div>

	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3>Unique Visitors and Total Hits Report</h3>
	  
	  <ul>
	    <li>Run m/d/yy at h:mm PM by [First name] [Last name]</li>
		<li>Standard parameters:<br>
		    Lorem ipsum dolor sit amet [summary here].</li>
	  </ul>
	  
	  <p><strong>Scheduling:</strong> This report is schedult to run on the first of each month and goes to 3 recipients.<br/>
	  <a href="#">Update the schedule and report recipients.</a></p>
	  
	  <div class="buttons">
	  	<a href="#" class="btn">Email This Report</a>
	  	<a href="#" class="float-right">Download Report to Excel</a>
	  </div>
	  
	  <table class="table table-bordered table-striped">
	  <caption><h3></h3></caption>
	  
	  <thead>
	    <tr>
			<th id="header1" scope="col">Statistic</th>
			<th id="header2" scope="col">FY13(Full)</th>
			<th id="header3" scope="col">FY13(to Date)</th>
			<th id="header4" scope="col">FY14(to Date)</th>
			<th id="header5" scope="col">% Difference to Date</th>
			
		</tr>
	  </thead>
	  
	  <tbody>
	    <tr>
		  <td headers="header1">Unique Visitors</th>
		  <td headers="header2">41,351</td>
		  <td headers="header3">37,721</td>
		  <td headers="header4">40,496</td>
		  <td headers="header5">7.4% increase</td>
		  
		</tr>
		<tr>
		  <td headers="header1">Number of Hits</th>
		  <td headers="header2">2,373,464</td>
		  <td headers="header3">1,873,448</td>
		  <td headers="header4">2,027,268</td>
		  <td headers="header5">8.2% increase</td>
		  
		</tr>
		
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

