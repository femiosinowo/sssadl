<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="admin_resources_Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
	<script type="text/javascript">

	    $(document).ready(function () {

	        //simple tab creation
	        $("#simpletab1").dynatabs({
	            tabBodyID: "simpletabbody1"
	        });
	        //closable tabs
	        $("#closabletab1").dynatabs({
	            tabBodyID: "closabletabbody1",
	            showCloseBtn: true
	        });

	        //tabs with custom close confirmation message call backs
	        $("#closeconfirmtabs").dynatabs({
	            tabBodyID: "closeconfirmtabsbody",
	            showCloseBtn: true,
	            confirmDelete: true,
	            confirmMessage: 'Do you want to delete this tab?'
	        });

	        //add ajax tabs
	        $("#addajaxtab").dynatabs({
	            tabBodyID: "addajaxtabbody", 
	            showCloseBtn: true,
	            confirmDelete: true
	        });

	        //add static tabs
	        $("#addstatictab").dynatabs({
	            tabBodyID: 'addstatictabbody',
	            showCloseBtn: true,
	            confirmDelete: true
	        });

	        //add hidden tabs example
	        $("#adddivtab").dynatabs({
	            tabBodyID: 'adddivtabbody',
	            showCloseBtn: true,
	            confirmDelete: true
	        });

	        //nested tab 1
	        $("#nestedtab1").dynatabs({
	            tabBodyID: 'nestedtabbody1',
	            showCloseBtn: true,
	            confirmDelete: true
	        });
	        //nested tab 2
	        $("#nestedtab2").dynatabs({
	            tabBodyID: 'nestedtabbody2',
	            showCloseBtn: true,
	            confirmDelete: true
	        });
	    });

	    function addNewTab() {
	        $.addDynaTab({
	            tabID: 'addajaxtab',
	            type: 'ajax',
	            url: '/admin/resources/ajaxgetcontracts.aspx',
	            method: 'get',
	            dtype: 'html',
	            params: {},
	            tabTitle: 'New FY'
	        });
	    }

	    function addNewStaticTab() {
	        $.addDynaTab({
	            tabID: 'addstatictab',
	            type: 'html',
	            html: '<p>This HTML content is loaded statically</p>',
	            params: {},
	            tabTitle: 'New Static Tab'
	        });
	    }

	    function addNewDivTab() {
	        $.addDynaTab({
	            tabID: 'adddivtab',
	            type: 'div',
	            divID: 'hdnDataTabDiv',
	            params: {},
	            tabTitle: 'New Div Tab'
	        });
	    }
	</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">

 <h3>&nbsp;</h3>
 <h3>Example 5 - Add Dynamic New Tabs Using Static HTML</h3>
 <hr></hr>
 <input type="button" name="saddNewStaticTab" id="saddNewStaticTab" value="Add New Static Tab" onClick="javascript:addNewTab();" />
 <br></br>
 <div id="vertical-tabs">
 	<!-- Tab Headers Start -->
 	<ul   id="addajaxtab">
 		<li><a href="#tabview13">Tab 1</a></li>
 		<li><a href="#tabview14">Tab 2</a></li>
 		<li><a href="#tabview15">Tab 3</a></li>
 	</ul>
 	<!-- Tab Headers End -->
 	<!-- Tab Body Start -->
 	<div   id="addajaxtabbody">
 		<div id="tabview13">
 			Lorem ipsum contents......
 		</div>
 		<div id="tabview14">
 			Some more static content...
 		</div>
 		<div id="tabview15">
 			A bit more...
 		</div>
 	</div>
 	<!-- Tab Body End --> 	
 </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

