<%@ Page Language="C#" AutoEventWireup="true" 
    CodeBehind="AddNewAnnouncement.aspx.cs" Inherits="HeadStartVMS.UI.AddNewAnnouncement" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphAddRight" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                <tr>
                    <td align="left">
                        <fieldset class="ForFieldSet">
                            <legend class="ForLegend">Add New Announcement</legend>&nbsp;<table border="0" cellpadding="0"
                                cellspacing="0" style="border-collapse: collapse" width="100%">
                                <tr>
                                    <td class="GridContent_padding5" width="18%">
                                        <b>Title</b><span style="color: Red"> *</span>
                                    </td>
                                    <td class="GridContent_padding5" width="79%" valign="top">
                                        &nbsp;<asp:TextBox ID="txtTitle" runat="server" CssClass="FormItem" Style="width: 300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                            Display="Dynamic" ErrorMessage="Required Field!"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5" valign="top" width="18%">
                                        <b>Description</b><span style="color: Red"> *</span>
                                    </td>
                                    <td class="GridContent_padding5" width="79%" valign="top">
                                        &nbsp;<asp:TextBox ID="txtDescription" runat="server" CssClass="FormItem" Rows="2" Style="width: 298px;
                                            height: 67px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                                            Display="Dynamic" ErrorMessage="Required Field!"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5">
                                        &nbsp;<asp:CheckBox ID="chkSendEmail" runat="server" 
                                            Text="&nbsp;Send Email&nbsp;" />
                                    </td>
                                    <td class="GridContent_padding5">
                                        &nbsp;<asp:DropDownList ID="ddlEmailOption" runat="server" CssClass="FormItem" Style="width: 300px">
                                            <asp:ListItem Selected="True" Value="0">Send to all</asp:ListItem>
                                            <asp:ListItem Value="1">Send to selected users</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5" valign="top">
                                        <b>Emails</b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        &nbsp;<asp:ListBox ID="lstEmployeeList" runat="server" CssClass="FormItem" multiple=""
                                            SelectionMode="Multiple" Style="width: 300px"></asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td class="height30">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSaveAnnouncement" class="Btn_Form" runat="server" Text="Save Announcement"
                            OnClick="btnSaveAnnouncement_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="height30">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
