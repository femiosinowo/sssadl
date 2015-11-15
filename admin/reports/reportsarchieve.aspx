<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="reportsarchieve.aspx.cs" Inherits="admin_users_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
<style>

.div1, .div2 {
    display: inline-block;
    width: 10%;
}

select {
    max-width: 100%;
    width: 100%;
}

</style>

<script>

    function goto(id, ReportType) {
        switch (ReportType) {
            case "Ad Hoc Contract Report":
                location.href = '/admin/reports/resources/contracts.aspx?reportId=' + id + '#sb=0';
                break;
            case "Ad Hoc Resource Report":
                location.href = '/admin/reports/resources/adhocresource.aspx?reportId=' + id + '#sb=0';
                break;
            case "Ad Hoc Help Report":
                location.href = '/admin/reports/helprequests/AdHocHelpReports.aspx?reportid=' + id + '#sb=1';
                break;
    
            case "Unique Visitors and Total Hits Reports":
                location.href = '/admin/reports/resources/#sb=0';
                break;
            case "Total Help Requests":
                location.href = '/admin/reports/helprequests/#sb=1';
                break;
            case "Clicks per Resource":
                location.href = '/admin/reports/resources/clicksperresource.aspx#sb=0';
                break;

                
        }


        
    }
 
 
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server"><div id="title-bar"><h2>Ad Hoc Resource Archieve</h2></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


<asp:HiddenField ID="searchStringHF" runat="server" />
<asp:HiddenField ID="sortOrderbyHF" Value=" order by lastname asc" runat="server" />
<asp:HiddenField ID="ShowInactive" Value="False" runat="server" />
<asp:HiddenField ID="currentSQL"  runat="server" />

 <!-- PAGE TITLE -->


<div id="content" role="main">
<div class="grid" id="admin-tools-int-content">
	    <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>Reports</li> <li>Ad Hoc Resource Archieve </li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  
	  <ux:SideNav runat="server" />
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
  <div class="buttons" >
	  <%--	<a href="#" class="btn">Add User</a> --%>
        <asp:LinkButton runat="server" PostBackUrl="/admin/resources/add.aspx" CssClass="btn" ID="AddResource" Text="Create a report" 
           ></asp:LinkButton>

          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
        Search : <asp:TextBox runat="server" Width="250" ID="searchWordTxt" ></asp:TextBox>
       
        <asp:LinkButton runat="server" Text="Search Now" CssClass="btn" 
            ID="updateList" onclick="updateList_Click" 
              ></asp:LinkButton>


 	
	  </div>
      <span class="results_content" runat="server" id="pagertop" >
 

