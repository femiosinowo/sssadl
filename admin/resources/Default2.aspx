<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="admin_resources_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <script src="/admin/framework/js/jquery-1.8.3.js" type="text/javascript"></script>

 <style>
 
label  {  padding-left: 5px; display: inline !important; }


h3 {
  border-bottom: 4px solid #cccccc;
  padding-bottom: 5px;
  padding-top: 5px;
}
input, select {
    height: auto;
}

.date1, .date2 {
    display: inline-block;
    width: 44%;
}

input[type="file"] {
   
    border: none;
    
}
label.error 
{
  display: table-cell !important; 
    padding-left: 5px;
    font-style: italic;
    font-size: small;
    color: Red;   
}
input.error , select.error , textarea.error
{
   border: 1px solid Red !important;
}

.required {
    color: #333 !important;
}


.block {
		display: block;
	}
	  
 
 
 .ui-tabs.ui-tabs-vertical {
    padding: 0;
    width: 42em;
}
.ui-tabs.ui-tabs-vertical .ui-widget-header {
    border: none;
}
.ui-tabs.ui-tabs-vertical .ui-tabs-nav {
    float: left;
    width: 5em;
    background: #CCC;
    border-radius: 4px 0 0 4px;
    border-right: 1px solid gray;
}
.ui-tabs.ui-tabs-vertical .ui-tabs-nav li {
    clear: left;
    width: 100%;
    margin: 0.2em 0;
    border: 1px solid gray;
    border-width: 1px 0 1px 1px;
    border-radius: 4px 0 0 4px;
    overflow: hidden;
    position: relative;
    right: -2px;
    z-index: 2;
}
.ui-tabs.ui-tabs-vertical .ui-tabs-nav li a {
    display: block;
    width: 100%;
    padding: 0.6em 1em;
}
.ui-tabs.ui-tabs-vertical .ui-tabs-nav li a:hover {
    cursor: pointer;
}
.ui-tabs.ui-tabs-vertical .ui-tabs-nav li.ui-tabs-active {
    margin-bottom: 0.2em;
    padding-bottom: 0;
    border-right: 1px solid white;
}
.ui-tabs.ui-tabs-vertical .ui-tabs-nav li:last-child {
    margin-bottom: 10px;
}
.ui-tabs.ui-tabs-vertical .ui-tabs-panel {
    float: left;
    width: 28em;
    border-left: 1px solid gray;
    border-radius: 0;
    position: relative;
    left: -1px;
}




</style>
   <script src="/admin/framework/js/tdi.tabs.js"></script>
     <link rel="stylesheet" href="/admin/framework/css/tabs.css">
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
	            url: 'ajaxcontent.html',
	            method: 'get',
	            dtype: 'html',
	            params: {},
	            tabTitle: 'New Ajax Tab'
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
 
 
</head>
<body>
    <form id="form1" runat="server">
 <h3>Example 6 - Add Dynamic New Tabs Using hidden div</h3>
 <hr></hr>
 <input type="button" name="addNewDivTab1" id="addNewDivTab1" value="Add New Div Tab" onclick="javascript:addNewDivTab();" />
 <br></br>
 <div id="adddivtabdiv">
 	<!-- Tab Headers Start -->
 	<ul class="tabs" >
 		<li><a href="#tabview11">Tab 1</a></li>
 		<li><a href="#tabview17">Tab 2</a></li>
 		<li><a href="#tabview18">Tab 3</a></li>
         
        <li><a  onclick="javascript:addNewDivTab();">Add NEw Tab</a></li>
 	</ul> 
     	 
 	<!-- Tab Headers End -->
 	<!-- Tab Body Start -->
 	 	<div class="tabcontents" id="adddivtabbody">
 		<div id="tabview11">
 			Lorem ipsum contents......
 		</div>
 		<div id="tabview17">
 			Some more static content...
 		</div>
 		<div id="tabview18">
 			A bit more...
 		</div>
 	</div>
 </div>
 <div id="hdnDataTabDiv" style="display:none;"> This is loaded from a hidden div in the html body. Div ID : hdnDataTabDiv </div> 
 <hr></hr>
 
 
  
 </div>
    </form>
</body>
</html>
