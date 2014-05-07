<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddNewContact.aspx.cs" Inherits="METAOPTION.UI.AddNewContact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/AjaxRelated.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />

    <script src="../CSS/ext-core-debug.js" type="text/javascript"></script>

    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    
</head>

<body style="background-color:White;">

    <form id="form1" runat="server">
    <asp:ScriptManager 
    ID="ScriptManager1" 
    runat="server">
    </asp:ScriptManager>
    <div class="RightPanel">
     <fieldset class="ForFieldSet">
                        <legend class="ForLegend">Contacts
                        </legend> <br>
                       <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                       <tr>
                       <td> <asp:ObjectDataSource ID="objJobTitle" runat="server" SelectMethod="GetJobTitle"
                            TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="objContactType" runat="server" SelectMethod="GetContactType"
                            TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                   
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        
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
                        <br>
                       </td>
                       </tr>
                       <tr>
                       <td align="center">
                        <asp:Button ID="btnAdd" runat="server" CssClass="Btn_Form" Text="Add" Width="68px"
                        OnClick="btnAdd_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       </td>
                       </tr>
                       </table>
                       
                       
                    </fieldset>
    </div>
    </form>
</body>
</html>
