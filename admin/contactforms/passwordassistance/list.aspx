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
 
	  
	   
	 
       <script>
           $(function () {
               $("#ctl00_mainContentCP_DateFrom").datepicker();
               $("#ctl00_mainContentCP_DateTo").datepicker();
           });
  </script>
         <div class="buttons">
	  	<a href="#" class="btn">Email This Report</a>
	  	<a href="#" class="float-right">Download Report to Excel</a>
	  </div>
  
	 
     <table class="table table-bordered table-striped">
	   
	  
	  <tbody>
	    <tr>
		  <td headers="header1">Submission Date from <br /> <asp:TextBox ID="DateFrom" runat="server" TextMode="Date" ></asp:TextBox>  </th>
		  <td headers="header2">Submission Date to <br /> <asp:TextBox ID="DateTo" runat="server" TextMode="Date" ></asp:TextBox>  
              <asp:Button runat="server" Text="GO" ID="SearchDates" 
                  onclick="SearchDates_Click" /></td>
 
		  
		</tr>
		 
		
	  </tbody>
	  </table>



	  <table class="table table-bordered table-striped">
	  
	  <thead>
	    <tr>
			
			<th id="header3" scope="col">Submitted By PIN</th>
			<th id="header4" scope="col">Submitted By LastName</th>
			<th id="header5" scope="col">Submitted By FirstName</th>


            	<th id="header6" scope="col">Form Status</th>

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
		
		  <td headers="header3"><%#Eval("SubmittedByPIN")%>   </td>
		  <td headers="header4"><%#Eval("SubmittedByLastName")%>   </td>
		   <td headers="header5"><%#Eval("SubmittedByFirstName")%>   </td>
		  <td headers="header6"><%#Eval("FormStatus")%>   </td>
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

