<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPage.Master"
    EnableEventValidation="false" CodeBehind="ViewDealer.aspx.cs" Inherits="METAOPTION.UI.ViewDealer" %>

<%@ Register Src="../UserControls/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/EditPreference.ascx" TagName="EditPreference" TagPrefix="uc2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<script type="text/C#" runat="server">
    protected void Refresh_Click(object sender, EventArgs args)
    {
        //  update the grids contents
        this.BindContactDetails();

        // Response.Redirect("ViewDealer.aspx?EntityId=" + EntityId + "&type=" + type);
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hWelcomeTasks" runat="server" />
    <script type="text/javascript">
        function updated() {
            alert('hello');
            //  close the popup
            tb_remove();

            //  refresh the update panel so we can view the changes  
            $('#<%= this.btnRefreshCustomers.ClientID %>').click();
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox');
            }
        }
    
    </script>
    <asp:HiddenField ID="hfReferrerURL" runat="server" />
    <div class="RightPanel">
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td colspan="2">
                    <%-- <asp:UpdatePanel ID="upDealerDetails" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                           
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnDelete" runat="server" Visible="false" CssClass="Btn_Form" Text="Delete Purchased From/Sold To"
                                    OnClientClick="javascript:return confirm('Are you sure you want to delete this Purchased From/Sold To? You will not be able to access this Buyer once deleted\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                                    OnClick="btnDelete_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; padding: 10px 10px 10px 0px; font-size: 12px; font-weight: bold">
                                <asp:Button ID="btnBack" CausesValidation="false" Text="<< Back" class="Btn_Form"
                                    runat="server" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TableHeadingBg">
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="TableHeading">
                                            Purchased From/Sold To Details
                                        </td>
                                        <td align="right" class="HeadingEditButton">
                                            <img border="0" src="../Images/Deleteblue.png" id="imgDelete" alt="close" onclick="Delete_ArchiveDealer('0');"
                                                runat="server" />
                                            <img border="0" src="../Images/Deletered.png" id="imgUnDelete" alt="close" onclick="Delete_ArchiveDealer('1');"
                                                runat="server" />
                                            <img border="0" src="../Images/Archiveblue.png" id="imgArchive" alt="close" onclick="Delete_ArchiveDealer('2');"
                                                runat="server" />
                                            <img border="0" src="../Images/ArchiveRed.png" id="imgUnArchive" alt="close" onclick="Delete_ArchiveDealer('-1');"
                                                runat="server" />
                                            <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif"
                                                Style="padding-right: 5px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:FormView ID="frmviewDealer" runat="server" Width="100%" OnDataBound="frmviewDealer_DataBound">
                                    <ItemTemplate>
                                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                            class="arial-12">
                                            <tr>
                                                <td class="TableBorder" style="width: 150px">
                                                    <b>Dealership Name</b>
                                                </td>
                                                <td class="TableBorder" colspan="3">
                                                    <%#Eval("DealerName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Dealer ID (Optional)</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("DealerDIN")%>
                                                </td>
                                                <td class="TableBorder" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Address</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Street")%>
                                                    <%#Eval("Suite")%>
                                                </td>
                                                <td class="TableBorder" style="width: 120px">
                                                    <b>Auction Access#</b>
                                                </td>
                                                <td class="TableBorder" style="width: 150px">
                                                    <%#Eval("AuctionAccessNo")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>City</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("City")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Dealer Type</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("DealerType")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>State/Zip</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("State")%>
                                                    -
                                                    <%#Eval("Zip")%>
                                                </td>
                                                <td class="TableBorder" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Country</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("CountryName")%>
                                                </td>
                                                <td class="TableBorder" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Phone</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <span style="font-size: 11px">
                                                        <%#Eval("Phone1")%></span>
                                                </td>
                                                <td class="TableBorder" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Secondary Phone</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <span style="font-size: 11px">
                                                        <%#Eval("Phone2")%></span>
                                                </td>
                                                <td class="TableBorder" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Fax</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Fax")%>
                                                </td>
                                                <td class="TableBorder" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Email</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Email1")%>
                                                </td>
                                                <td class="TableBorder" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Welcome Task</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("WelcomeTask")%>
                                                </td>
                                                <td class="TableBorder" colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Comments</b>
                                                </td>
                                                <td class="TableBorder" colspan="3">
                                                    <%#Eval("Comment")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="FooterContentDetails" colspan="2">
                                                    <%# Eval("AddedByText")%>
                                                </td>
                                                <td class="FooterContentDetails" colspan="2">
                                                    <%# Eval("ModifiedByText")%>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hfIsDealerExists" Value='<%# Eval("IsExists") %>' runat="server" />
                                        <asp:HiddenField ID="hfIsActive" Value='<%# Eval("IsActive") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:FormView>
                                <asp:HiddenField ID="hfDealerid" runat="server" />
                                <asp:ObjectDataSource ID="objDealerDetails" runat="server" SelectMethod="GetDealerDetails"
                                    TypeName="METAOPTION.BAL.DealerCustomerBAL">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" Type="Int64" />
                                        <asp:SessionParameter Name="OrgID" SessionField="OrgID" Type="Int16" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:Panel ID="pnlEditDealerDetails" Style="display: none;" Width="700px" runat="server"
                                    CssClass="modalPopup">
                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="PopUpBoxHeading">
                                                &nbsp;&nbsp; Edit Purchased From/Sold To Details
                                            </td>
                                            <td class="PopUpBoxHeading" align="right">
                                                <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="$find('mdpop').hide();return false;"
                                                    ImageUrl="../Images/close.gif" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2" style="padding: 10px">
                                                <asp:FormView ID="frmviewEditDealer" runat="server" Width="100%" OnDataBound="frmviewEditDealer_DataBound">
                                                    <ItemTemplate>
                                                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Dealer Name
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtDealerName" runat="server" CssClass="txtMan2" Text='<%#Eval("DealerName")%>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvDealerName" runat="server" ControlToValidate="txtDealerName"
                                                                        ErrorMessage="*" SetFocusOnError="True" />
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Dealer DIN
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtDealerDIN" Text='<%#Eval("DealerDIN")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Dealer Type
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="txtMan2" DataSourceID="objDealerType"
                                                                        DataTextField="DealerType1" DataValueField="DealerTypeId" SelectedValue='<%#Eval("DealerTypeId")%>'>
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:ObjectDataSource ID="objDealerType" runat="server" SelectMethod="GetDealerType"
                                                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                </td>
                                                                <td class="TableBorderB" rowspan="2" valign="top">
                                                                    Franchise Make(s)
                                                                </td>
                                                                <td class="TableBorder" rowspan="2">
                                                                    <asp:ListBox ID="lstMake" runat="server" CssClass="txtMan2" SelectionMode="Multiple"
                                                                        DataTextField="VINDivisionName" DataValueField="MakeID"></asp:ListBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Dealer Category
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="txtMan2" DataSourceID="objDelaerCategory"
                                                                        DataTextField="Category" SelectedValue='<%#Eval("DealerCategoryId")%>' DataValueField="DealerCategoryId"
                                                                        AppendDataBoundItems="True">
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:ObjectDataSource ID="objDelaerCategory" runat="server" SelectMethod="GetDealerCategory"
                                                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Source
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlSource" runat="server" CssClass="txtMan2" DataSourceID="objDealerSource"
                                                                        DataTextField="Source" SelectedValue='<%#Eval("DealerSourceId")%>' DataValueField="DealerSourceId"
                                                                        AppendDataBoundItems="True">
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:ObjectDataSource ID="objDealerSource" runat="server" SelectMethod="GetDealerSource"
                                                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                </td>
                                                                <%-- <td class="TableBorderB">
                                                                            Accounting Code
                                                                        </td>
                                                                        <td class="TableBorder">
                                                                            <asp:TextBox ID="txtAccountingCode" Text='<%#Eval("AccountingCode")%>' runat="server"
                                                                                 CssClass="txtMan2"></asp:TextBox>
                                                                        </td>--%>
                                                                <td class="TableBorderB">
                                                                    Street
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtStreet" Text='<%#Eval("Street")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Suite
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtSuite" Text='<%#Eval("Suite")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    City
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="txtMan2" Text='<%#Eval("City")%>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hfieldCountry" Value='<%# Eval("CountryId")%>' runat="server" />
                                                                    <asp:HiddenField ID="hfieldState" Value='<%# Eval("StateId")%>' runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    State
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="txtMan2" DataTextField="State"
                                                                                DataValueField="StateId">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Country
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlCountry" AutoPostBack="True" runat="server" CssClass="txtMan2"
                                                                        DataTextField="CountryName" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                                                        ErrorMessage="*" InitialValue="0" SetFocusOnError="True" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Zip
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtZip" CssClass="txtMan2" runat="server" Text='<%#Eval("Zip")%>'></asp:TextBox>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Phone
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtPhone" CssClass="txtMan2" runat="server" Text='<%#Eval("Phone1")%>'></asp:TextBox>
                                                                    <ajax:FilteredTextBoxExtender ID="txtPhoneExt" runat="server" TargetControlID="txtPhone"
                                                                        FilterType="Numbers,Custom" ValidChars="+() -" FilterMode="ValidChars">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Fax
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtFax" runat="server" CssClass="txtMan2" Text='<%#Eval("Fax")%>'></asp:TextBox>
                                                                    <ajax:FilteredTextBoxExtender ID="txtFaxExt" runat="server" TargetControlID="txtFax"
                                                                        FilterType="Numbers,Custom" ValidChars="+() -">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Other Number
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtOtherNumber" CssClass="txtMan2" runat="server" Text='<%#Eval("Phone2")%>'></asp:TextBox>
                                                                    <ajax:FilteredTextBoxExtender ID="txtOtherNuext" runat="server" TargetControlID="txtOtherNumber"
                                                                        FilterType="Numbers,Custom" ValidChars="+() -" FilterMode="ValidChars">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Email
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtMan2" Text='<%#Eval("Email1")%>'></asp:TextBox>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Auction Access Number
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtAuctionAccessNo" runat="server" CssClass="txtMan2" MaxLength="20"
                                                                        Text='<%#Eval("AuctionAccessNo")%>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Welcome Task
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    <asp:HiddenField ID="HFWelcomeTaskIDs" Value='<%# Eval("WelcomeTaskIDs")%>' runat="server" />
                                                                    <asp:HiddenField ID="HFWelcomeTaskTexts" Value='<%# Eval("WelcomeTask")%>' runat="server" />
                                                                    <telerik:RadComboBox ID="ddlWelcomeTasks" runat="server" Width="150px" AllowCustomText="true"
                                                                        EmptyMessage="" Style="z-index: 10000001">
                                                                        <ItemTemplate>
                                                                            <div onclick="StopPropagation(event)" class="combo-item-template">
                                                                                <asp:CheckBox runat="server" ID="chk1" CssClass="cb" Text='<%#Eval("Task")%>' onclick="onCheckBoxClick(this,'WelcomeTasks')" />
                                                                                <asp:HiddenField ID="HFWelcomeTaskID" Value='<%# Eval("WelcomeTaskID")%>' runat="server" />
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Comments
                                                                </td>
                                                                <td class="TableBorder" colspan="3">
                                                                    <asp:TextBox ID="txtComment" CssClass="txtMan2" Rows="3" runat="server" Text='<%#Eval("Comment")%>'
                                                                        TextMode="MultiLine"></asp:TextBox>
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("AddressId") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:FormView>
                                                <asp:ObjectDataSource ID="objMake" runat="server" SelectMethod="GetMakeList" TypeName="METAOPTION.BAL.MasterBAL">
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 10px" align="center">
                                                <asp:Button ID="btnEditCancel" runat="server" Text="Cancel" CssClass="Btn_Form" Width="75px" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnSave" runat="server" CssClass="Btn_Form" Text="Save" Width="75px"
                                                    OnClick="btnSave_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajax:ModalPopupExtender ID="modPopUp" BehaviorID="mdpop" runat="server" TargetControlID="imgbtnEdit"
                                    PopupControlID="pnlEditDealerDetails" CancelControlID="btnEditCancel" BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="height30">
                    &nbsp;
                </td>
            </tr>
            <%-- <tr>
                <td colspan="2">
                <asp:Button ID="btnRefreshCustomers" runat="server" Style="display: none" OnClick="Refresh_Click" />
                    <uc1:Contact ID="Contact1" runat="server" />
                </td>
            </tr>--%>
            <tr>
                <td>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td class="TableHeadingBg TableHeading">
                                Contacts
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <asp:ObjectDataSource ID="objContactType" runat="server" SelectMethod="GetContactType"
                                    TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="objJobTitle" runat="server" SelectMethod="GetJobTitle"
                                    TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                <asp:Button ID="btnRefreshCustomers" runat="server" Style="display: none" OnClick="Refresh_Click" />
                                <asp:GridView ID="grdContactDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderStyle="Solid" BorderWidth="0" DataKeyNames="ContactId" OnRowDataBound="grdContactDetails_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="JobTitle" HeaderText="Job Title" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="ContactType" HeaderText="Contact Type" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="OfficePhone" HeaderText="Office Phone" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                            HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlkcontact" runat="server" ImageUrl="~/Images/ChangePassword.png"
                                                    ToolTip="Add/Change Password"></asp:HyperLink>
                                                <a id="EditContct" runat="server" href='<%# "EditContact.aspx?ID="+Eval("ContactId")+"&TB_iframe=true&height=220&width=800" %>'
                                                    title="Edit Details" class="thickbox">
                                                    <img alt="" src="../Images/newedit.gif" border="0" /></a>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/DeleteButton.jpg"
                                                    OnClick="ibtnDelete_Click" OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
                                                <asp:HiddenField ID="hfSecurityUserID" runat="server" Value='<%# Eval("SecurityUserID") %>' />
                                                <asp:HiddenField ID="hfUserName" runat="server" Value='<%# Eval("UserName") %>' />
                                                <asp:HiddenField ID="hfContactID" runat="server" Value='<%# Eval("ContactId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Contact Details
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="objContactDetails" runat="server" SelectMethod="GetEntityContactDetails"
                                    TypeName="METAOPTION.BAL.CommonBAL">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="EntityId" QueryStringField="EntityId" Type="Int64" />
                                        <asp:QueryStringParameter DefaultValue="" Name="EntityTypeId" QueryStringField="type"
                                            Type="Int64" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="FooterContentDetails">
                                <a id="AddNContct" runat="server" href='<%# "AddNewContact.aspx?EntityId="+EntityId+"&type="+type+"&TB_iframe=true&height=220&width=800" %>'
                                    title="Add New Contact" style="font-size: 11px; font-weight: bold; color: #535152;
                                    font-family: Arial, Helvetica, sans-serif; text-decoration: underline;" class="thickbox">
                                    Add New Contact</a>
                            </td>
                        </tr>
                    </table>
                    <%-- <uc1:Contact ID="Contact1" runat="server" />--%>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc2:EditPreference ID="EditPreference1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td class="TableHeadingBg TableHeading">
                                Car Purchased from &#39;<asp:Label ID="lblDealerName" runat="server"></asp:Label>
                            &#39;
                        </tr>
                        <tr>
                            <td class="TableBorder">
                                <%--  <asp:UpdatePanel ID="upParchsadCars" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                            
                            </ContentTemplate>
                            </asp:UpdatePanel>--%>
                                <asp:GridView ID="grdviewPurchsedCars" runat="server" AutoGenerateColumns="False"
                                    Width="100%" BorderStyle="Solid" AllowPaging="True" BorderWidth="0px" PageSize="20"
                                    DataSourceID="objPurchasedCars" PagerSettings-Mode="NumericFirstLast">
                                    <Columns>
                                        <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="VIN Number" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnkVIN" runat="server" Text='<%#Eval("VINNumber") %>' NavigateUrl='<%# "InventoryDetail.aspx?Code="+Eval("InventoryId") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridContent" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PurchaseDate" HeaderText="Purchase Date" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContent"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CarCost" DataFormatString="{0:c}&nbsp;" HeaderText="Cost"
                                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Expense" HeaderText="Expense" DataFormatString="{0:c}&nbsp;"
                                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SoledPrice" HeaderText="Sold Price" DataFormatString="{0:c}&nbsp;"
                                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Profit" HeaderText="Profit" DataFormatString="{0:c}&nbsp;"
                                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No Cars Purchased from this Dealer
                                    </EmptyDataTemplate>
                                    <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="objPurchasedCars" runat="server" SelectCountMethod="GetPurchasedCarsByDealerCount"
                                    SelectMethod="GetPurchasedCarsByDealer" EnablePaging="True" TypeName="METAOPTION.BAL.DealerCustomerBAL">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" Type="Int64" />
                                        <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                        <asp:Parameter Name="MaximumRows" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="FooterContentDetails">
                                <%--<a href="../VMSReports/PurchasedCarsByDealers.aspx" class="AddNewExpenseTxt">View All Purchased Cars</a>--%>
                                <asp:LinkButton ID="lnkViewAllPurchasedCars" runat="server" Text="View All Purchased Cars"
                                    CssClass="AddNewExpenseTxt" OnClick="lnkViewAllPurchasedCars_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="2">
                                Car Sold to
                                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" colspan="2">
                                <asp:UpdatePanel ID="upSoldCars" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdViewSoldCars" runat="server" AutoGenerateColumns="False" Width="100%"
                                            BorderStyle="Solid" AllowPaging="True" BorderWidth="0px" PagerSettings-Mode="NumericFirstLast"
                                            OnPageIndexChanging="grdViewSoldCars_PageIndexChanging" DataSourceID="objSoldCars"
                                            PageSize="20">
                                            <Columns>
                                                <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="VIN Number" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkVIN" runat="server" Text='<%#Eval("VINNumber") %>' NavigateUrl='<%# "InventoryDetail.aspx?Code="+Eval("InventoryId") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Title" HeaderText="Title" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SoldDate" HeaderText="Sold Date" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CarCost" HeaderText="Cost" DataFormatString="{0:c}&nbsp;"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Expense" HeaderText="Expense" DataFormatString="{0:c}&nbsp;"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SoledPrice" HeaderText="Sold Price" DataFormatString="{0:c}&nbsp;"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Profit" HeaderText="Profit" DataFormatString="{0:c}&nbsp;"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Cars Sold to this Customer
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="objSoldCars" runat="server" SelectCountMethod="GetSoldCarsToDealerCount"
                                            SelectMethod="GetSoldCarsToDealer" EnablePaging="True" TypeName="METAOPTION.BAL.DealerCustomerBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" Type="Int64" />
                                                <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                                <asp:Parameter Name="MaximumRows" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="FooterContentDetails" width="50%" colspan="2">
                                <%--<a href="../VMSReports/ViewAllSoldCars.aspx" class="AddNewExpenseTxt">View All Sold Cars</a>--%>
                                <asp:LinkButton ID="lnkViewAllSoldCars" runat="server" Text="View All Sold Cars"
                                    CssClass="AddNewExpenseTxt" OnClick="lnkViewAllSoldCars_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td class="TableHeadingBg TableHeading" colspan="3">
                                Payments Made
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" colspan="3">
                                <asp:UpdatePanel ID="upCommission" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdCommision" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CellPadding="4" GridLines="None" AllowPaging="True" PagerSettings-Mode="NumericFirstLast"
                                            OnPageIndexChanging="grdCommision_PageIndexChanging" DataSourceID="objPayments"
                                            PageSize="20">
                                            <Columns>
                                                <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                                                <asp:BoundField DataField="DatePaid" HeaderText="Date Paid" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="FirstName">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InvoiceNumber" HeaderText="Invoice Number" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="FirstName">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Check No." HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkCheckNo" runat="server" Text='<%#Eval("CheckNumber") %>'
                                                            NavigateUrl='<%# "ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Amount" DataFormatString="{0:c}&nbsp;" HeaderText="Amount"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight" SortExpression="LastDesc">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PaidBy" HeaderText="Paid By" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PeechtreeRefNumber" HeaderText="PeachTree Ref. No." HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text=" No Payment Made"></asp:Label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="#E4EDF4" />
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="objPayments" runat="server" SelectCountMethod="GetPaymentsCount"
                                            SelectMethod="GetPayments" EnablePaging="True" TypeName="METAOPTION.BAL.CommonBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="entityId" QueryStringField="EntityId" Type="Int64" />
                                                <asp:QueryStringParameter Name="entityTypeId" QueryStringField="type" Type="Int32" />
                                                <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                                <asp:Parameter Name="MaximumRows" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="FooterContentDetails" colspan="3">
                                <a id="Newparmnt" runat="server" href='<%# "MakeANewPayment.aspx?EntityId="+(EntityId)+"&type=1" %>'
                                    class="AddNewExpenseTxt">Add New Payment</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:FormView ID="frmViewSummary" runat="server" Width="100%" DataSourceID="objSummary">
                        <ItemTemplate>
                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                class="arial-12">
                                <tr>
                                    <td colspan="4" class="TableHeadingBg TableHeading">
                                        Summary
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder" width="150">
                                        <b>Total Cars Purchased</b>
                                    </td>
                                    <td class="TableBorder" width="200">
                                        <%# Eval("PurchasedQty") %>
                                    </td>
                                    <td class="TableBorder" width="150">
                                        <b>Total Purchased Amount</b>
                                    </td>
                                    <td class="TableBorder">
                                        <%# String.Format("{0:c}", Eval("PurchaseAmt")) %>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <b>Total Cars Sold</b>
                                    </td>
                                    <td class="TableBorder">
                                        <%#Eval("SoldQty")%>
                                    </td>
                                    <td class="TableBorder">
                                        <b>Total Sold Amount</b>
                                    </td>
                                    <td class="TableBorder">
                                        <%# String.Format("{0:c}",Eval("SoldAmt"))%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <b>Last Car Purchased</b>
                                    </td>
                                    <td class="TableBorder">
                                        <%--  <%# String.Format("{0:MM/dd/yyyy}", Eval("LastPurchaseDate"))%>--%>
                                        <asp:Label ID="lblLastPurchsedCar" runat="server" Text='<%# Eval("LastPurchaseDate")%>'></asp:Label>
                                    </td>
                                    <td class="TableBorder">
                                        <b>Last Car Sold</b>
                                    </td>
                                    <td class="TableBorder">
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("LastSoldDate")%>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            No Car Purchased or Sold to this Purchased From/Sold To
                        </EmptyDataTemplate>
                    </asp:FormView>
                    <asp:ObjectDataSource ID="objSummary" runat="server" SelectMethod="DealerBusinessSummary"
                        TypeName="METAOPTION.BAL.DealerCustomerBAL">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" Type="Int64" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding: 10px 10px 10px 0px; font-size: 12px; font-weight: bold">
                    <asp:Button ID="btnBack1" CausesValidation="false" Text="<< Back" class="Btn_Form"
                        runat="server" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        function Delete_ArchiveDealer(Status) {
            var Dealerid = $('#<%=hfDealerid.ClientID %>').val();
            var UserID = '<%=Session["empId"] %>';
            var Answer = true;

            if (Status == "0")
                Answer = confirm("Do you want to delete this Dealer? Once deleted, it will not appear in Searchlist. Do you want to continue?");
            else if (Status == "2")
                Answer = confirm("Do you want to archive this Dealer? Once archived, it will appear in Searchlist, but no transaction will happen for this Dealer. Do you want to continue?");

            if (Answer) {
                $.ajax({ type: "POST",
                    url: "ViewDealer.aspx/DeleteArchiveDealer",
                    data: "{Status:" + Status + ",DealerID:" + Dealerid + ",UserID:" + UserID + "}",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    cache: false,
                    error: Failed,
                    success: function () {

                        if (Status == "0") {
                            $('#<%=imgDelete.ClientID %>').hide();
                            $('#<%=imgUnDelete.ClientID %>').show();
                        }
                        else if (Status == "2") {
                            $('#<%=imgArchive.ClientID %>').hide();
                            $('#<%=imgUnArchive.ClientID %>').show();
                        }
                        else if (Status == "1") {
                            $('#<%=imgDelete.ClientID %>').show();
                            $('#<%=imgUnDelete.ClientID %>').hide();
                        }
                        else if (Status == "-1") {
                            $('#<%=imgArchive.ClientID %>').show();
                            $('#<%=imgUnArchive.ClientID %>').hide();
                        }
                        window.location.href = "../UI/DealerList.aspx";
                    }
                });
            }
        }

        //Failed Function
        function Failed(result) {
            alert(result.status + " " + result.statusText);
        }


 
    </script>
    <script type="text/javascript">
        var objHidden;
        var combo;
        var cancelDropDownClosing = false;

        function StopPropagation(e) {
            //cancel bubbling
            e.cancelBubble = true;
            if (e.stopPropagation) {
                e.stopPropagation();
            }
        }

        function onDropDownClosing() {
            cancelDropDownClosing = false;
        }

        function onCheckBoxClick(chk, ctrlType) {
            if (ctrlType == "WelcomeTasks") {
                objHidden = document.getElementById("<%= this.hWelcomeTasks.ClientID %>");
                combo = document.getElementById('<%=frmviewEditDealer.FindControl("ddlWelcomeTasks").ClientID%>');
            }
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items

            // var items = combo.get_items();

            //enumerate all items
            for (var i = 0; i <= combo.childElementCount; i++) {
                //var item = items.getItem(i);
                //get the checkbox element of the current item

                //alert(combo.get_id() + "_i" + i + "_chk1");

                var chk1 = $get(combo.id + "_i" + i + "_chk1");
                var chkLbl = chk1.nextElementSibling;
                if (chk1.checked) {
                    text += chkLbl.innerHTML + ",";
                    values += GetValue(i) + ",";
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);

            if (text.length > 0) {
                //set the text of the combobox
                SetText(text);
                //combo.set_text(text);
                objHidden.value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                SetText('');
                //combo.set_text("");
                objHidden.value = '';
            }

        }

        function GetText(indx) {
            var obj = document.getElementById("ctl00_ContentPlaceHolder1_frmviewEditDealer_ddlWelcomeTasks_i" + indx + "_chk1");
            return obj.innerHTML;
        }

        function GetValue(indx) {
            var obj = document.getElementById("ctl00_ContentPlaceHolder1_frmviewEditDealer_ddlWelcomeTasks_i" + indx + "_HFWelcomeTaskID");
            return obj.value;
        }

        function SetText(txt) {
            var obj = document.getElementById("ctl00_ContentPlaceHolder1_frmviewEditDealer_ddlWelcomeTasks_Input");
            obj.value = txt;
        }

        //this method removes the ending comma from a string
        function removeLastComma(str) {
            return str.replace(/,$/, "");
        }
 
    </script>
    <style type="text/css">
        .cb label
        {
            margin-left: 5px;
        }
    </style>
</asp:Content>
