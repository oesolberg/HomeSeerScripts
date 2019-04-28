	dim  logName as string="Guahtdim_StoreAndGetDeviceValueFromIniScript"
	 
	Sub Main(ByVal inputArgs as string)
		If inputArgs.Length = 0 Then
			hs.WriteLog(logName,"No input value found. Exiting script")
			Exit Sub
		End If

		Dim arrInputs() as string
		arrInputs=inputArgs.Split(",")

		If arrInputs.Length <= 2 Then
			hs.WriteLog(logName,"No values found. Exiting script")
			Exit Sub
		End If

 
        'Specify a configuration name. This is used when calling "Save" and "Load" later.
        Dim config_name As String = arrInputs(0)
		Dim devicesList As New List(Of Integer)
		For i As Integer = 1 To arrInputs.Lengt()
            devicesList.Add(arrInputs(i)
        Next
		
        'Creating initial ini file and store it in a list
        ' Dim lst As New System.Collections.Generic.List(Of DeviceState)
        For Each d As Integer In devicesList
            Dim dS As New DeviceState
            dS.deviceRef = d
            dS.deviceValue = hs.DeviceValueEx(d)
            lst.Add(dS)
        Next

        'Write the list to file
        SaveToFile(config_name, lst)

    End Sub

	Sub SaveToFile(ByVal config_name As String, ByVal stateList As System.Collections.Generic.List(Of DeviceState))
        Dim filename As String = "DeviceState_" & config_name & ".ini"
        For Each d As DeviceState In stateList
            hs.SaveINISetting("Devices", d.deviceRef, d.deviceValue, filename)
        Next
    End Sub
	
    Sub Save(ByVal config_name As String)
        'Get the device list
        Dim lst As System.Collections.Generic.List(Of DeviceState) = LoadFromFile(config_name)

        'Get the current device values for each device
        For Each d As DeviceState In lst
            d.deviceValue = hs.DeviceValueEx(d.deviceRef)
        Next

        'Store the list
        SaveToFile(config_name, lst)
    End Sub

    Sub Load(ByVal config_name As String)
        'Get the device list
        Dim lst As System.Collections.Generic.List(Of DeviceState) = LoadFromFile(config_name)

        For Each d As DeviceState In lst
            'Find the correct CAPI based on device value...
            Dim CAPIcontrol As HomeSeerAPI.CAPIControl = Nothing
            For Each cc As HomeSeerAPI.CAPIControl In hs.CAPIGetControl(d.deviceRef)
                If d.deviceValue = cc.ControlValue Then
                    CAPIcontrol = cc
                    Exit For
                End If
            Next

            '... And execute it
            hs.CAPIControlHandler(CAPIcontrol)
        Next
    End Sub

    Function LoadFromFile(ByVal config_name As String) As System.Collections.Generic.List(Of DeviceState)
        Dim lst As New System.Collections.Generic.List(Of DeviceState)
        Dim filename As String = "DeviceState_" & config_name & ".ini"

        Dim lines() As String = hs.GetINISectionEx("Devices", filename)
        For Each line As String In lines
            Dim deviceRef As Integer = line.Split("=")(0).Trim
            Dim deviceValue As Double = line.Split("=")(1).Trim

            lst.Add(New DeviceState(deviceRef, deviceValue))
        Next


        Return lst
    End Function


﻿

    <Serializable>
    Public Class DeviceState
        Public Property deviceRef As Integer
        Public Property deviceValue As Double
        Public Sub New()
        End Sub

        Public Sub New(ByVal _deviceRef As Integer, ByVal _deviceValue As Double)
            Me.deviceRef = _deviceRef
            Me.deviceValue = _deviceValue
        End Sub
    End Class