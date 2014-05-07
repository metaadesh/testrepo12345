<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Scanner.ascx.cs" Inherits="METAOPTION.UserControls.Scanner" %>
<link href="../CSS/ScanRelated.css" rel="stylesheet" type="text/css" />
<link href="../CSS/ControlStyle.css" rel="stylesheet" type="text/css" />
<script src="../CSS/jquery-1.7.2.min.js" type="text/javascript"></script>
<div style="width: 820px; float: left">
    <div id='divControlNotInstalled' style="width: 93%; float: left; position: relative;
        top: 25%; left: 5%; ">
        <div id="show1" style="display: none;">
        </div>
        <div id='divIfNotMac' style='display: block; color: Red !important; display:none'>
            <a id="hrDWTEXE" runat="server" target="_blank" href="http://www.dynamsoft.com/demo/DWT/online_demo_scan.aspx">
                <strong>Download and install the Plug-in Here</strong></a><br />
            <span>After the installation, please restart your browser.</span>
        </div>
        <div id='divIfMac' style='display: none; color: Red !important; display:none'>
            <a id="hrDWTMac" runat="server"><strong>Download and install the Plug-in Here</strong></a><br />
            After the installation, please quit and restart your browser.
            <br />
            If you are using Safari 5.0, you need to <a href="http://kb.dynamsoft.com/questions/666/How+to+run+Safari+5.0+in+32-bit+mode+on+Mac+OS+X">
                run the browser in 32-bit Mode</a>.
        </div>
    </div>
    <div style="width: 99%; float: left">
        <asp:HiddenField ID="hdnEntityId" runat="server" />
        <asp:HiddenField ID="hdnEntityTypeId" runat="server" />
        <asp:HiddenField ID="hdnDocumentTypeId" runat="server" />
        <asp:HiddenField ID="hdnFileType" runat="server" />
        <asp:HiddenField ID="hdnAddedBy" runat="server" />
        <asp:HiddenField ID="hdnFileName" runat="server" />
        <asp:HiddenField ID="hdnFile" runat="server" Value="application/pdf" />
        <div id='mainControlInstalled' class='divcontrol' style="background-color: White !important;
            width: 55%; float: left; margin: 0 0 0 22px; padding: 10px 2px">
            <div id='divWebTwainIE'>
                <object id='mainDynamicWebTwainIE' style='display: none' class='divcontrol' codebase='~/DynamicWebTWAIN/DynamicWebTWAIN.cab#version=8,0,1'
                    classid='clsid:FFC6F181-A5CF-4ec4-A441-093D7134FBF2'>
                </object>
                <div id="maindivIE">
                    <object classid="clsid:5220cb21-c88d-11cf-b347-00aa00a28331" style="display: none;">
                        <param name="LPKPath" value="Resources/DynamicWebTwain.lpk" />
                    </object>
                </div>
            </div>
            <div id='divWebTwainNotIE'>
                <embed style='display: none' id='mainDynamicWebTWAINnotIE' type='Application/DynamicWebTwain-Plugin'
                    class="divcontrol" pluginspage="~/DynamicWebTWAIN/DynamicWebTWAINPlugIn.msi"></embed>
            </div>
        </div>
        <div style="width: 38%; float: left; margin-left: 15px">
            <div style="background-color: #F0F0F0; color: #222222; font-family: verdana,sans-serif;
                font-size: 11px; line-height: 14px; margin: 5px 5px 10px; padding: 10px; text-align: left;">
                
                <ul style='list-style-type: none'>
                    <li>
                        <div style="padding:5px 0 5px 0;display:none;"><asp:CheckBox ID="chkShowScanner" Text="Show Scanner UI" runat="server" CssClass="ChkBoxLabelVerticalAlign"  /></div>
                    </li>
                    <li>
                        <input value="Scan" onclick="scan();" type="button" class="btn" style="width: 120px;
                            height: 30px" />
                    </li>
                </ul>
                <label id='lblWebTwainMessages'>
                </label>
                <ul style='list-style-type: none'>
                    <li><b>Document Type (Optional):</b><asp:DropDownList ID="ddlDocumentType" runat="server"
                        CssClass="txt2" />
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDocumentType"
                        ErrorMessage="Value Required!" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                    </li>
                    <li><b>Title (Optional):</b><br />
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="txt2" Width="220px" />
                        <span id="Title_warning" runat="server" style="color: red"></span></li>
                    <li><b>Description (Optional):</b><br />
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="txt2"
                            Width="220px" />
                    </li>
                </ul>
            </div>
            <div id="divUpload" class="divinput">
                <ul style='list-style-type: none'>
                    <li><b>Edit/Upload Document</b></li>
                    <li>
                        <label for="txt_fileName">
                            <b>File Name:</b>
                            <br />
                            <input type="text" size="20" id="txt_fileName" class="txt2" style="width:220px" onblur="SetFileName();" /></label>
                        <span id="File_Name" runat="server" style="color: red"></span></li>
                    <li>
                        <label for="imgTypejpeg2">
                            <input type="radio" value="jpg" name="ImageType" id="imgTypejpeg2" onclick="rd_onclick();" />JPEG</label>
                        <label for="imgTypetiff2" style="display: none">
                            <input type="radio" value="tif" name="ImageType" id="imgTypetiff2" onclick="rdTIFF_onclick();" />TIFF</label>
                        <label for="imgTypepng2" style="display: none">
                            <input type="radio" value="png" name="ImageType" id="imgTypepng2" onclick="rd_onclick();" />PNG</label>
                        <label for="imgTypepdf2">
                            <input type="radio" value="pdf" name="ImageType" id="imgTypepdf2" checked="checked"
                                onclick="rdPDF_onclick();" />PDF</label>
                    </li>
                    <li>
                        <label for="MultiPageTIFF" style="display: none;">
                            <input type="checkbox" id="MultiPageTIFF" />Multi-Page TIFF</label>
                        <label for="MultiPagePDF">
                            <input type="checkbox" id="MultiPagePDF" checked="checked" />Multi-Page PDF
                        </label>
                    </li>
                    <br />
                    <li style="padding-left: 5px;">
                        <input type="button" value="Edit Document" id="btnEditor" onclick="return btnShowImageEditor_onclick()"
                            class="btn" style="width: 120px" /></li>
                    <br />
                    <li style="padding-left: 5px;">
                        <input id="btnUpload" type="button" value="Upload Document" onclick="return btnUpload_onclick()"
                            class="btn" style="width: 120px" /></li>
                </ul>
            </div>
            <div id="divEdit" style="display: none" class="divinput">
                <ul style='list-style-type: none;'>
                    <li><b>Edit Image</b></li>
                    <li style="padding-left: 9px; visibility: hidden">
                        <input type="button" value="Rotate Right" id="btnRotateR" onclick="return btnRotateRight_onclick()" />
                        <input type="button" value="Rotate Left" id="btnRotateL" onclick="return btnRotateLeft_onclick()" /></li>
                    <li style="padding-left: 9px; visibility: hidden">
                        <input type="button" value="Mirror" id="btnMirror" onclick="return btnMirror_onclick()" />
                        <input type="button" value="Flip" id="btnFlip" onclick="return btnFlip_onclick()" />
                        <input type="button" value="Crop" id="btnCrop" onclick="btnCrop_onclick();" /></li>
                    <li style="padding-left: 9px; height: 20px; visibility: hidden">
                        <input type="button" value="Change Image Size" id="btnChangeImageSize" onclick="return btnChangeImageSize_onclick();"
                            style="float: left" /></li>
                </ul>
                <div id="ImgSizeEditor" style="visibility: hidden; text-align: left;">
                    <ul>
                        <li>
                            <label for="img_height">
                                <b>New Height :</b>
                                <input type="text" id="img_height" style="width: 50%;" size="10" />pixel</label></li>
                        <li>
                            <label for="img_width">
                                <b>New Width :</b>&nbsp;
                                <input type="text" id="img_width" style="width: 50%;" size="10" />pixel</label></li>
                        <li>Interpolation method:
                            <select size="1" id="InterpolationMethod">
                                <option value=""></option>
                            </select></li>
                        <li style="text-align: center;">
                            <input type="button" value="   OK   " id="btnChangeImageSizeOK" onclick="return btnChangeImageSizeOK_onclick();" />
                            <input type="button" value=" Cancel " id="btnCancelChange" onclick="return btnCancelChange_onclick();" /></li>
                    </ul>
                </div>
                <div id="Crop" style="visibility: hidden;">
                    <div style="width: 50%; height: 100%; float: left; text-align: left;">
                        <ul>
                            <li>
                                <label for="img_left">
                                    <b>left: </b>
                                    <input type="text" id="img_left" style="width: 50%;" size="4" /></label></li>
                            <li>
                                <label for="img_top">
                                    <b>top: </b>
                                    <input type="text" id="img_top" style="width: 50%;" size="4" /></label></li>
                            <li style="text-align: center;">
                                <input type="button" value="  OK  " id="btnCropOK" onclick="return btnCropOK_onclick()" /></li>
                        </ul>
                    </div>
                    <div style="width: 50%; height: 100%; float: left; text-align: right;">
                        <ul>
                            <li>
                                <label for="img_right">
                                    <b>right : </b>
                                    <input type="text" id="img_right" style="width: 50%;" size="4" /></label></li>
                            <li>
                                <label for="img_bottom">
                                    <b>bottom:</b>
                                    <input type="text" id="img_bottom" style="width: 50%;" size="4" /></label></li>
                            <li style="text-align: center;">
                                <input type="button" value="Cancel" id="cancelcrop" onclick="return btnCropCancel_onclick()" /></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div id="divSave" style="display: none;">
                <ul style='list-style-type: none'>
                    <li><b>Save Image</b></li>
                    <li style="padding-left: 15px;">
                        <label for="txt_fileNameforSave">
                            File Name:<input type="text" size="20" id="txt_fileNameforSave" /></label></li>
                    <li style="padding-left: 12px;">
                        <label for="imgTypebmp">
                            <input type="radio" value="bmp" name="imgType_save" id="imgTypebmp" onclick="rdsave_onclick();" />BMP</label>
                        <label for="imgTypejpeg">
                            <input type="radio" value="jpg" name="imgType_save" id="imgTypejpeg" onclick="rdsave_onclick();" />JPEG</label>
                        <label for="imgTypetiff">
                            <input type="radio" value="tif" name="imgType_save" id="imgTypetiff" onclick="rdTIFFsave_onclick();" />TIFF</label>
                        <label for="imgTypepng">
                            <input type="radio" value="png" name="imgType_save" id="imgTypepng" onclick="rdsave_onclick();" />PNG</label>
                        <label for="imgTypepdf">
                            <input type="radio" value="pdf" name="imgType_save" id="imgTypepdf" onclick="rdPDFsave_onclick();" />PDF</label></li>
                    <li style="padding-left: 12px;">
                        <label for="MultiPageTIFF_save">
                            <input type="checkbox" id="MultiPageTIFF_save" />Multi-Page TIFF</label>
                        <label for="MultiPagePDF_save">
                            <input type="checkbox" id="MultiPagePDF_save" />Multi-Page PDF
                        </label>
                    </li>
                    <li style="text-align: center">
                        <input id="btnSave" type="button" value="Save Files(s)" onclick="return btnSave_onclick()" /></li>
                </ul>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">

    var ostype = "windows";
    var EventKey = '0';
    var FileTitle = '';
    var FileDescription = '';
    if (navigator.userAgent.toLowerCase().indexOf("macintosh") != -1)
        ostype = "mac";
    $(document).ready(function () {
        try {
           
//            var Scanner_ShowUI = '<%=Scanner_ShowUI %>';
//            if (Scanner_ShowUI == 'True')
//                $('#<%=chkShowScanner.ClientID %>').attr('checked', true);
//            else
//                $('#<%=chkShowScanner.ClientID %>').removeAttr('checked');

            if (navigator.userAgent.indexOf("MSIE") != -1) {

                document.getElementById('divWebTwainIE').style.display = 'inline';
                document.getElementById('divWebTwainNotIE').style.display = 'none';
                document.getElementById("mainDynamicWebTWAINnotIE").style.display = 'none';

                if (navigator.userAgent.indexOf("Win64") == -1) {
                    document.getElementById("mainDynamicWebTwainIE").style.display = 'inline';

                }
                else {

                    document.getElementById("mainDynamicWebTwainIE").style.display = 'none';
                }
            }
            else {

                document.getElementById('divWebTwainIE').style.display = 'none';
                document.getElementById('divWebTwainNotIE').style.display = 'inline';
                document.getElementById('divWebTwainNotIE').style.width = '100%';

                document.getElementById("mainDynamicWebTwainIE").style.display = 'none'
                document.getElementById("mainDynamicWebTWAINnotIE").style.display = 'inline';
                document.getElementById('mainDynamicWebTWAINnotIE').style.width = '100%';
            }

            ControlLoad()

        } catch (e) {
            alert(e)
        }
    });
    function ControlLoad() {
        try {
            if (ExplorerType() == "IE") {

                document.getElementById('divWebTwainIE').style.display = 'inline';
                document.getElementById('divWebTwainNotIE').style.display = 'none';

                document.getElementById("mainDynamicWebTWAINnotIE").style.display = 'none';
                if (navigator.userAgent.indexOf("Win64") == -1) {

                    document.getElementById("mainDynamicWebTwainIE").style.display = 'inline';


                }
                else {

                    document.getElementById("mainDynamicWebTwainIE").style.display = 'none';

                }
            }
            else {

                document.getElementById('divWebTwainIE').style.display = 'none';
                document.getElementById('divWebTwainNotIE').style.display = 'inline';


                document.getElementById("mainDynamicWebTwainIE").style.display = 'none'
                document.getElementById("mainDynamicWebTWAINnotIE").style.display = 'inline';
            }

            ControlDetect();
        } catch (e) {
            alert(e)
        }
    }

    function ExplorerType() {
        ua = (navigator.userAgent.toLowerCase());
        if (ua.indexOf("msie") != -1) {
            return "IE";
        }
        else {
            return "notIE";
        }
    }
    function getWebTWAINObject() {
        if (ExplorerType() == "IE") {
            document.getElementById("mainDynamicWebTWAINnotIE").style.display = 'none';
            document.getElementById("mainDynamicWebTwainIE").style.display = 'inline'

            return document.getElementById("mainDynamicWebTwainIE");
        }
        else {
            document.getElementById("mainDynamicWebTwainIE").style.display = 'none'
            document.getElementById("mainDynamicWebTWAINnotIE").style.display = 'inline';

            return document.getElementById("mainDynamicWebTWAINnotIE");

        }
    }
    function ControlDetect() {
        var WebTWAIN = getWebTWAINObject();
        var ua = navigator.userAgent;


        if (WebTWAIN.ErrorCode == 0) {

            var i;

            WebTWAIN.OpenSourceManager();



            WebTWAIN.MaxImagesInBuffer = 4096;
            if (ostype != "mac") {
                WebTWAIN.MouseShape = false;
            }


            MultiPageTIFF_save = document.getElementById("MultiPageTIFF_save");
            MultiPageTIFF_save.disabled = true;
            MultiPagePDF_save = document.getElementById("MultiPagePDF_save");
            MultiPagePDF_save.disabled = true;
            MultiPageTIFF = document.getElementById("MultiPageTIFF");
            MultiPageTIFF.disabled = true;
            MultiPagePDF = document.getElementById("MultiPagePDF");
            MultiPagePDF.disabled = false;




            if (WebTWAIN.SourceCount == 0) {
                document.getElementById("aNoScanner").style.color = "Red";
                document.getElementById("aNoScanner").innerHTML = "<b>No TWAIN compatible drivers detected:<b/>";
                document.getElementById("Resolution").style.display = "none";

            }

        }
        else if (ua.indexOf('MSIE') == -1) {
            var obj = document.getElementById("maindivIE");
            obj.style.display = "none";
            setInterval(sensePlugIn, 1500);

            // 

        }

    }
    function sensePlugIn() {
        var WebTWAIN = getWebTWAINObject();

        if (WebTWAIN.ErrorCode != 0) {
            document.getElementById('divControlNotInstalled').style.display = 'inline';
            if (ostype == 'mac') {
                document.getElementById('divIfNotMac').style.display = 'none';
                document.getElementById('divIfMac').style.display = 'inline';

            }
            else {
                document.getElementById('divIfNotMac').style.display = 'inline';
                document.getElementById('divIfMac').style.display = 'none';
            }

            //document.getElementById("mainControlInstalled").style.display = "none";
        }
        else {

            document.getElementById('divControlNotInstalled').style.display = 'none';
            document.getElementById('divIfNotMac').style.display = 'none';
            document.getElementById('divIfMac').style.display = 'none';
        }

    }

    function scan_old() {
        hideError();

        var WebTWAIN = getWebTWAINObject();

        var Scanner_ShowUI = '<%=Scanner_ShowUI %>';
        var Scanner_AutoFeedEnabled = '<%=Scanner_AutoFeedEnabled %>';
        var Scanner_DuplexEnabled = '<%=Scanner_DuplexEnabled %>';
        var Scanner_PixelType = '<%=Scanner_PixelType %>';
        var Scanner_Resolution = '<%=Scanner_Resolution %>';

        var WebTWAIN = getWebTWAINObject();
        var isShowScannerChecked = $('#<%=chkShowScanner.ClientID %>').attr('checked') ? true : false;

        try {
           
                WebTWAIN.OpenSource;
                if (Scanner_ShowUI == 'True')
                    WebTWAIN.IfShowUI = true;
                else
                    WebTWAIN.IfShowUI = false;

                if (Scanner_AutoFeedEnabled == 'True')
                    WebTWAIN.IfAutoFeed = true;
                else
                    WebTWAIN.IfAutoFeed = false;

                if (Scanner_DuplexEnabled == 'True')
                    WebTWAIN.IfDuplexEnabled = true;
                else
                    WebTWAIN.IfDuplexEnabled = false;

//                WebTWAIN.PixelType = parseInt(Scanner_PixelType);
//                WebTWAIN.Resolution = parseFloat(Scanner_Resolution);
                WebTWAIN.AcquireImage();
        } catch (e) {

            showMessage(e, true);
        }
    }

    function scan2() {
       // hideError();

        var WebTWAIN = getWebTWAINObject();

        var Scanner_ShowUI = '<%=Scanner_ShowUI %>';

        var Scanner_AutoFeedEnabled = '<%=Scanner_AutoFeedEnabled %>';

        var Scanner_DuplexEnabled = '<%=Scanner_DuplexEnabled %>';

        var Scanner_PixelType = '<%=Scanner_PixelType %>';

        var Scanner_Resolution = '<%=Scanner_Resolution %>';

        var isShowScannerChecked = $('#<%=chkShowScanner.ClientID %>').attr('checked') ? true : false;

        // define initial values
        //WebTWAIN.CloseSource();
       // WebTWAIN.CloseSourceManager();
       // WebTWAIN.OpenSourceManager();
        WebTWAIN.SelectSource();
        WebTWAIN.OpenSource();
        //WebTWAIN.IfAutoFeed = false;
        //WebTWAIN.IfDuplexEnabled = false;

        WebTWAIN.IfShowUI = false;
        /*
            if (!isShowScannerChecked) {

                if (Scanner_AutoFeedEnabled == 'True')
                    WebTWAIN.IfAutoFeed = true;

                if (Scanner_DuplexEnabled == 'True')
                    WebTWAIN.IfDuplexEnabled = true;

//                WebTWAIN.PixelType = parseInt(Scanner_PixelType);

//                WebTWAIN.Resolution = parseFloat(Scanner_Resolution);

            }

            else
        

                WebTWAIN.IfShowUI = true;
            */

            WebTWAIN.AcquireImage();

    }

    function scan() {
        hideError();
        var WebTWAIN = getWebTWAINObject();

        try {
            //New
            WebTWAIN.IfShowUI = true;
            WebTWAIN.PixelType = 1;
            WebTWAIN.AcquireImage();
        } catch (e) {

            showMessage(e, true);
        }
    }

    function CheckIfImagesInBuffer() {
        var WebTWAIN = getWebTWAINObject();
        if (WebTWAIN.HowManyImagesInBuffer == 0) {
            return false;
        }
        else {
            return true;
        }
    }
    function showMessage(msg, isError) {

        if (isError == true) {
            document.getElementById('lblWebTwainMessages').style.color = 'Red';
            document.getElementById('lblWebTwainMessages').innerHTML = msg;
        }
        else {
            document.getElementById('lblWebTwainMessages').style.color = 'Green';
            document.getElementById('lblWebTwainMessages').innerHTML = msg;

        }
    }
    function hideError() {
        document.getElementById('lblWebTwainMessages').innerHTML = '';

    }

    function btnSave_onclick() {
        try {

            hideError();
            if (!CheckIfImagesInBuffer()) {
                alert('No images in buffer.')
                showMessage('No images in buffer.');
                return;
            }
            var WebTWAIN = getWebTWAINObject();
            var i, strimgType_save;
            for (i = 0; i < 5; i++) {
                if (document.getElementsByName("imgType_save").item(i).checked == true) {
                    strimgType_save = document.getElementsByName("imgType_save").item(i).value;
                    break;
                }
            }
            WebTWAIN.IfShowFileDialog = true;
            var txt_fileNameforSave = document.getElementById('txt_fileNameforSave');
            txt_fileNameforSave.className = "";
            var strre = /^[\s\w]+$/;
            if (!strre.test(txt_fileNameforSave.value)) {
                txt_fileNameforSave.className += " invalid";
                txt_fileNameforSave.focus();
                showMessage("Please input a valid <b>file name</b>.<br />", true);

                return;
            }
            var strFilePath = "C:\\" + txt_fileNameforSave.value + "." + strimgType_save;
            if (strimgType_save == "tif" && MultiPageTIFF_save.checked) {
                if ((WebTWAIN.SelectedImagesCount == 1) || (WebTWAIN.SelectedImagesCount == WebTWAIN.HowManyImagesInBuffer)) {
                    WebTWAIN.SaveAllAsMultiPageTIFF(strFilePath);
                }
                else {
                    WebTWAIN.SaveSelectedImagesAsMultiPageTIFF(strFilePath);
                }
            }
            else if (strimgType_save == "pdf" && MultiPagePDF_save.checked) {
                if ((WebTWAIN.SelectedImagesCount == 1) || (WebTWAIN.SelectedImagesCount == WebTWAIN.HowManyImagesInBuffer)) {
                    WebTWAIN.SaveAllAsPDF(strFilePath);
                }
                else {
                    WebTWAIN.SaveSelectedImagesAsMultiPagePDF(strFilePath);
                }
            }
            else {
                switch (i) {
                    case 0: WebTWAIN.SaveAsBMP(strFilePath, WebTWAIN.CurrentImageIndexInBuffer); break;
                    case 1: WebTWAIN.SaveAsJPEG(strFilePath, WebTWAIN.CurrentImageIndexInBuffer); break;
                    case 2: WebTWAIN.SaveAsTIFF(strFilePath, WebTWAIN.CurrentImageIndexInBuffer); break;
                    case 3: WebTWAIN.SaveAsPNG(strFilePath, WebTWAIN.CurrentImageIndexInBuffer); break;
                    case 4: WebTWAIN.SaveAsPDF(strFilePath, WebTWAIN.CurrentImageIndexInBuffer); break;
                }
            }
            showMessage('<b>Document saved successfully</b>', false);
        } catch (e) {
            alert(e);
        }

    }

    function btnUpload_onclick() {//For Dynamic Mac TWAIN, no progress bar will show
        try {

            var WebTWAIN = getWebTWAINObject();

            if (!CheckIfImagesInBuffer()) {
                alert('No images in buffer.')
                showMessage('No images in buffer.');
                return false; ;
            }
            var strre = /^[-\s\w]+$/;
            var i, strHTTPServer, strActionPage, strImageType;
            var txt_fileName = document.getElementById('txt_fileName');

            txt_fileName.className = "";

            var FileName = document.getElementById('<%= hdnFileName.ClientID %>').value;

            if (!strre.test(txt_fileName.value)) {
                txt_fileName.className += " invalid";
                txt_fileName.focus();

                alert('Please input file name.')
                showMessage('please input <b>file name</b>.<br />');

                return false;
            }
            strHTTPServer = location.hostname;
            WebTWAIN.HTTPPort = location.port == "" ? 80 : location.port;
            var CurrentPathName = unescape(location.pathname); // get current PathName in plain ASCII	
            var CurrentPath = CurrentPathName.substring(0, CurrentPathName.lastIndexOf("/") + 1);

            var strActionPage = CurrentPathName + '?EventKey=' + EventKey;
            for (i = 0; i < 4; i++) {
                if (document.getElementsByName("ImageType").item(i).checked == true) {
                    strImageType = i + 1;
                    break;
                }
            }



            var uploadfilename = txt_fileName.value + "." + document.getElementsByName("ImageType").item(i).value;


            var strTitle = document.getElementById('<%= txtTitle.ClientID %>').value;
            var strDesc = document.getElementById('<%= txtDescription.ClientID %>').value;
            var EntityId = document.getElementById('<%= hdnEntityId.ClientID %>').value;
            var EntityTypeId = document.getElementById('<%= hdnEntityTypeId.ClientID %>').value;
            var DocumentTypeId = document.getElementById('<%= hdnDocumentTypeId.ClientID %>').value;
            var ddlDocType = document.getElementById('<%= ddlDocumentType.ClientID %>');
            var FileType = ddlDocType.options[ddlDocType.selectedIndex].value;
            var AddedBy = document.getElementById('<%= hdnAddedBy.ClientID %>').value;


            if ((strTitle == '') || (strTitle == null))
                strTitle = 'nodata';
            if ((strDesc == '') || (strDesc == null))
                strDesc = 'nodata';
            if ((FileType == '-1') || (FileType == null))
                FileType = 'nodata';



            var fileFormat = document.getElementById('<%= hdnFile.ClientID %>').value;


            uploadfilename = uploadfilename + '#$#' + strTitle + '#$#' + strDesc + '#$#' + EntityId + '#$#' + EntityTypeId + '#$#' + DocumentTypeId + '#$#' + FileType + '#$#' + fileFormat + '#$#' + AddedBy;

            if (strImageType == 1 || strImageType == 3 || strImageType == 4) {
                if (strImageType == 4 && MultiPagePDF.checked) {
                    WebTWAIN.HTTPUploadAllThroughPostAsPDF(
                     strHTTPServer,
                     strActionPage,
                    uploadfilename);
                }
                else {
                    WebTWAIN.HTTPUploadThroughPostEx(
                        strHTTPServer,
                        WebTWAIN.CurrentImageIndexInBuffer,
                        strActionPage,
                        uploadfilename,
                        strImageType);
                }
            }

            //            if (strImageType == 2 && MultiPageTIFF.checked) {
            //                if ((WebTWAIN.SelectedImagesCount == 1) || (WebTWAIN.SelectedImagesCount == WebTWAIN.HowManyImagesInBuffer)) {
            //                    WebTWAIN.HTTPUploadAllThroughPostAsMultiPageTIFF(
            //                strHTTPServer,
            //                strActionPage,
            //                uploadfilename
            //            );
            //                }
            //                else {
            //                    WebTWAIN.HTTPUploadThroughPostAsMultiPageTIFF(
            //                strHTTPServer,
            //                strActionPage,
            //                uploadfilename
            //            );
            //                }
            //            }
            //            else if (strImageType == 4 && MultiPagePDF.checked) {
            //                if ((WebTWAIN.SelectedImagesCount == 1) || (WebTWAIN.SelectedImagesCount == WebTWAIN.HowManyImagesInBuffer)) {

            //                    WebTWAIN.HTTPUploadAllThroughPostAsPDF(
            //                strHTTPServer,
            //                strActionPage,
            //                uploadfilename
            //            );
            //                }
            //                else {
            //                    WebTWAIN.HTTPUploadThroughPostAsMultiPagePDF(
            //                strHTTPServer,
            //                strActionPage,
            //                uploadfilename
            //            );
            //                }
            //            }
            //            else {

            //                WebTWAIN.HTTPUploadThroughPostEx(
            //            strHTTPServer,
            //            WebTWAIN.CurrentImageIndexInBuffer,
            //            strActionPage,
            //            uploadfilename,
            //            strImageType
            //        );
            //            }

            //parent.HideDocumentpopup();
            parent.HideDocumentpopup11092012('<%= hdnEntityId.Value %>');

            //parent.location.reload(true);
            return false;
        } catch (e) {
            alert(e)
            parent.HideModelpopup();
        }
    }



    function rdTIFFsave_onclick() {
        MultiPageTIFF_save.disabled = false;

        MultiPageTIFF_save.checked = false;
        MultiPagePDF_save.checked = false;
        MultiPagePDF_save.disabled = true;
    }
    function rdPDFsave_onclick() {
        MultiPagePDF_save.disabled = false;

        MultiPageTIFF_save.checked = false;
        MultiPagePDF_save.checked = false;
        MultiPageTIFF_save.disabled = true;
    }
    function rdsave_onclick() {
        MultiPageTIFF_save.checked = false;
        MultiPagePDF_save.checked = false;

        MultiPageTIFF_save.disabled = true;
        MultiPagePDF_save.disabled = true;
    }
    function rdTIFF_onclick() {
        MultiPageTIFF.disabled = false;

        MultiPageTIFF.checked = false;
        MultiPagePDF.checked = false;
        MultiPagePDF.disabled = true;
    }
    function rdPDF_onclick() {

        document.getElementById('<%= hdnFile.ClientID %>').value = '';
        document.getElementById('<%= hdnFile.ClientID %>').value = 'application/pdf';
        MultiPagePDF.disabled = false;

        MultiPageTIFF.checked = false;
        MultiPagePDF.checked = true;
        MultiPageTIFF.disabled = true;
    }
    function rd_onclick() {


        document.getElementById('<%= hdnFile.ClientID %>').value = '';
        document.getElementById('<%= hdnFile.ClientID %>').value = 'image/jpeg';

        MultiPageTIFF.checked = false;
        MultiPagePDF.checked = false;

        MultiPageTIFF.disabled = true;
        MultiPagePDF.disabled = true;
    }

    function btnShowImageEditor_onclick() {//Dynamic Mac TWAIN doesn't support this method yet.
        if (!CheckIfImagesInBuffer()) {
            return;
        }
        var WebTWAIN = getWebTWAINObject();
        WebTWAIN.ShowImageEditor();
    }
