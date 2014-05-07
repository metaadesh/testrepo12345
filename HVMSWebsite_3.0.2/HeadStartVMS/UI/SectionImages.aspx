<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="SectionImages" CodeBehind="SectionImages.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Slide Show</title>
    <link href="../CSS/UCRZoomer.css" rel="stylesheet" type="text/css" />
    <%-- <link rel="stylesheet" href="/Themes/theme1/jquery.ad-gallery.css" type="text/css" />
    <link rel="stylesheet" href="/Themes/Theme2/jquery.jqzoom.css" type="text/css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="../CSS/jquery.ad-gallery.js"></script>
    <script src="../CSS/jquery.UCRjqzoom-core.js" type="text/javascript"></script>
    <link href="../CSS/jquery.UCRjqzoom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            font-family: Arial;
        }
        a img, :link img, :visited img
        {
            border: none;
        }
        table
        {
            border-collapse: collapse;
            border-spacing: 0;
        }
        :focus
        {
            outline: none;
        }
        *
        {
            margin: 0;
            padding: 0;
        }
        p, blockquote, dd, dt
        {
            margin: 0 0 8px 0;
            line-height: 1.5em;
        }
        fieldset
        {
            padding: 0px;
            padding-left: 7px;
            padding-right: 7px;
            padding-bottom: 7px;
        }
        fieldset legend
        {
            margin-left: 15px;
            padding-left: 3px;
            padding-right: 3px;
            color: #333;
        }
        dl dd
        {
            margin: 0px;
        }
        dl dt
        {
        }
        
        .clearfix:after
        {
            clear: both;
            content: ".";
            display: block;
            font-size: 0;
            height: 0;
            line-height: 0;
            visibility: hidden;
        }
        .clearfix
        {
            display: block;
            zoom: 1;
        }
        
        
        ul#thumblist
        {
            display: block;
        }
        ul#thumblist li
        {
            float: left;
            margin-right: 2px;
            list-style: none;
        }
        ul#thumblist li a
        {
            display: block;
            border: 1px solid #CCC;
        }
        ul#thumblist li a.zoomThumbActive
        {
            border: 1px solid red;
        }
        
        .jqzoom
        {
            text-decoration: none;
            float: left;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.jqzoom').jqzoom({
                zoomType: 'standard',
                lens: true,
                preloadImages: false,
                alwaysOn: false
            });

        });


        
    </script>
    <script type="text/javascript">
        $(function () {
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
        });
    </script>
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
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; padding: 5px;">
        <div style="width: 400px; float: left; padding: 2px 0px 2px 10px">
            <div class="vidoestyle">
                <asp:Button ID="lnkPhotos" runat="server" Text="Photos" CssClass="btnVideoImagesActive"
                    OnClick="lnkPhotos_Click" Height="30px" CausesValidation="false" />
                <asp:Button ID="lnkVideos" runat="server" CausesValidation="false" Text="Videos & Audio"
                    CssClass="btnVdieoImages" OnClick="lnkVideos_Click" Height="30px" />
            </div>
        </div>
        <div style="width: 40px; float: right; text-align: right; padding: 0px 10px 0px 0px">
            <img src="../Images/close_icon.gif" alt="close" id="imgCancel" runat="server" onclick="return parent.HideModelpopup();" />
        </div>
    </div>
    <div style="width: 100%; padding-left: 5px;">
        <div id="container" runat="server" visible="false">
            <div id="gallery" class="ad-gallery">
                <asp:HiddenField ID="hfDefaultBig" runat="server" Value="" />
                <div class="clearfix">
                    <a href="../Images/ExpandImage.gif" class="jqzoom" rel='gal1' title="" id="ancDefault"
                        runat="server">
                        <img src="../Images/ExpandImage.gif" title="" id="imgDefault" runat="server" alt="" />
                    </a>
                </div>
                <div style="text-align: right; margin-top: -30px; margin-left: 20px; position: absolute;
                    z-index: 999">
                    <a id="lnkZoom" href="#" target="_blank">
                        <img id="imgzoom" src="../Images/ExpandImage.gif" alt="" />
                    </a>
                </div>
                <div class="ad-thumbs" style="overflow: auto; width: 800px; padding: 5px 5px 5px 20px">
                    <ul id="thumblist">
                        <asp:DataList ID="dlUCRImages" RepeatDirection="Horizontal" runat="server" EnableViewState="false">
                            <ItemTemplate>
                                <a id="athumb" href='javascript:void(0);' rel="{gallery: 'gal1', smallimage: '<%# string.Format("{0}", Eval("MediumPath")) %>',largeimage: '<%# string.Format("{0}", Eval("BigPath")) %>'}">
                                    <img src='<%# string.Format("{0}", Eval("ThumbNailPath")) %>' alt="img" />
                                </a>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" />
                        </asp:DataList>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div style="width: 100%; text-align: center">
        <div id="containerVideo" runat="server" visible="false">
            <div style="width: 99%; float: left; height: auto; margin-top:30px ">
                <%-- <embed id="testid" runat="server" type="application/x-mplayer2" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"
                    src="" width="300" height="290" showstatusbar="1" autostart="0"
                    showcontrols="1">
         </embed>--%>
                <div style="text-align: left; padding: 0px 10px 0px 10px; vertical-align: middle;
                    float: left; width: 80%;">
                    <video id="myVideo" runat="server" class="video-js vjs-default-skin" controls preload="auto"
                        autoplay="autoplay" width="600px" height="400px" data-setup="{}">
                       <source type="video/mp4" src=""></source>
                   </video>
                </div>
                <div style="text-align: left; padding: 0px 10px 5px 15px; vertical-align: middle;
                    float: left; width: 80%;">
                    <audio id="myAudio" runat="server" autoplay="autoplay" controls preload="auto"> 
                    </audio>
                </div>
            </div>
            <div style="display: block; width: 98%; float: left; overflow: auto; padding: 0px 10px 5px 15px;
                text-align: center">
                <asp:DataList ID="dlVideosAudio" RepeatDirection="Horizontal" runat="server" DataKeyField="AudioVideoURL">
                    <ItemTemplate>
                        <div style="float: left; width: auto;">
                            <asp:ImageButton ID="imgBtnImage" runat="server" ImageUrl='<%# Convert.ToString(Eval("MediaType")) == "VIDEO" ? "../Images/video.png" : "../Images/AUDIO.PNG" %>'
                                ToolTip='<%#Eval("AudioVideoURL") %>' CommandArgument='<%#Eval("AudioVideoURL") %>'
                                OnClick="imgBtnImage_Click" Style="padding-right: 5px" /><br />
                            <%-- <div style="text-align: center">
                            <%# string.Format("{0}",Eval("Text")) %></div>--%>
                        </div>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Top" />
                </asp:DataList>
            </div>
        </div>
        <div>
            <asp:HiddenField ID="hfType" runat="server" Value="0" />
            <asp:HiddenField ID="hfLookupType" runat="server" Value="0" />
            <asp:HiddenField ID="hfInventoryID" runat="server" Value="0" />
        </div>
    </div>
    </form>
    <script type="text/javascript">
        //        var athumb = document.getElementById("athumb");
        //       // alert(athumb);
        //        document.getElementById("ancDefault").href = "CarImages/1023006/Images/23-Celica.jpg";
        //        document.getElementById("imgDefault").src = "CarImages/1023006/Images/23-Celica_m.jpg";
        //        //CarImages/1023006/Images/23-Celica_m.jpg  
    </script>
</body>
</html>
