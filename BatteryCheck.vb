Dim filename As String = "C:\Program Files (x86)\HomeSeer HS3\BatteryReport.html" ' full path and filename of report (if sending report as an email attachment)
	Dim mailTo As String = "oesolberg@hotmail.com" ' email TO address
	Dim mailFrom As String = "homeseer@guahtdim.com" ' email FROM address
	Dim ReportAsAttachment As Boolean = False ' set to 'True' if report should be emailed as a file (attachment), set to 'False' if report should be in email body
	'____ End of User Configuration ____

	Public Sub Main(ByVal param As String)
		Dim myArray(0) As BatteryDevice
		Dim strDeviceString, centerSTR
				
		' Header
		strDeviceString  = "<html><head><style type=" & chr(34) & "text/css" & chr(34) & ">TD{font-family: Arial; font-size: 10pt;}</style></head><body><p><table cellpadding=" & chr(34) & "4" & chr(34) & " WIDTH=" & chr(34) & "100%" & chr(34) & "border=" & chr(34) & "1" & chr(34) & "><tr><th>Ref ID</th><th>Battery %</th><th>Device Location / Name</th><th>Last Updated</th></tr>"
		centerSTR = "<td align=" & chr(34) & "center"  & chr(34) & "valign="  & chr(34) & "middle"  & chr(34) & ">&nbsp;"
					
		Try
			Dim dv As Scheduler.Classes.DeviceClass
			Dim EN As Scheduler.Classes.clsDeviceEnumeration
			EN = hs.GetDeviceEnumerator
			If EN Is Nothing Then
				hs.WriteLog("BatteryReport", "Error getting Enumerator")
				Exit Sub
			End If

			Do
				dv = EN.GetNext
				If dv Is Nothing Then Continue Do

				' Add to array if device type contains 'battery'
				If InStr(UCase(dv.Device_Type_String(Nothing)), "BATTERY") > 1 Then
					 ReDim Preserve myArray(UBound(myArray) + 1)
					myArray(UBound(myArray) - 1) = New BatteryDevice

					myArray(UBound(myArray) - 1).refID = ("[" & dv.Ref(Nothing).ToString & "]")
					If dv.devValue(Nothing) > 100 Then ' if battery value is larger than 100(%), battery is invalid state
						myArray(UBound(myArray) - 1).devValue = "  ERROR"
					Else
						myArray(UBound(myArray) - 1).devValue = (dv.devValue(Nothing).ToString & "%").PadLeft(7)
					End If                    
					myArray(UBound(myArray) - 1).devName = dv.Location2(Nothing) & " " & dv.Location(Nothing) & " " & dv.Name(Nothing)
					myArray(UBound(myArray) - 1).lastChange = dv.Last_Change(Nothing).ToString
					
				End If
			Loop Until EN.Finished

			ReDim Preserve myArray(UBound(myArray) - 1)
			Array.Sort(myArray)
		   
			Dim battdevice As Object
			For Each battdevice In myArray
				strDeviceString = strDeviceString & "<tr>" & centerSTR & battdevice.refID & "</td>" & centerSTR & battdevice.devValue  & "</td>" & "<td>" & battdevice.devName & "</td>" & centerSTR & battdevice.lastChange & "</td></tr>" 
			Next
			
			 ' FOOTER
			 strDeviceString = strDeviceString & "</table></p>"
			 strDeviceString = strDeviceString & " [" & UBound(myArray) + 1 & " Battery Devices] - Report created " & Now.tostring 
			 strDeviceString = strDeviceString & "</body></html>"
			 
			' SEND			
			If ReportAsAttachment Then
				System.IO.File.Delete(filename)
				My.Computer.FileSystem.WriteAllText(filename, strDeviceString, True, System.Text.Encoding.Default)
				hs.SendEmail(mailTo, mailFrom, "", "", "Battery Report", "Battery Report attached.", filename)
			Else
				hs.SendEmail(mailTo, mailFrom, "", "", "Battery Report", strDeviceString, "")
			End If
			
		Catch ex As Exception
			hs.WriteLog("BatteryReport", "ERROR:  Exception in script: " & ex.Message)
		End Try
		hs.WriteLog("BatteryReport", "Battery report created.")
		
	End Sub
	
	Public Class BatteryDevice
		Implements IComparable(Of BatteryDevice)

		Public refID As String
		Public devValue As String
		Public lastChange As String
		Public devName As String

		Public Function CompareTo(ByVal other As BatteryDevice) As Integer Implements IComparable(Of BatteryDevice).CompareTo
			Return Me.devValue.CompareTo(other.devValue)
		End Function
	End Class