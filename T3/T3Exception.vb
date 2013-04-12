Public Class T3Exception
    Inherits Exception

    Private Shared __messageLookup As New Dictionary(Of Integer, String)()

    Shared Sub New()
        __messageLookup(T3Library.NTBASEERR) = "Base error"
        __messageLookup(T3Library.NTEBADF) = "Bad file (NTHD) number"
        __messageLookup(T3Library.NTENOMEM) = "Not enough memory"
        __messageLookup(T3Library.NTEINVAL) = "Invalid argument"
        __messageLookup(T3Library.NTEMFILE) = "Too many open files"
        __messageLookup(T3Library.NTEALREADY) = "Operation already in progress"
        __messageLookup(T3Library.NTEWOULDBLOCK) = "Operation would block"
        __messageLookup(T3Library.NTENOTSUPPORTED) = "Cannot support this function"
        __messageLookup(T3Library.NTECONNRESET) = "Connection reset by peer"
        __messageLookup(T3Library.NTETIMEDOUT) = "Operation timed out"
        __messageLookup(T3Library.NTERESINVAL) = "Invalid response"
        __messageLookup(T3Library.NTEMNPRECVED) = """M NP"" received"
        __messageLookup(T3Library.NTEMERRECVED) = """M ER"" received"
        __messageLookup(T3Library.NTETBDRECVED) = """T BD"" received"
    End Sub

    Private Shared Function Lookup(ByVal code As Integer) As String
        Dim ret As String = Nothing

        If Not __messageLookup.TryGetValue(code, ret) Then ret = code.ToString()

        Return ret
    End Function

    Friend Sub New(ByVal errorCode As Integer)
        MyBase.New(Lookup(errorCode))
    End Sub
End Class
