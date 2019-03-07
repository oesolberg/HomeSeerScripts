Sub Main(ByVal Parms As Object) 
Dim msg as string = "" 
Try    
    Dim ParmArray() as String 
    ParmArray = Parms.tostring.split(",") 
    Dim tDevice = ParmArray(0) 
    Dim tCalendar = ParmArray(1)
    Dim tDays = cint(ParmArray(2))
    Dim allEvents As Object
    

    allEvents = hs.PluginFunction("GCalSeer", "", "GetItemsFromCalendar", new Object(){tCalendar ,tDays }) 
    For Each ev As Object In allEvents 

		Dim calendarEventAsString as string=""
		
		
		If ev.StartDateTimeLocal.Date=Now.Date Then
			If ev.AllDayEvent then
				calendarEventAsString ="Dagens "
			else
				calendarEventAsString ="Kl. "
			End If
		End If
		If ev.StartDateTimeLocal.Date=Now.Date.AddDays(1) then
			If ev.AllDayEvent then
				calendarEventAsString ="Imorgen "
			else
				calendarEventAsString ="Imorgen kl. "
			End If
		End If
		
		
		If ev.StartDateTimeLocal.Date < Now.Date.AddDays(2) then
			If ev.AllDayEvent then
				calendarEventAsString = calendarEventAsString & ev.Subject 
			else
				calendarEventAsString= calendarEventAsString & Format(ev.StartDateTimeLocal,"HH:mm") & " - " & ev.Subject
			End If
		Else
			If ev.AllDayEvent then
				calendarEventAsString= calendarEventAsString & Format(ev.StartDateTimeLocal,"yyyy-MM-dd") & " (hele dagen) - "  & ev.Subject 
			else
				calendarEventAsString= calendarEventAsString & Format(ev.StartDateTimeLocal,"yyyy-MM-dd HH:mm") & " - "  & ev.Subject 
			End If
		End If
		
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