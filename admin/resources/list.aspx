<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="admin_users_Default" %>
<%@ Register Src="~/admin/controls/SideNav.ascx" TagPrefix="ux" TagName="SideNav" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerCP" Runat="Server">

    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContentCP" Runat="Server">

<div class="grid" id="admin-tools-int-content">
	  
	  <div class="row-12">
	  
	  <ux:SideNav runat="server" />
	  <!-- END COLUMN -->
	  
	  <div class="column-9">
	  
	  <h3><%=h3Output %></h3>
 
	  
	   
	  
 
         
  
	 
     <table class="table table-bordered table-striped">
	   
	  
	  <tbody>
	    <tr>
		  <td headers="header1">Search Resource <br /> <asp:TextBox ID="searchResource" runat="server" ></asp:TextBox> 
              <asp:Button runat="server" ID="searchBtn" Text="GO" onclick="searchBtn_Click" /></th>
		  
 
		  
		</tr>
		 
		
	  </tbody>
	  </table>



	  <table class="table table-bordered table-striped">
	  
	  <thead>
	    <tr>
			
			<th id="header3" scope="col">Resource Name</th>
 


            	<th id="header6" scope="col">Status</th>

			<th id="header1" scope="col">View</th>
			<th id="header2" scope="col">Edit</th>
		</tr>
	  </thead>
	  
	  <tbody>


        <asp:ListView ID="SearchResultLV" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="SearchResult_ItemDatabound">
        <EmptyDataTemplate>
            <h4>
                Search criteria did not match any records.
            </h4>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            
               <tr>
		
		  <td headers="header3"><%#Eval("ResourceName")%>   </td>
 
		  <td headers="header6"><%#Eval("ResourceDisplayStatus")%>   </td>
            <td headers="header1"><asp:HyperLink runat="server" ID="viewLink"></asp:HyperLink></th>
		  <td headers="header2"><asp:HyperLink runat="server" ID="editLink"></asp:HyperLink></td>
		</tr>


        </ItemTemplate>
    </asp:ListView>
    
	  
		 
		
	  </tbody>
	  </table>


      <div class="results_pagination">

        <asp:DataPager ID="DataPager1" PageSize="10" runat="server" PagedControlID="SearchResultLV"
            OnPreRender="Datapager_prender">
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
 

                         
    </div>
	 





	  </div>
	  <!-- END COLUMN -->
	  
	  </div>
	  <!-- END ROW -->
	  
	  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

