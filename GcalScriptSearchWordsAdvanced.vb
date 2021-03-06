﻿Sub Main(ByVal Parms As Object)
        Dim msg As String = ""
        Try
            Dim ParmArray() As String
            ParmArray = Parms.ToString.Split(",")
            Dim deviceID = ParmArray(0)
            Dim calendarName = ParmArray(1)
            Dim numberOfDaysToSubtract = CInt(ParmArray(2))
            Dim numberOfDaysToAdd = CInt(ParmArray(3))
            Dim wordsToLookFor As String() 
            wordsToLookFor= ParmArray(4).ToString.Split("|")
            Dim allEvents As Object

            allEvents = hs.PluginFunction("GCalSeer", "", "GetItemsFromCalendarAdvanced", New Object() {calendarName,numberOfDaysToSubtract, numberOfDaysToAdd})

            Dim counter As Integer = 0
            For Each ev As Object In allEvents

                If WordExists(ev.Subject,wordsToLookFor) Then

                    counter += 1
                    Dim line As String = ""

                    If True Then
                       
                        If ev.StartDateTimeLocal.Date = Now.Date.AddDays(-1) Then
                            If ev.AllDayEvent Then
                                line = "Igår "
                            Else
                                line = "Igår Kl. "
                            End If
                        End If


                        If ev.StartDateTimeLocal.Date = Now.Date Then
                            If ev.AllDayEvent Then
                                line = "Dagens "
                            Else
                                line = "Kl. "
                            End If
                        End If

                        If ev.StartDateTimeLocal.Date = Now.Date.AddDays(1) Then
                            If ev.AllDayEvent Then
                                line = "I morgen "
                            Else
                                line = "I morgen kl. "
                            End If
                        End If

                        If ev.StartDateTimeLocal.Date > Now.Date.AddDays(1) Or ev.StartDateTimeLocal.Date < Now.Date.AddDays(-1)  Then
                           If ev.AllDayEvent Then
                                line &= Format(ev.StartDateTimeLocal, "ddd dd.MM") & " (hele dagen) - " & ev.Subject
                            Else
                                line &= Format(ev.StartDateTimeLocal, "ddd dd.MM HH:mm") & " - " & ev.Subject
                            End If
                        else
If ev.AllDayEvent Then
                                line &= ev.Subject
                            Else
                                line &= Format(ev.StartDateTimeLocal, "HH:mm") & " - " & ev.Subject
                            End If
End If

                        If ev.StartDateTimeLocal.Date = Now.Date AndAlso numberOfDaysToAdd > 1 Then line = "<b>" & line & "</b>"
                        msg &= line & "<br>"

                    End If
                End If

            Next

            If msg = "" Then
                msg = "Ingen hendelser"
            End If
            hs.SetDeviceString(deviceID, msg, True)

        Catch ex As Exception
            hs.WriteLog("", "Exception in script: " & ex.Message)
        End Try

    End Sub

Public Function WordExists(ByVal subject As String, wordsToSearchForArray As String() ) As Boolean
	 For Each wordToSearchFor As String In wordsToSearchForArray
		If subject.ToLower().Contains(wordToSearchFor.ToLower()) Then
			Return True
		End If
	 Next 
	Return False
 End Function