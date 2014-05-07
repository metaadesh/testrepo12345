<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddInventory.aspx.cs" Inherits="METAOPTION.UI.AddInventory"
    Title="HeadStartVMS" %>

<%@ MasterType VirtualPath="~/UI/MasterPage.Master" %>
<asp:content contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfReferrerURL" runat="server" />
    <script type="text/javascript">
        function validateAmount(sender, args) {
            var txt = (args.Value);
            var startindex = txt.indexOf(".")
            var lastindex = txt.lastIndexOf(".")

            if (startindex != lastindex) {

                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
    </script>

    <script type="text/javascript">
        function validateDealer(sender, args) {   //This function validate for dealer and cost fields, if car cost is entered
            //Then dealer Must be selected
            var dealerId = document.getElementById('<%=txtDealerShip.ClientID%>');
            var cost = document.getElementById('<%=txtCost.ClientID%>');
            if (cost.value != "" && dealerId.value == "") {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }
    </script>

    <script type="text/javascript">
        function GetCustomerId(val) {
            var txtcustomer = document.getElementById(val);

            // var hfId = val.replace("txtCustomerName", "hdCustomerId")
            var hfcontrol = document.getElementById('<%=hdDealerId.ClientID%>');
            var str = (txtcustomer.value).split("ID:");
            if (str.length > 1) {
                txtcustomer.value = str[0];
                hfcontrol.value = str[1];
            }
        }
    </script>
 <script type="text/javascript">
     function OnSelectedIndexChange() {
         var ddlVehiclePresent = document.getElementById('<%=ddlVehiclePresent.ClientID%>');
         var selddlVal = ddlVehiclePresent.value;

         //Change Request By Naushad Sir, dated  Aug 18,2010
         //if Vehicle Present dropdown has value true then display current date in arrival date &
         //display dealer designation "here" in dealer desgination dropdownlist in dealer section
         if (selddlVal == 'True') {
             var txtArrivalDate = document.getElementById('<%=txtArrivalDate.ClientID%>');
             var currentTime = new Date()
             var month = currentTime.getMonth() + 1
             var day = currentTime.getDate()
             var year = currentTime.getFullYear()
             txtArrivalDate.value = month + "/" + day + "/" + year

             var ddlDesignation = document.getElementById('<%=ddlDesignation.ClientID%>');
             //alert(ddlDesignation);
             ddlDesignation.selectedIndex = 1;
             //alert(ddlDesignation.value);
         }
         else if (selddlVal == 'False') {
             var txtArrivalDate = document.getElementById('<%=txtArrivalDate.ClientID%>');
             txtArrivalDate.value = "";

             var ddlDesignation = document.getElementById('<%=ddlDesignation.ClientID%>');
             ddlDesignation.selectedIndex = 0;
         }
     }
  </script> 
    <div class="AddHeading">Add An Inventory</div>
    <div style="text-align:left">
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Details</legend>
            <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                <tr>
                    <td class="TableBorderB">
                        VIN
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtVinNo" CssClass="txtMan2" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
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
                                    AutoPostBack="True" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TableBorderB">
                        Model
                    </td>
                    <td class="TableBorder">
                        <asp:UpdatePanel ID="updPnlModel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlModel" CssClass="txtMan2" runat="server" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged"
                                    AutoPostBack="True" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                                <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Vehicle Present
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlVehiclePresent" onChange="OnSelectedIndexChange();" CssClass="txtMan2" runat="server">
                            <asp:ListItem Value="True">Yes</asp:ListItem>
                            <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TableBorderB">
                        Body
                    </td>
                    <td class="TableBorder">
                        <asp:UpdatePanel ID="upPnlBody" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlBody" runat="server" OnSelectedIndexChanged="ddlBody_SelectedIndexChanged"
                                    CssClass="txtMan2" AutoPostBack="True" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                                <asp:AsyncPostBackTrigger ControlID="ddlModel" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Arrival Date
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtArrivalDate" runat="server" CssClass="txtMan2"></asp:TextBox>
                        <ajax:CalendarExtender ID="calArrivalDate" runat="server" TargetControlID="txtArrivalDate"
                            PopupButtonID="imgArrivalDate">
                        </ajax:CalendarExtender>
                        <asp:Image ID="imgArrivalDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                            Style="cursor: pointer;" />
                    </td>
                    <td class="TableBorderB">
                        Special Equipment
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtDEquipment" runat="server" CssClass="txtMan2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Mileage In
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtMileageIn" MaxLength="14" CssClass="txtMan2" runat="server"></asp:TextBox>
                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                            TargetControlID="txtMileageIn">
                        </ajax:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfvMileageIn" runat="server" ValidationGroup="A"
                            ControlToValidate="txtMileageIn" ErrorMessage="*" SetFocusOnError="True" />
                    </td>
                    <td class="TableBorderB">
                        Engine
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlEngine" CssClass="txtMan2" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Exterior Color
                    </td>
                    <td class="TableBorder">
                        <asp:UpdatePanel ID="updPnlExtCol" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlExtCol" CssClass="txtMan2" runat="server" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                                <asp:AsyncPostBackTrigger ControlID="ddlModel" />
                                <asp:AsyncPostBackTrigger ControlID="ddlBody" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TableBorderB">
                        Wheel Drive
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlWheelDrive" CssClass="txtMan2" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Interior Color
                    </td>
                    <td class="TableBorder">
                        <asp:UpdatePanel ID="updPnl" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlIntCol" CssClass="txtMan2" runat="server" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                                <asp:AsyncPostBackTrigger ControlID="ddlModel" />
                                <asp:AsyncPostBackTrigger ControlID="ddlBody" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TableBorderB">
                        Grade
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlGrade" CssClass="txtMan2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">Trans</td>
                    <td class="TableBorder" >
                        <asp:DropDownList ID="ddlTrans" CssClass="txtMan2" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtCarLocation" CssClass="txtMan2" Visible="false" runat="server"></asp:TextBox>
                    </td>
                    <td class="TableBorderB">Bad CARFAX</td>
                    <td class="TableBorder" >
                        <asp:DropDownList ID="ddlBarCarFax" CssClass="txt2" runat="server">
                            <asp:ListItem Value="1" Text="Yes" />
                            <asp:ListItem Value="0" Text="No" />
                            <asp:ListItem Value="-1" Text="Unknown" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Note
                    </td>
                    <td class="TableBorder" colspan="3">
                        <asp:TextBox ID="txtNote" CssClass="txtMulti" Rows="3" TextMode="MultiLine" runat="server"
                            Width="100%" onkeyup="MaxCharLimit(this, 'txtcount1', 255)" />
                        <asp:TextBox ID="txtcount1" runat="server" ReadOnly="true" CssClass="WordCounter"
                            Width="30px" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Bad AUTOCHECK
                    </td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlBadAutoCheck" CssClass="txt2" runat="server">
                            <asp:ListItem Value="1" Text="Yes" />
                            <asp:ListItem Value="0" Text="No" />
                            <asp:ListItem Value="-1" Text="Unknown" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    
    <div style="text-align:left">
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Car Properties</legend>
            <table border="0" cellspacing="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                <tr>
                    <td>
                        <asp:CheckBoxList ID="chkCarProp1" runat="server">
                            <asp:ListItem class="TableListItem">&#160;&#160;AC</asp:ListItem>
                            <asp:ListItem class="TableListItem">&#160;&#160;Powerlocks</asp:ListItem>
                            <asp:ListItem class="TableListItem">&#160;&#160;Power Windows</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td>
                        <asp:CheckBoxList ID="chkCarProp2" runat="server" class="TableListItem">
                            <asp:ListItem class="TableListItem">&#160;&#160;Sun Roof</asp:ListItem>
                            <asp:ListItem class="TableListItem">&#160;&#160;Leather</asp:ListItem>
                            <asp:ListItem class="TableListItem">&#160;&#160;Navigation</asp:ListItem>
                            <%-- <asp:ListItem  class="TableListItem">&nbsp;&nbsp;Alloy Wheels</asp:ListItem>--%>
                            <%-- <asp:ListItem class="TableListItem">&nbsp;&nbsp;Power Seats</asp:ListItem>--%>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    
     <div style="text-align:left">
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Lane Numbers</legend>
            <table border="0" cellspacing="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                <tr>
                    <td class="TableBorderB">
                        Regular Lane Number
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtRLaneNo" CssClass="txt1" Width="53px" runat="server" MaxLength="7"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revRegularLaneNumber" ValidationGroup="A" Display="Dynamic"
                            ControlToValidate="txtRLaneNo" runat="server" ValidationExpression="^\d{2}-\w{4}"
                            ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                       
                    </td>
                    <td class="TableBorderB">
                        Exotic Lane Number
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtELaneNo" CssClass="txt1" Width="53px" runat="server" MaxLength="7"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revExoticLaneNumber" Display="Dynamic" ControlToValidate="txtELaneNo"
                            ValidationExpression="^\d{2}-\w{4}" ValidationGroup="A" runat="server" ErrorMessage="*"
                            SetFocusOnError="True"></asp:RegularExpressionValidator>
                        
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Virtual Lane Number
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtVLaneNo" CssClass="txt1" Width="53px" runat="server" MaxLength="7"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revVirtualLaneNumber" Display="Dynamic" ControlToValidate="txtVLaneNo"
                            ValidationGroup="A" ValidationExpression="^\d{2}-\w{4}" runat="server" ErrorMessage="*"
                            SetFocusOnError="True"></asp:RegularExpressionValidator>
                       
                    </td>
                    <td class="TableBorderB">
                        Online Lane Number
                    </td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtOLaneNo" CssClass="txt1" Width="53px" runat="server" MaxLength="7"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revOnlineLaneNumber" Display="Dynamic" ControlToValidate="txtOLaneNo"
                            ValidationGroup="A" ValidationExpression="^\d{2}-\w{4}" runat="server" ErrorMessage="*"
                            SetFocusOnError="True"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="TableBorder">
                        <b>Note:-</b>Lane Number should be in the Format: First Two numeric characters 
                        then &quot;-&quot; and atlast Four alphanumeric characters i.e 00-99AA
                    </td>
                </tr>
            </table>
        </fieldset>
     </div>
     
     <div style="text-align:left">
         <fieldset class="ForFieldSet">
            <legend class="ForLegend">Dealer Details</legend>
            <table border="0" cellspacing="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                <tr>
                    <td class="TableBorderB">
                        DealerShip
                    </td>
                    <td class="TableBorder" nowrap="nowrap">
                        <asp:TextBox ID="txtDealerShip"  onblur="GetCustomerId(this.id)"  
                            CssClass="txtMan2" Width="250px" ToolTip="Type at least two characters to find customer name started with i.e MA or %MA to find all customer names having characters entered"
                            Wrap="false" runat="server" autocomplete="off"/>
                        <div style="float:left;">
                        <ajax:AutoCompleteExtender ID="txtTest_AutoCompleteExtender" runat="server" TargetControlID="txtDealerShip"
                            ServicePath="../WS/AutoFillCustomers.asmx" ServiceMethod="AutoFillCustomers" EnableCaching="true" UseContextKey="true"
                            MinimumPrefixLength="2" CompletionSetCount="25"  CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                          DelimiterCharacters=";, :">
                        </ajax:AutoCompleteExtender>
                        </div>
                    <asp:CustomValidator ID="cvDealerValidation" runat="server" ControlToValidate="txtCost"
                        ErrorMessage="Select Dealer First" SetFocusOnError="true" ValidationGroup="A"
                        ClientValidationFunction="validateDealer"></asp:CustomValidator>
                        <asp:HiddenField ID="hdDealerId" runat="server" />
                    </td>
                    <td class="TableBorderB">Buyer Purchase Agent</td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlBuyer" CssClass="txtMan2" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBuyer" runat="server" ControlToValidate="ddlBuyer"
                            ValidationGroup="A" ErrorMessage="*" SetFocusOnError="True" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">Designation</td>
                    <td class="TableBorder">
                        <asp:DropDownList ID="ddlDesignation" AppendDataBoundItems="true" CssClass="txtMan2"
                            runat="server">
                            <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TableBorderB" >Cheque Number</td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtChequeNo" CssClass="txtMan2" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">Cost</td>
                    <td class="TableBorder">
                        <asp:TextBox ID="txtCost" MaxLength="14" CssClass="txtMan2" runat="server"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:CustomValidator ID="cvCost" runat="server" ControlToValidate="txtCost" ErrorMessage="Invalid Amount"
                            ClientValidationFunction="validateAmount"></asp:CustomValidator>
                        <ajax:FilteredTextBoxExtender ID="txtCost_FilteredTextBoxExtender" runat="server"
                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtCost">
                        </ajax:FilteredTextBoxExtender>
                        &nbsp;&nbsp;
                        <b>Market Price</b>&nbsp;&nbsp;
                        <asp:TextBox ID="txtMarketPrice" CssClass="txt1" runat="server" />
                    </td>
                    <td class="TableBorderB">Purchase Date</td>
                    <td class="TableBorder" nowrap="nowrap">
                        <asp:TextBox ID="txtPurchaseDate" CssClass="txtMan2" runat="server"></asp:TextBox>
                        <ajax:CalendarExtender ID="txtPurchaseDate_CalendarExtender" PopupButtonID="imgPurchaseDt"
                            runat="server" TargetControlID="txtPurchaseDate">
                        </ajax:CalendarExtender>
                        <asp:Image ID="imgPurchaseDt" runat="server" ImageUrl="~/Images/calender-icon.gif"
                            Style="cursor: pointer;" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Title Present
                    </td>
                    <td class="TableBorder" colspan="3">
                        <asp:CheckBox ID="chkTitlePresent" CssClass="txtMan2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Title Present Note
                    </td>
                    <td class="TableBorder" colspan="3">
                        <asp:TextBox ID="txtTitlePresentNote" runat="server" CssClass="txtMulti" TextMode="MultiLine"
                            Rows="3" Width="100%" onkeyup="MaxCharLimit(this, 'txtcount', 255)" />
                        <asp:TextBox ID="txtcount" runat="server" ReadOnly="true" CssClass="WordCounter"
                            Width="30px" />
                    </td>
                </tr>
                <tr>
                    <td class="TableBorderB">
                        Title Tracking Note
                    </td>
                    <td class="TableBorder" colspan="3">
                                        <asp:GridView ID="gvSelLinkedCars" CssClass="gridView" 
                            runat="server" AutoGenerateColumns="False"
                                            Width="100%" CellPadding="4" GridLines="None" 
                            DataKeyNames="InventoryId" DataSourceID="objSelLinkedCars"
                                            OnRowDataBound="gvSelLinkedCars_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" 
                                                    ItemStyle-CssClass="GridContent">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnSelLinkedCar" CommandName="SelLinkedCar" CommandArgument='<%#Eval("InventoryId") %>'
                                                            runat="server" ImageUrl="~/Images/confirm.gif" 
                                                            OnClick="imgbtnSelLinkedCar_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="InventoryId" HeaderText="InventoryId" Visible="false"
                                                    HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Make" HeaderText="Make" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Body" HeaderText="Body" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ExteriorColor" HeaderText="Exterior Color" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="InteriorColor" HeaderText="Interior Color" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CarLocation" HeaderText="CarLocation" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ArrivalDate" HeaderText="ArrivalDate" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VehiclePresent" HeaderText="VehiclePresent" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CarCost" HeaderText="CarCost" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ChargebackCommision" HeaderText="Commission" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SoldPrice" HeaderText="Sold Price" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ComebackDealer" HeaderText="Comeback Dealer" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SoldTo" HeaderText="SoldTo" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PurchaseFrom" HeaderText="Purchase From" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BuyerName" HeaderText="Buyer Name" HeaderStyle-CssClass="GridHeader"
                                                    ItemStyle-CssClass="GridContent">
                                                    <HeaderStyle CssClass="GridHeader" />
                                                    <ItemStyle CssClass="GridContent" />
                                                </asp:BoundField>
                                                <%--<asp:BoundField Visible="false" DataField="Profit" HeaderText="Profit" HeaderStyle-CssClass="GridHeader"
                                                        ItemStyle-CssClass="GridContent">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:BoundField>
                                                     <asp:BoundField Visible="false" DataField="Expense" HeaderText="Expense" HeaderStyle-CssClass="GridHeader"
                                                        ItemStyle-CssClass="GridContent">
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <ItemStyle CssClass="GridContent" />
                                                    </asp:BoundField>--%>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <table border="0" width="100%" cellspacing="0" cellpadding="0" 
                                                    style="border-collapse: collapse;">
                                                    <tr>
                                                        <td align="center" class="TableBorderB" style="width: 100%">
                                                            No Rows found
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                        <asp:TextBox ID="txtTitleTrackingNote" runat="server" CssClass="txtMulti" TextMode="MultiLine"
                            Rows="3" Width="100%" onkeyup="MaxCharLimit(this, 'txtTitleTrackingCount', 1000)" />
                        <asp:TextBox ID="txtTitleTrackingCount" runat="server" ReadOnly="true" CssClass="WordCounter"
                            Width="30px" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    
    <div style="text-align:left">
        <table border="0" cellspacing="0" width="100%" cellpadding="0" style="border-collapse: collapse">
            <tr>
                <td class="FooterContentDetails" width="230">
                    <b>Select Linked Filter Car (if any):</b>
                    
                </td>
                <td class="TableBorder">
                    <asp:LinkButton ID="lnkSelectCar" runat="server" CssClass="gridcontent_link" OnClick="lnkSelectCar_Click">Select Car</asp:LinkButton>
                </td>
            </tr>
         </table>
    </div>
    <div style="text-align:left">
        <asp:Panel ID="pnlSelLinkedCars" CssClass="modalPopup" Style="display: none;" Width="620"
            runat="server" HorizontalAlign="Left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="PopUpBoxHeading">
                        &nbsp;&nbsp;SELECT LINKED CARS
                    </td>
                    <td class="PopUpBoxHeading" align="right">
                        <img border="0" onclick="$find('MBILinkedCars').hide();return false;" src="../Images/close.gif"
                            alt="close" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel runat="server" ID="pnlSelLinkedCars101" ScrollBars="Vertical" Width="600px"
                            Height="450px">
                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdOldInventoryId" runat="server" />
                                        <asp:ObjectDataSource ID="objSelLinkedCars" runat="server" SelectMethod="GetLinkedCars"
                                            TypeName="METAOPTION.BAL.InventoryBAL">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="txtVinNo" Name="strVIN" PropertyName="Text" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSelLinkedCancel" runat="server" CssClass="Btn_Form" Text="Cancel"
                                            Width="77px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
        </table>
    </asp:Panel>
        <ajax:ModalPopupExtender ID="MPELinkedCars" runat="server" BackgroundCssClass="modalBackground"
            BehaviorID="MBILinkedCars" TargetControlID="lnkSelectCar" PopupControlID="pnlSelLinkedCars"
            CancelControlID="btnSelLinkedCancel">
        </ajax:ModalPopupExtender>
        <table border="0" cellspacing="0" cellpadding="0" style="border-collapse: collapse;
            width: 100%;">
            <tr>
                <td class="TableBorderB">
                    Car Cost
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblCarCost" runat="server" Text=""></asp:Label>
                </td>
                <td class="TableBorderB">
                    Expense
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblExpense" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TableBorderB">
                    Commission
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblCommission" runat="server" Text=""></asp:Label>
                </td>
                <td class="TableBorderB">
                    Sold Price
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblSoldPrice" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TableBorderB">
                    Profit
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblProfit" runat="server" Text=""></asp:Label>
                </td>
                <td class="TableBorderB">
                    Sold To
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblSoldTo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TableBorderB">
                    Purchased From
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblPurchaseFrom" runat="server" Text=""></asp:Label>
                </td>
                <td class="TableBorderB">
                    Come Back From
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblComebackFrom" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="TableBorderB">
                    Designation
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                </td>
                <td class="TableBorderB">
                    Buyer (Purchase Agent)
                </td>
                <td class="TableBorder">
                    <asp:Label ID="lblBuyerName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div> 
    
    <div style="text-align:center">
         <asp:Button ID="btnCancel" CausesValidation="false" Text="Cancel" class="Btn_Form"
            runat="server" OnClick="btnCancel_Click" />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAdd" Text="Add" ValidationGroup="A" class="Btn_Form" runat="server"
            OnClick="btnAdd_Click" Width="75px" OnClientClick="return HideAddButton(this);" />
    </div>
    <table border="0" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td valign="top" style="padding: 0px 2px 0px 2px;">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td valign="top" width="730" class="RightPanel">
                            <!-- Right Panel/Content Panel Start -->
                            <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                
                               
                              
                                <tr>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td class="height30">
                                        &nbsp;
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td align="center">
                                       
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td class="height30">
                                        &nbsp;
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <!-- Right Panel/Content Panel End -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function HideAddButton(btn) {
            $('#<%=btnAdd.ClientID %>').hide();
            return true;
        }
    </script>
</asp:content>
