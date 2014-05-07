﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageGallery.aspx.cs" Inherits="METAOPTION.UI.ImageGallery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <link rel="stylesheet" type="text/css" href="../CSS/jquery.ad-gallery.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="../CSS/jquery.ad-gallery.js"></script>
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
    <style type="text/css">
        *
        {
            font-family: "Lucida Grande" , "Lucida Sans Unicode" , "Lucida Sans" , Verdana, Arial, sans-serif;
            color: #21618C;
            line-height: 140%;
        }
        select, input, textarea
        {
            font-size: 1em;
        }
        body
        {
            padding: 0;
            font-size: 70%;
            width: 800px;
        }
        h2
        {
            margin-top: 1.2em;
            margin-bottom: 0;
            padding: 0;
            border-bottom: 1px dotted #dedede;
        }
        h3
        {
            margin-top: 1.2em;
            margin-bottom: 0;
            padding: 0;
        }
        .example
        {
            border: 1px solid #CCC;
            background: #f2f2f2;
            padding: 10px;
        }
        ul
        {
            list-style-image: url(list-style.gif);
        }
        pre
        {
            font-family: "Lucida Console" , "Courier New" , Verdana;
            border: 1px solid #CCC;
            background: #f2f2f2;
            padding: 10px;
        }
        code
        {
            font-family: "Lucida Console" , "Courier New" , Verdana;
            margin: 0;
            padding: 0;
        }
        
        #gallery
        {
            padding: 30px;
            background: #e1eef5;
        }
        #descriptions
        {
            position: relative;
            height: 50px;
            background: #EEE;
            margin-top: 10px;
            width: 640px;
            padding: 10px;
            overflow: hidden;
        }
        #descriptions .ad-image-description
        {
            position: absolute;
        }
        #descriptions .ad-image-description .ad-description-title
        {
            display: block;
        }
    </style>
    <title>HeadStart VMS :: PreInventory</title>
</head>
<body>
    <div id="container">
        <div id="gallery" class="ad-gallery">
            <div style="margin-bottom: 5px;">
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
            <asp:DataList ID="dlthumb" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <HeaderTemplate>
                    <div class="ad-nav">
                        <div class="ad-thumbs">
                            <ul class="ad-thumb-list">
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
        </div>
    </div>
</body>
</html>
