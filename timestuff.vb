Sub Main(ByVal Parms As Object) 
Dim msg as string = "" 
Try    
    Dim ParmArray() as String 
    ParmArray = Parms.tostring.split(",") 
    Dim tDevice = ParmArray(0) 
    Dim tCalendar = ParmArray(1)
    Dim tDays = cint(ParmArray(2))
    Dim allEvents As Object()
    
	'hs.WriteLog("test","starter script")
    allEvents = hs.PluginFunction("GCalSeer", "", "GetItemsFromCalendar", new Object(){tCalendar ,tDays }) 
    For Each ev As Object In allEvents 
		hs.WriteLog("test",ev.Subject)
		Dim calendarEventAsString as string=""
		If ev.AllDayEvent then
			calendarEventAsString = Format(ev.StartDateTimeLocal,"yyyy-MM-dd") & " (hele dagen) : " & ev.Subject 
		Else
           calendarEventAsString= Format(ev.StartDateTimeLocal,"yyyy-MM-dd HH:mm") & " - " & Format(ev.EndDateTimeLocal,"HH:mm")& " : " & ev.Subject 
		End If
		
		' hs.WriteLog("test",calendarEventAsString)
		' hs.WriteLog("test now",Now.Date)
		' hs.WriteLog("test lokal",ev.StartDateTimeLocal.Date)
		If ev.StartDateTimeLocal.Date = Now.Date then
			calendarEventAsString= "<b>" & calendarEventAsString & "</b>"
		End If
		msg=msg & calendarEventAsString & "</br>"
    Next 
    
    hs.WriteLog("test","events found: " & msg)    
        
    If msg="" then 
		msg="No Events Scheduled"
	End If	
    
	hs.SetDeviceString(tDevice , msg, true) 
  
Catch ex As Exception        
    hs.WriteLog("", "Exception in script: " & ex.Message)    
End Try
hs.WriteLog("test","---=== DONE ===---")

End Sub