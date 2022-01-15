Public Class DMXControllerBoard
    Private _dmxServer As DMXServer.IDMXServer
    Private _dataStorage As Daten.DatenInterface
    Private _sceneManager As SceneManager

    Private Const _cNumberOfChannels As Integer = 12

    Public Sub New(ByVal dmxServer As DMXServer.IDMXServer, ByVal dataStorage As Daten.DatenInterface)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _dmxServer = dmxServer
        _dataStorage = dataStorage
        _sceneManager = Nothing
        Me.Dock = Windows.Forms.DockStyle.Fill
    End Sub

    Private Sub DMXControllerBoard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _sceneManager = New SceneManager(_cNumberOfChannels, _dmxServer, _dataStorage)
        FlowLayoutPanel1.Controls.Add(_sceneManager)
        For i As Integer = 1 To _cNumberOfChannels
            FlowLayoutPanel1.Controls.Add(New Channel(i, _dmxServer))
        Next
    End Sub

    Public Sub StopController()
        If (Not _sceneManager Is Nothing) Then
            _sceneManager.StopSceneManager()
        End If
    End Sub
End Class
