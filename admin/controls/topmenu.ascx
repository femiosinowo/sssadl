<%@ Control Language="C#" AutoEventWireup="true" CodeFile="topmenu.ascx.cs" Inherits="admin_controls_topmenu" %>
<nav class="nav-top-menu hide-print" id="nav-top-menu" role="navigation">

<ul>

<li><a href="/admin/default.aspx" title="Home" aria-label="Home">Home</a></li>

<li class="has-submenu"><a href="#" title="E Passwords" aria-label="EPasswords">e-Passwords</a>
<div>
	<div class="row-12">
		<div class="column-3">
			<a class="nav-header" href="#" tabindex="-1" title="Subject Areas" aria-label="Subject Areas">Access Requests</a>
			
				<ul>
					<li><a href="/admin/requests/default.aspx?show=onlyme" title="My Requests" aria-label="My Requests">My Requests</a></li>
					<li><a href="/admin/requests/default.aspx?show=all" title="All Incoming Requests" aria-label="All Incoming Requests">All Incoming Requests</a></li>    
				
				</ul>
			
		</div>
		<div class="column-9">
			<a class="nav-header" href="#" tabindex="-1" title="Help Requests" aria-label="Help Requests">e-Password Management</a>
				<ul>
					<li><a href="/admin/requests/byuser.aspx" title="by User" aria-label="by User">by User</a></li>
					<li><a href="/admin/requests/byresources.aspx" title="by Resource" aria-label="by Resource">by Resource</a></li>
				</ul>
		</div>
	</div>
</div>
</li>
<li class="has-submenu"><a href="#" title="Help Requests" aria-label="Help Requests">Help Requests</a>
<div>
	<div class="row-12">
		<div class="row-12">
			<ul class="column-12">
				<li><a href="/admin/helprequests/default.aspx?show=onlyme" title="Assigned to Me" aria-label="Assigned to Me">Assigned to Me</a></li>
				<li><a href="/admin/helprequests/default.aspx?show=all" title="All Help Requests" aria-label="All Help Requests">All Help Requests</a></li>       
			</ul>
		</div>
    </div>
</div>
</li>
<li class="has-submenu"><a href="#" title="Reports" aria-label="Reports">Reports</a>
<div>
	<div class="row-12">
		<div class="column-3">
			<a class="nav-header" href="#" tabindex="-1" title="Subject Areas" aria-label="Subject Areas">Resources</a>
			
				<ul>
					<li><a href="/admin/reports/resources/default.aspx#sb=0" title="Unique Visitors and Total Hits" aria-label="Unique Visitors and Total Hits">Unique Visitors and Total Hits</a></li>
					<li><a href="/admin/reports/resources/clicksperresource.aspx#sb=0" title="Clicks per Resource" aria-label="Clicks per Resource">Clicks per Resource</a></li>  
					<li><a href="/admin/reports/resources/contracts.aspx#sb=0" title="Contract Information" aria-label="Contract Information">Contract Information</a></li>  
					<li><a href="/admin/reports/resources/adhocresource.aspx#sb=0" title="Ad Hoc Resource Reports" aria-label="Ad Hoc Resource Reports">Ad Hoc Resource Reports</a></li>    
				
				</ul>
			
		</div>
		<div class="column-3">
			<a class="nav-header" href="#" tabindex="-1" title="Help Requests" aria-label="Help Requests">Help Requests</a>
				<ul>
					<li><a href="/admin/reports/helprequests/default.aspx#sb=1" title="Total Help Requests" aria-label="Total Help Requests">Total Help Requests</a></li>
					<li><a href="/admin/reports/helprequests/AdHocHelpReports.aspx#sb=1" title="Ad Hoc Help Reports" aria-label="Ad Hoc Help Reports">Ad Hoc Help Reports</a></li>
				</ul>
		</div>
		<div class="column-6">
			<a class="nav-header" href="#" tabindex="-1" title="Subject Areas" aria-label="Subject Areas">Visitor Behavior</a>
			<div class="row-12">
				<ul class="column-6">
					<li><a href="https://www.google.com/analytics/web/?hl=en#report/visitors-overview/a25977386w67317448p102319644/" target="_blank" title="Referral Sources" aria-label="Referral Sources">Referral Sources</a></li>
					<li><a href="https://www.google.com/analytics/web/?authuser=1#report/content-engagement-flow/a25977386w67317448p102319644/ " target="_blank" title="Click Paths" aria-label="Click Paths">Click Paths</a></li>
					<li><a href="https://www.google.com/analytics/web/?authuser=1#report/inpage/a25977386w67317448p102319644/ " target="_blank" title="Page Analytics" aria-label="Page Analytics">Page Analytics</a></li>        
				</ul>
				<ul class="column-6">
					<li><a href="/admin/reports/reportsarchieve.aspx" title="Report Archives" aria-label="Report Archives">Report Archives</a></li> 
				</ul>
			</div>
		</div>
	</div>
</div>
</li>
<li><a href="/admin/resources/default.aspx" title="Resources" aria-label="Resources">Resources</a></li>

<li><a href="/admin/users/default.aspx" title="Users" aria-label="Users">Users</a></li>

 

 
 

 <li class="has-submenu"><a href="#" title="Settings" aria-label="Settings">Settings</a>
<div>
	<div class="row-12">
		<div class="row-12">
			<ul class="column-12">
				<li><a href="/admin/autoreply/" title="Auto-Replies" aria-label="Auto-Replies">Auto-Replies</a></li>
				<li><a href="/admin/settings/systemsettings.aspx" title="System Settings" aria-label="System Settings">System Settings</a></li>   
                <li><a href="/admin/settings/sitemaintenance.aspx" title="System Settings" aria-label="Site Maintenance">Site Maintenance</a></li>   
                    <li><a href="/admin/helprequests/assignees.aspx" title="System Settings" aria-label="Help Requests Assigned To">Help Requests Assigned To</a></li>     
			</ul>
		</div>
    </div>
</div>
</li>



</ul>
</nav>