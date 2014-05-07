<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddCustomer.aspx.cs"
    Inherits="METAOPTION.UI.AddCustomer" Title="HeadStart VMS::Add Purchased From/Sold To"
    MasterPageFile="~/UI/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphAddCustomer" runat="server">
    <asp:HiddenField ID="hWelcomeTasks" runat="server" />
    <div class="RightPanel">
        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="AddHeading">
                    Add A Purchased From/Sold To
                </td>
            </tr>
            <tr>
                <td class="height30">
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset class="ForFieldSet">
                        <legend class="ForLegend">Purchased From/Sold To Details</legend>
                        <br>
                        <asp:UpdatePanel ID="upAddDealer" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                                    <tr>
                                        <td class="TableBorderB">
                                            Dealer Name
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtDealerName" runat="server" CssClass="txtMan2" />
                                            <asp:RequiredFieldValidator ID="rfvDealerName" runat="server" ControlToValidate="txtDealerName"
                                                ErrorMessage="*" SetFocusOnError="True" />
                                        </td>
                                        <td class="TableBorderB">
                                            Dealer DIN
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtDealerDIN" runat="server" CssClass="txtMan2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Dealer Type
                                        </td>
                                        <td class="TableBorder">
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="txtMan2" DataSourceID="objDealerType"
                                                DataTextField="DealerType1" DataValueField="DealerTypeId">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="objDealerType" runat="server" SelectMethod="GetDealerType"
                                                TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                        </td>
                                        <td class="TableBorderB" rowspan="2" valign="top">
                                            Franchise Make(s)
                                        </td>
                                        <td class="TableBorder" rowspan="2">
                                            <asp:ListBox ID="lstMake" runat="server" CssClass="txtMan2" SelectionMode="Multiple"
                                                DataSourceID="objMake" DataTextField="VINDivisionName" DataValueField="MakeID">
                                            </asp:ListBox>
                                            <asp:ObjectDataSource ID="objMake" runat="server" SelectMethod="GetMakeList" TypeName="METAOPTION.BAL.MasterBAL">
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Dealer Category
                                        </td>
                                        <td class="TableBorder">
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="txtMan2" DataSourceID="objDelaerCategory"
                                                DataTextField="Category" DataValueField="DealerCategoryId">
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
                                                DataTextField="Source" DataValueField="DealerSourceId">
                                            </asp:DropDownList>
                                            <asp:ObjectDataSource ID="objDealerSource" runat="server" SelectMethod="GetDealerSource"
                                                TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                        </td>
                                        <%-- <td class="TableBorderB">
                                            Accounting Code
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtAccountingCode" runat="server" CssClass="txtMan2"></asp:TextBox>
                                        </td>--%>
                                        <td class="TableBorderB">
                                            Street
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtStreet" runat="server" CssClass="txtMan2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            City
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="txtMan2"></asp:TextBox>
                                        </td>
                                        <td class="TableBorderB">
                                            Country
                                        </td>
                                        <td class="TableBorder">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="txt2" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
                                            <asp:RequiredFieldValidator ID="rfvCountry" runat="server" InitialValue="0" ControlToValidate="ddlCountry"
                                                ErrorMessage="*" SetFocusOnError="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            State
                                        </td>
                                        <td class="TableBorder">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
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
                                            Suite
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtSuite" runat="server" CssClass="txtMan2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Zip
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtZip" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID="txtZipExten" runat="server" TargetControlID="txtZip"
                                                FilterType="Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                        <td class="TableBorderB">
                                            Phone
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID="txtPhoneExten" runat="server" TargetControlID="txtPhone"
                                                FilterMode="ValidChars" ValidChars="+,-" FilterType="Custom,Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Fax
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtFax" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID="txtFaxExten" runat="server" TargetControlID="txtFax"
                                                FilterMode="ValidChars" ValidChars="+,-" FilterType="Custom,Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                        <td class="TableBorderB">
                                            Other Number
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtOtherNumber" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtOtherNumber"
                                                FilterMode="ValidChars" ValidChars="+,-" FilterType="Custom,Numbers">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Email
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="*"
                                                SetFocusOnError="True" />
                                        </td>
                                        <td class="TableBorderB">
                                            Auction Access Number
                                        </td>
                                        <td class="TableBorder">
                                            <asp:TextBox ID="txtAuctionAccessNumber" runat="server" CssClass="txtMan2" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Welcome Task
                                        </td>
                                        <td class="TableBorderB">
                                            <%-- <asp:DropDownList ID="ddlWelcomeTasks" runat="server" CssClass="txtMan2">
                                            </asp:DropDownList>--%>
                                            <telerik:RadComboBox ID="ddlWelcomeTasks" runat="server" Width="150px" AllowCustomText="true"
                                                EmptyMessage="">
                                                <ItemTemplate>
                                                    <div onclick="StopPropagation(event)" class="combo-item-template">
                                                        <asp:CheckBox runat="server" ID="chk1" CssClass="cb" Text='<%#Eval("Task")%>' onclick="onCheckBoxClick(this,'WelcomeTasks')" />
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
                                            <asp:TextBox ID="txtComment" CssClass="txtMulti" runat="server" Rows="5" TextMode="MultiLine"
                                                onkeyup="MaxCharLimit(this,  'txtcount' , 4000)" />
                                            <asp:TextBox ID="txtcount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                Width="30px" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset class="ForFieldSet">
                        <legend class="ForLegend">Contacts</legend>
                        <br />
                        <asp:ObjectDataSource ID="objJobTitle" runat="server" SelectMethod="GetJobTitle"
                            TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="objContactType" runat="server" SelectMethod="GetContactType"
                            TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server">
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
                                            <asp:TemplateField HeaderText="Job Title" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlJobTitle" runat="server" CssClass="txtMan1" DataTextField="JobTitle1"
                                                        DataValueField="JobTitleId">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="First Name" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtMan1" Text='<%# Eval("FirstName")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Middle Name" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txt1" Text='<%# Eval("MiddleName")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Name" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt1" Text='<%# Eval("LastName")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Type" HeaderStyle-Wrap="false" ItemStyle-CssClass="GridContent"
                                                HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlContactType" runat="server" CssClass="txtMan1" DataTextField="ContactType1"
                                                        DataValueField="ContactTypeId" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Phone" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOfficeNo" runat="server" CssClass="txtMan1" Text='<%# Eval("OfficeNo")%>' />
                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtOfficeNo"
                                                        FilterMode="ValidChars" ValidChars="+,-" FilterType="Custom,Numbers">
                                                    </ajax:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cell Phone" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCellNo" runat="server" CssClass="txtMan1" Text='<%# Eval("CellNo")%>' />
                                                    <ajax:FilteredTextBoxExtender ID="txtCellext" runat="server" TargetControlID="txtCellNo"
                                                        FilterMode="ValidChars" ValidChars="+,-" FilterType="Custom,Numbers">
                                                    </ajax:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email" ItemStyle-CssClass="GridContent" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtMan1" Text='<%# Eval("Email")%>' />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnNewRow" runat="server" ImageUrl="~/Images/AddButton.jpg"
                                                        Width="20" Height="20" OnClick="btnNewRow_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="15px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td class="height30">
                    <asp:ObjectDataSource ID="objYear" runat="server" SelectMethod="GetYearList" TypeName="METAOPTION.BAL.Common">
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset class="ForFieldSet">
                        <legend class="ForLegend">Preference</legend>
                        <br>
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr>
                                <td class="GridHeader">
                                    Preference Setting
                                </td>
                                <td class="GridHeader">
                                    <asp:UpdatePanel ID="upRblPreference" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="RblPrefSetting" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Would You like to receive SMS for new car
                                </td>
                                <td class="TableBorder">
                                    <asp:UpdatePanel ID="upMobilePref" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rblSmsSetting" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    Would You like to receive Email for new car
                                </td>
                                <td class="TableBorder">
                                    <asp:UpdatePanel ID="upEmailPref" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rblEmailSetting" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2" class="TableBorder">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlPreference" runat="server">
                                                <asp:GridView ID="grdPreference" BorderStyle="Solid" runat="server" Width="100%"
                                                    AutoGenerateColumns="false" OnRowDataBound="grdPreference_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImgbtnRemove" OnCommand="RemoveRow" runat="server" CommandArgument='<%# Eval("SNo") %>'
                                                                    ImageUrl="~/Images/DeleteButton.jpg" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Enable" HeaderStyle-CssClass="GridHeader">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkEnable" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Years" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridHeader">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlMinYear" DataTextField="Year" DataValueField="Year" runat="server"
                                                                    CssClass="txtMan1">
                                                                </asp:DropDownList>
                                                                <asp:DropDownList ID="ddlMaxYear" DataTextField="Year" DataValueField="Year" runat="server"
                                                                    CssClass="txtMan1" AutoPostBack="True" OnSelectedIndexChanged="ddlMaxYear_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" Type="Integer" 
                                                                    ControlToCompare="ddlMaxYear" ControlToValidate="ddlMinYear" Display="None" 
                                                                    ErrorMessage="&lt;b&gt;! Erro&lt;/b&gt;Value can not greater than&lt;br&gt; Min year"></asp:CompareValidator>
                                                                    <ajax:ValidatorCalloutExtender ID="ddlMax_Validate" runat="server" TargetControlID="CompareValidator1"></ajax:ValidatorCalloutExtender>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Make" HeaderStyle-CssClass="GridHeader">
                                                            <ItemTemplate>
                                                                <asp:DropDownList CssClass="txtMan1" AutoPostBack="true" DataTextField="Make" DataValueField="MakeId"
                                                                    ID="ddlMake" runat="server" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Model" HeaderStyle-CssClass="GridHeader">
                                                            <ItemTemplate>
                                                                <asp:DropDownList CssClass="txtMan1" ID="ddlModel" DataTextField="Model" DataValueField="ModelID"
                                                                    runat="server">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mileage Range" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridHeader">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMinMileage" CssClass="txtMan1" runat="server" Text='<%#Eval("MinMileage") %>' />
                                                                <cc1:FilteredTextBoxExtender ID="txtMinMileage_FilteredTextBoxExtender" runat="server"
                                                                    FilterType="Numbers" TargetControlID="txtMinMileage">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <asp:TextBox ID="txtMaxMileage" CssClass="txtMan1" runat="server" Text='<%#Eval("MaxMileage") %>' />
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtMaxMileage">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Price Range" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridHeader">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtMinPrice" runat="server" Text='<%#Eval("MinPrice") %>' CssClass="txtMan1" />
                                                                <asp:TextBox ID="txtMaxPrice" runat="server" Text='<%#Eval("MaxPrice") %>' CssClass="txtMan1" />
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtMinPrice">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtMaxPrice">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImgbtnAddRow" runat="server" ImageUrl="~/Images/AddButton.jpg"
                                                                    OnClick="ImgbtnAddRow_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="RblPrefSetting" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    SMS Setting
                                </td>
                                <td class="TableBorderB">
                                    Email Setting
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorder">
                                    <asp:UpdatePanel ID="uptblMobile" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table border="0" width="100%" id="tblMobile" runat="server" cellpadding="0" style="border-collapse: collapse">
                                                <thead>
                                                    <tr>
                                                        <td class="GridHeader">
                                                            Enable
                                                        </td>
                                                        <td class="GridHeader">
                                                            Mobile Number
                                                        </td>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td class="TableBorder">
                                                        <asp:CheckBox ID="ChkSmsEnable1" runat="server" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtMobile1" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                        <ajax:FilteredTextBoxExtender ID="txtMobile1_Extender" runat="server" FilterType="Numbers,Custom"
                                                            ValidChars="+,-" TargetControlID="txtMobile1">
                                                        </ajax:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorder">
                                                        <asp:CheckBox ID="ChkSmsEnable2" runat="server" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtMobile2" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                        <ajax:FilteredTextBoxExtender ID="txtMobile2_Extender" runat="server" FilterType="Numbers,Custom"
                                                            ValidChars="+,-" TargetControlID="txtMobile2">
                                                        </ajax:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorder">
                                                        <asp:CheckBox ID="ChkSmsEnable3" runat="server" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtMobile3" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                        <ajax:FilteredTextBoxExtender ID="txtMobile3_Extender" runat="server" FilterType="Numbers,Custom"
                                                            ValidChars="+,-" TargetControlID="txtMobile3">
                                                        </ajax:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rblSmsSetting" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TableBorder">
                                    <asp:UpdatePanel ID="uptblEmail" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table border="0" width="100%" id="tblEmail" runat="server" cellpadding="0" style="border-collapse: collapse">
                                                <thead>
                                                    <tr>
                                                        <td class="GridHeader">
                                                            Enable
                                                        </td>
                                                        <td class="GridHeader">
                                                            Email
                                                        </td>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td class="TableBorder">
                                                        <asp:CheckBox ID="ChkEmailEnable1" runat="server" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtEmail1" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorder">
                                                        <asp:CheckBox ID="ChkEmailEnable2" runat="server" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtEmail2" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorder">
                                                        <asp:CheckBox ID="ChkEmailEnable3" runat="server" />
                                                    </td>
                                                    <td class="TableBorder">
                                                        <asp:TextBox ID="txtEmail3" CssClass="txtMan2" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rblEmailSetting" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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
                    <asp:Button ID="btnCancel" CssClass="Btn_Form" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" CssClass="Btn_Form" Text="Add" Width="68px"
                        OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </div>
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
                combo = $find("<%= ddlWelcomeTasks.ClientID %>");

            }
            //holds the text of all checked items
            var text = "";
            //holds the values of all checked items
            var values = "";
            //get the collection of all items
            var items = combo.get_items();
            //enumerate all items
            for (var i = 0; i < items.get_count(); i++) {
                var item = items.getItem(i);
                //get the checkbox element of the current item
                var chk1 = $get(combo.get_id() + "_i" + i + "_chk1");
                if (chk1.checked) {
                    text += item.get_text() + ",";
                    values += item.get_value() + ",";
                }
            }
            //remove the last comma from the string
            text = removeLastComma(text);
            values = removeLastComma(values);

            if (text.length > 0) {
                //set the text of the combobox                   
                combo.set_text(text);
                objHidden.value = values;

            }
            else {
                //all checkboxes are unchecked
                //so reset the controls
                combo.set_text("");
                objHidden.value = '';
            }

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
