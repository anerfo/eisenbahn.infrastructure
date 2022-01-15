Imports System.Reflection

Public Class DatenPlugin
    Implements PluginManagerLibrary.PluginInterface

    Private myDaten As New DatenKonverter

    Public ReadOnly Property beschreibung As String Implements PluginManagerLibrary.PluginInterface.beschreibung
        Get
            Return "Speichert und lädt Daten in/von einer Datei"
        End Get
    End Property

    Public ReadOnly Property name As String Implements PluginManagerLibrary.PluginInterface.name
        Get
            Return "Datenspeicher"
        End Get
    End Property

    Public Function pluginInitalisieren() As Object() Implements PluginManagerLibrary.PluginInterface.pluginInitalisieren
        Dim obj() As Object = {myDaten}
        Return obj
    End Function

    Public Sub pluginStarten(ByVal Referenz As PluginManagerLibrary.InterfaceFuerPlugins) Implements PluginManagerLibrary.PluginInterface.pluginStarten
        myDaten.Laden()
    End Sub

    Public Sub pluginStoppen() Implements PluginManagerLibrary.PluginInterface.pluginStoppen
        myDaten.Speichern()
    End Sub
End Class
