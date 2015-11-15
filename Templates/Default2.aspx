<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Templates_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainPBPageHost" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" Runat="Server">

 <ul class="divider no-margin">
      <asp:ListView     ID="DL_newslist"   runat="server" OnItemDataBound="DL_newslist_ItemDatabound" >
               
 
               <ItemTemplate>
 <li>
			<div class="byline"> <asp:Literal runat="server" ID="NewsDate" ></asp:Literal></div>
			 
            <asp:HyperLink runat="server" CssClass="post"  ID="NewsTitle" ></asp:HyperLink>
		</li>

   
                  <!--end multimedia_block -->


 


               </ItemTemplate>
               </asp:ListView>
	   
        </ul>
         <asp:DataPager ID="DataPager1"  PageSize="2" runat="server" PagedControlID="DL_newslist" OnPreRender="Datapager_prender">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="false" ShowNextPageButton="false"
                                    ButtonCssClass="arrow prev" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowNextPageButton="true"
                                    ButtonCssClass="arrow next" ShowPreviousPageButton="false" />
                            </Fields>
                        </asp:DataPager>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphJSCode" Runat="Server">
</asp:Content>

