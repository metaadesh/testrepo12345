<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotesPopUp.aspx.cs" Inherits="METAOPTION.UI.NotesPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Inventory Notes</title>
       
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
       
    </head>
<body style="background-color:White">
    <form id="form1" runat="server">
    <div >
    
       <%-- <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
           <tr>
                <td align="center" width="100%"  colspan="2">
                    </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="TopMenuBarTd" colspan="2">
                    <b>Inventory Notes</b></td>
            </tr>
            <tr>
                <td align="right" width="15%"class="EditContent" valign="top">
                    &nbsp;</td>
                <td align="left" width="85%" class="EditContent">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" width="15%"class="EditContent" valign="top">
                    Notes :</td>
                <td align="left" width="85%" class="EditContent">
                    <asp:TextBox ID="txtNotes" runat="server" BorderStyle="None" Rows="6"
                        TextMode="MultiLine" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" width="85%" class="EditContent" colspan="2">
                    <a href="#" onclick="window.close(this);return false;">Close</a></td>
            </tr>
            </table>--%>
          <asp:GridView
               runat="server"
               Width="100%"
               ID="gvNotes"
               AllowPaging="True"
               PageSize="10"
               AutoGenerateColumns="False"                  
               OnPageIndexChanging="gvNotes_PageIndexChanging">
                  <Columns>
                     <asp:BoundField DataField="RowId" HeaderText="RowId" Visible="False" />
                     <asp:BoundField DataField="Notes" HeaderText="Notes" >
                        <ItemStyle Width="300px" />
                     </asp:BoundField>
                     <asp:BoundField DataField="AddedBy" HeaderText="Added By" /> 
                     <asp:BoundField DataField="DateAdded" HeaderText="Date Added" DataFormatString="{0:MM/dd/yyyy}" /> 
                      <asp:TemplateField>
                           <ItemTemplate>
                              <asp:ImageButton ID="ibtnExpenseEdit" ImageUrl="~/Images/edit-icon.jpg" runat="server"/>
                              <asp:ImageButton ID="ibtnExpenseDelete" ImageUrl="~/Images/DeleteButton.jpg" runat="server"/>
                           </ItemTemplate>
                           <ItemStyle Width="60px" />
                        </asp:TemplateField>
                   </Columns>
                  <AlternatingRowStyle CssClass="gvAlternateRow" />
                  <HeaderStyle CssClass="gvHeading" />
                  <RowStyle CssClass="gvRow" />
              </asp:GridView> 
              <br />
             <div class="AddNewExpenseTxt" style="text-align:center">
               <a href="#" onclick="window.close(this);return false;">Close</a>
             </div>
    </div>
    </form>
</body>
</html>
