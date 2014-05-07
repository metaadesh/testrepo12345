<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/MasterPage.Master"
    CodeBehind="ViewBuyerDetails.aspx.cs" Inherits="METAOPTION.UI.ViewBuyer" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RightPanel">
        <asp:HiddenField ID="hfReferrerURL" runat="server" />
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td align="right">
                    <asp:Button ID="btnDelete" runat="server" Visible="false" CssClass="Btn_Form" Text="Delete Buyer"
                        OnClientClick="javascript:return confirm('Are you sure you want to delete this Buyer? You will not be able to access this Buyer once deleted\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                        OnClick="btnDelete_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <div onmousemove="SetProgressPosition(event)">
                        <asp:UpdatePanel ID="upBuyerDetails" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:HiddenField ID="hfBuyerCode" runat="server" />
                                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="ParentID" runat="server"></asp:HiddenField>
                                            <asp:HiddenField ID="EntityTYpeID" runat="server"></asp:HiddenField>
                                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                                class="arial-12">
                                                <tr>
                                                    <td class="TableHeadingBg">
                                                        <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                                            <tr>
                                                                <td class="TableHeading">
                                                                    Buyer Details
                                                                </td>
                                                                <td align="right" class="HeadingEditButton">
                                                                    <img border="0" src="../Images/Deleteblue.png" id="imgDelete" alt="close" onclick="Delete_ArchiveBuyer('0');"
                                                                        runat="server" />
                                                                    <img border="0" src="../Images/Deletered.png" id="imgUnDelete" alt="close" onclick="Delete_ArchiveBuyer('1');"
                                                                        runat="server" />
                                                                    <img border="0" src="../Images/Archiveblue.png" id="imgArchive" alt="close" onclick="Delete_ArchiveBuyer('2');"
                                                                        runat="server" />
                                                                    <img border="0" src="../Images/ArchiveRed.png" id="imgUnArchive" alt="close" onclick="Delete_ArchiveBuyer('-1');"
                                                                        runat="server" />
                                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif"
                                                                        Style="padding-right: 5px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:FormView ID="frmviewBuyer" runat="server" Width="100%" DataSourceID="objBuyerDetails"
                                                OnDataBound="frmviewBuyer_DataBound">
                                                <ItemTemplate>
                                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                                        class="arial-12">
                                                        <tr>
                                                            <td class="TableBorder" width="22%">
                                                                <b>First Name</b>
                                                            </td>
                                                            <td class="TableBorder" width="25%">
                                                                <%# Eval("FirstName")%>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Middle Name</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Eval("MiddleName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Last Name</b>
                                                            </td>
                                                            <td class="TableBorder" width="25%">
                                                                <%# Eval("LastName")%>
                                                            </td>
                                                            <td class="TableBorderB" width="26%">
                                                                Buyer Code
                                                            </td>
                                                            <td class="TableBorder" width="20%">
                                                                <%# Eval("Buyer_Code")%>
                                                                <asp:HiddenField ID="hfBCode" runat="server" Value='<%# Eval("Buyer_Code")%>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Tax Id Number</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Eval("TaxIdNumber")%>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Payment Terms</b>
                                                            </td>
                                                            <td class="TableBorder" width="25%">
                                                                <%# Eval("PaymentTerm")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Commission Type</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:Label ID="lblCommissionType" runat="server" Text='<%# Eval("CommissionType")%>' />
                                                                <asp:HiddenField ID="hdnCommTypeId" Value='<%# Eval("CommissionTypeId")%>' runat="server" />
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
                                                                <b>Commission Rule</b>
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <asp:Label Text='<%# Eval("CommissionRule")%>' ID="lblCommissionRule" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>State Salesman License</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Eval("StateSalesmanLicenseNumber")%>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>License Plate No.</b>
                                                            </td>
                                                            <td class="TableBorder" width="25%">
                                                                <%# Eval("LicensePlateNumber")%>
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
                                                                <b>Suit</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Eval("Suite")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>City</b>
                                                            </td>
                                                            <td class="TableBorder" width="25%">
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
                                                            <td class="TableBorder" width="25%">
                                                                <%# Eval("Zip")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Cell Phone</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Eval("CellPhone")%>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Phone</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Eval("Phone1")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Fax</b>
                                                            </td>
                                                            <td class="TableBorder" width="25%">
                                                                <%# Eval("Fax")%>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Others</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Eval("Phone2")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Email</b>
                                                            </td>
                                                            <td class="TableBorder" width="25%">
                                                                <%# Eval("Email1")%>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>When commission gets paid</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Eval("CommissionTerm")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Average Cost of the Car</b>
                                                            </td>
                                                            <td class="TableBorder" width="25%">
                                                                <%# String.Format("{0:c}", Eval("AverageCarCost"))%>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Average Reconditioning Cost</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# String.Format("{0:c}", Eval("AverageReconditioningCarCost"))%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Comments</b>
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <%# Eval("Comments")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Is Direct Buyer</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Convert.ToString(Convert.ToInt32(Eval("IsDirectBuyer"))) == "1" ? "Yes" : "No"%>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Parent Buyer</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <%# Convert.ToString(Eval("ParentBuyerName")) == "" ? "None" : Eval("ParentBuyerName")%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Access Level</b>
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <%# Eval("BuyerAccess")%>
                                                            </td>
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="upBuyerDetails">
                                                                <ProgressTemplate>
                                                                    <div id="dvProg" class="overlay">
                                                                        <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />
                                                                        &nbsp;Please wait...
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
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
                                                    <asp:HiddenField ID="hfIsBuyerExists" Value='<%# Eval("IsExists") %>' runat="server" />
                                                    <asp:HiddenField ID="hfIsActive" Value='<%# Eval("IsActive") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:FormView>
                                            <asp:HiddenField ID="hfBuyerid" runat="server" />
                                            <asp:ObjectDataSource ID="objBuyerDetails" runat="server" SelectMethod="GetBuyerDetails"
                                                TypeName="METAOPTION.BAL.BuyerBAL">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="BuyerId" QueryStringField="BuyerId" Type="Int64" />
                                                    <asp:SessionParameter Name="OrgID" SessionField="OrgID" Type="Int16" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <asp:Panel ID="pnlEditBuyerDetails" Style="display: none;" runat="server" CssClass="modalPopup"
                                                Width="700">
                                                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                    <tr>
                                                        <td class="PopUpBoxHeading">
                                                            &nbsp;&nbsp;Update Buyer Details
                                                        </td>
                                                        <td class="PopUpBoxHeading" align="right">
                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" OnClientClick="$find('mdpop').hide();return false;"
                                                                ImageUrl="../Images/close.gif" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <asp:FormView ID="frmviewEditBuyer" DataSourceID="objBuyerDetails" runat="server"
                                                                Width="100%" OnDataBound="frmviewEditBuyer_DataBound">
                                                                <ItemTemplate>
                                                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                First Name
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtFirstName" Text='<%# Eval("FirstName")%>' runat="server" CssClass="txtMan2" />
                                                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtFirstName"
                                                                                    ErrorMessage="*" SetFocusOnError="True" />
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Middle Name
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtMiddleName" Text='<%# Eval("MiddleName")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Last Name
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtLastName" Text='<%# Eval("LastName")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Buyer Code
                                                                                <%--<b>Title</b>&nbsp;&nbsp;&nbsp;
                                                                            <asp:HiddenField ID="hfieldTitle" Value='<%# Eval("TitleId")%>' runat="server" />
                                                                            <asp:DropDownList ID="ddlTitle" runat="server" CssClass="txtMan1" DataSourceID="objTitle"
                                                                                DataTextField="Title1" DataValueField="TitleId" AppendDataBoundItems="True" SelectedValue='<%# Eval("TitleId")%>'>
                                                                                <asp:ListItem Text="" Value="0">
                                                                                </asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:ObjectDataSource ID="objTitle" runat="server" SelectMethod="GetTitleList" TypeName="METAOPTION.BAL.MasterBAL">
                                                                            </asp:ObjectDataSource>--%>
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtBuyerCode" Text='<%# Eval("Buyer_Code")%>' runat="server" CssClass="txtMan1" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Tax Id Number
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtIdNumber" Text='<%# Eval("TaxIdNumber")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Payments Terms
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:DropDownList ID="ddlPaymentTerms" runat="server" DataSourceID="objPaymentTerm"
                                                                                    DataTextField="PaymentTerm1" DataValueField="PaymentTermId" SelectedValue='<%# Eval("PaymentTermId") %>'
                                                                                    AppendDataBoundItems="True" CssClass="txtMan2">
                                                                                    <asp:ListItem Text="" Value="0">
                                                                                    </asp:ListItem>
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
                                                            <asp:TextBox ID="txtAccountingCode" Text='<%# Eval("AccountingCode")%>' runat="server"
                                                                CssClass="txtMan2"></asp:TextBox>
                                                        </td>--%>
                                                                            <td class="TableBorderB">
                                                                                Commission Value
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtCommisionValue" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                                                <ajax:FilteredTextBoxExtender ID="txtCommisionValue_Ext" runat="server" FilterType="Custom,Numbers"
                                                                                    TargetControlID="txtCommisionValue" ValidChars=".">
                                                                                </ajax:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Commission Type
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:DropDownList ID="ddlCommisionType" runat="server" CssClass="txtMan2" DataSourceID="objCommisionType"
                                                                                    DataTextField="CommissionType1" DataValueField="CommissionTypeId" SelectedValue='<%# Eval("CommissionTypeId") %>'
                                                                                    AppendDataBoundItems="True">
                                                                                    <asp:ListItem Text="" Value="0">
                                                                                    </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:ObjectDataSource ID="objCommisionType" runat="server" SelectMethod="GetCommisionType"
                                                                                    TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                State Salesman License
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtSSLNumber" CssClass="txtMan2" Text='<%# Eval("StateSalesmanLicenseNumber")%>'
                                                                                    runat="server"></asp:TextBox>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                License Plate No.
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtLPnumber" CssClass="txtMan2" Text='<%# Eval("LicensePlateNumber")%>'
                                                                                    runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Street
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtStreet" Text='<%# Eval("Street")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Suite
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtSuite" Text='<%# Eval("Suite")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                City
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtCity" Text='<%# Eval("City")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                                <asp:HiddenField ID="hfieldCountry" Value='<%# Eval("CountryId")%>' runat="server" />
                                                                                <asp:HiddenField ID="hfieldState" Value='<%# Eval("StateId")%>' runat="server" />
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                State
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Country
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:UpdatePanel ID="test" runat="server" UpdateMode="Conditional">
                                                                                    <ContentTemplate>
                                                                                        <asp:DropDownList ID="ddlCountry" AutoPostBack="True" runat="server" CssClass="txtMan2"
                                                                                            DataTextField="CountryName" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCountry"
                                                                                    ErrorMessage="*" InitialValue="0" SetFocusOnError="True" />
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Zip
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtZip" Text='<%# Eval("Zip")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                                <%--<ajax:FilteredTextBoxExtender ID="txtZip_Extender" runat="server" FilterType="Numbers"
                                                                                TargetControlID="txtZip">
                                                                            </ajax:FilteredTextBoxExtender>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Phone
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtPhone" Text='<%# Eval("Phone1")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                                <ajax:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                                                                    FilterType="Numbers,Custom" ValidChars="+() -" TargetControlID="txtPhone">
                                                                                </ajax:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Cell Phone
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtCellPhone" Text='<%# Eval("CellPhone")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                                <ajax:FilteredTextBoxExtender ID="txtCellPhone_Extender" runat="server" FilterType="Numbers,Custom"
                                                                                    ValidChars="+() -" TargetControlID="txtCellPhone">
                                                                                </ajax:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Fax
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtFax" Text='<%# Eval("Fax")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                                <ajax:FilteredTextBoxExtender ID="txtFax_Extender" runat="server" FilterType="Numbers,Custom"
                                                                                    ValidChars="+() -" TargetControlID="txtFax">
                                                                                </ajax:FilteredTextBoxExtender>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Other Number
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtOtherNumber" Text='<%# Eval("Phone2")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                                <ajax:FilteredTextBoxExtender ID="txtOtherNumber_Extender" runat="server" FilterType="Numbers,Custom"
                                                                                    ValidChars="+() -" TargetControlID="txtOtherNumber">
                                                                                </ajax:FilteredTextBoxExtender>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Email
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:TextBox ID="txtEmail" Text='<%# Eval("Email1")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                When Commission gets paid
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:DropDownList ID="ddlCGetPaid" runat="server" DataSourceID="objCommisionPaid"
                                                                                    DataTextField="CommissionTerm1" DataValueField="CommissionTermId" SelectedValue='<%# Eval("CommissionTermId") %>'
                                                                                    AppendDataBoundItems="True" CssClass="txtMan2">
                                                                                    <asp:ListItem Text="" Value="0">
                                                                                    </asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <asp:ObjectDataSource ID="objCommisionPaid" runat="server" SelectMethod="GetCommisionPaid"
                                                                                    TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Comments
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:TextBox ID="txtComment" runat="server" Rows="5" Width="100%" Text='<%# Eval("Comments")%>'
                                                                                    CssClass="txtMulti" TextMode="MultiLine"></asp:TextBox>
                                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("AddressId") %>' />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Direct Buyer
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:HiddenField ID="hfDirectBuyer" runat="server" Value='<%#Eval("IsDirectBuyer") %>' />
                                                                                <asp:DropDownList ID="ddlDirectBuyer" runat="server" CssClass="txt1" onchange="onDirectBuyerChange()">
                                                                                    <asp:ListItem Text="Yes" Value="1" />
                                                                                    <asp:ListItem Text="No" Value="0" />
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="TableBorderB">
                                                                                Parent Buyer
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:HiddenField ID="hfParentBuyer" runat="server" Value='<%#Eval("ParentBuyer") %>' />
                                                                                <div style="display: inline-block; float: left; padding-right: 1px">
                                                                                    <asp:DropDownList ID="ddlParentBuyer" runat="server" CssClass="txt2" />
                                                                                </div>
                                                                                <span id="spanParentB" runat="server" class="err" style="display: none">*</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="TableBorderB">
                                                                                Access Level
                                                                            </td>
                                                                            <td class="TableBorder">
                                                                                <asp:HiddenField ID="hfAccessLevel" runat="server" Value='<%#Eval("AccessLevel") %>' />
                                                                                <div style="display: inline-block; float: left; padding-right: 1px">
                                                                                    <asp:DropDownList ID="ddlAccessLevel" runat="server" CssClass="txt2">
                                                                                        <asp:ListItem Text="Select" Value="" />
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
                                                                </ItemTemplate>
                                                            </asp:FormView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="padding: 10px" align="center">
                                                            <asp:Button ID="btnEditCancel" CausesValidation="false" runat="server" Text="Cancel"
                                                                class="Btn_Form" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnUpdate" runat="server" Text="Save" class="Btn_Form" OnClick="btnUpdate_Click"
                                                                OnClientClick="return ValidateEditBuyer()" />
                                                            <asp:Label ID="lblError" runat="server" CssClass="err" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <ajax:ModalPopupExtender ID="modPopUp" runat="server" BehaviorID="mdpop" TargetControlID="imgbtnEdit"
                                                PopupControlID="pnlEditBuyerDetails" BackgroundCssClass="modalBackground" CancelControlID="btnEditCancel">
                                            </ajax:ModalPopupExtender>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="updCommission" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="TableHeadingBg TableHeading">
                                        Commission Settings
                                        <asp:ImageButton ID="ibtnEditCommissionSetting" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif"
                                            Style="padding: 0px 5px 0px 10px" ImageAlign="Right" OnClientClick="GetCommissionDetails()" />
                                        <asp:ImageButton ID="ibtnAudit" runat="server" ToolTip="View Audit Trail" ImageUrl="~/Images/Auditicon.png"
                                            ImageAlign="Right" OnClick="ibtnAudit_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <asp:GridView ID="gvCommissionSettings" runat="server" AutoGenerateColumns="false"
                                            GridLines="None" DataSourceID="odsCommissionSettings" Width="100%" EmptyDataText="No commission setting found for this buyer"
                                            CssClass="gridView">
                                            <Columns>
                                                <asp:BoundField HeaderText="Min Gross" DataField="Min_Gross" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="MinVal Gross" DataField="MinValue_Gross" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="Max Gross" DataField="Max_Gross" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="MaxVal Gross" DataField="MaxValue_Gross" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="Exact Value" DataField="Exact_Value" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="TF" DataField="Title_fee_5050" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="RF" DataField="Recon_fee_5050" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="Inventory Id" DataField="InventoryId" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="Fixed Commission" DataField="FixedCommission" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                                <asp:BoundField HeaderText="SBC" DataField="SecondBuyerCommission_5050Split" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentNumbers" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="odsCommissionSettings" runat="server" SelectMethod="GetCommissionSetting"
                                            TypeName="METAOPTION.BAL.BuyerBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="buyerId" QueryStringField="BuyerId" Type="Int64" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FooterContentDetails">
                                        <%-- <a href="#" class="AddNewExpenseTxt">View All Purchased Car</a>--%>
                                        <asp:LinkButton ID="lbtnSetupCommission" runat="server" Text="Setup Commission" CssClass="AddNewExpenseTxt"
                                            OnClientClick="GetCommissionDetails()" />
                                    </td>
                                </tr>
                            </table>
                            <%--Edit Commission Settings Panel BEGIN--%>
                            <asp:Panel ID="pnlEditCommissionSetting" Style="display: none;" runat="server" CssClass="modalPopup"
                                Width="750">
                                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                    <tr>
                                        <td class="PopUpBoxHeading">
                                            &nbsp;&nbsp;Update Commission Settings
                                        </td>
                                        <td class="PopUpBoxHeading" align="right">
                                            <asp:ImageButton ID="ibtnCloseCommissionSettingPopUp" runat="server" CausesValidation="false"
                                                OnClientClick="$find('behCommissionPopup').hide();return false;" ImageUrl="../Images/close.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <asp:FormView ID="fvEditCommissionSetting" DataSourceID="odsCommissionSettings" runat="server"
                                                Width="100%">
                                                <ItemTemplate>
                                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                        <tr>
                                                            <td class="TableBorderHeader">
                                                                Commission Type:-
                                                            </td>
                                                            <td class="TableBorderHeader" colspan="5">
                                                                <asp:Label ID="lblBuyerComTypeEdit" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderHeader">
                                                                Commission Rule:-
                                                            </td>
                                                            <td class="TableBorderHeader" colspan="5">
                                                                <asp:Label ID="lblBuyerComRuleEdit" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Min_Gross
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtMinGrossEdit" Text='<%# Eval("Min_Gross")%>' runat="server" CssClass="txtMan1" />
                                                                <ajax:FilteredTextBoxExtender ID="ftbMinGrossEdit" runat="server" TargetControlID="txtMinGrossEdit"
                                                                    FilterType="Numbers" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                MinValue_Gross
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtMinValueGrossEdit" Text='<%# Eval("MinValue_Gross")%>' runat="server"
                                                                    CssClass="txtMan1" />
                                                                <ajax:FilteredTextBoxExtender ID="ftbMinValueGrossEdit" runat="server" TargetControlID="txtMinValueGrossEdit"
                                                                    FilterType="Numbers" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Max_Gross
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtMaxGrossEdit" Text='<%# Eval("Max_Gross")%>' runat="server" CssClass="txtMan1" />
                                                                <ajax:FilteredTextBoxExtender ID="ftbMaxGrossEdit" runat="server" TargetControlID="txtMaxGrossEdit"
                                                                    FilterType="Numbers" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                MaxValue_Gross
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtMaxValueGrossEdit" Text='<%# Eval("MaxValue_Gross")%>' runat="server"
                                                                    CssClass="txtMan1" />
                                                                <ajax:FilteredTextBoxExtender ID="ftbMaxValueGrossEdit" runat="server" TargetControlID="txtMaxValueGrossEdit"
                                                                    FilterType="Numbers" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Title_fee_5050
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtTitlefee5050Edit" Text='<%# Eval("Title_fee_5050")%>' runat="server"
                                                                    CssClass="txtMan1" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Recon_fee_5050
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtReconfee5050Edit" Text='<%# Eval("Recon_fee_5050")%>' runat="server"
                                                                    CssClass="txtMan1" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                InventoryId
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtInventoryIdEdit" Text='<%# Eval("InventoryId")%>' runat="server"
                                                                    CssClass="txtMan1" />
                                                                <ajax:FilteredTextBoxExtender ID="ftbInventoryIdEdit" runat="server" TargetControlID="txtInventoryIdEdit"
                                                                    FilterType="Numbers" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                FixedCommission
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtFixedCommissionEdit" Text='<%# Eval("FixedCommission")%>' runat="server"
                                                                    CssClass="txtMan1" />
                                                            </td>
                                                            <td class="TableBorderB">
                                                                SecondBuyerCommission_5050Split
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtSecondBuyerCommission5050SplitEdit" Text='<%# Eval("SecondBuyerCommission_5050Split")%>'
                                                                    runat="server" CssClass="txtMan1" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Exact Value
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtExactValueEdit" Text='<%# Eval("Exact_Value")%>' runat="server"
                                                                    CssClass="txtMan1" />
                                                                <ajax:FilteredTextBoxExtender ID="ftbExactValueEdit" runat="server" TargetControlID="txtExactValueEdit"
                                                                    FilterType="Numbers" />
                                                            </td>
                                                            <td class="TableBorder" colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:FormView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 10px" align="center">
                                            <asp:Button ID="btnCancelEditCommission" CausesValidation="false" runat="server"
                                                Text="Cancel" class="Btn_Form" Width="70px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnUpdateCommissionSetting" runat="server" Text="Save" class="Btn_Form"
                                                OnClick="btnUpdateCommissionSetting_Click" Width="70px" />
                                            <asp:Label ID="lblCommisionSetting" runat="server" CssClass="err" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <ajax:ModalPopupExtender ID="mpeEditCommissionSetting" runat="server" BehaviorID="behCommissionPopup"
                                TargetControlID="ibtnEditCommissionSetting" PopupControlID="pnlEditCommissionSetting"
                                BackgroundCssClass="modalBackground" CancelControlID="btnCancelEditCommission">
                            </ajax:ModalPopupExtender>
                            <%--Edit Commission Settings Panel END--%>
                            <%--Audit History PopUp Begin--%>
                            <asp:Panel ID="pnlAudit" CssClass="modalPopup" Style="display: none; height: auto;
                                width: 800px;" runat="server" HorizontalAlign="Left">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="PopUpBoxHeading">
                                            &nbsp;&nbsp;AUDIT
                                        </td>
                                        <td class="PopUpBoxHeading" align="right">
                                            <img id="imgCloseAudit" border="0" src="../Images/close.gif" alt="close" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="gvAudit" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                Width="100%" RowStyle-CssClass="gvRow" CssClass="Grid" AlternatingRowStyle-CssClass="gvAlternateRow"
                                                EmptyDataText="No record found" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvAudit_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Column" DataField="ColumnName" ItemStyle-CssClass="GridContent"
                                                        HeaderStyle-CssClass="GridContent" SortExpression="ColumnName" />
                                                    <asp:BoundField HeaderText="Old Value" DataField="OldValue" ItemStyle-CssClass="GridContent"
                                                        HeaderStyle-CssClass="GridContent" />
                                                    <asp:BoundField HeaderText="New Value" DataField="NewValue" ItemStyle-CssClass="GridContent"
                                                        HeaderStyle-CssClass="GridContent" />
                                                    <asp:TemplateField HeaderText="Updated From" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"
                                                        ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <%#Eval("Source")%>
                                                            (<%#Eval("UpdateFrom")%>)
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Modified On" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridContent"
                                                        ItemStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <%#Eval("ModifiedDate")%>
                                                            <br />
                                                            <%#Eval("ModifiedBy")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="gvEmpty" />
                                                <HeaderStyle CssClass="gvHeading" />
                                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:HiddenField ID="hfAudit" runat="server" />
                            <ajax:ModalPopupExtender ID="mpeAudit" runat="server" BackgroundCssClass="modalBackground"
                                TargetControlID="hfAudit" PopupControlID="pnlAudit" CancelControlID="imgCloseAudit">
                            </ajax:ModalPopupExtender>
                            <%--Audit History PopUp End--%>
                            <%--Add Commission Settings Panel BEGIN--%>
                            <asp:Panel ID="pnlAddCommissionSetting" Style="display: none;" runat="server" CssClass="modalPopup"
                                Width="750">
                                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                    <tr>
                                        <td class="PopUpBoxHeading">
                                            &nbsp;&nbsp;Add Commission Settings
                                        </td>
                                        <td class="PopUpBoxHeading" align="right">
                                            <asp:ImageButton ID="ibtnCloseAddComSettingPopUp" runat="server" CausesValidation="false"
                                                OnClientClick="$find('behAddCommissionPopup').hide();return false;" ImageUrl="../Images/close.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                <tr>
                                                    <td class="TableBorderHeader">
                                                        Commission Type:-
                                                    </td>
                                                    <td colspan="5" class="TableBorderHeader">
                                                        <asp:Label ID="lblComTypeAdd" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderHeader">
                                                        Commission Rule:-
                                                    </td>
                                                    <td colspan="5" class="TableBorderHeader">
                                                        <asp:Label ID="lblComRuleAdd" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderB">
                                                        Min_Gross
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtMinGross" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                    <td class="TableBorderB">
                                                        MinValue_Gross
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtMinValueGross" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                    <td class="TableBorderB">
                                                        Max_Gross
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtMaxGross" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderB">
                                                        MaxValue_Gross
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtMaxValueGross" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                    <td class="TableBorderB">
                                                        Title_fee_5050
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtTitlefee5050" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                    <td class="TableBorderB">
                                                        Recon_fee_5050
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtReconfee5050" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderB">
                                                        InventoryId
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtInventoryId" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                    <td class="TableBorderB">
                                                        FixedCommission
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtFixedCommission" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                    <td class="TableBorderB">
                                                        SecondBuyerCommission_5050Split
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtSecondBuyerCommission5050Split" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderB">
                                                        Exact Value
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtExactValue" runat="server" CssClass="txtMan1" />
                                                    </td>
                                                    <td class="TableBorder" colspan="4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 10px" align="center">
                                            <asp:Button ID="btnCancelAddCommission" CausesValidation="false" runat="server" Text="Cancel"
                                                class="Btn_Form" Width="70px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSaveCommissionSetting" runat="server" Text="Save" class="Btn_Form"
                                                OnClick="btnSaveCommissionSetting_Click" Width="70px" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <ajax:ModalPopupExtender ID="mpeAddCommSetting" runat="server" BehaviorID="behAddCommissionPopup"
                                TargetControlID="lbtnSetupCommission" PopupControlID="pnlAddCommissionSetting"
                                BackgroundCssClass="modalBackground" CancelControlID="btnCancelAddCommission">
                            </ajax:ModalPopupExtender>
                            <%--Edit Commission Settings Panel END--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="upBuyerOutstanding" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                class="arial-12">
                                <tr>
                                    <td class="TableHeadingBg TableHeading">
                                        <span style="text-transform: uppercase"><b>
                                            <asp:Label ID="lblTotalOutstanding" runat="server" Text=""></asp:Label></b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <asp:GridView ID="gvBuyerOutstandings" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CssClass="gridView" CellPadding="4" GridLines="None" AllowPaging="True"
                                            DataSourceID="objOutstandingTransactions" PageSize="25" PagerSettings-Mode="NumericFirstLast">
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text=" No Payment Outstandings found against this buyer"></asp:Label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="#E4EDF4" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <Columns>
                                                <asp:BoundField DataField="DateAdded" HeaderStyle-CssClass="GridHeader" HeaderText="Date Added"
                                                    SortExpression="DateAdded">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader" HeaderText="Check No."
                                                    ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnk" runat="server" Text='<%#Eval("CheckNumber")%>' NavigateUrl='<%#"~/UI/ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId")%>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeader" Width="80px"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CheckAmount" DataFormatString="{0:C}" HeaderStyle-CssClass="GridHeader"
                                                    HeaderText="Check Amount" SortExpression="CheckAmount">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight" HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ExpenseAmount" DataFormatString="{0:C}" HeaderStyle-CssClass="GridHeader"
                                                    HeaderText="Expense Amount" SortExpression="ExpenseAmount">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight" HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CurrentOutstandingAmount" DataFormatString="{0:C}" HeaderStyle-CssClass="GridHeader"
                                                    HeaderText="Current Outstanding Amount" SortExpression="CurrentOutstandingAmount">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight" HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PreviousOutstanding" DataFormatString="{0:C}" HeaderStyle-CssClass="GridHeader"
                                                    HeaderText="Previous Outstanding" SortExpression="PreviousOutstanding">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight" HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TotalOutstandingAmount" DataFormatString="{0:C}" HeaderStyle-CssClass="GridHeader"
                                                    HeaderText="Total Outstanding Amount" SortExpression="TotalOutstandingAmount">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight" HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="objOutstandingTransactions" runat="server" SelectMethod="GetBuyerOutstandingDetails"
                                            TypeName="METAOPTION.BAL.BuyerBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="buyerId" QueryStringField="BuyerId" Type="Int64" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="upCars" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                class="arial-12">
                                <tr>
                                    <td class="TableHeadingBg TableHeading">
                                        <span style="text-transform: uppercase"><b>Cars Purchased</b></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <asp:GridView ID="grdViewCars" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="gridView" CellPadding="4" GridLines="None" AllowPaging="True" DataSourceID="objCars"
                                            PageSize="25" PagerSettings-Mode="NumericFirstLast">
                                            <Columns>
                                                <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                                                <asp:TemplateField HeaderText="VIN Number" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkVIN" runat="server" Text='<%#Eval("VIN") %>' NavigateUrl='<%# "InventoryDetail.aspx?Code="+Eval("InventoryId") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="Year">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VINDivisionName" HeaderText="Make" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VINModelName" HeaderText="Model" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Date" HeaderText="Purchase Date" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CarCost" DataFormatString="{0:$#,##}&nbsp;" HeaderText="Car Cost"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight" HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Commission" HeaderText="Commission" HeaderStyle-CssClass="GridHeader"
                                                    DataFormatString="{0:c}&nbsp;" ItemStyle-CssClass="GridContentRight" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text=" No Cars Purchased by this user"></asp:Label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="#E4EDF4" />
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="objCars" runat="server" SelectCountMethod="GetPurchasedCarsByBuyerCount"
                                            SelectMethod="GetPurchasedCarsByBuyer" EnablePaging="True" TypeName="METAOPTION.BAL.BuyerBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="BuyerId" QueryStringField="BuyerId" Type="Int64" />
                                                <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                                <asp:Parameter Name="MaximumRows" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FooterContentDetails">
                                        <%-- <a href="#" class="AddNewExpenseTxt">View All Purchased Car</a>--%>
                                        <asp:LinkButton ID="lnkViewAllPurchasedCars" runat="server" Text="View All Purchased Cars"
                                            CssClass="AddNewExpenseTxt" OnClick="lnkViewAllPurchasedCars_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                                            DataSourceID="objPayments" PagerSettings-Mode="NumericFirstLast">
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
                                                <asp:BoundField DataField="Amount" DataFormatString="{0:c}&nbsp;" HeaderText="Amount"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
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
                                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text=" No Commission Paid"></asp:Label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="#E4EDF4" />
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="objPayments" runat="server" SelectCountMethod="GetPaymentsCount"
                                            SelectMethod="GetPayments" EnablePaging="True" TypeName="METAOPTION.BAL.CommonBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="entityId" QueryStringField="BuyerID" Type="Int64" />
                                                <asp:QueryStringParameter Name="entityTypeId" QueryStringField="type" Type="Int32" />
                                                <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                                <asp:Parameter Name="MaximumRows" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FooterContentDetails">
                                        <a id="ancrPayment" href='<%="MakeANewPayment.aspx?EntityId="+(EntityId)+"&type=2" %>'
                                            class="AddNewExpenseTxt">Add New Payment</a>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                class="arial-12">
                                <tr>
                                    <td class="TableHeadingBg TableHeading">
                                    Commissions
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="gridView" CellPadding="4" GridLines="None" AllowPaging="True" PageSize="25"
                                            DataSourceID="objExpenses" PagerSettings-Mode="NumericFirstLast" OnRowDataBound="gvExpense_RowDataBound"
                                            OnPageIndexChanging="gvExpense_PageIndexChanging">
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <Columns>
                                                <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                                                <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ExpenseAmount" DataFormatString="{0:c}&nbsp;" HeaderText="Commission"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="VIN" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkVIN" runat="server" Text='<%#Eval("VIN") %>' NavigateUrl='<%# "InventoryDetail.aspx?Code="+Eval("InventoryId")%>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Check#" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hylnkCheckNo" runat="server" Text='<%#Eval("CheckNumber") %>'
                                                            NavigateUrl='<%# "ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="ExpenseID" HeaderText="Expense ID" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" />--%>
                                                <asp:BoundField DataField="AddedByText" HeaderText="Added By" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="lnkShowBuyerCalculation" runat="server" ImageUrl="~/Images/hist-icon-lane.jpg"
                                                            ToolTip="Display Commission Calculation Information" CausesValidation="false" />
                                                        <asp:HiddenField ID="hdExpenseId" runat="server" Value='<%#Eval("ExpenseID") %>' />
                                                        <asp:HiddenField ID="hdInventoryId" runat="server" Value='<%#Eval("InventoryId") %>' />
                                                        <asp:Panel ID="pnlShowCommissionDetails" Style="display: none;" runat="server" CssClass="modalPopup"
                                                            Width="700">
                                                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                                <tr>
                                                                    <td class="PopUpBoxHeading">
                                                                        &nbsp;
                                                                        <asp:Label ID="lblHeaderInventoryInfo" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="PopUpBoxHeading" align="right">
                                                                        <asp:ImageButton ID="imgCloseBuyerCalcPopUp" runat="server" CausesValidation="false"
                                                                            ImageUrl="../Images/close.gif" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:FormView ID="frmBCommissionCalculationDetails" runat="server" DataKeyNames="BuyerCommissionId"
                                                                            Width="100%" OnDataBound="frmBCommissionCalculationDetails_DataBound" DataSourceID="objBuyerCommissionCalculation">
                                                                            <ItemTemplate>
                                                                                <table border="0" cellpadding="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                                                                    <tr>
                                                                                        <td class="TableBorderB" width="25%">
                                                                                            Buyer Name
                                                                                        </td>
                                                                                        <td class="TableBorder" width="75%">
                                                                                            <%# Eval("BuyerName")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="TableBorderB" width="25%">
                                                                                            Commission Type
                                                                                        </td>
                                                                                        <td class="TableBorder" width="75%">
                                                                                            <asp:Label ID="lblCommissionTypeId" Width="1%" Text='<%# Eval("CommissionTypeId")%>'
                                                                                                runat="server"></asp:Label>
                                                                                            <%# Eval("CommissionType")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="TableBorderB">
                                                                                            Formulae Description
                                                                                        </td>
                                                                                        <td class="TableBorder">
                                                                                            <asp:Label ID="lblCommissionRuleDesc" Text='<%# Eval("Description")%>' runat="server"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="tr5050" runat="server">
                                                                                        <td colspan="2">
                                                                                            <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                                                                                                <tr>
                                                                                                    <td class="TableBorderB" width="25%">
                                                                                                        Deposit Amount
                                                                                                    </td>
                                                                                                    <td class="TableBorder" width="25%">
                                                                                                        <%# String.Format("{0:C}",Eval("DepositAmount")?? "&nbsp;&nbsp;")%>
                                                                                                    </td>
                                                                                                    <td class="TableBorderB" width="25%">
                                                                                                        Car Cost (expense amt)
                                                                                                    </td>
                                                                                                    <td class="TableBorder" width="25%">
                                                                                                        <%# String.Format("{0:C}", Eval("ExpenseAmount") ?? "&nbsp;&nbsp;")%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="TableBorderB" nowrap="nowrap">
                                                                                                        <asp:Label ID="lblExpensesText" runat="server" Text="Expenses"></asp:Label>
                                                                                                    </td>
                                                                                                    <td class="TableBorder">
                                                                                                        <%# String.Format("{0:C}", Eval("TotalInventoryExpense") ?? "&nbsp;&nbsp;")%>
                                                                                                    </td>
                                                                                                    <td class="TableBorderB">
                                                                                                        Title Fee
                                                                                                    </td>
                                                                                                    <td class="TableBorder">
                                                                                                        <%# String.Format("{0:C}", Eval("Title_Fee") ?? "&nbsp;&nbsp;")%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trReconFee" runat="server">
                                                                                                    <td class="TableBorderB">
                                                                                                        Recon Fee
                                                                                                    </td>
                                                                                                    <td class="TableBorder" colspan="3">
                                                                                                        <%# Eval("Recon_fee") ?? "&nbsp;&nbsp;"%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <%--<tr>
                                                                                                    <td class="TableBorderB">
                                                                                                        Recon Fee
                                                                                                    </td>
                                                                                                    <td class="TableBorder" colspan="3">
                                                                                                        <%# Eval("Recon_fee")?? "&nbsp;&nbsp;"%>
                                                                                                    </td>
                                                                                                </tr>--%>
                                                                                                <tr id="tr5050IIndLevelComm" runat="server">
                                                                                                    <td class="TableBorderB">
                                                                                                        First Level Commission Buyer (if any)
                                                                                                    </td>
                                                                                                    <td class="TableBorder">
                                                                                                        <%# Eval("FirstCommission_BuyerId_5050Split") ?? "&nbsp;&nbsp;"%>
                                                                                                    </td>
                                                                                                    <td class="TableBorderB">
                                                                                                        Other Buyer Commission (if any)
                                                                                                    </td>
                                                                                                    <td class="TableBorder">
                                                                                                        <%# String.Format("{0:C}", Eval("SecondBuyerCommission_5050Split") ?? "&nbsp;&nbsp;")%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td class="TableBorderB">
                                                                                                        IInd Level Buyer (if any)
                                                                                                    </td>
                                                                                                    <td class="TableBorder" colspan="3">
                                                                                                        <%# Eval("SecondCommission_BuyerId") ?? "&nbsp;&nbsp;"%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trGrossProfit" runat="server">
                                                                                        <td colspan="2">
                                                                                            <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                                                                                                <tr>
                                                                                                    <td class="TableBorderB" width="25%">
                                                                                                        Sold Price
                                                                                                    </td>
                                                                                                    <td class="TableBorder" width="25%">
                                                                                                        <%# String.Format("{0:C}", Eval("SoldPrice") ?? "&nbsp;&nbsp;")%>
                                                                                                    </td>
                                                                                                    <td class="TableBorderB" width="25%">
                                                                                                        Car Cost (expense amount)
                                                                                                    </td>
                                                                                                    <td class="TableBorder" width="25%">
                                                                                                        <%# String.Format("{0:C}", Eval("ExpenseAmount") ?? "&nbsp;&nbsp;")%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <%--<tr>
                                                                                                        <td class="TableBorderB">
                                                                                                            Min
                                                                                                        </td>
                                                                                                        <td class="TableBorder">
                                                                                                            <%# Eval("Min_Gross")?? "&nbsp;&nbsp;"%>
                                                                                                        </td>
                                                                                                        <td class="TableBorderB">
                                                                                                            Min Value
                                                                                                        </td>
                                                                                                        <td class="TableBorder">
                                                                                                            <%# Eval("MinValue_Gross")?? "&nbsp;&nbsp;"%>
                                                                                                        </td>
                                                                                                    </tr>--%>
                                                                                                <%-- <tr>
                                                                                                        <td class="TableBorderB">
                                                                                                            Max
                                                                                                        </td>
                                                                                                        <td class="TableBorder">
                                                                                                            <%# Eval("Max_Gross")?? "&nbsp;&nbsp;"%>
                                                                                                        </td>
                                                                                                        <td class="TableBorderB">
                                                                                                            Max Value
                                                                                                        </td>
                                                                                                        <td class="TableBorder">
                                                                                                            <%# Eval("MaxValue_Gross")?? "&nbsp;&nbsp;"%>
                                                                                                        </td>
                                                                                                    </tr>--%>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trFixedCommission" runat="server">
                                                                                        <td class="TableBorderB">
                                                                                            Fixed Commission
                                                                                        </td>
                                                                                        <td class="TableBorder">
                                                                                            <%# String.Format("{0:C}", Eval("FixedCommission") ?? "&nbsp;&nbsp;")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="TableBorderB">
                                                                                            Buyer Commission Amount
                                                                                        </td>
                                                                                        <td class="TableBorderB">
                                                                                            <%# String.Format("{0:C}", Eval("CommissionAmount") ?? "&nbsp;&nbsp;")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ItemTemplate>
                                                                        </asp:FormView>
                                                                        <asp:ObjectDataSource ID="objBuyerCommissionCalculation" runat="server" SelectMethod="GetBuyerComm_CalculationInformation"
                                                                            TypeName="METAOPTION.BAL.BuyerBAL">
                                                                            <SelectParameters>
                                                                                <asp:QueryStringParameter Name="buyerId" QueryStringField="BuyerId" Type="Int64" />
                                                                                <asp:ControlParameter ControlID="ParentID" Name="ParentBuyerID" PropertyName="Value"
                                                                                    Type="Int32" />
                                                                                <asp:ControlParameter ControlID="EntityTYpeID" Name="EntityTypeID" PropertyName="Value"
                                                                                    Type="Int32" />
                                                                                <asp:ControlParameter ControlID="hdExpenseId" Name="expenseId" PropertyName="Value"
                                                                                    Type="Int64" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <ajax:ModalPopupExtender ID="MPEBuyerCommCalculation" runat="server" TargetControlID="lnkShowBuyerCalculation"
                                                            PopupControlID="pnlShowCommissionDetails" CancelControlID="imgCloseBuyerCalcPopUp"
                                                            PopupDragHandleControlID="pnlShowCommissionDetails" DropShadow="false" BackgroundCssClass="modalBackground">
                                                        </ajax:ModalPopupExtender>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Commission found"></asp:Label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="#E4EDF4" />
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="objExpenses" runat="server" SelectCountMethod="GetExpenseListCount"
                                            SelectMethod="GetExpenseList" EnablePaging="True" TypeName="METAOPTION.BAL.CommonBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="entityId" QueryStringField="BuyerId" Type="Int64" />
                                                <asp:QueryStringParameter Name="entityTypeId" QueryStringField="Type" Type="Int32" />
                                                <asp:QueryStringParameter Name="expenseTypeId" QueryStringField="ExpenseType" Type="Int32"
                                                    DefaultValue="-1" />
                                                <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                                <asp:Parameter Name="MaximumRows" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FooterContentDetails">
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
        function pageLoad() {
        onDirectBuyerChange();
        var EntityType = "<%= Session["LoginEntityTypeID"]%>";
            if (EntityType == "2") {             
                $('#ancrPayment').hide();
              
            }
        }
       window.onload = pageLoad;

        function Delete_ArchiveBuyer(Status) {
            var Buyerid = $('#<%=hfBuyerid.ClientID %>').val();
            var UserID = '<%=Session["empId"] %>';
            var Answer = true;

            if (Status == "0")
                Answer = confirm("Do you want to delete this Buyer? Once deleted, it will not appear in Searchlist. Do you want to continue?");
            else if (Status == "2")
                Answer = confirm("Do you want to archive this Buyer? Once archived, it will appear in Searchlist, but no transaction will happen for this Buyer. Do you want to continue?");

            if (Answer) {
                $.ajax({ type: "POST",
                    url: "ViewBuyerDetails.aspx/DeleteArchiveBuyer",
                    data: "{Status:" + Status + ",BuyerID:" + Buyerid + ",UserID:" + UserID + "}",
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
                        window.location.href = "../UI/BuyerList.aspx";
                    }
                });
            }
        }

        //Failed Function
        function Failed(result) {
            alert(result.status + " " + result.statusText);
        }

        function ValidateEditBuyer() {
            var valid = true;

            var dirBuyer = document.getElementById('ctl00_ContentPlaceHolder1_frmviewEditBuyer_ddlDirectBuyer');
            var parentBuyer = document.getElementById('ctl00_ContentPlaceHolder1_frmviewEditBuyer_ddlParentBuyer');
            var accessLevel = document.getElementById('ctl00_ContentPlaceHolder1_frmviewEditBuyer_ddlAccessLevel');

            if (dirBuyer.value == "0" && parentBuyer.value == "-1") {
                ctl00_ContentPlaceHolder1_frmviewEditBuyer_spanParentB.style.display = 'block';                
                valid = false;
            }

            if (dirBuyer.value == "0" && parentBuyer.value != "-1" && accessLevel.value == "") {
                ctl00_ContentPlaceHolder1_frmviewEditBuyer_spanAccessLevel.style.display = 'block';                
                valid = false;
            }

            return valid;
        }

        function SetProgressPosition(e) {
            var posx = 0;
            var posy = 0;
            if (!e) var e = window.event;
            if (e.pageX || e.pageY) {
                posx = e.pageX;
                posy = e.pageY;
            }
            else if (e.clientX || e.clientY) {
                posx = e.clientX + document.documentElement.scrollLeft;
                posy = e.clientY + document.documentElement.scrollTop;
            }
            document.getElementById('dvProg').style.left = posx + 10 + "px";
            document.getElementById('dvProg').style.top = posy + "px";
        }

        function onDirectBuyerChange() {
            var dirBuyer = document.getElementById('ctl00_ContentPlaceHolder1_frmviewEditBuyer_ddlDirectBuyer');
            var parentBuyer = document.getElementById('ctl00_ContentPlaceHolder1_frmviewEditBuyer_ddlParentBuyer');
            var accessLevel = document.getElementById('ctl00_ContentPlaceHolder1_frmviewEditBuyer_ddlAccessLevel');
            if (dirBuyer.value == "1")
            {
                parentBuyer.value = "-1";
                parentBuyer.disabled = true;
                accessLevel.value = "";
                accessLevel.disabled = true;
            }                 
            else
            {
                parentBuyer.disabled = false;
                accessLevel.disabled = false;
            }
        }
    </script>
    <script type="text/javascript">
        function GetCommissionDetails() {
            var comType = $('span[id$="lblCommissionType"]').html();
            var comRule = $('span[id$="lblCommissionRule"]').html();

            $('span[id$="lblComTypeAdd"]').html(comType);
            $('span[id$="lblComRuleAdd"]').html(comRule);
            $('span[id$="lblBuyerComTypeEdit"]').html(comType);
            $('span[id$="lblBuyerComRuleEdit"]').html(comRule);

        }
    </script>
</asp:Content>
