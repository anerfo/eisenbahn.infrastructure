Public Class DatenKonverter
    Implements DatenInterface

    Private savePath As String = Klassen.Konstanten.Speicherpfad & "\save.txt"

    Public Function read_from_table(ByVal Spalte As Integer, ByVal Zeile As Integer) As String Implements DatenInterface.read_from_table
        Return Daten.read_from_table(Spalte, Zeile)
    End Function

    Public Function read_from_table(ByVal Spalte As String, ByVal Zeile As Integer) As String Implements DatenInterface.read_from_table
        Return Daten.read_from_table(Spalte, Zeile)
    End Function

    Public Function write_to_table(ByVal Spalte As Integer, ByVal Zeile As Integer, ByVal wert As String) As Integer Implements DatenInterface.write_to_table
        Return Daten.write_to_table(Spalte, Zeile, wert)
    End Function

    Public Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As Boolean) As Integer Implements DatenInterface.write_to_table
        Return Daten.write_to_table(Spalte, Zeile, wert)
    End Function

    Public Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As Integer) As Integer Implements DatenInterface.write_to_table
        Return Daten.write_to_table(Spalte, Zeile, wert)
    End Function

    Public Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As String) As Integer Implements DatenInterface.write_to_table
        Return Daten.write_to_table(Spalte, Zeile, wert)
    End Function

    Public Sub Speichern()
        Daten.Save_table(savePath)
    End Sub

    Public Sub Laden()
        Daten.Load_table(savePath)
    End Sub
End Class
