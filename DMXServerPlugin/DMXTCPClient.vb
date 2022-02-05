Imports System.Net.Sockets

''' <summary>
''' This class implements the DMX Server interface and communicates with
''' a DMX Server via TCP
''' </summary>
''' <remarks></remarks>
Public Class DMXTCPClient
    Implements IDMXServer

    Private tcpClient As TcpClient

    Private Const _cStartByte As Byte = 255
    Private Const _cEndByte As Byte = 0
    Private Const _cNumberOfChannels = 12 + 1 'Because 0 does not exist.

    Private _observers As List(Of List(Of IDMXServer.ValueChangedCallback))
    Private _values As List(Of Integer)

#Region "Public Methods"
    Public Sub New()
        tcpClient = New TcpClient()
        _observers = New List(Of List(Of IDMXServer.ValueChangedCallback))
        _values = New List(Of Integer)
        For i As Integer = 0 To _cNumberOfChannels
            _observers.Add(New List(Of IDMXServer.ValueChangedCallback))
            _values.Add(0)
        Next
    End Sub

    ''' <summary>
    ''' Initializes the communication with the DMX-server.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StartCommunication(ByVal host As String, ByVal port As Integer)
        Try
            tcpClient.Connect(host, port)
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Stops the communication with the DMX-bus
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub StopCommunication()
        tcpClient.Close()
    End Sub

    ''' <summary>
    ''' Sets the number of used channels. This may improve the performance of the bus
    ''' </summary>
    ''' <param name="count">The number of the channels used</param>
    ''' <remarks>Should only be set once at startup.</remarks>
    Public Sub SetChannelCount1(ByVal count As Integer) Implements IDMXServer.SetChannelCount
        Throw New NotImplementedException("This is not supported by the Server")
    End Sub

    ''' <summary>
    ''' Sets the value of a dmx channel.
    ''' </summary>
    ''' <param name="channel">The channel to set</param>
    ''' <param name="value">The value to write</param>
    ''' <remarks></remarks>
    Public Sub SetData(ByVal channel As Integer, ByVal value As Integer) Implements IDMXServer.SetData
        If (channel > 255) Then
            channel = 255
        End If
        If (value > 255) Then
            value = 255
        End If
        If (value < 0) Then
            value = 0
        End If
        If (Not tcpClient.Client Is Nothing) Then
            If (tcpClient.Client.Connected) Then
                Dim buffer() As Byte = {_cStartByte, channel, value, _cEndByte}
                tcpClient.Client.Send(buffer)
            End If
        End If
        For Each observer As IDMXServer.ValueChangedCallback In _observers(channel)
            If (Not observer Is Nothing) Then
                observer.Invoke(channel, value)
            End If
        Next
        _values(channel) = value
    End Sub

    ''' <summary>
    ''' Returns the current value of the channel or -1 if the request was not valid
    ''' </summary>
    ''' <param name="channel">The channel to get the value for</param>
    ''' <returns>The value of the requested channel or -1 if the channel does not exist.</returns>
    ''' <remarks></remarks>
    Public Function GetData(ByVal channel As Integer) As Integer Implements IDMXServer.GetData
        Dim result As Integer = -1
        If channel > 0 And channel < _cNumberOfChannels Then
            result = _values(channel)
        End If
        Return result
    End Function

    Public Sub AddObserver(ByVal channel As Integer, ByVal callback As IDMXServer.ValueChangedCallback) Implements IDMXServer.AddObserver
        _observers(channel).Add(callback)
        callback.Invoke(channel, _values(channel))
    End Sub

    Public Sub RemoveObserver(ByVal channel As Integer, ByVal callback As IDMXServer.ValueChangedCallback) Implements IDMXServer.RemoveObserver
        If (Not _observers(channel) Is Nothing) Then
            _observers(channel).Remove(callback)
        End If
    End Sub
#End Region
End Class
