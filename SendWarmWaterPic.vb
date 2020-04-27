Dim filename As String = "C:\Program Files (x86)\HomeSeer HS3\BatteryReport.html" ' full path and filename of report (if sending report as an email attachment)
	Dim mailTo As String = "oesolberg@hotmail.com" ' email TO address
	Dim mailFrom As String = "homeseer@guahtdim.com" ' email FROM address
	Dim ReportAsAttachment As Boolean = False ' set to 'True' if report should be emailed as a file (attachment), set to 'False' if report should be in email body
	'____ End of User Configuration ____

	
	Public Sub Main(ByVal param As String)
				
	
		Try
				'System.IO.File.Delete(filename)
				'My.Computer.FileSystem.WriteAllText(filename, strDeviceString, True, System.Text.Encoding.Default)
Dim rotationValue As Double= hs.DeviceValueEx(265)
Dim probabilityValue As Double= hs.DeviceValueEx(266)

        
				hs.SendEmail(mailTo, mailFrom, "Trykk varmtvannstank", "", "Warm water image", "Rotation: "+ CStr(rotationValue) +", probability: "+ CStr(probabilityValue) + vbCrLf + "Image attached.", "D:\ftp\WarmWaterPix\currentImage\currentImage.jpg")
			
						
		Catch ex As Exception
			hs.WriteLog("SendEmail warmwater", "ERROR:  Exception in script: " & ex.Message)
		End Try
		hs.WriteLog("SendEmail warmwater", "Warm water image sendt.")
		
	End Sub