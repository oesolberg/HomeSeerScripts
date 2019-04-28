	dim  logName as string="Guahtdim_StoreAndGetDeviceValueFromIniScript"
	' dim deviceRef as Integer=19
	dim filename as string="DeviceState_defaultTemp.ini"
	Sub Main(ByVal inputArgs as string)
		'Get value
		' dim deviceValue as double= hs.DeviceValueEx(deviceRef)
        'Write the list to file
		' hs.SaveINISetting("Devices", deviceRef, deviceValue , filename)
		 Dim lines() As String = hs.GetINISectionEx("Devices", filename)
        For Each line As String In lines
            Dim deviceRef As Integer = line.Split("=")(0).Trim
            Dim deviceValue As Double = line.Split("=")(1).Trim

			Dim CAPIcontrol As HomeSeerAPI.CAPIControl = Nothing
            For Each cc As HomeSeerAPI.CAPIControl In hs.CAPIGetControl(deviceRef)
                If deviceValue = cc.ControlValue Then
                    CAPIcontrol = cc
                    Exit For
                End If
            Next
            '... And execute it
            hs.CAPIControlHandler(CAPIcontrol)
        Next
    End Sub