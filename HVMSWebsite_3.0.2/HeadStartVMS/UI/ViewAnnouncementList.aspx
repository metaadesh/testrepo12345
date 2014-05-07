<%@ Page Language="C#" AutoEventWireup="True" 
    CodeBehind="ViewAnnouncementList.aspx.cs" Inherits="HeadStartVMS.UI.ViewNewAnnouncement" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphAddRight" runat="server">
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td class="TableHeadingBg TableHeading">
                            Announcement List
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder">
                            <asp:UpdatePanel ID="upAnnouncementList" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gvNewAnnouncementList" runat="server" AutoGenerateColumns="False"
                                        GridLines="None" PageSize="20" Width="100%" OnPageIndexChanging="gvnewAnnouncementList_PageIndexChanging"
                                        DataKeyNames="AnnouncementId" OnRowCancelingEdit="gvnewAnnouncementList_RowCancelingEdit"
                                        OnRowDeleting="gvnewAnnouncementList_RowDeleting" OnRowEditing="gvnewAnnouncementList_RowEditing"
                                        OnRowUpdating="gvnewAnnouncementList_RowUpdating" AllowPaging="True" OnSelectedIndexChanged="gvnewAnnouncementList_SelectedIndexChanged"
                                        OnRowDataBound="gvNewAnnouncementList_RowDataBound" OnSorting="gvNewAnnouncementList_Sorting"
                                        AllowSorting="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Title">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlTitle" runat="server" Text='<%# Eval("AnnouncementTitle") %>'
                                                        PostBackUrl='<%# "~/UI/AnnouncementDetails.aspx?AnnouncementId="+Eval("AnnouncementId") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div style="vertical-align: top">
                                                        <p>
                                                            <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("AnnouncementTitle") %>'></asp:TextBox></p>
                                                    </div>
                                                </EditItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" Height="15px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div style="vertical-align: top">
                                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                                            TextMode="MultiLine" Rows="2"></asp:TextBox></div>
                                                </EditItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" Height="15px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Announcement Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("AnnouncementType") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <div style="vertical-align: top">
                                                        <asp:DropDownList ID="ddlAnnouncementType" runat="server" DataTextField="AnnouncementType"
                                                            DataValueField="AnnouncementTypeID" DataSourceID="odsAnnouncementType" SelectedValue='<%# Eval("AnnouncementTypeID") %>'
                                                            CssClass="GridContent">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--<asp:TextBox ID="TextBox3" runat="server" 
                                                        Text='<%# Bind("AnnouncementType") %>'></asp:TextBox>--%>
                                                </EditItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" Wrap="False" />
                                                <ItemStyle CssClass="GridContent" Height="15px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date Added">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("DateAdded", "{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("DateAdded", "{0:d}") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" Wrap="False" />
                                                <ItemStyle CssClass="GridContent" Height="15px" HorizontalAlign="Center" VerticalAlign="Middle"
                                                    Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Added by">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle"
                                                    Wrap="False" />
                                                <ItemStyle CssClass="GridContent" Height="15px" HorizontalAlign="Left" VerticalAlign="Middle"
                                                    Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Image" CancelImageUrl="~/Images/Delete.gif" EditImageUrl="~/Images/edit-icon.jpg"
                                                UpdateImageUrl="~/Images/confirm.gif" DeleteImageUrl="~/Images/DeleteButton.jpg"
                                                ShowEditButton="True" UpdateText="Update" Visible="False">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" Wrap="False" />
                                            </asp:CommandField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnViewDetails" runat="server" CommandName="Select" ImageUrl="~/Images/edit-icon.jpg" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/DeleteButton.jpg" ShowDeleteButton="True">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" Wrap="False" />
                                            </asp:CommandField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                                        <EditRowStyle CssClass="GridContentEdit" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="FooterContentDetails" width="50%">
                            <asp:HyperLink ID="hplAddNewAnnouncement" CssClass="AddNewExpenseTxt" NavigateUrl="AddNewAnnouncement.aspx"
                                runat="server">Add New Announcement</asp:HyperLink>
                            <asp:ObjectDataSource ID="odsAnnouncementType" runat="server" TypeName="METAOPTION.BAL.LaneAssignmentBAL"
                                SelectMethod="GetAnnouncementType"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="500px" Style="display: none;">
                                <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                                        <ajax:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
                                            PopupControlID="pnlPopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                                        </ajax:ModalPopupExtender>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>
                                                <td class="PopUpBoxHeading" style="padding-left: 10px" nowrap>
                                                    Update Announcement
                                                </td>
                                                <td align="right" class="PopUpBoxHeading" style="padding-right: 5px">
                                                    <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnCancel.ClientID %>').click();" />
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="text-align: center">
                                            <br>
                                            <asp:DetailsView ID="dvAnnouncementEdit" runat="server" GridLines="None" DefaultMode="Edit"
                                                AutoGenerateRows="false" Visible="false" Width="96%" OnItemUpdating="dvAnnouncementEdit_ItemUpdating">
                                                <HeaderStyle BackColor="AliceBlue" />
                                                <Fields>
                                                    <asp:TemplateField HeaderText="Title">
                                                        <EditItemTemplate>
                                                            <div style="vertical-align: middle; text-align: left">
                                                                <asp:TextBox ID="txtAnnouncementTitle" CssClass="txtMan2" runat="server" Text='<%# Bind("AnnouncementTitle") %>'
                                                                    Width="380px" />
                                                                <asp:RequiredFieldValidator ID="rfvAnnouncementTitle" runat="server" ControlToValidate="txtAnnouncementTitle"
                                                                    ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" /></div>
                                                        </EditItemTemplate>
                                                        <HeaderStyle CssClass="TableBorder" Font-Bold="True" />
                                                        <ItemStyle CssClass="TableBorder" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <EditItemTemplate>
                                                            <div style="vertical-align: middle; text-align: left">
                                                                <asp:TextBox ID="txtDescription" CssClass="txtMan2" runat="server" Text='<%# Bind("Description") %>'
                                                                    Rows="4" TextMode="MultiLine" Width="380px" />
                                                                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                                                                    ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" /></div>
                                                        </EditItemTemplate>
                                                        <HeaderStyle CssClass="TableBorder" Font-Bold="True" />
                                                        <ItemStyle CssClass="TableBorder" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <EditItemTemplate>
                                                            <div style="vertical-align: middle; text-align: left">
                                                                <asp:DropDownList ID="ddlAnnouncement1Type" runat="server" DataTextField="AnnouncementType"
                                                                    Enabled="false" DataValueField="AnnouncementTypeID" DataSourceID="odsAnnouncementType"
                                                                    SelectedValue='<%# Eval("AnnouncementTypeID") %>' CssClass="GridContent">
                                                                </asp:DropDownList>
                                                                <div style="display: none">
                                                                    <asp:TextBox ID="txtAnnouncementId" runat="server" Text='<%# Bind("AnnouncementId") %>' /></div>
                                                            </div>
                                                        </EditItemTemplate>
                                                        <HeaderStyle CssClass="TableBorder" Font-Bold="True" />
                                                        <ItemStyle CssClass="TableBorder" />
                                                    </asp:TemplateField>
                                                </Fields>
                                                <EditRowStyle />
                                            </asp:DetailsView>
                                        </div>
                                        <div>
                                            <div style="height: 8px">
                                            </div>
                                            <div style="text-align: center">
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Btn_Form" />
                                                <%--<asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/confirm.gif" OnClick="imgSave_Click"
                                                    ToolTip="Update" />--%>&nbsp;&nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Save"
                                                        OnClick="btnUpdate_Click" CssClass="Btn_Form" />
                                                <%--<asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Images/Delete.gif" ToolTip="Cancel" />--%>
                                            </div>
                                            <div style="height: 8px">
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
