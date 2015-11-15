<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/helprequests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
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

    function goto(id, formid) {
        location.href = '/admin/helprequests/editRequest.aspx?reqid=' + id + '&formId=' + formid;
    }





</script>
 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
    <div id="title-bar">
    <h2>
      <span class="favorite-id">
           <%=pageTitle%></span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


<asp:HiddenField ID="searchStringHF" runat="server" />
<asp:HiddenField ID="sortOrderbyHF" Value=" order by lastname asc" runat="server" />
<asp:HiddenField ID="ShowInactive" Value="False" runat="server" />
<asp:HiddenField ID="currentSQL"  runat="server" />
<div class="grid" id="admin-tools-int-content">
	     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>   <%=pageTitle%> </li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	 
	  
	  <!-- END COLUMN -->
	  
	  <div class="">
  <div class="buttons" >
	  <%--	<a href="#" class="btn">Add User</a> --%>
        <asp:LinkButton runat="server" PostBackUrl="/admin/helprequests/newRequest.aspx" 
          CssClass="btn" ID="AddRequest" Text="New Request"   
           ></asp:LinkButton>

          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
        Search : <asp:TextBox runat="server" Width="250" ID="searchWordTxt" ></asp:TextBox>
       
        <asp:LinkButton runat="server" Text="Update List" CssClass="btn" 
            ID="updateList"    
              ></asp:LinkButton> 


 	
	  </div>
          <HR />
	   <div>
              <div class="column-4">
        Type<br />
     <asp:ListBox runat="server" ID="RequestType"  DataTextField="FormName" DataValueField="ID" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more requests"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_RequestType')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_RequestType')">Select None</a>
       </div>
        <div class="column-4">
        Assigned To<br />
     <asp:ListBox runat="server" ID="AdminUsers"  DataTextField="Name" DataValueField="ID" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more Admin User(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_AdminUsers')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_AdminUsers')">Select None</a>
       </div>
       
       
       <div class="column-4">
       <p></p><asp:Button  runat="server" ID="UpdateListBtnForUsers" 
               Text="Apply Filters" CssClass="btn" onclick="UpdateListBtnForUsers_Click" 
                /></div>
       </div>
      <asp:LinkButton runat="server" CssClass="float-right" ID="ShowInactiveResources" 
              Text="Show Closed Requests" onclick="ShowInactiveResources_Click" ></asp:LinkButton>

                                    <span class="results_content" runat="server" id="pagertop" visible="false">
 

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

	  <table class="table table-bordered table-striped">
	  <caption> 
	
	  
	  </caption>
 
 
		

        <asp:ListView ID="passwordAssistanceLV" OnSorting="passwordAssistanceLV_OnSorting" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="passwordAssistanceLV_ItemDatabound">
        
        <EmptyDataTemplate>  
      
	    <tr><td>
            
                No Requests found.
             </td> </tr>
             
        </EmptyDataTemplate>
        
        <LayoutTemplate>
        <thead>
	    <tr>
			<th id="header1" scope="col">
            <asp:LinkButton runat="server" ID="DateBtn" 
         Text="Date" CommandName="Sort" CommandArgument="SubmissionDateandTime" />

            <asp:ImageButton ID="DateImg" CommandArgument="SubmissionDateandTime"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/desc.png" runat="server" />

               </th>
			<th id="header2" scope="col">
             

              <asp:LinkButton runat="server" ID="RequestorBtn" 
         Text="Requestor" CommandName="Sort" CommandArgument="SubmittedForFirstName" />

            <asp:ImageButton ID="RequestorImg" CommandArgument="SubmittedForFirstName"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
             
            </th>
            <th id="Th1" scope="col">
             <asp:LinkButton runat="server" ID="RequestTypeBtn" 
         Text="Type" CommandName="Sort" CommandArgument="RequestType" />

            <asp:ImageButton ID="RequestTypeImg" CommandArgument="RequestType"   AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

            </th>
			<th id="header3" scope="col">Assigned to</th>
			<th id="header4" scope="col">
             <asp:LinkButton runat="server" ID="FormStatusBtn" 
         Text="Status" CommandName="Sort" CommandArgument="FormStatus" />

            <asp:ImageButton ID="FormStatusImg" CommandArgument="FormStatus"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            
            </th>
			 
			<%--<th id="Th3" scope="col">View/Edit</th>--%>
		</tr>
	  </thead>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
           <tr <asp:Literal runat="server" ID="rowClick" ></asp:Literal> >
		  <td headers="header1">  <asp:HyperLink runat="server" ID="SubmissionDateandTime" ></asp:HyperLink>   </td>
		  <td headers="header2"><asp:Literal runat="server" ID="SubmittedByPIN" ></asp:Literal> </td>
           <td headers="header3"> <asp:Literal runat="server" ID="RequestType" ></asp:Literal> </td>
		  <td headers="header3"> <asp:Literal runat="server" ID="AssignedTo" ></asp:Literal> </td>

		   <td headers="header4"><asp:Literal runat="server" ID="Active" ></asp:Literal></td>

          <%-- <td headers="header5"> <asp:HyperLink runat="server" ID="ViewEdit" Text="View/Edit" ></asp:HyperLink>  </td>--%>
		</tr>
        </ItemTemplate>
    </asp:ListView>

     


	  
	  </table>



        
    <div class="results_pagination">

        <asp:DataPager ID="DataPager1" PageSize="10" runat="server" PagedControlID="passwordAssistanceLV"
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
                                <asp:TextBox runat="server" onkeypress="return isNumberKey(event)" ID="pager_textbox" Width="40" Visible="false"  ></asp:TextBox>
                                 <asp:DropDownList runat="server" ID="pager_dropdown" AutoPostBack="true" Width="50" OnSelectedIndexChanged="pager_dropdown_SelectedIndexChanged" ></asp:DropDownList>
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:Button runat="server" ID="goBtn" onclick="goBtnClick" CssClass="btn" Text="Go"   Visible="false"/>
                            </p>
                        </div>
    </div>


      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

