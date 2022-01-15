
Public Module Eisenbahn



    Public Property kontakt(ByVal Modul As Integer, ByVal Adresse As Integer, Optional ByVal NotSimulated As Boolean = False)
        Get
            Dim value As Klassen.Kontakt = V5PluginKompatibilitaet.eb.rueckmeldung(Modul, Adresse)
            If value Is Nothing Then
                Return 0
            End If
            If value.status Then
                Return 1
            Else
                Return 0
            End If
        End Get
        Set(ByVal value)

        End Set
    End Property

    Public ReadOnly Property KontaktWechsel(ByVal Modul As Integer, ByVal Adresse As Integer, Optional ByVal NotSimulated As Boolean = False)
        Get
            Dim k As Klassen.Kontakt() = V5PluginKompatibilitaet.eb.holeAlleGeaendertenKontakte()
            For i As Integer = 0 To k.Length - 1
                If k(i).Modul = Modul And k(i).Adresse = Adresse Then
                    Return 1

                End If
            Next
            Return 0
        End Get
    End Property

    Public Property Weiche(ByVal Weichenadresse As Integer) As definitionen.Weichen_Zustaende
        Get
            If V5PluginKompatibilitaet.eb.weiche(Weichenadresse).Richtung = Klassen.WeichenRichtung.links Then
                Return definitionen.Weichen_Zustaende.Links
            Else
                Return definitionen.Weichen_Zustaende.Rechts
            End If
        End Get
        Set(ByVal value As definitionen.Weichen_Zustaende)
            If value = definitionen.Weichen_Zustaende.Links Or value = definitionen.Weichen_Zustaende.Gruen Then
                V5PluginKompatibilitaet.eb.weicheSchalten(Weichenadresse, Klassen.WeichenRichtung.links)
            ElseIf value = definitionen.Weichen_Zustaende.Rechts Or value = definitionen.Weichen_Zustaende.Rot Then
                V5PluginKompatibilitaet.eb.weicheSchalten(Weichenadresse, Klassen.WeichenRichtung.rechts)
            Else
                If V5PluginKompatibilitaet.eb.weiche(Weichenadresse).Richtung = Klassen.WeichenRichtung.rechts Then
                    V5PluginKompatibilitaet.eb.weicheSchalten(Weichenadresse, Klassen.WeichenRichtung.links)
                Else
                    V5PluginKompatibilitaet.eb.weicheSchalten(Weichenadresse, Klassen.WeichenRichtung.rechts)
                End If
            End If
        End Set
    End Property

    Public Function WeicheSchalten(ByVal Weichenadresse As Integer, ByVal Richtung As definitionen.Weichen_Zustaende)
        Weiche(Weichenadresse) = Richtung
        Return 1
    End Function

    Public Sub LokUmdrehen(ByVal Loknummer As Integer)
        V5PluginKompatibilitaet.eb.lokUmdrehen(Loknummer)
    End Sub

    Public Property LokGeschwindigkeit(ByVal Loknummer As Integer)
        Get
            Return V5PluginKompatibilitaet.eb.lok(Loknummer).Geschwindigkeit
        End Get
        Set(ByVal Geschwindigkeit)
            V5PluginKompatibilitaet.eb.lokSteuern(Loknummer, Klassen.LokEigenschaften.Geschwindigkeit, Geschwindigkeit)
        End Set
    End Property

    Public Property LokFunktion(ByVal Loknummer As Integer, ByVal Funktion As Integer)
        Get
            If Funktion = 1 Then
                Return V5PluginKompatibilitaet.eb.lok(Loknummer).Funktion1
            ElseIf Funktion = 2 Then
                Return V5PluginKompatibilitaet.eb.lok(Loknummer).Funktion2
            ElseIf Funktion = 3 Then
                Return V5PluginKompatibilitaet.eb.lok(Loknummer).Funktion3
            ElseIf Funktion = 4 Then
                Return V5PluginKompatibilitaet.eb.lok(Loknummer).Funktion4
            Else
                Return V5PluginKompatibilitaet.eb.lok(Loknummer).Hauptfunktion
            End If
        End Get
        Set(ByVal value)
            If Funktion = 1 Then
                V5PluginKompatibilitaet.eb.lokSteuern(Loknummer, Klassen.LokEigenschaften.Funktion1, value)
            ElseIf Funktion = 2 Then
                V5PluginKompatibilitaet.eb.lokSteuern(Loknummer, Klassen.LokEigenschaften.Funktion2, value)
            ElseIf Funktion = 3 Then
                V5PluginKompatibilitaet.eb.lokSteuern(Loknummer, Klassen.LokEigenschaften.Funktion3, value)
            ElseIf Funktion = 4 Then
                V5PluginKompatibilitaet.eb.lokSteuern(Loknummer, Klassen.LokEigenschaften.Funktion4, value)
            Else
                V5PluginKompatibilitaet.eb.lokSteuern(Loknummer, Klassen.LokEigenschaften.Hauptfunktion, value)
            End If
        End Set
    End Property

    Public Property Nothalt() As Boolean
        Get
            Return V5PluginKompatibilitaet.eb.nothalt
        End Get
        Set(ByVal Status As Boolean)
            V5PluginKompatibilitaet.eb.nothalt = Status
        End Set
    End Property

    'Funktionen um Informationen aus dem Modul 'Daten' zu bekommen:
    Public ReadOnly Property Data(ByVal Spalte As Integer, ByVal Zeile As Integer) As String
        Get
            Return V5PluginKompatibilitaet.dataStorage.read_from_table(Spalte, Zeile)
        End Get
    End Property


    Public Property Data(ByVal Spalte As String, ByVal Zeile As Integer)
        Get
            Return V5PluginKompatibilitaet.dataStorage.read_from_table(Spalte, Zeile)
        End Get
        Set(ByVal value)
            If TypeOf value Is String Then
                V5PluginKompatibilitaet.dataStorage.write_to_table(Spalte, Zeile, CType(value, String))
            ElseIf TypeOf value Is Integer Then
                V5PluginKompatibilitaet.dataStorage.write_to_table(Spalte, Zeile, CType(value, Integer))
            Else
                V5PluginKompatibilitaet.dataStorage.write_to_table(Spalte, Zeile, CType(value, Boolean))
            End If
        End Set
    End Property

    'Meldung ausgeben
    Public Sub Meldung(ByVal sender As Object, ByVal typ As definitionen.MeldungTyp, ByVal text As String, Optional ByVal LogOnly As Boolean = False)

    End Sub

    ''Tastenbefehle anmelden die über das Control herausgehen
    'Public Sub TastaturbefehlAnmelden(ByRef HostOfControl As ControlHost, ByVal Key As Keys, ByRef Method As ControlInterface.KeyHandling.DELEGATEKeyHandleSub)

    'End Sub

    'Hier die Update-Routinen einfügen
    Public Sub Update(ByVal Typ As definitionen.Typen, Optional ByVal Variable As Object = Nothing)

    End Sub

    'Variable für Controls
    Public Property Variable(ByVal Variablenname As String) As Object
        Get
            Return Nothing
        End Get
        Set(ByVal value As Object)
        End Set
    End Property

End Module
