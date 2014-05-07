<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidateVin.aspx.cs" Inherits="HeadStartVMS.UI.ValidateVin"
    Title="HeadStart VMS::VIN Validate" %>

<%@ MasterType VirtualPath="~/UI/MasterPage.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="updValidateVin" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div>
        <fieldset class="ForFieldSet">
            <legend class="ForLegend">Validate VIN #</legend>
            <table border="0" cellpadding="0" style="border-collapse: collapse"
                width="100%">
                <tr>
                    <td class="TableBorderB">
                        <asp:Label ID="lblVINNO" Text="Enter VIN" runat="server"></asp:Label>
                    </td>
                    <td class="TableBorder" style="width: 80%">
                        <asp:TextBox ID="txtVinNo" CssClass="txt2"  MaxLength="17" runat="server" Width="190px" 
                            ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder" colspan="2" style="padding-left: 100px">
                        <asp:Button ID="btnNext" Text="Next" CssClass="Btn_Form" runat="server" OnClick="btnNext_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnWithoutVin" CssClass="Btn_Form" Text="I don't have VIN" runat="server"
                            OnClick="btnWithoutVin_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="TableBorder" style="width: 100%">
                        Note: If you don't have VIN # press "I don't have VIN" button.
                    </td>
                </tr>
                <tr>
                    <td class="TableBorder" colspan="2" style="padding-top: 10px">
                        <asp:GridView ID="gvVinMatchData" Width="100%" AutoGenerateColumns="false" CssClass="gridView"
                            runat="server" OnRowCreated="gvVinMatchData_RowCreated" OnRowCommand="gvVinMatchData_RowCommand"
                           EmptyDataText="No Rows Found"  OnRowDataBound="gvVinMatchData_RowDataBound" AlternatingRowStyle-CssClass="gvAlternateRow">
                        

                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnSelect" CommandName="SelectVINDetails" runat="server"
                                            ImageUrl="~/Images/confirm.gif" OnClick="imgbtnSelect_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeader" />
                                    <ItemStyle CssClass="GridContent" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Year" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderText="Year" />
                                <asp:BoundField DataField="MakeId" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderText="MakeId" />
                                <asp:BoundField DataField="ModelId" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderText="ModelId" />
                                <asp:BoundField DataField="BodyId" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderText="BodyId" />
                                <asp:BoundField DataField="VinDivisionName" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderText="Make" />
                                <asp:BoundField DataField="VinModelName" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderText="Model" />
                                <asp:BoundField DataField="VinStyleName" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridContent"
                                    HeaderText="Body" />
                       
                            </Columns>
                        
                        </asp:GridView>
                       

                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="divToOpenPopup1" runat="server" />
     <ajax:ModalPopupExtender 
            ID="mpeConfirmVin" 
            runat="server"
            TargetControlID="divToOpenPopup1"
            PopupControlID="dvPopUpMsg"
            BackgroundCssClass="modalBackground"
            CancelControlID ="imgClose1"    
            OkControlID="imgClose1"
            DropShadow="false" />
     </div>
    <div id="dvPopUpMsg" style="width: 220px; height: 150px; background-color:White;">
        <img src="/images/btnClose.png" alt="close" id="imgClose1" style="visibility:hidden;" />
            <div>
                <div style="padding-left:30px; padding-right:30px; padding-top:15px;">Vin does not matches, <br />Do you want to continue, it will cost you!</div>
                    <div style="padding-top:30px;padding-left:30px; padding-right:30px;">
                        <asp:Button ID="btnConfirmYes" runat="server" Text="OK" Width="70px" 
                        onclick="btnConfirmYes_Click" CausesValidation="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;                    
                        <asp:Button ID="btnConfirmNo" runat="server" Text="Cancel" Width="70px" 
                        onclick="btnConfirmNo_Click" CausesValidation="false" />                       
                    </div>
            </div>            
    </div>
       <asp:UpdateProgress ID="uprgActivityStats" runat="server" DisplayAfter="150" AssociatedUpdatePanelID="updValidateVin">
            <ProgressTemplate>
                <div id="dvProg" class="overlay">
                    <img id="image" src="../Images/Wait.gif" alt="Wait..." style="vertical-align: middle" />&nbsp;Please wait...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:content>
