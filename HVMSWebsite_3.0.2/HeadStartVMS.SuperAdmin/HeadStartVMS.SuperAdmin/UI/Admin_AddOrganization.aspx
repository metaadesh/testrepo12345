<%@ Page Language="C#" MasterPageFile="~/UI/Admin_MasterLeftPanel.Master" AutoEventWireup="true"
    Title="Admin Panel:: Add Organization" CodeBehind="Admin_AddOrganization.aspx.cs"
    Inherits="METAOPTION.UI.Admin_AddOrganization" %>

<asp:Content ID="contAddOrg" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Add New Organization</legend>
            <div style="min-height: 400px;">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                    <tr style="background-color: White">
                        <td style="height: 15px">
                            <asp:Label ID="ShowMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;">
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;" width="180px">
                            <asp:Label ID="Label1" runat="server" Text="Organization" CssClass="TableBorderLabel"></asp:Label>
                            <b style="color: Red">*</b>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_orgname" runat="server" CssClass="txt2" Width="200px" Height="15"
                                MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
                                ControlToValidate="txt_orgname" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:Label ID="Label2" runat="server" Text="Organization Code" CssClass="TableBorderLabel"></asp:Label>
                            <b style="color: Red">*</b>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_orgcode" runat="server" CssClass="txt2" Style="text-transform: uppercase"
                                Height="15" Width="200px" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required"
                                ControlToValidate="txt_orgcode" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage=" White space not allowed"
                                ControlToValidate="txt_orgcode" ForeColor="Red" ValidationExpression="^\S+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:Label ID="Label3" runat="server" Text="Website" CssClass="TableBorderLabel"></asp:Label>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_website" runat="server" CssClass="txt2" Width="200px" Height="15"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage=" Enter Valid Website."
                                ControlToValidate="txt_website" ForeColor="Red" ValidationExpression="(http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?)|(([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?)"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:Label ID="Label4" runat="server" Text="Address" CssClass="TableBorderLabel"></asp:Label>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_address" runat="server" TextMode="MultiLine" CssClass="txt2"
                                Height="50" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:Label ID="Label5" runat="server" Text="Phone" CssClass="TableBorderLabel"></asp:Label>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_phone" runat="server" CssClass="txt2" Width="200px" Height="15"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage=" Enter  Valid Phone No."
                                ControlToValidate="txt_phone" ForeColor="Red" ValidationExpression="(\d{10})"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:Label ID="Label6" runat="server" Text="Fax" CssClass="TableBorderLabel"></asp:Label>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_fax" runat="server" CssClass="txt2" Width="200px" Height="15"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_fax"
                                ErrorMessage="Enter Valid Fax No." ForeColor="Red" ValidationExpression="(\d{10})"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:Label ID="Label7" runat="server" Text="Email" CssClass="TableBorderLabel"></asp:Label>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:TextBox ID="txt_mail" runat="server" CssClass="txt2" Width="200px" Height="15"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage=" Enter valid email id"
                                ControlToValidate="txt_mail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:Label ID="Label8" runat="server" Text="Allow Lane Automation" CssClass="TableBorderLabel"></asp:Label>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:CheckBox ID="chkAllowLaneAutomation" runat="server" Checked="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:Label ID="Label9" runat="server" Text="Allow MAA" CssClass="TableBorderLabel"></asp:Label>
                        </td>
                        <td class="TableBorder" style="padding: 5px; font-size: 11px;">
                            <asp:CheckBox ID="chkAllowMAA" runat="server" Checked="false" />
                        </td>
                    </tr>
                    <tr style="height: 20px;">
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btn_back" runat="server" OnClick="btn_back_redirect" Text="<< Back"
                                Width="71px" CssClass="Btn_Form" Visible="false" />
                        </td>
                        <td>
                            <table width="190px">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="Button_OrgSubmit" runat="server" OnClick="Btn_AddOrganization" Text="Submit"
                                            Width="71px" CssClass="Btn_Form" Style="margin-left: 50px;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
