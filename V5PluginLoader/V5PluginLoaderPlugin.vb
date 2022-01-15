Public Class V5PluginLoaderPlugin
    Implements PluginManagerLibrary.PluginInterface

    Private thePluginRegistration As New PluginRegistration
    Private theControlManager As New Controls(thePluginRegistration)
    Private thePluginUpdater As New PluginUpdater

    Public ReadOnly Property beschreibung As String Implements PluginManagerLibrary.PluginInterface.beschreibung
        Get
            Return "Ermöglicht das Laden von V5-Plugins"
        End Get
    End Property

    Public ReadOnly Property name As String Implements PluginManagerLibrary.PluginInterface.name
        Get
            Return "V5 Plugin Loader"
        End Get
    End Property

    Public Function pluginInitalisieren() As Object() Implements PluginManagerLibrary.PluginInterface.pluginInitalisieren
        Return Nothing
    End Function

    Public Sub pluginStarten(ByVal Referenz As PluginManagerLibrary.InterfaceFuerPlugins) Implements PluginManagerLibrary.PluginInterface.pluginStarten
        Dim eb As Communication.KernelInterface
        Dim dummystart As V5PluginKompaitibilitaet.DummyStartInterface = Referenz.getReferenceToObject("V5PluginKompaitibilitaet.DummyStartInterface", Me)

        thePluginRegistration.init(Referenz)
        theControlManager.Load(Strings.Left(System.Reflection.Assembly.GetEntryAssembly.Location, Strings.InStrRev(System.Reflection.Assembly.GetEntryAssembly.Location, "\")) & "Plugins\V5Plugins\")
        'theControlManager.Load("C:\Users\Andi\Desktop\FahrwegSteuerung\FahrwegSteuerung\bin\Debug")
        Referenz.registerConfigControl(theControlManager.Manager)
        thePluginUpdater.init(theControlManager)

        eb = Referenz.getReferenceToObject("Communication.KernelInterface", Me)
        eb.registriereFuerKontaktEvents(thePluginUpdater)
        eb.registriereFuerLokEvents(thePluginUpdater)
        eb.registriereFuerNothaltEvents(thePluginUpdater)
        eb.registriereFuerWeichenEvents(thePluginUpdater)
    End Sub

    Public Sub pluginStoppen() Implements PluginManagerLibrary.PluginInterface.pluginStoppen

    End Sub
End Class
