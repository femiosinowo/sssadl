<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_users_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
<script>

    function goto(id) {
        location.href = '/admin/users/view.aspx?userid=' + id;
    }
 
 
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">

     <div id="title-bar">
    <h2>
      <span class="favorite-id">
         Users</span> 
 

                    
                  


                </h2>
</div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">


<asp:HiddenField ID="searchStringHF" runat="server" />
<asp:HiddenField ID="sortOrderbyHF" Value=" order by lastname asc" runat="server" />
<asp:HiddenField ID="ShowInactive" Value="False" runat="server" />

<div class="grid" id="admin-tools-int-content">
	  
      <div class="ssadl-breadcrumbs margin-bottom">
     <ul>
          <li><a href="/admin/">Home</a></li><li>Users</li>  
    </ul> 

   
    
      
</div>


	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column">
  <div class="buttons" >
	  <%--	<a href="#" class="btn">Add User</a> --%>
        <asp:LinkButton runat="server" CssClass="btn" ID="AddUser" Text="Add User" 
          onclick="AddUser_Click" ></asp:LinkButton>

          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
        Search : <asp:TextBox runat="server" Width="250" ID="searchWordTxt" ></asp:TextBox>
       
        <asp:LinkButton runat="server" Text="Search Now" CssClass="btn" 
            ID="updateList" onclick="updateList_Click" 
              ></asp:LinkButton>


 	
	  </div>      <asp:Panel ID="actionMessage" class="container-green" Visible="false" runat="server">
             <h4><%=actionMessages %></h4> 
          </asp:Panel>
	         <span class="results_content" runat="server" id="pagertop"  >
 

Results <asp:Label runat="server" ID="idResultsLabel" ></asp:Label> &nbsp;&nbsp;&nbsp; |   &nbsp;&nbsp;
<asp:LinkButton runat="server"  ID="ExcelClick" 
              Text="Download Report to Excel" onclick="ExcelClick_Click"   ></asp:LinkButton>
              
              	  <span class="float-right">Show:   <asp:DropDownList runat="server" AutoPostBack="true" ID="show_results" OnSelectedIndexChanged="Index_Changed" >
            
             <asp:ListItem Value="25">25</asp:ListItem>
              <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="100">100</asp:ListItem>    
                 <asp:ListItem Value="250">250</asp:ListItem>                    
            </asp:DropDownList></span> 
 </span>
      <asp:LinkButton runat="server" CssClass="float-right" ID="ShowInactiveEmployees" 
              Text="Show Inactive Employees" onclick="ShowInactiveEmployees_Click" ></asp:LinkButton>
      

	  <table class="table table-bordered table-striped">
 
	
 
 
		

        <asp:ListView ID="usersLV" OnSorting="UsersListView_OnSorting" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="userst_ItemDatabound">
         
        <EmptyDataTemplate>
            <h4>
                No Users found.
            </h4>
        </EmptyDataTemplate>
        
        <LayoutTemplate>
        <thead>
	    <tr   >
			<th id="header1" scope="col">
            <asp:LinkButton runat="server" ID="LastNameBtn" 
         Text="Last Name" CommandName="Sort" CommandArgument="LastName" />

            <asp:ImageButton ID="LastNameImg" CommandArgument="LastName"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

               </th>
			<th id="header2" scope="col">
            <asp:LinkButton runat="server" ID="FirstNameBtn" 
         Text="First Name" CommandName="Sort" CommandArgument="FirstName" />
           <asp:ImageButton ID="FirstNameImg" CommandArgument="FirstName"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

            </th>
			<th id="header3" scope="col">

                  <asp:LinkButton runat="server" ID="PINBtn" 
         Text="PIN" CommandName="Sort" CommandArgument="PIN" />
           <asp:ImageButton ID="PINImg" CommandArgument="PIN"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />


			</th>
			<th id="header4" scope="col">  <asp:LinkButton runat="server" ID="AccessLevelBtn" 
         Text="Access" CommandName="Sort" CommandArgument="AccessLevel" />
           <asp:ImageButton ID="AccessLevelImg" CommandArgument="AccessLevel"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
 </th>
			<th id="header5" scope="col">  <asp:LinkButton runat="server" ID="CORExpireDateBtn" 
         Text="COR through" CommandName="Sort" CommandArgument="COR" />
           <asp:ImageButton ID="CORExpireDateImg" CommandArgument="COR"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" /></th>
            <th id="Th1" scope="col">

                 <asp:LinkButton runat="server" ID="ActiveBtn" 
         Text="Active " CommandName="Sort" CommandArgument="Active" />
           <asp:ImageButton ID="ActiveImg" CommandArgument="Active"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

            </th>
			<th id="Th2" scope="col"> 

                 <asp:LinkButton runat="server" ID="LastAccessedBtn" 
         Text="Last Accessed " CommandName="Sort" CommandArgument="LastAccessed" />
           <asp:ImageButton ID="LastAccessedImg" CommandArgument="LastAccessed"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />

			</th>
			 
		</tr>
	  </thead>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
           <tr <asp:Literal runat="server" ID="rowClick" ></asp:Literal> >
		  <td headers="header1">   <asp:HyperLink runat="server" ID="LastName" ></asp:HyperLink> </td>
		  <td headers="header2"><%#Eval("FirstName")%></td>
		  <td headers="header3"><%#Eval("PIN")%></td>
		  <td headers="header4"><%#Eval("AccessLevel")%></td>
		  <td headers="header5"><asp:Literal runat="server" ID="CORExpireDate" ></asp:Literal></td>
		   <td headers="header4"><asp:Literal runat="server" ID="Active" ></asp:Literal></td>
		  <td headers="header5"><asp:Literal runat="server" ID="LastAccessed" ></asp:Literal></td>
           
		</tr>
        </ItemTemplate>
    </asp:ListView>

     


	  
	  </table>



        
    <div class="results_pagination">

        <asp:DataPager ID="DataPager1" PageSize="25" runat="server" PagedControlID="usersLV"
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
 

                          <div class="float-right" runat="server" id="pagerbelow"  >
                            <p>
                                <label for="pager_textbox" class="inline-label">
                                    Go to page</label>
                                <asp:TextBox runat="server" ID="pager_textbox" onkeypress="return isNumberKey(event)" Width="40" Visible="false" ></asp:TextBox>
                                  <asp:DropDownList runat="server" ID="pager_dropdown" AutoPostBack="true" Width="50" OnSelectedIndexChanged="pager_dropdown_SelectedIndexChanged" ></asp:DropDownList>
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:Button runat="server" ID="goBtn" onclick="goBtnClick" CssClass="btn" Text="Go"  Visible="false" />
                            </p>
                        </div>
    </div>


      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

