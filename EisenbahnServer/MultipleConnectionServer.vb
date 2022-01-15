Imports System.Threading
Imports System.Net.Sockets
Imports System.Net
Imports System.Windows.Forms

Public Class MultipleConnectionServer

#Region "Definitionen"
    Public Event DataReceived(ByVal sender As Client, ByVal data As Object)     'Wird ausgelöst wenn der Sendethreat Daten empfängt
    Public Event ServerStarted()                                                'Wird ausgelöst wenn der Server gestartet ist
    Public Event ConnectionEstablished(ByVal sender As Client)                  'Wird ausgelöst wenn eine Verbindung angenommen wurde
    Public Event ConnectionLost(ByVal sender As Client)                         'Wird ausgelöst wenn die Verbindung verloren wurde
    Public Event ErrorOccured(ByVal meldung As String)                          'Wird ausgelöst wenn ein Fehler aufgetreten ist

    Private Delegate Sub Sub1(ByVal Text As String)
    Private Delegate Sub Sub2(ByVal Sender As Client)
    Private Delegate Sub Sub3(ByVal Sender As Client, ByVal Data As Object)

    Private ConnectionEstablishedDEL As Sub2 = AddressOf RaiseConnectionEstablishedEvent
    Private ErrorOccuredDEL As Sub1 = AddressOf Fehler
    Private DataReceivedDel As Sub3 = AddressOf DataReceivedfromClient
    Private ConnectionLossDEL As Sub2 = AddressOf LostConnection

    Private Const PORTNUMMER = 9042

    Private HostName As String                      'speichert den Name des lokalen Hosts
    Private LocalIP As IPAddress                    'speichert die Lokale Ip-Adresse

    Private clients(0) As Client                    'Speichert alle verbundenen Clients

    Private AcceptThread As Thread
    Private AcceptThreadStart As ThreadStart

    Private myTCPListener As TcpListener

    Private UIThreadObject As Control               'Speichert ein Objekt aus dem Hauptthread, welches dann eine Funktion in einem anderen Thread aufrufen kann.
    'Am besten beim Instanziieren dieser Klasse 'Me' übergeben -> wird hier gespeichert

    Public eb As Communication.KernelInterface
    Public configControl As ServerConfig
#End Region

    Public Sub New(ByRef Reference As Control)
        UIThreadObject = Reference
    End Sub

#Region "Server starten"
    Public Sub ServerStarten()
        'Sub die den Thread startet der eingehende Verbindungen akzeptiert
        AcceptThreadStart = New ThreadStart(AddressOf Accepting)
        AcceptThread = New Thread(AcceptThreadStart)
        AcceptThread.Start()
        RaiseEvent ServerStarted()
    End Sub

    Private Sub Accepting()
        'Thread der eingehende Verbindungen akzeptiert und die Clients speichert
        HostName = Dns.GetHostName
        Dim HostInfo As IPHostEntry = Dns.GetHostEntry(HostName)



        Try
            LocalIP = HostInfo.AddressList(1)

            Dim localEP As New IPEndPoint(LocalIP, PORTNUMMER)

            myTCPListener = New TcpListener(localEP)
            myTCPListener.Start()
        Catch
            UIThreadObject.Invoke(ErrorOccuredDEL, Err.Description)
            AcceptThread.Abort()
        End Try
        While True  'Dauerschleife
            Try
                clients(clients.Length - 1) = New Client(myTCPListener.AcceptTcpClient(), AddressOf ConnectionLoss, AddressOf DataReceivedfromClientSub)
                clients(clients.Length - 1).eb = eb
                clients(clients.Length - 1).configControl = configControl
            Catch
                'UIThreadObject.Invoke(ErrorOccuredDEL, Err.Description)
            End Try

            'UIThreadObject.Invoke(ConnectionEstablishedDEL, clients(clients.Length - 1))

            ReDim Preserve clients(clients.Length)
        End While
    End Sub

    Private Sub RaiseConnectionEstablishedEvent(ByVal sender As Client)
        'Löst den Event aus der neue Verbindung signalisiert
        RaiseEvent ConnectionEstablished(sender)
    End Sub
#End Region

    Public Sub beenden()
        'sollte vor dem Beenden aufgerufen werden
        If Not myTCPListener Is Nothing Then
            myTCPListener.Stop()
            If AcceptThread.IsAlive Then
                AcceptThread.Abort()
            End If
            While AcceptThread.IsAlive
                Application.DoEvents()
            End While
        End If
    End Sub

#Region "Kommunikation mit höherer Klasse"
    Private Sub LostConnection(ByVal sender As Client)
        RaiseEvent ConnectionLost(sender)
    End Sub

    Private Sub ConnectionLoss(ByVal sender As Client)
        UIThreadObject.Invoke(ConnectionLossDEL, sender)
    End Sub

    Private Sub DataReceivedfromClient(ByVal sender As Client, ByVal Data As Object)
        RaiseEvent DataReceived(sender, Data)
    End Sub

    Private Sub DataReceivedfromClientSub(ByVal sender As Client, ByVal Data As Object)
        UIThreadObject.Invoke(DataReceivedDel, sender, Data)
    End Sub

    Public Sub Fehler(ByVal meldung As String)
        RaiseEvent ErrorOccured(meldung)
    End Sub

    Public ReadOnly Property IPAdresse()
        Get
            Return LocalIP
        End Get
    End Property

    Public ReadOnly Property Host()
        Get
            Return HostName
        End Get
    End Property

    Public Sub Send(ByVal ClientNumber As Integer, ByVal Value As Object)
        clients(ClientNumber).send(Value)
    End Sub
#End Region

End Class
