Namespace definitionen
    Public Enum MeldungTyp
        Nachricht
        Warnung
        Fehler
    End Enum

    Public Enum Weichen_Zustaende                          'Mögliche Zustände für Weichen
        Rechts              '1 entspricht rechts
        Links               '0 entspricht links
        Rot                 'entspricht 0
        Gruen               'entspricht 1
        None
    End Enum

    Public Enum Typen
        None
        Weiche
        Strecke
        Kontakt
    End Enum

    Public Enum LokEigenschaften
        Geschwindigkeit
        Funktion
        F1
        F2
        F3
        F4
    End Enum

    Public Module Converts
        'Diese Funktion wandelt einen String in einen WeichenZustand um
        Public Function StringToWeichenZustände(ByVal Text As String) As Definitionen.Weichen_Zustaende
            If Strings.UCase(Text) = "LINKS" Then
                Return Definitionen.Weichen_Zustaende.Links
            ElseIf Strings.UCase(Text) = "GRÜN" Then
                Return Definitionen.Weichen_Zustaende.Gruen
            ElseIf Strings.UCase(Text) = "ROT" Then
                Return Definitionen.Weichen_Zustaende.Rot
            ElseIf Strings.UCase(Text) = "RECHTS" Then
                Return Definitionen.Weichen_Zustaende.Rechts
            Else
                Return Definitionen.Weichen_Zustaende.None
            End If
        End Function

        'Diese Funktion wandelt einen String in einen Typ von einem Bild um
        Public Function StringToTypen(ByVal Text As String) As Definitionen.Typen
            If Strings.UCase(Text) = "KONTAKT" Or Strings.UCase(Text) = "KONTAKTE" Then
                Return Definitionen.Typen.Kontakt
            ElseIf Strings.UCase(Text) = "STRECKE" Or Strings.UCase(Text) = "STRECKEN" Then
                Return Definitionen.Typen.Strecke
            ElseIf Strings.UCase(Text) = "WEICHE" Or Strings.UCase(Text) = "WEICHEN" Then
                Return Definitionen.Typen.Weiche
            Else
                Return Definitionen.Typen.None
            End If
        End Function
    End Module


    Public Structure Loks                           'Struktur des Lok-Arrays
        Public Geschwindigkeit As Integer           'letzte gesendete Geschwindigkeit der Lok
        Public Soll_Geschwindigkeit                 'Geschwindigkeit die Lok annehmen soll
        Public Funktion() As Boolean                'wird später noch für die 5 Funktionen dimensioniert
    End Structure

    Public Structure Weichen                        'Struktur des Weichenarrays
        Public Status As Integer                    'Richtung in die Weiche gerade geschalten ist
        Public Schaltdauer As Integer               'Dauer die die Weiche geschalten werden soll
        Public Umdrehen As Boolean                  '=true wenn 0 guener Taste auf Keyboard entspricht
        Public Startzustand As Definitionen.Weichen_Zustaende    'speichert in welchem Zustand die Weiche am Anfang des Programmes stehen soll
    End Structure

    Public Class Constants
        Public Shared AnzahlRückmeldemodule = Klassen.Konstanten.AnzahlRueckmeldeModule                         'Anzahl möglicher angeschlossener Rückmeldemodule
        Public Shared AnzahlAnschlüsseRückmeldmodul = Klassen.Konstanten.AnzahlAnschluesseProRueckmeldemodul    'Anzahl Anschlüsse an einem Rückmeldemodul
        Public Shared Angeschlossene_Rueckmeldemodule As Integer = Klassen.Konstanten.AnzahlRueckmeldeModule    'Konstante: wieviele Rückmeldemodule ausgelesen werden sollen
        Public Shared anzahl_weichen As Integer = Klassen.Konstanten.AnzahlWeichen                              'Wieviel Weichen tatsächlich auf der Anlage sind (Max. 255!!)
        Public Shared AnzahlLoks As Integer = Klassen.Konstanten.AnzahlLoks
    End Class


End Namespace