Results <asp:Label runat="server" ID="idResultsLabel" ></asp:Label> &nbsp;&nbsp;&nbsp; |   &nbsp;&nbsp;
<asp:LinkButton runat="server"  ID="ExcelClick" 
              Text="Download Report to Excel" onclick="ExcelClick_Click"   ></asp:LinkButton>
              
              	  <span class="float-right">Show:   <asp:DropDownList runat="server" AutoPostBack="true" ID="show_results" OnSelectedIndexChanged="Index_Changed" >
            <asp:ListItem Value="10">10</asp:ListItem>
             <asp:ListItem Value="25">25</asp:ListItem>
              <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="100">100</asp:ListItem>    
                 <asp:ListItem Value="250">250</asp:ListItem>                    
            </asp:DropDownList></span> 
 </span>

	   <div>
         
        <div class="column-5">
         Type<br />
     <asp:ListBox runat="server" ID="ReportTypeListBox"  DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more Report Type(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_ReportTypeListBox')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_ReportTypeListBox')">Select None</a>
       </div>
       <div class="column-5">
       <p>
       Run by <br />
        <asp:ListBox runat="server" ID="RunByListBox"  DataTextField="DisplayName" DataValueField="RunbyPIN" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more Subject Area(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_RunByListBox')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_RunByListBox')">Select None</a>
       </p>
       </div>
       
       <div class="column-2">
       <p></p><asp:LinkButton CssClass="btn"  runat="server" ID="UpdateListBtnForSubJEPass" 
               Text="Apply Filter" onclick="UpdateListBtnForSubJEPass_Click" ></asp:LinkButton></div>
       </div>
       

	  <table class="table table-bordered table-striped">
	   
	  
 
 
		

        <asp:ListView ID="reportsLV" OnSorting="reportsLV_OnSorting" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="reportsLV_ItemDatabound">
         
        <EmptyDataTemplate>
		<caption><h3>No Data found.</h3></caption>
            
        </EmptyDataTemplate>
        
        <LayoutTemplate>
        <thead>
	    <tr>
			<th id="header1" scope="col">
            
                   <asp:LinkButton runat="server" ID="ReportTypeBtn" 
         Text="Report Type" CommandName="Sort" CommandArgument="ReportType" />

            <asp:ImageButton ID="ReportTypeImg" CommandArgument="ReportType" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
               </th>
			<th id="header2" scope="col">
              
                   <asp:LinkButton runat="server" ID="DisplayNameBtn" 
         Text="Run By" CommandName="Sort" CommandArgument="DisplayName" />

            <asp:ImageButton ID="DisplayNameImg" CommandArgument="DisplayName" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            </th>
			<th id="header3" scope="col">
                        <asp:LinkButton runat="server" ID="RunDateTimeBtn" 
         Text="Run on Date/Time" CommandName="Sort" CommandArgument="DateTimRun" />

            <asp:ImageButton ID="RunDateTimeImg" CommandArgument="DateTimRun" CommandName="Sort" ImageUrl="~/admin/img/desc.png" runat="server" />
            
            </th>
		 
		 
		</tr>
	  </thead>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
           <tr <asp:Literal runat="server" ID="rowClick" ></asp:Literal> >
		  <td headers="header1"><%#Eval("ReportType")%></th>
		  <td headers="header2"><%#Eval("DisplayName")%></td>
		  <td headers="header3"> <asp:Literal runat="server" ID="DateTimRun" ></asp:Literal> </td>

		  

           
		</tr>
        </ItemTemplate>
    </asp:ListView>

     


	  
	  </table>



        
    <div class="results_pagination" id="pagerDiv" runat="server">

        <asp:DataPager ID="DataPager1" PageSize="10" runat="server" PagedControlID="reportsLV"
            OnPreRender="Datapager_prender" >
            <Fields>
                <asp:TemplatePagerField>
                    <PagerTemplate>
                        <div class="float-left">
                            <span class="pager_buttons" id="pager_prev">
                    </PagerTemplate>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField PreviousPageText="Prev" ButtonType="Link" ShowLastPageButton="false"
                    ShowNextPageButton="false" ButtonCssClass="btn" />
                <asp:TemplatePagerField>
                    <PagerTemplate>
                        </span> <span id="pager_next" class="pager_buttons">
                    </PagerTemplate>
                </asp:TemplatePagerField>
                <asp:NextPreviousPagerField NextPageText="Next" ButtonType="Link" ShowFirstPageButton="false"
                    ShowNextPageButton="true" ButtonCssClass="btn" ShowPreviousPageButton="false" />
                <asp:TemplatePagerField>
                    <PagerTemplate>
                        </span> </div>
                       


 


                    </PagerTemplate>
                </asp:TemplatePagerField>
              
            </Fields>
        </asp:DataPager>
 

                          <div class="float-right" runat="server" id="pagerbelow" visible="false">
                            <p>
                                <label for="pager_textbox" class="inline-label">
                                    Go to page</label>
                                <asp:TextBox runat="server" placeholder="1" Visible="false" ID="pager_textbox" Width="40"  ></asp:TextBox>
                                  <asp:DropDownList runat="server" ID="pager_dropdown" AutoPostBack="true" Width="50" OnSelectedIndexChanged="pager_dropdown_SelectedIndexChanged" ></asp:DropDownList>
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:LinkButton runat="server" ID="goBtn" onclick="goBtnClick" Visible="false" CssClass="btn" Text="Go" ></asp:LinkButton>
                            </p>
                        </div>
    </div>


      </div></div></div>
      </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

