using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using METAOPTION.BAL;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using METAOPTION.UserControls;
using System.Net;

namespace METAOPTION.UI
{
    public partial class ViewAllImages : System.Web.UI.Page
    {
        #region [PreInventory]
        String ServerUrlPreInventory = System.Configuration.ConfigurationManager.AppSettings["MobileImagePath"];
        PreInventoryBAL PreInvBAL = new PreInventoryBAL();
        System.Collections.ArrayList arraylistPreInventory = new ArrayList();
        String Year, Make, Model, Body, Price, Mileage, UserName, DeviceID, DeviceName, VIN, SyncDate, Lat, Long, AutoCheck;
        #endregion

        #region [PreExpense]
        String ServerUrlPreEXpense = System.Configuration.ConfigurationManager.AppSettings["ExpenseImagePath"];
        PreExpenseBAL PreExpBAL = new PreExpenseBAL();
        System.Collections.ArrayList arraylistPreEXp = new ArrayList();
        String UserNamePreExpense, DeviceIDPreExpense, DeviceNamePreExpense, VINPreExpense, SyncDatePreExpense, CountPreExpense, DefaultPrice, TotalPrice, ExpenseType, LatPreExpense, LongPreExpense;
        #endregion

        #region [Generic]
        CommonBAL bal = new CommonBAL();
        System.Collections.ArrayList arraylistGen = new ArrayList();
        String UserNameGen, DeviceIDGen, DeviceNameGen, VINGen, SyncDateGen, LatGen, LongGen, ImageDetails;
        String ServerUrlGeneric = System.Configuration.ConfigurationManager.AppSettings["GenericImagePath"];

        #endregion

        #region [UCR]
        UCRBAL objUCR = new UCRBAL();
        System.Collections.ArrayList arraylistUCR = new ArrayList();
        System.Collections.ArrayList arraylistAudioVideo = new ArrayList();
        String UserNameUCR, DeviceIDUCR, DeviceNameUCR, VINUCR, SyncDateUCR, LatUCR, LongUCR;
        String strType, Location, CRID, CRStatusDesc, RunNo, LaneNo, CarFax;
        string ExtColor, InColor, ActionCode, AuctionText, SaleDate;

        #endregion

