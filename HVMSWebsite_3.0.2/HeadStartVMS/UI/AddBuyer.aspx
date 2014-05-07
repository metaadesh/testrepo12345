<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPage.Master"
    CodeBehind="AddBuyer.aspx.cs" Inherits="METAOPTION.UI.AddBuyer" Title="HeadStart VMS::Add Buyer" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphAddBuyer" runat="server">
    <%--<script type="text/javascript" language="javascript">
  function ResetForm()
  {
    document.getElementById('<%=txtFirstName.ClientID %>').value = "";
    document.getElementById('<%=txtMiddleName.ClientID %>').value = "";
    document.getElementById('<%=txtLastName.ClientID %>').value = "";
    document.getElementById('<%=txtIdNumber.ClientID %>').value = "";
    document.getElementById('<%=txtCommisionValue.ClientID %>').value = "";
    document.getElementById('<%=txtSSLNumber.ClientID %>').value = "";
    document.getElementById('<%=txtLPnumber.ClientID %>').value = "";
    document.getElementById('<%=txtStreet.ClientID %>').value = "";
    document.getElementById('<%=txtSuite.ClientID %>').value = "";
    document.getElementById('<%=txtCity.ClientID %>').value = "";
    document.getElementById('<%=txtZip.ClientID %>').value = "";
    document.getElementById('<%=txtPhone.ClientID %>').value = "";
    document.getElementById('<%=txtCellPhone.ClientID %>').value = "";
    document.getElementById('<%=txtFax.ClientID %>').value = "";
    document.getElementById('<%=txtOtherNumber.ClientID %>').value = "";
    document.getElementById('<%=txtEmail.ClientID %>').value = "";
    document.getElementById('<%=txtComment.ClientID %>').value = "";
  }
