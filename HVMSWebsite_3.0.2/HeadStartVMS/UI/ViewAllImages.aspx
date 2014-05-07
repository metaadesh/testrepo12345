<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAllImages.aspx.cs"
    Inherits="METAOPTION.UI.ViewAllImages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../CSS/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../CSS/jquery.ad-gallery.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="../CSS/jquery.ad-gallery.js"></script>
    <link href="../CSS/UCRAssets.css" rel="stylesheet" type="text/css" />
    <script src="../CSS/UCRAssets.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('img.image1').data('ad-desc', 'Whoa! This description is set through elm.data("ad-desc") instead of using the longdesc attribute.<br>And it contains <strong>H</strong>ow <strong>T</strong>o <strong>M</strong>eet <strong>L</strong>adies... <em>What?</em> That aint what HTML stands for? Man...');
            $('img.image1').data('ad-title', 'Title through $.data');
            $('img.image4').data('ad-desc', 'This image is wider than the wrapper, so it has been scaled down');
            $('img.image5').data('ad-desc', 'This image is higher than the wrapper, so it has been scaled down');
            var galleries = $('.ad-gallery').adGallery();
            $('#switch-effect').change(
      function () {
          galleries[0].settings.effect = $(this).val();
          return false;
      }
    );
            $('#toggle-slideshow').click(
      function () {
          galleries[0].slideshow.toggle();
          return false;
      }
    );
            $('#toggle-description').click(
      function () {
          if (!galleries[0].settings.description_wrapper) {
              galleries[0].settings.description_wrapper = $('#descriptions');
          } else {
              galleries[0].settings.description_wrapper = false;
          }
          return false;
      }
    );

            $('.ad-image').live("mouseover", (function () {
                $('.ad-image-description').hide();

            }));

            $('.ad-image').live("mouseout", (function () {
                $('.ad-image-description').show();

            }));

        });
    </script>
    <script language="javascript" type="text/javascript">



        function showDetailsDisplay() {

            if (document.getElementById("hdnComefrom").value != '0')
                ShowFromInventoryDetails();
            else
                ShowFromInventory();

            return false;
        }

        function ShowFromInventoryDetails() {
            var comefeomValue = document.getElementById("hdnComefrom").value;
            if (comefeomValue == 'Inventory') {
                document.getElementById('lnkPreInventory').style.color = '#21618C';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = '';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';
                return false;
            }
            if (comefeomValue == 'Expense') {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = '#21618C';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = '';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';
                return false;
            }
            if (comefeomValue == 'Generic') {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = '#21618C';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = '';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';
                return false;
            }
            if (comefeomValue == 'UCR') {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = '#21618C';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = '';
                document.getElementById('divAllIMageGallery').style.display = 'none';
                return false;

            }
        }

        function ShowFromInventory() {
            if (document.getElementById('hdnInventotryCount').value > 0) {
                document.getElementById('lnkPreInventory').style.color = '#21618C';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = '';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';
                return false;
            }
            if (document.getElementById('hdnExpenseCount').value > 0) {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = '#21618C';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = '';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';
                return false;
            }
            if (document.getElementById('hdnGenericCount').value > 0) {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = '#21618C';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = '';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';
                return false;
            }
            if (document.getElementById('hdnUCRCount').value > 0) {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = '#21618C';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = '';
                document.getElementById('divAllIMageGallery').style.display = 'none';
                return false;

            }
        }


        function ShowHideImages(ctrl) {
            var Id = ctrl.id;
            var divPre = document.getElementById('divInventory');
            var divPreExp = document.getElementById('divPreExpense');
            var divGen = document.getElementById('divGeneric');
            var divUCR = document.getElementById('divUCRgallery');
            if (Id == "lnkPreInventory") {
                document.getElementById('lnkPreInventory').style.color = '#21618C';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = '';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';

                SetDefaultUCRTabs();
            }
            if (Id == "lnkPreExpense") {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = '#21618C';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = '';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';

                SetDefaultUCRTabs();
            }
            if (Id == "lnkGeneric") {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = '#21618C';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = '';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = 'none';

                SetDefaultUCRTabs();
            }
            if (Id == "lnkUCR") {

                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = '#21618C';
                document.getElementById('lnkViewAllImages').style.color = 'white';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#4E9CAF';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = '';
                document.getElementById('divAllIMageGallery').style.display = 'none';


                //                document.getElementById('lnkUCRImage').style.color = '#21618C';
                //                document.getElementById('lnkUCRAudio').style.color = 'white';
                //                document.getElementById('lnkUCRVideo').style.color = 'white';
                SetDefaultUCRTabs();
            }
            if (Id == "lnkViewAllImages") {
                document.getElementById('lnkPreInventory').style.color = 'white';
                document.getElementById('lnkPreExpense').style.color = 'white';
                document.getElementById('lnkGeneric').style.color = 'white';
                document.getElementById('lnkUCR').style.color = 'white';
                document.getElementById('lnkViewAllImages').style.color = '#21618C';
                document.getElementById('lnkPreInventory').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkPreExpense').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkGeneric').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCR').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkViewAllImages').style.backgroundColor = '#e1eef5';
                document.getElementById('divInventory').style.display = 'none';
                document.getElementById('divPreExpense').style.display = 'none';
                document.getElementById('divGeneric').style.display = 'none';
                document.getElementById('divUCRgallery').style.display = 'none';
                document.getElementById('divAllIMageGallery').style.display = '';
                SetDefaultUCRTabs();
            }
            return false;
        }

        function SetDefaultUCRTabs() {
            if (document.getElementById('hdnUCRCount').value > 0) {
                document.getElementById('lnkUCRImage').style.color = '#21618C';
                document.getElementById('lnkUCRAudio').style.color = 'white';
                document.getElementById('lnkUCRVideo').style.color = 'white';

                document.getElementById('lnkUCRImage').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkUCRAudio').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCRVideo').style.backgroundColor = '#4E9CAF';

                document.getElementById('divUCRImages').style.display = '';
                // document.getElementById('UCRgalleryThumb').style.display = '';
                document.getElementById('divUCRAudio').style.display = 'none';
                document.getElementById('divUCRVideo').style.display = 'none';
            }
            else {
                document.getElementById('UCRImagegallery').style.display = '';

                document.getElementById('lnkUCRImage').style.color = '#21618C';
                document.getElementById('lnkUCRAudio').style.color = 'white';
                document.getElementById('lnkUCRVideo').style.color = 'white';

                document.getElementById('lnkUCRImage').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkUCRAudio').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCRVideo').style.backgroundColor = '#4E9CAF';

                // document.getElementById('divUCRImages').style.display = '';
                // document.getElementById('UCRgalleryThumb').style.display = '';
                document.getElementById('divUCRAudio').style.display = 'none';
                document.getElementById('divUCRVideo').style.display = 'none';
            }

            if (document.getElementById('hdnAudioCount').value > 0) {
                document.getElementById('divUCRAudio').style.display = 'none';
                document.getElementById('UCRAudioThumb').style.display = '';

            }
            else {
                document.getElementById('Audiogallery').style.display = '';
                document.getElementById('UCRAudioThumb').style.display = 'none';
            }

            if (document.getElementById('hdnVideoCount').value > 0) {
                document.getElementById('Videogallery').style.display = 'none';
                document.getElementById('UCRVideoThumb').style.display = '';

            }
            else {
                document.getElementById('Videogallery').style.display = '';
                document.getElementById('UCRVideoThumb').style.display = 'none';
            }
            return false;
        }

        function ShowHideUCRTabs(ctrl) {
            var Id = ctrl.id;
            var divUCRImages = document.getElementById('divUCRImages');
            var lnkUCRAudio = document.getElementById('lnkUCRAudio');
            var divUCRVideo = document.getElementById('divUCRVideo');

            if (Id == "lnkUCRImage") {
                document.getElementById('lnkUCRImage').style.color = '#21618C';
                document.getElementById('lnkUCRAudio').style.color = 'white';
                document.getElementById('lnkUCRVideo').style.color = 'white';

                document.getElementById('lnkUCRImage').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkUCRAudio').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCRVideo').style.backgroundColor = '#4E9CAF';

                document.getElementById('divUCRImages').style.display = '';
                document.getElementById('divUCRAudio').style.display = 'none';
                document.getElementById('divUCRVideo').style.display = 'none';

            }
            if (Id == "lnkUCRAudio") {

                //soundPlay(document.getElementById('hdnAudioUrl').value);
                //document.getElementById('EMbadeAudio').src = document.getElementById('hdnAudioUrl').value;
                document.getElementById('lnkUCRImage').style.color = 'white';
                document.getElementById('lnkUCRAudio').style.color = '#21618C';
                document.getElementById('lnkUCRVideo').style.color = 'white';

                document.getElementById('lnkUCRImage').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCRAudio').style.backgroundColor = '#e1eef5';
                document.getElementById('lnkUCRVideo').style.backgroundColor = '#4E9CAF';

                document.getElementById('divUCRImages').style.display = 'none';
                document.getElementById('divUCRAudio').style.display = '';
                document.getElementById('divUCRVideo').style.display = 'none';

            }
            if (Id == "lnkUCRVideo") {
                document.getElementById('lnkUCRImage').style.color = 'white';
                document.getElementById('lnkUCRAudio').style.color = 'white';
                document.getElementById('lnkUCRVideo').style.color = '#21618C';

                document.getElementById('lnkUCRImage').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCRAudio').style.backgroundColor = '#4E9CAF';
                document.getElementById('lnkUCRVideo').style.backgroundColor = '#e1eef5';

                document.getElementById('divUCRImages').style.display = 'none';
                document.getElementById('divUCRAudio').style.display = 'none';
                document.getElementById('divUCRVideo').style.display = '';

            }

            return false;
        }

        function SetAudioURL(ctrl) {
            document.getElementById('EMbadeAudio').innerHTML = "";
            var Id = ctrl.id;
            var IDObject = document.getElementById(Id);
            document.getElementById('hdnAudioUrl').value = IDObject.title;
            // document.getElementById('EMbadeAudio1').src = document.getElementById('hdnAudioUrl').value;
            //document.getElementById('EMbadeAudio').removeAttributes();
            //ShowHideImages('lnkUCR');
            // ShowHideUCRTabs('lnkUCRAudio');
            // document.getElementById('EMbadeAudio').removed = true;
            document.getElementById('EMbadeAudio').innerHTML = document.getElementById('hdnAudioUrl').value;
            //            document.getElementById('hdn').value = "1";
            // soundPlay(IDObject.title);

            return false;
        }
        var soundEmbed = null;
        //======================================================================
        function soundPlay(which) {
            if (!soundEmbed) {
                soundEmbed = document.createElement("embed");
                soundEmbed.setAttribute("src", which + ".mp3");
                soundEmbed.setAttribute("hidden", false);
                soundEmbed.setAttribute("autostart", true);
            }
            else {
                document.body.removeChild(soundEmbed);
                soundEmbed.removed = true;
                soundEmbed = null;
                soundEmbed = document.createElement("embed");
                soundEmbed.setAttribute("src", which + ".mp3");
                soundEmbed.setAttribute("hidden", false);
                soundEmbed.setAttribute("autostart", true);
            }
            soundEmbed.removed = false;
            document.body.appendChild(soundEmbed);
        }

        function RemoveIframe() {
            var iframe = window.parent.document.getElementById('ctl00_ContentPlaceHolder1_ModalPopupExtender1');
            iframe.hide();
            return true;
        }
    </script>
    <title>HeadStart VMS :: ViewAllImages</title>
