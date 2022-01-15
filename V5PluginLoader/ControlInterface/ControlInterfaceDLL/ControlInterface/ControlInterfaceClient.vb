Imports V5PluginKompaitibilitaet

Public Interface ControlInterfaceClient
    Function Init(ByRef PluginHost As ControlInterfaceHost) As TabManagerReference
    Function GetControl() As System.Windows.Forms.Control
    Sub Update(ByVal Typ As Definitionen.Typen)
End Interface
