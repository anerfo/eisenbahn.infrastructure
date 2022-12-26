Public Class DMXServerPlugin
    Implements PluginManagerLibrary.PluginInterface

    Private Const _cDMXServerFileName As String = "DMXServer/DMXServer.exe"
    Private Const _cPortNumber As Integer = 12409

    Private _dmxServer As DMXTCPClient = New DMXTCPClient()
    Private _dmxServerProcess As Process = Nothing

    Private Const DisablePlugin As Boolean = True

    Public ReadOnly Property beschreibung As String Implements PluginManagerLibrary.PluginInterface.beschreibung
        Get
            Return "This plugin provides a server to access a DMX-Bus."
        End Get
    End Property

    Public ReadOnly Property name As String Implements PluginManagerLibrary.PluginInterface.name
        Get
            Return "DMX Server Plugin"
        End Get
    End Property

    Public Function pluginInitalisieren() As Object() Implements PluginManagerLibrary.PluginInterface.pluginInitalisieren
        Return {_dmxServer}
    End Function

    Public Sub pluginStarten(Referenz As PluginManagerLibrary.InterfaceFuerPlugins) Implements PluginManagerLibrary.PluginInterface.pluginStarten
        If DisablePlugin = False Then
            Dim serverPath As String = Left(Reflection.Assembly.GetEntryAssembly.Location, InStrRev(System.Reflection.Assembly.GetEntryAssembly.Location, "\")) & _cDMXServerFileName
            If IO.File.Exists(serverPath) Then
                If Not Process.GetProcessesByName("DMXServer.exe").Count > 0 Then
                    _dmxServerProcess = New Process
                    _dmxServerProcess.StartInfo.FileName = serverPath
                    _dmxServerProcess.Start()
                    Threading.Thread.Sleep(500)
                End If
            End If
            _dmxServer.StartCommunication("localhost", _cPortNumber)
        End If
    End Sub

    Public Sub pluginStoppen() Implements PluginManagerLibrary.PluginInterface.pluginStoppen
        _dmxServer.StopCommunication()
        If Not _dmxServerProcess Is Nothing Then
            If Not _dmxServerProcess.HasExited Then
                _dmxServerProcess.CloseMainWindow()
            End If
        End If
    End Sub

    'Protected Overrides Sub Finalize()
    '    _dmxServer.StopCommunication()
    'End Sub
End Class
