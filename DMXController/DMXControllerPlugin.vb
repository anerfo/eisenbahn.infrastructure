Public Class DMXControllerPlugin
    Implements PluginManagerLibrary.PluginInterface

    Private _dmxController As DMXControllerBoard

    Public ReadOnly Property beschreibung As String Implements PluginManagerLibrary.PluginInterface.beschreibung
        Get
            Return "This plugin provides a DMX controller"
        End Get
    End Property

    Public ReadOnly Property name As String Implements PluginManagerLibrary.PluginInterface.name
        Get
            Return "DMX Controller"
        End Get
    End Property

    Public Function pluginInitalisieren() As Object() Implements PluginManagerLibrary.PluginInterface.pluginInitalisieren
        Return Nothing
    End Function

    Public Sub pluginStarten(ByVal Referenz As PluginManagerLibrary.InterfaceFuerPlugins) Implements PluginManagerLibrary.PluginInterface.pluginStarten
        Dim dmxServer As DMXServer.IDMXServer = Referenz.getReferenceToObject("DMXServer.IDMXServer", Me)
        Dim dataStorage As Daten.DatenInterface = Referenz.getReferenceToObject("Daten.DatenInterface", Me)

        _dmxController = New DMXControllerBoard(dmxServer, dataStorage)

        Referenz.registerGUIControl(_dmxController)
    End Sub

    Public Sub pluginStoppen() Implements PluginManagerLibrary.PluginInterface.pluginStoppen
        _dmxController.StopController()
    End Sub
End Class
