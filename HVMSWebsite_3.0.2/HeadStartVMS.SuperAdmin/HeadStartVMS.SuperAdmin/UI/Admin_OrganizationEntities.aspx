<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Admin_Master.Master"
    Title="Admin Panel::Organization Entities" CodeBehind="Admin_OrganizationEntities.aspx.cs"
    Inherits="METAOPTION.UI.Admin_OrganizationEntities" %>

<asp:Content ID="Admin_OrgEntities" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
        .GridContent_alignCenter
        {
            border: #e2e2e2 1px solid;
            font-size: 11px;
            color: #535152;
            font-family: Arial, Helvetica, sans-serif;
            text-decoration: none;
            padding: 4px;
            text-align: center;
        }
    </style>
    <table width="99%" style="padding-left: 1%">
        <tr>
            <td>
                <div>
                    <fieldset class="ForFieldSet">
                        <legend class="ForLegend">Organization Entities</legend>
                        <div style="width: 100%; height: 400px">
                            <asp:GridView ID="gv_entity" Width="100%" DataKeyNames="OrgID" HeaderStyle-BackColor="Silver" 
                                PageSize="20" runat="server" AutoGenerateColumns="false" GridLines="None" EmptyDataText="No Record Found" AllowPaging="true" OnPageIndexChanging="gv_entity_pageindexchange">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. no."  
                                        ItemStyle-CssClass="GridContent_alignCenter" HeaderStyle-CssClass="GridContent_alignCenter" HeaderStyle-Width="40px" >
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Organization" DataField="Organisation"  
                                         ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" HeaderStyle-Width="150px" />
                                    <asp:BoundField HeaderText="Organization Code" DataField="OrgCode" 
                                         ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent" HeaderStyle-Width="150px" />
                                    <asp:BoundField HeaderText="Buyer" DataField="Buyer" 
                                         ItemStyle-CssClass="GridContent_alignCenter" HeaderStyle-CssClass="GridContent_alignCenter" HeaderStyle-Width="100px"/>
                                    <asp:BoundField HeaderText="Dealer" DataField="Dealer" 
                                         ItemStyle-CssClass="GridContent_alignCenter" HeaderStyle-CssClass="GridContent_alignCenter" HeaderStyle-Width="100px" />
                                    <asp:BoundField HeaderText="Employee" DataField="Employee" 
                                         ItemStyle-CssClass="GridContent_alignCenter" HeaderStyle-CssClass="GridContent_alignCenter" HeaderStyle-Width="100px" />
                                    <asp:BoundField HeaderText="Utility Company" DataField="UtilityCompany" 
                                         ItemStyle-CssClass="GridContent_alignCenter" HeaderStyle-CssClass="GridContent_alignCenter" HeaderStyle-Width="100px" />
                                    <asp:BoundField HeaderText="Vendor" DataField="Vendor"
                                         ItemStyle-CssClass="GridContent_alignCenter" HeaderStyle-CssClass="GridContent_alignCenter" HeaderStyle-Width="100px" />
                                </Columns>
                                <RowStyle CssClass="gvRow" />
                                <AlternatingRowStyle CssClass="gvAlternateRow" />
                                <HeaderStyle CssClass="gvHeading" />
                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                <EmptyDataRowStyle CssClass="gvEmpty" />
                            </asp:GridView>
                        </div>
                    </fieldset>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
