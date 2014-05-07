<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Admin_MasterLeftPanel.Master"
    CodeBehind="Admin_AddNewGroup.aspx.cs" Inherits="METAOPTION.UI.Admin_AddNewGroup"
    Title="Admin Panel :: Add New Group" %>

<asp:Content ID="contAddNewGroup" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="AddHeading">
        Group Management</div>
    <div>
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Add New Group</legend>
            <br />
            <table border="0" style="border-collapse: collapse" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="TableBorderB">
                        <asp:Label ID="lblOrg" runat="server" AssociatedControlID="txtGName" Text="Organization" />
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlOrganization" runat="server" CssClass="txt2" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        <asp:Label ID="lblGName" runat="server" AssociatedControlID="txtGName" Text="Group Name" />
                    </td>
                    <td class="TableBorder" style="white-space: nowrap;">
                        <asp:TextBox ID="txtGName" runat="server" CssClass="txtMan3" />
                        <asp:RequiredFieldValidator ID="rfvGName" runat="server" ErrorMessage="*" ControlToValidate="txtGName"
                            Font-Bold="true" Font-Size="12" ForeColor="Red" SetFocusOnError="true" ValidationGroup="insertG"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        <asp:Label ID="lblGDesc" runat="server" AssociatedControlID="txtGDesc" Text="Description" />
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtGDesc" runat="server" TextMode="MultiLine" CssClass="txtMulti"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder" colspan="2" align="center">
                        <asp:Button ID="btnCancel" runat="server" Text="<< Back" CssClass="btn" OnClick="btnCancel_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Text="Insert" OnClick="btnSubmit_Click"
                            CssClass="btn" ValidationGroup="insertG" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                            ValidationGroup="insertG" OnClientClick="return validatePage();" CssClass="btn" />
                    </td>
                </tr>
            </table>
            <br />
        </fieldset>
    </div>
    <div>
        <fieldset id="fsetRights" runat="server" class="ForFieldSet">
            <legend class="ForLegend">Associated Rights</legend>
            <br />
            <table style="width: 100%" align="left" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="left" colspan="2" style="width: 80%">
                        <asp:UpdatePanel ID="upPanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvAssociatedRights" runat="server" AutoGenerateColumns="False"
                                    GridLines="None" Width="100%" AllowPaging="true" PageSize="20" EmptyDataText="No right is associated with this group."
                                    OnRowCommand="gvAssociatedRights_RowCommand" OnPageIndexChanging="gvAssociatedRights_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgbtnDelRight" runat="server" CommandArgument='<% #Eval("SecurityGroupRightId") %>'
                                                    CommandName="DeleteRight" ImageUrl="~/Images/DeleteButton.jpg" />
                                                <asp:Panel Height="100" Style="display: none;" Width="300" CssClass="modalPopup"
                                                    ID="Panel1" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="GridHeader">
                                                                Confirmation
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="LeftPanelContentHeading" align="center">
                                                                Are You sure? You want to delete this item.
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Button ID="btnOk" runat="server" CssClass="Btn_Form" Text="  Ok  " />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnCancel" runat="server" CssClass="Btn_Form" Text="Cancel" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <ajax:ConfirmButtonExtender ID="confrm" runat="server" TargetControlID="ImgbtnDelRight"
                                                    DisplayModalPopupID="mod" OnClientCancel="btnCancel">
                                                </ajax:ConfirmButtonExtender>
                                                <ajax:ModalPopupExtender ID="mod" runat="server" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
                                                    OkControlID="btnOk" CancelControlID="btnCancel" TargetControlID="ImgbtnDelRight">
                                                </ajax:ModalPopupExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RightName" HeaderText="Right Name" />
                                        <asp:BoundField DataField="RightDesc" HeaderText="Description" />
                                    </Columns>
                                    <HeaderStyle CssClass="gvHeading" />
                                    <RowStyle CssClass="gvRow" />
                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                </asp:GridView>
                                <div id="divRights" runat="server" style="max-height: 450px; background-color: White">
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td class="PopUpBoxHeading" colspan="2" style="padding-left: 5px">
                                                Add Right
                                            </td>
                                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                                <img border="0" src="../Images/close.gif" onclick="$find('mdpopDoc').hide();return false;"
                                                    alt="" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Right Initial
                                            </td>
                                            <td class="TableBorder">
                                                <asp:TextBox ID="txtRightName" runat="server" CssClass="txt3" />
                                            </td>
                                            <td class="TableBorder">
                                                <asp:Button ID="btnSearchRight" runat="server" CssClass="btn" Text="Search Right"
                                                    OnClick="btnSearchRight_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder" colspan="3">
                                                <asp:GridView ID="gvRights" runat="server" Width="100%" AllowPaging="true" PageSize="10"
                                                    DataKeyNames="SecurityRightId" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                                    OnPageIndexChanging="gvRights_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ADD" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel ID="upAddrightsOK" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="ibtnOk" ImageUrl="~/Images/confirm.gif" runat="server" OnClick="ibtnOk_Click" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="ibtnOk" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RightName" HeaderText="Right Name" />
                                                        <asp:BoundField DataField="RightDesc" HeaderText="Description" />
                                                    </Columns>
                                                    <RowStyle CssClass="gvRow" />
                                                    <AlternatingRowStyle CssClass="gvAlternateRow" />
                                                    <HeaderStyle CssClass="gvHeading" />
                                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <asp:Button ID="btnCancelRight" runat="server" Text="Cancel" CssClass="btn" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="hideCol">
                                    <asp:Button ID="btnRightOpenner" runat="server" />
                                    <ajax:ModalPopupExtender ID="mpeOpenRights" runat="server" TargetControlID="btnRightOpenner"
                                        PopupControlID="divRights" BehaviorID="mdpopDoc" BackgroundCssClass="modalBackground"
                                        DropShadow="false" CancelControlID="btnCancelRight" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAddRight" runat="server" Text="Add Right" CssClass="btn" OnClick="btnAddRight_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <script type="text/javascript">
        function validatePage() {
            var grpName = $('#<%=txtGName.ClientID %>');
            if (grpName.val() == '') {
                grpName.css("background-color", "lightyellow");
                alert("Please provide the input");
                return false;
            }
            else
                return true;

        }
    </script>
</asp:Content>
