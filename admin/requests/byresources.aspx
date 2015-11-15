<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="byresources.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
<%@ Register Src="~/admin/requests/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
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

    function goto(id) {
        location.href = '/admin/requests/userswithaccess.aspx?rid=' + id;
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
  <div id="title-bar">
    <h2>
      <span class="favorite-id">
            e-Passwords Requests By Resources</span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


<asp:HiddenField ID="searchStringHF" runat="server" />
<asp:HiddenField ID="sortOrderbyHF" Value=" order by lastname asc" runat="server" />
<asp:HiddenField ID="ShowInactive" Value="False" runat="server" />
<asp:HiddenField ID="currentViewSQL"   runat="server" />
<div class="grid" id="admin-tools-int-content">
	    <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li>
        <li>
         <a href="/admin/requests/">E-Password Requests</a></li>
          <li>e-Passwords Requests By Resources </li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
      <asp:Panel ID="searchField" runat="server" >
      <div>
      <h3>Resources with e-Passwords</h3>
   
 
      <label for="ctl00_mainContentCP_searchTxt">Search: </label>  &nbsp;&nbsp;&nbsp;
     
      
       <asp:TextBox ID="searchTxt" runat="server" ></asp:TextBox>  <asp:Button CssClass="btn" Text="Search Now" runat="server" ToolTip="Search Now" 
          ID="SearchBtn" onclick="SearchBtn_Click"
           />
 
          </asp:Panel>

      <asp:Panel runat="server" ID="PanelResult"   >
  <div class="buttons" >
	 
       
         


 	
	  </div>

	    
    
	  <table class="table table-bordered table-striped">
	   
	  
	  
  <caption><h3>Results</h3>
  
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
  
  </caption>
 
		

        <asp:ListView ID="passwordAssistanceLV" OnSorting="passwordAssistanceLV_OnSorting" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="passwordAssistanceLV_ItemDatabound">
         
        <EmptyDataTemplate>
        <hr />
            <h4>
                No Resource found.
            </h4>
        </EmptyDataTemplate>
        
        <LayoutTemplate>
        <thead>
	    <tr >
			<th id="header1" scope="col">
            <asp:LinkButton runat="server" ID="ResourceNameBtn" 
         Text="Resource Name" CommandName="Sort" CommandArgument="ResourceName" />

            <asp:ImageButton ID="ResourceNameIMg" CommandArgument="ResourceName" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

               </th>
			<th id="header2" scope="col">
                
                 <asp:LinkButton runat="server" ID="TotalLicensesBtn" 
         Text="Total Licenses" CommandName="Sort" CommandArgument="TotalLicenses" />

            <asp:ImageButton ID="TotalLicensesImg" CommandArgument="TotalLicenses" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            </th>
			<th id="header3" scope="col">
             
               <asp:LinkButton runat="server" ID="AssignedBtn" 
         Text="Assigned Licenses" CommandName="Sort" CommandArgument="AssignedLicenses" />

            <asp:ImageButton ID="AssignedImg" CommandArgument="AssignedLicenses" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            </th>
			<th id="header4" scope="col">
             

              <asp:LinkButton runat="server" ID="AvailableBtn" 
         Text="Available Licenses" CommandName="Sort" CommandArgument="AvailableLicenses" />

            <asp:ImageButton ID="AvailableImg" CommandArgument="AvailableLicenses" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

            </th>
			 
			 
		</tr>
	  </thead>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
           <tr  <asp:Literal runat="server" ID="rowClick" ></asp:Literal>  >
		  <td headers="header1">    <asp:HyperLink runat="server" ID="ResourceName" ></asp:HyperLink>  </td>
		  <td headers="header2">   <asp:Literal runat="server" ID="TotalLicenses" ></asp:Literal>  </td>
		  <td headers="header3"><%#Eval("AssignedLicenses")%>  <asp:Literal runat="server" ID="AssignedLicenses" ></asp:Literal>  </td>

		   <td headers="header4">  <asp:Literal runat="server" ID="AvailableLicenses" ></asp:Literal>   </td>

            
		</tr>
        </ItemTemplate>
    </asp:ListView>

     <a name="MOVEHERE"></a>


	  
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
                                <asp:TextBox runat="server" ID="pager_textbox" Width="40" Visible="false"  ></asp:TextBox>
                                  <asp:DropDownList runat="server" ID="pager_dropdown" AutoPostBack="true" Width="50" OnSelectedIndexChanged="pager_dropdown_SelectedIndexChanged" ></asp:DropDownList>
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:Button runat="server" ID="goBtn" onclick="goBtnClick" CssClass="btn" Text="Go"  Visible="false" />
                            </p>
                        </div>
    </div>

    </asp:Panel>
      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

