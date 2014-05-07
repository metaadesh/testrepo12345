<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddUtilityCompany.aspx.cs"
    Inherits="METAOPTION.UI.AddUtilityCompany" Title="HeadStartVMS::Add UtilityCompany" %>

<asp:content contentplaceholderid="ContentPlaceHolder1" id="cphAddUtiComp" runat="server">
    <div>
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="AddHeading">
                    Add a Utility Company/Sub Contractor
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">
                    <fieldset class="ForFieldSet">
                        <legend class="ForLegend">Company Details</legend>
                        <br>
                        <table border="0" cellspacing="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr>
                                <td class="TableBorderB">
                                    Company Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCompanyName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName"
                                        ErrorMessage="*" SetFocusOnError="True" />
                                </td>
                                <td class="TableBorderB">
                                    Tax ID
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtTaxIdNo" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Company Category
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlCompanyCategory" CssClass="txtMan2" runat="server" DataSourceID="objCategory"
                                        DataTextField="CompanyCategory1" DataValueField="CompanyCategoryId">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="objCategory" runat="server" SelectMethod="GetCompanyCategory"
                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                </td>
                                <td class="TableBorderB">
                                    Frequency of Payments
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlPaymentFreq" CssClass="txtMan2" runat="server" DataSourceID="objPayFrequency"
                                        DataTextField="PayementFrequency1" DataValueField="PayementFrequencyId">
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
                                    <asp:TextBox ID="txtAccountNo" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                                <%-- <td class="TableBorderB">
                                    Accounting Code
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtAccountingCode" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>--%>
                                <td class="TableBorderB">
                                    Street
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtStreet" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Suite
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSuite" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                                <td class="TableBorderB">
                                    City
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCity" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    State
                                </td>
                                <td class="TableBorder">
                                    <asp:UpdatePanel ID="updPnlState" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="txtMan2" DataTextField="State"
                                                DataValueField="StateId">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TableBorderB">
                                    Country
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="txt2"
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
                                    <asp:TextBox ID="txtZip" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtZip_Extender" runat="server" FilterType="Numbers"
                                        TargetControlID="txtZip">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                                <td class="TableBorderB">
                                    Phone
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="txtMan2"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                        FilterType="Numbers,Custom" ValidChars="+,-" TargetControlID="txtPhone">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Fax
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtFax" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtFax_Extender" runat="server" FilterType="Numbers"
                                        TargetControlID="txtFax">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                                <td class="TableBorderB">
                                    Other
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtOther" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtOther_Extender" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="+,-" TargetControlID="txtOther">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Email
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtEmail" CssClass="txtMan2" runat="server"></asp:TextBox>
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
                                    <asp:TextBox ID="txtComments" Width="100%" runat="server" CssClass="txtMan2" Rows="5"
                                        TextMode="MultiLine" onkeyup="MaxCharLimit(this, 'txtcount', 200)" />
                                    <asp:TextBox ID="txtcount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                        Width="30px" />
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
                <td>
                    <asp:UpdatePanel ID="updPnlContact" runat="server">
                        <ContentTemplate>
                            <fieldset class="ForFieldSet">
                                <legend class="ForLegend">Contacts</legend>
                                <br>
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ObjectDataSource ID="objJobTitle" runat="server" SelectMethod="GetJobTitle"
                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                    <asp:ObjectDataSource ID="objContactType" runat="server" SelectMethod="GetContactType"
                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                    <asp:GridView ID="gdvContactDetail" runat="server" AutoGenerateColumns="false" OnRowDataBound="gdvContactDetail_RowDataBound"
                                        GridLines="None" CssClass="gridView" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnRemove" runat="server" ImageUrl="~/Images/DeleteButton.jpg"
                                                        CommandArgument='<%# Eval("SNo") %>' OnCommand="REMOVE" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="15px" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="S.No." DataField="SNO" Visible="false"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Job Title" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlJobTitle" runat="server" CssClass="txtMan1" DataTextField="JobTitle1"
                                                        DataValueField="JobTitleId" AppendDataBoundItems="True">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="First Name" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtMan1" Text='<%# Eval("FirstName")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Middle Name" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txt1" Text='<%# Eval("MiddleName")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Name" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt1" Text='<%# Eval("LastName")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Type" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlContactType" runat="server" CssClass="txtMan1" DataTextField="ContactType1"
                                                        DataValueField="ContactTypeId" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Phone" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOfficeNo" runat="server" CssClass="txtMan1" Text='<%# Eval("OfficeNo")%>' />
                                                    <ajax:FilteredTextBoxExtender ID="txtOfficeNo_Extender" runat="server" FilterType="Numbers,Custom"
                                                        ValidChars="+,-" TargetControlID="txtOfficeNo">
                                                    </ajax:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cell Phone" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCellNo" runat="server" CssClass="txtMan1" Text='<%# Eval("CellNo")%>' />
                                                    <ajax:FilteredTextBoxExtender ID="txtCellNo_Extender" runat="server" FilterType="Numbers,Custom"
                                                        ValidChars="+,-" TargetControlID="txtCellNo">
                                                    </ajax:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtMan1" Text='<%# Eval("Email")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnNewRow" runat="server" CssClass="txt1" ImageUrl="~/Images/AddButton.jpg"
                                                        Width="20" Height="20" OnClick="btnNewRow_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="15px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="height30">
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnCancel"  CausesValidation="false" CssClass="Btn_Form" runat="server"
                        Text="Cancel" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAdd" CssClass="Btn_Form" runat="server"
                        Text="Add" OnClick="btnAdd_Click" Width="63px" />
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:content>
