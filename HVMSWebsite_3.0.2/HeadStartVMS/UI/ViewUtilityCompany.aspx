<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewUtilityCompany.aspx.cs"
    Inherits="METAOPTION.UI.ViewUtilityCompany" Title="HeadStartVMS::View UtilityCompany" %>

<%@ Register Src="../UserControls/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<script type="text/C#" runat="server">
    
    protected void Refresh_Click(object sender, EventArgs args)
    {
        //  update the grids contents
        this.BindContactDetails();

        // Response.Redirect("ViewDealer.aspx?EntityId=" + EntityId + "&type=" + type);
    }
    
</script>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function updated() {
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
    <div class="RightPanel">
        <asp:HiddenField ID="hfUtilityCompanyId" runat="server" />
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td>
                    <%--<asp:UpdatePanel ID="upComDetails" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnDelete" runat="server" Visible="false" CssClass="Btn_Form" Text="Delete Company"
                                    OnClientClick="javascript:return confirm('Are you sure you want to delete this Utility Company? You will not be able to access this Utility Company once deleted\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                                    OnClick="btnDelete_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="TableHeadingBg">
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="TableHeading">
                                            Utility Company
                                        </td>
                                        <td align="right" class="HeadingEditButton">
                                            <img border="0" src="../Images/Deleteblue.png" id="imgDelete" alt="close" onclick="Delete_ArchiveUtilityCompany('0');"
                                                runat="server" />
                                            <img border="0" src="../Images/Deletered.png" id="imgUnDelete" alt="close" onclick="Delete_ArchiveUtilityCompany('1');"
                                                runat="server" />
                                            <img border="0" src="../Images/Archiveblue.png" id="imgArchive" alt="close" onclick="Delete_ArchiveUtilityCompany('2');"
                                                runat="server" />
                                            <img border="0" src="../Images/ArchiveRed.png" id="imgUnArchive" alt="close" onclick="Delete_ArchiveUtilityCompany('-1');"
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
                                <asp:FormView ID="frmViewCompDetails" runat="server" Width="100%" DataSourceID="objCompanyDetails"
                                    OnDataBound="frmViewCompDetails_DataBound">
                                    <ItemTemplate>
                                        <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                            <tr>
                                                <td class="TableBorder" width="150">
                                                    <b>Company Name</b>
                                                </td>
                                                <td class="TableBorder" width="200">
                                                    <%# Eval("CompanyName") %>
                                                </td>
                                                <td class="TableBorder" width="150">
                                                    <b>Tax Id Number</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("TaxIdNumber")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Company Category</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("CompanyCategory")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Frequency of Payments</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("PayementFrequency")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Account Number</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("AccountNumber")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Accounting Code</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("AccountingCode")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Street</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("Street")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Suite</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("Suite")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>City</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("City")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>State</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("State")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Country</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("CountryName")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Zip</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("Zip")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Phone</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("Phone1")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Fax</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("Fax")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Others</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("Phone2")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Email</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Eval("Email1")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder" valign="top">
                                                    <b>Comments</b>
                                                </td>
                                                <td class="TableBorder" colspan="3">
                                                    <%# Eval("Comments")%>
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
                                        <asp:HiddenField ID="hfIsComUtilityExists" Value='<%# Eval("IsExists") %>' runat="server" />
                                        <asp:HiddenField ID="hfIsActive" Value='<%# Eval("IsActive") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:FormView>
                                <asp:ObjectDataSource ID="objCompanyDetails" runat="server" SelectMethod="GetCompanyDetailsByCompanyId"
                                    TypeName="METAOPTION.BAL.UtilityCompanyBAL">
                                    <SelectParameters>
                                        <asp:QueryStringParameter DefaultValue="" Name="utiCompanyId" QueryStringField="EntityId"
                                            Type="Int64" />
                                        <asp:SessionParameter Name="OrgID" SessionField="OrgID" Type="Int16" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:Panel ID="pnlEditCompanyDetail" Width="700" Style="display: none" runat="server"
                                    CssClass="modalPopup">
                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                        <tr style="width: 100%">
                                            <td class="PopUpBoxHeading">
                                                &nbsp;&nbsp;Edit Utility Company Details
                                            </td>
                                            <td class="PopUpBoxHeading" align="right">
                                                <img id="ImageButton2" runat="server" onclick="$find('mdpop').hide();return false;"
                                                    alt="" src="../Images/close.gif" style="padding-right: 5px;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2" style="padding: 10px">
                                                <asp:FormView ID="fvEditCompanyDetail" runat="server" Width="100%" DataSourceID="objCompanyDetails"
                                                    OnDataBound="fvEditCompanyDetail_DataBound">
                                                    <ItemTemplate>
                                                        <table border="0" cellspacing="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Company Name
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtCompanyName" CssClass="txtMan2" Text='<%# Eval("CompanyName") %>'
                                                                        runat="server"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                                                        ErrorMessage="*" SetFocusOnError="True" />
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    TaxId No
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtTaxIdNo" CssClass="txtMan2" Text='<%# Eval("TaxIdNumber") %>'
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Company Category
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlCompanyCategory" AppendDataBoundItems="true" CssClass="txtMan2"
                                                                        runat="server" DataSourceID="objCategory" DataTextField="CompanyCategory1" SelectedValue='<%# Eval("CompanyCategoryId") %>'
                                                                        DataValueField="CompanyCategoryId">
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:ObjectDataSource ID="objCategory" runat="server" SelectMethod="GetCompanyCategory"
                                                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Frequency of Payments
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlPaymentFreq" AppendDataBoundItems="true" SelectedValue='<%# Eval("PayementFrequencyId") %>'
                                                                        CssClass="txtMan2" runat="server" DataSourceID="objPayFrequency" DataTextField="PayementFrequency1"
                                                                        DataValueField="PayementFrequencyId">
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:ObjectDataSource ID="objPayFrequency" runat="server" SelectMethod="GetPaymentFrequency"
                                                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Account Number
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtAccountNo" CssClass="txtMan2" Text='<%# Eval("AccountNumber") %>'
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                                <%-- <td class="TableBorderB">
                                                                            Accounting Code
                                                                        </td>
                                                                        <td class="TableBorder">
                                                                            <asp:TextBox ID="txtAccountingCode" CssClass="txtMan2" Text='<%# Eval("AccountingCode") %>'
                                                                                runat="server"></asp:TextBox>
                                                                        </td>--%>
                                                                <td class="TableBorderB">
                                                                    Street
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:HiddenField ID="hdnAddressId" Value='<%# Eval("AddressId")%>' runat="server" />
                                                                    <asp:TextBox ID="txtStreet" CssClass="txtMan2" Text='<%# Eval("Street") %>' runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Suite
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtSuite" CssClass="txtMan2" Text='<%# Eval("Suite") %>' runat="server"></asp:TextBox>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    City
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtCity" CssClass="txtMan2" Text='<%# Eval("City") %>' runat="server"></asp:TextBox>
                                                                    <asp:HiddenField ID="hfieldCountry" Value='<%# Eval("CountryId")%>' runat="server" />
                                                                    <asp:HiddenField ID="hfieldState" Value='<%# Eval("StateId")%>' runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    State
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:UpdatePanel ID="updPnlState" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="txt2" DataTextField="State"
                                                                                DataValueField="StateId">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
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
                                                                    <asp:TextBox ID="txtZip" CssClass="txtMan2" Text='<%# Eval("Zip") %>' runat="server"></asp:TextBox>
                                                                    <ajax:FilteredTextBoxExtender ID="txtZip_Extender" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtZip">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Phone
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtPhone" runat="server" Text='<%# Eval("Phone1") %>' CssClass="txtMan2"></asp:TextBox>
                                                                    <ajax:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                                                        FilterType="Numbers,Custom" ValidChars="+() -" TargetControlID="txtPhone">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Fax
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtFax" CssClass="txtMan2" Text='<%# Eval("Fax") %>' runat="server"></asp:TextBox>
                                                                    <ajax:FilteredTextBoxExtender ID="txtFax_Extender" runat="server" FilterType="Numbers,Custom"
                                                                        ValidChars="+() -" TargetControlID="txtFax">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Other
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtOther" CssClass="txtMan2" Text='<%# Eval("Phone2") %>' runat="server"></asp:TextBox>
                                                                    <ajax:FilteredTextBoxExtender ID="txtOther_Extender" runat="server" FilterType="Numbers,Custom"
                                                                        ValidChars="+() -" TargetControlID="txtOther">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Email
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtEmail" CssClass="txtMan2" Text='<%# Eval("Email1") %>' runat="server"></asp:TextBox>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="TableBorder">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Comments
                                                                </td>
                                                                <td class="TableBorder" colspan="3">
                                                                    <asp:TextBox ID="txtComments" Text='<%# Eval("Comments") %>' CssClass="txtMulti"
                                                                        Rows="5" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:FormView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 10px" align="center">
                                                <asp:Button ID="btnEditCancel" CausesValidation="false" runat="server" Text="Cancel"
                                                    CssClass="Btn_Form" Width="75px" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnSave" runat="server" CssClass="Btn_Form" Text="Save" Width="75px"
                                                    OnClick="btnSave_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <ajax:ModalPopupExtender ID="modPopUp" runat="server" BehaviorID="mdpop" TargetControlID="imgbtnEdit"
                                    PopupControlID="pnlEditCompanyDetail" CancelControlID="btnEditCancel" BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <%-- <uc1:Contact ID="Contact1" runat="server" />--%>
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
                                    BorderStyle="Solid" BorderWidth="0" DataKeyNames="ContactId" OnRowDeleting="grdContactDetails_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="JobTitle" HeaderText="Job Title" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="ContactType" HeaderText="Contact Type" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="OfficePhone" HeaderText="Office Phone" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="CellPhone" HeaderText="Cell Phone" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                            HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                            <ItemTemplate>
                                                <a href='<%# "EditContact.aspx?ID="+Eval("ContactId")+"&TB_iframe=true&height=220&width=800" %>'
                                                    title="Edit Details" class="thickbox">
                                                    <img alt="" src="../Images/edit-icon.jpg" border="0" /></a>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/DeleteButton.jpg"
                                                    OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
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
                                <a href='<%="AddNewContact.aspx?EntityId="+EntityId+"&type="+type+"&TB_iframe=true&height=220&width=800" %>'
                                    title="Add New Contact" style="font-size: 11px; font-weight: bold; color: #535152;
                                    font-family: Arial, Helvetica, sans-serif; text-decoration: underline;" class="thickbox">
                                    Add New Contact</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="upCommission" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                class="arial-12">
                                <tr>
                                    <td class="TableHeadingBg TableHeading">
                                        Payments Made
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <asp:GridView ID="grdCommision" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="gridView" CellPadding="4" GridLines="None" AllowPaging="True" PageSize="25"
                                            OnPageIndexChanging="grdCommision_PageIndexChanging" DataSourceID="objPayments"
                                            PagerSettings-Mode="NumericFirstLast">
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
                                                <asp:TemplateField HeaderText="Check#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkCheckNo" runat="server" Text='<%#Eval("CheckNumber") %>'
                                                            NavigateUrl='<%# "ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Amount" DataFormatString="{0:c}" HeaderText="Amount" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PaidBy" HeaderText="Paid By" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PeechtreeRefNumber" HeaderText="Peachtree Ref. No." HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="LastDesc">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Payment Made"></asp:Label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="#E4EDF4" />
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="objPayments" runat="server" SelectCountMethod="GetPaymentsCount"
                                            SelectMethod="GetPayments" EnablePaging="True" TypeName="METAOPTION.BAL.CommonBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="entityId" QueryStringField="EntityId" Type="Int64" />
                                                <asp:QueryStringParameter Name="entityTypeId" QueryStringField="Type" Type="Int32" />
                                                <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                                <asp:Parameter Name="MaximumRows" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr id="trAddNewPayment" runat="server">
                                    <td class="FooterContentDetails">
                                        <a href='<%="MakeANewPayment.aspx?EntityId="+(EntityId)+"&type=4" %>' class="AddNewExpenseTxt">
                                            Add New Payment</a>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">

        function Delete_ArchiveUtilityCompany(Status) {
            var UtilityCompanyId = $('#<%=hfUtilityCompanyId.ClientID %>').val();
            var UserID = '<%=Session["empId"] %>';
            var Answer = true;

            if (Status == "0")
                Answer = confirm("Do you want to delete this Utility Company? Once deleted, it will not appear in Searchlist. Do you want to continue?");
            else if (Status == "2")
                Answer = confirm("Do you want to archive this Utility Company? Once archived, it will appear in Searchlist, but no transaction will happen for this Utility Company. Do you want to continue?");

            if (Answer) {
                $.ajax({ type: "POST",
                    url: "ViewUtilityCompany.aspx/DeleteArchiveUtilityCompany",
                    data: "{Status:" + Status + ",UtilityCompanyId:" + UtilityCompanyId + ",UserID:" + UserID + "}",
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
                        window.location.href = "../UI/UtilityCompanyList.aspx";
                    }
                });
            }
        }

        //Failed Function
        function Failed(result) {
            alert(result.status + " " + result.statusText);
        }
        
    </script>
</asp:content>
