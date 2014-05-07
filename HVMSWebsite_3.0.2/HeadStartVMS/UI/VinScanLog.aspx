<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="VinScanLog.aspx.cs" Inherits="METAOPTION.UI.VinScanLog" %>

<asp:Content ID="contVinScanLog" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div onmousemove="SetProgressPosition(event)">
        <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="padding: 5px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="8">
                                VIN Scan Log
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                VIN#
                            </td>
                            <td class="TableBorder">
                                <asp:TextBox ID="txtVIN" runat="server" CssClass="txt2" />
                            </td>
                            <td class="TableBorder">
                                Added By
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlAddedBy" runat="server" CssClass="txt2" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="Btn_Form" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:GridView ID="gvVinScanLog" runat="server" AutoGenerateColumns="false" GridLines="None"
                        CssClass="Grid" RowStyle-CssClass="gvRow" AlternatingRowStyle-CssClass="gvAlternateRow"
                        Width="100%" EmptyDataText="No record found" EmptyDataRowStyle-CssClass="gvEmpty"
                        AllowPaging="true" PageSize="20" DataKeyNames="LogID" PagerStyle-HorizontalAlign="Right"
                        OnPageIndexChanging="gvVinScanLog_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="VIN#" DataField="VIN" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader" />
                            <asp:BoundField HeaderText="Year" DataField="Year" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader" />
                            <asp:BoundField HeaderText="Make" DataField="Make" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader"  />
                            <asp:BoundField HeaderText="Model" DataField="Model" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader"  />
                            <asp:BoundField HeaderText="Latitude" DataField="Latitude" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader" />
                            <asp:BoundField HeaderText="Longitude" DataField="Longitude" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader" />
                            <asp:TemplateField HeaderText="Device<br/>Info" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <%# Eval("DeviceName")%>
                                    <br />
                                    <asp:Label ID="lbldeviceid" runat="server" Text='<%# FormatDeviceID(Eval("DeviceID")) %>'
                                        ToolTip='<%# Eval("DeviceID") %>' CssClass="Tooltip"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Added On" DataField="DateAdded" ItemStyle-CssClass="GridContent"
                                HeaderStyle-CssClass="GridHeader" />
                            <asp:TemplateField HeaderText="Added By" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <%#Eval("DisplayName")%>&nbsp;(<%#Eval("EntityType")%>)
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updMain">
                    <ProgressTemplate>
                        <div id="dvProg" class="overlay">
                            <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please
                            wait...
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
