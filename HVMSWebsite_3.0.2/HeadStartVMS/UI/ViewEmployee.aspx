<%@ Page Language="C#"  AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ViewEmployee.aspx.cs" Inherits="METAOPTION.UI.ViewEmployee"
    Title="HeadStart VMS::View Employee" %>

<script type="text/C#" runat="server">
    
    protected void Refresh_Click(object sender, EventArgs args)
    {
        //  update the grids contents
        //this.BindComments();
        Response.Redirect("ViewEmployee.aspx?EmployeeId=" + Request["EmployeeId"]);

    }
    
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <asp:HiddenField ID="hfEmployeeId" runat="server" />
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                    class="arial-12">
                    <tr>
                        <td class="TableHeadingBg">
                            <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                <tr>
                                    <td class="TableHeading">
                                        Employee Details
                                    </td>
                                    <td align="right" class="HeadingEditButton">
                                        <img border="0" src="../Images/Deleteblue.png" id="imgDelete" alt="close" onclick="Delete_ArchiveEmployee('0');"
                                            runat="server" />
                                        <img border="0" src="../Images/Deletered.png" id="imgUnDelete" alt="close" onclick="Delete_ArchiveEmployee('1');"
                                            runat="server" />
                                        <img border="0" src="../Images/Archiveblue.png" id="imgArchive" alt="close" onclick="Delete_ArchiveEmployee('2');"
                                            runat="server" />
                                        <img border="0" src="../Images/ArchiveRed.png" id="imgUnArchive" alt="close" onclick="Delete_ArchiveEmployee('-1');"
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
                            <asp:FormView ID="frmViewEmployeeDetails" runat="server" Width="100%" OnDataBound="frmviewEmployee_DataBound"
                                DataSourceID="objEmployeeDetails">
                                <ItemTemplate>
                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                        class="arial-12">
                                        <tr>
                                            <td class="TableBorder" width="23%">
                                                <b>Title</b>
                                            </td>
                                            <td class="TableBorder" width="20%">
                                                <%# Eval("Title")%>
                                            </td>
                                            <td class="TableBorder" width="27%">
                                                <b>First Name</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("FirstName")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <b>Middle Name</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("MiddleName")%>
                                            </td>
                                            <td class="TableBorder">
                                                <b>Last Name</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("LastName")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <b>Employee Code</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("EmployeeCode")%>
                                            </td>
                                            <td class="TableBorder">
                                                <b>Employee Type</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("EmployeeType")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <b>Driver License State</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("LState")%>
                                            </td>
                                            <td class="TableBorder">
                                                <b>Driver License Number</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("DriverLicenseNumber")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <b>Driver License Expiration</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("DriverLicensExpDate")%>
                                            </td>
                                            <td class="TableBorder">
                                                <b>Street</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("Street")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <b>Suite</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("Suite")%>
                                            </td>
                                            <td class="TableBorder">
                                                <b>City</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("City")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <b>State</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("State")%>
                                            </td>
                                            <td class="TableBorder">
                                                <b>Country</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("CountryName")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <b>Zip</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("Zip")%>
                                            </td>
                                            <td class="TableBorder">
                                                <b>Phone</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("Phone1")%>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder">
                                                <b>Ext.</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("Phone1Ext")%>
                                            </td>
                                            <td class="TableBorder">
                                                <b>Cell Phone</b>
                                            </td>
                                            <td class="TableBorder" width="24%">
                                                <%# Eval("CellPhone")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder" valign="top">
                                                <b>Email</b>
                                            </td>
                                            <td class="TableBorder">
                                                <%# Eval("Email1")%>
                                            </td>
                                            <td class="TableBorder">
                                                &nbsp;
                                            </td>
                                            <td class="TableBorder" width="24%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder" valign="top">
                                                <b>Special Payroll Conditions</b>
                                            </td>
                                            <td class="TableBorder" colspan="3">
                                                <%# Eval("SpecialPayrollConditions")%>
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
                                    <asp:HiddenField ID="hfIsEmployeeExists" Value='<%# Eval("IsExists") %>' runat="server" />
                                    <asp:HiddenField ID="hfIsActive" Value='<%# Eval("IsActive") %>' runat="server" />
                                </ItemTemplate>
                            </asp:FormView>
                            <asp:ObjectDataSource ID="objEmployeeDetails" runat="server" SelectMethod="GetEmployeeDetails"
                                TypeName="METAOPTION.BAL.EmployeeBAL">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="EmployeeId" QueryStringField="EmployeeId" Type="Int64" />
                                    <asp:SessionParameter Name="OrgID" SessionField="OrgID" Type="Int16" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:Panel ID="pnlEditEmployee" Width="800" Style="display: none;" runat="server"
                                CssClass="modalPopup">
                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="PopUpBoxHeading">
                                            &nbsp;&nbsp; Edit Employee Details
                                        </td>
                                        <td class="PopUpBoxHeading" align="right">
                                            <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="$find('mdpop').hide();return false;"
                                                ImageUrl="../Images/close.gif" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="padding: 10px">
                                            <asp:FormView ID="frmviewEditEmployee" OnDataBound="frmviewEditEmployee_DataBound"
                                                DataSourceID="objEmployeeDetails" runat="server" Width="100%">
                                                <ItemTemplate>
                                                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                                                        class="arial-12">
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Title</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlTitle" AppendDataBoundItems="true" runat="server" CssClass="txtMan1"
                                                                    DataSourceID="objTitle" DataTextField="Title1" SelectedValue='<%# Eval("TitleId")%>'
                                                                    DataValueField="TitleId">
                                                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objTitle" runat="server" SelectMethod="GetTitleList" TypeName="METAOPTION.BAL.MasterBAL">
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>First Name</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtFirstName" Text='<%# Eval("FirstName")%>' runat="server" CssClass="txtMan2" />
                                                                <ajax:TextBoxWatermarkExtender ID="txtFirstName_TextBoxWatermarkExtender" runat="server"
                                                                    TargetControlID="txtFirstName" WatermarkCssClass="watermarked" WatermarkText="First Name">
                                                                </ajax:TextBoxWatermarkExtender>
                                                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtFirstName"
                                                                    ErrorMessage="*" SetFocusOnError="True" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Middle Name</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtMiddleName" Text='<%# Eval("MiddleName")%>' CssClass="txtMan2"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Last Name</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtLastName" Text='<%# Eval("LastName")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Employee Code</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmployeeCode" runat="server" Text='<%# Eval("EmployeeCode")%>'
                                                                    CssClass="txtMan2"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Employee Type</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="txtMan2" AppendDataBoundItems="true"
                                                                    DataSourceID="objEmployeeType" DataTextField="EmployeeType" SelectedValue='<%# Eval("EmployeeTypeId")%>'
                                                                    DataValueField="EmployeeTypeId">
                                                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objEmployeeType" runat="server" SelectMethod="GetEmployeeType"
                                                                    TypeName="METAOPTION.BAL.Common"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <%-- <td class="TableBorder">
                                                                        <b>Accounting Code</b>
                                                                    </td>
                                                                    <td class="TableBorder">
                                                                        <asp:TextBox ID="txtAccountingCode" runat="server" Text='<%# Eval("AccountingCode")%>'
                                                                            CssClass="txtMan2"></asp:TextBox>
                                                                    </td>--%>
                                                            <td class="TableBorder">
                                                                <b>Driver License State</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:HiddenField ID="hfieldDLState" Value='<%# Eval("DriverLicenseStateId")%>' runat="server" />
                                                                <asp:DropDownList ID="ddlDLState" AppendDataBoundItems="true" runat="server" CssClass="txtMan2"
                                                                    DataTextField="State" DataValueField="StateId">
                                                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Driver License Number</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtDLNumber" CssClass="txtMan2" runat="server" Text='<%# Eval("DriverLicenseNumber")%>'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Driver License
                                                                    <br>
                                                                    Expiration Date</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtDLExpDate" CssClass="txtMan2" runat="server" Text='<%# Eval("DriverLicensExpDate")%>'></asp:TextBox>
                                                                <ajax:CalendarExtender ID="caltxtLicExpDate" runat="server" PopupButtonID="imgLicExpDate"
                                                                    TargetControlID="txtDLExpDate">
                                                                </ajax:CalendarExtender>
                                                                <asp:Image ID="imgLicExpDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                                                    Style="cursor: pointer; vertical-align: middle;" />
                                                                <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                                                    ValidChars="/,-" TargetControlID="txtDLExpDate">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Street</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtStreet" CssClass="txtMan2" runat="server" Text='<%# Eval("Street")%>'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Suite</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtSuite" runat="server" Text='<%# Eval("Suite")%>' CssClass="txtMan2"> 
                                                                </asp:TextBox>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>City</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCity" runat="server" CssClass="txtMan2" Text='<%# Eval("City")%>'></asp:TextBox>
                                                                <asp:HiddenField ID="hfieldState" runat="server" Value='<%# Eval("StateId")%>' />
                                                                <asp:HiddenField ID="hfieldCountry" runat="server" Value='<%# Eval("CountryId")%>' />
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
                                                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="txt2"
                                                                    DataTextField="CountryName" DataValueField="CountryId" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                                                    ErrorMessage="*" InitialValue="0" SetFocusOnError="True" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Zip</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtZip" runat="server" CssClass="txtMan2" Text='<%# Eval("Zip")%>'></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtZip_Extender" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtZip">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <b>Phone</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="txtMan2" Text='<%# Eval("Phone1")%>'></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server"
                                                                    FilterType="Numbers,Custom" TargetControlID="txtPhone" ValidChars="+() -">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Ext.</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtExt" runat="server" CssClass="txtMan2" Text='<%# Eval("Phone1Ext")%>'></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtExt_Extender1" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtExt">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                            <td class="TableBorder" valign="top">
                                                                <b>Cell Phone</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtCellPhone" runat="server" CssClass="txtMan2" Text='<%# Eval("CellPhone")%>'></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtCellPhone_Extender" runat="server" FilterType="Numbers,Custom"
                                                                    TargetControlID="txtCellPhone" ValidChars="+() -">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Email</b>
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txtMan2" Text='<%# Eval("Email1")%>'></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorder">
                                                                &nbsp;
                                                            </td>
                                                            <td class="TableBorder">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorder">
                                                                <b>Special Payroll Conditions</b>
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <asp:TextBox ID="txtSPCondition" runat="server" Width="100%" CssClass="txtMan2" Rows="5"
                                                                    Text='<%# Eval("SpecialPayrollConditions")%>' TextMode="MultiLine"></asp:TextBox>
                                                                <asp:HiddenField ID="hdfAddressId" runat="server" Value='<%#Eval("AddressId") %>' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:FormView>
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
                                PopupControlID="pnlEditEmployee" CancelControlID="btnEditCancel" BackgroundCssClass="modalBackground">
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
                <table id="tblComment" runat="server" border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                    class="arial-12">
                    <tr>
                        <td class="TableHeadingBg TableHeading">
                            Comments
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder">
                            <asp:GridView ID="grdComments" AllowPaging="true" PageSize="20" runat="server" AutoGenerateColumns="False"
                                Width="100%" CssClass="gridView" BorderStyle="Solid" GridLines="None" BorderWidth="0"
                                OnPageIndexChanging="grdComments_PageIndexChanging" DataKeyNames="RowId" OnRowDeleting="grdComments_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="Notes" HeaderText="Comments" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <ItemStyle CssClass="GridContent"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AddedBy" HeaderText="Added By" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <ItemStyle CssClass="GridContent"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DateAdded" HeaderText="Date Added" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <ItemStyle CssClass="GridContent"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                        HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <a href='<%# "AddNote.aspx?ID="+Eval("RowId")+"&EntityId="+Eval("EntityId")+"&mode=edit&TB_iframe=true&height=220&width=600" %>'
                                                title="Edit Comment" class="thickbox">
                                                <img alt="" src="../Images/edit-icon.jpg" border="0" /></a>
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/DeleteButton.jpg"
                                                OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeader" Width="50px"></HeaderStyle>
                                        <ItemStyle CssClass="GridContent" Width="50px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Comments"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trAddNewComment" runat="server">
                        <td class="FooterContentDetails" width="100%">
                            <a href='<%= "AddNote.aspx?EntityId="+EntityId+"&type=5&TB_iframe=true&height=220&width=600" %>'
                                title="Add New Comment" style="font-size: 11px; font-weight: bold; color: #535152;
                                font-family: Arial, Helvetica, sans-serif; text-decoration: underline;" class="thickbox">
                                Add New Comment</a>
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
                <table id="tblDocument" runat="server" border="0" width="100%" cellpadding="0" style="border-collapse: collapse"
                    class="arial-12">
                    <tr>
                        <td class="TableHeadingBg TableHeading">
                            DOCUMENTS
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder">
                            <asp:Button ID="btnRefreshCustomers" runat="server" Style="display: none" OnClick="Refresh_Click" />
                            <asp:GridView ID="grdDocument" runat="server" AutoGenerateColumns="False" Width="100%"
                                CssClass="gridView" BorderStyle="Solid" GridLines="None" BorderWidth="0" OnRowCommand="grdDocument_RowCommand"
                                DataKeyNames="DocumentId" OnRowDeleting="grdDocument_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Document Type" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("DocumentType") %>' />
                                            <asp:HiddenField ID="hfDocumentTypeId" runat="server" Value='<%# Eval("DocumentTypeId") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeader" Wrap="False" />
                                        <ItemStyle CssClass="GridContent" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DocumentTitle" HeaderText="Title" HeaderStyle-Wrap="false"
                                        HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                        <HeaderStyle CssClass="GridHeader" Wrap="False" />
                                        <ItemStyle CssClass="GridContent" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Wrap="false"
                                        HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                        <HeaderStyle CssClass="GridHeader" Wrap="False" />
                                        <ItemStyle CssClass="GridContent" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Document Name" HeaderStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="upDocLoader" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="lnkOpenDocument" runat="server" CommandName="OpenDoc" CausesValidation="false"
                                                        Text='<%# Eval("DocumentName") %>' CommandArgument='<%# Eval("DocumentId") %>' />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkOpenDocument" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeader" Wrap="False" />
                                        <ItemStyle CssClass="GridContent" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Added By" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAddedBy" Text='<%# string.Format("{0} on {1}", Eval("AddedBy"),  Eval("DateAdded", "{0:MMM dd, yyyy hh:mm tt}")) %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeader" />
                                        <ItemStyle CssClass="GridContent" Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                        HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <a href='<%# "AddDocument.aspx?ID="+Eval("DocumentId")+"&EntityId="+Eval("EntityId")+"&mode=edit&type=5&TB_iframe=true&height=240&width=700" %>'
                                                title="Edit Document" style="font-size: 11px; font-weight: bold; color: #535152;
                                                font-family: Arial, Helvetica, sans-serif; text-decoration: underline;" class="thickbox">
                                                <img alt="" src="../Images/edit-icon.jpg" border="0" /></a>
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/DeleteButton.jpg"
                                                OnClientClick="javascript:return confirm('Really do you want to delete?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeader" Width="50px"></HeaderStyle>
                                        <ItemStyle CssClass="GridContent" Width="50px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblDataMsg" runat="server" CssClass="leftpanelcontentheading" Text="No Documents"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trAddNewDoc" runat="server">
                        <td class="FooterContentDetails" width="100%">
                            <a href='<%= "AddDocument.aspx?EntityId="+EntityId+"&type=5&TB_iframe=true&height=240&width=700" %>'
                                title="Add New Document" style="font-size: 11px; font-weight: bold; color: #535152;
                                font-family: Arial, Helvetica, sans-serif; text-decoration: underline;" class="thickbox">
                                Add New Document</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">

        function Delete_ArchiveEmployee(Status) {
            var Employeeid = $('#<%=hfEmployeeId.ClientID %>').val();
            var UserID = '<%=Session["empId"] %>';
            var Answer = true;

            if (Status == "0")
                Answer = confirm("Do you want to delete this Employee? Once deleted, it will not appear in Searchlist. Do you want to continue?");
            else if (Status == "2")
                Answer = confirm("Do you want to archive this Employee? Once archived, it will appear in Searchlist, but no transaction will happen for this Employee. Do you want to continue?");

            if (Answer) {
                $.ajax({ type: "POST",
                    url: "ViewEmployee.aspx/DeleteArchiveEmployee",
                    data: "{Status:" + Status + ",EmployeeID:" + Employeeid + ",UserID:" + UserID + "}",
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
                        window.location.href = "../UI/EmployeeSearchList.aspx";
                    }
                });
            }
        }

        //Failed Function
        function Failed(result) {
            alert(result.status + " " + result.statusText);
        }
        
    </script>
</asp:Content>
