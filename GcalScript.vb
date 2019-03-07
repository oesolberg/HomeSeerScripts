sub Main(ByVal Parms As Object) 
Dim msg as string = "" 
Try    
    Dim ParmArray() as String 
    ParmArray = Parms.tostring.split(",") 
    Dim tDevice = ParmArray(0) 
    Dim tCalendar = ParmArray(1)
    Dim tDays = cint(ParmArray(2))
    Dim allEvents As Object()
    

    allEvents = hs.PluginFunction("GCalSeer", "", "GetItemsForCalendar", new Object(){tCalendar ,tDays }) 

    For Each ev As Object In allEvents 
           msg = msg & "<br>" & ev & "</br>"   
    Next 
    
    hs.WriteLog("test","events found: " & msg)    
        
    if msg="" then msg="No Events Scheduled"        
    hs.SetDeviceString(tDevice , msg, true) 
  
Catch ex As Exception        
    hs.WriteLog("", "Exception in script: " & ex.Message)    
End Try
hs.WriteLog("test","---=== DONE ===---")

End Sub