using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace METAOPTION.WS
{
    /// <summary>
    /// Summary description for DecodeUCRVirtualPah
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DecodeUCRVirtualPah : System.Web.Services.WebService
    {

        [WebMethod]
        public static string GetUCRPhysicalPath(String VirtualPath)
        {
            string html = GetHtml(VirtualPath);
            List<Uri> ImgLink = FetchLinksFromSource(html);
            if (ImgLink.Count > 0)
                return ImgLink[0].AbsoluteUri;
            else
                return "";
        }


        public static string GetHtml(string URL)
        {
            HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);
            myWebRequest.Method = "GET";
            myWebRequest.Timeout = 30000;
            // make request for web page

            HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            StreamReader myWebSource = new StreamReader(myWebResponse.GetResponseStream());
            string myPageSource = string.Empty;
            myPageSource = myWebSource.ReadToEnd();
            myWebResponse.Close();
            return myPageSource;

        }

        public static List<Uri> FetchLinksFromSource(string htmlSource)
        {
            List<Uri> links = new List<Uri>();
            string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match m in matchesImgSrc)
            {
                string href = m.Groups[1].Value;
                links.Add(new Uri(href));
            }
            return links;
        }

        #region [Added by Rupendra 08 Oct 12]
        /// <summary>
        /// Added by Rupendra 8 Ocr 12 to show Audio and Video Images
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetUCRAudioPhysicalPath(String VirtualPath)
        {
            string html = GetHtml(VirtualPath);
            List<Uri> ImgLink = FetchAudioLinksFromSource(html);
            if (ImgLink.Count > 0)
                return ImgLink[0].AbsoluteUri;
            else
                return "";
        }
        public static List<Uri> FetchAudioLinksFromSource(string htmlSource)
        {
            List<Uri> links = new List<Uri>();           
            string regexImgSrc = @"<source[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match m in matchesImgSrc)
            {
                string href = m.Groups[1].Value;
                links.Add(new Uri(href));
            }
            return links;
        }


        [WebMethod]
        public static string GetUCRVideoPhysicalPath(String VirtualPath)
        {
            string html = GetHtml(VirtualPath);
            List<Uri> ImgLink = FetchVideoLinksFromSource(html);
            if (ImgLink.Count > 0)
                return ImgLink[0].AbsoluteUri;
            else
                return "";
        }
        public static List<Uri> FetchVideoLinksFromSource(string htmlSource)
        {
            List<Uri> links = new List<Uri>();
            string regexImgSrc = @"<source[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
            MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match m in matchesImgSrc)
            {
                string href = m.Value;
                string matchString = Regex.Match(href, "<source.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                links.Add(new Uri(matchString));
            }
            return links;
        }
        #endregion
    }
}
