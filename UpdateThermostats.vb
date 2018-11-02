
Public Sub Main(ByVal params As Object)
	'split input to get 1-Wanted temperature 1+x the thermostats to change.
	Dim heatPointCelsius As Integer = parameters(0)
	For i As Integer=1 to parameters.Length-1
		Dim deviceRef=parameters(i)
		UpdateTemperatureDevice(heatPointCelsius,deviceRef)
	Next
  Dim eventName As String = parameters(1)
End Sub


Private Sub UpdateTemperatureDevice(ByVal temperature as Integer, ByVal deviceRef As Integer)
    Dim s As New ThermostatSettings(hs, deviceRef)
    'Dim roomTemperature As Double = Math.Round((hs.DeviceValueEx(s.ExternalTemperatureSensorRef) + s.TemperatureCorrectionAddition) * s.TemperatureCorrectionMultiplier, 1)
﻿
    hs.SetDeviceValueByRef(GetChildReference(rootDeviceRef, ThermostatDeviceType.Thermostat_Temperature), temperature, True)
End Sub