</script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var FileName = document.getElementById('<%= hdnFileName.ClientID %>').value;
        if (txt_fileName.value != '' || txt_fileName.value != null) {
            txt_fileName.value = FileName;
        }
        else
            txt_fileName.value = txt_fileName.value;
    });


    function SetFileName() {
        txt_fileName.value = txt_fileName.value;
    }

    //    $(document).ready(function () {
    //        txt_fileName.value = txt_fileName.value;

    //        $("input[id$='txt_fileName']").blur(function () {
    //            //debugger;
    //            var FileName = $("input[id$='txt_fileName']").val();
    //            $("#show1").load('../UI/CheckTitleFileName.ashx?FileName=' + FileName + '&Type=FileName' + '&EntityId=' + document.getElementById('<%= hdnEntityId.ClientID %>').value, {
    //            },
    //            function (data) {
    //                if (data == "1") {
    //                    //  debugger;
    //                    $('span[id$="File_Name"]').html("File Name already exists.");
    //                    $("input[id$='btnUpload']").attr("disabled", true);
    //                    $("input[id$='btnUpload']").css("background-color", "#FFB546");
    //                }
    //                else {
    //                    $('span[id$="File_Name"]').empty();
    //                    $("input[id$='btnUpload']").attr("disabled", false);
    //                    $("input[id$='btnUpload']").css("background-color", "#0C4F7C");
    //                }
    //            });
    //        });
    //    });



    //    $(document).ready(function () {
    //        $("input[id$='txtTitle']").blur(function () {
    //            //debugger;
    //            var DocumentTitle = $("input[id$='txtTitle']").val();
    //            $("#show1").load('../UI/CheckTitleFileName.ashx?Title=' + DocumentTitle + '&Type=Title' + '&EntityId=' + document.getElementById('<%= hdnEntityId.ClientID %>').value, {
    //            },
    //            function (data) {
    //                if (data == "1") {
    //                    //  debugger;
    //                    $('span[id$="Title_warning"]').html("Title already exists.");
    //                    $("input[id$='btnUpload']").attr("disabled", true);
    //                    $("input[id$='btnUpload']").css("background-color", "#FFB546");
    //                }
    //                else {
    //                    $('span[id$="Title_warning"]').empty();
    //                    $("input[id$='btnUpload']").attr("disabled", false);
    //                    $("input[id$='btnUpload']").css("background-color", "#0C4F7C");
    //                }
    //            });
    //        });
    //    });
</script>
