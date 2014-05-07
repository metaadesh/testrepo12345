<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewVendor.aspx.cs"
    Inherits="METAOPTION.UI.ViewVendor" MasterPageFile="~/UI/MasterPage.Master" %>

<%@ Register Src="../UserControls/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/ExpenseDetails.ascx" TagName="ExpenseDetails" TagPrefix="uc1" %>
<script type="text/C#" runat="server">
    
    protected void Refresh_Click(object sender, EventArgs args)
    {
        //  update the grids contents
        this.BindContactDetails();

        // Response.Redirect("ViewDealer.aspx?EntityId=" + EntityId + "&type=" + type);
    }
    
</script>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphViewVendor" runat="server">
    <script type="text/javascript" src="../CSS/jquery-1.7.2.min.js"></script>
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

        function SetExpenseAutoApproval(ShowHideFlag) {
            var vendorid = $('#<%=hfvendorid.ClientID %>').val();
            var SentFlag;
            if (ShowHideFlag == "1")
                SentFlag = "0";
            else
                SentFlag = 1;

            $.ajax({ type: "POST",
                url: "ViewVendor.aspx/UpdateAutoApproval",
                data: "{Flag:" + SentFlag + ",VendorID:" + vendorid + "}",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: false,
                error: Failed,
                success: function () {
                    if (ShowHideFlag == "1") {
                        $('#ctl00_ContentPlaceHolder1_frmviewVendorDetails_imgOn').hide();
                        $('#ctl00_ContentPlaceHolder1_frmviewVendorDetails_imgOff').show();
                    }
                    else {
                        $('#ctl00_ContentPlaceHolder1_frmviewVendorDetails_imgOn').show();
                        $('#ctl00_ContentPlaceHolder1_frmviewVendorDetails_imgOff').hide();
                    }
                }
            });
        }

        function Delete_ArchiveVendor(Status) {
            var vendorid = $('#<%=hfvendorid.ClientID %>').val();
            var UserID = '<%=Session["empId"] %>';
            var Answer = true;

            if (Status == "0")
                Answer = confirm("Do you want to delete this vendor? Once deleted, it will not appear in Searchlist. Do you want to continue?");
            else if (Status == "2")
                Answer = confirm("Do you want to archive this vendor? Once archived, it will appear in Searchlist, but no transaction will happen for this vendor. Do you want to continue?");

            if (Answer) {
                $.ajax({ type: "POST",
                    url: "ViewVendor.aspx/DeleteArchiveVendor",
                    data: "{Status:" + Status + ",VendorID:" + vendorid + ",UserID:" + UserID + "}",
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
                        window.location.href = "../UI/VendorList.aspx";
                    }
                });
            }
        }

        //Failed Function
        function Failed(result) {
            alert(result.status + " " + result.statusText);
        }
        
    </script>
    <asp:HiddenField ID="hfReferrerURL" runat="server" />
    <div class="RightPanel">
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td align="right">
                    <asp:Button ID="btnDelete" runat="server" Visible="false" CssClass="Btn_Form" Text="Delete Vendor"
                        OnClientClick="javascript:return confirm('Are you sure you want to delete this Vendor? You will not be able to access this Buyer once deleted\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                        OnClick="btnDelete_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:UpdatePanel ID="upVendorDetails" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                         
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                        class="arial-12">
                        <tr>
                            <td class="TableHeadingBg">
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="TableHeading">
                                            Vendor Details
                                        </td>
                                        <td align="right" class="HeadingEditButton">
                                            <img border="0" src="../Images/Deleteblue.png" id="imgDelete" alt="close" onclick="Delete_ArchiveVendor('0');"
                                                runat="server" />
                                            <img border="0" src="../Images/Deletered.png" id="imgUnDelete" alt="close" onclick="Delete_ArchiveVendor('1');"
                                                runat="server" />
                                            <img border="0" src="../Images/Archiveblue.png" id="imgArchive" alt="close" onclick="Delete_ArchiveVendor('2');"
                                                runat="server" />
                                            <img border="0" src="../Images/ArchiveRed.png" id="imgUnArchive" alt="close" onclick="Delete_ArchiveVendor('-1');"
                                                runat="server" />
                                            <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif"
                                                ToolTip="Edit Vendor" Style="float: right; padding-right: 5px;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:FormView ID="frmviewVendorDetails" runat="server" Width="100%" DataSourceID="objVendorDetails"
                                    OnDataBound="frmviewVendorDetails_DataBound">
                                    <ItemTemplate>
                                        <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                            <tr>
                                                <td class="TableBorder" width="150">
                                                    <b>Vendor Name</b>
                                                </td>
                                                <td class="TableBorder" width="200">
                                                    <%#Eval("VendorName") %>
                                                </td>
                                                <td class="TableBorder" width="150">
                                                    <b>Vendor Category</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("VendorCategory")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Vendor DIN</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("VendorDIN")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Vendor Type</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("VendorType")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Payment Terms</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("PaymentTerm")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Tax Id Number</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("TaxIdNumber")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Accounting Code</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("AccountingCode")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Street</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Street")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Suite</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Suite")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>City</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("City")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>State</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("State")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Country</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("CountryName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Zip</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Zip")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Phone</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Phone1")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <b>Fax</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Fax")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <b>Others</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Phone2")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder" valign="top">
                                                    <b>Comments</b>
                                                </td>
                                                <td class="TableBorder" colspan="3">
                                                    <%#Eval("Comments")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder" valign="top">
                                                    <b>Expense Auto Approval</b>
                                                </td>
                                                <td class="TableBorder">
                                                    <img border="0" src="../Images/on.png" runat="server" id="imgOn" alt="close" onclick="SetExpenseAutoApproval('1');" />
                                                    <img border="0" src="../Images/off.png" runat="server" id="imgOff" alt="close" onclick="SetExpenseAutoApproval('0');" />
                                                    <img border="0" src="../Images/off.png" runat="server" id="imgOffDummy" alt="close"
                                                        style="display: none" title="If you want to enable auto approval please contact the Admin." />
                                                    <img border="0" src="../Images/on.png" runat="server" id="imgonDummy" alt="close"
                                                        style="display: none" title="If you want to enable auto approval please contact the Admin." />
                                                    <%--<asp:ImageButton ID="imgOn" runat="server" ImageUrl="~/Images/on.png" OnClientClick="SetExpenseAutoApproval($(this).attr('id'));" />
                                                    <asp:ImageButton ID="imgOff" runat="server" ImageUrl="~/Images/off.png" OnClientClick="SetExpenseAutoApproval($(this).attr('id'));" />--%>
                                                </td>
                                                <td class="TableBorder" valign="top">
                                                    <b>Email</b>
                                                </td>
                                                <td class="TableBorder" valign="top">
                                                    <%#Eval("Email1") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder" valign="top">
                                                    <b>TRP Expense Calculation</b>
                                                </td>
                                                <td class="TableBorder" valign="top" colspan="3">
                                                    <%# Convert.ToString(Convert.ToInt32(Eval("TransExpenseCalculationMethod"))) == "1" ? "Mileage" : "Location"%>
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
                                        <asp:HiddenField ID="hfExpenseAutoApproval" Value='<%# Eval("ExpenseAutoApproval") %>'
                                            runat="server" />
                                        <asp:HiddenField ID="hfIsVendorExists" Value='<%# Eval("IsExists") %>' runat="server" />
                                        <asp:HiddenField ID="hfIsActive" Value='<%# Eval("IsActive") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:FormView>
                                <asp:HiddenField ID="hfvendorid" runat="server" />
                                <asp:ObjectDataSource ID="objVendorDetails" runat="server" SelectMethod="GetVendorDetails"
                                    TypeName="METAOPTION.BAL.VendorBAL">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="VendorId" QueryStringField="EntityId" Type="Int64" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:Panel ID="pnlEditVendorDetails" Style="display: none;" Width="700" runat="server"
                                    CssClass="modalPopup">
                                    <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                        <tr style="width: 100%">
                                            <td class="PopUpBoxHeading">
                                                &nbsp;&nbsp;Edit Vendor Details
                                            </td>
                                            <td class="PopUpBoxHeading" align="right">
                                                <img id="ImageButton2" runat="server" onclick="$find('mdpop').hide();return false;"
                                                    alt="" src="../Images/close.gif" style="padding-right: 5px;" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2" style="padding: 10px">
                                                <asp:FormView ID="frmviewEditVendor" DataSourceID="objVendorDetails" runat="server"
                                                    Width="100%" OnDataBound="frmviewEditVendor_DataBound">
                                                    <ItemTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-collapse: collapse;">
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Vendor Name
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtName" Text='<%#Eval("VendorName") %>' runat="server" CssClass="txtMan2"
                                                                        EnableViewState="true"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                                                        ErrorMessage="*" SetFocusOnError="True" />
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Vendor DIN
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtDIN" runat="server" CssClass="txtMan2" Text='<%#Eval("VendorDIN") %>'
                                                                        EnableViewState="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Vendor Category
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlCategory" runat="server" SelectedValue='<%#Eval("VendorCategoryId")%>'
                                                                        CssClass="txtMan2" DataSourceID="objVendorCategory" DataTextField="VendorCategory1"
                                                                        DataValueField="VendorCategoryId" AppendDataBoundItems="True">
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:ObjectDataSource ID="objVendorCategory" runat="server" SelectMethod="GetVendorCategory"
                                                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Vendor Type
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlVenderType" AppendDataBoundItems="true" SelectedValue='<%#Eval("VendorTypeId")%>'
                                                                        runat="server" CssClass="txtMan2" DataSourceID="objVendorType" DataTextField="VendorType1"
                                                                        DataValueField="VendorTypeId">
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
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
                                                                    <asp:TextBox Text='<%# Eval("TaxIdNumber") %>' ID="txtIdNumber" runat="server" CssClass="txtMan2" />
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Payments Terms
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:DropDownList ID="ddlPaymentTerms" runat="server" AppendDataBoundItems="true"
                                                                        SelectedValue='<%#Eval("PaymentTermId")%>' CssClass="txtMan2" DataSourceID="objPaymentTerms"
                                                                        DataTextField="PaymentTerm1" DataValueField="PaymentTermId">
                                                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:ObjectDataSource ID="objPaymentTerms" runat="server" SelectMethod="GetPaymentTerms"
                                                                        TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <%-- <td class="TableBorderB">
                                                                            Accounting Code
                                                                        </td>
                                                                        <td class="TableBorder">
                                                                            <asp:TextBox ID="txtAccountingCode" Text='<%# Eval("AccountingCode") %>' runat="server"
                                                                                CssClass="txtMan2" />
                                                                        </td>--%>
                                                                <td class="TableBorderB">
                                                                    Street
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox Text='<%# Eval("Street") %>' ID="txtStreet" runat="server" CssClass="txtMan2" />
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Suite
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox Text='<%# Eval("Suite") %>' ID="txtSuite" runat="server" CssClass="txt2" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    City
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox Text='<%# Eval("City") %>' ID="txtCity" runat="server" CssClass="txt2" />
                                                                    <asp:HiddenField ID="hfieldCountry" Value='<%# Eval("CountryId")%>' runat="server" />
                                                                    <asp:HiddenField ID="hfieldState" Value='<%# Eval("StateId")%>' runat="server" />
                                                                </td>
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
                                                            </tr>
                                                            <tr>
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
                                                                <td class="TableBorderB">
                                                                    Zip
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox Text='<%# Eval("Zip") %>' ID="txtZip" runat="server" CssClass="txtMan2" />
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
                                                                    <asp:TextBox ID="txtPhone" Text='<%# Eval("Phone1") %>' runat="server" CssClass="txt2" />
                                                                    <ajax:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                                                        FilterType="Numbers,Custom" ValidChars="+() -" TargetControlID="txtPhone">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Other Number
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox Text='<%# Eval("Phone2") %>' ID="txtOtherNumber" runat="server" CssClass="txt2" />
                                                                    <ajax:FilteredTextBoxExtender ID="txtOtherNumberExtender" runat="server" FilterType="Numbers,Custom"
                                                                        ValidChars="+() -" TargetControlID="txtOtherNumber">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Fax
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox Text='<%# Eval("Fax") %>' ID="txtFax" runat="server" CssClass="txt2" />
                                                                    <ajax:FilteredTextBoxExtender ID="txtFax_Extender" runat="server" FilterType="Numbers,Custom"
                                                                        ValidChars="+() -" TargetControlID="txtFax">
                                                                    </ajax:FilteredTextBoxExtender>
                                                                </td>
                                                                <td class="TableBorderB">
                                                                    Email
                                                                </td>
                                                                <td class="TableBorder">
                                                                    <asp:TextBox ID="txtEmail" Text='<%# Eval("Email1") %>' runat="server" CssClass="txt2" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    Comments
                                                                </td>
                                                                <td class="TableBorder" colspan="3">
                                                                    <asp:TextBox Text='<%# Eval("Comments") %>' Width="100%" Rows="5" ID="txtComment"
                                                                        runat="server" CssClass="txtMan2" TextMode="MultiLine" />
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("AddressId") %>' />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TableBorderB">
                                                                    TRP Expense Calculation
                                                                </td>
                                                                <td class="TableBorder" colspan="3">
                                                                    <asp:HiddenField ID="hfExpenseCalcMethod" runat="server" Value='<%#Eval("TransExpenseCalculationMethod") %>' />
                                                                    <asp:DropDownList ID="ddlExpenseCalcMethod" runat="server" CssClass="txt2">
                                                                        <asp:ListItem Text="Mileage" Value="1" />
                                                                        <asp:ListItem Text="Location" Value="2" />
                                                                    </asp:DropDownList>
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
                                    PopupControlID="pnlEditVendorDetails" CancelControlID="btnEditCancel" BackgroundCssClass="modalBackground">
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
                                    BorderStyle="Solid" BorderWidth="0" DataKeyNames="ContactId" OnRowDeleting="grdContactDetails_RowDeleting"
                                    OnRowDataBound="grdContactDetails_RowDataBound">
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
                                                <a id="contactEdit" href='<%# "EditContact.aspx?ID="+Eval("ContactId")+"&TB_iframe=true&height=220&width=800" %>'
                                                    title="Edit Details" class="thickbox">
                                                    <img alt="" src="../Images/newedit.gif" border="0" /></a>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/DeleteButton.jpg"
                                                    OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
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
                                <a id="AddNewContact" href='<%="AddNewContact.aspx?EntityId="+EntityId+"&type="+type+"&TB_iframe=true&height=220&width=800" %>'
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
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:ExpenseDetails ID="ucexpensedetails" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FooterContentDetails">
                    <a id="ancAddExpense" runat="server" class="AddNewExpenseTxt">Add New Expense</a>
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
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <asp:GridView ID="grdCommision" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="gridView" CellPadding="4" GridLines="None" OnPageIndexChanging="grdCommision_PageIndexChanging"
                                            AllowPaging="True" PageSize="20" DataSourceID="objPayments" PagerSettings-Mode="NumericFirstLast">
                                            <Columns>
                                                <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                                                <%-- <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                            HeaderStyle-Width="20px" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnkView" ToolTip="View" NavigateUrl='<%# "ViewBuyerDetails.aspx?BuyerId="+Eval("BuyerId") %>'
                                                    runat="server" ImageUrl="~/Images/Select.gif" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                            <ItemStyle Width="20px"></ItemStyle>
                                        </asp:TemplateField>--%>
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
                                                <asp:BoundField DataField="Amount" DataFormatString="{0:c}" HeaderText="Amount" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContentRight" SortExpression="LastDesc">
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
                                                <asp:QueryStringParameter Name="entityId" QueryStringField="EntityId" Type="Int64" />
                                                <asp:QueryStringParameter Name="entityTypeId" QueryStringField="Type" Type="Int32" />
                                                <asp:Parameter Name="StartRowIndex" Type="Int32" />
                                                <asp:Parameter Name="MaximumRows" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FooterContentDetails">
                                        <a id="AddPayment" href='<%="MakeANewPayment.aspx?EntityId="+(EntityId)+"&type=3" %>'
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
                                    Expenses
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <asp:GridView ID="gvExpense" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="gridView" CellPadding="4" GridLines="None" AllowPaging="True" PageSize="50"
                                            DataSourceID="objExpenses" OnRowDataBound="gvExpense_RowDataBound" PagerSettings-Mode="NumericFirstLast">
                                            <Columns>
                                                <asp:ButtonField Text="SingleClick" Visible="false" CommandName="SingleClick" />
                                                <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ExpenseAmount" DataFormatString="{0:c}&nbsp;" HeaderText="Expense Amount"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContentRight" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContentRight"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="VIN" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkVIN" runat="server" Text='<%#Eval("VIN") %>' NavigateUrl='<%# "InventoryDetail.aspx?Code="+Eval("InventoryId")%>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ExpenseType" HeaderText="Expense Type" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CheckNumber" HeaderText="Check#" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AddedByText" HeaderText="Added By" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" SortExpression="">
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <ItemStyle CssClass="GridContent"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Comments" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnComment" runat="server" Value='<%# Eval("Comments") %>' />
                                                        <asp:Label ID="lblInvoiceNumber" runat="server" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Expense found"></asp:Label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="#E4EDF4" />
                                            <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="objExpenses" runat="server" SelectCountMethod="GetExpenseListCount"
                                            SelectMethod="GetExpenseList" EnablePaging="True" TypeName="METAOPTION.BAL.CommonBAL">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="entityId" QueryStringField="EntityId" Type="Int64" />
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
            <tr>
                <td class="height30">
                    &nbsp;
                </td>
            </tr>
            <tr id="ZoneRatestr" runat="server">
                <td>
                    <asp:UpdatePanel ID="ZoneRatesUpdate" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                class="arial-12">
                                <tr>
                                    <td class="TableHeadingBg TableHeading">
                                    Zone Rates
                                </tr>
                                <tr>
                                    <td class="TableBorder">
                                        <asp:GridView ID="gvZoneRates" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CssClass="gridView" CellPadding="4" GridLines="None" DataSourceID="ZRates">
                                            <Columns>
                                                <asp:BoundField DataField="VendorName" HeaderText="Vendor" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" />
                                                <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" />
                                                <asp:BoundField DataField="MinMileage" HeaderText="Min. Mileage" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" />
                                                <asp:BoundField DataField="MaxMileage" HeaderText="Max. Mileage" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" />
                                                <asp:BoundField DataField="Price" HeaderText="Price" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" />
                                                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Zone Rates found"></asp:Label>
                                            </EmptyDataTemplate>
                                            <AlternatingRowStyle BackColor="#E4EDF4" />
                                        </asp:GridView>
                                        <asp:ObjectDataSource ID="ZRates" runat="server" SelectMethod="GetAllZonePrice" TypeName="METAOPTION.BAL.VendorBAL">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="OrgID" SessionField="OrgID" DefaultValue="-1" />
                                                <asp:QueryStringParameter Name="EntityID" QueryStringField="EntityId" Type="Int32" />
                                                <asp:QueryStringParameter Name="EntityTypeID" QueryStringField="Type" Type="Int32" />
                                                <asp:Parameter Name="Zone" DefaultValue="-1" Type="String" />
                                                <asp:Parameter Name="Mileage" DefaultValue="-1" Type="String" />
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
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var EntityType = "<%= Session["LoginEntityTypeID"]%>";
            if (EntityType == "3") {
                $("table[id$='grdContactDetails']").find('a[id="contactEdit"]').hide();
                $('#AddNewContact').hide();               
                $('#AddPayment').hide();
            }
        });
    </script>
</asp:Content>
