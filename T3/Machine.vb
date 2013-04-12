Option Strict On

''' <summary>
''' Machine represents an Okuma CNC accessible via T3.
''' </summary>
''' <remarks>
''' Usage: instantiate a Machine object, then call <see cref="Machine.Connect">Connect</see>. 
''' Call <see cref="Machine.GetStatus">GetStatus</see> to retrieve a <see cref="Status">Status</see> structure.
''' When finished, call <see cref="Machine.Disconnect">Disconnect</see>.
''' </remarks>
Public Class Machine
    Implements IDisposable

    Private _host As String
    Private _port As Integer
    Private _hd As Integer = -1

    Public Sub New(ByVal host As String, Optional ByVal port As Integer = 6003)
        _host = host
        _port = port
    End Sub

    Public Sub Connect()
        If Not _hd = -1 Then Disconnect()
        _hd = T3Library.NTCreateSocket()
        If T3Library.NTConnectStatus(_hd, _host, _port.ToString()) = T3Library.NT_ERROR Then Throw New T3Exception(T3Library.NTGetLastError())
    End Sub

    Public Sub Disconnect()
        If T3Library.NTCloseSocket(_hd) = T3Library.NT_ERROR Then Throw New T3Exception(T3Library.NTGetLastError())
        _hd = -1
    End Sub

    Function GetStatus() As Status
        'Throw New NotImplementedException()
        Dim buffer As New String(" "c, 2048)
        If T3Library.NTGetStatus(_hd, "000000FF", buffer) = T3Library.NT_ERROR Then Throw New T3Exception(T3Library.NTGetLastError())
        Return New Status(buffer)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            'Free unmanaged state objects
            If _hd <> -1 Then Disconnect()

            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class