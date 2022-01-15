Public Class EisenbahnServerPlugin
    Implements PluginManagerLibrary.PluginInterface

    Private _server As MultipleConnectionServer
    Private _serverConfig As ServerConfig
    Private WebSocketServer As TypeDef.WebSocket.Server

    Public ReadOnly Property beschreibung As String Implements PluginManagerLibrary.PluginInterface.beschreibung
        Get
            Return "Stellt einen Server für die Steuerung der Eisenbahn bereit"
        End Get
    End Property

    Public ReadOnly Property name As String Implements PluginManagerLibrary.PluginInterface.name
        Get
            Return "Eisenbahn Server"
        End Get
    End Property

    Public Function pluginInitalisieren() As Object() Implements PluginManagerLibrary.PluginInterface.pluginInitalisieren
        Return Nothing
    End Function

    Public Sub pluginStarten(ByVal Referenz As PluginManagerLibrary.InterfaceFuerPlugins) Implements PluginManagerLibrary.PluginInterface.pluginStarten
        Dim eb As Communication.KernelInterface

        eb = Referenz.getReferenceToObject("Communication.KernelInterface", Me)

        _serverConfig = New ServerConfig()

        Referenz.registerConfigControl(_serverConfig)

        _server = New MultipleConnectionServer(_serverConfig)

        _server.eb = eb
        _server.configControl = _serverConfig

        _server.ServerStarten()

        WebSocketServer = New TypeDef.WebSocket.Server("0.0.0.0", 8000, eb)
        WebSocketServer.startServer()
    End Sub

    Public Sub pluginStoppen() Implements PluginManagerLibrary.PluginInterface.pluginStoppen
        _server.beenden()
    End Sub

End Class
