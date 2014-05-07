<%@ Page Language="C#" MasterPageFile="~/UI/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ManageInventory.aspx.cs" Inherits="METAOPTION.UI.ManageInventory"
    Title="HeadStartVMS" %>

<%@ MasterType VirtualPath="~/UI/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
    function validateAmount(sender,args)
    {
        var txt = (args.Value);
        var startindex= txt.indexOf(".")
        var lastindex=txt.lastIndexOf(".")
        
        if(startindex != lastindex)
        {
           
            args.IsValid = false;
             return;
        }
             
        args.IsValid = true;
    }
    
    function CheckAmountDifference(val)
    {
      var txtsoldpriceOrDepositAmt= args.Value;
      var txtcarcost= document.getElementById("ctl00$ContentPlaceHolder1$frmViewEditDealerDetails$txtCost");
      //var diff = val1 - txtsoldpriceOrDepositAmt
      //if(val >3000 || val < -3000)
        alert('test');
//       {
//       
//        args.IsValid = false;
//             return;
//       }
//      args.IsValid = true;
    }
    </script>
    
     <script type="text/javascript">
    function GetCustomerId(val)
    {
        var txtcustomer = document.getElementById(val);
        
       // var hfId = val.replace("txtCustomerName", "hdCustomerId")
        var hfcontrol = document.getElementById('<%=hdComeBackDealer.ClientID%>');
        var str = (txtcustomer.value).split("ID:");
        if(str.length > 1)
        {
            txtcustomer.value = str[0];
            hfcontrol.value = str[1];
        }
    }
    </script>

    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
        <tr>
            <td nowrap="nowrap">
                <asp:Button ID="btnMoveToInv" class="Btn_Form" Text="Move to Inventory" Enabled="false"
                    runat="server" OnClick="btnMoveToInv_Click" />
                &nbsp; &nbsp;
                <asp:Button ID="btnMoveToArch" class="Btn_Form" Text="Move to Archive" Enabled="false"
                    runat="server" OnClick="btnMoveToArch_Click" />
                &nbsp; &nbsp;
                <asp:Button ID="btnMoveToOnHand" class="Btn_Form" Text="Move to On-Hand" Enabled="false"
                    runat="server" OnClick="btnMoveToOnHand_Click" />
                &nbsp; &nbsp;
                <asp:Button ID="btnBillofSale" class="Btn_Form" Text="Bill of Sale" Enabled="false"
                    runat="server" OnClick="btnBillofSale_Click" />
                &nbsp; &nbsp;
                <asp:Button ID="btnDeleteInventory" class="Btn_Form" Text="Delete Inventory" Visible="false"
                    runat="server" OnClick="btnDeleteInventory_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this Inventory? You will not be able to access this Inventory once deleted\n\nClick Ok to Confirm\nClick Cancel to ignore');" />
                &nbsp; &nbsp;
                <asp:Button ID="btnBackLaneAssignment" class="Btn_Form" Text="<< Lane" Visible="false"
                    runat="server" OnClick="btnBackLaneAssignment_Click" />
            </td>
        </tr>
        <%--    <tr>
            <td align="right" style="padding-top: 10px" visible="false">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; vertical-align: bottom;
                    font-weight: bold; text-align: center;">
                    <tr>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkExpense" runat="server" OnClick="lnkExpense_Click"></asp:LinkButton>
                        </td>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkNotes" runat="server" OnClick="lnkNotes_Click"></asp:LinkButton>
                        </td>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkDocuments" runat="server" OnClick="lnkDocuments_Click"></asp:LinkButton>
                        </td>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkDrivers" runat="server" OnClick="lnkDrivers_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2" class="height30">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="" border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td colspan="2">
                            <div id="dvCarDetails" runat="server" class="TableHeadingBg">
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="TableHeading">
                                            <asp:Label ID="lblCarStatus" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td align="right" class="HeadingEditButton">
                                            <asp:ImageButton ID="btnEditCarDetails" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:FormView ID="frmCarDetails" Width="100%" runat="server" OnDataBound="frmCarDetails_DataBound">
                                <ItemTemplate>
                                    <table width="100%" style="border-collapse: collapse">
                                        <tr>
                                            <td class="TableBorderB">
                                                VIN
                                            </td>
                                            <td class="TableBorder" runat="server">
                                                <%#Eval("VIN")%>
                                                <%--Use this Hidden label to SET VIN PROPERTY to be accessible throughout the page--%>
                                                <asp:Label ID="lblVIN" Visible="false" runat="server" Text='<%#Eval("VIN")%>'></asp:Label>
                                                <%--Use this Hidden label to display value in lblCarStatus Upper DIV Header--%>
                                                <asp:Label ID="lblCarStat" Visible="false" runat="server" Text='<%#Eval("CarStatus")%>'></asp:Label>
                                            </td>
                                            <td class="TableBorderB">
                                                Mileage In
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("MileageIn")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Year
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("Year")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Make
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("Make")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Model
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("Model")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Body
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("Body")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Special Equipment
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("DesignatedEquipment")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Arrival Date
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("ArrivalDate")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Vehicle Present
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("VehiclePresent")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Exterior Color
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("Ext1Desc")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Interior Color
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("IntDesc")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Engine
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("EngineType")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Trans
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("TransType")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Wheel Drive
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("WheelDrive")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Car Location
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("CarLocation")%>
                                            </td>
                                            <td class="TableBorderB" nowrap="nowrap">
                                                Regular Lane Number
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("RegularLane")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Exotic Lane Number
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("ExoticLane")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Virtual Lane Number
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("VirtualLane")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Online&nbsp; Lane Number
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("OnlineLane")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Note
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("CarNote")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Average MMR
                                            </td>
                                            <td class="TableBorder" colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trOldMMBC">
                                            <td class="TableBorderB">
                                                Old MMBC
                                            </td>
                                            <td class="TableBorder" colspan="3">
                                                <%#Eval("OldMMBC")%>
                                                <asp:Label ID="lbltemp" Text='<%#Eval("OldMMBC")%>' Visible="false" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="FooterContentDetails">
                                                <asp:Label ID="lblCarHistory" runat="server" Text='<%# Eval("UpdatedHistory") ?? "&nbsp;"%>'> </asp:Label>
                                            </td>
                                            <td colspan="2" class="FooterContentDetails">
                                                <asp:Label ID="lblCarDetailsDateAdded" runat="server" Text='<%# Eval("AddedBy") ?? "&nbsp;"%>'> </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:FormView>
                            <asp:Panel ID="pnlPopEditCarDet" runat="server" Style="display: none;" CssClass="modalPopup"
                                Width="700px">
                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="PopUpBoxHeading">
                                            &nbsp;&nbsp; EDIT CAR DETAILS
                                        </td>
                                        <td class="PopUpBoxHeading" align="right">
                                            <img alt="" border="0" src="../Images/close.gif" onclick="$find('mdpopCarUpdate').hide();return false;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="2" style="padding: 10px">
                                            <asp:FormView ID="frmCarDetailsUpdate" runat="server" OnDataBound="frmCarDetailsUpdate_DataBound">
                                                <ItemTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                VIN
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVinNo" Text='<%#Eval("VIN")%>' MaxLength="17" CssClass="txtMan2"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Mileage In
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtMileageIn" MaxLength="14" CssClass="txtMan2" Text='<%#Eval("MileageIn")%>'
                                                                    runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtMileageIn_Extender" runat="server" FilterType="Numbers"
                                                                    InvalidChars="." TargetControlID="txtMileageIn">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Year
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" DataTextField="Year"
                                                                    DataValueField="Year" CssClass="txtMan2" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Make
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:UpdatePanel ID="updPnlMake" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="True" DataTextField="Make"
                                                                            DataValueField="MakeId" CssClass="txtMan2" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlYear" />
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
                                                                        <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="True" DataTextField="Model"
                                                                            DataValueField="ModelId" CssClass="txtMan2" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlMake" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Body
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:UpdatePanel ID="upPnlBody" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlBody" runat="server" AutoPostBack="True" DataTextField="Body"
                                                                            DataValueField="BodyId" CssClass="txt2" OnSelectedIndexChanged="ddlBody_SelectedIndexChanged">
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
                                                                Special Equipment
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtDEquipment" Text='<%#Eval("DesignatedEquipment")%>' runat="server"
                                                                    CssClass="txtMan2"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Arrival Date
                                                            </td>
                                                            <td class="TableBorder" nowrap="nowrap">
                                                                <asp:UpdatePanel ID="updPnlArrivalDate" UpdateMode="Conditional" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:TextBox ID="txtArrivalDate" Text='<%#Eval("ArrivalDate")%>' runat="server" CssClass="txtMan2"></asp:TextBox>
                                                                        <ajax:CalendarExtender ID="CalendarExtender1" TargetControlID="txtArrivalDate" PopupButtonID="imgArrivalDate"
                                                                            runat="server">
                                                                        </ajax:CalendarExtender>
                                                                        <asp:Image ID="imgArrivalDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                                                            Style="cursor: pointer;" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlVehiclePresent" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Vehicle Present
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlVehiclePresent" AutoPostBack="true" runat="server" CssClass="txtMan2"
                                                                    OnSelectedIndexChanged="ddlVehiclePresent_SelectedIndexChanged">
                                                                    <asp:ListItem Value="True">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="False">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Exterior Color
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:UpdatePanel ID="updPnlExtCol" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlExtCol" runat="server" AppendDataBoundItems="true" CssClass="txtMan2">
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
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Interior Color
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:UpdatePanel ID="updPnl" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlIntCol" runat="server" AppendDataBoundItems="true" CssClass="txtMan2">
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
                                                                Engine
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlEngine" runat="server" DataTextField="EngineType" DataValueField="EngineId"
                                                                    DataSourceID="objEngines" CssClass="txtMan2">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objEngines" runat="server" SelectMethod="GetEngineList"
                                                                    TypeName="METAOPTION.BAL.Common"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Trans
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlTrans" runat="server" DataTextField="TransType" DataValueField="TransId"
                                                                    DataSourceID="objTrans" CssClass="txtMan2">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objTrans" runat="server" SelectMethod="GetTransList" TypeName="METAOPTION.BAL.Common">
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Wheel Drive
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:DropDownList ID="ddlWheelDrive" DataTextField="WheelDrive" DataValueField="WheelDriveId"
                                                                    runat="server" DataSourceID="objWheelDrives" CssClass="txtMan2">
                                                                </asp:DropDownList>
                                                                <asp:ObjectDataSource ID="objWheelDrives" runat="server" SelectMethod="GetWheelDriveList"
                                                                    TypeName="METAOPTION.BAL.Common"></asp:ObjectDataSource>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Car Location
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="ddlCarLocation" runat="server" Text='<%#Eval("CarLocation")%>' CssClass="txtMan2"></asp:TextBox>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Regular Lane Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtRegLaneNoPopUp" MaxLength="7" CssClass="txt1" Width="53px" Text='<%#Eval("RegularLane")%>'
                                                                    runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revRegularLaneNumber" ValidationGroup="lanevalidation"
                                                                    Display="Dynamic" ControlToValidate="txtRegLaneNoPopUp" runat="server" ValidationExpression="^\d{2}-\w{4}"
                                                                    ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                <%--<ajax:MaskedEditExtender ID="MEERLaneNo" runat="server" Mask="99-9999" Filtered="0123456789"
                                                                    TargetControlID="txtRegLaneNoPopUp" ClearMaskOnLostFocus="false">
                                                                </ajax:MaskedEditExtender>
                                                                <ajax:MaskedEditValidator ID="MEVRLaneNO" runat="server" ControlExtender="MEERLaneNo"
                                                                    ControlToValidate="txtRegLaneNoPopUp" IsValidEmpty="True" SetFocusOnError="true"
                                                                    InvalidValueMessage="Invalid Format">    
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                                                </ajax:MaskedEditValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Exotic Lane Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtExoLaneNumPopUp" MaxLength="7" CssClass="txt1" Width="53px" Text='<%#Eval("ExoticLane")%>'
                                                                    runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revExoticLaneNumber" Display="Dynamic" ControlToValidate="txtExoLaneNumPopUp"
                                                                    ValidationExpression="^\d{2}-\w{4}" ValidationGroup="lanevalidation" runat="server"
                                                                    ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                <%-- <ajax:MaskedEditExtender ID="MEEELaneNo" runat="server" Mask="99-9999" Filtered="0123456789"
                                                                    TargetControlID="txtExoLaneNumPopUp" ClearMaskOnLostFocus="false">
                                                                </ajax:MaskedEditExtender>
                                                                <ajax:MaskedEditValidator ID="MEVLaneNo" runat="server" ControlExtender="MEEELaneNo"
                                                                    ControlToValidate="txtExoLaneNumPopUp" IsValidEmpty="True" SetFocusOnError="true"
                                                                    InvalidValueMessage="Invalid Format">    
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                                                </ajax:MaskedEditValidator>--%>
                                                            </td>
                                                            <td class="TableBorderB">
                                                                Virtual Lane Number
                                                            </td>
                                                            <td class="TableBorder">
                                                                <asp:TextBox ID="txtVirLaneNoPopUp" MaxLength="7" CssClass="txt1" Width="53px" Text='<%#Eval("VirtualLane")%>'
                                                                    runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revVirtualLaneNumber" Display="Dynamic" ControlToValidate="txtVirLaneNoPopUp"
                                                                    ValidationGroup="lanevalidation" ValidationExpression="^\d{2}-\w{4}" runat="server"
                                                                    ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                <%-- <ajax:MaskedEditExtender ID="MEEVLaneNo" runat="server" Mask="99-9999" Filtered="0123456789"
                                                                    TargetControlID="txtVirLaneNoPopUp" ClearMaskOnLostFocus="false">
                                                                </ajax:MaskedEditExtender>
                                                                <ajax:MaskedEditValidator ID="MEVVLaneNo" runat="server" ControlExtender="MEEVLaneNo"
                                                                    ControlToValidate="txtVirLaneNoPopUp" IsValidEmpty="True" SetFocusOnError="true"
                                                                    InvalidValueMessage="Invalid Format">    
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                                                </ajax:MaskedEditValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Online Lane Number &nbsp;
                                                            </td>
                                                            <td class="TableBorder" colspan="4">
                                                                <asp:TextBox ID="txtOnlineLanPopUp" MaxLength="7" CssClass="txt1" Width="53px" Text='<%#Eval("OnlineLane")%>'
                                                                    runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revOnlineLaneNumber" Display="Dynamic" ControlToValidate="txtOnlineLanPopUp"
                                                                    ValidationGroup="lanevalidation" ValidationExpression="^\d{2}-\w{4}" runat="server"
                                                                    ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                <%--<ajax:MaskedEditExtender ID="MEEOnlineLaneNo" runat="server" Mask="99-9999" Filtered="0123456789"
                                                                    TargetControlID="txtOnlineLanPopUp" ClearMaskOnLostFocus="false">
                                                                </ajax:MaskedEditExtender>
                                                                <ajax:MaskedEditValidator ID="MEVOnLineLaneNumber" runat="server" ControlExtender="MEEOnlineLaneNo"
                                                                    ControlToValidate="txtOnlineLanPopUp" IsValidEmpty="True" SetFocusOnError="true"
                                                                    InvalidValueMessage="Invalid Format">    
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                                                </ajax:MaskedEditValidator>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TableBorderB">
                                                                Note
                                                            </td>
                                                            <td class="TableBorder" colspan="3">
                                                                <asp:TextBox ID="txtNote" CssClass="txtMulti" TextMode="MultiLine" Rows="5" Width="100%"
                                                                    Text='<%#Eval("CarNote")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00$ContentPlaceHolder1$frmCarDetailsUpdate$txtcount', 255)" />
                                                                <asp:TextBox ID="txtcount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                                    Width="30px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:FormView>
                                            <asp:ObjectDataSource ID="objGetCarDetails" runat="server" SelectMethod="GetCarDetails"
                                                TypeName="METAOPTION.BAL.InventoryBAL">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="inventoryId" QueryStringField="Code" Type="Int64" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnEditPopCarDetCancel" runat="server" CausesValidation="false" CssClass="Btn_Form"
                                                Width="75px" Text="Cancel" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnEditPopCarDet" Text="Save" ValidationGroup="lanevalidation" CssClass="Btn_Form"
                                                Width="75px" runat="server" OnClick="btnEditPopCarDet_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <ajax:ModalPopupExtender ID="MPECarDetailsUpdate" BehaviorID="mdpopCarUpdate" runat="server"
                                TargetControlID="btnEditCarDetails" PopupControlID="pnlPopEditCarDet" CancelControlID="btnEditPopCarDetCancel"
                                BackgroundCssClass="modalBackground" DropShadow="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td colspan="2">
                            <div id="dvCarProp" runat="server" class="TableHeadingBg">
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="TableHeading">
                                            Car Properties
                                        </td>
                                        <td align="right" class="HeadingEditButton">
                                            <asp:ImageButton ID="imgbtnEditCarProp" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:FormView ID="frmCarProp" Width="100%" runat="server">
                                <ItemTemplate>
                                    <table width="100%" style="border-collapse: collapse">
                                        <tr>
                                            <td class="TableBorderB">
                                                AC
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("AC")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Sun Roof
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("SunMoon")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Power Locks
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("PowerLocks")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Leather
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("Leather")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                Power Windows
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("PowerWindows")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Navigation
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("Navigation")%>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td class="TableBorderB">
                                                Alloy Wheels
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("AlloyWheels")%>
                                            </td>
                                            <td class="TableBorderB">
                                                Power Seats
                                            </td>
                                            <td class="TableBorder">
                                                <%#Eval("PowerSeat")%>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="4" class=" FooterContentDetails">
                                                <asp:Label ID="lblCarPropHistory" runat="server" Text='<%# Eval("UpdatedHistory") ?? "&nbsp;"%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:FormView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" class="height30">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="dvDealerDet" runat="server" class="TableHeadingBg">
                    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                        <tr>
                            <td class="TableHeading">
                                DEALER DETAILS
                            </td>
                            <td align="right" class="HeadingEditButton">
                                <asp:ImageButton ID="btnEditDealerDetails" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:FormView ID="frmDealerDetails" Width="100%" runat="server">
                    <ItemTemplate>
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr>
                                <td class="TableBorderB">
                                    Dealer Name
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("DealerName")%>
                                </td>
                                <td class="TableBorderB">
                                    Buyer
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("BuyerName")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Dealer Designation
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("Designation")%>
                                </td>
                                <td class="TableBorderB">
                                    Purchase Date
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("PurchaseDate")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Cost
                                </td>
                                <td class="TableBorder">
                                    <%# String.Format("{0:c}", Eval("CarCost"))%>
                                </td>
                                <td class="TableBorderB">
                                    Check Number
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("CheckNumber")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Title Present
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("TitlePresent")%>
                                </td>
                                <td class="TableBorderB">
                                    Expense 
                                </td>
                                <td class="TableBorder">
                                  <%#Eval("InventoryExpenseExcludingCarCost")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Title Present Note
                                </td>
                                <td colspan="3" class="TableBorder">
                                    <%#Eval("TitlePresentNotes")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Title Shipped
                                </td>
                                <td colspan="3" class="TableBorder">
                                    <%#Eval("TitleShipped")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Title Shipped Note
                                </td>
                                <td class="TableBorder" colspan="3">
                                    <%#Eval("TitleShippedNotes")%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="FooterContentDetails">
                                    <asp:Label ID="lblDealerHistory" Text='<%# Eval("UpdatedHistory") ?? "&nbsp;"%>'
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="height30">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="dvSoldTo" runat="server" class="TableHeadingBg">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                        <tr>
                            <td class="TableHeading">
                                SOLD TO
                            </td>
                            <td align="right" class="HeadingEditButton">
                                <asp:ImageButton ID="imgbtnEditSoldToDetails" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="height30">
                <asp:FormView ID="frmSoldTo" Width="100%" runat="server">
                    <ItemTemplate>
                        <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                            <tr >
                                <td class="TableBorderB"  >
                                    Customer Name
                                </td>
                                <td class="TableBorder" colspan="3">
                                    <%#Eval("DealerName")%>
                                </td>
                            </tr>
                            <tr>
                             <td class="TableBorderB">
                                    Date Sold
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("SoldDate")%>
                                </td>
                                <td class="TableBorderB">
                                    Market Price
                                </td>
                                <td class="TableBorder">
                                    <%# String.Format("{0:c}", Eval("MarketPrice"))%>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Actual Cost
                                </td>
                                <td class="TableBorder">
                                    <%# String.Format("{0:c}", Eval("ActualCost"))%>
                                </td>
                                <td class="TableBorderB">
                                    Price Sold
                                </td>
                                <td class="TableBorder">
                                    <%# String.Format("{0:c}", Eval("SoldPrice"))%>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Margin On Market Price
                                </td>
                                <td class="TableBorder" nowrap="nowrap">
                                    <%# String.Format("{0:c}", Eval("MarginMarketPrice"))%>&nbsp;
                                    <%# String.Format("{0:p}", Eval("MarginMarketPricePercent"))%>
                                </td>
                                <td class="TableBorderB">
                                    Margin On Sold Price
                                </td>
                                <td class="TableBorder" nowrap="nowrap">
                                    <%# String.Format("{0:c}", Eval("MarginSoldPrice"))%>&nbsp;
                                    <%# String.Format("{0:p}", Eval("MarginSoldPricePercent"))%>
                                </td>
                               
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Mileage Out
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("MileageOut")%>
                                </td>
                                <td class="TableBorderB">
                                    Deposit Date
                                </td>
                                <td class="TableBorder">
                                    <%#Eval("DepositDate")%>
                                </td>
                                
                            </tr>
                            <tr>
                            <td class="TableBorderB">
                                    Deposit Amount
                                </td>
                                <td class="TableBorder">
                                    <%# String.Format("{0:c}", Eval("DepositAmount"))%>
                                </td>
                                <td class="TableBorderB">
                                    Deposit Bank Account
                                </td>
                                <td colspan="3" class="TableBorder">
                                    <%# Eval("BankName") ?? "&nbsp;"%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Deposit Comments
                                </td>
                                <td colspan="3" class="TableBorder">
                                    <%#Eval("DepositComment")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Sold
                                </td>
                                <td colspan="3" class="TableBorder">
                                    <%#Eval("SoldStatus")%>&nbsp;&nbsp;<asp:ImageButton ID="imgOpenSoldStatusPopUp" ImageUrl="~/Images/edit-icon.jpg"
                                        Height="21" alt="" runat="server" OnClick="imgOpenSoldStatusPopUp_Click" />
                                    <ajax:ModalPopupExtender ID="MPEComeBackStatus" runat="server" BehaviorID="mdpopupComeBackStatus"
                                        TargetControlID="imgOpenComeBackPopUp" PopupControlID="pnlComeBackDetails" BackgroundCssClass="modalBackground"
                                        DropShadow="true" CancelControlID="btnComeBackCancel" />
                                    <asp:Label ID="lblSoldHistoryLastUpdated" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Sold Comments
                                </td>
                                <td colspan="3" class="TableBorder">
                                    <%#Eval("SoldComment")%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Come Back
                                </td>
                                <td class="TableBorder" colspan="3">
                                    <%#Eval("ComeBackStatus")%>&nbsp;&nbsp;
                                    <asp:ImageButton ID="imgOpenComeBackPopUp" ImageUrl="~/Images/edit-icon.jpg" Height="21"
                                        alt="" runat="server" />
                                    <asp:Label ID="lblComebackLastUpdatedHistory" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableBorderB">
                                    Come Back Comments
                                </td>
                                <td class="TableBorder" colspan="3">
                                    <asp:Label ID="lblComment" runat="server" Text='<%#Eval("ComeBackComments")%>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="FooterContentDetails" colspan="4">
                                    <asp:Label ID="lblSoltToHistory" Text='<%# Eval("UpdatedHistory") ?? "&nbsp;"%>'
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:FormView>
                <table id="pnlComeBackDetails" runat="server" style="display: none;" class="modalPopup"
                    width="656px" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="PopUpBoxHeading">
                            &nbsp;&nbsp;Update Comeback Status
                             <asp:HiddenField ID="hdComeBackDealer" runat="server" />
                        </td>
                        <td class="PopUpBoxHeading" align="right">
                            <img border="0" src="../Images/close.gif" alt="" onclick="$find('mdpopupComeBackStatus').hide();return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="padding: 10px">
                            <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse"
                                class="Nornmal-Arial-12">
                                <tr>
                                    <td nowrap="nowrap" class="GridContent_padding5">
                                        <b>Comeback Status</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <asp:DropDownList ID="ddlComebackStatus" EnableViewState="true" runat="server" CssClass="txtMan2">
                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvComebackStatus" ValidationGroup="comebackValGroup"
                                            ControlToValidate="ddlComebackStatus" runat="server" InitialValue="False" SetFocusOnError="true"
                                            ErrorMessage="Select Yes to Save Comeback Details "></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridContent_padding5" nowrap="nowrap">
                                        <b>Comeback Reason</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <asp:DropDownList ID="ddlComeBackReason" AppendDataBoundItems="true" runat="server"
                                            EnableViewState="true" CssClass="txtMan2" DataTextField="ComeBackReason" DataValueField="ComeBackReasonId">
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:ObjectDataSource ID="objComeBackReason" runat="server" SelectMethod="GetComeBackReason"
                                            TypeName="METAOPTION.BAL.InventoryBAL"></asp:ObjectDataSource>--%>
                                        <asp:RequiredFieldValidator ID="rfvComebackReason" runat="server" ControlToValidate="ddlComeBackReason"
                                            InitialValue="0" ValidationGroup="comebackValGroup" ErrorMessage="*" SetFocusOnError="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" class="GridContent_padding5">
                                        <b>Comeback Date</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    <asp:TextBox ID="txtComeBackDate" runat="server" CssClass="txtMan2"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="caltxtComeBackDate" runat="server" PopupButtonID="imgComeBackDate"
                                                        TargetControlID="txtComeBackDate">
                                                    </ajax:CalendarExtender>
                                                    <asp:Image ID="imgComeBackDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                                        Style="cursor: pointer;" />
                                                </td>
                                                <td style="padding: 0px 5px 0px 0px">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" class="GridContent_padding5">
                                        <b>Mileage In</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <asp:TextBox ID="txtMileageIn" MaxLength="14" runat="server" CssClass="txtMan2"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="txtMileageIn_Extender" runat="server" FilterType="Numbers"
                                            InvalidChars="." TargetControlID="txtMileageIn">
                                        </ajax:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" class="GridContent_padding5">
                                        <b>Comeback from Dealer</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <asp:TextBox ID="txtComeBackDealer" Width="450"  class="FormItem" runat="server" onblur="GetCustomerId(this.id)" CssClass="txtMan2" ToolTip="Type at least two characters to find customer name started with i.e MA or %MA to find all customer names having characters entered"></asp:TextBox>
                                         <ajax:AutoCompleteExtender ID="txtComeBackDealer_AutoCompleteExtender" runat="server" TargetControlID="txtComeBackDealer"
                                                            ServicePath="../WS/AutoFillCustomers.asmx" ServiceMethod="AutoFillCustomers" EnableCaching="true"
                                                            MinimumPrefixLength="2" CompletionSetCount="25"  CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                          DelimiterCharacters=";, :">
                                                        </ajax:AutoCompleteExtender>
                                        <asp:LinkButton ID="lnkSelComeBackDealer" Visible="false" Text="Select Dealer" runat="server" OnClientClick="$find('mdpopupComeBackStatus').hide();return false;"></asp:LinkButton>
                                          <asp:TextBox ID="txtChargeBackComm" Visible="false" CssClass="txtMan2" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td nowrap="nowrap" class="GridContent_padding5">
                                        <b>Chargeback Commission</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <asp:TextBox ID="txtChargeBackComm" Visible="false" CssClass="txtMan2" runat="server"></asp:TextBox>
                                        <%--<asp:CustomValidator ID="cvCommission" runat="server" ControlToValidate="txtChargeBackComm"
                                            ErrorMessage="Invalid Value" ClientValidationFunction="validateAmount"></asp:CustomValidator>
                                        <ajax:FilteredTextBoxExtender ID="txtChargeBackComm_FilteredTextBoxExtender" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtChargeBackComm">
                                        </ajax:FilteredTextBoxExtender>
                                        <br />
                                        Note:- Buyer must be selected against inventory for posting Charge Back Commission
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="GridContent_padding5" valign="top">
                                        <b>Comments</b>
                                    </td>
                                    <td class="GridContent_padding5">
                                        <asp:TextBox ID="txtComeBackComments" CssClass="txtMulti" TextMode="MultiLine" runat="server"
                                            Height="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 10px" align="center">
                                        <asp:Button ID="btnComeBackCancel" runat="server" CausesValidation="false" Text="Cancel"
                                            CssClass="Btn_Form" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnComeBackSave" Text="Save" ValidationGroup="comebackValGroup"
                                            runat="server" Width="59px" CssClass="Btn_Form" OnClick="btnComeBackSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="height30">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                    <tr>
                        <td colspan="2">
                            <div id="dvLinkedCars" runat="server" class="TableHeadingBg">
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="TableHeading" colspan="2" valign="middle">
                                            Linked Cars
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:GridView runat="server" Width="100%" ID="gvLinkedFilter" AllowPaging="True"
                                PageSize="5" AutoGenerateColumns="False" EmptyDataText="No Rows found" OnPageIndexChanging="gvLinkedFilter_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField DataField="DateAdded" HeaderText="Date Added" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                    <asp:TemplateField HeaderText="Linked Cars" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylnk" runat="server" Text='<%#Eval("VIN")%>' NavigateUrl='<%#"~/UI/ManageInventory.aspx?Code="+Eval("InventoryId")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DealerName" HeaderText="Dealer" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent" />
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer" HeaderStyle-CssClass="GridHeader"
                                        ItemStyle-CssClass="GridContent" />
                                    <asp:TemplateField SortExpression="CheckNumber" HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader" HeaderText="Check No."
                                        ItemStyle-CssClass="GridContent">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hylnk" runat="server" Text='<%#Eval("CheckNumber")%>' NavigateUrl='<%#"~/UI/ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId")%>' ></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Amount" HeaderText="Car Cost" DataFormatString="{0:C}&nbsp;"
                                        HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SoldDate" HeaderText="Sold On" DataFormatString="{0:MM/dd/yyyy}"
                                        HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                </Columns>
                                <AlternatingRowStyle CssClass="gvAlternateRow" />
                                <HeaderStyle CssClass="TableBorderB" />
                                <RowStyle CssClass="TableBorder" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <br />
            <td colspan="2" align="right" style="padding-top: 10px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; vertical-align: bottom;
                    font-weight: bold; text-align: center;">
                    <tr>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkExpense" runat="server" OnClick="lnkExpense_Click"></asp:LinkButton>
                        </td>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkNotes" runat="server" OnClick="lnkNotes_Click"></asp:LinkButton>
                        </td>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkDocuments" runat="server" OnClick="lnkDocuments_Click"></asp:LinkButton>
                        </td>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkDrivers" runat="server" OnClick="lnkDrivers_Click"></asp:LinkButton>
                        </td>
                        <td class="FooterLink">
                            <asp:LinkButton ID="lnkInvEvents" runat="server" Text="Inventory Events" OnClick="lnkInventoryEvents_Click"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="height30">
            </td>
        </tr>
        <tr>
            <td colspan="2" class="height30">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlPopEditCarProp" Width="550px" runat="server" Style="display: none;"
        CssClass="modalPopup">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="PopUpBoxHeading">
                    &nbsp;&nbsp; EDIT CAR PROPERTIES
                </td>
                <td class="PopUpBoxHeading" align="right">
                    <img border="0" src="../Images/close.gif" onclick="$find('mdpopCarProp').hide();return false;"
                        alt="" />
                </td>
            </tr>
            <tr>
                <%-- <td>
                    <asp:CheckBoxList ID="chkCarProp1" Height="100px" runat="server">
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;AC</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Power Locks</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Leather</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Alloy Wheels</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:CheckBoxList ID="chkCarProp2" runat="server" Height="100px" class="TableListItem">
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Power Windows</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Sun Moon</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Navigation</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Power Seats</asp:ListItem>
                    </asp:CheckBoxList>
                </td>--%>
                <td>
                    <asp:CheckBoxList ID="chkCarProp1" runat="server">
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;AC</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Powerlocks</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Power Windows</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td>
                    <asp:CheckBoxList ID="chkCarProp2" runat="server" class="TableListItem">
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Sun Roof</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Leather</asp:ListItem>
                        <asp:ListItem class="TableListItem">&nbsp;&nbsp;Navigation</asp:ListItem>
                        <%-- <asp:ListItem  class="TableListItem">&nbsp;&nbsp;Alloy Wheels</asp:ListItem>--%>
                        <%-- <asp:ListItem class="TableListItem">&nbsp;&nbsp;Power Seats</asp:ListItem>--%>
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="btnCarPropCancel" runat="server" Text="Cancel" CssClass="Btn_Form"
                        Width="75px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="carPopPropertyEdit" runat="server" Text="Save" OnClick="carPopPropertyEdit_Click"
                        CssClass="Btn_Form" Width="75px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlEditDealerDetails" Width="700px" runat="server" CssClass="modalPopup">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="PopUpBoxHeading">
                    &nbsp;&nbsp; EDIT DEALER DETAILS
                </td>
                <td class="PopUpBoxHeading" align="right">
                    <img border="0" src="../Images/close.gif" onclick="$find('mdpop').hide();return false;"
                        alt="" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="padding: 10px">
                    <asp:FormView ID="frmViewEditDealerDetails" Width="100%" runat="server">
                        <ItemTemplate>
                            <table width="100%" style="border-collapse: collapse">
                                <tr>
                                    <td class="TableBorderB">
                                        Dealer Name
                                    </td>
                                    <td class="TableBorder" nowrap="nowrap">
                                        <asp:TextBox ID="txtDealerShip" MaxLength="50" CssClass="txtMan2" Text='<%#Eval("DealerName")%>'
                                            runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:HiddenField ID="hdDealerIdSel" Value='<%# Eval("DealerId") ?? "&nbsp;"%>' runat="server" />
                                        <asp:LinkButton ID="lnkSelectDealer" runat="server" OnClientClick="$find('mdpop').hide();return false;"
                                            CssClass="GridContent_Link" Text="Select Dealer"></asp:LinkButton>
                                        <ajax:ModalPopupExtender ID="MPESelectDealer" BehaviorID="mdPopUpSelectDealer" runat="server"
                                            TargetControlID="lnkSelectDealer" CancelControlID="btnDealerPopUpCancel" PopupControlID="pnlDealerSearch"
                                            BackgroundCssClass="modalBackground" DropShadow="true" />
                                    </td>
                                    <td class="TableBorderB" nowrap>
                                        Buyer
                                    </td>
                                    <td class="TableBorder">
                                        <asp:DropDownList ID="ddlBuyer" SelectedValue='<%#Eval("BuyerId")%>' AppendDataBoundItems="true"
                                            runat="server" DataSourceID="objBuyer" DataTextField="BuyerName" DataValueField="BuyerId"
                                            CssClass="txtMan2">
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="objBuyer" runat="server" SelectMethod="GetBuyerList" TypeName="METAOPTION.BAL.Common">
                                        </asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Designation
                                    </td>
                                    <td class="TableBorder">
                                        <asp:DropDownList ID="ddlDesig" runat="server" SelectedValue='<%#Eval("DesignationId")%>'
                                            AppendDataBoundItems="true" DataTextField="Designation" DataValueField="DesignationId"
                                            DataSourceID="objDesignation" CssClass="txtMan2">
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="objDesignation" runat="server" SelectMethod="GetDesignationList"
                                            TypeName="METAOPTION.BAL.Common"></asp:ObjectDataSource>
                                    </td>
                                    <td class="TableBorderB">
                                        Check Number
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtCheckNumber" MaxLength="50" CssClass="txtMan2" Text=' <%#Eval("CheckNumber")%>'
                                            runat="server"></asp:TextBox>
                                       <ajax:FilteredTextBoxExtender ID="txtCheckNumber_FilteredTextBoxExtender" runat="server"
                                             FilterType="Numbers" TargetControlID="txtCheckNumber">
                                        </ajax:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Cost
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtCost" MaxLength="14" Text='<%#Eval("CarCost")%>' CssClass="txtMan2"
                                            runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="cvCost" runat="server" ControlToValidate="txtCost" ErrorMessage="Invalid Amount"
                                            ClientValidationFunction="validateAmount"></asp:CustomValidator>
                                        <ajax:FilteredTextBoxExtender ID="txtCost_FilteredTextBoxExtender" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtCost">
                                        </ajax:FilteredTextBoxExtender>
                                    </td>
                                    <td class="TableBorderB">
                                        Purchase Date
                                    </td>
                                    <td class="TableBorder" nowrap="nowrap">
                                        <asp:TextBox ID="txtPurchaseDate" CssClass="txtMan2" Text='<%#Eval("PurchaseDate")%>'
                                            runat="server"></asp:TextBox>
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgPurDate"
                                            TargetControlID="txtPurchaseDate">
                                        </ajax:CalendarExtender>
                                        <asp:Image ID="imgPurDate" runat="server" ImageUrl="~/Images/calender-icon.gif" Style="cursor: pointer;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Title Present
                                    </td>
                                    <td align="left" class="TableBorder" colspan="3">
                                        <asp:DropDownList ID="ddlTitlePresent" AutoPostBack="true" runat="server" CssClass="txt1"
                                            OnSelectedIndexChanged="ddlTitlePresent_SelectedIndexChanged">
                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB" nowrap="nowrap">
                                        Title Present Note
                                    </td>
                                    <td class="TableBorder" colspan="3">
                                        <asp:UpdatePanel ID="updPnlTitlePresentNotes" UpdateMode="Conditional" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTitlePresent" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtTitlePresentNotes" Width="100%" Rows="5" CssClass="txtMulti"
                                                    TextMode="MultiLine" Text='<%#Eval("TitlePresentNotes")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00$ContentPlaceHolder1$frmViewEditDealerDetails$txtcount1', 255)" />
                                                <asp:TextBox ID="txtcount1" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                    Width="30px" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Title Shipped
                                    </td>
                                    <td align="left" class="TableBorder" colspan="3">
                                        <asp:DropDownList ID="ddlTitleShipped" AutoPostBack="true" runat="server" CssClass="txt1"
                                            OnSelectedIndexChanged="ddlTitleShipped_SelectedIndexChanged">
                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB" nowrap="nowrap">
                                        Title Shipped Note
                                    </td>
                                    <td colspan="3" class="TableBorder">
                                        <asp:UpdatePanel ID="updPnlTitleShipped" UpdateMode="Conditional" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlTitleShipped" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtTitleShippedNotes" CssClass="txtMulti" Rows="5" Width="100%"
                                                    TextMode="MultiLine" Text='<%#Eval("TitleShippedNotes")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00$ContentPlaceHolder1$frmViewEditDealerDetails$txtcount2', 255)" />
                                                <asp:TextBox ID="txtcount2" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                    Width="30px" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnEditDealerCancel" runat="server" Text="Cancel" CssClass="Btn_Form"
                                    Width="75px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnEditDealerSave" Text="Save" runat="server" Width="75px" OnClick="btnSaveDealerDetails"
                                    CssClass="Btn_Form" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlPopEditSoldTo" runat="server" Style="display: none;" CssClass="modalPopup"
        Width="700px">
        <table border="0" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="PopUpBoxHeading">
                    &nbsp;&nbsp; EDIT SOLD TO DETAILS
                </td>
                <td class="PopUpBoxHeading" align="right">
                    <img border="0" src="../Images/close.gif" onclick="$find('mdpopupSoldTo').hide();return false;"
                        alt="" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="padding: 10px">
                    <asp:FormView ID="frmEditSoldTo" Width="100%" runat="server">
                        <ItemTemplate>
                            <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                                <tr>
                                    <td class="TableBorderB">
                                        Customer Name
                                    </td>
                                    <td class="TableBorder" nowrap="nowrap">
                                        <asp:TextBox ID="txtCustomerName" MaxLength="50" CssClass="txtMan2" Text='<%#Eval("DealerName")%>'
                                            runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:LinkButton ID="lnkSelectCustomer" OnClientClick="$find('mdpopupSoldTo').hide();return false;"
                                            runat="server" Text="Select Customer" CssClass="GridContent_Link"></asp:LinkButton>
                                        <ajax:ModalPopupExtender ID="MPESelectCustomer" BehaviorID="mdPopupSelectCustomer"
                                            runat="server" TargetControlID="lnkSelectCustomer" CancelControlID="btnEditCancelCust"
                                            PopupControlID="pnlCustomerSearch" BackgroundCssClass="modalBackground" DropShadow="true" />
                                    </td>
                                    <td class="TableBorderB">
                                        Date Sold
                                    </td>
                                    <td class="TableBorder" nowrap="nowrap">
                                        <asp:TextBox ID="txtSoldDate" Text='<%#Eval("SoldDate")%>'  CssClass="txtMan2" runat="server"></asp:TextBox>
                                        <ajax:CalendarExtender ID="calSoldDate" runat="server" PopupButtonID="imgSoldDate"
                                            TargetControlID="txtSoldDate">
                                        </ajax:CalendarExtender>
                                        <asp:Image ID="imgSoldDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                            Style="cursor: pointer;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Market Price
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtMarketPrice" MaxLength="14" CssClass="txtMan2" Text='<%#Eval("MarketPrice")%>'
                                            runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="cvMarketPrice" runat="server" ControlToValidate="txtMarketPrice"
                                            ErrorMessage="Invalid Amount" ClientValidationFunction="validateAmount"></asp:CustomValidator>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtMarketPrice">
                                        </ajax:FilteredTextBoxExtender>
                                    </td>
                                    <td class="TableBorderB">
                                        &nbsp;&nbsp;
                                        <%-- Actual Cost--%>
                                    </td>
                                    <td class="TableBorder">
                                        &nbsp;&nbsp;
                                        <%-- <asp:TextBox ID="txtActualCost" CssClass="txtMan2" runat="server"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Price Sold
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtPriceSold" CssClass="txtMan2" MaxLength="14" Text='<%#Eval("SoldPrice")%>'
                                            runat="server"></asp:TextBox>
                                         <asp:CustomValidator ID="cvSoldCheck3000"  SetFocusOnError="true"  Display="Dynamic" runat="server" ControlToValidate="txtPriceSold"
                                            ErrorMessage="The difference between car cost and price sold should not be > 3000 or < -3000" ClientValidationFunction="CheckAmountDifference"></asp:CustomValidator>
                                        <asp:CustomValidator ID="cvPriceSold" runat="server" ControlToValidate="txtPriceSold"
                                            ErrorMessage="Invalid Amount" ClientValidationFunction="validateAmount"></asp:CustomValidator>
                                        <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtPriceSold">
                                        </ajax:FilteredTextBoxExtender>
                                    </td>
                                    <td class="TableBorderB">
                                        Mileage Out
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtMileageOut" MaxLength="6" CssClass="txtMan2" Text='<%#Eval("MileageOut")%>'
                                            runat="server"></asp:TextBox>
                                        <ajax:FilteredTextBoxExtender ID="txtMileageOut_Extender" runat="server" FilterType="Numbers"
                                            InvalidChars="." TargetControlID="txtMileageOut">
                                        </ajax:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Deposit Date
                                    </td>
                                    <td class="TableBorder" nowrap="nowrap">
                                        <asp:TextBox ID="txtDepositDate" CssClass="txtMan2" Text='<%#Eval("DepositDate")%>'
                                            runat="server"></asp:TextBox>
                                        <ajax:CalendarExtender ID="calDepositDate" runat="server" PopupButtonID="imgDepositDate"
                                            TargetControlID="txtDepositDate">
                                        </ajax:CalendarExtender>
                                        <asp:Image ID="imgDepositDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                            Style="cursor: pointer;" />
                                    </td>
                                    <td class="TableBorderB">
                                        Deposit Amount
                                    </td>
                                    <td class="TableBorder">
                                        <asp:TextBox ID="txtDepositAmount" MaxLength="14" CssClass="txtMan2" Text='<%#Eval("DepositAmount")%>'
                                            runat="server"></asp:TextBox>
                                        <asp:CustomValidator ID="cvDepositCheck3000" SetFocusOnError="true" runat="server" ControlToValidate="txtDepositAmount"
                                            Display="Dynamic" ErrorMessage="The difference between car cost and Deposit amount should not be > 3000 or < -3000" ClientValidationFunction="CheckAmountDifference"></asp:CustomValidator>
                                        
                                        <asp:CustomValidator ID="cvAmount" runat="server" ControlToValidate="txtDepositAmount"
                                            ErrorMessage="Invalid Amount" ClientValidationFunction="validateAmount"></asp:CustomValidator>
                                        <ajax:FilteredTextBoxExtender ID="txtCost_FilteredTextBoxExtender" runat="server"
                                            FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtDepositAmount">
                                        </ajax:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Deposit Account
                                    </td>
                                    <td class="TableBorder" colspan="3">
                                        <asp:DropDownList ID="ddlBankName" runat="server" AppendDataBoundItems="true" DataTextField="AccountNoWithBankInfo"
                                            DataValueField="BankAccountId" Width="250px" SelectedValue='<%#Eval("BankAccountId")%>'
                                            DataSourceID="objBankList" CssClass="txtMan2">
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:ObjectDataSource ID="objBankList" runat="server" SelectMethod="GetBankAccounts"
                                            TypeName="METAOPTION.BAL.Common"></asp:ObjectDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Deposit Comments
                                    </td>
                                    <td class="TableBorder" colspan="3">
                                        <asp:TextBox ID="txtDepositComment" CssClass="txtMulti" Rows="5" Width="100%" TextMode="MultiLine"
                                            Text='<%#Eval("DepositComment")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00$ContentPlaceHolder1$frmEditSoldTo$txtcount3', 500)" />
                                        <asp:TextBox ID="txtCount3" runat="server" ReadOnly="true" CssClass="WordCounter"
                                            Width="30px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Sold
                                    </td>
                                    <td class="TableBorder" colspan="3">
                                        <asp:DropDownList ID="ddlSoldStatus" runat="server" CssClass="txt1">
                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Selected="True" Value="False"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableBorderB">
                                        Sold Comments
                                    </td>
                                    <td class="TableBorder" colspan="3">
                                        <%-- <asp:UpdatePanel ID="updPnlSoldComment" UpdateMode="Conditional" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlSoldStatus" />
                                            </Triggers>
                                            <ContentTemplate>--%>
                                        <asp:TextBox ID="txtSoldComment" CssClass="txtMulti" Rows="5" Width="100%" TextMode="MultiLine"
                                            Text='<%#Eval("SoldComment")%>' runat="server" onkeyup="CheckMaxCharLimit(this,'ctl00$ContentPlaceHolder1$frmEditSoldTo$txtcount4', 500)" />
                                        <asp:TextBox ID="txtcount4" runat="server" ReadOnly="true" CssClass="WordCounter"
                                            Width="30px" />
                                        <%-- </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:FormView>
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnEditPopUpSoldToCancel" runat="server" Text="Cancel" CssClass="Btn_Form"
                                    Width="75px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnEditPopUpSoldToDetails" Text="Save" runat="server" Width="75px"
                                    CssClass="Btn_Form" OnClick="btnEditPopUpSoldToDetails_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDealerSearch" CssClass="modalPopup" runat="server" Width="620px">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="PopUpBoxHeading">
                    &nbsp;&nbsp; SEARCH CUSTOMERS/DEALERS
                </td>
                <td class="PopUpBoxHeading" align="right">
                    <img border="0" src="../Images/close.gif" onclick="$find('mdPopUpSelectDealer').hide();return false;"
                        alt="close" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlDealerSearch1" runat="server" ScrollBars="Both" Width="600px" Height="450px">
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr>
                                <td align="left">
                                    <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
                                        <tr>
                                            <td class="TableBorderB">
                                                <b>Name</b>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:TextBox ID="txtDealerToSearch" CssClass="txtMan2" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="TableBorderB">
                                                <b>City</b>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:TextBox ID="txtDealerCity" CssClass="txtMan2" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                <b>State</b>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:DropDownList ID="ddlDealerState" runat="server" DataTextField="State" DataValueField="StateId"
                                                    DataSourceID="objStates" AppendDataBoundItems="True" CssClass="txtMan2">
                                                    <asp:ListItem Text="" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="objStates" runat="server" SelectMethod="GetStates" TypeName="METAOPTION.BAL.Common">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="1" Name="CountryId" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td class="TableBorderB">
                                                <b>Zip</b>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:TextBox ID="txtDealerZip" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnDealerPopUpCancel" runat="server" OnClientClick="$find('mdpop').show();return false;"
                                        CssClass="Btn_Form" Text="Cancel" Width="77px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSearchDealers" runat="server" CssClass="Btn_Form" Text="Search"
                                        Width="77px" OnClick="btnSearchDealers_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvDealerDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        CssClass="gridView" DataKeyNames="DealerId" GridLines="None" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnSelect" runat="server" CommandArgument='<%#Eval("DealerId") %>'
                                                        CommandName="SelectDealer" ImageUrl="~/Images/confirm.gif" OnClick="imgbtnSelect_Click"
                                                        Style="height: 16px" />
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DealerId" HeaderStyle-CssClass="GridHeader" HeaderText="DealerId"
                                                ItemStyle-CssClass="GridContent" Visible="false">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DealerName" HeaderStyle-CssClass="GridHeader" HeaderText="Name"
                                                ItemStyle-CssClass="GridContent">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="City" HeaderStyle-CssClass="GridHeader" HeaderText="City"
                                                ItemStyle-CssClass="GridContent">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Zip" HeaderStyle-CssClass="GridHeader" HeaderText="Zip"
                                                ItemStyle-CssClass="GridContent">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DealerDIN" HeaderStyle-CssClass="GridHeader" HeaderText="DealerDIN"
                                                ItemStyle-CssClass="GridContent">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Category" HeaderStyle-CssClass="GridHeader" HeaderText="Category"
                                                ItemStyle-CssClass="GridContent">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DealerType" HeaderStyle-CssClass="GridHeader" HeaderText="Type"
                                                ItemStyle-CssClass="GridContent">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="AccountingCode" HeaderStyle-CssClass="GridHeader" HeaderText="AccountingCode"
                                                ItemStyle-CssClass="GridContent">
                                                <HeaderStyle CssClass="GridHeader" />
                                                <ItemStyle CssClass="GridContent" />
                                            </asp:BoundField>--%>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;"
                                                width="100%">
                                                <tr>
                                                    <td align="center" class="TableBorderB" style="width: 100%">
                                                        No Rows found
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdSelDealerId" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlCustomerSearch" CssClass="modalPopup" Style="display: none;" Width="620px"
        runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="PopUpBoxHeading">
                    &nbsp;&nbsp; SEARCH CUSTOMERS/DEALERS
                </td>
                <td class="PopUpBoxHeading" align="right">
                    <img border="0" src="../Images/close.gif" onclick="$find('mdPopupSelectCustomer').hide();return false;"
                        alt="close" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlCustomerSearch1" runat="server" ScrollBars="Vertical" Width="600px"
                        Height="450px">
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr>
                                <td align="left">
                                    <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
                                        <tr>
                                            <td class="TableBorderB">
                                                <b>Name</b>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:TextBox ID="txtCustName" CssClass="txtMan2" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="TableBorderB">
                                                <b>City</b>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:TextBox ID="txtCustCity" CssClass="txtMan2" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="TableBorderB">
                                                <b>State</b>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:DropDownList ID="ddlCustState" runat="server" DataTextField="State" DataValueField="StateId"
                                                    DataSourceID="objStates" AppendDataBoundItems="True" CssClass="txtMan2">
                                                    <asp:ListItem Text="" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetStates"
                                                    TypeName="METAOPTION.BAL.Common">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="1" Name="CountryId" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td class="TableBorderB">
                                                <b>Zip</b>
                                            </td>
                                            <td class="TableBorder">
                                                <asp:TextBox ID="txtCustZip" runat="server" CssClass="txtMan2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="height30" colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:Button ID="btnEditSearchCust" runat="server" CssClass="Btn_Form" Text="Search"
                                                    Width="77px" CommandName="SearchCustomer" OnClick="btnEditSearchCust_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnEditCancelCust" runat="server" OnClientClick="$find('mdpopupSoldTo').show();return false;"
                                                    CssClass="Btn_Form" Text="Cancel" Width="77px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:GridView ID="gvCustomerSearch" CssClass="gridView" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CellPadding="4" GridLines="None" DataKeyNames="DealerId">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnSelect" CommandName="SelectCustomer" CommandArgument='<%#Eval("DealerId") %>'
                                                                    runat="server" ImageUrl="~/Images/confirm.gif" OnClick="imgbtnSelect_Clck" Style="height: 16px" />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DealerId" HeaderText="DealerId" Visible="false" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DealerName" HeaderText="Name" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Zip" HeaderText="Zip" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DealerDIN" HeaderText="DealerDIN" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Category" HeaderText="Category" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DealerType" HeaderText="Type" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:BoundField>
                                                        <%--<asp:BoundField DataField="AccountingCode" HeaderText="AccountingCode" HeaderStyle-CssClass="GridHeader"
                                                            ItemStyle-CssClass="GridContent">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <ItemStyle CssClass="GridContent" />
                                                        </asp:BoundField>--%>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table border="0" width="100%" cellspacing="0" cellpadding="0" style="border-collapse: collapse;">
                                                            <tr>
                                                                <td align="center" class="TableBorderB" style="width: 100%">
                                                                    No Rows found
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%--<tr>
                                                                <td class="height30">
                                                                    <ajax:ModalPopupExtender ID="MPEDealerSearch" runat="server" BackgroundCssClass="modalBackground"
                                                                        TargetControlID="lnkSelectDealer" PopupControlID="pnlDealerSearch" DropShadow="true"
                                                                        CancelControlID="btnDealerPopUpCancel">
                                                                    </ajax:ModalPopupExtender>
                                                                </td>
                                                            </tr>--%>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajax:ModalPopupExtender ID="MPECarProperties" BehaviorID="mdpopCarProp" runat="server"
        TargetControlID="imgbtnEditCarProp" PopupControlID="pnlPopEditCarProp" BackgroundCssClass="modalBackground"
        CancelControlID="btnCarPropCancel" />
    <ajax:ModalPopupExtender ID="MPEDealerDetails" BehaviorID="mdpop" runat="server"
        TargetControlID="btnEditDealerDetails" PopupControlID="pnlEditDealerDetails"
        BackgroundCssClass="modalBackground" DropShadow="true" CancelControlID="btnEditDealerCancel" />
    <ajax:ModalPopupExtender ID="MPESoldTo" runat="server" BehaviorID="mdpopupSoldTo"
        TargetControlID="imgbtnEditSoldToDetails" PopupControlID="pnlPopEditSoldTo" BackgroundCssClass="modalBackground"
        DropShadow="true" CancelControlID="btnEditPopUpSoldToCancel" />
    <table id="tblSoldSectionPopup" class="modalPopup" border="0" cellpadding="0" cellspacing="0"
        runat="server">
        <tr>
            <td class="PopUpBoxHeading" nowrap="nowrap">
                &nbsp;&nbsp;SOLD STATUS
            </td>
            <td class="PopUpBoxHeading" align="right">
                <img border="0" src="../Images/close.gif" onclick="$find('mdpopupSoldUpdate').hide();return false;"
                    alt="close" />
            </td>
        </tr>
        <tr>
            <td class="TableBorderB">
                Sold
            </td>
            <td class="TableBorder">
                <asp:DropDownList ID="ddlPopSoldStatus" runat="server" CssClass="txt1">
                    <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                    <asp:ListItem Text="No" Value="False"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="TableBorderB">
                Sold Comments
            </td>
            <td class="TableBorder">
                <asp:TextBox ID="txtPopSoldComment" CssClass="txtMulti" Rows="5" TextMode="MultiLine"
                    runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnPopSoldSectionCancel" runat="server" CssClass="Btn_Form" Text="Cancel"
                    Width="77px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server" CssClass="Btn_Form" Text="Save" Width="77px"
                    OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <%-- <table id="tblInvMessagePopup" class="modalPopup" border="0" cellpadding="0" cellspacing="0"
        runat="server">
        <tr>
            <td class="PopUpBoxHeading" nowrap="nowrap">
                &nbsp;Change Car Status!!!
            </td>
            <td class="PopUpBoxHeading" nowrap="nowrap" align="right">
                <img border="0" src="../Images/close.gif" onclick="$find('mdpopupInvMessage').hide();return false;"
                    alt="close" />
            </td>
        </tr>
        <tr>
            <td colspan="2" height="15px">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;<asp:Label ID="lblInvMessage" Text="" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="15px">
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnInvYes" runat="server" CssClass="Btn_Form" Text="Yes" Width="77px"
                    OnClick="btnInvYes_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnInvNo" runat="server" CssClass="Btn_Form" Text="No" Width="77px"
                    OnClick="btnInvNo_Click" />
            </td>
        </tr>
    </table>--%>
    <div style="display: none;">
        <asp:Button ID="btnSoldPopupOpener" runat="server" />
        <%-- <asp:Button ID="btnInvMessage" runat="server" />
        <asp:Button ID="btnInvCancel" runat="server" />--%>
    </div>
    <ajax:ModalPopupExtender ID="MPESoldUpdate" runat="server" BehaviorID="mdpopupSoldUpdate"
        TargetControlID="btnSoldPopupOpener" PopupControlID="tblSoldSectionPopup" BackgroundCssClass="modalBackground"
        DropShadow="true" CancelControlID="btnPopSoldSectionCancel" />
    <%--<ajax:ModalPopupExtender ID="MPEInvMessage" runat="server" BehaviorID="mdpopupInvMessage"
        TargetControlID="btnInvMessage" PopupControlID="tblInvMessagePopup" BackgroundCssClass="modalBackground"
        DropShadow="true" CancelControlID="btnInvCancel" />--%>
    <asp:HiddenField ID="hdInventoryId" runat="server" />
    <asp:HiddenField ID="hdDealerName" runat="server" />
</asp:Content>
