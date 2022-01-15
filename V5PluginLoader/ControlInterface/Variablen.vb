Public Class CLASSVariablen
    Private Vars() As SingleVariable

    Default Public Property Variable(ByVal Variablenname As String) As Object
        Get
            'Die Variable suchen und den gespeicherten Wert zurückgeben
            If Not Vars Is Nothing Then
                For i As Integer = 0 To Vars.Length - 1
                    If Strings.UCase(Vars(i).Name) = Strings.UCase(Variablenname) Then
                        Return Vars(i).Value
                    End If
                Next
            End If
            'Wenn es die Variable nicht gibt, Nichts zurückgeben
            Return Nothing
        End Get
        Set(ByVal Value As Object)
            Dim found As Boolean = False

            'Erst suchen, ob es die Variable schon gibt
            If Not Vars Is Nothing Then
                For i As Integer = 0 To Vars.Length - 1
                    If Strings.UCase(Vars(i).Name) = Strings.UCase(Variablenname) Then
                        'wenn ja, dann der Variable einen Wert zuweisen
                        found = True
                        Vars(i).Value = Value
                    End If
                Next
            End If
            'Wenn die Variable noch nicht gefunden wurde, oder es noch gar keine Variable gibt
            'Dann die Variable neu anlegen und ihr den entsprechenden Wert zuweisen
            If found = False Then
                If Vars Is Nothing Then
                    ReDim Vars(0)
                    Vars(0) = New SingleVariable
                    Vars(0).Name = Variablenname
                    Vars(0).Value = Value
                Else
                    ReDim Preserve Vars(Vars.Length)
                    Vars(Vars.Length - 1) = New SingleVariable
                    Vars(Vars.Length - 1).Name = Variablenname
                    Vars(Vars.Length - 1).Value = Value
                End If
            End If
        End Set
    End Property

    Public Property VariableNr(ByVal Nummer As Integer)
        'Für eine Auflistung der Variablen
        Get
            Return Vars(Nummer).Value
        End Get
        Set(ByVal Value As Object)
            Vars(Nummer).Value = Value
        End Set
    End Property

    Public ReadOnly Property VariableNameNr(ByVal Nummer As Integer)
        'Für eine Auflistung der Variablen
        Get
            Return Vars(Nummer).Name
        End Get

    End Property

    Public ReadOnly Property Count() As Integer
        Get
            If Vars Is Nothing Then
                Return 0
            Else
                Return Vars.Length
            End If
        End Get
    End Property
End Class
