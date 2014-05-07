<%@ Control Language="C#" AutoEventWireup="true"  %>
<%@ Import Namespace = "METAOPTION"%>
<%@ Import Namespace = "METAOPTION.BAL"%>
<%@ Import Namespace = "System.Data"%>
<%@ Import Namespace = "System.Data"%>
<%@ Import Namespace = "System.Collections.Generic"%>
<%--<%@ OutputCache Duration="120" VaryByParam="none" %>--%>

<link href="../CSS/MainStyle.css" rel="stylesheet" type="text/css" />
 
<% 
   
    
    ViewAnnouncementBAL ObjAnn = new ViewAnnouncementBAL();
    //Annuncement Type: 1=General,2=Lane,3=Commision,4=Chrome
    
    DataTable dt =ObjAnn.GetAnnouncementList(Constant.OrgID);
    bool bAnnouncement = false;
    List<String> Permissions = CommonBAL.GetPagePermission(Constant.UserId, "ANNOUNCEMENT");
    for(int i=0;i< dt.Rows.Count;i++)
    {
        string Title = dt.Rows[i]["AnnouncementTitle"].ToString();
        DateTime date = Convert.ToDateTime( dt.Rows[i]["DateAdded"]);
        string AnnouncementId = dt.Rows[i]["AnnouncementId"].ToString();
        //String.Format({0:M},dt)not working
        int d = date.Day;
        int M = date.Month;
        int H = date.Hour;
        int Mnt = date.Minute;
        string Time = H.ToString() + ":" + Mnt.ToString();
        string StrTitle = string.Empty;
        string Month = string.Empty;
        switch (M)
        {
            case 1:
                Month = "Jan";
                break;
            case 2:
                Month = "Feb";
                break;
            case 3:
                Month = "Mar";
                break;
            case 4:
                Month = "Apr";
                break;
            case 5:
                Month = "May";
                break;
            case 6:
                Month = "Jun";
                break;
            case 7:
                Month = "Jul";
                break;
            case 8:
                Month = "Aug";
                break;
            case 9:
                Month = "Sep";
                break;
            case 10:
                Month = "Oct";
                break;
            case 11:
                Month = "Nov";
                break;
            case 12:
                Month = "Dec";
                break;
                
        }
        string strA = string.Empty;
        string ToolTips=string.Empty;
        StrTitle = Title + " " + d.ToString() + " " + Month + "  " + Time;
        int strLen = StrTitle.ToString().Length;
        if(strLen>33)
        {
            ToolTips = StrTitle;
            StrTitle = StrTitle.Substring(0, 33);
            StrTitle = StrTitle + "...";
        }
        
        if ((Permissions.Contains("ANNOUNCEMENT.VIEW")) || (Permissions.Contains("ANNOUNCEMENT.MANAGE")))
        {
            bAnnouncement = true;
          
    %>
    
       <a class="OrangeText_Link" title="<%=ToolTips %>"  href="./AnnouncementDetails.aspx?AnnouncementId=<% =AnnouncementId%>"><% =StrTitle%></a> <br><div style="height:5px"></div> 
    <%
        
        }  
        
    }  
    if (bAnnouncement == false)
        { %>
            Announcement not available!
          <%   
        }    
    %>
    




        
   