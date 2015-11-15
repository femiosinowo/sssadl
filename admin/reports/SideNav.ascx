<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideNav.ascx.cs" Inherits="admin_reports_SideNav" %>
<div class="column-3 menu nav-sidebar" id="nav-sidebar">
	  
	  <nav role="navigation">
	  
	  <ul>
	  <li>
	  <a title="Resources" class="sub" href="#">Resources</a>
		  <ul>
			  <li><a title="Unique Visitors and Total Hits" href="/admin/reports/resources/">Unique Visitors and Total Hits</a></li>
			  <li><a title="Clicks per Resource" href="/admin/reports/resources/clicksperresource.aspx">Clicks per Resource</a></li>
			  <li><a title="Contract Information" href="/admin/reports/resources/contracts.aspx">Contract Information</a></li>
			  <li><a title="Ad Hoc Resource Reports" href="/admin/reports/resources/adhocresource.aspx">Ad Hoc Resource Reports</a></li>	
		  </ul>
	  </li>
	  <li>
	  <a class="sub" title="Help Requests" href="#">Help Requests</a>
		  <ul>
			  <li><a title="Total Help Requests" href="/admin/reports/helprequests/">Total Help Requests</a></li>
			  <li><a title="Ad Hoc Help Reports" href="/admin/reports/helprequests/AdHocHelpReports.aspx">Ad Hoc Help Reports</a></li>
		  </ul>
	  </li>
	  
	  <li>
	  <a class="sub" title="Visitor Behavior" href="#">Visitor Behavior</a>
		  <ul>
	<%--		<li><a title="Referral Sources" target="_blank" href="https://www.google.com/analytics/web/?authuser=1#report/trafficsources-referrals/a25977386w67317448p102319644/">Referral Sources <img alt="Referral Sources" src="/admin/framework/images/download_icon.png"></a></li>--%>
                <li><a title="Referral Sources" target="_blank" href="https://www.google.com/analytics/web/?hl=en#report/visitors-overview/a25977386w67317448p102319644/">Referral Sources <img alt="Referral Sources" src="/admin/framework/images/download_icon.png"></a></li>
              
			  <li><a title="Click Paths" target="_blank" href="https://www.google.com/analytics/web/?authuser=1#report/content-engagement-flow/a25977386w67317448p102319644/">Click Paths <img alt="Click Paths" src="/admin/framework/images/download_icon.png"></a></li>
			  <li><a title="Page Analytics" target="_blank" href="https://www.google.com/analytics/web/?authuser=1#report/inpage/a25977386w67317448p102319644/">Page Analytics <img alt="Page Analytics Image" src="/admin/framework/images/download_icon.png"></a></li>	
		  </ul>
	  </li>
	  <li><a title="Report Archives" href="/admin/reports/reportsarchieve.aspx">Report Archives</a></li>
	  
	  </ul>
	  
	  </nav>
	  
	  </div>