</script>--%>
    <div class="RightPanel">
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="AddHeading">
                    Add A Buyer
                </td>
            </tr>
            <tr>
                <td align="left">
                    <fieldset class="ForFieldSet">
                        <legend class="ForLegend">Buyer Details</legend>
                        <br>
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr>
                                <td class="TableBorderB" style="width: 177px">
                                    First Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtMan2" />
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                        SetFocusOnError="true" ErrorMessage="*" />
                                </td>
                                <td class="TableBorderB">
                                    Middle Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtMiddleName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB" style="width: 177px">
                                    Last Name
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txtMan2"></asp:TextBox>
                                </td>
                                <td class="TableBorderB">
                                    Buyer Code
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtBuyerCode" runat="server" CssClass="txtMan1" onblur="CheckBuyerCode(this)" />
                                    <img id="imgBuyerCode" name="BuyerCodeAvailability" src="" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Tax Id Number
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtIdNumber" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                                <td class="TableBorderB" style="width: 177px">
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
                                <td class="TableBorderB">
                                    Commission Type
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlCommisionType" runat="server" CssClass="txtMan2" DataSourceID="objCommisionType"
                                        DataTextField="CommissionType1" DataValueField="CommissionTypeId">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="objCommisionType" runat="server" SelectMethod="GetCommisionType"
                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                </td>
                                <td class="TableBorderB" style="width: 177px">
                                    Commission Value
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCommisionValue" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtCommisionValue_Ext" runat="server" FilterType="Custom,Numbers"
                                        TargetControlID="txtCommisionValue" ValidChars=".">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    State Salesman License Number
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSSLNumber" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                                <td class="TableBorderB" style="width: 177px">
                                    License Plate Number
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtLPnumber" CssClass="txtMan2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Street
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtStreet" runat="server" CssClass="txtMan2" />
                                </td>
                                <td class="TableBorderB" style="width: 177px">
                                    Suite
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtSuite" runat="server" CssClass="txtMan2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    City
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="txtMan2" />
                                </td>
                                <td class="TableBorderB" style="width: 177px">
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
                                <td class="TableBorderB" style="width: 177px">
                                    Zip
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtZip" runat="server" CssClass="txtMan2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Phone
                                </td>
                                <td class="TableBorder">
                                    <ajax:FilteredTextBoxExtender ID="txtZip_Extender" runat="server" FilterType="Numbers"
                                        TargetControlID="txtFax">
                                    </ajax:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="txtMan2"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                        FilterType="Numbers,Custom" ValidChars="+,-" TargetControlID="txtPhone">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                                <td class="TableBorderB" style="width: 177px">
                                    Cell Phone
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtCellPhone" runat="server" CssClass="txtMan2"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtCellPhone_Extender" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="+,-" TargetControlID="txtCellPhone">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Fax
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtFax" runat="server" CssClass="txtMan2"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtFax_Extender" runat="server" FilterType="Numbers"
                                        TargetControlID="txtFax">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                                <td class="TableBorderB" style="width: 177px">
                                    Other Number
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtOtherNumber" runat="server" CssClass="txtMan2"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="txtOtherNumber_Extender" runat="server" FilterType="Numbers,Custom"
                                        ValidChars="+,-" TargetControlID="txtOtherNumber">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Email
                                </td>
                                <td class="TableBorder">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtMan2" />
                                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="*" />
                                </td>
                                <td class="TableBorderB" style="width: 177px">
                                    When Commission gets paid
                                </td>
                                <td class="TableBorder">
                                    <asp:ObjectDataSource ID="objCommisionTerm" runat="server" SelectMethod="GetCommisionPaid"
                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                    <asp:DropDownList ID="ddlCGetPaid" CssClass="txtMan2" runat="server" DataSourceID="objCommisionTerm"
                                        DataTextField="CommissionTerm1" DataValueField="CommissionTermId">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Comments
                                </td>
                                <td class="TableBorder" colspan="3">
                                    <asp:TextBox ID="txtComment" runat="server" Rows="5" Width="100%" CssClass="txtMulti"
                                        TextMode="MultiLine" onkeyup="MaxCharLimit(this, 'txtcount', 255)" />
                                    <asp:TextBox ID="txtcount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                        Width="30px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Direct Buyer
                                </td>
                                <td class="TableBorder">
                                    <asp:DropDownList ID="ddlDirectBuyer" runat="server" CssClass="txt1" onchange="onDirectBuyerChange()">
                                        <asp:ListItem Text="Yes" Value="1" Selected="True" />
                                        <asp:ListItem Text="No" Value="0" />
                                    </asp:DropDownList>
                                </td>
                                <td class="TableBorderB">
                                    Parent Buyer
                                </td>
                                <td class="TableBorder">
                                    <div style="display: inline-block; float: left; padding-right: 1px">
                                        <asp:DropDownList ID="ddlParentBuyer" runat="server" CssClass="txt2" Width="140px"
                                            Enabled="false" />
                                    </div>
                                    <span id="spanParentB" runat="server" class="err" style="display: none">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Access Level
                                </td>
                                <td class="TableBorder">
                                    <div style="display: inline-block; float: left; padding-right: 1px">
                                        <asp:DropDownList ID="ddlAccessLevel" runat="server" CssClass="txt2" Enabled="false">
                                            <asp:ListItem Text="Select" Value="" Selected="True" />
                                            <asp:ListItem Text="Access Level 1" Value="1" Title="Buyer can access data belongs to them only" />
                                            <asp:ListItem Text="Access Level 2" Value="2" Title="Buyer can access data belongs to them as well as their parent" />
                                        </asp:DropDownList>
                                    </div>
                                    <span id="spanAccessLevel" runat="server" class="err" style="display: none">*</span>
                                </td>
                                <td class="TableBorder" colspan="2">
                                    &nbsp;
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
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel"
                        Width="71px" CssClass="Btn_Form" OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" Width="71px"
                        CssClass="Btn_Form" OnClientClick="return ValidatePage()" />
                    <asp:Label ID="lblError" runat="server" CssClass="err" />
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        function CheckBuyerCode(txt) {
            METAOPTION.WS.AutoFillNames.BuyerCodeAvailability(txt.value, wsSuccess, wsError)
        }

        function wsSuccess(value) {
            if (value == false) {
                document.getElementById("imgBuyerCode").src = "../Images/H_delete.png";
                document.getElementById("imgBuyerCode").title = "Not available";
                document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = "disabled";
            }
            else if (value == true) {
                document.getElementById("imgBuyerCode").src = "../Images/H_active.png";
                document.getElementById("imgBuyerCode").title = "Available";
                document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = false;
            }
        }

        function wsError(value) {
            document.getElementById("imgBuyerCode").src = "../Images/H_delete.png";
            document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = "disabled";
        }

        function ValidatePage() {
            var valid = true;

            var dirBuyer = document.getElementById('<%=ddlDirectBuyer.ClientID %>');
            var parentBuyer = document.getElementById('<%=ddlParentBuyer.ClientID %>');
            var accessLevel = document.getElementById('<%=ddlAccessLevel.ClientID %>');

            if (dirBuyer.value == "0" && parentBuyer.value == "-1") {
                document.getElementById('<%=spanParentB.ClientID %>').style.display = 'block';
                valid = false;
            }

            if (dirBuyer.value == "0" && parentBuyer.value != "-1" && accessLevel.value == "") {
                document.getElementById('<%=spanAccessLevel.ClientID %>').style.display = 'block';
                valid = false;
            }

            return valid;
        }

        function onDirectBuyerChange() {
            var dirBuyer = document.getElementById('<%=ddlDirectBuyer.ClientID %>');
            var parentBuyer = document.getElementById('<%=ddlParentBuyer.ClientID %>');
            var accessLevel = document.getElementById('<%=ddlAccessLevel.ClientID %>');
            if (dirBuyer.value == "1") {
                parentBuyer.value = "-1";
                parentBuyer.disabled = true;
                accessLevel.value = "";
                accessLevel.disabled = true;
            }
            else {
                parentBuyer.disabled = false;
                accessLevel.disabled = false;
            }
        }
    </script>
</asp:Content>
