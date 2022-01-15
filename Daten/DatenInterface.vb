Public Interface DatenInterface
    Function write_to_table(ByVal Spalte As Integer, ByVal Zeile As Integer, ByVal wert As String) As Integer
    Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As String) As Integer
    Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As Integer) As Integer
    Function write_to_table(ByVal Spalte As String, ByVal Zeile As Integer, ByVal wert As Boolean) As Integer

    Function read_from_table(ByVal Spalte As Integer, ByVal Zeile As Integer) As String
    Function read_from_table(ByVal Spalte As String, ByVal Zeile As Integer) As String

    'Function Save_table(ByVal Datei As String) As Integer

    'Function Load_table(ByVal Datei As String) As Integer

    'Sub show_Table(ByVal parent As Object, Optional ByVal show As Integer = -1)

    'Sub table_click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    'Sub erase_table()

    'Sub erase_Column(ByVal ColumnNumber As Integer, Optional ByVal FromLine As Integer = 0)

    'Sub erase_Column(ByVal Spalte As String, Optional ByVal FromLine As Integer = 0)


End Interface
