Namespace METAOPTION.BAL
    Public Class Utils
        Public Shared Sub FocusControlOnPageLoad(ByVal ClientID As String, ByVal page As System.Web.UI.Page)
            page.RegisterClientScriptBlock("CtrlFocus", "<script> " & Chr(13) & "" & Chr(10) & "" & Chr(13) & "" & Chr(10) & " function ScrollView()" & Chr(13) & "" & Chr(10) & "" & Chr(13) & "" & Chr(10) & " {" & Chr(13) & "" & Chr(10) & " var el = document.getElementById('" + ClientID + "')" & Chr(13) & "" & Chr(10) & " if (el != null)" & Chr(13) & "" & Chr(10) & " { " & Chr(13) & "" & Chr(10) & " el.scrollIntoView();" & Chr(13) & "" & Chr(10) & " el.focus();" & Chr(13) & "" & Chr(10) & " }" & Chr(13) & "" & Chr(10) & " }" & Chr(13) & "" & Chr(10) & "" & Chr(13) & "" & Chr(10) & " window.onload = ScrollView;" & Chr(13) & "" & Chr(10) & "" & Chr(13) & "" & Chr(10) & " </script>")
        End Sub
    End Class
End Namespace
