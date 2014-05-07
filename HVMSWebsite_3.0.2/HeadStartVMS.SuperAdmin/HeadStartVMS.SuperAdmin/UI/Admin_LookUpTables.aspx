<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_LookUpTables.aspx.cs"
    Inherits="METAOPTION.UI.Admin_LookUpTables" MasterPageFile="~/UI/Admin_Master.Master"
    Title="Admin Panel :: Lookup Tables" %>

<asp:Content runat="server" ID="admin_lookuptables" ContentPlaceHolderID="ContentPlaceHolder1">
    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td class="TableHeadingBg TableHeading">
                Look-up tables&nbsp; List
            </td>
        </tr>
        <tr>
            <td class="TableBorder">
                <div style="min-height: 450px;">
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                        <tr style="padding-top: 10px;">
                            <td class="TableBorder" style="width: 15%" nowrap align="right" valign="middle">
                                View Type :
                            </td>
                            <td class="TableBorder" style="width: 100%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="rdRO" />
                                        <asp:PostBackTrigger ControlID="rdRW" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:RadioButton ID="rdRO" runat="server" Checked="true" GroupName="a" Text="View Only"
                                                        AutoPostBack="True" OnCheckedChanged="rdRO_CheckedChanged" Font-Bold="False"
                                                        ForeColor="#FF8F00" />
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rdRW" runat="server" GroupName="a" Text="Both (Read & Write)"
                                                        AutoPostBack="True" OnCheckedChanged="rdRW_CheckedChanged" Font-Bold="False"
                                                        ForeColor="#FF8F00" />
                                                </td>
                                                <td style="width: 40%">
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rdActive" runat="server" GroupName="Status" Text="Active" Checked="True"
                                                        Font-Bold="False" ForeColor="#FF8F00" AutoPostBack="True" OnCheckedChanged="rdActive_CheckedChanged" />
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rdInActive" runat="server" GroupName="Status" Text="In Active"
                                                        Font-Bold="False" ForeColor="#FF8F00" AutoPostBack="True" OnCheckedChanged="rdInActive_CheckedChanged" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div style="text-align: center; width: 100%">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                        <ProgressTemplate>
                                            <img alt="..." src="../Images/Wait.gif" />
                                            Wait...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </td>
                        </tr>
                        <tr style="padding-top: 10px;">
                            <td class="TableBorder" style="width: 15%; text-align: right">
                                Choose table to view
                            </td>
                            <td class="TableBorder" style="width: 100%">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="ddlLookUpTables" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlLookUpTables" runat="server" CssClass="txt3" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlLookUpTables_SelectedIndexChanged" Width="200px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr style="padding-top: 10px;">
                            <td class="TableBorder" style="width: 15%; text-align: right;" nowrap>
                                Organization
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlOrganizationMainPage" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlOrg_selectedIndexChange" CssClass="txt3" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorder" colspan="2">
                                <asp:UpdatePanel ID="upLookupTables" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                                        
                                            <asp:GridView ID="gvLookUpTables" runat="server" AllowPaging="True" OnPageIndexChanging="gvLookUpTables_PageIndexChanging"
                                                OnSelectedIndexChanged="gvLookUpTables_SelectedIndexChanged" PageSize="50" Width="100%"
                                                DataKeyNames="id" OnRowDataBound="gvLookUpTables_RowDataBound" OnRowDeleting="gvLookUpTables_RowDeleting"
                                                AllowSorting="True" OnSorting="gvLookUpTables_Sorting" EmptyDataText="No Record Found">
                                                <%--onrowcreated="gvLookUpTables_RowCreated">--%>
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="ibtnUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                                ToolTip="Update" Text="Update" />
                                                            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False"
                                                                CommandName="Cancel" Text="Cancel" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Select"
                                                                ImageUrl="~/Images/edit-icon.jpg" Text="Edit" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/DeleteButton.jpg" ShowDeleteButton="True"
                                                        ShowHeader="True">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <HeaderStyle CssClass="gvHeading" />
                                                <RowStyle CssClass="gvRow" />
                                                <AlternatingRowStyle CssClass="gvAlternateRow" />
                                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <EditRowStyle Height="18px" />
                                                <EmptyDataRowStyle CssClass="gvEmpty" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlView" runat="server" Visible="false">
                                            <asp:GridView ID="gvViewOnly" runat="server" AllowPaging="True" PageSize="50" Width="100%"
                                                OnPageIndexChanging="gvViewOnly_PageIndexChanging" AllowSorting="True" OnSorting="gvViewOnly_Sorting"
                                                EmptyDataText="No Record Found">
                                                <HeaderStyle CssClass="gvHeading" />
                                                <RowStyle CssClass="gvRow" Height="25px" />
                                                <AlternatingRowStyle CssClass="gvAlternateRow" />
                                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <EmptyDataRowStyle CssClass="gvEmpty" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlReActive" runat="server" Visible="false">
                                            <asp:GridView ID="gvInActive" runat="server" AllowPaging="True" DataKeyNames="id"
                                                PageSize="50" Width="100%" OnRowDeleting="gvInActive_RowDeleting" AllowSorting="True"
                                                OnSorting="gvInActive_Sorting" OnRowDataBound="gvInActive_RowDataBound" EmptyDataText="No Record Found">
                                                <%--onselectedindexchanged="gvInActive_SelectedIndexChanged--%>
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/confirm.gif" ShowDeleteButton="True"
                                                        ShowHeader="True" DeleteText="Re-Activate">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <HeaderStyle CssClass="gvHeading" />
                                                <RowStyle CssClass="gvRow" />
                                                <AlternatingRowStyle CssClass="gvAlternateRow" />
                                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <EditRowStyle Height="18px" />
                                                <EmptyDataRowStyle CssClass="gvEmpty" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                        <tr>
                            <td align="left" valign="top">
                                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Width="400px" Style="display: none;">
                                    <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
                                            <ajax:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
                                                PopupControlID="pnlPopup" CancelControlID="btnEditCancel" BackgroundCssClass="modalBackground">
                                            </ajax:ModalPopupExtender>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="PopUpBoxHeading" style="padding-left: 10px" nowrap>
                                                        Update
                                                        <% =ViewState["Title"] %>
                                                    </td>
                                                    <td align="right" class="PopUpBoxHeading" style="padding-right: 5px">
                                                        <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnEditCancel.ClientID %>').click();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div>
                                                <div style="height: 8px">
                                                </div>
                                                <asp:DetailsView ID="dvLookupTables" runat="server" DefaultMode="Edit" AutoGenerateRows="true"
                                                    Width="96%" HeaderStyle-CssClass="TableBorder" HeaderStyle-Font-Bold="true" InsertRowStyle-CssClass="TableBorder"
                                                    InsertRowStyle-HorizontalAlign="Left" InsertRowStyle-VerticalAlign="Middle" EditRowStyle-CssClass="txtMan2">
                                                </asp:DetailsView>
                                                <div style="height: 10px">
                                                </div>
                                            </div>
                                            
                                            <div style="height: 20px; text-align: center;">
                                                <asp:Label ID="lbl_errormsg_Edit" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                            </div>
                                            <div style="text-align: center">
                                                <%--<asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/confirm.gif" OnClick="imgSave_Click"
                                                    ToolTip="Update" />--%><asp:Button ID="btnEditCancel" runat="server" Text="Cancel"
                                                        CssClass="Btn_Form" />&nbsp;&nbsp;
                                                <asp:Button ID="btnEdit" runat="server" Text=" Save " OnClick="btnEdit_Click" CssClass="Btn_Form" />
                                                <%--<asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Images/Delete.gif" ToolTip="Cancel" />--%>
                                                <div style="height: 10px">
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <%-- Add New looup records without dependent tables--%>
                                <asp:Panel ID="pnlAddIndependent" runat="server" CssClass="modalPopup" Width="400px"
                                    Style="display: none;">
                                    <asp:UpdatePanel ID="uppAddIndependentTable" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnShowIndependent" runat="server" Style="display: none" />
                                            <ajax:ModalPopupExtender ID="mpeShowIndependent" runat="server" TargetControlID="btnShowIndependent"
                                                PopupControlID="pnlAddIndependent" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
                                            </ajax:ModalPopupExtender>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="PopUpBoxHeading" style="padding-left: 5px; width: 80%" nowrap>
                                                        Add
                                                        <% =ViewState["Title"]%>
                                                    </td>
                                                    <td align="right" class="PopUpBoxHeading" style="padding-right: 5px; width: 20%">
                                                        <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnCancel.ClientID %>').click();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="text-align: center">
                                                <div style="height: 8px">
                                                </div>
                                                <asp:DetailsView ID="dvAddIndependent" runat="server" GridLines="None" DefaultMode="Insert"
                                                    AutoGenerateRows="true" Width="96%" HeaderStyle-CssClass="TableBorder" HeaderStyle-Font-Bold="true"
                                                    InsertRowStyle-CssClass="TableBorder" InsertRowStyle-HorizontalAlign="Left" InsertRowStyle-VerticalAlign="Middle">
                                                </asp:DetailsView>
                                                <div style="height: 8px">
                                                </div>
                                            </div>
                                            <div>
                                                <div style="height: 8px">
                                                </div>
                                                <div style="text-align: center">
                                                    <%--<asp:ImageButton ID="ibtnAddIndependentLooukUp" runat="server" ImageUrl="~/Images/confirm.gif"
                                                    OnClick="ibtnAddIndependentLooukUp_Click" ToolTip="Add New Record" />--%>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Btn_Form" />
                                                    &nbsp;&nbsp;<asp:Button ID="btnAddIndependentLooukUp" runat="server" Text=" Save "
                                                        OnClick="btnAddIndependentLooukUp_Click" CssClass="Btn_Form" />
                                                    <%--<asp:ImageButton ID="ibtnClose" runat="server" ImageUrl="~/Images/Delete.gif" ToolTip="Cancel" />--%>
                                                    &nbsp;&nbsp;
                                                </div>
                                                <div style="height: 8px">
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <%-- End --%>
                                <%-- Add New looup records dependent tables--%>
                                <asp:Panel ID="pnlBankAccount" runat="server" CssClass="modalPopup" Width="400px"
                                    Style="display: none;">
                                    <asp:UpdatePanel ID="upBankAccount" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnShowBankAccount" runat="server" Style="display: none" />
                                            <ajax:ModalPopupExtender ID="mdpBankAccount" runat="server" TargetControlID="btnShowBankAccount"
                                                PopupControlID="pnlBankAccount" CancelControlID="btnBankAccountClose" BackgroundCssClass="modalBackground">
                                            </ajax:ModalPopupExtender>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="PopUpBoxHeading" style="padding-left: 5px; width: 80%" nowrap>
                                                        Add
                                                        <% =ViewState["Title"] %>
                                                    </td>
                                                    <td align="right" class="PopUpBoxHeading" style="padding-right: 5px; width: 20%">
                                                        <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnBankAccountClose.ClientID %>').click();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div>
                                                <div style="height: 8px">
                                                </div>
                                                <asp:DetailsView ID="dvAddBankAccount" runat="server" AutoGenerateRows="false" DefaultMode="Insert"
                                                    GridLines="None" Width="100%">
                                                    <Fields>
                                                        <asp:TemplateField HeaderText="Organization">
                                                            <InsertItemTemplate>
                                                                <asp:DropDownList ID="ddlOrganizationBankAccount" runat="server" CssClass="GridContent"
                                                                    Style="font-size: 11px;" DataSourceID="odsOrganization" DataTextField="Organisation1"
                                                                    DataValueField="OrgID">
                                                                </asp:DropDownList>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank ID">
                                                            <InsertItemTemplate>
                                                                <asp:DropDownList ID="ddlBankId" runat="server" CssClass="GridContent" DataSourceID="odsBankId"
                                                                    DataTextField="BankName" DataValueField="BankId">
                                                                </asp:DropDownList>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Account Number">
                                                            <InsertItemTemplate>
                                                                <asp:TextBox ID="txtAccountNumber" runat="server" Text='<%# Bind("AccountNumber") %>'
                                                                    TextMode="SingleLine"></asp:TextBox><br />
                                                               <asp:Label ID="lbl_errormsg" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                                                <%--<asp:RequiredFieldValidator ID="rfvAccountNumber" runat="server" ControlToValidate="txtAccountNumber"
                                                                    ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />--%>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                            </div>
                                            <div>
                                            <div style="text-align: center">
                                                <div style="height: 20px">
                                                </div>
                                                    <%--<asp:ImageButton ID="btnBankAccountAdd" runat="server" ImageUrl="~/Images/confirm.gif"
                                                    OnClick="btnBankAccountAdd_Click" ToolTip="Add New Record" />--%>
                                                    <asp:Button ID="btnBankAccountClose" runat="server" Text="Cancel" CssClass="Btn_Form" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnBankAccountAdd" runat="server" Text=" Save " OnClick="btnBankAccountAdd_Click"
                                                        CssClass="Btn_Form" />
                                                    <%--<asp:ImageButton ID="ibtnBankAccountClose" runat="server" ImageUrl="~/Images/Delete.gif"
                                                    ToolTip="Cancel" />--%>
                                                    &nbsp;&nbsp;
                                                </div>
                                            </div>
                                            <div style="height: 8px">
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <asp:Panel ID="pnlDocType" runat="server" CssClass="modalPopup" Width="400px" Style="display: none;">
                                    <asp:UpdatePanel ID="upDocumentType" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnShowDocumentType" runat="server" Style="display: none" />
                                            <ajax:ModalPopupExtender ID="mdpDocumentType" runat="server" TargetControlID="btnShowDocumentType"
                                                PopupControlID="pnlDocType" CancelControlID="btnDocumentTypeClose" BackgroundCssClass="modalBackground">
                                            </ajax:ModalPopupExtender>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="PopUpBoxHeading" style="padding-left: 5px; width: 80%" nowrap>
                                                        Add
                                                        <% =ViewState["Title"]%>
                                                    </td>
                                                    <td align="right" class="PopUpBoxHeading" style="padding-right: 5px; width: 20%">
                                                        <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnDocumentTypeClose.ClientID %>').click();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div>
                                                <div style="height: 8px">
                                                </div>
                                                <asp:DetailsView ID="dvAddDocumentType" runat="server" GridLines="None" DefaultMode="Insert"
                                                    AutoGenerateRows="false" Width="100%">
                                                    <Fields>
                                                        <asp:TemplateField HeaderText="Organization">
                                                            <InsertItemTemplate>
                                                                <asp:DropDownList ID="ddlOrganizationDocumentType" runat="server" CssClass="GridContent"
                                                                    Style="font-size: 11px;" DataSourceID="odsOrganization" DataTextField="Organisation1"
                                                                    DataValueField="OrgID">
                                                                </asp:DropDownList>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Entity Type">
                                                            <InsertItemTemplate>
                                                                <asp:DropDownList ID="ddlDocumentType" runat="server" DataTextField="EntityType1"
                                                                    DataValueField="EntityTypeId" DataSourceID="obsAllEntityType" CssClass="GridContent">
                                                                </asp:DropDownList>
                                                            </InsertItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("EntityTypeId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Type">
                                                            <InsertItemTemplate>
                                                                <asp:TextBox ID="txtDocumentType" runat="server" Text='<%# Bind("DocumentType") %>'
                                                                    TextMode="SingleLine"></asp:TextBox>
                                                                <%--  <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtDocumentType"
                                                                    ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />--%>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                            </div>
                                            <div>
                                                <div style="text-align: center">
                                                    <div style="height: 10px">
                                                    </div>
                                                    <asp:Button ID="btnDocumentTypeClose" runat="server" Text="Cancel" CssClass="Btn_Form" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnAddDocumentType" runat="server" Text=" Save " OnClick="btnAddDocumentType_Click"
                                                        CssClass="Btn_Form" />
                                                    &nbsp;&nbsp;
                                                </div>
                                            </div>
                                            <div style="height: 8px">
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <asp:Panel ID="pnlGroups" runat="server" CssClass="modalPopup" Width="400px" Style="display: none;">
                                    <asp:UpdatePanel ID="upGroups" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnShowGroups" runat="server" Style="display: none" />
                                            <ajax:ModalPopupExtender ID="mdpGroups" runat="server" TargetControlID="btnShowGroups"
                                                PopupControlID="pnlGroups" CancelControlID="btnGroupsClose" BackgroundCssClass="modalBackground">
                                            </ajax:ModalPopupExtender>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="PopUpBoxHeading" style="padding-left: 5px; width: 80%" nowrap>
                                                        Add
                                                        <% =ViewState["Title"]%>
                                                    </td>
                                                    <td align="right" class="PopUpBoxHeading" style="padding-right: 5px; width: 20%">
                                                        <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnGroupsClose.ClientID %>').click();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div>
                                                <div style="height: 8px">
                                                </div>
                                                <asp:DetailsView ID="dvGroups" runat="server" GridLines="None" DefaultMode="Insert"
                                                    AutoGenerateRows="false" Width="100%">
                                                    <Fields>
                                                        <asp:TemplateField HeaderText="Organization">
                                                            <InsertItemTemplate>
                                                                <asp:DropDownList ID="ddlOrganizationGroups" runat="server" CssClass="GridContent"
                                                                    Style="font-size: 11px;" DataSourceID="odsOrganization" DataTextField="Organisation1"
                                                                    DataValueField="OrgID">
                                                                </asp:DropDownList>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group Status">
                                                            <InsertItemTemplate>
                                                                <asp:DropDownList ID="ddlGroupStatus" runat="server" DataTextField="GroupStatus1"
                                                                    DataValueField="GroupStatusId" DataSourceID="odsAllGroupsStatusList" CssClass="GridContent">
                                                                </asp:DropDownList>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group Name">
                                                            <InsertItemTemplate>
                                                                <asp:TextBox ID="txtGroupName" runat="server" Text='<%# Bind("GroupName") %>' TextMode="SingleLine"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtGroupName"
                                                                    ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />--%>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group Abbreviation">
                                                            <InsertItemTemplate>
                                                                <asp:TextBox ID="txtGroupAbbreviation" runat="server" Text='<%# Bind("GroupAbbreviation") %>'
                                                                    TextMode="SingleLine"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvtxtGroupAbbreviation" runat="server" ControlToValidate="txtGroupAbbreviation"
                                                                    ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />--%>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                            </div>
                                            <div>
                                                <div style="text-align: center">
                                                    <div style="height: 10px">
                                                    </div>
                                                    <asp:Button ID="btnGroupsClose" runat="server" Text="Cancel" CssClass="Btn_Form" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnAddGroup" runat="server" Text=" Save " OnClick="btnAddGroup_Click"
                                                        CssClass="Btn_Form" />
                                                    &nbsp;&nbsp;
                                                </div>
                                            </div>
                                            <div style="height: 8px">
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <asp:Panel ID="pnlAddState" runat="server" CssClass="modalPopup" Width="400px" Style="display: none;">
                                    <asp:UpdatePanel ID="upState" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnShowState" runat="server" Style="display: none" />
                                            <ajax:ModalPopupExtender ID="mdpState" runat="server" TargetControlID="btnShowState"
                                                PopupControlID="pnlAddState" CancelControlID="btnStateClose" BackgroundCssClass="modalBackground">
                                            </ajax:ModalPopupExtender>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td class="PopUpBoxHeading" style="padding-left: 5px; width: 80%" nowrap>
                                                        Add
                                                        <% =ViewState["Title"]%>
                                                    </td>
                                                    <td align="right" class="PopUpBoxHeading" style="padding-right: 5px; width: 20%">
                                                        <img src="../Images/close.gif" alt="" onclick="javascipt:document.getElementById('<%=btnStateClose.ClientID %>').click();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div>
                                                <div style="height: 8px">
                                                </div>
                                                &nbsp;
                                                <asp:DetailsView ID="dvState" runat="server" GridLines="None" DefaultMode="Insert"
                                                    AutoGenerateRows="false" Width="96%">
                                                    <Fields>
                                                        <asp:TemplateField HeaderText="Country">
                                                            <InsertItemTemplate>
                                                                <asp:DropDownList ID="ddlCountry" runat="server" DataTextField="CountryName" DataValueField="CountryId"
                                                                    DataSourceID="odsAllCountryList" CssClass="GridContent">
                                                                </asp:DropDownList>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State Code">
                                                            <InsertItemTemplate>
                                                                <asp:TextBox ID="txtStateCode" runat="server" Text='<%# Bind("StateCode") %>' TextMode="SingleLine"></asp:TextBox>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvtxtStateCode" runat="server" ControlToValidate="txtStateCode"
                                                                    ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />--%>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="State">
                                                            <InsertItemTemplate>
                                                                <asp:TextBox ID="txtState" runat="server" Text='<%# Bind("State") %>' TextMode="SingleLine"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState"
                                                                    ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />--%>
                                                            </InsertItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                            </div>
                                            <div>
                                                <div style="text-align: center">
                                                    <div style="height: 10px">
                                                    </div>
                                                    <asp:Button ID="btnStateClose" runat="server" Text="Cancel" CausesValidation="false"
                                                        CssClass="Btn_Form" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnAddState" runat="server" Text=" Save " OnClick="btnAddState_Click"
                                                        CssClass="Btn_Form" />
                                                    &nbsp;&nbsp;
                                                </div>
                                            </div>
                                            <div style="height: 8px">
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                <%-- End --%>
                                <%-- Add New lookup records Button--%>
                                <asp:UpdatePanel ID="upAdd" runat="server">
                                    <ContentTemplate>
                                        <div class="FooterContentDetails" style="width: 100%" id="dvAdd" runat="server" visible="false">
                                            <asp:LinkButton class="AddNewExpenseTxt" ID="lnkAdd" runat="server" CausesValidation="false"
                                                OnClick="lnkAdd_Click"></asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%-- End --%>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                        <tr class="legend">
                            <td>
                                <%--<asp:DetailsView ID="DetailsView1" runat="server" GridLines="None" DefaultMode="Insert"
                                                AutoGenerateRows="False" Width="100%">
                        
                                            </asp:DetailsView>--%>
                                <asp:ObjectDataSource ID="odsOrganization" runat="server" SelectMethod="OrganizationList"
                                    TypeName="METAOPTION.BAL.Admin_Common"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsBankId" runat="server" SelectMethod="GetAllBankList"
                                    TypeName="METAOPTION.BAL.Admin_Common"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="obsAllEntityType" runat="server" SelectMethod="GetAllEntityType"
                                    TypeName="METAOPTION.BAL.Admin_Common"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsAllCountryList" runat="server" SelectMethod="GetCountryList"
                                    TypeName="METAOPTION.BAL.Admin_Common"></asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsAllGroupsStatusList" runat="server" SelectMethod="GetAllGroupsStatusList"
                                    TypeName="METAOPTION.BAL.Admin_Common"></asp:ObjectDataSource>
                                <%-- <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="GetAllBankList"
                                TypeName="METAOPTION.BAL.Common"></asp:ObjectDataSource>--%>
                                <asp:ObjectDataSource ID="ddlbank" runat="server" SelectMethod="getAllBankList" TypeName="METAOPTION.BAL.Admin_Common">
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
