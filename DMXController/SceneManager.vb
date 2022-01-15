Public Class SceneManager

    Private _scenes As List(Of Scene)
    Private _channels As Integer
    Private _dmxServer As DMXServer.IDMXServer
    Private _dataStorage As Daten.DatenInterface
    Private _scenesEdited As Boolean

    Private Class Scene
        Public _name As String
        Public _values As List(Of Integer)

        Public Sub New()
            _values = New List(Of Integer)
        End Sub
    End Class

    Public Sub New(ByVal channels As Integer, ByVal dmxServer As DMXServer.IDMXServer, ByVal dataStorage As Daten.DatenInterface)
        InitializeComponent()

        Me.Anchor = Windows.Forms.AnchorStyles.Top And Windows.Forms.AnchorStyles.Left And Windows.Forms.AnchorStyles.Bottom
        _channels = channels
        _dmxServer = dmxServer
        _scenes = New List(Of Scene)
        _dataStorage = dataStorage
        _scenesEdited = False

        LoadScenes()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim scene As New Scene
        scene._name = TextBox1.Text
        For i As Integer = 1 To _channels
            scene._values.Add(_dmxServer.GetData(i))
        Next i
        _scenes.Add(scene)
        _scenesEdited = True

        FillListOfScenes()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim targetScene As Scene = _scenes(ListBox1.SelectedIndex)
        Dim currentScene As Scene = New Scene()
        For i As Integer = 1 To _channels
            currentScene._values.Add(_dmxServer.GetData(i))
        Next

        PerformFades(currentScene, targetScene)
    End Sub

    Private Sub PerformFades(ByVal currentScene As Scene, ByVal targetScene As Scene)
        For i As Integer = 0 To _channels - 1
            _dmxServer.SetData(i + 1, targetScene._values(i))
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        _scenes.RemoveAt(ListBox1.SelectedIndex)
        FillListOfScenes()
        _scenesEdited = True
    End Sub

    Private Sub FillListOfScenes()
        ListBox1.Items.Clear()
        For i As Integer = 0 To _scenes.Count - 1
            ListBox1.Items.Add(_scenes(i)._name)
        Next
    End Sub

    Private Sub LoadScenes()
        Dim value As String
        value = _dataStorage.read_from_table("SceneManager", 0)
        If (value <> String.Empty) Then
            Dim sceneCount As Integer = Integer.Parse(value)
            For i As Integer = 0 To sceneCount - 1
                Dim scene As Scene = New Scene()
                scene._name = _dataStorage.read_from_table("Scene" + i.ToString(), 0)
                For q As Integer = 1 To _channels
                    scene._values.Add(Integer.Parse(_dataStorage.read_from_table("Scene" + i.ToString(), q)))
                Next
                _scenes.Add(scene)
            Next
        End If
        FillListOfScenes()
    End Sub

    Private Sub StoreScenes()
        _dataStorage.write_to_table("SceneManager", 0, _scenes.Count)
        For i As Integer = 0 To _scenes.Count - 1
            _dataStorage.write_to_table("Scene" + i.ToString(), 0, _scenes(i)._name)
            For q As Integer = 1 To _channels
                _dataStorage.write_to_table("Scene" + i.ToString(), q, _scenes(i)._values(q - 1))
            Next
        Next
    End Sub

    Public Sub StopSceneManager()
        If _scenesEdited Then
            StoreScenes()
        End If
    End Sub
End Class
