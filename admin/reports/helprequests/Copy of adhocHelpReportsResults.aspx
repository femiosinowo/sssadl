<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Copy of adhocHelpReportsResults.aspx.cs" Inherits="admin_reports_Default" %>
<%@ Register Src="~/admin/reports/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">
 

 <!-- PAGE TITLE -->
<div id="title-bar"><h2>Ad Hoc Help Reports </h2></div>

	  
	  <!-- CONTENT -->
	  <div id="content" role="main">
	  
	  <!-- GRID -->
	  <div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  
	  
<ux:SideNav runat="server" />
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3>Ad Hoc Help Reports </h3>
	  
	  <ul>
	    <li><%=RunMEssage %></li>
		<li>Standard parameters:<br>
		   <%=ReportSummary %>
           <asp:HyperLink runat="server" Text="Change the report parameters" ID="ChangeParameters" ></asp:HyperLink>
           </li>
	  </ul>
	  
	   <p><strong>Scheduling:</strong> <%=SchedulingMessage%>.<br/>
	  <a href="/admin/reports/resources/scheduledreport.aspx?reportid=<%=reportID %>"><%=SchedulingMessageTag%> the schedule and report recipients.</a></p>
	  
	  <div class="buttons">
	  	<a href="/admin/reports/resources/emailReport.aspx?reportid=<%=reportID %>" class="btn">Email This Report</a>
       
          <asp:HyperLink runat="server"  CssClass="btn"  ID="RunSchudule" Visible="false" 
              Text="Run on a Schedule" onclick="ExcelClick_Click"   ></asp:HyperLink>

	  	<asp:LinkButton runat="server"  CssClass="float-right"  ID="ExcelClick" 
              Text="Download Report to Excel" onclick="ExcelClick_Click"   ></asp:LinkButton>
	  </div>
	  





      <table class="table table-bordered table-striped">
	  <caption><h3></h3></caption>
        <asp:ListView ID="passwordAssistanceLV"   runat="server" ItemPlaceholderID="phLV" OnItemDataBound="passwordAssistanceLV_ItemDatabound">
         
        <EmptyDataTemplate>
        <hr />
            <h4>
                No Requests found.
            </h4>
        </EmptyDataTemplate>
        
        <LayoutTemplate>
        <thead>
	    <tr>
			<th id="header1" scope="col">
            <asp:LinkButton runat="server" ID="DateBtn" 
         Text="Date" CommandName="Sort" CommandArgument="SubmissionDateandTime" />

            <asp:ImageButton ID="DateImg" CommandArgument="SubmissionDateandTime" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

               </th>
			<th id="header2" scope="col">
             Requestor



            </th>
            <th id="Th1" scope="col">
             <asp:LinkButton runat="server" ID="RequestTypeBtn" 
         Text="Type" CommandName="Sort" CommandArgument="RequestType" />

            <asp:ImageButton ID="RequestTypeImg" CommandArgument="RequestType" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

            </th>
			 
			<th id="header4" scope="col">
             <asp:LinkButton runat="server" ID="FormStatusBtn" 
         Text="Status" CommandName="Sort" CommandArgument="FormStatus" />

            <asp:ImageButton ID="FormStatusImg" CommandArgument="FormStatus" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            
            </th>
			 
			<%--<th id="Th3" scope="col">View/Edit</th>--%>
		</tr>
	  </thead>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
           <tr <asp:Literal runat="server" ID="rowClick" ></asp:Literal> >
		  <td headers="header1">   <asp:Literal runat="server" ID="SubmissionDateandTime" ></asp:Literal>   </th>
		  <td headers="header2"><asp:Literal runat="server" ID="SubmittedByPIN" ></asp:Literal> </td>
           <td headers="header3"> <asp:Literal runat="server" ID="RequestType" ></asp:Literal> </td>
		   
		   <td headers="header4"><asp:Literal runat="server" ID="Active" ></asp:Literal></td>

          <%-- <td headers="header5"> <asp:HyperLink runat="server" ID="ViewEdit" Text="View/Edit" ></asp:HyperLink>  </td>--%>
		</tr>
        </ItemTemplate>
    </asp:ListView>
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

