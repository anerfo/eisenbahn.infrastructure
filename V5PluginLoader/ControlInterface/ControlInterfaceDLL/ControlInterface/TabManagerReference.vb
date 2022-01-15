Public Class TabManagerReference
    'Von dieser Klasse muss jedes Control eine Instanz beinhalten
    Public MyParent As System.Windows.Forms.Control
    Public Name As String
    Public Child As System.Windows.Forms.Control

    Private Visibility As Boolean = False

    Public Delegate Sub DeletionSub(ByRef Reference As TabManagerReference)
    Public Delete As DeletionSub

    Public Sub New(ByVal MyName As String, ByRef Instanz As System.Windows.Forms.Control)
        Name = MyName
        Child = Instanz
    End Sub

    Public Sub New(ByVal MyName As String, ByRef Instanz As System.Windows.Forms.Control, ByRef EndSub As DeletionSub)
        Name = MyName
        Child = Instanz
        Delete = EndSub
    End Sub

    Public Sub Parent(ByRef ParentControl As System.Windows.Forms.Control)
        MyParent = ParentControl
        Child.Parent = ParentControl
    End Sub
End Class
