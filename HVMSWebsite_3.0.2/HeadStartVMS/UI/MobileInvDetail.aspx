<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileInvDetail.aspx.cs" Inherits="METAOPTION.UI.MobileInvDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<style type="text/css">
    .dvMain
    {
        font-family:Verdana, Tahoma, Arial;
        font-size:100%;
        
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border:1px solid #ccc; text-align:left;" class="dvMain">
        <div style="padding:5px">
            <asp:FormView ID="fvCommonInvDetails" runat="server">
                <ItemTemplate>
                    VIN:<span id="sVIN" runat="server" />
                </ItemTemplate>
            </asp:FormView>
            
            <asp:Label ID="lblVIN" runat="server" Text="WBAAV53461FT02065" />  PRE-INVENTORY<br />
            <b><asp:Label ID="lblYear" runat="server" Text="2010" />
            <asp:Label ID="lblMake" runat="server" Text="Porsche" /></b>
            <asp:Label ID="lblBody" runat="server" Text="911 Carrera 2dr Coupe C2 3.6L" />
            <b><asp:Label ID="lblMileage" runat="server" Text="5,032 Miles" /></b>
            <asp:Label ID="lblPrice" runat="server" Text="$64,000" />
            <asp:Label ID="Label1" runat="server" Text="Basalt Black/Metallic Black" /><br />
            Sun: Y, Lea: N, Nav: Y, CarFax: Good, AutoCheck: Bad, Grade: 9<br />
            Dru Marks/ CLASSIC CARS NISSAN INC&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="ibtnEditBasicCarInfo" runat="server" 
                ImageUrl="http://headstartvms.com/Images/Edit-Main-Icon.gif" 
                OnClick="ibtnEditBasicCarInfo_Click" />
        </div>
        <div style="padding:5px">
            Added On 7/3/2012 11:03 AM from iPhone by Dru/Approved by Claudia on 7/3/2012 12:05 PM
        </div>
        <div style="padding:5px">
            Lane(R): 24-0064    Lane(E): 08-0000&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://www.google.co.in/" target="_blank"><img src="http://headstartvms.com/Images/Edit-Main-Icon.gif" alt="UCR" /></a><br />
            Car Note: 140-BEDLINER/25-T/U&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://www.google.co.in/" target="_blank"><img src="http://headstartvms.com/Images/Edit-Main-Icon.gif" alt="UCR" /></a>
        </div>
        <div style="padding:5px">
            Additional Notes&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://www.google.co.in/" target="_blank"><img src="../Images/AddNote.png" alt="UCR" /></a>
        </div>
        <div style="padding:5px">
            7/3/2012 12:10 PM&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Joe Sciarra(Employee)<br />
            Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum<br /><br />
            7/3/2012 12:10 PM&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dru Marks(Buyer)<br />
            Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum<br /><br />
            7/3/2012 12:10 PM&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bob Hollenshead(Employee)<br />
            Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum<br /><br />
        </div> 
        <div style="text-align:right">
            more...
        </div>
    </div>
    <div style="border:1px solid #ccc; text-align:left;" class="dvMain">
        <div style="padding:5px">
            Expense:<br /><br />
            7/10/2012 Javier Abreu(B) (936.25) Commission Paid-102577<br />
            7/4/2012 Matthew J Murray $25 Touch Up Not Paid<br />
            6/29/2012 C Travis Shearer $105 Dent Not Paid<br />
            6/14/2012 Trans Fee $20 Fee Not Paid
        </div>
    </div>
    <div style="border:1px solid #ccc; text-align:left;" class="dvMain">
        <div style="padding:5px">
            Purchased Details:<br /><br />
            $9,475 7/3/2012 Title: Yes&nbsp;&nbsp;&nbsp;Desig: Here&nbsp;&nbsp;&nbsp;CHK#: 102504
            Exp: $650 Title Shipped: Yes(7/9/2012 7:03:22 AM)
            JH-GARDEN SPOT(1605 APPLE ST, EPHRATA PA 17522, 717-738-7900) ID: 22626
        </div>
    </div>
    <div style="border:1px solid #ccc; text-align:left;" class="dvMain">
        <div style="padding:5px">
            Sold Details:<br /><br />
            Sold - Upadted by Michelle Lincoln on July 9, 2012 3:23 PM
            $13,300 7/6/2012 69,303 Miles $10,125 Actual Cost
            $12,552.50 deposited on 7/9/2012
            SHORELINE AUTO CENTER INC(5409 VIRGINIA BEACH, BLVD)
        </div>
    </div>
    <div style="border:1px solid #ccc; text-align:left;" class="dvMain">
        <div style="padding:5px">
            Activities:<br /><br />
            Archieved on July 9, 2012 by Claudia Dominici<br />
            Adjustment MAA(V) $500 on July 9, 2012<br />
            Sold on July 6, 2012<br />
            Car registered to MAA on July 4, 2012 by Florence<br />
            Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum<br />
            Lorem Ipsum Lorem Ipsum Lorem Ipsum Lorem Ipsum<br /> 
            <div style="text-align:right">
                more...
            </div>
        </div>
    </div>
    <div style="border:1px solid #ccc; text-align:left;" class="dvMain">
        <div style="padding:5px">
            Locations:<br /><br />
            6/29/2012 4:10:30 AM In-Transit show address from Lat/Long by Dru Marks(Buyer)<br />
            6/26/2012 3:15:28 AM Detail Technologies(V) show address from Lat/Long by Joe Sciarra(Employee)<br />
            6/22/2012 9:03:11 AM Office 1373 show address from Lat/Long by Sheikh Basim(Employee)
            <div style="text-align:right">
                more...
            </div>
        </div>
    </div>
    <div style="border:1px solid #ccc; text-align:left;" class="dvMain">
        <div style="padding:5px">
            Images: 20 Images captured(12 Inventory, 5 Expenses, 3 Generic)
        </div>
        <div>
            
        </div>
    </div>
    </form>
</body>
</html>
