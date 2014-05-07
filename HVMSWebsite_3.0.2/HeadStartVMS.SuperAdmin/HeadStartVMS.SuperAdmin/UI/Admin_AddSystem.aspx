<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Admin_MasterLeftPanel.Master"
    CodeBehind="Admin_AddSystem.aspx.cs" Inherits="METAOPTION.UI.Admin_AddSystem"
    Title="Admin Panel :: Add System" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="cphEmpList" runat="server">
    <style type="text/css">
        .trlightBlue
        {
            background-color: #F0FAFF;
            border-color: #F0FAFF;
        }
    </style>
    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
        <ContentTemplate>
            <table width="100%">
                <asp:HiddenField ID="hScrollPosition" runat="server" Value="0" />
                <tr>
                    <td align="center">
                        <div style="min-height: 450px; vertical-align: top;">
                            <fieldset class="ForFieldSet">
                                <legend class="ForLegend" align="left">Add New System</legend>
                                <div>
                                    <br />
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="TableBorder" align="right" style="width: 200px;">
                                                <asp:Label ID="Label1" runat="server" Text="Organization" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td class="TableBorder" align="left">
                                                <asp:DropDownList ID="ddlOrganization" runat="server" Width="205px" AutoPostBack="false"
                                                    CssClass="txtMan2" Style="font-size: 11px;">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder" align="right">
                                                <asp:Label ID="Label2" runat="server" Text="System Name" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td class="TableBorder" align="left">
                                                <asp:TextBox ID="txtSystemName" runat="server" MaxLength="100" CssClass="txt2" Width="200px" />
                                                <asp:RequiredFieldValidator ID="rfvSystemName" runat="server" ErrorMessage="*" Font-Bold="true"
                                                    ControlToValidate="txtSystemName" ValidationGroup="AddSystem"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder" align="right">
                                                <asp:Label ID="Label3" runat="server" Text="System Logo" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td class="TableBorder" align="left" style="white-space: nowrap;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:FileUpload ID="fupSystemLogo" runat="server" />
                                                            <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="fupSystemLogo"
                                                                ValidationGroup="AddSystem" ErrorMessage="Only .gif, .jpg, .png, .jpeg" ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])$)">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            <table id="tblSystemLogo" runat="server" visible="false">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Image ID="imgSystemLogo" runat="server" border="0" Style="border-style: solid;
                                                                            border-color: #bbdef1; border-width: 1px;" ImageUrl="~/Images/Logos/Logo.gif"
                                                                            Width="170" Height="30" alt="" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:LinkButton ID="lnkRemoveLogo" runat="server" ForeColor="Red" Visible="false"
                                                                            OnClick="lnkRemoveLogo_Click">click to remove this logo</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder" align="right">
                                                <asp:Label ID="Label4" runat="server" Text="Is Active" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td class="TableBorder" align="left">
                                                <asp:CheckBox ID="chkSystemActiveStatus" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorder" align="right">
                                                <asp:Label ID="Label5" runat="server" Text="Is PeachTree" CssClass="TableBorderLabel"></asp:Label>
                                            </td>
                                            <td class="TableBorder" align="left">
                                                <asp:CheckBox ID="chkIsPeachTree" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 65%;" align="right">
                                                            <table style="margin-top: 10px;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="<< Back"
                                                                            Width="71px" CssClass="Btn_Form" OnClick="btnCancel_Click" />
                                                                    </td>
                                                                    <td width="50px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnAdd" CausesValidation="true" ValidationGroup="AddSystem" runat="server"
                                                                            Text="Add" OnClick="btnAdd_Click" Width="71px" CssClass="Btn_Form" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="padding-top: 16px;">
                                                            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="lblTextDisplayEntity"
                                                                Style="font-weight: 100; font-size: 12px; margin-left: 20px;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
