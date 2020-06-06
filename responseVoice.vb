Sub Main(Byval input as Object)
    Dim speakThis as String = hs.ReplaceVariables(input.ToString)

    Dim remoteFile as String = "https://code.responsivevoice.org/getvoice.php?t=" & speakThis & "&tl=no&sv=g2&vn=&pitch=0.5&rate=0.5&vol=1"
    Dim localFile as String = hs.GetAppPath & "\tts.mp3"

    My.Computer.Network.DownloadFile(remoteFile, localFile, "", "", False, 3000, True)
End Sub