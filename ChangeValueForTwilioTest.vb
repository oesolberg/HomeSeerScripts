Dim  logName as string="Guahtdim_SetValue"
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

	If arrInputs.Length < 1 Then
		hs.WriteLog(logName,"Misssing parameters. Exiting script")
		Exit Sub
	End If

	'Get value from first device ref
	Dim deviceReference as Integer = arrInputs(0)
	dim doublevalue as double = Datetime.Now.ToString("ddhhmmss")
	dim extractedResult as string = "(47) "  & DateTime.Now.ToString("ddHHmmss")
	
	hs.SetDeviceValueByRef(deviceReference, doubleValue, True)
	hs.SetDeviceString(deviceReference, extractedResult, True)
	hs.SetDeviceLastChange(deviceReference, DateTime.Now)
	
	
End Sub


