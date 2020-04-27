Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Collections.Generic


dim _logName as string="KolonialOldenEple"

Sub Main(ByVal parameters As Object)
	Dim input() As String = parameters.ToString.Split(",")
	Dim deviceRef As Integer = input(0).Trim
	hs.WriteLog(_logName, "deviceRef " & deviceRef )
	Dim url As String = "https://kolonial.no/produkter/21814-olden-olden-eple-med-kullsyre/"
	hs.WriteLog(_logName, "url " & url)
	Dim source As String = ""
	Try
		Using client As New System.Net.WebClient                
			Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12
			client.Encoding = System.Text.Encoding.UTF8
			source = client.DownloadString(url)
		End Using
	Catch ex As Exception
		hs.WriteLog(_logName, "Net Error: " & ex.Message)
	End Try
	
	If source = "" Then
		hs.WriteLog(_logName, "Got no response from url: " & url)
		Exit Sub
	End If
	'hs.WriteLog(_logName, "source " & source)
	
	Try
		If InStr(source,"<span class=""hidden-xs"">Utsolgt fra leverandør</span>")>0 Then
			hs.SetDeviceString(deviceRef, "Olden eple utsolgt" , True)
			hs.SetDeviceValueByRef(deviceRef, 0, True)
		Else
			hs.SetDeviceString(deviceRef, "Olden eple lager" , True)
			hs.SetDeviceValueByRef(deviceRef, 100, True)
		End If		
	
	Catch ex As Exception
		hs.WriteLog(_logName, "Net Error: " & ex.Message)
	End Try
End Sub