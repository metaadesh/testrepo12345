<%@ Page Language="C#"  AutoEventWireup="true"
    CodeBehind="AddCustomMMB.aspx.cs" Inherits="METAOPTION.AddCustomMMB" Title="HeadStartVMS::ADD CUSTOM MAKE, MODEL, BODY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellspacing="0" cellpadding="0" width="90%" style="border-collapse: collapse">
        <tr>
            <td align="left">
                <asp:Label ID="lblMessage" runat="server" CssClass="err"></asp:Label>
                <fieldset class="ForFieldSet">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                        <tr>
                            <td class="TableHeading TableHeadingBg" colspan="2">
                                ADD CUSTOM MAKE, MODEL, BODY
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Year
                            </td>
                            <td class="TableBorder">
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="txtMan2"
                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Make
                            </td>
                            <td class="TableBorder" nowrap="nowrap">
                                <asp:UpdatePanel ID="updPnlMake" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlMake" runat="server" CssClass="txtMan2" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        &nbsp;<asp:TextBox ID="txtMake" CssClass="txtMan2" runat="server"></asp:TextBox>
                                      <%--  <ajax:TextBoxWatermarkExtender ID="txtMake_TextBoxWatermarkExtender" WatermarkText="Enter Make For Sel Year"
                                            WatermarkCssClass="watermarked" runat="server" Enabled="True" TargetControlID="txtMake">
                                        </ajax:TextBoxWatermarkExtender>--%>
                                        <asp:Button ID="btnSaveMake" CssClass="Btn_Form" runat="server" Text="Save" OnClick="btnSaveMake_Click" />
                                        <asp:Button ID="btnUpdateMake" CssClass="Btn_Form" runat="server" Text="Update" OnClick="btnUpdateMake_Click" />
                                        <asp:Button ID="btnDeleteMake" CssClass="Btn_Form" runat="server" Text="Delete" OnClick="btnDeleteMake_Click"
                                            OnClientClick="javascript:return confirm('Are u sure you want to delete this Make?\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                        <asp:PostBackTrigger ControlID="btnSaveMake" />
                                        <asp:PostBackTrigger ControlID="btnUpdateMake" />
                                        <asp:PostBackTrigger ControlID="btnDeleteMake" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Model
                            </td>
                            <td class="TableBorder">
                                <asp:UpdatePanel ID="updPnlModel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True" CssClass="txtMan2"
                                            OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp;<asp:TextBox ID="txtModel" CssClass="txtMan2" runat="server"></asp:TextBox>
                                       <%-- <ajax:TextBoxWatermarkExtender ID="txtModel_TextBoxWatermarkExtender" WatermarkText="Enter Model For Sel Make"
                                            WatermarkCssClass="watermarked" runat="server" Enabled="True" TargetControlID="txtModel">
                                        </ajax:TextBoxWatermarkExtender>--%>
                                        <asp:Button ID="btnSaveModel" runat="server" CssClass="Btn_Form" Text="Save" OnClick="btnSaveModel_Click" />
                                        <asp:Button ID="btnUpdateModel" CssClass="Btn_Form" runat="server" Text="Update"
                                            OnClick="btnUpdateModel_Click" />
                                        <asp:Button ID="btnDeleteModel" CssClass="Btn_Form" runat="server" Text="Delete"
                                            OnClick="btnDeleteModel_Click" OnClientClick="javascript:return confirm('Are u sure you want to delete this Model?\n\nClick Ok to Confirm\nClick Cancel to ignore');"/>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                        <asp:PostBackTrigger ControlID="btnSaveModel" />
                                        <asp:PostBackTrigger ControlID="btnUpdateModel" />
                                        <asp:PostBackTrigger ControlID="btnDeleteModel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="TableBorderB">
                                Body
                            </td>
                            <td class="TableBorder">
                                <asp:UpdatePanel ID="upPnlBody" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlBody" runat="server" AutoPostBack="True" CssClass="txtMan2"
                                            OnSelectedIndexChanged="ddlBody_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp;<asp:TextBox ID="txtBody" CssClass="txtMan2" runat="server"></asp:TextBox>
                                       <%-- <ajax:TextBoxWatermarkExtender ID="txtBody_TextBoxWatermarkExtender" WatermarkText="Enter Body For Sel Model"
                                            WatermarkCssClass="watermarked" runat="server" Enabled="True" TargetControlID="txtBody">
                                        </ajax:TextBoxWatermarkExtender>--%>
                                        <asp:Button ID="btnSaveBody" runat="server" CssClass="Btn_Form" Text="Save" OnClick="btnSaveBody_Click" />
                                        <asp:Button ID="btnUpdateBody" runat="server" CssClass="Btn_Form" Text="Update" OnClick="btnUpdateBody_Click" />
                                        <asp:Button ID="btnDeleteBody" CssClass="Btn_Form" runat="server" Text="Delete" OnClick="btnDeleteBody_Click" OnClientClick="javascript:return confirm('Are u sure you want to delete this Body?\n\nClick Ok to Confirm\nClick Cancel to ignore');"/>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlModel" />
                                        <asp:PostBackTrigger ControlID="btnSaveBody" />
                                        <asp:PostBackTrigger ControlID="btnUpdateBody" />
                                        <asp:PostBackTrigger ControlID="btnDeleteBody" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Note:- Selection of all fields are mandatory to process Custom records.atory to
                                process Custom records.
                            </td>
                        </tr>
                    </table>
                    <div>
                        <br />
                        <center>
                            <asp:Button ID="btnSaveToMainDB" runat="server" CssClass="Btn_Form" Text="Process All Custom Make, Model, Body"
                                OnClick="btnSaveToMainDB_Click" Width="250px" /></center>
                    </div>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
