'IMPORTANT:
' Rename this script if you are going to use it. 
' This script will be overwritten with each plug-in update!!!

'strSplit(0) = RF signal OK, | No RF signal,
'strSplit(1) = Status: Stopped|Not Ready|Almost Ready|Ready|Overcooked,
'strSplit(2) = Meat: Beef|Lamb|Veal|Pork|Chicken|Turkey|Fish|Hamburger,
'strSplit(3) = Liking: Rare|Medium Rare|Medium|Well Done,
'strSplit(4) = Target Temp: 73 °C,
'strSplit(5) = Current Temp: 18 °C 

Sub Main(Optional ByVal pParms As String = "")
    Const dvref As Integer = 112 'enter the device reference of the BBQ sensor here!!!!!
    Dim strSplit(5), strTarget, strCurrent As String

    strSplit = Split(hs.DeviceString(dvref), ",")

    If InStr(strSplit(1), "Stopped") = 0 Then
        If InStr(strSplit(1), "Started") <> 0 Then
            strTarget = mid(strSplit(4), instr(strSplit(4), ":") + 2, 3)
            strCurrent = trim(mid(strSplit(5), instr(strSplit(5), ":") + 2, 3))
            hs.speak("Current temperature is " & strCurrent & " degrees and target is " & strTarget & " degrees")

        ElseIf InStr(strSplit(1), "Not Ready") <> 0 Then
            hs.speak("B B Q sensor is switched off")

        ElseIf InStr(strSplit(1), "Almost Ready") <> 0 Then
            hs.speak("Almost ready")

        ElseIf InStr(strSplit(1), "Ready") <> 0 Then
            hs.speak("Your meat is ready, bon apetit")

        ElseIf InStr(strSplit(1), "Overcooked") <> 0 Then
            hs.speak("Your meat is overcooked")

        Else
            hs.speak("Error, Unknown B B Q sensor status")
        End If
    Else
        hs.speak("B B Q sensor status stopped")
    End If

End Sub