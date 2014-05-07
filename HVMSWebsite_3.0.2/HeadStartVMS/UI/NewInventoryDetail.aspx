<%@ Page Title="HeadStartVMS::Inventory Detail" Language="C#" MasterPageFile="~/UI/MasterPageNoLeftPanel.Master"  
AutoEventWireup="true" CodeBehind="NewInventoryDetail.aspx.cs" Inherits="METAOPTION.UI.NewInventoryDetail" %>

<%@ MasterType VirtualPath="~/UI/MasterPageNoLeftPanel.Master" %>
<%@ Register Src="../UserControls/ManageInventoryStats.ascx" TagName="ManageInventoryStats" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    


    <div style="width: 100%; position: relative; float: left">
        <div style="vertical-align: top; padding: 6px; background-color: #E4EDF4; width: 24%;
            top: 0; bottom: 0; height: auto; float: left;">
            <%--display: inline-block;position: absolute;--%>
            <uc1:ManageInventoryStats ID="ManageInventoryStats1" runat="server" />
        </div>
        <div style="display: block; width: 74%; margin-left: 26%;">
            <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hfInventoryID" runat="server" Value="0" />
                    <asp:HiddenField ID="hfcryear" runat="server" />
                    <asp:HiddenField ID="hfcrmake" runat="server" />
                    <asp:HiddenField ID="hfcrmodel" runat="server" />
                    <asp:HiddenField ID="hfcrbody" runat="server" />
                    <asp:HiddenField ID="hfcrprice" runat="server" />
                    <asp:HiddenField ID="hfcrmileage" runat="server" />
                    <asp:HiddenField ID="hfcrextcol" runat="server" />
                    <asp:HiddenField ID="hfcrintcol" runat="server" />
                    <asp:HiddenField ID="hfCRID" runat="server" Value="0" />
                    <table border="0" width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                        <tr>
                            <td nowrap="nowrap" style="padding-bottom: 7px">
                                <asp:Button ID="btnMoveToInv" class="Btn_Form" Text="Move to Inventory" runat="server"
                                    OnClick="btnMoveToInv_Click" Width="125px" />
                                <asp:Button ID="btnMoveToArch" class="Btn_Form" Text="Move to Archive" runat="server"
                                    OnClick="btnMoveToArch_Click" Width="125px" />
                                <asp:Button ID="btnMoveToOnHand" class="Btn_Form" Text="Move to On-Hand" runat="server"
                                    OnClick="btnMoveToOnHand_Click" Width="125px" />
                                <asp:Button ID="btnBillofSale" class="Btn_Form" Text="Bill of Sale" runat="server"
                                    OnClick="btnBillofSale_Click" Width="125px" />
                                <asp:Button ID="btnBillofSale_PDF" class="Btn_Form" Text="Bill of Sale(PDF)" runat="server"
                                    OnClick="btnBillofSale_PDF_Click" Width="125px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="Btn_Form" Style="padding: 3px 4px;
                                    text-decoration: none" Width="60px" Text="<< Back" NavigateUrl="~/UI/InventorySearch.aspx" />
                                <asp:Button ID="btnDeleteInventory" class="Btn_Form" Text="Delete Inventory" Visible="false"
                                    runat="server" OnClick="btnDeleteInventory_Click" OnClientClick="javascript:return confirm('Are you sure you want to delete this Inventory? You will not be able to access this Inventory once deleted\n\nClick Ok to Confirm\nClick Cancel to ignore');"
                                    Width="125px" />
                                <asp:Button ID="btnBackLaneAssignment" class="Btn_Form" Text="<< Lane" Visible="false"
                                    runat="server" OnClick="btnBackLaneAssignment_Click" Width="125px" />
                                <asp:Button ID="btnCreateUcr" runat="server" class="Btn_Form" Text="Create/Link UCR"
                                    OnClick="btnCreateUcr_Click" Width="125px" />
                                <asp:Button ID="btnChangeUcr" runat="server" class="Btn_Form" Text="Change UCR" OnClick="btnChangeUcr_Click"
                                    Width="125px" />
                                <asp:Button ID="btnViewUCR" runat="server" class="Btn_Form" Text="View UCR" OnClick="btnViewUCR_Click"
                                    Width="125px" />
                            </td>
                        </tr>
                    </table>
                    <table id="" border="0" width="100%" cellpadding="0" style="border-collapse: collapse;
                        margin-top: 10px;">
                        <tr>
                            <td colspan="2">
                                <div id="dvCarDetails" runat="server" class="TableHeadingBg">
                                    <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                        <tr>
                                            <td class="TableHeading">
                                                <asp:Label ID="lblCarStatus" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="TableHeading">
                                                <asp:Label ID="lblUCRStatus" runat="server" Text="" />
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
                                                    Regular #
                                                </td>
                                                <td class="TableBorderB">
                                                    Exotic #
                                                </td>
                                                <td class="TableBorderB">
                                                    Virtual #
                                                </td>
                                                <td class="TableBorderB">
                                                    Online #
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorder">
                                                    <%#Eval("RegularLane")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("ExoticLane")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("VirtualLane")%>
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("OnlineLane")%>
                                                </td>
                                            </tr>
                                            <tr class="TableBorderB">
                                                <td class="TableBorderB">
                                                    VIN
                                                </td>
                                                <td id="Td1" class="TableBorder" runat="server">
                                                    <%#Eval("VIN")%>
                                                    <%--Use this Hidden label to SET VIN PROPERTY to be accessible throughout the page--%>
                                                    <asp:Label ID="lblVIN" Visible="false" runat="server" Text='<%#Eval("VIN")%>'></asp:Label>
                                                    <%--Use this Hidden label to display value in lblCarStatus Upper DIV Header--%>
                                                    <asp:Label ID="lblCarStat" runat="server" Visible="false" Text='<%# CarStatus(Eval("CarStatus"), Eval("isActive"), Eval("DateDeleted"), Eval("DeletedBy")) %>' />
                                                    <asp:Label ID="lblCRStatus" runat="server" Text='<%#Eval("CR_Status") %>' Visible="false" />
                                                </td>
                                                <td class="TableBorderB">
                                                    Car Location
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("CarLocation")%>
                                                </td>
                                            </tr>
                                            <tr class="TableBorderB">
                                                <td class="TableBorderB">
                                                    Year
                                                </td>
                                                <td class="TableBorder">
                                                    <font color="red"><b>
                                                        <%#Eval("Year")%></b></font>
                                                </td>
                                                <td class="TableBorderB">
                                                    Mileage In
                                                </td>
                                                <td class="TableBorder">
                                                    <font color="red"><b>
                                                        <%#Eval("MileageIn")%></b></font>
                                                    <asp:HiddenField ID="hdnSystemText" Value='<% #Eval("Description") %>' runat="server" />
                                                    <asp:HiddenField ID="hdnSystemValue" Value='<% #Eval("SystemID") %>' runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorderB">
                                                    Make
                                                </td>
                                                <td class="TableBorder">
                                                    <font color="red"><b>
                                                        <%#Eval("Make")%></b></font>
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
                                                    Model
                                                </td>
                                                <td class="TableBorder">
                                                    <font color="red"><b>
                                                        <%#Eval("Model")%></b></font>
                                                </td>
                                                <td class="TableBorderB">
                                                    Interior Color
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("IntDesc")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorderB">
                                                    Body
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Body")%>
                                                </td>
                                                <td class="TableBorderB">
                                                    Special Equipment
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("DesignatedEquipment")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorderB">
                                                    Bad CARFAX
                                                </td>
                                                <td class="TableBorder">
                                                    <%# FormatBadCarFax(Eval("BadCarFax")) %>
                                                </td>
                                                <td class="TableBorderB">
                                                    Grade
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("Grade")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorderB">
                                                    Vehicle Present
                                                </td>
                                                <td class="TableBorder">
                                                    <%# Convert.ToBoolean(Eval("VehiclePresent"))== true?"Yes" : "No" %>
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
                                                    Average MMR
                                                </td>
                                                <td class="TableBorder">
                                                    &nbsp;
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
                                                    Wheel Drive
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("WheelDrive")%>
                                                </td>
                                                <td class="TableBorderB">
                                                    Trans
                                                </td>
                                                <td class="TableBorder">
                                                    <%#Eval("TransType")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorderB">
                                                    Note
                                                </td>
                                                <td class="TableBorder" colspan="3">
                                                    <%#Eval("CarNote")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorderB">
                                                    Special Case Note
                                                </td>
                                                <td class="TableBorder" colspan="3">
                                                    <%#Eval("SpecialCaseNote")%>
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
                                                <td class="TableBorderB">
                                                    Bad AUTOCHECK
                                                </td>
                                                <td class="TableBorder">
                                                    <%# FormatBadAutoCheck(Eval("BadAutoCheck"))%>
                                                </td>
                                                <td class="TableBorderB">
                                                    Market Price
                                                </td>
                                                <td class="TableBorder">
                                                    <%#String.Format("{0:c}", Eval("MarketPrice"))%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="FooterContentDetails">
                                                    <asp:Label ID="lblCarHistory" runat="server" Text='<%# Eval("UpdatedHistory") ?? "&nbsp;"%>'> </asp:Label>
                                                    <asp:HiddenField ID="hdnUpdatedSystemHistory" runat="server" Value='<%# Eval("UpdatedSystemHistory") ?? "&nbsp;"%>' />
                                                </td>
                                                <td colspan="2" class="FooterContentDetails">
                                                    <asp:Label ID="lblCarDetailsDateAdded" runat="server" Text='<%# Eval("AddedBy") ?? "&nbsp;"%>'> </asp:Label>
                                                    <asp:HiddenField ID="hdnAddedBy" runat="server" Value='<%# Eval("AddedBy") ?? "&nbsp;"%>' />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="hfVIN2" runat="server" Value='<%# Eval("VIN") %>' />
                                        <asp:HiddenField ID="hfyear" runat="server" Value='<%# Eval("Year") %>' />
                                        <asp:HiddenField ID="hfmake" runat="server" Value='<%# Eval("Make") %>' />
                                        <asp:HiddenField ID="hfmodel" runat="server" Value='<%# Eval("Model") %>' />
                                        <asp:HiddenField ID="hfbody" runat="server" Value='<%# Eval("Body") %>' />
                                        <asp:HiddenField ID="hfprice" runat="server" Value='<%# Eval("CarCost") %>' />
                                        <asp:HiddenField ID="hfmileage" runat="server" Value='<%# Eval("MileageIn") %>' />
                                        <asp:HiddenField ID="hfextcol" runat="server" Value='<%# Eval("Ext1Desc") %>' />
                                        <asp:HiddenField ID="hfintcol" runat="server" Value='<%# Eval("INTDesc") %>' />
                                    </ItemTemplate>
                                </asp:FormView>
                                <asp:Panel ID="pnlPopEditCarDet" runat="server" Style="display: none; padding: 0px;"
                                    CssClass="modalPopup" Width="700px">
                                    <div>
                                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="PopUpBoxHeading">
                                                    &nbsp;&nbsp; EDIT CAR DETAILS
                                                </td>
                                                <td class="PopUpBoxHeading" align="right">
                                                    <img alt="" border="0" src="../Images/close.gif" onclick="$find('mdpopCarUpdate').hide();return false;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div style="padding: 10px; text-align: left">
                                        <asp:FormView ID="frmCarDetailsUpdate" runat="server" Width="100%" OnDataBound="frmCarDetailsUpdate_DataBound">
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
                                                            <font color="red"><b>
                                                                <asp:TextBox ID="txtMileageIn" MaxLength="14" CssClass="txtMan1" Text='<%#Eval("MileageIn")%>'
                                                                    runat="server"></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtMileageIn_Extender" runat="server" FilterType="Numbers"
                                                                    InvalidChars="." TargetControlID="txtMileageIn">
                                                                </ajax:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TableBorderB">
                                                            Year</font>
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
                                                                    <asp:TextBox ID="txtArrivalDate" Text='<%#Eval("ArrivalDate")%>' runat="server" CssClass="txtMan1" />
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
                                                                DataSourceID="objEngines" CssClass="txtMan1">
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
                                                                DataSourceID="objTrans" CssClass="txtMan1">
                                                            </asp:DropDownList>
                                                            <asp:ObjectDataSource ID="objTrans" runat="server" SelectMethod="GetTransList" TypeName="METAOPTION.BAL.Common">
                                                            </asp:ObjectDataSource>
                                                        </td>
                                                        <td class="TableBorderB">
                                                            Wheel Drive
                                                        </td>
                                                        <td class="TableBorder">
                                                            <asp:DropDownList ID="ddlWheelDrive" DataTextField="WheelDrive" DataValueField="WheelDriveId"
                                                                runat="server" DataSourceID="objWheelDrives" CssClass="txtMan1">
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
                                                            <asp:TextBox ID="txtRegLaneNoPopUp" MaxLength="7" CssClass="txt1" Text='<%#Eval("RegularLane")%>'
                                                                runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revRegularLaneNumber" ValidationGroup="lanevalidation"
                                                                Display="Dynamic" ControlToValidate="txtRegLaneNoPopUp" runat="server" ValidationExpression="^\d{2}-\w{4}"
                                                                ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TableBorderB">
                                                            Exotic Lane Number
                                                        </td>
                                                        <td class="TableBorder">
                                                            <asp:TextBox ID="txtExoLaneNumPopUp" MaxLength="7" CssClass="txt1" Text='<%#Eval("ExoticLane")%>'
                                                                runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revExoticLaneNumber" Display="Dynamic" ControlToValidate="txtExoLaneNumPopUp"
                                                                ValidationExpression="^\d{2}-\w{4}" ValidationGroup="lanevalidation" runat="server"
                                                                ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td class="TableBorderB">
                                                            Virtual Lane Number
                                                        </td>
                                                        <td class="TableBorder">
                                                            <asp:TextBox ID="txtVirLaneNoPopUp" CssClass="txt1" Text='<%#Eval("VirtualLane")%>'
                                                                MaxLength="7" runat="server" />
                                                            <asp:RegularExpressionValidator ID="revVirtualLaneNumber" Display="Dynamic" ControlToValidate="txtVirLaneNoPopUp"
                                                                ValidationGroup="lanevalidation" ValidationExpression="^\d{2}-\w{4}" runat="server"
                                                                ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TableBorderB">
                                                            Online Lane Number
                                                        </td>
                                                        <td class="TableBorder">
                                                            <asp:TextBox ID="txtOnlineLanPopUp" MaxLength="7" CssClass="txt1" Text='<%#Eval("OnlineLane")%>'
                                                                runat="server"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revOnlineLaneNumber" Display="Dynamic" ControlToValidate="txtOnlineLanPopUp"
                                                                ValidationGroup="lanevalidation" ValidationExpression="^\d{2}-\w{4}" runat="server"
                                                                ErrorMessage="*" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td class="TableBorderB">
                                                            Grade &nbsp;
                                                        </td>
                                                        <td class="TableBorder">
                                                            <asp:DropDownList ID="ddlGrade" CssClass="txtMan1" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TableBorderB">
                                                            Bad CARFAX
                                                        </td>
                                                        <td class="TableBorder">
                                                            <asp:DropDownList ID="ddlBadCarFax" CssClass="txtMan1" runat="server">
                                                                <asp:ListItem Value="1" Text="Yes" />
                                                                <asp:ListItem Value="0" Text="No" Selected="True" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="TableBorderB" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TableBorderB">
                                                            Note
                                                        </td>
                                                        <td class="TableBorder" colspan="3">
                                                            <asp:TextBox ID="txtNote" CssClass="txtMulti" TextMode="MultiLine" Rows="3" Width="100%"
                                                                Text='<%#Eval("CarNote")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00_ContentPlaceHolder1_frmCarDetailsUpdate_txtcount', 255)" />
                                                            <asp:TextBox ID="txtcount" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                                Width="30px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TableBorderB">
                                                            Special Case Note
                                                        </td>
                                                        <td class="TableBorder" colspan="3">
                                                            <asp:TextBox ID="txtSpecialCaseNote" CssClass="txtMulti" TextMode="MultiLine" Rows="3"
                                                                Width="100%" Text='<%#Eval("SpecialCaseNote")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00_ContentPlaceHolder1_frmCarDetailsUpdate_txtCountSpecialCase', 255)" />
                                                            <asp:TextBox ID="txtCountSpecialCase" runat="server" ReadOnly="true" CssClass="WordCounter"
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
                                    </div>
                                    <div style="padding: 10px; text-align: center">
                                        <asp:Button ID="btnEditPopCarDetCancel" runat="server" CausesValidation="false" CssClass="Btn_Form"
                                            Width="75px" Text="Cancel"  />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnEditPopCarDet" Text="Save" ValidationGroup="lanevalidation" CssClass="Btn_Form"
                                            Width="75px" runat="server" OnClick="btnEditPopCarDet_Click" OnClientClick="javascript:$find('mdpopCarUpdate').hide();return true;" />
                                    </div>
                                </asp:Panel>
                                <ajax:ModalPopupExtender ID="MPECarDetailsUpdate" BehaviorID="mdpopCarUpdate" runat="server"
                                    TargetControlID="btnEditCarDetails" PopupControlID="pnlPopEditCarDet" CancelControlID="btnEditPopCarDetCancel"
                                    BackgroundCssClass="modalBackground" DropShadow="true" />
                            </td>
                        </tr>
                    </table>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse;
                        margin-top: 10px">
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
                    <div id="dvDealerDet" runat="server" class="TableHeadingBg" style="margin-top: 10px">
                        <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                            <tr>
                                <td class="TableHeading">
                                    PURCHASED FROM
                                </td>
                                <td align="right" class="HeadingEditButton">
                                    <asp:ImageButton ID="btnEditDealerDetails" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:FormView ID="frmDealerDetails" Width="100%" runat="server">
                            <ItemTemplate>
                                <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                                    <tr>
                                        <td class="TableBorderB">
                                            Cost
                                        </td>
                                        <td class="TableBorder">
                                            <%# String.Format("{0:c}", Eval("CarCost"))%>
                                        </td>
                                        <td class="TableBorderB">
                                            Dealer Designation
                                        </td>
                                        <td class="TableBorder">
                                            <%#Eval("Designation")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Purchase Date
                                        </td>
                                        <td class="TableBorder">
                                            <%#Eval("PurchaseDate")%>
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
                                            Title Present Note
                                        </td>
                                        <td colspan="3" class="TableBorder">
                                            <%#Eval("TitlePresentNotes")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Dealer Name & Address
                                        </td>
                                        <td class="TableBorder" colspan="3">
                                            <font color="red"><b>
                                                <%#Eval("DealerName")%></b></font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Buyer
                                        </td>
                                        <td class="TableBorder">
                                            <%#Eval("BuyerName")%>
                                        </td>
                                        <td class="TableBorderB">
                                            Expense
                                        </td>
                                        <td class="TableBorder">
                                            <%#String.Format("{0:c}", Eval("InventoryExpenseExcludingCarCost"))%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Title Shipped
                                        </td>
                                        <td class="TableBorder">
                                            <%#Eval("TitleShipped")%>
                                        </td>
                                        <td class="TableBorderB">
                                            Title Shipped Note
                                        </td>
                                        <td class="TableBorder" colspan="3">
                                            <%#Eval("TitleShippedNotes")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            <%--Title Copy Received--%>
                                            Dup Title
                                        </td>
                                        <td class="TableBorder">
                                            <%--<%#Eval("TitleCopyReceived")%>--%>
                                            <%#Eval("DupTitle")%>
                                            <asp:HiddenField ID="hfDupTitleNote" runat="server" Value='<%#Eval("DupTitleNote") %>' />
                                            <asp:Label ID="lblDupTitleNote" runat="server" />
                                        </td>
                                        <td class="TableBorderB">
                                            Vehicle History Report
                                        </td>
                                        <td class="TableBorder">
                                            <%#Eval("VehicleHistoryReport")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Title Tracking Note
                                        </td>
                                        <td colspan="3" class="TableBorder">
                                            <%#Eval("TitleTrackingNote")%>
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
                    </div>
                    <div id="dvSoldTo" runat="server" class="TableHeadingBg" style="margin-top: 10px">
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
                    <div>
                        <asp:FormView ID="frmSoldTo" Width="100%" runat="server">
                            <ItemTemplate>
                                <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                                    <tr>
                                        <td class="TableBorderB">
                                            Sold To Dealer
                                        </td>
                                        <td class="TableBorder" colspan="3">
                                            <font color="red"><b>
                                                <%#Eval("DealerName")%></b></font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Price Sold
                                        </td>
                                        <td class="TableBorder">
                                            <%# String.Format("{0:c}", Eval("SoldPrice"))%>
                                        </td>
                                        <td class="TableBorderB">
                                            Deposit Amount
                                        </td>
                                        <td class="TableBorder">
                                            <%# String.Format("{0:c}", Eval("DepositAmount"))%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TableBorderB">
                                            Mileage Out
                                        </td>
                                        <td class="TableBorder">
                                            <%#String.Format("{0:#,#}", Eval("MileageOut"))%>
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
                                            Date Sold
                                        </td>
                                        <td class="TableBorder">
                                            <%#Eval("SoldDate")%>
                                        </td>
                                        <td class="TableBorderB">
                                            Deposit Bank
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
                                            Actual Cost
                                        </td>
                                        <td class="TableBorder" colspan="3">
                                            <%# String.Format("{0:c}", Eval("ActualCost"))%>
                                        </td>
                                        <!---
                                        <td class="TableBorderB">
                                            Market Price
                                        </td>
                                        <td class="TableBorder">
                                            <%# String.Format("{0:c}", Eval("MarketPrice"))%>
                                        </td>
                                        --->
                                    </tr>
                                    <tr>
                                        <!--commented by Naushad on 12/27/2011, Bob Hollenshead asked to hide this--->
                                        <!---<td class="TableBorderB">
                                            Margin On Market Price
                                        </td>
                                        <td class="TableBorder" nowrap="nowrap">
                                            <%# String.Format("{0:c}", Eval("MarginMarketPrice"))%>&nbsp;
                                            <%# String.Format("{0:p}", Eval("MarginMarketPricePercent"))%>
                                        </td>--->
                                        <td class="TableBorderB">
                                            Margin On Sold Price
                                        </td>
                                        <td class="TableBorder" nowrap="nowrap" colspan="3">
                                            <%# String.Format("{0:c}", Eval("MarginSoldPrice"))%>&nbsp;&nbsp;&nbsp;
                                            <%# String.Format("{0:p}", Eval("MarginSoldPricePercent"))%>
                                        </td>
                                    </tr>
                                    <tr>
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
                        <table id="pnlComeBackDetails" runat="server" style="display: none; padding: 0px;"
                            class="modalPopup" width="656px" border="0" cellpadding="0" cellspacing="0">
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
                                                <asp:TextBox ID="txtComeBackDealer" Width="450" class="FormItem" runat="server" onblur="GetCustomerId(this.id)"
                                                    CssClass="txtMan2" ToolTip="Type at least two characters to find customer name started with i.e MA or %MA to find all customer names having characters entered"></asp:TextBox>
                                                <ajax:AutoCompleteExtender ID="txtComeBackDealer_AutoCompleteExtender" runat="server"
                                                    TargetControlID="txtComeBackDealer" ServicePath="../WS/AutoFillCustomers.asmx"
                                                    ServiceMethod="AutoFillCustomers" EnableCaching="true" MinimumPrefixLength="2"
                                                    CompletionSetCount="25" CompletionListCssClass="autocomplete_completionListElement"
                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    DelimiterCharacters=";, :">
                                                </ajax:AutoCompleteExtender>
                                                <asp:LinkButton ID="lnkSelComeBackDealer" Visible="false" Text="Select Dealer" runat="server"
                                                    OnClientClick="$find('mdpopupComeBackStatus').hide();return false;"></asp:LinkButton>
                                                <asp:TextBox ID="txtChargeBackComm" Visible="false" CssClass="txtMan2" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
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
                    </div>
                    <div id="divSystem" runat="server" class="TableHeadingBg" style="margin-top: 10px">
                        <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                            <tr>
                                <td class="TableHeading">
                                    System
                                </td>
                                <td align="right" class="HeadingEditButton">
                                    <asp:ImageButton ID="imgSystem" runat="server" ImageUrl="~/Images/Edit-Main-Icon.gif" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table width="100%" style="border-collapse: collapse">
                            <tr>
                                <td class="TableBorderB">
                                    Current System
                                </td>
                                <td class="TableBorder">
                                    <asp:Label ID="lblSystem" Text="" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="FooterContentDetails">
                                    <asp:Label ID="lblSystemHistory" runat="server" Text=""></asp:Label>
                                </td>
                                <td colspan="2" class="FooterContentDetails">
                                    <asp:Label ID="lblSystemDateAdded" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlSystem" runat="server" Style="display: none; padding: 0px" CssClass="modalPopup"
                            Width="400px">
                            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="PopUpBoxHeading">
                                        &nbsp;&nbsp; EDIT System
                                    </td>
                                    <td class="PopUpBoxHeading" align="right">
                                        <img alt="" border="0" src="../Images/close.gif" onclick="$find('mdpopSystemUpdate').hide();return false;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" style="padding: 10px">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="TableBorderB">
                                                    Current System
                                                </td>
                                                <td class="TableBorder">
                                                    <asp:Label ID="lblSystem2" Text="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TableBorderB">
                                                    Change System
                                                </td>
                                                <td class="TableBorder">
                                                    <asp:DropDownList ID="ddlSystem" runat="server" Width="220" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:ObjectDataSource ID="odsSystem" runat="server" SelectMethod="FetchSystems" TypeName="METAOPTION.BAL.InventoryBAL">
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
                                    <td colspan="2" align="right">
                                        <asp:Button ID="btnSystemCancel" runat="server" CausesValidation="false" CssClass="Btn_Form"
                                            Width="75px" Text="Cancel" />
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button2" Text="Save" ValidationGroup="lanevalidation" CssClass="Btn_Form"
                                            Width="75px" runat="server" OnClick="btnSystemUpdate_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <ajax:ModalPopupExtender ID="mdpopSystem" BehaviorID="mdpopSystemUpdate" runat="server"
                            TargetControlID="imgSystem" PopupControlID="pnlSystem" CancelControlID="btnSystemCancel"
                            BackgroundCssClass="modalBackground" DropShadow="true" />
                    </div>
                    <div id="dvLinkedCars" runat="server" class="TableHeadingBg" style="margin-top: 10px">
                        <table border="0" cellpadding="0" width="100%" style="border-collapse: collapse">
                            <tr>
                                <td class="TableHeading" colspan="2" valign="middle">
                                    Linked Cars
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                        <tr>
                            <td colspan="2">
                                <asp:GridView runat="server" Width="100%" ID="gvLinkedFilter" AllowPaging="True"
                                    PageSize="5" AutoGenerateColumns="False" EmptyDataText="No Rows found" OnPageIndexChanging="gvLinkedFilter_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="DateAdded" HeaderText="Date Added" DataFormatString="{0:MM/dd/yyyy}"
                                            HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent" />
                                        <asp:TemplateField HeaderText="Linked Cars" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnk1" runat="server" Text='<%#Eval("VIN")%>' NavigateUrl='<%#"~/UI/InventoryDetail.aspx?Code="+Eval("InventoryId")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DealerName" HeaderText="Dealer" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" HeaderStyle-CssClass="GridHeader"
                                            ItemStyle-CssClass="GridContent" />
                                        <asp:TemplateField SortExpression="CheckNumber" HeaderStyle-Width="80px" HeaderStyle-CssClass="GridHeader"
                                            HeaderText="Check No." ItemStyle-CssClass="GridContent">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hylnk" runat="server" Text='<%#Eval("CheckNumber")%>' NavigateUrl='<%#"~/UI/ExpenseAgainstPayment.aspx?PaymentId="+Eval("PaymentId")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Amount" HeaderText="Car Cost" DataFormatString="{0:C}"
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
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; vertical-align: bottom;
                        font-weight: bold; text-align: center; margin-top: 10px;">
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
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; padding: 10px 10px 10px 0px; font-size: 12px; font-weight: bold">
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" Style="padding: 3px 4px;
                                    text-decoration: none" Text="<< Back To Search" NavigateUrl="~/UI/InventorySearch.aspx" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlPopEditCarProp" Width="550px" runat="server" Style="display: none;
                        padding: 0px" CssClass="modalPopup">
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
                            <tr style="padding: 10px">
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
                    <asp:Panel ID="pnlEditDealerDetails" Width="700px" runat="server" CssClass="modalPopup"
                        Style="padding: 0px; display: none">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; padding: 0px;">
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
                                                    <td align="left" class="TableBorder">
                                                        <asp:DropDownList ID="ddlTitlePresent" AutoPostBack="true" runat="server" CssClass="txt1"
                                                            OnSelectedIndexChanged="ddlTitlePresent_SelectedIndexChanged">
                                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="TableBorderB">
                                                        Dup Title
                                                    </td>
                                                    <td align="left" class="TableBorder">
                                                        <asp:DropDownList ID="ddlDupTitle" runat="server" AutoPostBack="true" CssClass="txt1"
                                                            OnSelectedIndexChanged="ddlDupTitle_SelectedIndexChanged">
                                                            <asp:ListItem Text="Yes" Value="1" />
                                                            <asp:ListItem Text="No" Value="0" />
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorder" colspan="2">
                                                        <b>Title Present Note</b>
                                                        <asp:UpdatePanel ID="updPnlTitlePresentNotes" UpdateMode="Conditional" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlTitlePresent" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtTitlePresentNotes" Width="100%" Rows="3" CssClass="txtMulti"
                                                                    TextMode="MultiLine" Text='<%#Eval("TitlePresentNotes")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00_ContentPlaceHolder1_frmViewEditDealerDetails_txtcount1', 255)" />
                                                                <asp:TextBox ID="txtcount1" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                                    Width="30px" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td class="TableBorder" colspan="2">
                                                        <b>Dup Title Note</b>
                                                        <asp:UpdatePanel ID="updDupTitleNote" UpdateMode="Conditional" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlDupTitle" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="txtDupTitleNote" Width="100%" Rows="3" CssClass="txtMulti" TextMode="MultiLine"
                                                                    Text='<%#Eval("DupTitleNote")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00_ContentPlaceHolder1_frmViewEditDealerDetails_txtCountDupTitleNoteChar', 255)" />
                                                                <asp:TextBox ID="txtCountDupTitleNoteChar" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                                    Width="30px" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderB" nowrap="nowrap">
                                                        Title Tracking Note
                                                    </td>
                                                    <td class="TableBorder" colspan="3">
                                                        <asp:TextBox ID="txtTitleTrackingNote" Width="100%" Rows="3" CssClass="txtMulti"
                                                            TextMode="MultiLine" Text='<%#Eval("TitleTrackingNote")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00_ContentPlaceHolder1_frmViewEditDealerDetails_txtcounttracking', 1000)" />
                                                        <asp:TextBox ID="txtcounttracking" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                            Width="30px" />
                                                        <asp:HiddenField ID="hdnNoteID" Value='<%#Eval("NoteID")%>' runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderB">
                                                        Title Shipped
                                                    </td>
                                                    <td align="left" class="TableBorder">
                                                        <asp:DropDownList ID="ddlTitleShipped" AutoPostBack="true" runat="server" CssClass="txt1"
                                                            OnSelectedIndexChanged="ddlTitleShipped_SelectedIndexChanged">
                                                            <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="TableBorderB">
                                                        Vehicle History Report
                                                    </td>
                                                    <td align="left" class="TableBorder">
                                                        <asp:DropDownList ID="ddlVehicleHistoryReport" runat="server" CssClass="txt1" DataSourceID="objVehicleHistoryReport"
                                                            DataTextField="VehicleHistoryReport" SelectedValue='<%#Eval("VehicleHistoryReportId")%>'
                                                            AppendDataBoundItems="true" DataValueField="VehicleHistoryReportID">
                                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:ObjectDataSource ID="objVehicleHistoryReport" runat="server" SelectMethod="GetVehicleHistoryReports"
                                                            TypeName="METAOPTION.BAL.InventoryBAL"></asp:ObjectDataSource>
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
                                                                <asp:TextBox ID="txtTitleShippedNotes" CssClass="txtMulti" Rows="3" Width="100%"
                                                                    TextMode="MultiLine" Text='<%#Eval("TitleShippedNotes")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00_ContentPlaceHolder1_frmViewEditDealerDetails_txtcount2', 255)" />
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
                    <asp:Panel ID="pnlPopEditSoldTo" runat="server" Style="display: none; padding: 0px"
                        CssClass="modalPopup" Width="700px">
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
                                                        <asp:TextBox ID="txtSoldDate" Text='<%#Eval("SoldDate")%>' CssClass="txtMan2" runat="server"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtDepositComment" CssClass="txtMulti" Rows="3" Width="100%" TextMode="MultiLine"
                                                            Text='<%#Eval("DepositComment")%>' runat="server" onkeyup="CheckMaxCharLimit(this, 'ctl00_ContentPlaceHolder1_frmEditSoldTo_txtCount3', 500)" />
                                                        <asp:TextBox ID="txtCount3" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                            Width="30px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderB">
                                                        Sold
                                                    </td>
                                                    <td class="TableBorder" colspan="3">
                                                        <asp:DropDownList ID="ddlSoldStatus" Width="100px" runat="server" CssClass="txt2">
                                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Sold Not Paid" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="TableBorderB">
                                                        Sold Comments
                                                    </td>
                                                    <td class="TableBorder" colspan="3">
                                                        <asp:TextBox ID="txtSoldComment" CssClass="txtMulti" Rows="3" Width="100%" TextMode="MultiLine"
                                                            Text='<%#Eval("SoldComment")%>' runat="server" onkeyup="CheckMaxCharLimit(this,'ctl00_ContentPlaceHolder1_frmEditSoldTo_txtcount4', 500)" />
                                                        <asp:TextBox ID="txtcount4" runat="server" ReadOnly="true" CssClass="WordCounter"
                                                            Width="30px" />
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
                    <asp:Panel ID="pnlDealerSearch" CssClass="modalPopup" runat="server" Width="620px"
                        Style="margin-top: 5px; display: none">
                        <table border="0" cellpadding="0" cellspacing="0" width="100% ">
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
                        runat="server" style="display: none;">
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
                                <asp:DropDownList ID="ddlPopSoldStatus" runat="server" Width="100px" CssClass="txt2">
                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Sold Not Paid" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
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
                    <div style="display: none;">
                        <asp:Button ID="btnSoldPopupOpener" runat="server" />
                        <%-- <asp:Button ID="btnInvMessage" runat="server" />
                        <asp:Button ID="btnInvCancel" runat="server" />--%>
                    </div>
                    <ajax:ModalPopupExtender ID="MPESoldUpdate" runat="server" BehaviorID="mdpopupSoldUpdate"
                        TargetControlID="btnSoldPopupOpener" PopupControlID="tblSoldSectionPopup" BackgroundCssClass="modalBackground"
                        DropShadow="true" CancelControlID="btnPopSoldSectionCancel" />
                    <asp:HiddenField ID="hdInventoryId" runat="server" />
                    <asp:HiddenField ID="hdDealerName" runat="server" />
                    <%--------------------UCR BEGIN--------------------%>
                    <div id="dvUCR" class="modalPopup" style="display: none; min-width: 400px; min-height: 200px"
                        runat="server">
                        <div>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="PopUpBoxHeading">
                                        &nbsp;&nbsp;Add/Link UCR
                                        <asp:Label ID="lblucrheader" runat="server"></asp:Label>
                                    </td>
                                    <td class="PopUpBoxHeading" align="right">
                                        <img border="0" src="../Images/close.gif" id="imgCloseCR" alt="close" />
                                    </td>
                                </tr>
                            </table>
                            <div style="padding: 10px 0px; margin: 0px 10px" class="LeftPanelContentHeading">
                                What do you want to do?</div>
                            <div style="padding: 0px 5px; margin: 0px10px">
                                <asp:RadioButtonList ID="rbtnListCR" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="rbtnListCR_SelectedIndexChanged"
                                    CellSpacing="5">
                                    <asp:ListItem Text="Create CR" Value="1" class="LeftPanelContentHeading" Selected="True" />
                                    <asp:ListItem Text="Link CR" Value="2" class="LeftPanelContentHeading" />
                                </asp:RadioButtonList>
                            </div>
                            <div id="dvLinkCR" runat="server" style="padding: 0 0 5px 25px; margin: 0 0px 0px 10px;
                                display: none; width: 100%">
                                <div style="float: left;">
                                    <asp:RadioButtonList ID="rbtnCRIdUrl" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="rbtnCRIdUrl_SelectedIndexChanged"
                                        CellSpacing="5">
                                        <asp:ListItem Text="CR ID" Value="1" Selected="True" class="LeftPanelContentHeading" />
                                        <asp:ListItem Text="CR URL" Value="2" class="LeftPanelContentHeading" />
                                        <asp:ListItem Text="Find By VIN" Value="3" class="LeftPanelContentHeading" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div id="dvCRIdUrl" runat="server" style="padding: 5px; margin: 0 0px 0px 42px; display: none;
                                clear: both;">
                                <asp:TextBox ID="txtCRIdUrl" runat="server" CssClass="txt1" />
                            </div>
                            <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                                <asp:Button ID="btncridvalidate" runat="server" Text="Validate" Style="display: none;"
                                    CssClass="btn" OnClick="btncridvalidate_Click" Width="80px" />
                                <asp:Button ID="btncrurlvalidate" runat="server" Text="Validate" CssClass="btn" Style="display: none;"
                                    OnClick="btncrurlvalidate_Click" Width="80px" />
                                <asp:Button ID="btncrsearch" runat="server" Text="Search" CssClass="btn" Style="display: none;"
                                    OnClick="btncrsearch_Click" Width="80px" />
                                <asp:Button ID="btnCRok" runat="server" Text="OK" OnClick="btnCRok_Click" OnClientClick="OpenCreateUCRLink();"
                                    CssClass="btn" Width="80px" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCRcancel" runat="server" Text="Cancel" CssClass="btn" Width="80px"
                                    OnClick="btnCRcancel_Click" />
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hfCR" runat="server" />
                    <asp:HiddenField ID="hfvin" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeCR" runat="server" BackgroundCssClass="modalBackground"
                        BehaviorID="mpecrbeh" TargetControlID="hfCR" PopupControlID="dvUCR" CancelControlID="imgCloseCR">
                    </ajax:ModalPopupExtender>
                    <%--------------------UCR END--------------------%>
                    <%----------------------Change UCR Begin----------------------%>
                    <div id="dvChangeUCR" class="modalPopup" style="display: none; min-width: 400px;
                        min-height: 200px" runat="server">
                        <div>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <td class="PopUpBoxHeading">
                                        &nbsp;&nbsp;Change UCR
                                        <asp:Label ID="lblucrheader2" runat="server"></asp:Label>
                                    </td>
                                    <td class="PopUpBoxHeading" align="right">
                                        <img border="0" src="../Images/close.gif" id="imgCloseCR2" alt="close" />
                                    </td>
                                </tr>
                            </table>
                            <div style="padding: 10px 0px; margin: 0px 10px" class="LeftPanelContentHeading">
                                What do you want to do?</div>
                            <div style="padding: 0px 5px; margin: 0px10px">
                                <asp:RadioButtonList ID="rbtnlistchangecr" runat="server" RepeatDirection="Vertical"
                                    CellSpacing="5">
                                    <asp:ListItem Text="Remove UCR" Value="1" class="LeftPanelContentHeading" Selected="True" />
                                    <asp:ListItem Text="Relink UCR" Value="2" class="LeftPanelContentHeading" />
                                </asp:RadioButtonList>
                            </div>
                            <div id="dvLinkCR2" runat="server" style="padding: 0 0 5px 25px; margin: 0 0px 0px 10px;
                                display: none; width: 100%">
                                <div style="float: left;">
                                    <asp:RadioButtonList ID="rbtnChangeCRIdUrl" runat="server" RepeatDirection="Vertical"
                                        CellSpacing="5">
                                        <asp:ListItem Text="CR ID" Value="1" Selected="True" class="LeftPanelContentHeading" />
                                        <asp:ListItem Text="CR URL" Value="2" class="LeftPanelContentHeading" />
                                        <asp:ListItem Text="Find By VIN" Value="3" class="LeftPanelContentHeading" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div id="dvCRIdUrl2" runat="server" style="padding: 5px; margin: 0 0px 0px 42px;
                                display: none; clear: both;">
                                <asp:TextBox ID="txtCRIdUrl2" runat="server" CssClass="txt1" />
                            </div>
                            <div style="padding: 5px 0px 0px 15; margin: 5px 0px 10px 30px">
                                <asp:Button ID="btncridvalidate2" runat="server" Text="Validate" CssClass="btn" Width="80px"
                                    Style="display: none;" OnClick="btncridvalidate2_Click" />
                                <asp:Button ID="btncrurlvalidate2" runat="server" Text="Validate" CssClass="btn"
                                    Width="80px" Style="display: none;" OnClick="btncrurlvalidate2_Click" />
                                <asp:Button ID="btncrsearch2" runat="server" Text="Search" CssClass="btn" Style="display: none;"
                                    Width="80px" OnClick="btncrsearch2_Click" />
                                <asp:Button ID="btnCRok2" runat="server" Text="OK" OnClick="btnCRok2_Click" CssClass="btn"
                                    Width="80px" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCRcancel2" runat="server" Text="Cancel" CssClass="btn" Width="80px"
                                    OnClick="btnCRcancel_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:HyperLink ID="hlnkViewReport" runat="server" Text="View Report" Target="_blank"
                                    CssClass="btn" Width="100px" />
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hfCR2" runat="server" />
                    <ajax:ModalPopupExtender ID="mpeChangeCR" runat="server" BackgroundCssClass="modalBackground"
                        BehaviorID="mpecrbeh2" TargetControlID="hfCR2" PopupControlID="dvChangeUCR" CancelControlID="imgCloseCR2">
                    </ajax:ModalPopupExtender>
                    <%------------------------Change UCR End------------------------%>
                    <%--Show UCR Response Begin--%>
                    <asp:Panel ID="pnlUCRResponse" CssClass="modalPopup" Style="display: none; height: auto;
                        width: 700px;" runat="server" HorizontalAlign="Left">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="PopUpBoxHeading">
                                    &nbsp;&nbsp;UCR Details
                                </td>
                                <td class="PopUpBoxHeading" align="right">
                                    <img border="0" src="../Images/close.gif" id="imgucrclose" alt="close" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 330px;">
                                    <iframe id="iframeucr" scrolling="no" style="width: 700px; height: 330px;" frameborder="0"
                                        runat="server"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; display: none;">
                                    <asp:Button ID="btnucrok" runat="server" Text="OK" />
                                    <asp:Button ID="btnucrcancel" runat="server" Text="Cancel" OnClick="btnucrcancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:HiddenField ID="hfucr" runat="server" />
                    <ajax:ModalPopupExtender ID="mpucr" runat="server" BackgroundCssClass="modalBackground"
                        TargetControlID="hfucr" PopupControlID="pnlUCRResponse" CancelControlID="imgucrclose"
                        OkControlID="btnucrok">
                    </ajax:ModalPopupExtender>
                    <%--Show UCR Response End--%>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Button2" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>  
    
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
        function GetCustomerId(val) {
            var txtcustomer = document.getElementById(val);

            // var hfId = val.replace("txtCustomerName", "hdCustomerId")
            var hfcontrol = document.getElementById('<%=hdComeBackDealer.ClientID%>');
            var str = (txtcustomer.value).split("ID:");
            if (str.length > 1) {
                txtcustomer.value = str[0];
                hfcontrol.value = str[1];
            }
        }
    </script>
    <script type="text/javascript">
        var BrowserName = $.browser.name;

        function ChangeCSS(rbl) {
            var url = '<%=UCRlinkUrl %>';
            var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
            if (selectedvalue == "1") {
                $('#<%=txtCRIdUrl.ClientID %>').attr('readonly', false);
                document.getElementById('<%=txtCRIdUrl.ClientID %>').style.width = "150px";
                $('#<%=btncridvalidate.ClientID %>').show();
                $('#<%=btncrurlvalidate.ClientID %>').hide();
                $('#<%=btncrsearch.ClientID %>').hide();
                $('#<%=txtCRIdUrl.ClientID %>').val('');
            }
            else if (selectedvalue == "2") {
                $('#<%=txtCRIdUrl.ClientID %>').attr('readonly', false);
                document.getElementById('<%=txtCRIdUrl.ClientID %>').style.width = "300px";
                $('#<%=btncridvalidate.ClientID %>').hide();
                $('#<%=btncrurlvalidate.ClientID %>').show();
                $('#<%=btncrsearch.ClientID %>').hide();
                //$('#<%=txtCRIdUrl.ClientID %>').val('http://web.metaoptionllc.com:82/Report/');
                $('#<%=txtCRIdUrl.ClientID %>').val(url);
            }
            else if (selectedvalue == "3") {
                var vin = $('#<%=hfvin.ClientID %>').val();
                document.getElementById('<%=txtCRIdUrl.ClientID %>').style.width = "150px";
                $('#<%=btncridvalidate.ClientID %>').hide();
                $('#<%=btncrurlvalidate.ClientID %>').hide();
                $('#<%=btncrsearch.ClientID %>').show();
                $('#<%=txtCRIdUrl.ClientID %>').val(vin);
                $('#<%=txtCRIdUrl.ClientID %>').attr('readonly', true);
            }
        }

        function ShowHideLinkCR(rbtn) {

            var selvalue = $("#" + rbtn.id + " input:radio:checked").val();

            if (selvalue == "1") {
                $('#<%=dvLinkCR.ClientID %>').hide();
                $('#<%=dvCRIdUrl.ClientID %>').hide();
                $('#<%=btnCRok.ClientID %>').show();
                $('#<%=btncridvalidate.ClientID %>').hide();
                $('#<%=btncrurlvalidate.ClientID %>').hide();
                $('#<%=btncrsearch.ClientID %>').hide();
            }
            else if (selvalue == "2") {
                $('#<%=dvLinkCR.ClientID %>').show();
                $('#<%=dvCRIdUrl.ClientID %>').show();
                $('#<%=btnCRok.ClientID %>').hide();
                $('#<%=rbtnCRIdUrl.ClientID %>').find("input[value='1']").attr("checked", "checked");
                $('#<%=txtCRIdUrl.ClientID %>').val('');
                $('#<%=btncridvalidate.ClientID %>').show();
                $('#<%=btncrurlvalidate.ClientID %>').hide();
                $('#<%=btncrsearch.ClientID %>').hide();
            }
        }

        function OpenCreateUCRLink() {
            debugger;
            var url = '<%=CreateUCRUrl %>';
            var querystring = "SystemID=2&VIN=" + $('#<%=hfvin.ClientID %>').val();
            querystring += "&SourceInventoryID=" + $('#<%=hfInventoryID.ClientID %>').val();
            querystring += "&Year=" + $('#<%=hfcryear.ClientID %>').val();
            querystring += "&Make=" + $('#<%=hfcrmake.ClientID %>').val();
            querystring += "&Model=" + $('#<%=hfcrmodel.ClientID %>').val();
            querystring += "&Body=" + $('#<%=hfcrbody.ClientID %>').val();
            querystring += "&Mileage=" + $('#<%=hfcrmileage.ClientID %>').val();
            querystring += "&MPrice=" + $('#<%=hfcrprice.ClientID %>').val();
            querystring += "&ExtColor=" + $('#<%=hfcrextcol.ClientID %>').val();
            querystring += "&IntColor=" + $('#<%=hfcrintcol.ClientID %>').val();

            //window.open("http://web.metaoptionllc.com:82/cardetail/newcr?"+querystring);
            window.open(url + querystring);
        }

        function ShowHideChangeCR(rbtn) {
            var selvalue = $("#" + rbtn.id + " input:radio:checked").val();

            if (selvalue == "1") {
                $('#<%=dvLinkCR2.ClientID %>').hide();
                $('#<%=dvCRIdUrl2.ClientID %>').hide();
                $('#<%=btnCRok2.ClientID %>').show();
                $('#<%=btncridvalidate2.ClientID %>').hide();
                $('#<%=btncrurlvalidate2.ClientID %>').hide();
                $('#<%=btncrsearch2.ClientID %>').hide();
            }
            else if (selvalue == "2") {
                $('#<%=dvLinkCR2.ClientID %>').show();
                $('#<%=dvCRIdUrl2.ClientID %>').show();
                $('#<%=btnCRok2.ClientID %>').hide();
                $('#<%=rbtnChangeCRIdUrl.ClientID %>').find("input[value='1']").attr("checked", "checked");
                $('#<%=txtCRIdUrl2.ClientID %>').val('');
                $('#<%=btncridvalidate2.ClientID %>').show();
                $('#<%=btncrurlvalidate2.ClientID %>').hide();
                $('#<%=btncrsearch2.ClientID %>').hide();

            }
        }

        function ChangeCSS2(rbl) {
            var url = '<%=UCRlinkUrl %>';
            var selectedvalue = $("#" + rbl.id + " input:radio:checked").val();
            if (selectedvalue == "1") {
                document.getElementById('<%=txtCRIdUrl2.ClientID %>').style.width = "150px";
                $('#<%=btncridvalidate2.ClientID %>').show();
                $('#<%=btncrurlvalidate2.ClientID %>').hide();
                $('#<%=btncrsearch2.ClientID %>').hide();
                $('#<%=txtCRIdUrl2.ClientID %>').val('');
                $('#<%=txtCRIdUrl2.ClientID %>').attr('readonly', false);
            }
            else if (selectedvalue == "2") {
                document.getElementById('<%=txtCRIdUrl2.ClientID %>').style.width = "300px";
                $('#<%=btncridvalidate2.ClientID %>').hide();
                $('#<%=btncrurlvalidate2.ClientID %>').show();
                $('#<%=btncrsearch2.ClientID %>').hide();
                //$('#<%=txtCRIdUrl.ClientID %>').val('http://web.metaoptionllc.com:82/Report/');
                $('#<%=txtCRIdUrl2.ClientID %>').val(url);
                $('#<%=txtCRIdUrl2.ClientID %>').attr('readonly', false);
            }
            else if (selectedvalue == "3") {
                var vin = $('#<%=hfvin.ClientID %>').val();
                document.getElementById('<%=txtCRIdUrl2.ClientID %>').style.width = "150px";
                $('#<%=btncridvalidate2.ClientID %>').hide();
                $('#<%=btncrurlvalidate2.ClientID %>').hide();
                $('#<%=btncrsearch2.ClientID %>').show();
                $('#<%=txtCRIdUrl2.ClientID %>').val(vin);
                $('#<%=txtCRIdUrl2.ClientID %>').attr('readonly', true);
            }
        }

        if (BrowserName == "firefox") {
            $('#<%=rbtnListCR.ClientID %>').live("change", function () { ShowHideLinkCR(this); });
            $('#<%=rbtnCRIdUrl.ClientID %>').live("change", function () { ChangeCSS(this); });

            $('#<%=rbtnlistchangecr.ClientID %>').live("change", function () { ShowHideChangeCR(this); });
            $('#<%=rbtnChangeCRIdUrl.ClientID %>').live("change", function () { ChangeCSS2(this); });
        }
        else if (BrowserName == "msie") {
            $('#<%=rbtnListCR.ClientID %>').live("change", function () { ShowHideLinkCR(this); });
            $('#<%=rbtnCRIdUrl.ClientID %>').live("change", function () { ChangeCSS(this); });

            $('#<%=rbtnlistchangecr.ClientID %>').live("change", function () { ShowHideChangeCR(this); });
            $('#<%=rbtnChangeCRIdUrl.ClientID %>').live("change", function () { ChangeCSS2(this); });
        }
        else if (BrowserName == "chrome") {
            $('#<%=rbtnListCR.ClientID %>').live("click", function () { ShowHideLinkCR(this); });
            $('#<%=rbtnCRIdUrl.ClientID %>').live("click", function () { ChangeCSS(this); });

            $('#<%=rbtnlistchangecr.ClientID %>').live("click", function () { ShowHideChangeCR(this); });
            $('#<%=rbtnChangeCRIdUrl.ClientID %>').live("click", function () { ChangeCSS2(this); });
        }

        function openBOFwindow(Url) {
            window.open(Url);
        }

        function closeModal() {
            debugger;
            var modal = $('#<%=MPECarDetailsUpdate.ClientID %>');
            console.log(model);
            modal.hide();
        }

        
       function pageLoad() {
        var EntityType = "<%= Session["LoginEntityTypeID"]%>";
            if (EntityType == "2") {             
                $('#ctl00_ContentPlaceHolder1_frmSoldTo_imgOpenSoldStatusPopUp').hide();
               $('#ctl00_ContentPlaceHolder1_frmSoldTo_imgOpenComeBackPopUp').hide();
            }
        }
        window.onload = pageLoad;
     

    </script> 
</asp:Content>
