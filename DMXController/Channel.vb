Public Class Channel
    Private _receivingUpdate As Boolean
    Private _dmxServer As DMXServer.IDMXServer
    Private _channel As Integer
    Private Delegate Sub InvokeUpdateDelegate(ByVal value As Integer)

    Private _invokeUpdate As InvokeUpdateDelegate

    Public Sub New(ByVal channel As Integer, ByVal dmxServer As DMXServer.IDMXServer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ChannelLabel.Text = channel

        Dim callback As DMXServer.IDMXServer.ValueChangedCallback = New DMXServer.IDMXServer.ValueChangedCallback(AddressOf ChannelUpdate)
        _receivingUpdate = False
        _dmxServer = dmxServer
        _channel = channel

        _invokeUpdate = New InvokeUpdateDelegate(AddressOf SetDisplayValues)

        dmxServer.AddObserver(channel, callback)
    End Sub

    Private Sub ChannelUpdate(ByVal channel As Integer, ByVal value As Integer)
        _receivingUpdate = True
        SetDisplayValues(value)
        _receivingUpdate = False
    End Sub

    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        If (Not _receivingUpdate) Then
            _dmxServer.SetData(_channel, TrackBar1.Value)
        End If
    End Sub

    Private Sub SetDisplayValues(ByVal value As Integer)
        If (TrackBar1.InvokeRequired) Then
            TrackBar1.Invoke(_invokeUpdate, {value})
        Else
            If (value >= 0 And value <= TrackBar1.Maximum) Then
                TrackBar1.Value = value
            End If
            ValueLabel.Text = value
            TrackBar1.Update()
        End If
    End Sub
End Class
