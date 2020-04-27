Sub Main(ByVal params As String) 

dim s, data, title, message 
Dim paramArr() 
const server_url = "https://api.pushbullet.com/api/pushes" 
const headers="Authorization: Basic by4zYVZ4ZWtpQkdEczFvZ1V4dVpieHB5QUpTRU1IRjJ6UQ==" 

paramArr = params.Split("#") 
title = paramArr(0) 
message = paramArr(1) 

data = "device_iden=ujvaHlAAzh6&type=note&title="+title+"&body="+message 

s = hs.URLAction(server_url, "POST", data, headers) 

End Sub