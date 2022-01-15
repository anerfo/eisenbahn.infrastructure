Public Interface ControlInterfaceHost
    'dies ist das Interface über welches das Plugin Aktionen im Hauptprogramm auslösen kann

    Property Weiche(ByVal Weichenadresse) As Definitionen.Weichen_Zustaende

    ReadOnly Property Kontakt(ByVal Modulnummer As Integer, ByVal Adresse As Integer)

    ReadOnly Property KontaktWechsel(ByVal Modulnummer As Integer, ByVal Adresse As Integer)

    Property Lok(ByVal Adresse As Integer, ByVal Funktion As Definitionen.LokEigenschaften)

    Property Variable(ByVal Variablenname As String) As Object

    Property nothalt() As Boolean

    Sub LokUmdrehen(ByVal Adresse As Integer)

    Sub TastaturbefehlAnmelden(ByVal Key As System.Windows.Forms.Keys, ByVal AufzurufendeFunktion As KeyHandling.DELEGATEKeyHandleSub)
End Interface
