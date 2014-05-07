<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditExpenses.aspx.cs" Inherits="METAOPTION.UI.EditExpenses" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            <legend class="ForLegend">Edit Expenses</legend>
            <br/>
            <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                <tr>
                    <td>
                        <asp:GridView ID="grdexpensesDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                            CssClass="gridView" BorderStyle="Solid" GridLines="None" BorderWidth="0px" DataSourceID="objexpenseById">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                    <HeaderTemplate>
                                        Expense Type
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="txtMan1" DataSourceID="objExpensetypes"
                                            DataTextField="ExpenseType1" DataValueField="ExpenseTypeID" SelectedValue='<%# Eval("ExpenseTypeId") %>'>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="objExpensetypes" runat="server" SelectMethod="GetAllExpenseType"
                                            TypeName="METAOPTION.BAL.PreExpenseBAL"></asp:ObjectDataSource>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-CssClass="GridHeader" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="50px" ItemStyle-Width="50px"
                                    ItemStyle-CssClass="GridContent">
                                    <HeaderTemplate>
                                        Min Count
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMinCount" runat="server" CssClass="txt1" Text='<%#Eval("MinCount") %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle CssClass="GridHeader" />
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                    <HeaderTemplate>
                                        Max Count
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtmaxcount" runat="server" CssClass="txt1" Text='<%#Eval("MaxCount") %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                    <HeaderTemplate>
                                        Default Price($)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtdefaultprice" runat="server" CssClass="txt1" Text='<%#Eval("Defaultprice") %>' Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="objexpenseById" runat="server" SelectMethod="GetEntityExpenseByID"
                            TypeName="METAOPTION.BAL.PreExpenseBAL">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="EntityExpenseID" QueryStringField="ID" Type="Int64" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnAdd" runat="server" CssClass="Btn_Form" Text="Save" 
                            Width="68px" onclick="btnAdd_Click"/>
                      
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    </form>
</body>
</html>
