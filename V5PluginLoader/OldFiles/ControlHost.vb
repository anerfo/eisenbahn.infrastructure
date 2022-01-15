Imports V5PluginKompaitibilitaet

Public Class ControlHost
    'Diese Klasse verwaltet ein einzelnes Plugin
    Implements ControlInterface.ControlInterfaceHost

    Public Instanz As ControlInterface.ControlInterfaceClient   'Beinhaltet die Instanz des Controls

    Public TabReference As ControlInterface.TabManagerReference 'Beinhaltet Informationen über das Control für den Tabmanager
    Private Variablen As CLASSVariablen                         'Eine Referenz auf Controls-Variablen

    Public Delegate Sub AktionLoggenDelegate(ByVal Name As String, ByVal Eintrag As String)

    Private AktionLoggen As AktionLoggenDelegate

    Public DarfWeichenSchalten As Boolean = True

    Private thePluginRegistration As PluginRegistration

    Public Sub Load(ByRef Control As ControlInterface.ControlInterfaceClient, ByRef Variables As CLASSVariablen, ByRef LogSub As AktionLoggenDelegate, ByRef thePluginRegistrationRef As PluginRegistration)
        Instanz = Control
        TabReference = Instanz.Init(Me)
        'TabManager.ControlAnmelden(TabReference)
        'Konfiguration.ControlTypAnmelden(TabReference.Name, 1, Instanz.GetControl)
        thePluginRegistration = thePluginRegistrationRef
        thePluginRegistrationRef.ControlAnzeigen(Instanz.GetControl)
        Variablen = Variables
        AktionLoggen = LogSub
    End Sub

    'Ab hier sind die Funktionen, die das Control aufrufen kann
    Public ReadOnly Property Kontakt(ByVal Modulnummer As Integer, ByVal Adresse As Integer) As Object Implements ControlInterface.ControlInterfaceHost.Kontakt
        Get
            Return Eisenbahn.kontakt(Modulnummer, Adresse)
        End Get
    End Property

    Public ReadOnly Property KontaktWechsel(ByVal Modulnummer As Integer, ByVal Adresse As Integer) As Object Implements ControlInterface.ControlInterfaceHost.KontaktWechsel
        Get
            Return Eisenbahn.KontaktWechsel(Modulnummer, Adresse)
        End Get
    End Property

    Public Property Lok(ByVal Adresse As Integer, ByVal Funktion As Definitionen.LokEigenschaften) As Object Implements ControlInterface.ControlInterfaceHost.Lok
        Get
            Select Case Funktion
                Case Definitionen.LokEigenschaften.Geschwindigkeit
                    Return Eisenbahn.LokGeschwindigkeit(Adresse)
                Case Definitionen.LokEigenschaften.Funktion
                    Return Eisenbahn.LokFunktion(Adresse, 0)
                Case Definitionen.LokEigenschaften.F1
                    Return Eisenbahn.LokFunktion(Adresse, 1)
                Case Definitionen.LokEigenschaften.F2
                    Return Eisenbahn.LokFunktion(Adresse, 2)
                Case Definitionen.LokEigenschaften.F3
                    Return Eisenbahn.LokFunktion(Adresse, 3)
                Case Definitionen.LokEigenschaften.F4
                    Return Eisenbahn.LokFunktion(Adresse, 4)
                Case Else
                    Return 0
            End Select
        End Get
        Set(ByVal value As Object)
            AktionLoggen(TabReference.Name, "Lok gesteuert " & Adresse & ": " & value)
            Select Case Funktion
                Case Definitionen.LokEigenschaften.Geschwindigkeit
                    Eisenbahn.LokGeschwindigkeit(Adresse) = value
                Case Definitionen.LokEigenschaften.Funktion
                    Eisenbahn.LokFunktion(Adresse, 0) = value
                Case Definitionen.LokEigenschaften.F1
                    Eisenbahn.LokFunktion(Adresse, 1) = value
                Case Definitionen.LokEigenschaften.F2
                    Eisenbahn.LokFunktion(Adresse, 2) = value
                Case Definitionen.LokEigenschaften.F3
                    Eisenbahn.LokFunktion(Adresse, 3) = value
                Case Definitionen.LokEigenschaften.F4
                    Eisenbahn.LokFunktion(Adresse, 4) = value
            End Select
        End Set
    End Property

    Public Property Weiche(ByVal Weichenadresse As Object) As Definitionen.Weichen_Zustaende Implements ControlInterface.ControlInterfaceHost.Weiche
        Get
            Return Eisenbahn.Weiche(Weichenadresse)
        End Get
        Set(ByVal value As Definitionen.Weichen_Zustaende)
            If Eisenbahn.Nothalt = False And DarfWeichenSchalten = True Then
                Eisenbahn.WeicheSchalten(Weichenadresse, value)
                AktionLoggen(TabReference.Name, "Weiche " & Weichenadresse & " geschalten")
            End If
        End Set
    End Property

    Public Property Variable(ByVal Variablenname As String) As Object Implements ControlInterface.ControlInterfaceHost.Variable
        Get
            Return Variablen(Variablenname)
        End Get
        Set(ByVal Value As Object)
            Variablen(Variablenname) = Value
            AktionLoggen(TabReference.Name, "Variable " & Variablenname & " = " & Value)
        End Set
    End Property

    Public Property nothalt() As Boolean Implements ControlInterface.ControlInterfaceHost.nothalt
        Get
            Return Eisenbahn.Nothalt
        End Get
        Set(ByVal value As Boolean)
            Eisenbahn.Nothalt = value
            AktionLoggen(TabReference.Name, "Nothalt ausgelöst")
        End Set
    End Property

    Public Sub LokUmdrehen(ByVal Adresse As Integer) Implements ControlInterface.ControlInterfaceHost.LokUmdrehen
        Eisenbahn.LokUmdrehen(Adresse)
        AktionLoggen(TabReference.Name, "Lok umgedreht: " & Adresse)
    End Sub

    Public Sub TastaturbefehlAnmelden(ByVal e As System.Windows.Forms.Keys, ByVal AufzurufendeFunktion As ControlInterface.KeyHandling.DELEGATEKeyHandleSub) Implements ControlInterface.ControlInterfaceHost.TastaturbefehlAnmelden
        'Eisenbahn.TastaturbefehlAnmelden(Me, e, AufzurufendeFunktion)
    End Sub
End Class
