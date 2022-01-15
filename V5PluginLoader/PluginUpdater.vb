Imports V5PluginKompaitibilitaet

Public Class PluginUpdater
    Implements Communication.KontaktEventUpdateInterface
    Implements Communication.LokEventUpdateInterface
    Implements Communication.NothaltEventUpdateInterface
    Implements Communication.WeichenEventUpdateInterface

    Private theControlsRef As Controls

    Public Sub init(ByVal ControlsRef As Controls)
        theControlsRef = ControlsRef
    End Sub

    Public Sub update(ByVal Kontakte() As Klassen.Kontakt) Implements Communication.KontaktEventUpdateInterface.update
        theControlsRef.Updaten(Definitionen.Typen.Kontakt)
    End Sub

    Public Sub update1(ByVal Lok() As Klassen.Lok) Implements Communication.LokEventUpdateInterface.update
        theControlsRef.Updaten(Definitionen.Typen.Strecke)
    End Sub

    Public Sub update2(ByVal status As Boolean) Implements Communication.NothaltEventUpdateInterface.update
        theControlsRef.Updaten(Definitionen.Typen.None)
    End Sub

    Public Sub update3(ByVal Weiche() As Klassen.Weiche) Implements Communication.WeichenEventUpdateInterface.update
        theControlsRef.Updaten(Definitionen.Typen.Weiche)
    End Sub
End Class
