<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Default1.aspx.cs" Inherits="admin_users_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">

<div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column">
  <div class="buttons" >
	  <%--	<a href="#" class="btn">Add User</a> --%>
        <asp:LinkButton runat="server" CssClass="btn" ID="AddUser" Text="Add User" 
          onclick="AddUser_Click" ></asp:LinkButton>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
        Search : <asp:TextBox runat="server" Width="250" ID="searchWordTxt" ></asp:TextBox>
       
        <asp:LinkButton runat="server" Text="Update List" CssClass="btn" 
            ID="updateList" onclick="updateList_Click" 
              ></asp:LinkButton>
 <span class="results_content" runat="server" id="pagertop" visible="false">
 

Results <asp:Label runat="server" ID="idResultsLabel" ></asp:Label> &nbsp;&nbsp;&nbsp; |   &nbsp;&nbsp;
<asp:LinkButton runat="server"  ID="ExcelClick" 
              Text="Download Report to Excel" onclick="ExcelClick_Click"   ></asp:LinkButton>
              
              	  <span class="float-right">Show:   <asp:DropDownList runat="server" AutoPostBack="true" ID="show_results" OnSelectedIndexChanged="Index_Changed" >
                    <asp:ListItem Value="1">1</asp:ListItem>
                      <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
            <asp:ListItem Value="10">10</asp:ListItem>
             <asp:ListItem Value="25">25</asp:ListItem>
              <asp:ListItem Value="50">50</asp:ListItem>
               <asp:ListItem Value="100">100</asp:ListItem>                
            </asp:DropDownList></span> 
 </span>

 	
	  </div>
	   
      <asp:LinkButton runat="server" CssClass="float-right" ID="ShowInactiveEmployees" 
              Text="Show Inactive Employees" onclick="ShowInactiveEmployees_Click" ></asp:LinkButton>
	   

        <table class="table table-bordered table-striped">
	  <caption><h3></h3></caption>


      </table>


      <asp:GridView ID="usersListGV" Runat="server" 
    
    AutoGenerateColumns="False" BorderWidth="1px" OnRowDataBound="UsersGridView_RowDataBound"  OnPageIndexChanging="gridView_PageIndexChanging"
      GridLines="Horizontal" AllowSorting="true"
    CellPadding="3" CssClass="table table-bordered table-striped" AllowPaging="true" PageSize="2"  >

         <emptydatarowstyle  
          forecolor="Red"/>

        <emptydatatemplate>

          

            No Data Found.  

        </emptydatatemplate> 
        
        <HeaderStyle BackColor="#EEEEEE" />
    
   
    <Columns>
   

 
              <%-- <asp:BoundField DataField="EventID" HeaderText="Event ID" />--%>
         

          
 <%-- <tr>
		  <td headers="header1"><%#Eval("LastName")%></th>
		  <td headers="header2"><%#Eval("FirstName")%></td>
		  <td headers="header3"><%#Eval("PIN")%></td>
		  <td headers="header4"><%#Eval("AccessLevel")%></td>
		  <td headers="header5"><asp:Literal runat="server" ID="CORExpireDate" ></asp:Literal></td>
		   <td headers="header4"><asp:Literal runat="server" ID="Active" ></asp:Literal></td>
		  <td headers="header5"><asp:Literal runat="server" ID="LastAccessed" ></asp:Literal></td>
		</tr>--%>

		   
			
      
          <asp:BoundField DataField="LastName" HeaderText="Last Name" />
           <asp:BoundField DataField="FirstName" HeaderText="First Name" />
             <asp:BoundField DataField="PIN" HeaderText="PIN" />
               <asp:BoundField DataField="AccessLevel" HeaderText="Access" />
                 <asp:TemplateField HeaderText="COR through">
            <ItemTemplate>
                <asp:Literal runat="server" ID="CORExpireDate" ></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>  

             <asp:TemplateField HeaderText="Active">
            <ItemTemplate>
                <asp:Literal runat="server" ID="Active" ></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>  
                  
        <asp:TemplateField HeaderText="Last Accessed">
            <ItemTemplate>
                <asp:Literal runat="server" ID="LastAccessed" ></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>  
                  
 
              
<%--
           <asp:TemplateField HeaderText="Territories">
           <ItemTemplate>

          

            <asp:BulletedList ID="bltTerritories" Runat="server" 
                DataTextField="TerritoryDescription"
                DataValueField="TerritoryDescription">
            </asp:BulletedList>
        </ItemTemplate>
        </asp:TemplateField>--%>

        
    </Columns>
    
</asp:GridView>
        
    <div class="results_pagination">

         
 

                          <div class="float-right" runat="server" id="pagerbelow" visible="false">
                            <p>
                                <label for="pager_textbox" class="inline-label">
                                    Go to page</label>
                                <asp:TextBox runat="server" ID="pager_textbox" Width="40"  ></asp:TextBox>
                                 
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:Button runat="server" ID="goBtn" onclick="goBtnClick" CssClass="btn" Text="Go" />
                            </p>
                        </div>
    </div>


      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

