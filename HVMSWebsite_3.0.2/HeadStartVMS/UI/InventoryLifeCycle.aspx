<%@ Page Language="C#"  AutoEventWireup="true"
    CodeBehind="InventoryLifeCycle.aspx.cs" Inherits="METAOPTION.UI.InventoryLifeCycle"
    Title="Inventory Life Cycle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnlChromeHistory" runat="server">
        <ContentTemplate>
            <div class="AddHeading">
            </div>
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                <tr>
                    <td align="left">
                        <asp:GridView runat="server" Width="100%" ID="gvInventoryCycle" widh="100px%" AllowPaging="true"
                            PageSize="20" AutoGenerateColumns="False" EmptyDataText="No Rows found" OnPageIndexChanging="gvInventoryCycle_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="HISTORY" HeaderText="INVENTORY LIFE CYCLE EVENTS" />
                            </Columns>
                            <AlternatingRowStyle CssClass="gvAlternateRow" />
                            <HeaderStyle CssClass="gvHeading" />
                            <RowStyle CssClass="gvRow" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" style="width: 98%; padding-left: 10px;">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkbtnBack" runat="server" OnClick="lnkbtnBack_Click" CausesValidation="false">
                                 <img src="../Images/back.jpg" alt="back" style="border:none; padding-top:10px" />
                                    </asp:LinkButton>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
