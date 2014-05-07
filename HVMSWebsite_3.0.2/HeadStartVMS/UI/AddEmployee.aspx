<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="METAOPTION.UI.AddEmployee"
    Title="HeadStartVMS::Add Employee" %>

<asp:content contentplaceholderid="ContentPlaceHolder1" id="cphAddEmp" runat="server">
    <div>
        <table border="0" cellpadding="0" style="border-collapse: collapse"
            width="100%">
            <tr>
                <td class="AddHeading">
                    Add A Employee
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
                        <legend class="ForLegend">Employee Details</legend>
                        <br />
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr>
                                <td class="TableBorderB">
                                    Title
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="txtMan2">
                                        <asp:ListItem>Mr</asp:ListItem>
                                        <asp:ListItem>Mrs</asp:ListItem>
                                        <asp:ListItem>Ms</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="TableBorderB">
                                    First Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtFirstName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtFirstName"
                                        ErrorMessage="*" SetFocusOnError="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Middle Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtMiddleName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                                <td class="TableBorderB">
                                    Last Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtLastName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Employee Code
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtEmployeeCode" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                                <td class="TableBorderB">
                                    Employee Type
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlEmployeeType" CssClass="txtMan2" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
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
                                <td class="TableBorderB">
                                    Suite
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSuite" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    City
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCity" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity"
                                        ErrorMessage="*" SetFocusOnError="True" />--%>
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
                                            <asp:DropDownList ID="ddlState" CssClass="txtMan2" DataTextField="State"
                                                DataValueField="StateId" runat="server">
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
                                        DataTextField="CountryName"  DataValueField="CountryId"
                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                      InitialValue="0"    ErrorMessage="*" SetFocusOnError="True" />
                                </td>
                                <td class="TableBorderB">
                                    Zip
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtZip" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtZip_Extender" runat="server" FilterType="Numbers"
                                        TargetControlID="txtZip">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Phone
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtPhone" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                        FilterType="Numbers,Custom" ValidChars="+,-" TargetControlID="txtPhone">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                                <td class="TableBorderB">
                                    Ext.
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtExt" runat="server" CssClass="txtMan2" Style="height: 22px"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtExt_Extender1" runat="server" FilterType="Numbers"
                                        TargetControlID="txtExt">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Cell Phone
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCellPhone" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtCellPhone_Extender" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="+,-" TargetControlID="txtCellPhone">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                                <td class="TableBorderB">
                                    Email
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtEmail" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="*"
                                        SetFocusOnError="True" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Driver License State
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlDriverLicState" CssClass="txtMan2" 
                                        runat="server"  DataTextField="State" DataValueField="StateId">
                                    </asp:DropDownList>
                                </td>
                                <td class="TableBorderB">
                                    Driver License No.
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtDriverLicNo" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB" style="height: 22px">
                                    Driver License Expiration Date
                                </td>
                                <td class="TableBorder" nowrap="nowrap" style="height: 22px">
                                    <asp:TextBox ID="txtLicExpDate" runat="server" CssClass="txtMan2"></asp:TextBox>
                                    <ajax:CalendarExtender ID="caltxtLicExpDate" runat="server" PopupButtonID="imgLicExpDate"
                                        TargetControlID="txtLicExpDate">
                                    </ajax:CalendarExtender>
                                    <asp:Image ID="imgLicExpDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                        Style="cursor: pointer;" />
                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="/,-" TargetControlID="txtLicExpDate">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                                <td class="TableBorderB" style="height: 22px">
                                    &nbsp;</td>
                                <td class="TableBorder" style="height: 22px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Special Payroll Conditions</td>
                                <td class="TableBorder" nowrap="nowrap" colspan="3">
                                    <asp:TextBox ID="txtSpcPayCondt"  runat="server" CssClass="txtMan2" Rows="5" 
                                        TextMode="MultiLine" Width="550px"></asp:TextBox>
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
                    <asp:Button ID="btnCancel" runat="server" CssClass="Btn_Form" OnClick="btnCancel_Click"
                        Text="Cancel" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" CssClass="Btn_Form" OnClick="btnAdd_Click"
                        Text="Add" Width="60px" />
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
