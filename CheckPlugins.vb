Sub Main(ByVal Parms As String)

    Dim RequiredPlugins() As String = {"AutomowerConnectSeer:","DaikinSeer:","DataCurveSeer:","Device History:","GCalSeer:","MQTT:","NetCAM:","Pushover 3P:","RfLinkSeer:","RFXCOM:","SCREPOSITORY:","SqueezeBox:","TwilioSeer:","Z-Wave:"}

    Dim Debug As Boolean = True
    Dim logName As String = "Plugin Check"
    Dim NonRunningPlugins As String
	hs.WriteLog(logName,"Starting check")
    Try
        Dim RunningPlugins As Object = hs.GetPluginsList
        If RunningPlugins IsNot Nothing AndAlso RunningPlugins.Length > 0 Then
		'hs.WriteLog(logName,"Got a list")
            ' loop thru required list
            For index As Integer = 0 To RequiredPlugins.Length - 1
                ' search running list for this required
                For Each RunningPlugin As String In RunningPlugins
                    If RequiredPlugins(index) = RunningPlugin Then
                        ' this plugin is running, so no longer required!
                        RequiredPlugins(index) = Nothing
                        Exit For
                    End If
                Next
            Next
            ' format & pretty up the list
            NonRunningPlugins = String.Join("", RequiredPlugins).Replace(":", ", ").Trim()
            If NonRunningPlugins.Length > 0 Then NonRunningPlugins = Left(NonRunningPlugins,NonRunningPlugins.Length - 1)
            If Debug Then hs.WriteLog(logName,"***" & NonRunningPlugins & "***")
            If NonRunningPlugins > "" Then
                'Do Something
                'Send Pushover Message
                    Dim CO(8) As Object
                    CO(0) = "All Clients" 'must be matched with a device name already in the plugin or 'All Clients' CASE SENSITIVE
                    CO(1) = NonRunningPlugins 'message body
                    CO(2) = "Plugins Not Running" 'message subject
                    CO(3) = "normal" 'low/normal/high/emergency
                    CO(4) = "pushover" 'message sound from list: pushover,bike,bugle,cashregister,classical,cosmic,falling,gamelan,incoming,intermission,magic,mechanical,pianobar,siren,spacealarm,tugboat,alien,climb,persistent,echo,updown,none
                    CO(5) = Nothing
                    CO(6) = Nothing
                    CO(7) = Nothing
                    CO(8) = Nothing
                    hs.PluginFunction("Pushover 3P", "", "Pushscript", CO)
            Else
                'All Good
                If Debug Then hs.WriteLog(logName,"All required plugins are running")
            End If
        Else
            hs.WriteLog(logName,"Nothing to see here.  Move along.")
        End If
	hs.WriteLog(logName,"Ended check")
    Catch ex As Exception
        hs.WriteLogEx(logname, "Exception " & ex.ToString, "#ff0000")
    End Try
End Sub