<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditPreference.aspx.cs"
    Inherits="METAOPTION.UI.EditPreference" %>

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
            <legend class="ForLegend">Edit Preference</legend>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br>
           
             <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
               <tr>
               <td colspan="2" class="TableBorder">
                <asp:UpdatePanel ID="upPreSetting" runat="server" UpdateMode="Conditional">
               <ContentTemplate>
               <asp:FormView ID="fvPreference" runat="server" Width="100%" 
                       ondatabound="fvPreference_DataBound">
               <ItemTemplate>
               <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                  <tr>
                    <td>
                       <tr>
                    <td class="GridHeader">
                        Preference Setting
                    </td>
                    <td class="GridHeader">
                       
                                <asp:RadioButtonList ID="RblPrefSetting" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                                 <asp:HiddenField ID="hfieldPreSetting" runat="server" Value='<%# Eval("PreferenceSettings") %>' />
 <asp:HiddenField ID="hfieldMobile" runat="server" Value='<%# Eval("ReceiveSms") %>' />
<asp:HiddenField ID="hfieldEmail" runat="server" Value='<%# Eval("ReceiveEmail") %>' />
                          
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Would You like to receive SMS for new car
                    </td>
                    <td class="TableBorder">
                        
                                <asp:RadioButtonList ID="rblSmsSetting" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                          
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        Would You like to receive Email for new car
                    </td>
                    <td class="TableBorder">
                       
                                <asp:RadioButtonList ID="rblEmailSetting" runat="server" 
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:RadioButtonList>
                          
                    </td>
                </tr>
                    </td>
                  </tr>
                 </table>
               </ItemTemplate>
               </asp:FormView>
                   <asp:ObjectDataSource ID="objPrefSetting" runat="server" 
                       SelectMethod="GetDealerPreferenceSetting" 
                       TypeName="METAOPTION.BAL.DealerCustomerBAL">
                       <SelectParameters>
                           <asp:QueryStringParameter Name="DealerId" QueryStringField="EntityId" 
                               Type="Int64" />
                       </SelectParameters>
                   </asp:ObjectDataSource>
               </ContentTemplate>
               </asp:UpdatePanel>
                 
               </td>
               </tr>
                <tr>
                    <td align="left" colspan="2" class="TableBorder">
                    <asp:ObjectDataSource ID="objYear" runat="server" SelectMethod="GetYearList" TypeName="METAOPTION.BAL.Common">
                    </asp:ObjectDataSource>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="pnlPreference" runat="server">
                                    <asp:GridView ID="grdPreference" BorderStyle="Solid" runat="server" Width="100%"
                                        AutoGenerateColumns="false" OnRowDataBound="grdPreference_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgbtnRemove" OnCommand="RemoveRow" CommandName="Pref" runat="server" CommandArgument='<%# Eval("SNo") %>'
                                                        ImageUrl="~/Images/DeleteButton.jpg" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Enable" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkEnable" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Years" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlMinYear" DataTextField="Year" DataValueField="Year" runat="server"
                                                        CssClass="txtMan1">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlMaxYear" DataTextField="Year" DataValueField="Year" runat="server"
                                                        CssClass="txtMan1" AutoPostBack="True" OnSelectedIndexChanged="ddlMaxYear_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<asp:CompareValidator ID="CompareValidator1" runat="server" Type="Integer" 
                                                                    ControlToCompare="ddlMaxYear" ControlToValidate="ddlMinYear" Display="None" 
                                                                    ErrorMessage="&lt;b&gt;! Erro&lt;/b&gt;Value can not greater than&lt;br&gt; Min year"></asp:CompareValidator>
                                                                    <ajax:ValidatorCalloutExtender ID="ddlMax_Validate" runat="server" TargetControlID="CompareValidator1"></ajax:ValidatorCalloutExtender>--%>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Make" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:DropDownList CssClass="txtMan1" AutoPostBack="true" DataTextField="Make" DataValueField="MakeId"
                                                        ID="ddlMake" runat="server" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:DropDownList CssClass="txtMan1" ID="ddlModel" DataTextField="Model" DataValueField="ModelID"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mileage Range" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMinMileage" CssClass="txtMan1" runat="server" Text='<%#Eval("MileageMin") %>' />
                                                    <ajax:FilteredTextBoxExtender ID="txtMinMileage_FilteredTextBoxExtender" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtMinMileage">
                                                    </ajax:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtMaxMileage" CssClass="txtMan1" runat="server" Text='<%#Eval("MileageMax") %>' />
                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMaxMileage">
                                                    </ajax:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price Range" ItemStyle-Wrap="false" HeaderStyle-CssClass="GridHeader">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMinPrice" runat="server" Text='<%#Eval("PriceMin") %>' CssClass="txtMan1" />
                                                    <asp:TextBox ID="txtMaxPrice" runat="server" Text='<%#Eval("PriceMax") %>' CssClass="txtMan1" />
                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                                        TargetControlID="txtMinPrice" ValidChars=".,,">
                                                    </ajax:FilteredTextBoxExtender>
                                                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom"
                                                        TargetControlID="txtMaxPrice" ValidChars=".,,">
                                                    </ajax:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgbtnAddRow" CommandArgument="1" runat="server" ImageUrl="~/Images/AddButton.jpg"
                                                        OnClick="ImgbtnAddRow_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </ContentTemplate>
                           
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        SMS Setting
                    </td>
                    <td class="TableBorderB">
                        Email Setting
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder">
                        <asp:UpdatePanel ID="uptblMobile" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="grdViewMobile" runat="server" AutoGenerateColumns="False" 
                                    BorderStyle="Solid" Width="100%" 
                                    onrowdatabound="grdViewMobile_RowDataBound">
                                    <Columns>
                                     <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgbtnRemove" OnCommand="RemoveRow" CommandName="MPref" runat="server" CommandArgument='<%# Eval("SNo") %>'
                                                        ImageUrl="~/Images/DeleteButton.jpg" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" HeaderText="Enable">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEnable" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" HeaderText="Mobile Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMobile" CssClass="txtMan2" runat="server" Text='<%#Eval("MobileNo") %>' />
                                                <ajax:FilteredTextBoxExtender ID="txtMobileext" runat="server" 
                                                    FilterMode="ValidChars" FilterType="Custom,Numbers" TargetControlID="txtMobile" 
                                                    ValidChars="+,-">
                                                </ajax:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:TemplateField>
                                         <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgbtnAddRow" CommandArgument="2" runat="server" ImageUrl="~/Images/AddButton.jpg"
                                                        OnClick="ImgbtnAddRow_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                           
                        </asp:UpdatePanel>
                    </td>
                    <td class="TableBorder">
                        <asp:UpdatePanel ID="uptblEmail" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="grdViewEmail" AutoGenerateColumns="False"
                                    runat="server" Width="100%" BorderStyle="Solid" 
                                    onrowdatabound="grdViewEmail_RowDataBound">
                                    <Columns>
                                     <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgbtnRemove" OnCommand="RemoveRow" CommandName="EPref" runat="server" CommandArgument='<%# Eval("SNo") %>'
                                                        ImageUrl="~/Images/DeleteButton.jpg" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Enable" HeaderStyle-CssClass="GridHeader">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEnable" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email ID" HeaderStyle-CssClass="GridHeader">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEmail" CssClass="txtMan2" Text='<%#Eval("Email") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:TemplateField>
                                         <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgbtnAddRow" CommandArgument="3" runat="server" ImageUrl="~/Images/AddButton.jpg"
                                                        OnClick="ImgbtnAddRow_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                           
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                <td colspan="2" class="TableBorder" align="center">
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="Btn_Form" 
                        onclick="btnSave_Click" Width="62px" />
                </td>
                </tr>
            </table>
          
           
        </fieldset>
    </div>
    </form>
</body>
</html>
