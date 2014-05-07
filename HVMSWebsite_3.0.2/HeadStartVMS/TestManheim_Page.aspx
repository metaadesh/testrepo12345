<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestManheim_Page.aspx.cs" Inherits="METAOPTION.TestManheim_Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Test Manheim Auction Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>    <b>&nbsp;&nbsp; *************************Activities to be Tested***************************************<br />
            <br />
            <br />
        1)(Request Inititation)
         This test page send request for registration of inventories in Manheim Auction.
        <br />
            <br />
            <br />
       (Note:-Steps Omitted here for testing purpose,Create generic Method to generate request file
       and read text file from some grid data and call webservice method without uploading any file.
        <br />
        &nbsp;<br />
            <asp:TextBox ID="txtRequestForReg" TextMode="MultiLine" Rows="20"  
                runat="server" Width="640px" ></asp:TextBox>
            <br />
            <asp:Button id="btnUpload" Text="Initiate Request For Registration" runat="server" 
                onclick="btnUpload_Click" />
            <br />
            <asp:GridView ID="gvVehiclesAccepted" runat="server">
            </asp:GridView>
            <br />
          
        2)Get Acknowledgement for request initiated whether the same has been accepted/validated in their System
        <br />
            <br />
            <asp:Label ID="lblRequestAckXml" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
           <asp:Label ID="lblErrorMessage"   runat="server"></asp:Label>
        3)(Request Cancellation) Send request for Cancellation of previous request's initiated.
        </b>
        </div>
        
        
    </div>
    </form>
</body>
</html>
