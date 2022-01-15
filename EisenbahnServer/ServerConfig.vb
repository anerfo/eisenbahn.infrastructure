Public Class ServerConfig

    Private Delegate Sub AddCommandToListDel(ByVal value As String)

    Private AddCommandToListCaller As AddCommandToListDel = AddressOf SyncCall

    Public Sub AddCommandToList(ByVal value As String)
        Me.Invoke(AddCommandToListCaller, value)
    End Sub

    Private Sub SyncCall(ByVal value As String)
        ListBox1.Items.Add(value)
    End Sub
End Class
