<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditContact.aspx.cs" Inherits="METAOPTION.UI.EditContact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/AjaxRelated.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />

    <script src="../CSS/ext-core-debug.js" type="text/javascript"></script>

    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: White;">
   <%-- <script runat="server">
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
        }
    
    </script>--%>
    <form id="form1" runat="server">
    <div class="RightPanel">
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Edit Contact</legend>
            <br>
            <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="grdContactDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="gridView" BorderStyle="Solid" GridLines="None" BorderWidth="0px" DataSourceID="objContactById">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        Job Title
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlJobTitle" runat="server" CssClass="txtMan1" DataSourceID="objJobTitle"
                                            DataTextField="JobTitle1" DataValueField="JobTitleId" SelectedValue='<%# Eval("JobTitleId") %>'>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="objJobTitle" runat="server" SelectMethod="GetJobTitle"
                                            TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-CssClass="GridHeader" HeaderStyle-CssClass="GridHeader"
                                    ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        First Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt1" Text='<%#Eval("FirstName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle CssClass="GridHeader" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        Middle Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txt1" Text='<%#Eval("MiddleName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        Last Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txt1" Text='<%#Eval("LastName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        Contact Type
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContactType1"
                                            DataValueField="ContactTypeId" DataSourceID="objContactType" SelectedValue='<%# Eval("ContactTypeId") %>'
                                            CssClass="txtMan1">
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="objContactType" runat="server" SelectMethod="GetContactType"
                                            TypeName="METAOPTION.BAL.MasterBAL"></asp:ObjectDataSource>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        Office Phone
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOfficePhone" CssClass="txt1" runat="server" Text='<%#Eval("OfficePhone") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        Cell Phone
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCellPhone" CssClass="txt1" runat="server" Text='<%#Eval("CellPhone") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        Email
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEmail" CssClass="txt1" runat="server" Text='<%#Eval("Email") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="objContactById" runat="server" SelectMethod="GetContactDetailsByContactId"
                            TypeName="METAOPTION.BAL.CommonBAL">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="ContactId" QueryStringField="ID" Type="Int64" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" CssClass="Btn_Form" Text="Save" Width="68px"
                            OnClick="btnAdd_Click" />
                      
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    </form>
</body>
</html>
