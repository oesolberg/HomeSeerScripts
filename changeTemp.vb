'This script is used to fetch a common temperature in first ref in inputArgs. 
'And that is set to all the rest of the refs in the inputArgs
Sub Main (ByVal inputArgs as string)
	'Change temperature by script
	If inputArgs.Length = 0 Then
		hs.WriteLog("test","No input value found. Exiting script")
		Exit Sub
	End If

	Dim arrInputs() as string
	arrInputs=inputArgs.Split(",")

	If arrInputs.Length <= 1 Then
		hs.WriteLog("test","No values found. Exiting script")
		Exit Sub
	End If

	'Get value from first device ref
	Dim setpointDeviceRef as Integer=arrInputs(0)
	Dim tempToSet As Double = hs.DeviceValueEx(setpointDeviceRef)
	hs.WriteLog("test","Current temp: "+tempToSet.tostring())
	For i As Integer=1 to arrInputs.Count-1
		'hs.WriteLog("test",arrInputs(i))
		hs.SetDeviceValueByRef(arrInputs(i), tempToSet, True)
	Next
End Sub