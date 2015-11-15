<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
<script>
    function gotoThisURL() {

        location.href = document.getElementById("ReportDD").value;
    }

</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentCP" Runat="Server">

<!-- GRID -->
<div class="grid" id="admin-tools-home-content">


<div class="row-12">

<div class="column-3">

   <h3>e-Passwords</h3>
   <p><a href="/admin/requests/default.aspx?show=onlyme">My e-Password Requests</a></p>
   <ul>
     <li><%=MyNewEpassCount %> new requests</li>
	 <li><%=MyOutEpassCount %> outstanding requests</li>
   </ul>
   
   <p><a href="/admin/requests/default.aspx?show=all">All Incoming Requests</a></p>
   <ul>
     <li><%=AllNewEpassCount %> new requests</li>
	 <li><%=AllOutEpassCount %> outstanding requests</li>
   </ul>

</div>
<!-- END COLUMN -->

<div class="column-3">

   <h3>Help Requests</h3>
   <p><a href="/admin/helprequests/default.aspx?show=onlyme">My Help Requests</a></p>
   <ul>
        <li><%=myNewRequestsCount %> new requests</li>
	 <li><%=myOutStandingRequestsCount %> outstanding requests</li>
   </ul>
   
   <p><a href="/admin/helprequests/default.aspx?show=all">All Incoming Requests</a></p>
   <ul>

          <li><%=allNewRequestsCount %> new requests</li>
	 <li><%=allOutStandingRequestsCount %> outstanding requests</li>
   </ul>

</div>
<!-- END COLUMN -->

<div class="column-3">

   <h3>Reports</h3>
   <p>My Recent Reports<br/>
  
 <%=reportMessage%></p>
	<p><a href="/admin/reports/reportsarchieve.aspx">More in the Report Archive ></a></p>
	
	<div id="create_report_block">
		<p>Create a report:</p>
		<select id="ReportDD">
		  <option value="/admin/reports/resources/#sb=0">Unique Visitors and Total Hits</option>
		  <option value="/admin/reports/resources/clicksperresource.aspx#sb=0">Clicks per Resource</option>
		  <option value="/admin/reports/resources/adhocresource.aspx">Ad Hoc</option>
		</select>
		<br/>
		<input type="button" value="Go" onclick="gotoThisURL()" class="btn">
	</div>
   
   

</div>
<!-- END COLUMN -->

<div class="column-3">

   <h3>Resources</h3>	
  <a href="/admin/resources/default.aspx"  class="btn">Manage Resources</a>
   <a href="/admin/resources/add.aspx" class="btn">Add a New Resource</a>
   <asp:Panel runat="server" ID="ContractPanel" Visible="false" >
   <%-- <p>My Recent Reports</p>--%>
   <div class="container-yellow">
     <p>You have <%=contractExpiringCount.ToString() %> contracts expiring soon:</p>
	 <ul>
	  <%=contractsExpiring %>
	 </ul>
   </div>
   </asp:Panel>
   

</div>
<!-- END COLUMN -->

</div>
<!-- END ROW -->




</div>
<!-- END GRID -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

