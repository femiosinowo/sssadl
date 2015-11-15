<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true" CodeFile="subjectareadetails.aspx.cs" Inherits="Templates_Content" %>
<%@ Register Src="~/Controls/RightSideColumn.ascx" TagPrefix="ux"  TagName="RightSideContent" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/Controls/PageTitle.ascx" TagPrefix="ux" TagName="PageTitle" %>
<%@ Register Src="~/Controls/Breadcrumbs.ascx" TagPrefix="ux" TagName="Breadcrumbs" %>


 <asp:Content ID="Content5" ContentPlaceHolderID="cphSecondaryPageTitle" runat="Server">
 <ux:PageTitle ID="uxPageTitle"  runat="server" />
</asp:Content>

 <asp:Content ID="Content6" ContentPlaceHolderID="cphSecondaryBreadcrumb" runat="Server">
<ux:Breadcrumbs ID="uxBreadcrumb" runat="server" />
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="cphSecondaryMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphSecondaryMainContent" runat="Server">

  <CMS:ContentBlock ID="mainContent" DynamicParameter="id"   runat="server" />

 <h3><%=SATitle %></h3>

 <%=SADescription %>
  <hr />

  <div class="type_search">
		  <label for="resource_type">Resource Type:</label>

            <asp:DropDownList runat="server" ID="ResourceTypeTaxonomy"  DataTextField="TaxName" DataValueField="TaxID" >
            <asp:ListItem Selected="True" Text="All Resources" Value="" ></asp:ListItem>
            
            </asp:DropDownList>
 
		  <asp:Button runat="server" CssClass="btn" Text="Show" 
               ID="BtnShowSelectedResourceType" 
              onclick="BtnShowSelectedResourceType_Click" />
	 		 
	  </div>
       <span class="results_content" runat="server" id="pagertop"  >
 

Showing <asp:Label runat="server" ID="idResultsLabel" ></asp:Label> &nbsp;&nbsp;&nbsp; 
</span>

      <asp:HiddenField runat="server" ID="SubjectAreaTaxIDHF" /> 
      
      <a name="MOVEHERE"></a>
      <asp:ListView     ID="DL_Resourceslist"   runat="server" OnItemDataBound="DL_Resourceslist_ItemDatabound" >
               
 <EmptyDataTemplate>
 <table class="table table-bordered table-striped database-table">
				    
					<thead tabindex="0" role="button">
						<tr>
						   <th>No Resource found</th>
						</tr>	
					 </thead>
                     </table>
 </EmptyDataTemplate>
               <ItemTemplate>
  


          <p>
           <asp:HyperLink runat="server" CssClass="post"  ID="ResourceTitle" ></asp:HyperLink>
           <asp:Literal runat="server" ID="myfavicons" ></asp:Literal>
          
	  <br/>
	  <asp:Literal runat="server" ID="Description" ></asp:Literal>
      <asp:Literal runat="server" ID="LoginInfomation" ></asp:Literal>
      
      <asp:Panel runat="server" ID="sharedPasswordPanel" Visible="false" >
      
      
      </asp:Panel>
      <asp:Literal runat="server" ID="bottomLinks" ></asp:Literal>
      </p> 

   
                  <!--end multimedia_block -->


 


               </ItemTemplate>
               </asp:ListView>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Ektron.DbConnection %>"
                                        >
                                    </asp:SqlDataSource>
                                    <hr />

                  <div class="results_pagination">

        <asp:DataPager ID="DataPager1" PageSize="30" runat="server" PagedControlID="DL_Resourceslist"
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
                                <asp:TextBox runat="server" ID="pager_textbox" Width="40"  ></asp:TextBox>
                                 
                                of <span>
                                    <asp:Label runat="server" ID="labelTotalPages"   /></span>
                                 
                                <asp:Button runat="server" ID="goBtn" onclick="goBtnClick" CssClass="btn" Text="Go" />
                            </p>
                        </div>
    </div>
 
    
	  <!-- END COLUMN -->
	  
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphSideContent" runat="Server">
    <ux:RightSideContent ID="RightSideContent" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphSecondaryJSCode" runat="Server">
</asp:Content>
