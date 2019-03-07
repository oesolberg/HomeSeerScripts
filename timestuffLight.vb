Sub Main(ByVal Parms As Object) 
Dim msg as string = "" 
Try    
    Dim ParmArray() as String 
    ParmArray = Parms.tostring.split(",") 
    Dim tDevice = ParmArray(0) 
    Dim tCalendar = ParmArray(1)
    Dim tDays = cint(ParmArray(2))
    Dim allEvents As Object()
    
hs.WriteLog("test","starter script")
    allEvents = hs.PluginFunction("GCalSeer", "", "GetItemsFromCalendar", new Object(){tCalendar ,tDays }) 
hs.WriteLog("test","utlisting")
    For Each ev As Object In allEvents 
		If ev.AllDayEvent then
			msg = msg & "<br>" & Format(ev.StartDateTimeLocal,"yyyy-MM-dd") & " (hele dagen) - " & ev.Subject & "</br>"
		Else
           msg = msg & "<br>" &  Format(ev.StartDateTimeLocal,"yyyy-MM-dd HH:mm") & " - " & ev.Subject & "</br>"
		End If
    Next 
    
    hs.WriteLog("test","events found: " & msg)    
        
    if msg="" then msg="No Events Scheduled"        
    hs.SetDeviceString(tDevice , msg, true) 
  
Catch ex As Exception        
    hs.WriteLog("", "Exception in script: " & ex.Message)    
End Try
hs.WriteLog("test","---=== DONE ===---")

End Sub