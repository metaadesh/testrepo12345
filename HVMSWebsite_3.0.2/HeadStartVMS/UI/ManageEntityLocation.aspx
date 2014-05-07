<%@ Page Title="Manage Entity Location" Language="C#" AutoEventWireup="true" CodeBehind="ManageEntityLocation.aspx.cs"
    Inherits="METAOPTION.UI.ManageEntityLocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upLocation" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfEntityTypeID" runat="server" Value="0" />
            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                <tr>
                    <td class="AddHeading">
                        Add Entity Location
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <fieldset class="ForFieldSet">
                            <legend class="ForLegend">Make a New Location</legend>
                            <br />
                            <asp:Label ID="lblError" runat="server" Text="" Style="color: Red; font-weight: bold;"></asp:Label>
                            <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse">
                                <tr>
                                    <td class="GridContent_padding5">
                                        <b>Location Type</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <asp:DropDownList ID="ddlLocationType" class="FormItem" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlLocationType_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="Select One " Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="18%" class="GridContent_padding5">
                                        <b>
                                            <asp:Label ID="lblLocationCode" runat="server" Text="Location Code"></asp:Label>
                                        </b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:TextBox ID="txtLocationCode" runat="server" class="FormItem"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="18%" class="GridContent_padding5">
                                        <b>
                                            <asp:Label ID="lblLocationDesc" runat="server" Text="Location Desc"></asp:Label>
                                        </b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                        <asp:TextBox ID="txtLocationDesc" runat="server" class="FormItem" TextMode="MultiLine"
                                            Rows="2" Width="450px" />
                                    </td>
                                </tr>
                                <tr id="trSelectEntity" runat="server" visible="false">
                                    <td width="18%" class="GridContent_padding5">
                                        <b>Select Entity</b>
                                    </td>
                                    <td class="GridContent_padding5" width="79%">
                                       <asp:HiddenField ID="hdnSelectedEntityID" runat="server" Value="0" />
                                      <asp:Label ID="lblSelectedEntityName" runat="server" Text=""></asp:Label>
                                      <asp:LinkButton ID="lnkSelect" runat="server" class="GridContent_Link">Select</asp:LinkButton>
                                      <ajax:ModalPopupExtender ID="mpeSelectRecipient" runat="server" TargetControlID="lnkSelect"
                                            PopupControlID="pnlSelectRecipient" BackgroundCssClass="modalBackground" DropShadow="False"
                                            CancelControlID="btnCancel" PopupDragHandleControlID="pnlHeader">
                                        </ajax:ModalPopupExtender>
                                        <asp:Panel runat="server" ID="pnlSelectRecipient" Style="display: none; background: #f0f0f0;">
                                            <table border="0" width="800" cellpadding="0" class="PopUpBox">
                                                <tr>
                                                    <td class="PopUpBoxHeading">
                                                        <asp:Label ID="lblEntityType" runat="server" Text="Entity(s)"></asp:Label>
                                                    </td>
                                                    <td class="PopUpBoxHeading" align="right">
                                                        <asp:ImageButton ID="btnClosePopup" runat="server" CausesValidation="false" ImageUrl="~/Images/close.gif"
                                                            Style="margin-right: 3px; margin-bottom: 4px;" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2" style="padding: 10px">
                                                        <table class="arial-12" cellpadding="0" border="0" width="100%" style="border-collapse: collapse;
                                                            margin-bottom: 20px;">
                                                            <tr>
                                                                <td class="GridContent_padding5">
                                                                    <div style="padding-left: 10px;">
                                                                        <b>Search: </b>
                                                                        <asp:DropDownList ID="ddlSearchField" runat="server" class="FormItem">
                                                                            <asp:ListItem Value="recipientname" Text="Name" Selected="True"> </asp:ListItem>
                                                                            <asp:ListItem Value="Street" Text="Street"> </asp:ListItem>
                                                                            <asp:ListItem Value="City" Text="City"> </asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:DropDownList ID="ddlSearchOperator" runat="server" class="FormItem">
                                                                            <asp:ListItem Value="begins with">Begins With</asp:ListItem>
                                                                            <asp:ListItem Value="ends with">Ends With</asp:ListItem>
                                                                            <asp:ListItem Value="contains">Contains</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txtSearchString" runat="server" class="FormItem"></asp:TextBox>
                                                                        <asp:Button ID="btnFilter" runat="server" Text="Filter" class="Btn_Form filterbtn"
                                                                            CausesValidation="false" OnClick="btnFilter_Click" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:GridView ID="grvRecipient" runat="server" DataKeyNames="recipientid" AutoGenerateColumns="false"
                                                            ShowHeader="true" AllowPaging="true" AllowSorting="true" PageSize="15" Width="100%"
                                                            EmptyDataText="No record found." EmptyDataRowStyle-CssClass="GridEmptyRow" CssClass="TableBorder"
                                                            OnPageIndexChanging="grvRecipient_PageIndexChanging" OnSorting="grvRecipient_OnSorting"
                                                            PagerSettings-Mode="NumericFirstLast">
                                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="25px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                                    <ItemTemplate>
                                                                        <mo:GroupRadioButton ID="selectedRadioButton" runat="server" GroupName="recipients" />
                                                                        <asp:HiddenField ID="hdnAccCode" runat="server" Value='<%# Eval("AccountingCode")%>' />
                                                                  </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:BoundField DataField="recipienttype" HeaderText="Recipient Type" Visible="false"
                                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="AccountingCode" SortExpression="AccountingCode" HeaderText="Acc. Code"
                                                                    HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="recipientname" SortExpression="recipientname" HeaderText="Name"
                                                                    HeaderStyle-Width="200px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Street" SortExpression="Street" HeaderText="Street" HeaderStyle-CssClass="GridHeader"
                                                                    ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="City" SortExpression="City" HeaderText="City" HeaderStyle-Width="80px"
                                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="State" SortExpression="State" HeaderText="State" HeaderStyle-Width="80px"
                                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"></asp:BoundField>
                                                                <asp:BoundField DataField="CountryCode" SortExpression="CountryCode" HeaderText="Country"
                                                                    HeaderStyle-Width="50px" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                                </asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="padding: 10px" align="center">
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="Btn_Form" CausesValidation="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnSelectRecipient" runat="server" Text="Select" CausesValidation="false"
                                                            class="Btn_Form" OnClick="btnSelectRecipient_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
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
                        <asp:Button ID="btnAddLocation" runat="server" Text="Add Location" class="Btn_Form"
                            OnClientClick="return ValidateLocationType();" OnClick="btnAddLocation_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="height30">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                function ShowMsg(Message)
                { alert(Message); }

                function ValidateLocationType() {
                    var locType = document.getElementById("ctl00_ContentPlaceHolder1_ddlLocationType").value;
                    if (locType == "0") {
                        alert("Please select a Location Type");
                        return false;
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
