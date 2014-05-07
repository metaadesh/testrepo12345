<%@ Page Language="C#" AutoEventWireup="true" 
CodeBehind="SelectDealer.aspx.cs" 
Inherits="METAOPTION.UI.SelectDealer"
 enableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Customer/Dealer</title>
    <link href="../CSS/AjaxRelated.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ModalPopUp.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/thickbox.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function ReturnResult(dealerId)
    {
       window.parent.parent.document.getElementById('<%= IdControlId %>').value = dealerId;
       var rows = document.getElementById("gvDealerDetails").rows;
        for(i=0;i <rows.length;i++)
        {
            if(rows[i].cells[1].innerHTML == dealerId)
            {
               var finalValue =  rows[i].cells[2].innerHTML + " (" +
                     rows[i].cells[3].innerHTML + "-" +
                     rows[i].cells[4].innerHTML + "-" +
                     rows[i].cells[5].innerHTML + "-" +
                     rows[i].cells[6].innerHTML + ")";
               var dealerName = finalValue.replace("&nbsp;", "");     
                 window.parent.parent.document.getElementById('<%= NameControlId %>').value = dealerName;                
                 break;
            }
        } 
        window.parent.document.getElementById("TB_closeWindowButton").click();
        return false;
    }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
      
   <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
       <tr>
           <td align="left">
               <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
                   <tr>
                       <td class="TableBorderB">
                           <b>Name</b>
                       </td>
                       <td class="TableBorder">
                           <asp:TextBox ID="txtDealerName" CssClass="txtMan2" runat="server"></asp:TextBox>
                       </td>
                       <td class="lblb">
                           Country
                       </td>
                       <td class="lbl">
                          
                          <asp:DropDownList 
                              ID="ddlCountry" 
                              runat="server"
                              CssClass="txt2"
                              AutoPostBack="true" 
                             onselectedindexchanged="ddlCountry_SelectedIndexChanged" />                         
                       </td>
                       <td class="lblb">
                           State
                       </td>
                       <td class="TableBorder">
                           <asp:DropDownList 
                              ID="ddlDealerState" 
                              runat="server" 
                              CssClass="txt2" />
                       </td>
                   </tr>
                   <tr>
                       <td class="TableBorderB">
                           <b>City</b>
                       </td>
                       <td class="TableBorder">
                           <asp:TextBox ID="txtCity" CssClass="txtMan2" runat="server"></asp:TextBox>
                       </td>
                       <td class="TableBorderB">
                           <b>Zip</b>
                       </td>
                       <td class="TableBorder">
                           <asp:TextBox ID="txtZip" runat="server" CssClass="txtMan2"></asp:TextBox>
                       </td>
                       <td colspan="2" class="lblb">
               
                     <asp:Button 
                        ID="btnSearchDealers" 
                        runat="server" 
                        CssClass="btn" 
                        Text="   Search   "
                        OnClick="btnSearchDealers_Click" />
                       
                       </td>
                   </tr>
               </table>
           </td>
       </tr>
       <tr>
           <td>
               <asp:GridView 
                  ID="gvDealerDetails" 
                  runat="server" 
                  DataKeyNames="DealerId" 
                  AutoGenerateColumns="False" 
                  GridLines="Vertical"
                  AllowPaging="true"
                  PageSize="20"
                  Width="100%" onpageindexchanging="gvDealerDetails_PageIndexChanging">
                   <Columns>
                       <asp:TemplateField HeaderStyle-Width="25px">
                           <ItemTemplate>
                               <asp:ImageButton 
                               ID="imgbtnSelect"
                               runat="server"
                               ImageUrl="~/Images/confirm.gif" 
                               OnClientClick='<%# String.Format("return ReturnResult({0})", Eval("DealerId")) %> ' />
                           </ItemTemplate> 
                        <HeaderStyle Width="25px"></HeaderStyle>
                       </asp:TemplateField>
                       <asp:BoundField DataField="DealerId" HeaderText="DealerId" 
                          ItemStyle-CssClass="hideCol" HeaderStyle-CssClass="hideCol"> 
                     <HeaderStyle CssClass="hideCol"></HeaderStyle>
                     <ItemStyle CssClass="hideCol"></ItemStyle>
                       </asp:BoundField>
                       <asp:BoundField DataField="DealerName" HeaderText="Name" />
                       <asp:BoundField DataField="City" HeaderText="City" />
                       <asp:BoundField DataField="State" HeaderText="State" />
                       <asp:BoundField DataField="Zip" HeaderText="Zip" />
                       <asp:BoundField DataField="Country" HeaderText="Country" />
                       <asp:BoundField DataField="Category" HeaderText="Category" />
                       <asp:BoundField DataField="DealerType" HeaderText="Type"/>
                   </Columns>
                  <HeaderStyle CssClass="gvHeading" HorizontalAlign="Left" />
                  <RowStyle CssClass="gvRow" />                           
                  <AlternatingRowStyle CssClass="gvAlternateRow" />
                  <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                  <EmptyDataRowStyle HorizontalAlign="Center" />                                        
               </asp:GridView>
           </td>
       </tr>
   </table>
    </form>
</body>
</html>
