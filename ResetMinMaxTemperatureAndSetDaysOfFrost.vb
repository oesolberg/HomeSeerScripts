dim highestTempStart as double  = -99
dim lowestTempStart as double   = 99
dim  logName as string="Guahtdim_ResetMinMaxTempAndSetDaysOfFrost"
'0=numberOfDaysWithoutFrost
'1=Min value
'2=Max value
'3=Min value
'4=Max value

Public Sub Main(ByVal params As Object)
	'split input if any
  	If params.Length = 0 Then
		hs.WriteLog(logName,"No input value found. Exiting script")
		Exit Sub
	End If

	Dim arrInputs() as string
	arrInputs=params.Split(",")

	If arrInputs.Length < 5 Then
		hs.WriteLog(logName,"Misssing parameters. Exiting script")
		Exit Sub
	End If

	'CheckAndSetFrost
	UpdateNumberOfDaysWithoutFrost(arrInputs(0),arrInputs(1),arrInputs(3))
	
	'ResetValues
	ResetMinMaxValues(arrInputs(1),arrInputs(2))
	ResetMinMaxValues(arrInputs(3),arrInputs(4))
	
End Sub


Private Sub UpdateNumberOfDaysWithoutFrost(ByVal deviceRefDaysWithoutFrost As Integer,ByVal deviceRefMinTemp1 As Integer,ByVal deviceRefMinTemp2 As Integer)
    Dim foundFrost as boolean=False
	Dim foundTemp1 as double=hs.DeviceValueEx(deviceRefMinTemp1)
hs.WriteLog("Update days of frost","temp1:"+  CStr(foundTemp1))
	Dim foundTemp2 as double=hs.DeviceValueEx(deviceRefMinTemp2)
hs.WriteLog("Update days of frost","temp2:"+  CStr(foundTemp1))
	If foundTemp1<=0 Or foundTemp2<=0 Then
		foundFrost=True
	End If
		
	If foundFrost=False Then
		Dim numberOfDaysWithoutFrost as double = hs.DeviceValueEx(deviceRefDaysWithoutFrost)
hs.WriteLog("Update days of frost","current days without frost:" + CStr(numberOfDaysWithoutFrost))
		numberOfDaysWithoutFrost=numberOfDaysWithoutFrost+1
hs.WriteLog("Update days of frost","New number of days without frost:" + CStr(numberOfDaysWithoutFrost))
		hs.SetDeviceValueByRef(deviceRefDaysWithoutFrost, numberOfDaysWithoutFrost , True)
	Else
hs.WriteLog("Update days of frost","resetting days to zero")
		hs.SetDeviceValueByRef(deviceRefDaysWithoutFrost, 0 , True)
	End If
	

End Sub


Private Sub ResetMinMaxValues(ByVal deviceRefMinValue As Integer,ByVal deviceRefMaxValue As Integer)
    
	hs.SetDeviceValueByRef(deviceRefMinValue, lowestTempStart , True)
	hs.SetDeviceValueByRef(deviceRefMaxValue, highestTempStart , True)
	
End Sub