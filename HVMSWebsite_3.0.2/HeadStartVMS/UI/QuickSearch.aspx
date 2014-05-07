<%@ Page Language="C#"  AutoEventWireup="true"
   CodeBehind="QuickSearch.aspx.cs" Inherits="METAOPTION.UI.QuickSearch"
   Title="HeadstartVMS::Quick Inventory Search" %>

<asp:Content ID="cphSearchInventory" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
      <tr>
         <td class="TableHeadingBg TableHeading"  colspan="2">
            Quick Inventory Search
         </td>
         
      </tr>
      <tr>
         <td colspan="2">
            <asp:GridView ID="gvInventoryList" DataKeyNames="InventoryId" runat="server" Width="100%" AutoGenerateColumns="False"
         EmptyDataText="No record found for this search criteria."
            RowStyle-CssClass="gvRow"  AllowPaging="true" PageSize="25"
            PagerSettings-Mode="NumericFirstLast">
            <Columns>
               <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
               <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                  <ItemTemplate>
                     <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "InventoryDetail.aspx?Mode=View&Code="+Eval("InventoryId") %>'
                        runat="server" ImageUrl="~/Images/Select.gif" />
                  </ItemTemplate>
                  <HeaderStyle Width="20px"></HeaderStyle>
               </asp:TemplateField>
               <asp:BoundField DataField="VIN" HeaderText="VIN #" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                  <HeaderStyle Wrap="False"></HeaderStyle>
               </asp:BoundField>
               <asp:BoundField DataField="ModelName" HeaderText="Model"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"/>
               <asp:BoundField DataField="MakeName" HeaderText="Make"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"/>
               <asp:BoundField DataField="Arrival" HeaderText="Arrival Date" HeaderStyle-Wrap="false" DataFormatString="{0:d}"  HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                  <HeaderStyle Wrap="False"></HeaderStyle>
               </asp:BoundField>
               <asp:BoundField DataField="TitleYes" HeaderText="Present" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                  <HeaderStyle Wrap="False"></HeaderStyle>
                  <ItemStyle HorizontalAlign="Center" />
               </asp:BoundField>
               <asp:BoundField DataField="CarCost" HeaderText="Car Cost" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                  <HeaderStyle Wrap="False"></HeaderStyle>
                  <ItemStyle HorizontalAlign="Right"  />
               </asp:BoundField>
               <asp:BoundField DataField="MileageIn" HeaderText="Mileage In" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                  <HeaderStyle Wrap="False"></HeaderStyle>
                  <ItemStyle HorizontalAlign="Right" />
               </asp:BoundField>
               <asp:BoundField DataField="ComeBackYes" HeaderText="Come Back" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                  <HeaderStyle Wrap="False"></HeaderStyle>
                  <ItemStyle HorizontalAlign="Center" />
               </asp:BoundField>
               <asp:BoundField DataField="DealerName" HeaderText="Dealer Name" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                  <HeaderStyle Wrap="False"></HeaderStyle>
               </asp:BoundField>
            </Columns>
            <RowStyle CssClass="gvRow" />
            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
            <HeaderStyle CssClass="gvHeading"></HeaderStyle>
            <AlternatingRowStyle CssClass="gvAlternateRow"></AlternatingRowStyle>
            <EmptyDataRowStyle CssClass="gvEmpty" />
         </asp:GridView>
         </td>
      </tr>
   </table>
  

   

</asp:Content>
