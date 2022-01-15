Public Class V5PluginKompatibilitaet
    Implements PluginManagerLibrary.PluginInterface

    Public Shared dataStorage As Daten.DatenInterface
    Public Shared eb As Communication.KernelInterface
    Public Shared mediaProvider As MediaProvider.IMediaProvider
    Private dummyStartIF As New DummyStartClass

    Public ReadOnly Property beschreibung() As String Implements PluginManagerLibrary.PluginInterface.beschreibung
        Get
            Return "Vereinfacht die Nutzung des Quellcodes von Plugins, welche für Version 5 geschrieben wurden"
        End Get
    End Property

    Public ReadOnly Property name() As String Implements PluginManagerLibrary.PluginInterface.name
        Get
            Return "V5 Plugin Kompatibilität"
        End Get
    End Property

    Public Function pluginInitalisieren() As Object() Implements PluginManagerLibrary.PluginInterface.pluginInitalisieren
        Dim obj() As Object = {dummyStartIF}
        Return obj
    End Function

    Public Sub pluginStarten(ByVal Referenz As PluginManagerLibrary.InterfaceFuerPlugins) Implements PluginManagerLibrary.PluginInterface.pluginStarten
        dataStorage = Referenz.getReferenceToObject("Daten.DatenInterface", Me)
        eb = Referenz.getReferenceToObject("Communication.KernelInterface", Me)
        mediaProvider = Referenz.getReferenceToObject("MediaProvider.IMediaProvider", Me)
    End Sub

    Public Sub pluginStoppen() Implements PluginManagerLibrary.PluginInterface.pluginStoppen

    End Sub
End Class
