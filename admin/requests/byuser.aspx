<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="byuser.aspx.cs" Inherits="admin_users_Default" ValidateRequest="false" %>
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
        location.href = '/admin/requests/usersProfileAccess.aspx?PIN=' + id;
    }

</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
  <div id="title-bar">
    <h2>
      <span class="favorite-id">
           e-Passwords By Users</span> 
 

                    
                  


                </h2>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


<asp:HiddenField ID="searchStringHF" runat="server" />
<asp:HiddenField ID="sortOrderbyHF" Value=" order by userlastname asc" runat="server" />
<asp:HiddenField ID="ShowInactive" Value="False" runat="server" />
<asp:HiddenField ID="currentSQL"  runat="server" />
<div class="grid" id="admin-tools-int-content">
	     <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>E-Password Requests</li><li> e-Passwords By Users </li>  
    </ul> 

   
    
      
</div>
	  <div class="row-12">
	  <ux:SideNav ID="SideNav1" runat="server" />
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
      <asp:Panel ID="searchField" runat="server" >
      <div>
      <h3>Search for Users</h3>
      <p>You may use one or more fields to find your user.</p>
      <p><label for='ctl00_mainContentCP_FirstName'>First</label>  <br />
      <asp:TextBox runat="server" ID="FirstName" ></asp:TextBox></p>

      <p><label for='ctl00_mainContentCP_LastName'>Last Name</label>  <br />
      <asp:TextBox runat="server" ID="LastName" ></asp:TextBox></p>
      
       <p><label for='ctl00_mainContentCP_Email'>Email Address</label> <br /> 
      <asp:TextBox runat="server" ID="Email" ></asp:TextBox></p>


       <p><label for='ctl00_mainContentCP_OfficeCode'>Office Code </label>  <br />
      <asp:TextBox runat="server" ID="OfficeCode" ></asp:TextBox></p>

        <p><label for='ctl00_mainContentCP_PIN'>PIN</label> <br /> 
      <asp:TextBox runat="server" ID="PIN" ></asp:TextBox></p>
      </div>

      <p>
      <asp:Button CssClass="btn" Text="Search Now" runat="server" ToolTip="Search Now" 
          ID="SearchBtn" onclick="SearchBtn_Click"
           />
      
      </p>
      </asp:Panel>

      <asp:Panel runat="server" ID="PanelResult" Visible="false" >
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
	  
	  
	  
 
 
		
          <a name="MOVEHERE"></a>
        <asp:ListView ID="passwordAssistanceLV" OnSorting="passwordAssistanceLV_OnSorting" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="passwordAssistanceLV_ItemDatabound">
         
        <EmptyDataTemplate>
            <h4>
                No Users found.
            </h4>
        </EmptyDataTemplate>
        
        <LayoutTemplate>
        <thead>
	    <tr>
			<th id="header1" scope="col">
            <asp:LinkButton runat="server" ID="LastNameBtn" 
         Text="Last Name" CommandName="Sort" CommandArgument="UserLastName" />

            <asp:ImageButton ID="LastNameImg" CommandArgument="UserLastName" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

               </th>
			<th id="header2" scope="col">
               <asp:LinkButton runat="server" ID="FirstNameBtn" 
         Text="First Name" CommandName="Sort" CommandArgument="UserFirstName" />

            <asp:ImageButton ID="FirstNameImg" CommandArgument="UserFirstName" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

            </th>
			<th id="header3" scope="col">
              <asp:LinkButton runat="server" ID="EmailAddressBtn" 
         Text="Email Address" CommandName="Sort" CommandArgument="UserEmail" />

            <asp:ImageButton ID="EmailAddressImg" CommandArgument="UserEmail" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            
            </th>
			<th id="header4" scope="col">
              <asp:LinkButton runat="server" ID="PINBtn" 
         Text="PIN" CommandName="Sort" CommandArgument="UserPIN" />

            <asp:ImageButton ID="PINImg" CommandArgument="UserPIN" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            </th>
			<th id="header4" scope="col">
              <asp:LinkButton runat="server" ID="OfficeCodeBtn" 
         Text="Office Code" CommandName="Sort" CommandArgument="UserOfficeCode" />

            <asp:ImageButton ID="OfficeCodeImg" CommandArgument="UserOfficeCode" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            </th> 
			 
		</tr>
	  </thead>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
           <tr  <asp:Literal runat="server" ID="rowClick" ></asp:Literal>  >
		  <td headers="header1">  <asp:HyperLink runat="server" ID="UserLastName" ></asp:HyperLink>  </th>
		  <td headers="header2"> <%#Eval("UserFirstName")%> </td>
		  <td headers="header3">  <%#Eval("UserEmail")%> </td>

		   <td headers="header4"> <%#Eval("UserPIN")%></td>

           <td headers="header5"> <%#Eval("UserOfficeCode")%>  </td>
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
                                <asp:TextBox runat="server" ID="pager_textbox" Width="40"  Visible="false" ></asp:TextBox>
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