        #region[ViewAllImage]
        DataTable dtAllImage = new DataTable();
        DataTable dtAllimageValue = new DataTable();
        DataTable dtHeader = new DataTable();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["comeFrom"] != null)
                {
                    hdnComefrom.Value = Request.QueryString["comeFrom"];
                }
                CreateAllImageTable();
                BindAllImage();
            }
        }

        public void BindAllImage()
        {
            try
            {
                #region[Inventory]
                if (Request.QueryString.HasKeys())
                {
                    if (Convert.ToInt64(Request.QueryString["preid"]) > 0)
                    {
                        arraylistPreInventory = PreInvBAL.PreInventoryImages(Convert.ToInt64(Request.QueryString["preid"]));
                        if (((DataTable)arraylistPreInventory[0]).Rows.Count > 0)
                        {
                            hdnInventotryCount.Value = Convert.ToString(((DataTable)arraylistPreInventory[0]).Rows.Count);
                            lnkPreInventory.Text = "Inventory Images" + " " + "(" + Convert.ToString(((DataTable)arraylistPreInventory[0]).Rows.Count) + ")";
                            gallery1.Visible = false;
                            gallery.Visible = true;
                            dlPreInventory.DataSource = arraylistPreInventory[0];
                            dlPreInventory.DataBind();

                        }
                        else
                        {

                            lnkPreInventory.Text = "Inventory Images" + " " + "(0)";
                            gallery1.Visible = true;
                            gallery.Visible = false;
                        }
                    }
                    else
                    {

                        lnkPreInventory.Text = "Inventory Images" + " " + "(0)";
                        gallery1.Visible = true;
                        gallery.Visible = false;
                    }
                }
                #endregion

                #region[Expense]
                if (Request.QueryString.HasKeys())
                {
                    // Convert.ToInt64(Request.QueryString["id"])
                    if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["v"])))
                    {
                        arraylistPreEXp = PreExpBAL.PreExpenseImagesByInvId(Convert.ToString(Request.QueryString["v"]));//2086
                        if (((System.Data.DataTable)arraylistPreEXp[0]).Rows.Count > 0)
                        {
                            hdnExpenseCount.Value = Convert.ToString(((DataTable)arraylistPreEXp[0]).Rows.Count);
                            lnkPreExpense.Text = "Expense Images" + " " + "(" + Convert.ToString(((DataTable)arraylistPreEXp[0]).Rows.Count) + ")";
                            PreExpensegallery1.Visible = false;
                            PreExpensegallery.Visible = true;
                            dlPreExpense.DataSource = arraylistPreEXp[0];
                            dlPreExpense.DataBind();


                        }
                        else
                        {

                            lnkPreExpense.Text = "Expense Images" + " " + "(0)";
                            PreExpensegallery1.Visible = true;
                            PreExpensegallery.Visible = false;
                        }
                    }
                    else
                    {

                        lnkPreExpense.Text = "Expense Images" + " " + "(0)";
                        PreExpensegallery1.Visible = true;
                        PreExpensegallery.Visible = false;
                    }

                }
                #endregion

                #region[Generic]

                if (Request.QueryString.HasKeys())
                {
                    long ImageID = Convert.ToInt64(Request.QueryString["i"]);
                    String VIN = Request.QueryString["v"];
                    Int32 Type = Convert.ToInt32(Request.QueryString["t"]);
                    Int32 Period = Convert.ToInt32(Request.QueryString["p"]);
                    hfType.Value = Convert.ToString(Type);
                    //GenericImageGallery.aspx?i={0}&v={1}&t={2}&p={3}", -1, hfVIN.Value, 3, ImagesPeriod)
                    arraylistGen = bal.AllImages(-1, VIN, 3, 30);//()-1, "SALSK25469A206112", 3, 50

                    DataTable dtImageDetails = bal.GenericImageDetails(VIN);
                    if (dtImageDetails.Rows.Count > 0)
                        ImageDetails = Convert.ToString(dtImageDetails.Rows[0]["Year"]) + " " + Convert.ToString(dtImageDetails.Rows[0]["Make"]) + " " + Convert.ToString(dtImageDetails.Rows[0]["Model"]) + " " + Convert.ToString(dtImageDetails.Rows[0]["Body"]);
                    if (((System.Data.DataTable)arraylistGen[0]).Rows.Count > 0)
                    {
                        hdnGenericCount.Value = Convert.ToString(((DataTable)arraylistGen[0]).Rows.Count);
                        lnkGeneric.Text = "Generic Images" + " " + "(" + Convert.ToString(((DataTable)arraylistGen[0]).Rows.Count) + ")";
                        Genericgallery1.Visible = false;
                        Genericgallery.Visible = true;
                        dlGeneric.DataSource = arraylistGen[0];
                        dlGeneric.DataBind();
                    }
                    else
                    {

                        lnkGeneric.Text = "Generic Images" + " " + "(0)";
                        Genericgallery1.Visible = true;
                        Genericgallery.Visible = false;
                    }
                }

                #endregion

                #region[UCR]
                int UCRRowCount = 0;
                if (Request.QueryString.HasKeys())
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["id"])))
                    {
                        arraylistUCR = objUCR.UCRImagesByInvId(Convert.ToInt64(Request.QueryString["id"]));
                        UCRRowCount += ((System.Data.DataTable)arraylistUCR[0]).Rows.Count;
                        if (((System.Data.DataTable)arraylistUCR[0]).Rows.Count > 0)
                        {
                            hdnUCRCount.Value = Convert.ToString(((DataTable)arraylistUCR[0]).Rows.Count);
                            lnkUCRImage.Text = "UCR Images" + " " + "(" + Convert.ToString(((DataTable)arraylistUCR[0]).Rows.Count) + ")";
                            UCRgalleryThumb.Visible = true;
                            dlUCRImages.DataSource = arraylistUCR[0];
                            dlUCRImages.DataBind();
                        }
                        else
                        {

                            lnkUCRImage.Text = "UCR Images" + " " + "(0)";
                            UCRgallery.Visible = true;
                            UCRgalleryThumb.Visible = false;
                        }
                    }
                }

                #region[Audio and Video Region]
                if (Request.QueryString.HasKeys())
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["id"])))
                    {
                        arraylistAudioVideo = objUCR.UCRAudioVideoByInvId(Convert.ToInt64(Request.QueryString["id"]));
                        UCRRowCount += ((System.Data.DataTable)arraylistAudioVideo[0]).Rows.Count;
                        UCRRowCount += ((System.Data.DataTable)arraylistAudioVideo[1]).Rows.Count;
                        if (((System.Data.DataTable)arraylistAudioVideo[0]).Rows.Count > 0)
                        {
                            hdnAudioCount.Value = Convert.ToString(((DataTable)arraylistAudioVideo[0]).Rows.Count);
                            lnkUCRAudio.Text = "UCR Audio" + " " + "(" + Convert.ToString(((DataTable)arraylistAudioVideo[0]).Rows.Count) + ")";
                            //Audiogallery.Visible = false;
                            //UCRAudioThumb.Visible = true;
                            dlAudioList.DataSource = arraylistAudioVideo[0];
                            for (int i = 0; i < ((System.Data.DataTable)arraylistAudioVideo[0]).Rows.Count; i++)
                            {
                                string videoPath = string.Empty;
                                videoPath = ((System.Data.DataTable)arraylistAudioVideo[0]).Rows[0]["AudioVideoUrl"].ToString();
                                string str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRAudioPhysicalPath(videoPath.ToString());
                                hdnAudioUrl.Value = str;
                                String extn = str.Substring(videoPath.LastIndexOf("."));
                                switch (extn.ToUpper())
                                {
                                    case ".CAF":
                                    case ".WAV":
                                    case ".MP3":
                                        myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                                        myAudio.Visible = true;
                                        // myVideo.Visible = false;
                                        break;
                                    case ".WMV":
                                    case ".MP4":
                                    //myVideo.InnerHtml = String.Format("<source src='{0}' AddType='video/mp4' codecs=mp4, speex>Your browser does not support the <code>video</code> element.", videoPath);
                                    //myAudio.Visible = false;
                                    //myVideo.Visible = true;
                                    //break;
                                    case ".FLV":
                                    //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/flv'>", videoPath);
                                    //myAudio.Visible = false;
                                    //myVideo.Visible = true;
                                    //break;
                                    case ".OGG":
                                    //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/ogg'>", videoPath);
                                    // myAudio.Visible = false;
                                    //  myVideo.Visible = true;
                                    //  break;
                                    default:
                                        myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                                        myAudio.Visible = true;
                                        // myVideo.Visible = false;
                                        break;

                                }
                                break;
                            }
                            dlAudioList.DataBind();
                        }
                        else
                        {

                            lnkUCRAudio.Text = "UCR Audio" + " " + "(0)";
                            //Audiogallery.Visible = false;
                            //UCRAudioThumb.Visible = true;
                        }

                        if (((System.Data.DataTable)arraylistAudioVideo[1]).Rows.Count > 0)
                        {
                            hdnVideoCount.Value = Convert.ToString(((DataTable)arraylistAudioVideo[1]).Rows.Count);
                            lnkUCRVideo.Text = "UCR Video" + " " + "(" + Convert.ToString(((DataTable)arraylistAudioVideo[1]).Rows.Count) + ")";
                            //Videogallery.Visible = false;
                            //UCRVideoThumb.Visible = true;
                            dlVideoList.DataSource = arraylistAudioVideo[1];
                            for (int i = 0; i < ((System.Data.DataTable)arraylistAudioVideo[1]).Rows.Count; i++)
                            {
                                string videoPath = string.Empty;
                                videoPath = ((System.Data.DataTable)arraylistAudioVideo[1]).Rows[0]["AudioVideoUrl"].ToString();
                                string str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRVideoPhysicalPath(videoPath.ToString());
                                String extn = str.Substring(videoPath.LastIndexOf("."));
                                switch (extn.ToUpper())
                                {
                                    case ".CAF":
                                    case ".WAV":
                                    case ".MP3":
                                    //myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", videoPath);
                                    //myAudio.Visible = true;
                                    // myVideo.Visible = false;
                                    // break;
                                    case ".WMV":
                                    case ".MP4":
                                        myVideo.InnerHtml = String.Format("<source src='{0}' AddType='video/mp4' codecs=mp4, speex>Your browser does not support the <code>video</code> element.", str);
                                        // myAudio.Visible = false;
                                        myVideo.Visible = true;
                                        break;
                                    case ".FLV":
                                    //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/flv'>", videoPath);
                                    //myAudio.Visible = false;
                                    //myVideo.Visible = true;
                                    //break;
                                    case ".OGG":
                                    //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/ogg'>", videoPath);
                                    // myAudio.Visible = false;
                                    //  myVideo.Visible = true;
                                    //  break;
                                    default:
                                        myVideo.InnerHtml = String.Format("<source src='{0}' AddType='video/mp4' codecs=mp4, speex>Your browser does not support the <code>video</code> element.", str);
                                        // myAudio.Visible = false;
                                        myVideo.Visible = true;
                                        break;

                                }
                                break;
                            }
                            dlVideoList.DataBind();
                        }
                        else
                        {

                            lnkUCRVideo.Text = "UCR Video" + " " + "(0)";
                            //Videogallery.Visible = false;
                            //UCRVideoThumb.Visible = true;
                        }
                    }
                }


                #endregion

                lnkUCR.Text = "UCR Assets" + " " + "(" + UCRRowCount + ")";
                #endregion

                #region[All Images]

                #region [Create Image Data]
                DataRow dr;
                if (arraylistPreInventory.Count > 0)
                {
                    if (((DataTable)arraylistPreInventory[0]).Rows.Count > 0)
                    {
                        for (int i = 0; i < ((DataTable)arraylistPreInventory[0]).Rows.Count; i++)
                        {
                            dr = dtAllImage.NewRow();
                            dr["ImageID"] = ((DataTable)arraylistPreInventory[0]).Rows[i]["ImageID"];
                            dr["ServerPath"] = ((DataTable)arraylistPreInventory[0]).Rows[i]["ServerPath"];
                            dr["ThumbNailPath"] = ((DataTable)arraylistPreInventory[0]).Rows[i]["ThumbNailPath"];
                            dr["DateAdded"] = ((DataTable)arraylistPreInventory[0]).Rows[i]["DateAdded"];
                            dr["Latitude"] = ((DataTable)arraylistPreInventory[0]).Rows[i]["Latitude"];
                            dr["Longitude"] = ((DataTable)arraylistPreInventory[0]).Rows[i]["Longitude"];
                            dr["ImageType"] = "Inventory";
                            dtAllImage.Rows.Add(dr);
                        }

                    }
                }
                if (arraylistPreEXp.Count > 0)
                {
                    if (((System.Data.DataTable)arraylistPreEXp[0]).Rows.Count > 0)
                    {
                        for (int i = 0; i < ((DataTable)arraylistPreEXp[0]).Rows.Count; i++)
                        {
                            dr = dtAllImage.NewRow();
                            dr["ImageID"] = ((DataTable)arraylistPreEXp[0]).Rows[i]["ImageID"];
                            dr["ServerPath"] = ((DataTable)arraylistPreEXp[0]).Rows[i]["ServerPath"];
                            dr["ThumbNailPath"] = ((DataTable)arraylistPreEXp[0]).Rows[i]["ThumbNailPath"];
                            dr["DateAdded"] = ((DataTable)arraylistPreEXp[0]).Rows[i]["DateAdded"];
                            dr["Latitude"] = ((DataTable)arraylistPreEXp[0]).Rows[i]["Latitude"];
                            dr["Longitude"] = ((DataTable)arraylistPreEXp[0]).Rows[i]["Longitude"];
                            dr["ImageType"] = "Expense";
                            dtAllImage.Rows.Add(dr);
                        }
                    }
                }

                if (arraylistGen.Count > 0)
                {
                    if (((System.Data.DataTable)arraylistGen[0]).Rows.Count > 0)
                    {
                        for (int i = 0; i < ((DataTable)arraylistGen[0]).Rows.Count; i++)
                        {
                            dr = dtAllImage.NewRow();
                            dr["ImageID"] = ((DataTable)arraylistGen[0]).Rows[i]["ImageID"];
                            dr["ServerPath"] = ((DataTable)arraylistGen[0]).Rows[i]["ServerPath"];
                            dr["ThumbNailPath"] = ((DataTable)arraylistGen[0]).Rows[i]["ThumbNailPath"];
                            dr["DateAdded"] = ((DataTable)arraylistGen[0]).Rows[i]["DateAdded"];
                            dr["Latitude"] = ((DataTable)arraylistGen[0]).Rows[i]["Latitude"];
                            dr["Longitude"] = ((DataTable)arraylistGen[0]).Rows[i]["Longitude"];
                            dr["ImageType"] = "Generic";
                            dtAllImage.Rows.Add(dr);
                        }
                    }
                }

                if (arraylistUCR.Count > 0)
                {
                    if (((System.Data.DataTable)arraylistUCR[0]).Rows.Count > 0)
                    {
                        for (int i = 0; i < ((DataTable)arraylistUCR[0]).Rows.Count; i++)
                        {
                            dr = dtAllImage.NewRow();
                            dr["ImageID"] = ((DataTable)arraylistUCR[0]).Rows[i]["UCRMediaDetailID"];
                            dr["ServerPath"] = ((DataTable)arraylistUCR[0]).Rows[i]["BigPath"];
                            dr["ThumbNailPath"] = ((DataTable)arraylistUCR[0]).Rows[i]["ThumbNailPath"];
                            dr["DateAdded"] = ((DataTable)arraylistUCR[0]).Rows[i]["DateAdded"];
                            dr["ImageType"] = "UCR";
                            dr["MediumPath"] = ((DataTable)arraylistUCR[0]).Rows[i]["MediumPath"];
                            dtAllImage.Rows.Add(dr);
                        }
                    }
                }

                #endregion

                #region[Create DatalistData]
                DataRow drData;
                if (arraylistPreInventory.Count > 0)
                {
                    if (((DataTable)arraylistPreInventory[1]).Rows.Count > 0)
                    {
                        for (int i = 0; i < ((DataTable)arraylistPreInventory[1]).Rows.Count; i++)
                        {
                            drData = dtAllimageValue.NewRow();
                            drData["DeviceName"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["DeviceName"];
                            drData["DeviceId"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["DeviceID"];
                            drData["UserName"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["UserName"];
                            drData["Year"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["Year"];
                            drData["Make"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["Make"];
                            drData["Model"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["Model"];
                            drData["Body"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["Body"];
                            drData["Price"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["Price"];
                            drData["Mileage"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["MileageIn"];
                            drData["VIN"] = ((DataTable)arraylistPreInventory[1]).Rows[i]["VIN"];
                            drData["ImageType"] = "Inventory";
                            dtAllimageValue.Rows.Add(drData);
                        }

                    }
                }
                if (arraylistPreEXp.Count > 0)
                {
                    if (((System.Data.DataTable)arraylistPreEXp[1]).Rows.Count > 0)
                    {
                        for (int i = 0; i < ((DataTable)arraylistPreEXp[1]).Rows.Count; i++)
                        {
                            drData = dtAllimageValue.NewRow();
                            drData["DeviceName"] = ((DataTable)arraylistPreEXp[1]).Rows[i]["DeviceName"];
                            drData["DeviceId"] = ((DataTable)arraylistPreEXp[1]).Rows[i]["DeviceID"];
                            drData["UserName"] = ((DataTable)arraylistPreEXp[1]).Rows[i]["UserName"];
                            drData["ExpenseType"] = ((DataTable)arraylistPreEXp[1]).Rows[i]["ExpenseType"];
                            drData["Count"] = ((DataTable)arraylistPreEXp[1]).Rows[i]["Count"];
                            drData["DefaultPrice"] = ((DataTable)arraylistPreEXp[1]).Rows[i]["DefaultPrice"];
                            drData["TotalPrice"] = ((DataTable)arraylistPreEXp[1]).Rows[i]["TotalPrice"];
                            drData["VIN"] = ((DataTable)arraylistPreEXp[1]).Rows[i]["VIN"];
                            drData["ImageType"] = "Expense";
                            dtAllimageValue.Rows.Add(drData);
                        }
                    }
                }
                if (arraylistGen.Count > 0)
                {
                    if (((System.Data.DataTable)arraylistGen[0]).Rows.Count > 0)
                    {
                        for (int i = 0; i < ((DataTable)arraylistGen[0]).Rows.Count; i++)
                        {
                            drData = dtAllimageValue.NewRow();
                            drData["DeviceName"] = ((DataTable)arraylistGen[0]).Rows[i]["DeviceName"];
                            drData["DeviceId"] = ((DataTable)arraylistGen[0]).Rows[i]["DeviceID"];
                            drData["UserName"] = ((DataTable)arraylistGen[0]).Rows[i]["DisplayName"];
                            drData["DateAdded"] = ((DataTable)arraylistGen[0]).Rows[i]["DateAdded"];
                            drData["Longitude"] = ((DataTable)arraylistGen[0]).Rows[i]["Longitude"];
                            drData["latitude"] = ((DataTable)arraylistGen[0]).Rows[i]["latitude"];
                            drData["VIN"] = ((DataTable)arraylistGen[0]).Rows[i]["VIN"];
                            drData["ImageType"] = "Generic";
                            dtAllimageValue.Rows.Add(drData);
                        }
                    }
                }
                if (arraylistUCR.Count > 0)
                {
                    if (((System.Data.DataTable)arraylistUCR[1]).Rows.Count > 0)
                    {
                        for (int i = 0; i < ((DataTable)arraylistUCR[1]).Rows.Count; i++)
                        {
                            drData = dtAllimageValue.NewRow();
                            drData["Type"] = ((DataTable)arraylistUCR[1]).Rows[i]["Type"];
                            drData["Location"] = ((DataTable)arraylistUCR[1]).Rows[i]["Location"];
                            drData["UserName"] = ((DataTable)arraylistUCR[1]).Rows[i]["UserName"];
                            drData["Year"] = ((DataTable)arraylistUCR[1]).Rows[i]["Year"];
                            drData["Make"] = ((DataTable)arraylistUCR[1]).Rows[i]["Make"];
                            drData["Model"] = ((DataTable)arraylistUCR[1]).Rows[i]["Model"];
                            drData["Body"] = ((DataTable)arraylistUCR[1]).Rows[i]["Body"];
                            drData["Price"] = ((DataTable)arraylistUCR[1]).Rows[i]["MarketPrice"];
                            drData["Mileage"] = ((DataTable)arraylistUCR[1]).Rows[i]["Mileage"];
                            drData["CRId"] = ((DataTable)arraylistUCR[1]).Rows[i]["CR_ID"];
                            drData["CRStatusDesc"] = ((DataTable)arraylistUCR[1]).Rows[i]["CR_StatusDesc"];
                            drData["RunNo"] = ((DataTable)arraylistUCR[1]).Rows[i]["RunNumber"];
                            drData["LaneNo"] = ((DataTable)arraylistUCR[1]).Rows[i]["LaneNumber"];
                            drData["CarFax"] = ((DataTable)arraylistUCR[1]).Rows[i]["CarFax"];
                            drData["ExtColor"] = ((DataTable)arraylistUCR[1]).Rows[i]["ExteriorColor"];
                            drData["IntColor"] = ((DataTable)arraylistUCR[1]).Rows[i]["InteriorColor"];
                            drData["AuctionCode"] = ((DataTable)arraylistUCR[1]).Rows[i]["AuctionCode"];
                            drData["AuctionText"] = ((DataTable)arraylistUCR[1]).Rows[i]["AuctionStatusText"];
                            drData["SaleDate"] = ((DataTable)arraylistUCR[1]).Rows[i]["SaleDate"];
                            drData["ImageType"] = "UCR";
                            dtAllimageValue.Rows.Add(drData);
                        }
                    }
                }
                #endregion

                if (dtAllImage.Rows.Count > 0)
                {
                    lnkViewAllImages.Text = "All Images" + " " + "(" + dtAllImage.Rows.Count + ")";
                    AllImageGallery.Visible = false;
                    divAllImageThumb.Visible = true;
                    dlAllImages.DataSource = dtAllImage;
                    dlAllImages.DataBind();
                }
                else
                {

                    lnkViewAllImages.Text = "All Images" + " " + "(0)";
                    AllImageGallery.Visible = true;
                    divAllImageThumb.Visible = false;
                }

                #endregion

                #region[Set Header for All Image]
                dtHeader = objUCR.GetYMMBByVIN(Convert.ToString(Request.QueryString["v"]));
                if (dtHeader.Rows.Count >= 0)
                {
                    SpanAll0.InnerText = dtHeader.Rows[0]["VIN"] + " ";
                    SpanAll1.InnerText = String.Format(" {0} {1} {2}", dtHeader.Rows[0]["Year"], dtHeader.Rows[0]["Make"], dtHeader.Rows[0]["Model"]);
                    SpanAll2.InnerText = String.Format(" {0} ", dtHeader.Rows[0]["Body"]);

                }
                #endregion
            }
            catch (Exception ex)
            { }
        }

        #region [PreInventory Image]
        public String GetPreInventoryImagePath(object Path)
        {
            return String.Format("{0}{1}", ServerUrlPreInventory, Path);
        }

        public String GetDescPreInventory(object ImageID)
        {
            try
            {
                DataTable dtImg = new DataTable();
                dtImg = (DataTable)arraylistPreInventory[0];

                DataTable dtInv = new DataTable();
                dtInv = (DataTable)arraylistPreInventory[1];

                DataRow[] Imgrow = dtImg.Select(String.Format("ImageID={0}", ImageID));
                SyncDate = Convert.ToString(Imgrow[0]["DateAdded"]);
                Lat = Convert.ToString(Imgrow[0]["Latitude"]);
                Long = Convert.ToString(Imgrow[0]["Longitude"]);

                DataRow[] Invrow = dtInv.Select();
                DeviceName = Convert.ToString(Invrow[0]["DeviceName"]);
                DeviceID = Convert.ToString(Invrow[0]["DeviceID"]);
                UserName = Convert.ToString(Invrow[0]["UserName"]);
                Year = Convert.ToString(Invrow[0]["Year"]);
                Make = Convert.ToString(Invrow[0]["Make"]);
                Model = Convert.ToString(Invrow[0]["Model"]);
                Body = Convert.ToString(Invrow[0]["Body"]);
                Price = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["Price"]));
                Mileage = String.Format("{0:#,###}", Convert.ToDecimal(Invrow[0]["MileageIn"]));

                VIN = Convert.ToString(Invrow[0]["VIN"]);
                spHeader0.InnerText = VIN;
                spHeader1.InnerText = String.Format(" {0} {1} {2}", Year, Make, Model);
                spHeader2.InnerText = String.Format(" {0}  {1}  {2}", Body, Price, Mileage);
                return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Type: {4} | Location: {5}", DeviceName, DeviceID, UserName, SyncDate, Lat, Long);
            }

            catch (Exception ex)
            { return ""; }
        }

        #endregion

        #region [PreExpense Images]
        public String GetPreExpenseImagePath(object Path)
        {
            return String.Format("{0}{1}", ServerUrlPreEXpense, Path);
        }

        public String GetDescPreExpense(object ImageID)
        {
            try
            {
                DataTable dtImg = new DataTable();
                dtImg = (DataTable)arraylistPreEXp[0];

                DataTable dtInv = new DataTable();
                dtInv = (DataTable)arraylistPreEXp[1];

                DataRow[] Imgrow = dtImg.Select(String.Format("ImageID={0}", ImageID));
                SyncDatePreExpense = Convert.ToString(Imgrow[0]["DateAdded"]);
                LatPreExpense = Convert.ToString(Imgrow[0]["Latitude"]);
                LongPreExpense = Convert.ToString(Imgrow[0]["Longitude"]);

                DataRow[] Invrow = dtInv.Select();
                DeviceNamePreExpense = Convert.ToString(Invrow[0]["DeviceName"]);
                DeviceIDPreExpense = Convert.ToString(Invrow[0]["DeviceID"]);
                UserNamePreExpense = Convert.ToString(Invrow[0]["UserName"]);
                CountPreExpense = Convert.ToString(Invrow[0]["Count"]);
                ExpenseType = Convert.ToString(Invrow[0]["ExpenseType"]);
                DefaultPrice = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["DefaultPrice"]));
                TotalPrice = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["TotalPrice"]));
                VINPreExpense = Convert.ToString(Invrow[0]["VIN"]);
                ExpSpan0.InnerText = VINPreExpense + " ";
                ExpSpan1.InnerText = String.Format("Expense Type: {0}", ExpenseType + " ");
                ExpSpan2.InnerText = String.Format("Default Price: {0}  Total Price: {1}  Count: {2} ", DefaultPrice, TotalPrice, CountPreExpense);
                return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Latitude: {4} | Longitude: {5}", DeviceNamePreExpense, DeviceIDPreExpense, UserNamePreExpense, SyncDatePreExpense, LatPreExpense, LongPreExpense);
            }
            catch (Exception ex)
            { return ""; }
        }
        #endregion

        #region [Generic Images]
        public String GetImagePath(object Path)
        {
            String ServerUrl = String.Empty;
            string path;
            ServerUrl = System.Configuration.ConfigurationManager.AppSettings["GenericImagePath"];
            path = String.Format("{0}{1}", ServerUrl, Path);
            return path;

        }

        public String GetDesc(object ImageID)
        {
            try
            {
                DataTable dtImg = new DataTable();
                dtImg = (DataTable)arraylistGen[0];


                DataTable dtInv = new DataTable();
                dtInv = (DataTable)arraylistGen[1];

                DataRow[] Imgrow = dtImg.Select(String.Format("ImageID={0}", ImageID));
                SyncDateGen = Convert.ToString(Imgrow[0]["DateAdded"]);
                if (Imgrow != null && Imgrow.Count() > 0)
                {
                    VINGen = Convert.ToString(Imgrow[0]["VIN"]);
                    UserNameGen = Convert.ToString(Imgrow[0]["DisplayName"]);
                    DeviceIDGen = Convert.ToString(Imgrow[0]["DeviceID"]);
                    DeviceNameGen = Convert.ToString(Imgrow[0]["DeviceName"]);
                    spHeader0.InnerText = VIN;
                    LatGen = Convert.ToString(Imgrow[0]["Latitude"]);
                    LongGen = Convert.ToString(Imgrow[0]["Longitude"]);
                    SyncDateGen = Convert.ToString(Imgrow[0]["DateAdded"]);

                    SpanGen0.InnerText = VINGen + " ";
                    SpanGen1.InnerText = ImageDetails + " ";
                    //SpanGen1.InnerText = String.Format("Expense Type: {0}", ExpenseTypeGen + " ");
                    //SpanGen2.InnerText = String.Format("Default Price: {0}  Total Price: {1}  Count: {2} ", DefaultPriceGen, TotalPriceGen, CountGen);

                    return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Latitude: {4} | Longitude: {5}", DeviceNameGen, DeviceIDGen, UserNameGen, SyncDateGen, LatGen, LongGen);
                }
                else
                    return "Missing";
            }
            catch (Exception ex)
            { return ""; }

        }
        #endregion

        #region [UCR Images]
        public String GetImagePathUCR(object Path)
        {
            String str = String.Empty;
            try
            {
                //Path = "http://web.metaoptionllc.com:82/UCRAssets?id=ee9e5e5fa3dc4a44a947ea47e055a6f10ee168853bad494fa2a0e2d2196dab9e3";
                str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(Path.ToString());
                if (String.IsNullOrEmpty(str))
                    str = "../Images/nocar.PNG";

            }
            catch (Exception ex)
            {

            }
            return str;


        }
        public String GetImagePathUCRThumb(object Path)
        {
            String str = String.Empty;
            try
            {
                str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(Path.ToString());
                if (String.IsNullOrEmpty(str))
                    str = "../Images/nocarthumb.PNG";
            }
            catch (Exception ex)
            {
            }

            return str;
        }


        public String GetDescUCR(object UCRMediaDetailID)
        {
            try
            {
                DataTable dtUCRImage = new DataTable();
                dtUCRImage = (DataTable)arraylistUCR[0];


                DataTable dtUCR = new DataTable();
                dtUCR = (DataTable)arraylistUCR[1];

                DataRow[] Imgrow = dtUCRImage.Select(String.Format("UCRMediaDetailID={0}", UCRMediaDetailID));
                SyncDateUCR = Convert.ToString(Imgrow[0]["DateAdded"]);

                DataRow[] UCRrow = dtUCR.Select();
                if (UCRrow != null && UCRrow.Count() > 0)
                {
                    VINUCR = Convert.ToString(UCRrow[0]["VIN"]).ToUpper();
                    UserNameUCR = Convert.ToString(UCRrow[0]["UserName"]);
                    Year = Convert.ToString(UCRrow[0]["Year"]);
                    Make = Convert.ToString(UCRrow[0]["Make"]);
                    Model = Convert.ToString(UCRrow[0]["Model"]);
                    Body = Convert.ToString(UCRrow[0]["Body"]);
                    Price = String.Format("${0:#,###}", Convert.ToDecimal(UCRrow[0]["MarketPrice"]));
                    Mileage = String.Format("{0:#,###}", Convert.ToDecimal(UCRrow[0]["Mileage"]));
                    strType = Convert.ToString(UCRrow[0]["Type"]);
                    Location = Convert.ToString(UCRrow[0]["Location"]);
                    CRID = Convert.ToString(UCRrow[0]["CR_ID"]);
                    CRStatusDesc = Convert.ToString(UCRrow[0]["CR_StatusDesc"]);
                    RunNo = Convert.ToString(UCRrow[0]["RunNumber"]);
                    LaneNo = Convert.ToString(UCRrow[0]["LaneNumber"]);
                    CarFax = Convert.ToString(UCRrow[0]["CarFax"]);
                    ExtColor = Convert.ToString(UCRrow[0]["ExteriorColor"]);
                    InColor = Convert.ToString(UCRrow[0]["InteriorColor"]);
                    ActionCode = Convert.ToString(UCRrow[0]["Auctioncode"]);
                    AuctionText = Convert.ToString(UCRrow[0]["AuctionStatusText"]);
                    SaleDate = Convert.ToString(UCRrow[0]["SaleDate"]);
                    AutoCheck = Convert.ToString(UCRrow[0]["AutoCheck"]);
                    UCRSpan0.InnerText = VINUCR + " ";
                    UCRSpan1.InnerText = String.Format(" {0} {1} {2}", Year, Make, Model);
                    UCRSpan2.InnerText = String.Format(" {0}  {1}  {2} {3} {4}", Body, Price, Mileage, InColor, ExtColor);
                    return String.Format("CRID : {0} |CRStatus : {1}<br/>Date Added: {2} | Type: {3} | Location: {4}| AutoCheck: {5}|<br/> Lane#: {6}| Run#: {7}| CarFax: {8}|<br/> Auction Code: {9}| Auction Status Text: {10}| SaleDate: {11}", CRID, CRStatusDesc, SyncDate, strType, Location, AutoCheck, LaneNo, RunNo, CarFax, ActionCode, AuctionText, SaleDate);
                }
                else
                    return "Missing";
            }
            catch (Exception ex)
            { return ""; }

        }
        #endregion

        #region["ViewAllImage"]

        public String GetAllImagePath(object Path)
        {
            string ImageType = string.Empty;
            string retImages = string.Empty;
            string[] str = Path.ToString().Split(',');
            if (str.Length > 2)
            {

                DataRow[] dr = dtAllImage.Select("ImageType='" + str[2] + "' and ImageId= '" + str[1] + "'");
                if (dr.Length > 0)
                {
                    if (Convert.ToString(dr[0]["ImageType"]) == "Inventory")
                    {
                        retImages = String.Format("{0}{1}", ServerUrlPreInventory, str[0]);
                    }
                    if (Convert.ToString(dr[0]["ImageType"]) == "Expense")
                    {
                        retImages = String.Format("{0}{1}", ServerUrlPreEXpense, str[0]);
                    }
                    if (Convert.ToString(dr[0]["ImageType"]) == "Generic")
                    {
                        String ServerUrl = String.Empty;
                        string path;
                        ServerUrl = System.Configuration.ConfigurationManager.AppSettings["GenericImagePath"];
                        path = String.Format("{0}{1}", ServerUrl, str[0]);
                        retImages = path;
                    }
                    if (Convert.ToString(dr[0]["ImageType"]) == "UCR")
                    {
                        String strUCR = String.Empty;
                        strUCR = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRPhysicalPath(Path.ToString());
                        if (String.IsNullOrEmpty(strUCR))
                            strUCR = "../Images/nocar.PNG";
                        retImages = strUCR;
                    }
                }
            }
            return retImages;

        }

        public String GetAllImageDesc(object ImageID)
        {
            try
            {
                DataRow drHeader = dtHeader.NewRow();
                string[] str = ImageID.ToString().Split(',');
                string ImageType = string.Empty;
                if (str.Length > 1)
                {
                    if (str[1] == "Inventory")
                    {
                        DataRow[] Imgrow = dtAllImage.Select(String.Format("ImageID={0}", str[0]));
                        if (Imgrow.Length > 0)
                        {
                            SyncDate = Convert.ToString(Imgrow[0]["DateAdded"]);
                            Lat = Convert.ToString(Imgrow[0]["Latitude"]);
                            Long = Convert.ToString(Imgrow[0]["Longitude"]);
                        }

                        DataRow[] Invrow = dtAllimageValue.Select("ImageType='Inventory'");
                        if (Invrow.Length > 0)
                        {
                            DeviceName = Convert.ToString(Invrow[0]["DeviceName"]);
                            DeviceID = Convert.ToString(Invrow[0]["DeviceID"]);
                            UserName = Convert.ToString(Invrow[0]["UserName"]);
                            Year = Convert.ToString(Invrow[0]["Year"]);
                            Make = Convert.ToString(Invrow[0]["Make"]);
                            Model = Convert.ToString(Invrow[0]["Model"]);
                            Body = Convert.ToString(Invrow[0]["Body"]);
                            Price = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["Price"]));
                            Mileage = String.Format("{0:#,###}", Convert.ToDecimal(Invrow[0]["Mileage"]));

                            VIN = Convert.ToString(Invrow[0]["VIN"]);
                        }
                        //SpanAll0.InnerText = VIN;
                        //SpanAll1.InnerText = String.Format(" {0} {1} {2}", Year, Make, Model);
                        //SpanAll2.InnerText = String.Format(" {0}  {1}  {2}", Body, Price, Mileage);

                        return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Type: {4} | Location: {5}", DeviceName, DeviceID, UserName, SyncDate, Lat, Long);

                    }

                    else if (str[1] == "Expense")
                    {
                        DataRow[] Imgrow = dtAllImage.Select(String.Format("ImageID={0}", str[0]));
                        if (Imgrow.Length > 0)
                        {
                            SyncDatePreExpense = Convert.ToString(Imgrow[0]["DateAdded"]);
                            LatPreExpense = Convert.ToString(Imgrow[0]["Latitude"]);
                            LongPreExpense = Convert.ToString(Imgrow[0]["Longitude"]);
                        }


                        DataRow[] Invrow = dtAllimageValue.Select("ImageType='Expense'");
                        if (Invrow.Length > 0)
                        {
                            DeviceNamePreExpense = Convert.ToString(Invrow[0]["DeviceName"]);
                            DeviceIDPreExpense = Convert.ToString(Invrow[0]["DeviceID"]);
                            UserNamePreExpense = Convert.ToString(Invrow[0]["UserName"]);
                            CountPreExpense = Convert.ToString(Invrow[0]["Count"]);
                            ExpenseType = Convert.ToString(Invrow[0]["ExpenseType"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(Invrow[0]["DefaultPrice"])))
                                DefaultPrice = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["DefaultPrice"]));
                            if (!string.IsNullOrEmpty(Convert.ToString(Invrow[0]["TotalPrice"])))
                                TotalPrice = String.Format("${0:#,###}", Convert.ToDecimal(Invrow[0]["TotalPrice"]));
                            VINPreExpense = Convert.ToString(Invrow[0]["VIN"]);
                        }
                        //SpanAll0.InnerText = VIN + " ";
                        //SpanAll1.InnerText = String.Format("Expense Type: {0}", ExpenseType + " ");
                        //SpanAll2.InnerText = String.Format("Default Price: {0}  Total Price: {1}  Count: {2} ", DefaultPrice, TotalPrice, CountPreExpense);


                        return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Latitude: {4} | Longitude: {5}", DeviceNamePreExpense, DeviceIDPreExpense, UserNamePreExpense, SyncDatePreExpense, LatPreExpense, LongPreExpense);

                    }
                    else if (str[1] == "Generic")
                    {
                        DataRow[] Imgrow = dtAllImage.Select(String.Format("ImageID={0}", str[0]));
                        SyncDateGen = Convert.ToString(Imgrow[0]["DateAdded"]);

                        DataRow[] GenRow = dtAllimageValue.Select("ImageType='Generic'");
                        if (Imgrow != null && Imgrow.Count() > 0)
                        {
                            VINGen = Convert.ToString(GenRow[0]["VIN"]);
                            UserNameGen = Convert.ToString(GenRow[0]["UserName"]);
                            DeviceIDGen = Convert.ToString(GenRow[0]["DeviceID"]);
                            DeviceNameGen = Convert.ToString(GenRow[0]["DeviceName"]);
                            spHeader0.InnerText = VIN;
                            LatGen = Convert.ToString(GenRow[0]["Latitude"]);
                            LongGen = Convert.ToString(GenRow[0]["Longitude"]);
                            SyncDateGen = Convert.ToString(GenRow[0]["DateAdded"]);

                            //SpanAll0.InnerText = VINGen + " ";
                            //SpanAll1.InnerText = ImageDetails + " ";


                            return String.Format("Device: {0} | DeviceID: {1}<br/>Synced By: {2} | Synced Date: {3} | Latitude: {4} | Longitude: {5}", DeviceNameGen, DeviceIDGen, UserNameGen, SyncDateGen, LatGen, LongGen);
                        }
                        else
                            return "Missing";

                    }
                    else if (str[1] == "UCR")
                    {
                        DataRow[] Imgrow = dtAllImage.Select(String.Format("ImageID={0}", str[0]));
                        if (Imgrow.Length > 0)
                            SyncDateGen = Convert.ToString(Imgrow[0]["DateAdded"]);

                        DataRow[] UCRrow = dtAllimageValue.Select("ImageType='UCR'");
                        if (UCRrow != null && UCRrow.Count() > 0)
                        {
                            VINUCR = Convert.ToString(UCRrow[0]["VIN"]);
                            UserNameGen = Convert.ToString(UCRrow[0]["UserName"]);
                            Year = Convert.ToString(UCRrow[0]["Year"]);
                            Make = Convert.ToString(UCRrow[0]["Make"]);
                            Model = Convert.ToString(UCRrow[0]["Model"]);
                            Body = Convert.ToString(UCRrow[0]["Body"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(UCRrow[0]["Price"])))
                                Price = String.Format("${0:#,###}", Convert.ToDecimal(UCRrow[0]["Price"]));
                            if (!string.IsNullOrEmpty(Convert.ToString(UCRrow[0]["Mileage"])))
                                Mileage = String.Format("{0:#,###}", Convert.ToDecimal(UCRrow[0]["Mileage"]));
                            strType = Convert.ToString(UCRrow[0]["Type"]);
                            Location = Convert.ToString(UCRrow[0]["Location"]);
                            CRID = Convert.ToString(UCRrow[0]["CRID"]);
                            CRStatusDesc = Convert.ToString(UCRrow[0]["CRStatusDesc"]);
                            RunNo = Convert.ToString(UCRrow[0]["RunNo"]);
                            LaneNo = Convert.ToString(UCRrow[0]["LaneNo"]);
                            CarFax = Convert.ToString(UCRrow[0]["CarFax"]);
                            ExtColor = Convert.ToString(UCRrow[0]["ExtColor"]);
                            InColor = Convert.ToString(UCRrow[0]["IntColor"]);
                            ActionCode = Convert.ToString(UCRrow[0]["AuctionCode"]);
                            AuctionText = Convert.ToString(UCRrow[0]["AuctionText"]);
                            SaleDate = Convert.ToString(UCRrow[0]["SaleDate"]);
                            //SpanAll0.InnerText = VINUCR + " ";
                            //SpanAll1.InnerText = String.Format(" {0} {1} {2}", Year, Make, Model);
                            //SpanAll2.InnerText = String.Format(" {0}  {1}  {2} {3} {4}", Body, Price, Mileage, InColor, ExtColor);


                            return String.Format("CRStatus : {0} | CRID: {1}<br/>Added Date: {2} | Type: {3} | Location: {4}|<br/> Lane#: {5}| Run#: {6}| CarFax: {7}|<br/> Auction Code: {8}| Auction Status Text: {9}| SaleDate: {10}", CRStatusDesc, CRID, SyncDate, strType, Location, LaneNo, RunNo, CarFax, ActionCode, AuctionText, SaleDate);
                        }
                        else
                            return "Missing";

                    }
                    else
                        return "Missing";
                }
                else
                    return "Missing";
            }
            catch (Exception ex)
            { return ""; }
        }


        public void CreateAllImageTable()
        {
            dtAllImage.Columns.Add("ImageID");
            dtAllImage.Columns.Add("ServerPath");
            dtAllImage.Columns.Add("ThumbNailPath");
            dtAllImage.Columns.Add("MediumPath");
            dtAllImage.Columns.Add("DateAdded");
            dtAllImage.Columns.Add("Latitude");
            dtAllImage.Columns.Add("Longitude");
            dtAllImage.Columns.Add("ImageType");


            dtAllimageValue.Columns.Add("UserName");
            dtAllimageValue.Columns.Add("Year");
            dtAllimageValue.Columns.Add("VIN");
            dtAllimageValue.Columns.Add("Make");
            dtAllimageValue.Columns.Add("Model");
            dtAllimageValue.Columns.Add("Body");
            dtAllimageValue.Columns.Add("Mileage");
            dtAllimageValue.Columns.Add("ExtColor");
            dtAllimageValue.Columns.Add("IntColor");
            dtAllimageValue.Columns.Add("DeviceId");
            dtAllimageValue.Columns.Add("DeviceName");
            dtAllimageValue.Columns.Add("Price");
            dtAllimageValue.Columns.Add("SyncdBy");
            dtAllimageValue.Columns.Add("SyncDate");
            dtAllimageValue.Columns.Add("Count");
            dtAllimageValue.Columns.Add("ExpenseType");
            dtAllimageValue.Columns.Add("DefaultPrice");
            dtAllimageValue.Columns.Add("TotalPrice");
            dtAllimageValue.Columns.Add("Type");
            dtAllimageValue.Columns.Add("Location");
            dtAllimageValue.Columns.Add("CRID");
            dtAllimageValue.Columns.Add("CRStatusDesc");
            dtAllimageValue.Columns.Add("CarFax");
            dtAllimageValue.Columns.Add("LaneNo");
            dtAllimageValue.Columns.Add("RunNo");
            dtAllimageValue.Columns.Add("AuctionCode");
            dtAllimageValue.Columns.Add("AuctionText");
            dtAllimageValue.Columns.Add("SaleDate");
            dtAllimageValue.Columns.Add("Latitude");
            dtAllimageValue.Columns.Add("Longitude");
            dtAllimageValue.Columns.Add("DateAdded");
            dtAllimageValue.Columns.Add("ImageType");

            dtHeader.Columns.Add("HeaderText");
        }
        #endregion

        #region[Audio Video region]
        protected void imgBtnImage_Click(object sender, EventArgs e)
        {
            string videoPath = ((ImageButton)sender).CommandArgument;
            String extn = videoPath.Substring(videoPath.LastIndexOf("."));
            hdnAudioUrl.Value = videoPath;
            ImageButton ibtnExpenseEdit = (ImageButton)sender;
            ibtnExpenseEdit.Attributes.Add("onClientClick", "SetAudioURL();");
            //switch (extn.ToUpper())
            //{
            //    case ".CAF":
            //    case ".WAV":
            //    case ".MP3":
            //      //  myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", videoPath);
            //      //  myAudio.Visible = true;
            //        myVideo.Visible = false;
            //        break;
            //    case ".WMV":
            //    case ".OGG":
            //    case ".FLV":
            //    //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/ogg'>", videoPath);
            //    //myAudio.Visible = false;
            //    //myVideo.Visible = true;
            //    //break;
            //    case ".MP4":
            //      //  myVideo.InnerHtml = String.Format("<source src='{0}' type='video/mp4' codecs=dirac, speex>Your browser does not support the <code>video</code> element.", videoPath);
            //      //  myAudio.Visible = false;
            //        myVideo.Visible = true;
            //        break;
            //    default:
            //        //myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p>", videoPath);
            //        // myAudio.Visible = true;
            //        // myVideo.Visible = false;
            //        break;

            //}
        }
        #endregion

        #region[Set Audio and Video]
        protected void imgBtnImageAudio_Click(object sender, EventArgs e)
        {

            string videoPath = ((ImageButton)sender).CommandArgument;
            string str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRAudioPhysicalPath(videoPath.ToString());
            String extn = str.Substring(videoPath.LastIndexOf("."));
            switch (extn.ToUpper())
            {
                case ".CAF":
                case ".WAV":
                case ".MP3":
                    myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                    myAudio.Visible = true;
                    // myVideo.Visible = false;
                    break;
                case ".WMV":
                case ".OGG":
                case ".FLV":
                //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/ogg'>", videoPath);
                //myAudio.Visible = false;
                //myVideo.Visible = true;
                //break;               
                default:
                    myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                    myAudio.Visible = true;
                    // myVideo.Visible = false;
                    break;

            }
        }

        protected void imgBtnImageVideo_Click(object sender, EventArgs e)
        {
            string videoPath = ((ImageButton)sender).CommandArgument;
            string str = METAOPTION.WS.DecodeUCRVirtualPah.GetUCRVideoPhysicalPath(videoPath.ToString());
            String extn = str.Substring(videoPath.LastIndexOf("."));
            switch (extn.ToUpper())
            {
                case ".CAF":
                case ".WAV":
                case ".MP3":
                //myAudio.InnerHtml = String.Format("<source src='{0}' controls autoplay loop><p>Your browser does not support the audio element.</p></audio>", str);
                //myAudio.Visible = true;
                //myVideo.Visible = false;
                //break;
                case ".WMV":
                case ".OGG":
                case ".FLV":
                //myVideo.InnerHtml = String.Format("<source src='{0}' type='video/ogg'>", videoPath);
                //myAudio.Visible = false;
                //myVideo.Visible = true;
                //break;
                case ".MP4":
                    myVideo.InnerHtml = String.Format("<source src='{0}' type='video/mp4' codecs=dirac, speex>Your browser does not support the <code>video</code> element.", str);
                    //  myAudio.Visible = false;
                    myVideo.Visible = true;
                    break;
                default:
                    myVideo.InnerHtml = String.Format("<source src='{0}' AddType='video/mp4' codecs=mp4, speex>Your browser does not support the <code>video</code> element.", str);
                    //  myAudio.Visible = false;
                    myVideo.Visible = true;
                    break;
            }
        }
        #endregion

    }

}