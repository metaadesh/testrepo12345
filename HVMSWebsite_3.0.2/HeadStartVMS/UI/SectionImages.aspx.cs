using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.IO;
using METAOPTION;

public partial class SectionImages : System.Web.UI.Page
{
    UCRBAL objUCRBAL = new UCRBAL();
    int type;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            Int64 UCRID;
            if (Request.QueryString["UCRID"] != null)
            {
                UCRID = Convert.ToInt64(Request.QueryString["UCRID"]);
                AudioVideosCount(Convert.ToInt32(UCRID));
                ImagesCount(Convert.ToInt32(UCRID));
                BindDataList(1, UCRID);
            }
        }
    }

    #region Event fire on Type button click to bind right side lookup type

    protected void lnkPhotos_Click(object sender, EventArgs e)
    {
        BindDataList(1, Convert.ToInt64(Request.QueryString["UCRID"]));
    }
    protected void lnkVideos_Click(object sender, EventArgs e)
    {
        BindDataList(2, Convert.ToInt64(Request.QueryString["UCRID"]));
    }
    #endregion



    #region Count images videos and display in list

    private void AudioVideosCount(Int32 UCRId)
    {
        Int32 audioVideoCount = objUCRBAL.GetUCRAudioVideoCount(UCRId);
        lnkVideos.Text = String.Format("Videos & Audio ({0})", audioVideoCount);
    }
    private void ImagesCount(Int32 UCRId)
    {
        Int32 imageCount = objUCRBAL.GetUCRImagesCount(UCRId);
        lnkPhotos.Text = String.Format("Photos ({0})", imageCount);
    }
    #endregion

    #region[BindDataList() method]
    // ******************************************************************************* 
    //      Bind list of Image, Audio, Videos and Note section we are passing Type,
    //      InventoryId, LookupTypeId for these items 1 for Images 2 for Audio & Vidio 
    //      and 3 for Notes and also passing 0 to show All Lookup Types
    //********************************************************************************

    private void BindDataList(int type, long UCRId)
    {
        container.Visible = false;
        containerVideo.Visible = false;
        string videoPath = string.Empty;

        List<UCRAudioVideoByUCRIdResult> videos;
        videos = objUCRBAL.GetAudioVideoByUCRID(UCRId);
        if (videos.Count > 0)
            lnkVideos.Enabled = true;
        else
            lnkVideos.Enabled = false;

        if (type == 1)
        {
            #region[Get Images for Image section]
            List<UCRImageDisplaybyUCRIdResult> images = objUCRBAL.GetUCRImagesByUCRID(UCRId);
            DataTable dtUCRImages = new DataTable();
            dtUCRImages.Columns.Add("UCRID");
            dtUCRImages.Columns.Add("UCRMediaDetailID");
            dtUCRImages.Columns.Add("ThumbNailPath");
            dtUCRImages.Columns.Add("MediumPath");
            dtUCRImages.Columns.Add("BigPath");
            dtUCRImages.Columns.Add("AudioVideoURL");
            DataRow dr;
            foreach (UCRImageDisplaybyUCRIdResult p in images)
            {
                dr = dtUCRImages.NewRow();
                dr["UCRID"] = p.UCRID;
                dr["UCRMediaDetailID"] = p.UCRMediaDetailID;
                if (p.ThumbNailPath.Contains("http"))
                    dr["ThumbNailPath"] = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(p.ThumbNailPath);
                else
                    dr["ThumbNailPath"] = "../Images/nocarthumb.PNG";
                if (p.MediumPath.Contains("http"))
                    dr["MediumPath"] = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(p.MediumPath);
                else
                    dr["MediumPath"] = "../Images/nocarMedium.png";
                if (p.BigPath.Contains("http"))
                    dr["BigPath"] = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(p.BigPath);
                else
                    dr["BigPath"] = "../Images/nocar.PNG";
                dr["AudioVideoURL"] = p.AudioVideoURL;

                dtUCRImages.Rows.Add(dr);
                dtUCRImages.AcceptChanges();
            }

            if (images != null && images.Count > 0)
            {
                if (images[0].BigPath.Contains("http"))
                    ancDefault.HRef = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(images[0].BigPath.ToString());
                else
                    ancDefault.HRef = "../Images/nocarMedium.png";
                if (images[0].MediumPath.Contains("http"))
                    imgDefault.Src = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(images[0].MediumPath.ToString());
                else
                    imgDefault.Src = "../Images/nocarMedium.png";
            }

            dlUCRImages.DataSource = dtUCRImages;

            dlUCRImages.DataBind();
            containerVideo.Visible = false;
            container.Visible = true;
        }
        else if (type == 2)
        {
            //string videoPath = string.Empty;

            // List<UCRAudioVideoByUCRIdResult> videos;

            // videos = objUCRBAL.GetAudioVideoByUCRID(UCRId);

          
            dlVideosAudio.DataSource = videos;

            foreach (UCRAudioVideoByUCRIdResult item in videos)
            {
                videoPath = item.AudioVideoURL.ToString();
                string str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRVideoPhysicalPath(videoPath);
                String extn = str.Substring(str.LastIndexOf("."));
                switch (extn.ToUpper())
                {
                    case ".CAF":
                    case ".WAV":
                        myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                        myAudio.Visible = true;
                        myVideo.Visible = false;
                        break;
                    case ".MP3":
                        myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                        myAudio.Visible = true;
                        myVideo.Visible = false;
                        break;
                    case ".WMV":
                    case ".MP4":
                        myVideo.InnerHtml = String.Format("<source src='{0}' AddType='video/mp4' codecs=mp4, speex>Your browser does not support the <code>video</code> element.", str);
                        myAudio.Visible = false;
                        myVideo.Visible = true;
                        break;
                    case ".FLV":
                    //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/flv'>", videoPath);
                    //myAudio.Visible = false;
                    //myVideo.Visible = true;
                    //break;
                    case ".OGG":
                        myVideo.InnerHtml = String.Format("<source src='{0}' type='video/ogg'>", str);
                        myAudio.Visible = false;
                        myVideo.Visible = true;
                        break;

                }
                break;
            }
            dlVideosAudio.DataBind();
            containerVideo.Visible = true;
            container.Visible = false;


        }

    }
            #endregion

    protected void imgBtnImage_Click(object sender, EventArgs e)
    {
        string videoPath = ((ImageButton)sender).CommandArgument;
        string str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRVideoPhysicalPath(videoPath);
        String extn = str.Substring(str.LastIndexOf("."));
        switch (extn.ToUpper())
        {
            case ".CAF":
            case ".WAV":
                myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                myAudio.Visible = true;
                myVideo.Visible = false;
                break;
            case ".MP3":
                myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                myAudio.Visible = true;
                myVideo.Visible = false;
                break;
            case ".WMV":
            case ".OGG":
            case ".FLV":
            //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/ogg'>", videoPath);
            //myAudio.Visible = false;
            //myVideo.Visible = true;
            //break;
            case ".MP4":
                myVideo.InnerHtml = String.Format("<source src='{0}' type='video/mp4' codecs=dirac, speex>Your browser does not support the <code>video</code> element.", str);
                myAudio.Visible = false;
                myVideo.Visible = true;
                break;

        }
    }



    #region Change type button color on selection

    //********************************************
    //  On the basis of button click it will apply 
    //  the css class to show button highlighted
    //********************************************

    private void changeButtonColor(string btnId)
    {
        lnkPhotos.CssClass = "btnVdieoImages";
        lnkVideos.CssClass = "btnVdieoImages";


        if (btnId == "lnkPhotos")
            lnkPhotos.CssClass = "btnVideoImagesActive";
        else if (btnId == "lnkVideos")
            lnkVideos.CssClass = "btnVideoImagesActive";

    }
    #endregion
    #endregion
}