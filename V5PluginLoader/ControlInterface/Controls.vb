Public Class Controls

    Public ControlHosts() As ControlHost       'in den Hosts laufen die Instanzen der Controls
    Public Variablen As CLASSVariablen = New CLASSVariablen    'Hier werden die Variablen der Controls gespeichert -> Sie sind auch Controlübergreifend verfügbar

    Public Manager As ControlManager

    Public Sub Load()       'Laden der Controls
        Dim plugins() As AvailablePlugin
        plugins = PluginServices.FindPlugins(Application.StartupPath & "\Plugins\Controls", "ControlInterface.ControlInterfaceClient")
        If Not plugins Is Nothing Then
            ReDim ControlHosts(plugins.Length - 1)
            Dim i As Integer = 0
            While i < ControlHosts.Length
                '                For i As Integer = 0 To plugins.Length - 1
                Try
                    ControlHosts(i) = New ControlHost
                    ControlHosts(i).Load(PluginServices.CreateInstance(plugins(i)), Variablen, AddressOf AktionLoggen)
                    i += 1
                Catch ex As Exception
                    Eisenbahn.Meldung(Me, Definitionen.MeldungTyp.Fehler, "In einem extern Control ist ein Fehler aufgetreten. Es konnte nicht geladen werden. Wenn sie beim Programmieren das Hilfsmodul verwenden, schauen sie nach, ob darin das richtige Control instanziiert wird.", True)
                    Eisenbahn.Meldung(Me, Definitionen.MeldungTyp.Fehler, "Fehlerhafte Plugindatei: " & plugins(i).AssemblyPath, True)
                    If ControlHosts.Length = 1 Then
                        ControlHosts = Nothing
                        Exit While
                    Else
                        ReDim Preserve ControlHosts(ControlHosts.Length - 1)
                    End If
                End Try
                'Next
            End While
        End If
        Manager = New ControlManager(Variablen, ControlHosts)
    End Sub

    Public Sub Updaten(ByVal Typ As Definitionen.Typen) 'Die Controls updaten
        If Not ControlHosts Is Nothing Then
            For i As Integer = 0 To ControlHosts.Length - 1
                Try
                    ControlHosts(i).Instanz.Update(Typ)
                Catch ex As Exception
                    Eisenbahn.Meldung(Me, Definitionen.MeldungTyp.Warnung, "Fehler in der UpdateRoutine von " & ControlHosts(i).TabReference.Name & "! Fehlerbeschreibung: " & ex.Message, True)
                End Try
            Next
        End If
    End Sub

    Public Sub AktionLoggen(ByVal Name As String, ByVal Eintrag As String)
        Manager.AktionHinzu(Name, Eintrag)
    End Sub
End Class
