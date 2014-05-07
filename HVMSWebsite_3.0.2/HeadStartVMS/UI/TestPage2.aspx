<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage2.aspx.cs" Inherits="METAOPTION.UI.TestPage2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:DropDownList ID="ddlTest" AutoPostBack="true" runat="server" 
            onselectedindexchanged="test_SelectedIndexChanged">
    <asp:ListItem Text="1" Value="1"></asp:ListItem>
    <asp:ListItem Text="2" Value="2"></asp:ListItem>
    <asp:ListItem Text="3" Value="3"></asp:ListItem>
    </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    
    <asp:GridView ID="gvTest" runat="server" DataSourceID="odsInventory"  ></asp:GridView>
        <asp:TextBox ID="TextBox1" runat="server" Height="86px" TextMode="MultiLine" 
            Width="127px"></asp:TextBox>
     <asp:ObjectDataSource ID="odsInventory" runat="server" SelectCountMethod="SearchInventoryCount"
            SelectMethod="SearchInventory" TypeName="METAOPTION.BAL.InventoryBAL" EnablePaging="True">
            <SelectParameters>
              
               <asp:Parameter DefaultValue="-1" Name="VInMatch"
                  Type="Int32" />
                <asp:Parameter  DefaultValue=""  Name="VINNo" 
                  Type="String" />
                <asp:Parameter  Name="checkNo"  Type="String" />
                <asp:Parameter DefaultValue="-1" Name="year" 
                  Type="Int32" />
                <asp:Parameter  DefaultValue="-1" Name="makeId" 
                  Type="Int64" />
                <asp:Parameter  DefaultValue="-1" Name="modelId"
                  Type="Int64" />
                <asp:Parameter DefaultValue="-1" Name="dealerId" 
                  Type="Int64" />
               <asp:Parameter  DefaultValue="-1" Name="customerId"
                  Type="Int64" />
                <asp:Parameter DefaultValue="-1" Name="buyerId" 
                  Type="Int64" />
                <asp:Parameter  DefaultValue="-1" Name="designationId"
                  Type="Int64" />
                <asp:Parameter  DefaultValue="-1" Name="comeBack" 
                  Type="Int32" />
               <asp:Parameter  DefaultValue="-1" Name="sold" 
                  Type="Int32" />
               <asp:Parameter  DefaultValue="1" Name="CarStatus"
                  Type="Int32" />
               <asp:Parameter Name="StartRowIndex" Type="Int32" />
               <asp:Parameter Name="MaximumRows" Type="Int32" />
            </SelectParameters>
         </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
