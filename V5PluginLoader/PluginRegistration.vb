Imports System.Windows.Forms

Public Class PluginRegistration
    Private mainProg As PluginManagerLibrary.InterfaceFuerPlugins

    Public Sub init(ByVal mainProgrammReference As PluginManagerLibrary.InterfaceFuerPlugins)
        mainProg = mainProgrammReference
    End Sub

    Public Sub ControlAnzeigen(ByRef GUIControl As control)
        mainProg.registerGUIControl(GUIControl)
    End Sub
End Class
