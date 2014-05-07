<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InventoryDriver.aspx.cs"
    Inherits="METAOPTION.UI.InvDrivers" Title="Inventory Driver" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnlDriver" runat="server">
        <ContentTemplate>
            <div class="AddHeading">
                <asp:Label ID="lblInventoryHeader" runat="server" Text=""></asp:Label>
            </div>
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                <tr>
                    <td align="left">
                        <asp:GridView ID="gvDriver" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EmptyDataText="No Rows found" OnPageIndexChanging="gvDriver_PageIndexChanging"
                            PageSize="20" Width="100%" DataKeyNames="InventoryDriverId">
                            <Columns>
                                <asp:BoundField DataField="DriverName" ConvertEmptyStringToNull="true" HeaderText="Driver"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                <asp:BoundField DataField="StartLocation" ConvertEmptyStringToNull="true" HeaderText="Start Location"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EndLocation" ConvertEmptyStringToNull="true" HeaderText="End Location"
                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StartLocationDate" ConvertEmptyStringToNull="true" DataFormatString="{0:MM/dd/yyyy}"
                                    HeaderText="Start Date" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EndLocationDate" ConvertEmptyStringToNull="true" DataFormatString="{0:MM/dd/yyyy}"
                                    HeaderText="End Date" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <ItemStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtnDriverEdit" runat="server" ImageUrl="~/Images/edit-icon.jpg"
                                            OnClick="ibtnDriverEdit_Click" ToolTip='EDIT DRIVER DETAILS' />
                                        <asp:ImageButton ID="ibtnDriverDelete" runat="server" ImageUrl="~/Images/DeleteButton.jpg"
                                            OnClientClick="javascript:return confirm('Are u sure you want to delete this driver ?\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                                            OnClick="ibtnDriverDelete_Click" />
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
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
                        <table border="0" cellpadding="0" cellpadding="0" style="width: 98%; padding-left: 10px;">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkbtnBack" runat="server" OnClick="lnkbtnBack_Click" CausesValidation="false">
                     <img src="../Images/back.jpg" alt="back" style="border:none; padding-top:10px" />
                                    </asp:LinkButton>
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="lnkAddNewDriver" runat="server" CssClass="AddNewExpenseTxt" OnClick="lnkAddNewDriver_Click">
                     <img src="../Images/AddNew.gif"  alt="Add New" style="border:none; padding-top:10px" /> 
                                    Add New Driver
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblDriver" width="80%" style="display: none;" runat="server" class="modalPopup"
                            border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="PopUpBoxHeading">
                                    &nbsp;&nbsp;<asp:Label ID="lblHeading" Text="Add Inventory Driver" runat="server"></asp:Label>
                                </td>
                                <td class="PopUpBoxHeading" align="right">
                                    <img id="imgCloseAddDriver" border="0" src="../Images/close.gif" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" style="padding: 10px">
                                    <table border="0" cellspacing="0" cellpadding="0" style="border-collapse: collapse;
                                        width: 100%;" class="Nornmal-Arial-12">
                                        <tr id="trDrivers" runat="server">
                                            <td nowrap="nowrap" class="GridContent_padding5">
                                                <b>Driver</b>
                                            </td>
                                            <td class="GridContent_padding5">
                                                <asp:DropDownList ID="ddlDrivers" runat="server" DataTextField="DriverName" DataValueField="DriverID"
                                                    DataSourceID="objDrivers" CssClass="txtMan2">
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="objDrivers" runat="server" SelectMethod="GetDriverList"
                                                    TypeName="METAOPTION.BAL.InventoryBAL">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="OrgID" SessionField="OrgID" DbType="Int16" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr id="trDriverName" runat="server">
                                            <td nowrap="nowrap" class="GridContent_padding5">
                                                <b>Driver Name</b>
                                            </td>
                                            <td class="GridContent_padding5">
                                                <asp:Label ID="lblInvDriverName" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" class="GridContent_padding5">
                                                <b>Start Location</b>
                                            </td>
                                            <td class="GridContent_padding5">
                                                <asp:TextBox ID="txtStartLocation" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" class="GridContent_padding5">
                                                <b>End Location</b>
                                            </td>
                                            <td class="GridContent_padding5">
                                                <asp:TextBox ID="txtEndLocation" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" class="GridContent_padding5">
                                                <b>Start Date</b>
                                            </td>
                                            <td class="GridContent_padding5">
                                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate"
                                                    PopupButtonID="imgStartDate">
                                                </ajax:CalendarExtender>
                                                <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                                    Style="cursor: pointer;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap="nowrap" class="GridContent_padding5">
                                                <b>End Date</b>
                                            </td>
                                            <td class="GridContent_padding5">
                                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                <ajax:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate"
                                                    PopupButtonID="imgEndDate">
                                                </ajax:CalendarExtender>
                                                <asp:Image ID="imgEndDate" runat="server" ImageUrl="~/Images/calender-icon.gif" Style="cursor: pointer;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnDriverCancel" runat="server" Text="Cancel" CssClass="Btn_Form"
                                                    Width="75px" OnClientClick="javascript:$find('mdpopDriver').hide();return false;" />
                                                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnSaveDriver" runat="server" Text="Save" CssClass="Btn_Form"
                                                    Width="75px" OnClick="btnSaveDriver_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div style="display: none; width: 1px;">
                <asp:Button ID="btnDriverPopupOpener" runat="server" />
                <asp:HiddenField ID="hdUpdateDriverId" runat="server" />
            </div>
            <ajax:ModalPopupExtender ID="MPEAddDriver" BehaviorID="mdpopDriver" runat="server"
                TargetControlID="btnDriverPopupOpener" PopupControlID="tblDriver" BackgroundCssClass="modalBackground"
                DropShadow="true" CancelControlID="imgCloseAddDriver" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
