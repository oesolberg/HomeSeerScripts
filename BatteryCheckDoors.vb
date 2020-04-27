dim LogName as string="DoorBatteryCheck"
Dim MinimumCharge as Integer=45

Public Sub Main(ByVal params As String)
	If params.Length = 0 Then
		hs.WriteLog(LogName,"No input value found. Exiting script")
		Exit Sub
	End If

	Dim arrInputs() as string
	arrInputs=params.Split(",")

	If arrInputs.Length < 4 Then
		hs.WriteLog(LogName,"Misssing parameters. Exiting script")
		Exit Sub
	End If
	Try
		hs.SetDeviceValueByRef(CInt(arrInputs(0)), 0, True)
		dim i,upper As Integer
		upper=UBound(arrInputs)
		For i=1 To upper
			Dim item as string
			item=arrInputs(i)
			'hs.WriteLog(LogName ," " & item)
			Dim batteryPercent as double=hs.DeviceValueEx(CInt(item))
			hs.WriteLog(LogName ,item & ":" & CStr(batteryPercent))
			If(batteryPercent<MinimumCharge) Then
				hs.SetDeviceValueByRef(arrInputs(0), 100, True)
			End If
		Next
	Catch ex As Exception
		hs.WriteLog(LogName , "ERROR:  Exception in script: " & ex.Message)
	End Try

	hs.WriteLog(LogName , "Battery report for doors done.")
		
End Sub