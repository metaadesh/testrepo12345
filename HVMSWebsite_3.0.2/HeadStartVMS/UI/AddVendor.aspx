<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVendor.aspx.cs" Inherits="METAOPTION.UI.AddVendor"
    EnableEventValidation="false" Title="HeadStartVMS::Add Vendor" %>

<asp:content contentplaceholderid="ContentPlaceHolder1" runat="server" id="cphAddVendor">
    <div class="RightPanel">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td class="AddHeading">
                            Add Vendor
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <fieldset class="ForFieldSet">
                                <legend class="ForLegend">Vendor Details</legend>
                                <br>
                                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
                                    width="100%">
                                    <tr>
                                        <td class="TableBorderB">
                                            Vendor Name
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="txtMan2" EnableViewState="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                                ErrorMessage="*" SetFocusOnError="True" />
                                        </td>
                                        <td class="TableBorderB">
                                            Vendor DIN
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtDIN" runat="server" CssClass="txtMan2" EnableViewState="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Vendor Category
                                        </td>
                                        <td class="TableBorder">
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="txtMan2" DataSourceID="objVendorCategory"
                                                DataTextField="VendorCategory1" DataValueField="VendorCategoryId">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="objVendorCategory" runat="server" SelectMethod="GetVendorCategory"
                                                TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                        </td>
                                        <td class="TableBorderB">
                                            Vendor Type
                                        </td>
                                        <td class="TableBorder">
                                            <asp:DropDownList ID="ddlVenderType" runat="server" CssClass="txtMan2" DataSourceID="objVendorType"
                                                DataTextField="VendorType1" DataValueField="VendorTypeId">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="objVendorType" runat="server" SelectMethod="GetVendorType"
                                                TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Tax Id Number
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtIdNumber" runat="server" CssClass="txtMan2" />
                                        </td>
                                        <td class="TableBorderB">
                                            Payments Terms
                                        </td>
                                        <td class="TableBorder">
                                            <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="txtMan2" DataSourceID="objPaymentTerm"
                                                DataTextField="PaymentTerm1" DataValueField="PaymentTermId">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="objPaymentTerm" runat="server" SelectMethod="GetPaymentTerms"
                                                TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%-- <td class="TableBorderB">
                                               Accounting Code
                                           </td>
                                           <td class="TableBorder">
                                               <asp:TextBox ID="txtAccountingCode" runat="server" CssClass="txtMan2" />
                                           </td>--%>
                                        <td class="TableBorderB">
                                            Street
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtStreet" runat="server" CssClass="txtMan2" />
                                        </td>
                                        <td class="TableBorderB">
                                            Suite
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtSuite" runat="server" CssClass="txt2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            City
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="txt2" />
                                        </td>
                                        <td class="TableBorderB">
                                            State
                                        </td>
                                        <td class="TableBorder">
                                            <asp:UpdatePanel ID="updPnlState" UpdateMode="Conditional" runat="server">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlState" CssClass="txtMan2" DataTextField="State" DataValueField="StateId"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
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
                                        <td class="TableBorderB">
                                            Zip
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtZip" runat="server" CssClass="txt2" />
                                            <ajax:FilteredTextBoxExtender ID="txtZip_Extender" runat="server" FilterType="Numbers"
                                                TargetControlID="txtZip">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Phone Number
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="txt2" />
                                            <ajax:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                                FilterType="Numbers,Custom" TargetControlID="txtPhone" ValidChars="+,-">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                        <td class="TableBorderB">
                                            Other Number
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtOtherNumber" runat="server" CssClass="txt2" />
                                            <ajax:FilteredTextBoxExtender ID="txtOtherNumber_Extender" runat="server" FilterType="Numbers,Custom"
                                                TargetControlID="txtOtherNumber" ValidChars="+,-">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Fax
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtFax" runat="server" CssClass="txt2" />
                                            <ajax:FilteredTextBoxExtender ID="txtFax_Extender" runat="server" FilterType="Numbers"
                                                TargetControlID="txtFax">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                        <td class="TableBorderB">
                                            Email
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt2" />
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="*" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Comments
                                        </td>
                                        <td class="TableBorder" colspan="3">
                                            <asp:TextBox ID="txtComment" Width="100%" runat="server" CssClass="txtMan2" Rows="5"
                                                TextMode="MultiLine" onkeyup="MaxCharLimit(this, 'txtcount', 500)" />
                                            <asp:TextBox ID="txtcount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                Width="30px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            TRP Expense Calculation
                                        </td>
                                        <td class="TableBorder" colspan="3">
                                            <asp:DropDownList ID="ddlExpenseCalcMethod" runat="server" CssClass="txt2">
                                                <asp:ListItem Text="Mileage" Value="1" Selected="True" />
                                                <asp:ListItem Text="Location" Value="2" />
                                            </asp:DropDownList>
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
                        </td>
                    </tr>
                    <tr>
                        <td class="height30">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnCancel" CausesValidation="false" runat="server" CssClass="Btn_Form"
                                Text="Cancel" Width="75px" OnClick="btnCancel_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddVender" runat="server" CssClass="Btn_Form"
                                OnClick="btnAdd_Click" Text="Add" Width="75px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:content>
