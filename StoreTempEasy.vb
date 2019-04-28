	dim  logName as string="Guahtdim_StoreAndGetDeviceValueFromIniScript"
	dim deviceRef as Integer=19
	dim filename as string="DeviceState_defaultTemp.ini"
	Sub Main(ByVal inputArgs as string)
		'Get value
		dim deviceValue as double= hs.DeviceValueEx(deviceRef)
        'Write the list to file
		hs.SaveINISetting("Devices", deviceRef, deviceValue , filename)
		
    End Sub