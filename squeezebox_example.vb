Sub Main(ByVal parm As Object)

    Dim hspi As Object = hs.plugin("SqueezeBox")

    If hspi Is Nothing Then
        hs.WriteLog("SqueezeBox Script", "Plugin not found!")
    Else
        hs.WriteLog("SqueezeBox Script", "Found plugin " & hspi.Name())
        hspi.BroadcastMessage("Device Status", "$$DN:B1 (B1) is $$DS:B1")
    End If
End Sub
