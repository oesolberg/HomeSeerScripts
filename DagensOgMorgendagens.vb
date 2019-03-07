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
		
		
		If ev.StartDateTimeLocal.Date=Now.Date AndAlso ev.AllDayEvent Then
			calendarEventAsString ="Dagens "
		ElseIf ev.StartDateTimeLocal.Date=Now.Date AndAlso (Not ev.AllDayEvent) Then
			calendarEventAsString ="Kl. "
		ElseIf ev.StartDateTimeLocal.Date=Now.Date.AddDays(1) AndAlso (ev.AllDayEvent) Then
			calendarEventAsString ="Imorgen "
		ElseIf ev.StartDateTimeLocal.Date=Now.Date.AddDays(1) AndAlso (Not ev.AllDayEvent) Then
			calendarEventAsString ="Imorgen kl. "
		End If
		
		
		If ev.AllDayEvent AndAlso ev.StartDateTimeLocal.Date < Now.Date.AddDays(2) then
			calendarEventAsString = calendarEventAsString & ev.Subject 
		ElseIf Not ev.AllDayEvent AndAlso ev.StartDateTimeLocal.Date < Now.Date.AddDays(2) then
			calendarEventAsString= calendarEventAsString & Format(ev.StartDateTimeLocal,"HH:mm") & " - " & ev.Subject
		ElseIf Not ev.AllDayEvent AndAlso ev.StartDateTimeLocal.Date >= Now.Date.AddDays(2) then
			calendarEventAsString= calendarEventAsString & Format(ev.StartDateTimeLocal,"yyyy-MM-dd HH:mm") & " - "  & ev.Subject 
		Else
			calendarEventAsString= calendarEventAsString & Format(ev.StartDateTimeLocal,"yyyy-MM-dd") & " (hele dagen) - "  & ev.Subject 
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