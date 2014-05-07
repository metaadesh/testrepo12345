<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDocument.aspx.cs" Inherits="METAOPTION.UI.AddDocument" %>

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
    <form id="form1" runat="server">
    <div class="RightPanel">
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Add Document</legend>
            <br>
            <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                <tr>
                    <td class="TableBorder" style="width: 18%;" nowrap>
                        Document Type
                    </td>
                    <td class="TableBorder" style="width: 37%">
                        <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="txt2" DataSourceID="objDocType"
                            DataTextField="DocumentType1" DataValueField="DocumentTypeId" />
                        <asp:ObjectDataSource ID="objDocType" runat="server" SelectMethod="DocumentType"
                            TypeName="METAOPTION.BAL.MasterBAL">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="EntityTypeId" QueryStringField="type" Type="Int64" />
                                <asp:SessionParameter Name="OrgID" DefaultValue='0' Type="Int16" SessionField="OrgID" />
                             
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td class="TableBorder" style="width: 15%">
                        Title
                    </td>
                    <td class="TableBorder" style="width: 30%">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="txt2" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Description
                    </td>
                    <td class="TableBorder" colspan="3">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="txtMulti" Rows="3" TextMode="MultiLine" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Select File
                    </td>
                    <td colspan="3" class="TableBorder">
                        <asp:FileUpload ID="fuUpload" runat="server" Width="350px" />
                        <div class="err">
                            Note: File size must be less than 2MB!</div>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <br />
                        <asp:Button ID="btnUpload" runat="server" CssClass="Btn_Form" OnClick="btnUpload_Click"
                            Text="Submit" />
                        &nbsp;<asp:Button ID="btnUpdate" runat="server" CssClass="Btn_Form" Text="Save" Width="57px"
                            OnClick="btnUpdate_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    </form>
</body>
</html>
