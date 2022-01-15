Imports System.Net.Sockets
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class Client

    Public Delegate Sub ConnectionLostDEL(ByVal Sender As Client)  'Delegant mit einem Parameter
    Public Delegate Sub DataReceivedDEL(ByVal Sender As Client, ByVal Obj As Object)  'Delegant mit einem Parameter

    Private ConnectionLostSub As ConnectionLostDEL
    Private DataReceivedSub As DataReceivedDEL

    Private Const Buffersize = 1024

    Private mgID As Guid = Guid.NewGuid

    Private myTCPClient As TcpClient

    Private ReadBuffer(Buffersize) As Byte

    Public eb As Communication.KernelInterface
    Public configControl As ServerConfig

    Public ReadOnly Property ID()
        Get
            Return mgID.ToString
        End Get
    End Property

    Public Sub New(ByVal client As TcpClient, ByVal ConnectionLostCallback As ConnectionLostDEL, ByVal DataReceivedCallback As DataReceivedDEL)
        myTCPClient = client
        ConnectionLostSub = ConnectionLostCallback
        DataReceivedSub = DataReceivedCallback
        myTCPClient.GetStream.BeginRead(ReadBuffer, 0, Buffersize, AddressOf Receive, Nothing)
    End Sub

    Public Sub Receive(ByVal ar As IAsyncResult)
        Try
            Dim intCount As Integer
            Dim bConnected As Boolean = True

            SyncLock myTCPClient.GetStream
                intCount = myTCPClient.GetStream.EndRead(ar)
            End SyncLock

            EvaluateCommand(ReadBuffer)

            If myTCPClient.Client.Poll(0, SelectMode.SelectRead) Then
                Dim buff(1) As Byte
                If myTCPClient.Client.Receive(buff, SocketFlags.Peek) = 0 Then
                    bConnected = False
                End If
            End If

            If bConnected Then
                SyncLock (myTCPClient.GetStream)
                    myTCPClient.GetStream.BeginRead(ReadBuffer, 0, Buffersize, AddressOf Receive, Nothing)
                End SyncLock
            End If
        Catch e As Exception
            ConnectionLostSub(Me)
        End Try
    End Sub

    Private received(65536) As Byte
    Private byteCount As Integer = 0

    Public Sub send(ByVal bytes() As Byte)
        SyncLock myTCPClient.GetStream
            Dim w As New IO.StreamWriter(myTCPClient.GetStream)
            w.Write(bytes)
            w.Flush()
        End SyncLock
    End Sub

    Public Sub send(ByVal Value As Object)
        Dim ms As New MemoryStream
        Dim bf As New BinaryFormatter

        bf.Serialize(ms, Value)

        send(ms.ToArray)
    End Sub

    Private Sub EvaluateCommand(ByVal data() As Byte)
        If data(0) >= 0 And data(0) <= 14 Then
            eb.lokSteuern(data(1), Klassen.LokEigenschaften.Geschwindigkeit, data(0))
        End If
        configControl.AddCommandToList(data(1).ToString + ": " + data(0).ToString())
    End Sub

End Class
