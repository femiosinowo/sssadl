<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Masters/AdminMain.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_users_Default" %>

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
       location.href = '/admin/resources/view.aspx?resourceID=' + id;
   }
 
 
         </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageTitleCP" Runat="Server">

     <div id="title-bar">
    <h2>
      <span class="favorite-id">
         Resources</span> 
 

                    
                  


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
          <li><a href="/admin/">Home</a></li>
           
         <li>Resources</li>  
    </ul> 

   
    
      
</div>


	  <div class="row-12">
	  
	  
	  <!-- END COLUMN -->
	  
	  <div class="column-12 print-full-width">
  <div class="buttons" >
	  <%--	<a href="#" class="btn">Add User</a> --%>
        
          <a id="ctl00_mainContentCP_AddResource" class="btn" href='/admin/resources/add.aspx'>Add a New Resource</a>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
        Search : <asp:TextBox runat="server" Width="250" ID="searchWordTxt" ></asp:TextBox>
       
        <asp:LinkButton runat="server" Text="Search Now" CssClass="btn" 
            ID="updateList" onclick="updateList_Click" 
              ></asp:LinkButton>


 	
	  </div>
          <hr />
	   <div>

           <div class="column-1">
               <b>Show:</b></div>
        <div class="column-3">
         Subject Areas<br />
     <asp:ListBox runat="server" ID="SubjectAreasTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" SelectionMode="Multiple" Height="120"  ToolTip="Select One or more Subject Area(s)"  ></asp:ListBox> <br />
     <a href="javascript:selectAll('ctl00_mainContentCP_SubjectAreasTaxonomy')"   >Select All</a> |  <a href="javascript:selectNone('ctl00_mainContentCP_SubjectAreasTaxonomy')">Select None</a>
       </div>
       <div class="column-3">
       <p>
       e-Password <br />
       <asp:DropDownList runat="server" ID="ePassDD" >
       
       <asp:ListItem Selected="True" Value="All" Text="All" ></asp:ListItem>
       <asp:ListItem   Value="ePassword" Text="e-Password" ></asp:ListItem>
       <asp:ListItem   Value="noneePassword" Text="None e-Password" ></asp:ListItem>
       </asp:DropDownList> 
       </p>
       </div>
       
       <div class="column-3">
       <p></p><asp:LinkButton CssClass="btn"  runat="server" ID="UpdateListBtnForSubJEPass" 
               Text="Apply Filters" onclick="UpdateListBtnForSubJEPass_Click" ></asp:LinkButton></div>
       </div>
      <asp:LinkButton runat="server" CssClass="float-right" ID="ShowInactiveResources" 
              Text="Show Inactive Resources" onclick="ShowInactiveResources_Click" ></asp:LinkButton>

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


	  <table class="table table-bordered table-striped">
	  
	     
	  
                  
 
	  
 
 
		

        <asp:ListView ID="resourcesLV" OnSorting="resourcesLV_OnSorting" runat="server" ItemPlaceholderID="phLV" OnItemDataBound="ResourceLV_ItemDatabound">
         
        <EmptyDataTemplate>
		<caption><h3>No Resources found.</h3></caption>
            
        </EmptyDataTemplate>
        
        <LayoutTemplate>
        <thead>
	    <tr>
			<th id="header1" scope="col">
            <asp:LinkButton runat="server" ID="ResourceNameBtn" 
         Text="Resource Name" CommandName="Sort" CommandArgument="ResourceName" />

            <asp:ImageButton ID="ResourceNameImg" CommandArgument="ResourceName" CommandName="Sort" AlternateText="Sort by ascending/descending order" ImageUrl="~/admin/img/asc.png" runat="server" />

               </th>
			<th id="header2" scope="col">
              Subject Areas
                        <%-- <asp:LinkButton runat="server" ID="SubjectAreasBtn" 
         Text="Subject Areas" CommandName="Sort" CommandArgument="SubjectAreas" />

            <asp:ImageButton ID="SubjectAreasImg" CommandArgument="SubjectAreas" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />--%>


            </th>
			<th id="header3" scope="col">
                        <asp:LinkButton runat="server" ID="ePassBtn" 
         Text="e-Pass" CommandName="Sort" CommandArgument="ePass" />

            <asp:ImageButton ID="ePassImg" CommandArgument="ePass" CommandName="Sort"  AlternateText="Sort by ascending/descending order" ImageUrl="~/admin/img/asc.png" runat="server" />
            
            </th>
			<th id="header4" scope="col">
            
                        <asp:LinkButton runat="server" ID="StatusBtn" 
         Text="Status" CommandName="Sort" CommandArgument="ResourceDisplayStatus" />

            <asp:ImageButton ID="StatusImg" CommandArgument="ResourceDisplayStatus"  AlternateText="Sort by ascending/descending order" CommandName="Sort" ImageUrl="~/admin/img/asc.png" runat="server" />
            
            </th>
		 
		</tr>
	  </thead>
            <asp:PlaceHolder ID="phLV" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
           <tr <asp:Literal runat="server" ID="rowClick" ></asp:Literal> >
		  <td headers="header1">  <asp:HyperLink runat="server" ID="ResourceName" ></asp:HyperLink>  </td>
		  <td headers="header2"><asp:Literal runat="server" ID="SubjectAreasTaxonomyLit" ></asp:Literal> </td>
		  <td headers="header3"> <asp:Literal runat="server" ID="ePass" ></asp:Literal> </td>

		   <td headers="header4"><asp:Literal runat="server" ID="Active" ></asp:Literal></td>

           
		</tr>
        </ItemTemplate>
    </asp:ListView>

     


	  
	  </table>



        
    <div class="results_pagination" id="pagerDiv" runat="server">

        <asp:DataPager ID="DataPager1" PageSize="10" runat="server" PagedControlID="resourcesLV"
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
 

                          <div class="float-right" runat="server" id="pagerbelow" >
                            <p>
                                <label for="pager_textbox" class="inline-label">
                                    Go to page</label>
                                <asp:TextBox runat="server" ID="pager_textbox" onkeypress="return isNumberKey(event)" Width="40" Visible="false"  ></asp:TextBox>
                                  <asp:DropDownList runat="server" ID="pager_dropdown" AutoPostBack="true" Width="50" OnSelectedIndexChanged="pager_dropdown_SelectedIndexChanged" ></asp:DropDownList>
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:LinkButton runat="server" ID="goBtn" onclick="goBtnClick" CssClass="btn" Text="Go"  Visible="false" ></asp:LinkButton>
                            </p>
                        </div>
    </div>


      </div></div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footerCP" Runat="Server">
</asp:Content>

