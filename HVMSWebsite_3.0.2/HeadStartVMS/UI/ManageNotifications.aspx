<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageNotifications.aspx.cs"
    Inherits="METAOPTION.UI.ManageNotifications" Title="HeadstartVMS::Manage Notifications" %>

<asp:content id="contManageNotifications" runat="server" contentplaceholderid="ContentPlaceHolder1">
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="TableHeadingBg TableHeading" colspan="6">
                            MANAGE NOTIFICATIONS
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 5px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="lblb" style="width: 100px">
                            Notification Type
                        </td>
                        <td class="lbl" style="width: 150px">
                            <asp:DropDownList ID="ddlNotificationTypes" runat="server" CssClass="txt2" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlNotificationTypes_SelectedIndexChanged" />
                        </td>
                        <td class="lbl" style="text-align: center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn" OnClick="btnAdd_Click"
                                Width="75px" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvNotificationCcBcc" runat="server" GridLines="None" DataKeyNames="NotificationRecipientCcBccID"
                    AutoGenerateColumns="false" Width="100%" EmptyDataRowStyle-CssClass="gvEmpty"
                    EmptyDataText="No record found" OnRowDataBound="gvNotificationCcBcc_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent" />
                        <asp:BoundField HeaderText="Type" DataField="EmployeeType" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent" />
                        <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent" />
                        <asp:BoundField HeaderText="Cell" DataField="CellPhone" HeaderStyle-CssClass="GridHeader"
                            ItemStyle-CssClass="GridContent" />
                        <asp:TemplateField HeaderText="CC" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEmailCC" runat="server" ImageUrl="~/Images/H_active.png"
                                    OnClick="ibtnEmailCC_Click" />
                                <asp:HiddenField ID="hfEmailCC" runat="server" Value='<%#Eval("EmailCC") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BCC" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEmailBCC" runat="server" ImageUrl="~/Images/H_active.png"
                                    OnClick="ibtnEmailBCC_Click" />
                                <asp:HiddenField ID="hfEmailBCC" runat="server" Value='<%#Eval("EmailBCC") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SMS" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnSMS" runat="server" ImageUrl="~/Images/H_active.png" OnClick="ibtnSMS_Click" />
                                <asp:HiddenField ID="hfSMS" runat="server" Value='<%#Eval("SMS") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="~/Images/DeleteButton1.png"
                                    ToolTip="Delete" OnClick="ibtnDelete_Click" OnClientClick="javascript:return (confirm ('Do you want to delete this preference?'));" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <%--Add CC --%>
                <asp:Panel ID="pnlAddCC" runat="server" CssClass="modalPopup" Style="display: none;"
                    HorizontalAlign="Left" Width="700px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="5" class="PopUpBoxHeading">
                                &nbsp;&nbsp;Add CC/BCC/SMS
                            </td>
                            <td class="PopUpBoxHeading" align="right">
                                <img border="0" src="../Images/close.gif" id="imgCloseAddCCPopUp" onclick="return false;"
                                    alt="close" />
                            </td>
                        </tr>
                        <tr>
                            <td class="lblb">
                                Employee Type
                            </td>
                            <td class="lbl">
                                <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="txt2" />
                            </td>
                            <td class="lblb">
                                Email
                            </td>
                            <td class="lbl">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txt2" />
                            </td>
                            <td class="lblb">
                                Cell
                            </td>
                            <td class="lbl">
                                <asp:TextBox ID="txtCellPhone" runat="server" CssClass="txt2" />
                            </td>
                        </tr>
                        <tr>
                            <td class="lblb">
                                First Name
                            </td>
                            <td class="lbl">
                                <asp:TextBox ID="txtFName" runat="server" CssClass="txt2" />
                            </td>
                            <td class="lblb">
                                Last Name
                            </td>
                            <td class="lbl">
                                <asp:TextBox ID="txtLName" runat="server" CssClass="txt2" />
                            </td>
                            <td class="lbl" colspan="2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvEmployeeDetails" runat="server" AutoGenerateColumns="false" GridLines="None"
                        Width="100%" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvEmployeeDetails_PageIndexChanging"
                        PagerStyle-CssClass="FooterContentDetails" PagerStyle-HorizontalAlign="Right"
                        EmptyDataText="No record found for this search criteria" EmptyDataRowStyle-CssClass="gvEmpty"
                        OnRowDataBound="gvEmployeeDetails_RowDataBound" DataKeyNames="EmployeeID">
                        <Columns>
                            <asp:BoundField HeaderText="Name" DataField="Name" HeaderStyle-CssClass="GridHeader"
                                ItemStyle-CssClass="GridContent" ItemStyle-Width="200px" />
                            <asp:TemplateField HeaderText="Employee Type" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmployeeType" runat="server" Text='<%#Eval("EmployeeType") %>' />
                                    <asp:HiddenField ID="hfEmpTypeID" runat="server" Value='<%#Eval("EmployeeTypeId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CellPhone" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                <ItemTemplate>
                                    <asp:Label ID="lblCellPhone" runat="server" Text='<%#Eval("CellPhone") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CC" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectCCto" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BCC" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectBCCto" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SMS" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectSMSto" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div style="padding: 5px">
                        <asp:Button ID="btnSaveCcBccSmsTo" runat="server" Text="Save" CssClass="btn" OnClick="btnSaveCcBccSmsTo_Click" />
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hfAddCC" runat="server" />
                <ajax:ModalPopupExtender ID="mpeAddCC" runat="server" BackgroundCssClass="modalBackground"
                    TargetControlID="hfAddCC" PopupControlID="pnlAddCC" CancelControlID="imgCloseAddCCPopUp">
                </ajax:ModalPopupExtender>
                <%--Add CC --%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
