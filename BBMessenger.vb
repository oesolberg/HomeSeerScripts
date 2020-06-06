'BBMessenger example script on how to send a message via script
Public Sub Main(ByVal Parms As Object)
        Dim logto As String = "BBMessenger-Script"
        hs.WriteLog(logto, "Running script")
        hs.WriteLog(logto, "Sending Message: " + Parms.ToString())
        Dim plugin As HomeSeerAPI.PluginAccess = New HomeSeerAPI.PluginAccess(hs, "BBMessenger", "")
        Dim s1 As String() = Parms.ToString().Split("|".ToCharArray())
        If (s1.Length >= 2) Then
            If plugin IsNot Nothing Then
                Dim message(1) As Object
                message(0) = s1(0)
                message(1) = s1(1)
                plugin.PluginFunction("SendMessage", message)
            Else
                hs.WriteLog(logto, "Error sending message. Not able to locate the plug-in.")
            End If
        End If
        hs.WriteLog(logto, "Exiting script")
    End Sub