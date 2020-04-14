Sub Main(ByVal Parm As Object)

    Try


Dim pushoverParams(8) As Object
pushoverParams(0) = "All Clients" 'Device/Group - must matched with a device name already in the plugin or 'All Clients' CASE SENSITIVE
pushoverParams(1) = "Server is starting up again" 'Message Body
pushoverParams(2) = "HomeSeer StartUp" 'Message Title
pushoverParams(3) = "normal" 'low/normal/high/emergency
pushoverParams(4) = "none" 'Message Sound - must match from list already in HomeSeer
pushoverParams(5) = Nothing 'Message URL
pushoverParams(6) = Nothing 'Message URL Title
pushoverParams(7) = Nothing 'API String
pushoverParams(8) = Nothing 'Attachment

        hs.PluginFunction("Pushover 3P", "", "Pushscript", pushoverParams)

    Catch ex As Exception
        hs.WriteLog("", "Exception in script: " & ex.Message)
    End Try
End Sub