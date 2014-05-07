<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="ManageLocation.aspx.cs" 
    Inherits="METAOPTION.UI.ManageLocation" Title="HeadStartVMS::Manage Location" %>

<asp:Content ID="contManageLocation" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="TableHeadingBg TableHeading">
                Manage Location
            </div>
            <div style="padding:5px 0px 5px 15px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width:100px; font-size:12px">Location Status</td>
                        <td>
                            <asp:DropDownList ID="ddlLocationStatus" runat="server" CssClass="txt1" OnSelectedIndexChanged="ddlLocationStatus_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="ALL" Value="-1" />
                                <asp:ListItem Text="Active" Value="1" Selected="True" />
                                <asp:ListItem Text="Archived" Value="2" />
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:right">
                            <asp:Button ID="btnAdd" runat="server" Text="ADD NEW" CssClass="Btn_Form" 
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding:5px">
                <asp:GridView ID="gvLocation" runat="server"
                    AutoGenerateColumns="false"
                    GridLines="None"
                    CssClass="Grid"
                    RowStyle-CssClass="gvRow"
                    AlternatingRowStyle-CssClass="gvAlternateRow"
                    Width="100%"
                    EmptyDataText="No record found"  
                    EmptyDataRowStyle-CssClass="gvEmpty"
                    >
                    <Columns>
                        <asp:BoundField HeaderText="Location Code" DataField="LocationCode" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="150px" />
                        <asp:BoundField HeaderText="Description" DataField="LocationDesc" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField HeaderText="Entity" DataField="EntityName" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" />
                        <asp:BoundField HeaderText="Type" DataField="LocationType" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader" ItemStyle-Width="110px" />
                        <asp:TemplateField HeaderStyle-Width="230px">
                            <HeaderTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <th colspan="3" style="text-align:center" class="GridHeader">PreInventory</th>
                                    </tr>
                                    <tr>
                                        <th class="GridHeader">Pending</th>
                                        <th class="GridHeader">Rejected</th>
                                        <th class="GridHeader" title="Added To Inventory">Added</th>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width:33%; text-align:right" class="GridContent"><%#Eval("Pending")%></td>
                                        <td style="width:33%; text-align:right" class="GridContent"><%#Eval("Discarded")%></td>
                                        <td style="width:33%; text-align:right" class="GridContent"><%#Eval("AddedToInventory")%></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="230px">
                            <HeaderTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <th colspan="3" style="text-align:center" class="GridHeader">Inventory</th>
                                    </tr>
                                    <tr>
                                        <th class="GridHeader">Inventory</th>
                                        <th class="GridHeader">On-Hand</th>
                                        <th class="GridHeader">Archived</th>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width:33%; text-align:right" class="GridContent"><%#Eval("Inventory")%></td>
                                        <td style="width:33%; text-align:right" class="GridContent"><%#Eval("OnHand")%></td>
                                        <td style="width:33%; text-align:right" class="GridContent"><%#Eval("Archived")%></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
