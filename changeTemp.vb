Sub Main (ByVal inputArgs as string)
'Originally devices 412,74,261,236,228,522
'412=device med satt temperatur(Heating temperature living rooms),74=stue sør,261=stue vest,236=spisestue,228=kjøkken,522=Varmepumpe spisestue
	'Change temperature by script
	If inputArgs.Length = 0 Then
		hs.WriteLog("ChangeTemp","No input value found. Exiting script")
		Exit Sub
	End If

	Dim arrInputs() as string
	arrInputs=inputArgs.Split(",")

	If arrInputs.Length <= 1 Then
		hs.WriteLog("ChangeTemp","No values found. Exiting script")
		Exit Sub
	End If

	'Get value from first device ref
	Dim setpointDeviceRef as Integer=arrInputs(0)
	Dim tempToSet As Double = hs.DeviceValueEx(setpointDeviceRef)
	hs.WriteLog("ChangeTemp","New temp: "+tempToSet.tostring())
	For i As Integer=1 to arrInputs.Count-1
		'hs.WriteLog("ChangeTemp",arrInputs(i))
		hs.SetDeviceValueByRef(arrInputs(i), tempToSet, True)
		hs.CAPIControlHandler(hs.CAPIGetSingleControl(arrInputs(i),false,tempToSet,false,true))
	Next
End Sub