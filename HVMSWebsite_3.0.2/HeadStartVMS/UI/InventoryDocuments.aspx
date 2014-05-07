<%@ Page Language="C#"  AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="InventoryDocuments.aspx.cs" Inherits="METAOPTION.UI.InventoryDocuments"
    Title="HeadstartVMS::Inventory Documents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfInventoryID" runat="server" />
    <asp:UpdatePanel ID="updDocument" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
        <Triggers>
            <asp:PostBackTrigger ControlID="gvDocuments" />
        </Triggers>
        <ContentTemplate>
            <div style="text-align: center">
                <asp:Label ID="lblErr" runat="server" CssClass="err" Visible="false" />
            </div>
            <div class="AddHeading">
                <asp:Label ID="lblInventoryHeader" runat="server" Text=""></asp:Label>
            </div>
            <asp:GridView runat="server" Width="100%" ID="gvDocuments" AutoGenerateColumns="false"
                DataKeyNames="DocumentId" AllowPaging="true" PageSize="20" EmptyDataText="No Rows found"
                OnPageIndexChanging="gvDocuments_PageIndexChanging" OnRowCommand="gvDocuments_RowCommand"
                OnRowDataBound="gvDocuments_RowDataBound" EnableSortingAndPagingCallbacks="false">
                <%----%>
                <RowStyle CssClass="gvRow" />
                <Columns>
                    <asp:TemplateField HeaderText="Document Type" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                        <ItemTemplate>
                            <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("DocumentType") %>' />
                            <asp:HiddenField ID="hfDocumentTypeId" runat="server" Value='<%# Eval("DocumentTypeId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-Wrap="false"
                        HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Wrap="false"
                        HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                        <HeaderStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Document Name" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent">
                        <ItemTemplate>
                            <%#Eval("DocumentName")%>
                            <%--<asp:UpdatePanel ID="upDocLoader" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkOpenDocument" runat="server" CausesValidation="false" Text='<%# Eval("DocumentName") %>'
                                        CommandArgument='<%# Eval("DocumentId") %>' />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lnkOpenDocument" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Added By" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContent">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAddedBy" Text='<%# string.Format("{0} on {1}", Eval("AddedBy"),  Eval("DateAdded", "{0:MMM dd, yyyy hh:mm tt}")) %>' />
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Action" HeaderStyle-CssClass="GridHeader"
                        ItemStyle-CssClass="GridContentCentre">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnFileFormat" runat="server" Value=' <%# Eval("FileType")%>' />
                            <asp:ImageButton ID="imgBtnEditDoc" ImageUrl="~/Images/edit-icon.jpg" runat="server"
                                OnClick="imgBtnEditDoc_Click" ToolTip='<%#Eval("ModifiedBy")%>' />
                            <asp:ImageButton ID="btnImgDocument" runat="server" OnClick="btnImgDocument_Click"
                                ImageUrl="../Images/view_edit.png" ToolTip="Document Display" />
                            <asp:ImageButton ID="imgDownload" runat="server" ImageUrl="../Images/download.png"
                                ToolTip="Download File" Style="padding-bottom: 4px" OnClick="imgDownload_Click" />
                            <asp:ImageButton ID="imgBtnDelDoc" ImageUrl="~/Images/DeleteButton.jpg" runat="server"
                                OnClientClick="javascript:return confirm('Are u sure you want to delete this Document?\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                                OnClick="imgBtnDelDoc_Click" Style="padding-bottom: 2px" />
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="gvHeading" />
                <AlternatingRowStyle CssClass="gvAlternateRow" />
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
            </asp:GridView>
            <table border="0" cellpadding="0" style="width: 99%; padding-left: 10px;">
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkbtnBack" runat="server" OnClick="lnkbtnBack_Click" CausesValidation="false">
                     <img src="../Images/back.jpg" alt="back" style="border:none; padding-top:10px" />
                        </asp:LinkButton>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnScan" runat="server" Text="Scan and Upload Document" Style="margin-top: 12px"
                            CssClass="btn" OnClick="btnScan_Click" />
                        &nbsp;
                        <asp:LinkButton ID="lnkAddnewDocument" runat="server" CssClass="AddNewExpenseTxt"
                            OnClick="lnkAddnewDocument_Click">
                     <img src="../Images/AddNew.gif" alt="Add New" style="border:none; padding-top:10px" /> 
                     Browse and Upload Document
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
            <table id="tblInventoryDocs" style="display: none;" runat="server" class="modalPopup"
                border="0" cellspacing="0" cellpadding="0" width="550px">
                <tr>
                    <td class="PopUpBoxHeading" colspan="3">
                        &nbsp;&nbsp;<asp:Label ID="lblHeading" Text="Add Inventory Document" runat="server"></asp:Label>
                    </td>
                    <td class="PopUpBoxHeading" align="right">
                        <img border="0" src="../Images/close.gif" onclick="$find('mdpopDoc').hide();return false;"
                            alt="" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder" style="width: 18%;" nowrap>
                        Document Type
                    </td>
                    <td class="TableBorder" style="width: 37%">
                        <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="txt2" />
                    </td>
                    <td class="TableBorder" style="width: 15%">
                        Title
                    </td>
                    <td class="TableBorder" style="width: 30%">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="txt2" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Description
                    </td>
                    <td class="TableBorder" colspan="3">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="txtMulti" Rows="3" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr id="trUploadRow" runat="server">
                    <td class="TableBorder">
                        Select File
                    </td>
                    <td colspan="3" class="TableBorder">
                        <asp:FileUpload ID="fuUpload" runat="server" Width="350px" />
                        <div class="err">
                            Note: File size must be less than 2MB!</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnCancel" runat="server" Text="  Cancel  " CssClass="btn" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUpload" runat="server" Text="  Upload  " CssClass="btn" OnClick="btnUpload_Click" />
                        <asp:Button ID="btnUpdateDoc" runat="server" Text="  Save    " CssClass="btn" OnClick="btnUpdateDoc_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="display: none; width: 1px;">
                            <asp:Button ID="btnDocpopupOpener" runat="server" />
                            <asp:HiddenField ID="hdUpdateDocId" runat="server" />
                        </div>
                        <ajax:ModalPopupExtender ID="MPEDocs" BehaviorID="mdpopDoc" runat="server" TargetControlID="btnDocpopupOpener"
                            PopupControlID="tblInventoryDocs" BackgroundCssClass="modalBackground" DropShadow="true"
                            CancelControlID="btnCancel" />
                    </td>
                </tr>
            </table>
            <div id="divScan" class="modalPopup" style="display: none; min-width: 400px; min-height: 200px"
                runat="server">
                <div>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="PopUpBoxHeading">
                                &nbsp;&nbsp;
                                <asp:Label ID="lblScan" runat="server"></asp:Label>
                            </td>
                            <td class="PopUpBoxHeading" align="right" style="padding-right: 5px">
                                <img border="0" src="../Images/close.gif" id="imgCloseScanPopUp" alt="close" />
                            </td>
                        </tr>
                    </table>
                    <iframe id="frmScanner" runat="server" scrolling="yes" style="height: 680px; width: 880px;
                        overflow: auto" frameborder="0"></iframe>
                </div>
            </div>
            <asp:HiddenField ID="hfScan" runat="server" />
            <ajax:ModalPopupExtender ID="mpeScan" runat="server" BackgroundCssClass="modalBackground"
                TargetControlID="hfScan" PopupControlID="divScan" CancelControlID="imgCloseScanPopUp"
                BehaviorID="ScanModalPopup11">
            </ajax:ModalPopupExtender>
            <div style="width: 100%; height: auto">
                <asp:HiddenField ID="hfPopup" runat="server" />
                <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfPopup"
                    PopupControlID="panOpen" BackgroundCssClass="modalBackground" CancelControlID="imgCancel"
                    PopupDragHandleControlID="panOpen" BehaviorID="ImageModelPopup" />
                <asp:Panel ID="panOpen" runat="server" Height="680px" Width="890px" CssClass="imagevideobox"
                    Style="background: white;">
                    <div style="position: absolute; text-align: right; padding-right: 10px; width: 885px;">
                        <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" />
                    </div>
                    <iframe id="ifrmSlideShow" runat="server" scrolling="no" style="height: 780px; width: 890px;
                        margin-top: 10px;" frameborder="0"></iframe>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function HideDocumentpopup() {
            $find('ScanModalPopup11').hide();
            return true;
        }
    </script>
</asp:Content>
