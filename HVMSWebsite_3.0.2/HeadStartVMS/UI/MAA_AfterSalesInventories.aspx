<%@ Page Language="C#"  AutoEventWireup="true"
    CodeBehind="MAA_AfterSalesInventories.aspx.cs" Inherits="METAOPTION.UI.MAA_AfterSalesInventories"
    Title="HeadStartVMS::MAA After-Sales Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
    function GetCustomerId(val)
    {
        var txtcustomer = document.getElementById(val);
        
        var hfId = val.replace("txtCustomerName", "hdCustomerId")
        var hfcontrol = document.getElementById(hfId);
        var str = (txtcustomer.value).split("ID:");
        if(str.length > 1)
        {
            txtcustomer.value = str[0];
            hfcontrol.value = str[1];
        }
    }
    function ChangeRowColor(rowIdx)
    {
        var gv= document.getElementById('tblItem');   
        var selRow = gv.rows[rowIdx];
        if (selRow != null)
        {
            //selected row color will be RESET or restore
            selRow.style.backgroundColor = '#1FE8F8';
        }       
    }
       
       
    function OpenWindow(code) 
        {
          window.open("AfterSalesInventoryHistory.aspx?code=" + code +"&ReturnUrl=AfterSalesInventoryHistory.aspx",'AfterSales','height=600,width=900,scrollbars=1');
        }
  
    
    function GetFooter()
    {
      
       //alert(gv.rows[1]);
      var browser=navigator.appName;
     // alert(browser);
      
      var gv= document.getElementById('ctl00_ContentPlaceHolder1_gvAfterSales');
  
      if(browser=="Netscape")
        {             
            var lRows = gv.rows[1].childNodes[1].childNodes[1].rows.length;
            var selRow = gv.rows[1].childNodes[1].childNodes[1].rows[lRows-1].childNodes[1];     
            selRow.colSpan="18";  
        }
  }   
        
  </script>

    <script language="javascript" type="text/javascript">
        function OpenWindow(code) {
          window.open("AfterSalesInventoryHistory.aspx?code=" + code +"&ReturnUrl=AfterSalesInventoryHistory.aspx",'AfterSales','height=600,width=900,scrollbars=1');
        }
    </script>

    <script type="text/javascript">
        function SetProgressPosition(e) {
            var posx = 0;
            var posy = 0;
            if (!e) var e = window.event;
            if (e.pageX || e.pageY) {
                posx = e.pageX;
                posy = e.pageY;
            }
            else if (e.clientX || e.clientY) {
                posx = e.clientX + document.documentElement.scrollLeft;
                posy = e.clientY + document.documentElement.scrollTop;
            }
            document.getElementById('divProgress').style.left = posx + 20 + "px";
            document.getElementById('divProgress').style.top = posy -20  + "px";
            
        }
    </script>

    <div onmousemove="SetProgressPosition(event)">
        <div class="AddHeading">
            After Sale Management
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-collapse: collapse">
            <tr>
                <td class="TableBorderB" style="width: 12%; text-align: right">
                    Regular Lane # :
                </td>
                <td class="TableBorder" style="width: 8%; text-align: left">
                    <asp:TextBox ID="txtRegLane" CssClass="txtSmall" Text="" runat="server"></asp:TextBox>
                </td>
                <td class="TableBorderB" style="width: 12%; text-align: right">
                    Exotic Lane # :
                </td>
                <td class="TableBorder" style="width: 8%; text-align: left">
                    <asp:TextBox ID="txtExoticLane" CssClass="txtSmall" Text="" runat="server"></asp:TextBox>
                </td>
                <td class="TableBorderB" style="width: 10%; text-align: right">
                    Sold Status :
                </td>
                <td class="TableBorder" style="width: 8%; text-align: left">
                    <asp:DropDownList ID="ddlSoldStatus" runat="server" Width="100px" CssClass="txt2">
                        <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Sold Not Paid" Value="2"></asp:ListItem>
                        <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="TableBorderB" style="width: 10%; text-align: right">
                    Page Size :
                </td>
                <td class="TableBorder" style="width: 12%; text-align: left" nowrap="nowrap">
                    <asp:DropDownList ID="ddlPageSize" CssClass="txtSmall" runat="server">
                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                        <asp:ListItem Text="50" Selected="True" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        <asp:ListItem Text="200" Value="200"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox ID="chkMAA_Records" Text="MAA" Checked="true" runat="server" />
                </td>
                <td class="TableBorder" style="width: 45%; text-align: left; padding-left: 10px;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="Btn_Form" OnClick="btnSearch_Click"
                        Text="Apply" />
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-collapse: collapse;">
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="updPnlAfterSales" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvAfterSales" runat="server" 
                                DataKeyNames="InventoryId" 
                                AutoGenerateColumns="False"
                                GridLines="None"
                                EnableSortingAndPagingCallbacks="true" 
                                CssClass="gridview"
                                CellPadding="3" 
                                PagerStyle-HorizontalAlign="Right"
                                OnRowCancelingEdit="gvAfterSales_RowCancelingEdit" 
                                OnRowEditing="gvAfterSales_RowEditing"
                                OnRowUpdating="gvAfterSales_RowUpdating" 
                                OnPageIndexChanging="gvAfterSales_PageIndexChanging"
                                OnSorting="gvAfterSales_Sorting" 
                                PagerSettings-Position="TopAndBottom" 
                                AllowPaging="True"
                                AllowSorting="true" 
                                PagerSettings-Mode="NumericFirstLast" 
                                PagerStyle-CssClass="FooterContentDetails"
                                EmptyDataText="No Records Found"
                                EmptyDataRowStyle-CssClass="gvEmpty"                             
                                onrowdatabound="gvAfterSales_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table id="tblItem" border="0" cellpadding="0" cellspacing="0" width="100%" style="border-collapse: collapse;">
                                                <%--Note:-Table Starts in Header and Closed in Footer--%>
                                                <tr>
                                                    <th style="width: 10px;" align="left" class="gvHeading">
                                                        &nbsp;
                                                    </th>
                                                    <th class="gvHeading" style="width: 50px;" align="left">
                                                        <asp:LinkButton ID="lnkRegularLane" runat="server" Text="Regular#" CommandName="Sort"
                                                            CommandArgument="RegularLane"></asp:LinkButton>
                                                    </th>
                                                    <th class="gvHeading" style="width: 50px;" align="left">
                                                        <asp:LinkButton ID="lnkExotic" runat="server" Text="Exotic#" CommandName="Sort" CommandArgument="ExoticLane"></asp:LinkButton>
                                                    </th>
                                                    <th class="gvHeading" style="width: 50px;" align="left">
                                                        VIN
                                                    </th>
                                                    <th class="gvHeading" style="width: 50px;" align="left">
                                                        Year
                                                    </th>
                                                    <th class="gvHeading" style="width: 50px;" align="left">
                                                        Make
                                                    </th>
                                                    <th class="gvHeading" style="width: 50px;" align="left">
                                                        Model
                                                    </th>
                                                    <th class="gvHeading" style="width: 150px;" align="left">
                                                        <asp:LinkButton ID="lnkCustomerName" runat="server" Text="Customer" CommandName="Sort"
                                                            CommandArgument="D2.DealerName"></asp:LinkButton>
                                                    </th>
                                                    <th class="gvHeading" style="width: 50px;" align="left">
                                                        <asp:LinkButton ID="lnkSoldDate" runat="server" Text="S.Date" CommandName="Sort"
                                                            CommandArgument="SoldDate"></asp:LinkButton>
                                                    </th>
                                                    <th class="gvHeading" style="width: 30px;" align="left">
                                                        <asp:LinkButton ID="lnkSoldPrice" runat="server" Text="S.P" CommandName="Sort" CommandArgument="SoldPrice"></asp:LinkButton>
                                                    </th>
                                                    <th class="gvHeading" style="width: 30px;" align="left">
                                                        <asp:LinkButton ID="lnkMileageOut" runat="server" Text="M.Out" CommandName="Sort"
                                                            CommandArgument="MileageOut"></asp:LinkButton>
                                                    </th>
                                                    <%--  <th class="gvHeading" style="width: 50px;" align="left">
                                                        <asp:LinkButton ID="lnkDepositDate"  runat="server" Text="D.Date" CommandName="Sort"
                                                            CommandArgument="DepositDate"></asp:LinkButton>
                                                    </th>
                                                    <th class="gvHeading" style="width: 30px;" align="left">
                                                        <asp:LinkButton ID="lnkDepositAmount"  runat="server" Text="D.Amt" CommandName="Sort"
                                                            CommandArgument="DepositAmount"></asp:LinkButton>--%>
                                                    </th>
                                                    <th class="gvHeading" style="width: 20px;" align="left">
                                                        Sold
                                                    </th>
                                                    <th class="gvHeading" style="width: 20px;" align="left">
                                                        T
                                                    </th>
                                                    <th class="gvHeading" style="width: 100px;" align="left">
                                                        <asp:LinkButton ID="lnkDealerName" runat="server" Text="Dealer" CommandName="Sort"
                                                            CommandArgument="D1.DealerName"></asp:LinkButton>
                                                    </th>
                                                    <th class="gvHeading" style="width: 40px;" align="left">
                                                        <asp:LinkButton ID="lnkCarCost" runat="server" Text="C.Cost" CommandName="Sort" CommandArgument="CarCost"></asp:LinkButton>
                                                    </th>
                                                    <th class="gvHeading" style="width: 40px;" align="left">
                                                        &nbsp;
                                                    </th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class='<%# Container.DataItemIndex % 2 == 0 ? "gvRow" : "gvAlternateRow" %>'>
                                                <td class="GridContent" align="left" nowrap="nowrap">
                                                    <asp:ImageButton ID="btnEdit" ImageUrl="~/Images/edit-icon.jpg" CommandName="Edit"
                                                        runat="server" />
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("RegularLane") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("ExoticLane") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("VIN") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("Year") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("Make") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("Model") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("CustomerName") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("SoldDate") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentRight" style="width: 40px">
                                                    <%# String.Format("{0:F0}", Eval("SoldPrice") ?? "&nbsp;")%>
                                                </td>
                                                <td class="GridContentRight" style="width: 30px" align="right">
                                                    <%# String.Format("{0:F0}", Eval("MileageOut") ?? "&nbsp;")%>
                                                </td>
                                                <%-- <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("DepositDate") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentRight" style="width: 30px">
                                                    <%# String.Format("{0:F0}", Eval("DepositAmount") ?? "&nbsp;")%>
                                                </td>--%>
                                                <td class="GridContentLeft" style="width: 20px">
                                                    <%#Eval("SoldStatus") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 20px">
                                                    <%#Eval("TitlePresent") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("DealerName") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentRight" style="width: 40px">
                                                    <%# String.Format("{0:F0}", Eval("CarCost") ?? "&nbsp;")%>
                                                </td>
                                                <td class="GridContent" style="width: 40px" nowrap="nowrap">
                                                    <asp:HyperLink ID="hplViewCarPage" runat="server" NavigateUrl='<%# "InventoryDetail.aspx?Code=" +Eval("InventoryId")+"&ReturnURL=AfterSalesInventories.aspx" %>'
                                                        ImageUrl="~/Images/select.gif" ToolTip="View car details"></asp:HyperLink>
                                                    <asp:ImageButton ID="btnHistory" runat="server" ImageUrl="~/Images/hist-icon-lane.jpg"
                                                        ToolTip="View history" OnClientClick='<%# String.Format("OpenWindow({0});", Eval("InventoryId")) %>'
                                                        CausesValidation="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <tr class="gvRow">
                                                <td class="GridContentLeft">
                                                    &nbsp;
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("RegularLane") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("ExoticLane") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("VIN") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("Year") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("Make") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("Model") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("CustomerName") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("SoldDate") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentRight" style="width: 30px">
                                                    <%# String.Format("{0:F0}", Eval("SoldPrice") ?? "&nbsp;")%>
                                                </td>
                                                <td class="GridContentRight" style="width: 30px">
                                                    <%# String.Format("{0:F0}", Eval("MileageOut") ?? "&nbsp;")%>
                                                </td>
                                                <%--<td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("DepositDate") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentRight" style="width: 30px">
                                                    <%# String.Format("{0:F0}", Eval("DepositAmount") ?? "&nbsp;")%>
                                                </td>--%>
                                                <td class="GridContentLeft" style="width: 20px">
                                                    <%#Eval("SoldStatus") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 20px">
                                                    <%#Eval("TitlePresent") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentLeft" style="width: 100px">
                                                    <%#Eval("DealerName") ?? "&nbsp;"%>
                                                </td>
                                                <td class="GridContentRight" style="width: 30px">
                                                    <%# String.Format("{0:F0}", Eval("CarCost") ?? "&nbsp;")%>
                                                </td>
                                                <td class="GridContentLeft">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="nowrap" class="GridContent">
                                                    <asp:ImageButton ID="btnUpddateRecord" ImageUrl="~/Images/confirm.gif" ToolTip="Update"
                                                        CommandName="Update" runat="server" />
                                                    <asp:ImageButton ID="btnCancelEdit" ImageUrl="~/Images/Delete.gif" ToolTip="Cancel"
                                                        runat="server" CommandName="Cancel" />
                                                </td>
                                                <td colspan="17">
                                                    <table border="0" cellpadding="0" bgcolor="LightSkyBlue" cellspacing="0" width="100%"
                                                        style="border-collapse: collapse;">
                                                        <tr id="trMAA" runat="server">
                                                            <td style="text-align: left" colspan="5">
                                                                <asp:Label ID="lblMAACustomer" runat="server" Text='<%# Bind("MAACustomerName") %>'></asp:Label>
                                                                <asp:Label ID="lblMAAAddress" runat="server" Text='<%# Bind("MAACustomerAddress") %>'></asp:Label>
                                                                &nbsp;MileageIn: <asp:Label ID="lblMileageIn" runat="server" Text='<%# Bind("MileageIn") %>'></asp:Label>
                                                                &nbsp;S.D:
                                                                <asp:Label ID="lblMAASoldDate"  Text='<%# Bind("MAASoldDate") %>' runat="server"></asp:Label>
                                                                &nbsp;<b>S.P:
                                                                    <asp:Label ID="lblMAASoldPrice"  Text='<%# Bind("MAASoldPrice") %>'
                                                                        runat="server"></asp:Label>
                                                                    &nbsp;<b>Ph:  <asp:Label ID="lblMAABuyerPhone"  Text='<%# Bind("MAABuyerPhone") %>'
                                                                        runat="server"></asp:Label>&nbsp;&nbsp;
                                                                    <asp:Button ID="btnMAAPopulate" CssClass="btn" Text="MAA Populate" runat="server"
                                                                        OnClick="btnMAAUpdate_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left" >
                                                                <b>Customer Name: </b>
                                                                <br />
                                                                <asp:TextBox ID="txtCustomerName" onblur="GetCustomerId(this.id)" CssClass="txtMan2"
                                                                    Width="300px" Wrap="false" Text='<%# Bind("CustomerName") %>' runat="server"
                                                                    autocomplete="off" />
                                                                <ajax:AutoCompleteExtender ID="txtTest_AutoCompleteExtender" runat="server" TargetControlID="txtCustomerName"
                                                                    ServicePath="../WS/AutoFillCustomers.asmx" ServiceMethod="AutoFillCustomers"  UseContextKey="true"
                                                                    MinimumPrefixLength="2" CompletionSetCount="25" EnableCaching="false" CompletionListCssClass="autocomplete_completionListElement"
                                                                    CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                                    DelimiterCharacters=";,:">
                                                                </ajax:AutoCompleteExtender>
                                                                <asp:HiddenField ID="hdCustomerId" runat="server" Value=' <%#Eval("CustomerId")%>' />
                                                            </td>
                                                            
                                                            <td align="left" width="150px" >
                                                                <b>Sold Price:</b><br />
                                                                <asp:TextBox ID="txtSoldPrice" CssClass="txtSmall" Text='<%# Bind("SoldPrice") %>'
                                                                    runat="server"></asp:TextBox><br />
                                                                <ajax:FilteredTextBoxExtender ID="txtSoldPrice_FilteredTextBoxExtender" runat="server"
                                                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtSoldPrice">
                                                                </ajax:FilteredTextBoxExtender>
                                                                <b>Mileage-Out:</b>&nbsp;&nbsp;<br />
                                                                <asp:TextBox ID="txtMieageOut" CssClass="txtSmall" runat="server" Text='<%# Bind("MileageOut") %>'></asp:TextBox>
                                                                <ajax:FilteredTextBoxExtender ID="txtMieageOut_FilteredTextBoxExtender" runat="server"
                                                                    FilterType="Numbers" TargetControlID="txtMieageOut">
                                                                </ajax:FilteredTextBoxExtender>
                                                            </td>
                                                          
                                                            <td align="left" width="150px">
                                                                <b>Sold Status:</b><br />
                                                                <asp:DropDownList ID="ddlSoldStatusTable" SelectedValue='<%# Bind("SoldStatusbool") %>'
                                                                    runat="server" CssClass="txt2" Width="90px">
                                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Sold Not Paid" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Selected="True" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <br />
                                                                <b>Sold Date:</b><br />
                                                                <asp:TextBox ID="txtSoldDate" Enabled="false" CssClass="txtSmall" Text='<%# Bind("SoldDate") %>'
                                                                    runat="server" />
                                                                <ajax:CalendarExtender ID="calSoldDate" runat="server" PopupButtonID="imgSoldDate"
                                                                    TargetControlID="txtSoldDate">
                                                                </ajax:CalendarExtender>
                                                                <asp:Image ID="imgSoldDate" runat="server" ImageUrl="~/Images/calender-icon.gif"
                                                                    Style="cursor: pointer;" />
                                                            </td>
                                                            <td align="left">
                                                                <b>Sold Comments:</b><br />
                                                                <asp:TextBox ID="txtSoldComment" CssClass="txtMan2" TextMode="MultiLine" Rows="4"
                                                                    Text='<%# Bind("SoldComment") %>' runat="server"></asp:TextBox>&nbsp;
                                                            </td>
                                                          
                                                            <td align="center" width="30px">
                                                                <asp:ImageButton ID="btnUpdate" CommandName="Update" ImageUrl="~/Images/confirm.gif"
                                                                    ToolTip="Update" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="FooterContentDetails" HorizontalAlign="Right" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="objMAAAfterSalesData" runat="server" SelectMethod="GetMAAAfterSalesData"
                                TypeName="METAOPTION.BAL.MAAAfterSalesBAL" EnablePaging="True" SelectCountMethod="GetMAAAfterSalesDataRecordCount"
                                OnSelecting="objAfterSalesData_Selecting">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtRegLane" DefaultValue="-1" Name="regLaneNumber"
                                        PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txtExoticLane" DefaultValue="-1" Name="exoticLaneNumber"
                                        PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="ddlSoldStatus" DefaultValue="0" Name="soldStatus"
                                        PropertyName="SelectedValue" Type="Int32" />
                                    <asp:Parameter Name="startRowIndex" Type="Int32" />
                                    <asp:Parameter Name="maximumRows" Type="Int32" />
                                    <asp:Parameter Name="sortExpression" Type="String" />
                                    <asp:Parameter Name="sortDirection" Type="String" />
                                    <asp:ControlParameter ControlID="chkMAA_Records" DefaultValue="True" 
                                        Name="isMAASearch" PropertyName="Checked" Type="Boolean" />
                                    <asp:SessionParameter DefaultValue="-1" Name="SystemID" SessionField="systemid" Type="Int32" /> 
                                    <asp:SessionParameter Name="OrgID" Type="Int16" SessionField="OrgID" />   
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
                                <ProgressTemplate>
                                    <div class="overlay" id="divProgress">
                                        &nbsp; Please wait... &nbsp;
                                        <br />
                                        &nbsp;
                                        <asp:Image GenerateEmptyAlternateText="true" ID="Image1" runat="server" ImageUrl="../Images/Wait.gif"
                                            Style="margin-top: 7px;" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>

    
    <script type="text/javascript">
	Sys.Browser.WebKit = {}; //Safari 3 is considered WebKit
	if( navigator.userAgent.indexOf( 'WebKit/' ) > -1 )
	{
	  Sys.Browser.agent = Sys.Browser.WebKit;
	  Sys.Browser.version = parseFloat( navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
	  Sys.Browser.name = 'WebKit';
	}
	</script>

</asp:Content>
