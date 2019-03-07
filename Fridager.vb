Sub Main(ByVal deviceReference As String)
        'Moskus 2017
		hs.WriteLog( "Info", "Fridager: Starter script")
        Dim devRef As Integer = Integer.Parse(deviceReference.ToString)
		hs.WriteLog( "Info", "Fridager: Behandler dato: " & Year(Now) & "-" & Month(Now) & "-" & Day(Now) )
        Dim isCurrentDateHoliday As Boolean = False
        If Not isCurrentDateHoliday Then isCurrentDateHoliday = IsOfficialHoliday(Now) 'Sjekker offisielle helligdager
        If Not isCurrentDateHoliday Then isCurrentDateHoliday = IsCustomHoliday(Now)   'Sjekker egen-spesifiserte dager

        If isCurrentDateHoliday Then
            hs.CAPIControlHandler(hs.CAPIGetSingleControlByUse(devRef, HomeSeerAPI.ePairControlUse._On))
			hs.WriteLog( "Info", "Fridager: Satte til fridag")
        Else
            hs.CAPIControlHandler(hs.CAPIGetSingleControlByUse(devRef, HomeSeerAPI.ePairControlUse._Off))
			hs.WriteLog( "Info", "Fridager: Satte til arbeidsdag")
        End If

    End Sub

    Public Function IsCustomHoliday(ByVal _date As Date) As Boolean
        _date = _date.Date 'In case time was added

        'For eksempel sjekkeom det er en lørdag eller søndag
        If _date.DayOfWeek = DayOfWeek.Saturday Then Return True
        If _date.DayOfWeek = DayOfWeek.Sunday Then Return True


        'Jeg har alltid fri i romjulen
        If _date.Month = 12 Then
            If _date.Day >= 27 AndAlso _date.Day <= 31 Then
                Return True
            End If
        End If


        'Andre spesifikke dager kan legges i listen, f.eks. har barnehagen planleggingsdager
        Dim dateList As New System.Collections.Generic.List(Of Date)
        dateList.Add(New Date(2017, 4, 18))
		dateList.Add(New Date(2018, 3, 26))
        dateList.Add(New Date(2018, 3, 27))
        dateList.Add(New Date(2018, 3, 28))
		dateList.Add(New Date(2018, 5, 10))
        If dateList.Contains(_date) Then Return True


        'Har vi kommet så langt er det ingen fridager, returner "False"
        Return False
    End Function

    Public Function IsOfficialHoliday(ByVal _date As Date) As Boolean
        'translated php script form xibriz: https://www.diskusjon.no/index.php?showtopic=1084239

        _date = _date.Date 'in case time was added
        Dim easterDate As Date = GetEasterDate(_date.Year)

        'Sjekker om datoen er 1. Januar
        If _date = New Date(_date.Year, 1, 1) Then Return True

        'Sjekker om datoen er pamlesøndag (1. påskedag - 7 dager)
        If _date = easterDate.AddDays(-7) Then Return True

        'Sjekker om datoen er skjærtorsdag (1. påskedag - 3 dager)
        If _date = easterDate.AddDays(-3) Then Return True

        'Sjekker om datoen er langfredag (1. påskedag - 2 dager)
        If _date = easterDate.AddDays(-2) Then Return True

        'Sjekker om datoen er 1. påskedag
        If _date = easterDate Then Return True

        'Sjekker om datoen er 2. påskedag (1. påskedag + 1 dag)
        If _date = easterDate.AddDays(1) Then Return True

        'Sjekker om datoen er 1. mai (offentlig høytidsdag)
        If _date = New Date(_date.Year, 5, 1) Then Return True

        'Sjekker om datoen er 17. mai (grunnlovsdag)
        If _date = New Date(_date.Year, 5, 17) Then Return True

        'Sjekker om datoen er kristi himmelfartsdag (40. påskedag)
        If _date = easterDate.AddDays(39) Then Return True

        'Sjekker om datoen er 1. pinsedag (50. påskedag)
        If _date = easterDate.AddDays(49) Then Return True

        'Sjekker om datoen er 2 pinsedag (51. påskedag)
        If _date = easterDate.AddDays(50) Then Return True

        'Sjekker om datoen er 1. juledag (25. desember)
        If _date = New Date(_date.Year, 12, 25) Then Return True

        'Sjekker om datoen er 2 juledag (26. desember)
        If _date = New Date(_date.Year, 12, 26) Then Return True

        Return False
    End Function

    Public Function GetEasterDate(ByVal Year As Integer) As Date
        'Originally taken from: http://www.thoughtproject.com/Snippets/Easter/Easter.vb.txt

        Dim a As Integer
        Dim b As Integer
        Dim c As Integer
        Dim d As Integer
        Dim e As Integer
        Dim f As Integer
        Dim g As Integer
        Dim h As Integer
        Dim i As Integer
        Dim k As Integer
        Dim l As Integer
        Dim m As Integer
        Dim n As Integer
        Dim p As Integer

        If Year < 1583 Then
            Return Nothing
        Else

            ' Step 1: Divide the year by 19 and store the
            ' remainder in variable A.  Example: If the year
            ' is 2000, then A is initialized to 5.

            a = Year Mod 19

            ' Step 2: Divide the year by 100.  Store the integer
            ' result in B and the remainder in C.

            b = Year \ 100
            c = Year Mod 100

            ' Step 3: Divide B (calculated above).  Store the
            ' integer result in D and the remainder in E.

            d = b \ 4
            e = b Mod 4

            ' Step 4: Divide (b+8)/25 and store the integer
            ' portion of the result in F.

            f = (b + 8) \ 25

            ' Step 5: Divide (b-f+1)/3 and store the integer
            ' portion of the result in G.

            g = (b - f + 1) \ 3

            ' Step 6: Divide (19a+b-d-g+15)/30 and store the
            ' remainder of the result in H.

            h = (19 * a + b - d - g + 15) Mod 30

            ' Step 7: Divide C by 4.  Store the integer result
            ' in I and the remainder in K.

            i = c \ 4
            k = c Mod 4

            ' Step 8: Divide (32+2e+2i-h-k) by 7.  Store the
            ' remainder of the result in L.

            l = (32 + 2 * e + 2 * i - h - k) Mod 7

            ' Step 9: Divide (a + 11h + 22l) by 451 and
            ' store the integer portion of the result in M.

            m = (a + 11 * h + 22 * l) \ 451

            ' Step 10: Divide (h + l - 7m + 114) by 31.  Store
            ' the integer portion of the result in N and the
            ' remainder in P.

            n = (h + l - 7 * m + 114) \ 31
            p = (h + l - 7 * m + 114) Mod 31

            ' At this point p+1 is the day on which Easter falls.
            ' n is 3 for March or 4 for April.

            Return DateSerial(Year, n, p + 1)

        End If

    End Function