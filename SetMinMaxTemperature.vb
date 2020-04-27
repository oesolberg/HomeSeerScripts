Dim  logName as string="Guahtdim_SetMinMaxTemp"
'0=temp to be read
'1=Min value
'2=Max value

Public Sub Main(ByVal params As Object)
	'split input if any
  	If params.Length = 0 Then
		hs.WriteLog(logName,"No input value found. Exiting script")
		Exit Sub
	End If

	Dim arrInputs() as string
	arrInputs=params.Split(",")

	If arrInputs.Length < 3 Then
		hs.WriteLog(logName,"Misssing parameters. Exiting script")
		Exit Sub
	End If

	'Get value from first device ref
	Dim meterToReadRef as Integer = arrInputs(0)
	Dim foundTemp As Double = hs.DeviceValueEx(meterToReadRef)
	
	UpdateTempForMinimumValue(foundTemp,arrInputs(1))
	UpdateTempForMaximumValue(foundTemp,arrInputs(2))
	
End Sub


Private Sub UpdateTempForMinimumValue(ByVal foundTemperature as double, ByVal deviceRef As Integer)
    Dim currentTemp as double=hs.DeviceValueEx(deviceRef)
	If currentTemp>foundTemperature Then
		'Update temperature since it is lower
		hs.SetDeviceValueByRef(deviceRef, foundTemperature , True)
	End If
End Sub

Private Sub UpdateTempForMaximumValue(ByVal foundTemperature as double, ByVal deviceRef As Integer)
    Dim currentTemp as double=hs.DeviceValueEx(deviceRef)
	If currentTemp<foundTemperature Then
		'Update temperature since it is lower
		hs.SetDeviceValueByRef(deviceRef, foundTemperature , True)
	End If
End Sub