</head>
<body onload='showDetailsDisplay();'>
    <form id="form1" runat="server">
    <div style="width: 98%; float: left; height: auto">
        <Ajax:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </Ajax:ToolkitScriptManager>
        <asp:HiddenField ID="hfType" runat="server" Value="0" />
        <asp:HiddenField ID="hdnInventotryCount" runat="server" Value="0" />
        <asp:HiddenField ID="hdnExpenseCount" runat="server" Value="0" />
        <asp:HiddenField ID="hdnGenericCount" runat="server" Value="0" />
        <asp:HiddenField ID="hdnUCRCount" runat="server" Value="0" />
        <asp:HiddenField ID="hdnAudioCount" runat="server" Value="0" />
        <asp:HiddenField ID="hdnVideoCount" runat="server" Value="0" />
        <asp:HiddenField ID="hdnAudioUrl" runat="server" Value="0" />
        <asp:HiddenField ID="hdn" runat="server" Value="0" />
        <asp:HiddenField ID="hdnComefrom" runat="server" Value="0" />
        <div class="tabs">
            <div id="container">
                <div class="tab">
                    <input type="radio" id="tab-1" name="tab-group-1" checked />
                    <asp:LinkButton ID="lnkPreInventory" runat="server" Style="display: block; width: 160px;
                        height: 25px; background: #e1eef5; text-align: center; padding: 2px; border-radius: 5px;
                        color: #21618C; margin-right: 2px" OnClientClick="return ShowHideImages(this);">PreInventory Images</asp:LinkButton>
                    <div id="divInventory" runat="server" class="content">
                        <div id="gallery1" class="ad-gallery" runat="server">
                            <div id="Div1" class="ad-gallery" runat="server">
                                <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                    <span id="Span3" runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif;
                                        font-size: 12pt; font-weight: normal; text-decoration: none;">Inventory Images are
                                        not available.</span>
                                    <div style="width: 60%; float: left; text-align: center; padding: 100px; font-size: 14pt">
                                        <asp:ImageButton ID="ibtncars" runat="server" ImageUrl="~/Images/nocarthumb.PNG"
                                            Width="300px" Height="250px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="gallery" class="ad-gallery" runat="server">
                            <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                <span id="spHeader0" runat="server" style="color: #21618C; font-family: Arial,Helvetica,sans-serif;
                                    font-size: 12px; font-weight: normal; text-decoration: none;"></span><span id="spHeader1"
                                        runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                        font-weight: bold; text-decoration: none;"></span><span id="spHeader2" runat="server"
                                            style="color: #21618C; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                            font-weight: normal; text-decoration: none;"></span>
                            </div>
                            <div class="ad-image-wrapper">
                            </div>
                            <div class="ad-controls">
                            </div>
                            <asp:DataList ID="dlPreInventory" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <HeaderTemplate>
                                    <div class="ad-nav">
                                        <div class="ad-thumbs">
                                            <ul class="ad-thumb-list">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li><a href='<%# GetPreInventoryImagePath(Eval("ServerPath"))  %>'>
                                        <img src='<%# GetPreInventoryImagePath(Eval("ThumbNailPath")) %>' alt='<%# GetDescPreInventory(Eval("ImageID")) %>'
                                            class="image0">
                                    </a></li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul> </div> </div>
                                </FooterTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-2" name="tab-group-1">
                    <asp:LinkButton ID="lnkPreExpense" runat="server" Style="display: block; width: 120px;
                        height: 30px; background: #4E9CAF; padding: 5px; border-radius: 5px; color: white;
                        text-align: center; margin-right: 2px" OnClientClick="return ShowHideImages(this);">Expense Images</asp:LinkButton>
                    <div id="divPreExpense" runat="server" class="content">
                        <div id="PreExpensegallery1" class="ad-gallery" runat="server" visible="false">
                            <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                <span id="Span6" runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif;
                                    font-size: 12pt; font-weight: normal; text-decoration: none;">Expense Images are
                                    not available.</span>
                                <div style="width: 60%; float: left; text-align: center; padding: 100px; font-size: 14pt">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/nocarthumb.PNG"
                                        Width="300px" Height="250px" />
                                </div>
                            </div>
                        </div>
                        <div id="PreExpensegallery" class="ad-gallery" runat="server">
                            <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                <span id="ExpSpan0" runat="server" style="color: #21618C; font-family: Arial,Helvetica,sans-serif;
                                    font-size: 12px; font-weight: normal; text-decoration: none;"></span><span id="ExpSpan1"
                                        runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                        font-weight: bold; text-decoration: none;"></span><span id="ExpSpan2" runat="server"
                                            style="color: #21618C; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                            font-weight: normal; text-decoration: none;"></span>
                            </div>
                            <div class="ad-image-wrapper">
                            </div>
                            <div class="ad-controls">
                            </div>
                            <asp:Panel ID="pnlPreExpense" runat="server">
                                <asp:DataList ID="dlPreExpense" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <HeaderTemplate>
                                        <div class="ad-nav">
                                            <div class="ad-thumbs">
                                                <ul class="ad-thumb-list">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li><a href='<%# GetPreExpenseImagePath(Eval("ServerPath"))  %>'>
                                            <img src='<%# GetPreExpenseImagePath(Eval("ThumbNailPath")) %>' alt='<%# GetDescPreExpense(Eval("ImageID")) %>'
                                                class="image0">
                                        </a></li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul> </div> </div>
                                    </FooterTemplate>
                                </asp:DataList>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-3" name="tab-group-1">
                    <asp:LinkButton ID="lnkGeneric" runat="server" Style="display: block; width: 110px;
                        height: 25px; background: #4E9CAF; text-align: center; padding: 5px; border-radius: 5px;
                        color: white;" OnClientClick="return ShowHideImages(this);">Generic Images</asp:LinkButton>
                    <div id="divGeneric" runat="server" class="content">
                        <div id="Genericgallery1" class="ad-gallery" runat="server">
                            <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                <span id="Span9" runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif;
                                    font-size: 12pt; font-weight: normal; text-decoration: none;">Generic Images are
                                    not available.</span>
                                <div style="width: 60%; float: left; text-align: center; padding: 100px; font-size: 14pt">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/nocarthumb.PNG"
                                        Width="300px" Height="250px" />
                                </div>
                            </div>
                        </div>
                        <div id="Genericgallery" class="ad-gallery" runat="server">
                            <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                <span id="SpanGen0" runat="server" style="color: #21618C; font-family: Arial,Helvetica,sans-serif;
                                    font-size: 12px; font-weight: normal; text-decoration: none;"></span><span id="SpanGen1"
                                        runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                        font-weight: bold; text-decoration: none;"></span><span id="SpanGen2" runat="server"
                                            style="color: #21618C; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                            font-weight: normal; text-decoration: none;"></span>
                            </div>
                            <div class="ad-image-wrapper">
                            </div>
                            <div class="ad-controls">
                            </div>
                            <asp:Panel ID="pnlGeneric" runat="server">
                                <asp:DataList ID="dlGeneric" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <HeaderTemplate>
                                        <div class="ad-nav">
                                            <div class="ad-thumbs" style="width: 98%">
                                                <ul style="width: 98%">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li><a href='<%# GetImagePath(Eval("ServerPath"))  %>'>
                                            <img src='<%# GetImagePath(Eval("ThumbNailPath")) %>' alt='<%# GetDesc(Eval("ImageID")) %>'
                                                class="image0">
                                        </a></li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul> </div> </div>
                                    </FooterTemplate>
                                </asp:DataList>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="tab">
                    <input type="radio" id="tab-4" name="tab-group-1">
                    <asp:LinkButton ID="lnkUCR" runat="server" Style="display: block; width: 110px; height: 25px;
                        background: #4E9CAF; text-align: center; padding: 5px; margin-left: 2px; border-radius: 5px;
                        color: white;" OnClientClick="return ShowHideImages(this);">UCR Assets</asp:LinkButton>
                    <div id="divUCRgallery" runat="server" class="content" style="background-color: Gray">
                        <div id="UCRgallery" runat="server" style="padding-top: 5px">
                            <div class="tab">
                                <input type="radio" id="Radio2" name="tab-group-1">
                                <asp:LinkButton ID="lnkUCRImage" runat="server" Style="display: block; width: 90px;
                                    height: 15px; background: #4E9CAF; text-align: center; padding: 5px; margin-left: 2px;
                                    border-radius: 5px; color: white;" OnClientClick="return ShowHideUCRTabs(this);">UCR Images</asp:LinkButton>
                                <div id="divUCRImages" runat="server" class="content">
                                    <div id="UCRImagegallery" class="ad-gallery" runat="server" style="height: 535px;
                                        display: none">
                                        <div style="margin-bottom: 5px; margin-top: 13px; padding-top: 10px; padding-left: 30px">
                                            <span id="Span1" runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif;
                                                font-size: 12pt; font-weight: normal; text-decoration: none;">UCR Images are not
                                                available.</span>
                                            <div style="width: 60%; float: left; text-align: center; padding: 100px; font-size: 14pt">
                                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/nocarthumb.PNG"
                                                    Width="300px" Height="250px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div id="UCRgalleryThumb" class="ad-gallery" runat="server">
                                        <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                            <span id="UCRSpan0" runat="server" style="color: #21618C; font-family: Arial,Helvetica,sans-serif;
                                                font-size: 12px; font-weight: normal; text-decoration: none;"></span><span id="UCRSpan1"
                                                    runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                                    font-weight: bold; text-decoration: none;"></span><span id="UCRSpan2" runat="server"
                                                        style="color: #21618C; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                                        font-weight: normal; text-decoration: none;"></span>
                                        </div>
                                        <div class="ad-image-wrapper">
                                        </div>
                                        <div class="ad-controls">
                                        </div>
                                        <asp:Panel ID="pnlUCR" runat="server">
                                            <asp:DataList ID="dlUCRImages" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <HeaderTemplate>
                                                    <div class="ad-nav">
                                                        <div class="ad-thumbs">
                                                            <ul class="ad-thumb-list">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <li><a href='<%# GetImagePathUCR(Eval("BigPath"))  %>'>
                                                        <img src='<%# GetImagePathUCRThumb(Eval("ThumbNailPath")) %>' alt='<%# GetDescUCR(Eval("UCRMediaDetailID")) %>'
                                                            class="image0">
                                                    </a></li>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </ul> </div> </div>
                                                </FooterTemplate>
                                            </asp:DataList>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="tab">
                                <input type="radio" id="Radio3" name="tab-group-1">
                                <asp:LinkButton ID="lnkUCRAudio" runat="server" Style="display: block; width: 90px;
                                    height: 15px; background: #4E9CAF; text-align: center; padding: 5px; margin-left: 2px;
                                    border-radius: 5px; color: white;" OnClientClick="return ShowHideUCRTabs(this);">UCR Audio</asp:LinkButton>
                                <div id="divUCRAudio" runat="server" class="content" style="display: none">
                                    <div id="Audiogallery" class="ad-gallery" runat="server" style="height: 535px; display: none">
                                        <div style="margin-bottom: 5px; margin-top: 13px; padding-left: 30px; padding-top: 5px;">
                                            <span id="Span10" runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif;
                                                font-size: 12pt; font-weight: normal; text-decoration: none;">UCR Audio are not
                                                available.</span>
                                            <div style="width: 60%; float: left; text-align: center; padding: 100px; font-size: 14pt">
                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/audio-thumb.PNG"
                                                    Width="300px" Height="250px" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="upAudio" runat="server">
                                        <ContentTemplate>
                                            <div id="UCRAudioThumb" class="ad-gallery" runat="server" style="width: 100%; height: 530px;
                                                margin-bottom: 10px; padding-top: 10px; margin-top: 13px; position: relative;
                                                overflow: hidden;">
                                                <div style="text-align: left; padding: 30px 10px 5px 10px; vertical-align: middle;
                                                    float: left; width: 80%;">
                                                    <audio id="myAudio" runat="server" autoplay="autoplay" controls preload="auto">  
                                  
                                           </audio>
                                                    <%--   <embed id="EMbadeAudio" width="400" height="100" autostart="true" src="" enablejavascript="true" />--%>
                                                </div>
                                                <div style="display: block; width: 98%; float: left; overflow: auto; padding: 0px 10px 5px 70px;
                                                    text-align: center">
                                                    <asp:DataList ID="dlAudioList" RepeatDirection="Horizontal" runat="server" DataKeyField="AudioVideoURL">
                                                        <ItemTemplate>
                                                            <div style="float: left; width: auto;">
                                                                <asp:ImageButton ID="imgBtnImage" runat="server" ImageUrl="../Images/audio-img.PNG"
                                                                    ToolTip='<%#Eval("AudioVideoUrl") %>' CommandArgument='<%#Eval("AudioVideoUrl") %>'
                                                                    OnClick="imgBtnImageAudio_Click" Style="padding-right: 5px" /><br />
                                                                <%--OnClientClick="return SetAudioURL(this);"--%>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:DataList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="tab">
                                <input type="radio" id="Radio4" name="tab-group-1">
                                <asp:LinkButton ID="lnkUCRVideo" runat="server" Style="display: block; width: 90px;
                                    height: 15px; background: #4E9CAF; text-align: center; padding: 5px; margin-left: 2px;
                                    border-radius: 5px; color: white;" OnClientClick="return ShowHideUCRTabs(this);">UCR Video</asp:LinkButton>
                                <div id="divUCRVideo" runat="server" class="content">
                                    <div id="Videogallery" class="ad-gallery" runat="server" style="height: 535px; display: none">
                                        <div style="margin-bottom: 5px; padding-top: 5px; margin-top: 13px; padding-left: 30px">
                                            <span id="Span14" runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif;
                                                font-size: 12pt; font-weight: normal; text-decoration: none;">UCR Video are not
                                                available.</span>
                                            <div style="width: 60%; float: left; text-align: center; padding: 100px; font-size: 14pt">
                                                <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/video-thumb.PNG"
                                                    Width="300px" Height="250px" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="upVideo" runat="server">
                                        <ContentTemplate>
                                            <div id="UCRVideoThumb" class="ad-gallery" runat="server" style="width: 100%; height: 530px;
                                                margin-bottom: 10px; padding-top: 10px; margin-top: 13px; position: relative;
                                                overflow: hidden;">
                                                <div style="text-align: left; padding: 30px 10px 5px 10px; vertical-align: middle;
                                                    float: left; width: 80%;">
                                                    <video id="myVideo" runat="server" visible="false" class="video-js vjs-default-skin"
                                                        controls preload="auto" autoplay="autoplay" width="600px" height="400px" data-setup="{}">
                                                        <source type="" src=""></source>
                                                    </video>
                                                    <%-- <video id="myVideo1" runat="server" class="video-js vjs-default-skin" controls preload="auto"
                                                        autoplay="autoplay" width="600px" height="400px" data-setup="{}">
                                                    <source type="" src=""></source>--%>
                                                    </video>
                                                </div>
                                                <div style="display: block; width: 98%; float: left; overflow: auto; padding: 0px 10px 5px 70px;
                                                    text-align: center">
                                                    <asp:DataList ID="dlVideoList" RepeatDirection="Horizontal" runat="server" DataKeyField="AudioVideoURL">
                                                        <ItemTemplate>
                                                            <div style="float: left; width: auto;">
                                                                <asp:ImageButton ID="imgBtnImage" runat="server" ImageUrl="../Images/video.png" ToolTip='<%#Eval("AudioVideoUrl") %>'
                                                                    CommandArgument='<%#Eval("AudioVideoUrl") %>' OnClick="imgBtnImageVideo_Click"
                                                                    Style="padding-right: 5px" /><br />
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:DataList>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab">
                    <input type="radio" id="Radio1" name="tab-group-1">
                    <asp:LinkButton ID="lnkViewAllImages" runat="server" Style="display: block; width: 110px;
                        height: 25px; background: #4E9CAF; text-align: center; padding: 5px; margin-left: 2px;
                        border-radius: 5px; color: white;" OnClientClick="return ShowHideImages(this);">All Images</asp:LinkButton>
                    <div id="divAllIMageGallery" runat="server" class="content">
                        <div id="AllImageGallery" class="ad-gallery" runat="server" style="display: none">
                            <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                <span id="Span2" runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif;
                                    font-size: 12pt; font-weight: normal; text-decoration: none;">All Images are not
                                    available.</span>
                                <div style="width: 60%; float: left; text-align: center; padding: 100px; font-size: 14pt">
                                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/nocarthumb.PNG"
                                        Width="300px" Height="250px" />
                                </div>
                            </div>
                        </div>
                        <div id="divAllImageThumb" class="ad-gallery" runat="server">
                            <div style="margin-bottom: 5px; padding-top: 10px; padding-left: 30px">
                                <span id="SpanAll0" runat="server" style="color: #21618C; font-family: Arial,Helvetica,sans-serif;
                                    font-size: 12px; font-weight: normal; text-decoration: none;"></span><span id="SpanAll1"
                                        runat="server" style="color: red; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                        font-weight: bold; text-decoration: none;"></span><span id="SpanAll2" runat="server"
                                            style="color: #21618C; font-family: Arial,Helvetica,sans-serif; font-size: 12px;
                                            font-weight: normal; text-decoration: none;"></span>
                            </div>
                            <div class="ad-image-wrapper">
                            </div>
                            <div class="ad-controls">
                            </div>
                            <asp:Panel ID="pnlAllImages" runat="server">
                                <asp:DataList ID="dlAllImages" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <HeaderTemplate>
                                        <div class="ad-nav">
                                            <div class="ad-thumbs">
                                                <ul class="ad-thumb-list">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li><a href='<%# GetAllImagePath(Eval("ServerPath")+","+Eval("ImageID")+","+Eval("ImageType"))  %>'>
                                            <img src='<%# GetAllImagePath(Eval("ThumbNailPath")+","+Eval("ImageID")+","+Eval("ImageType")) %>'
                                                alt='<%# GetAllImageDesc(Eval("ImageID")+","+Eval("ImageType")) %>' width="100"
                                                height="60" class="image0">
                                        </a></li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul> </div> </div>
                                    </FooterTemplate>
                                </asp:DataList>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div style="width: 8px; float: right; padding-top: 6px; text-align: right">
                    <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" onclick="parent.HideModelpopup();" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
