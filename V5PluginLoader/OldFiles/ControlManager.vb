Imports System.Windows.Forms

Public Class ControlManager
    Private Variablen As CLASSVariablen
    Private Hosts() As ControlHost

    Public Sub New(ByRef Variables As CLASSVariablen, ByRef ControlHosts() As ControlHost)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Variablen = Variables
        Hosts = ControlHosts
    End Sub

    Private Sub Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Aktualisieren.Click
        VarViewUpdate()
    End Sub

    Private Sub VarViewUpdate()
        Varview.Rows.Clear()
        For i As Integer = 0 To Variablen.Count - 1
            Varview.Rows.Add()
            Varview.Item(0, i).Value = Variablen.VariableNameNr(i).ToString
            Varview.Item(1, i).Value = Variablen.VariableNr(i).ToString
        Next
    End Sub

    Private Sub setzten_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles setzten.Click
        For i As Integer = 0 To Variablen.Count - 1
            Variablen.VariableNr(i) = Varview.Item(1, i).Value
        Next
    End Sub

    Public Sub ControlManagerUpdaten()
        Me.Dock = DockStyle.Fill
        ListBox1.Items.Clear()
        If Not Hosts Is Nothing Then
            For i As Integer = 0 To Hosts.Length - 1
                ListBox1.Items.Add(Hosts(i).TabReference.Name)
            Next
        Else
            ListBox1.Items.Add("Es laufen keine Plugins")
        End If
        VarViewUpdate()
    End Sub

    Public Sub AktionHinzu(ByVal Name As String, ByVal Eintrag As String)
        ListBox2.Items.Insert(0, Name & ": " & Eintrag)
        While ListBox2.Items.Count > ListBox2.Height / ListBox2.ItemHeight - 1
            If ListBox2.Items.Count > 0 Then
                ListBox2.Items.RemoveAt(ListBox2.Items.Count - 1)
            End If
        End While
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        If DirectCast(sender, ListBox).SelectedIndex <> -1 Then
            Label2.Text = DirectCast(sender, ListBox).Items.Item(DirectCast(sender, ListBox).SelectedIndex)
        End If
    End Sub
End Class
