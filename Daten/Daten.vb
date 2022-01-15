Imports System.IO   'Um die Dateien ansprechen zu können
Imports System.Windows.Forms

Module Daten
    Public Table As New DataGridView
    Dim trennzeichen As String = Microsoft.VisualBasic.vbTab    'Das Trennzeichen mit welchem die Elemente in der Datei getrennt sind

    Public Function write_to_table(ByVal Spalte As Integer, ByVal Zeile As Integer, ByVal wert As String) As Integer
        'Schreibt einen Wert in das angegebene Feld, außerdem legt es das Feld an wenn es nicht existiert
        Dim i As Integer
        While Table.Columns.Count <= Spalte
            Table.Columns.Add(Table.Columns.Count, Table.Columns.Count)
        End While
        While Table.Rows.Count <= Zeile
            Table.Rows.Add()
            'Weil die Zeilen am Anfang eingefügt werden!!!
            For i = 0 To Table.Columns.Count - 1
                Table(i, Table.Rows.Count - 2).Value = Table(i, Table.Rows.Count - 1).Value
                Table(i, Table.Rows.Count - 1).Value = ""
            Next
        End While

        Table(Spalte, Zeile).Value = wert
        Return 1
    End Function

    Public Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As String) As Integer
        Dim i, q As Integer
        Dim found As Boolean

        found = False
        If wert = "LOK" Then
            q = 0
        End If
        For q = 0 To Table.Columns.Count - 1
            If Strings.UCase(Table.Columns(q).Name) = Strings.UCase(Spalte) Then
                found = True
                Exit For
            End If
        Next

        If found = False Then
            Table.Columns.Add(Spalte, Spalte)
        End If
        While Table.Rows.Count <= Zeile
            Table.Rows.Add()
            'Weil die Zeilen am Anfang eingefügt werden!!!
            For i = 0 To Table.Columns.Count - 1
                Table(i, Table.Rows.Count - 2).Value = Table(i, Table.Rows.Count - 1).Value
                Table(i, Table.Rows.Count - 1).Value = ""
            Next
        End While

        Table(q, Zeile).Value = wert
        Return 1
    End Function

    Public Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As Integer) As Integer
        Return write_to_table(Spalte, Zeile, wert.ToString)
    End Function

    Public Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As Boolean) As Integer
        Return write_to_table(Spalte, Zeile, wert.ToString)
    End Function

    Public Function read_from_table(ByVal Spalte As Integer, ByVal Zeile As Integer) As String
        'Gibt das angegebene Feld zurück Spalte ist der name der gesuchten spalte
        Return Table(Spalte, Zeile).Value
    End Function

    Public Function read_from_table(ByVal Spalte As String, ByVal Zeile As Integer) As String
        'Gibt das angegebene Feld zurück
        Dim i As Integer
        Dim found As Boolean

        found = False
        For i = 0 To Table.Columns.Count - 1
            If Strings.UCase(Table.Columns(i).Name) = Strings.UCase(Spalte) Then
                found = True
                Exit For
            End If
        Next
        If found = False Then
            Return Nothing
        Else
            If Zeile < Table.RowCount Then
                If Table(i, Zeile).Value = "" Then
                    Return Nothing
                Else
                    Return Table(i, Zeile).Value
                End If
            Else
                Return Nothing
            End If
        End If
    End Function


    Public Function Save_table(ByVal Datei As String) As Integer
        'speichert das Feld in der angegebenen Datei als CSV-Datei mit 'Trennzeichen' als Trennzeichen
        Dim objStreamWriter As StreamWriter
        Dim line As String
        Dim i, q As Integer

        objStreamWriter = New StreamWriter(Datei)

        For i = 0 To Table.Columns.Count - 1
            line = Table.Columns(i).Name
            For q = 0 To Table.Rows.Count - 1
                line &= trennzeichen & Table(Table.Columns(i).Name, q).Value
            Next
            objStreamWriter.WriteLine(line)
        Next
        objStreamWriter.Close()
        Return 1
    End Function

    Public Function Load_table(ByVal Datei As String) As Integer
        'Lädt das Feld aus der angegebenen Datei
        Dim objStreamReader As StreamReader
        Dim strLine, rest As String
        Dim i, q, w As Integer

        If System.IO.File.Exists(Datei) Then
            'table löschen
            Table.Columns.Clear()
            Table.Rows.Clear()

            objStreamReader = New StreamReader(Datei)
            strLine = objStreamReader.ReadLine

            i = 0
            Do While Not strLine Is Nothing
                w = Strings.InStr(strLine, trennzeichen)
                rest = Strings.Left(strLine, w - 1)
                strLine = Strings.Right(strLine, Strings.Len(strLine) - w)
                Table.Columns.Add(rest, rest)

                q = 0
                Do While Strings.InStr(strLine, trennzeichen)
                    w = Strings.InStr(strLine, trennzeichen)
                    rest = Strings.Left(strLine, w - 1)
                    strLine = Strings.Right(strLine, Strings.Len(strLine) - w)
                    write_to_table(i, q, rest)
                    q += 1
                Loop
                write_to_table(i, q, strLine)
                i += 1
                strLine = objStreamReader.ReadLine
            Loop
            objStreamReader.Close()
            Return 1
        Else
            Return 0
        End If
    End Function

    Public Sub show_Table(ByVal parent As Object, Optional ByVal show As Integer = -1)
        If Table.Created = False Or Table.Parent Is Nothing Then
            Table.Width = DirectCast(parent, TabPage).Width
            Table.Height = DirectCast(parent, TabPage).Height
            Table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            Table.AutoResizeColumns()
            Table.Dock = DockStyle.Fill
            Table.Visible = False
            DirectCast(parent, TabPage).Controls.Add(Table)
            AddHandler Table.Click, AddressOf table_click
        End If
        If Table.Visible = True Or show = 0 Then
            Table.Visible = False
        Else
            If Table.Visible = False Or show > 0 Then
                Table.Visible = True
                Table.BringToFront()
            End If
        End If
    End Sub

    Private Sub table_click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Public Sub erase_table()
        'Dim i, q As Integer
        'For i = 0 To Table.Columns.Count - 1
        '    For q = 0 To Table.Rows.Count - 1
        '        Table(i, q).Value = ""
        '    Next
        'Next
        Table.Rows.Clear()
        Table.Columns.Clear()
    End Sub

    Public Sub erase_Column(ByVal ColumnNumber As Integer, Optional ByVal FromLine As Integer = 0)
        For i As Integer = FromLine To Table.ColumnCount - 1
            Table(ColumnNumber, i).Value = ""
        Next
    End Sub

    Public Sub erase_Column(ByVal Spalte As String, Optional ByVal FromLine As Integer = 0)
        Dim q As Integer
        Dim found As Boolean

        found = False
        For q = 0 To Table.Columns.Count - 1
            If Strings.UCase(Table.Columns(q).Name) = Strings.UCase(Spalte) Then
                found = True
                Exit For
            End If
        Next

        erase_Column(q, FromLine)
    End Sub


End Module
