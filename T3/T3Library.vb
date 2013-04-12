Friend Module T3Library

    '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    ' DNC-T3 (mmsnt-t3.dll) 
    '_______________________________________________________________________________

    Public Declare Function NTCreateSocket Lib "mmsnt-t3.dll" () As Integer
    Public Declare Function NTCloseSocket Lib "mmsnt-t3.dll" (ByVal hd As Integer) As Integer
    Public Declare Function NTConnectMacman Lib "mmsnt-t3.dll " (ByVal hd As Integer, ByVal RemoteHost As String, ByVal Port As String) As Integer
    Public Declare Function NTGetMacman Lib "mmsnt-t3.dll" (ByVal hd As Integer, ByVal MacmanCommand As String, ByVal MacmanData As String) As Integer
    Public Declare Function NTConnectStatus Lib "mmsnt-t3.dll" (ByVal hd As Integer, ByVal RemoteHost As String, ByVal Port As String) As Integer
    Public Declare Function NTGetStatus Lib "mmsnt-t3.dll" (ByVal hd As Integer, ByVal StatusCommand As String, ByVal StatusData As String) As Integer
    Public Declare Function NTSetNonBlock Lib "mmsnt-t3.dll" (ByVal hd As Integer, ByVal hwnd As Integer, ByVal wMsg As Integer) As Integer
    Public Declare Function NTSetBlock Lib "mmsnt-t3.dll" (ByVal hd As Integer) As Integer
    Public Declare Function NTSetTimeout Lib "mmsnt-t3.dll" (ByVal hd As Integer, ByVal TimeOut As Integer) As Integer
    Public Declare Function NTGetLastError Lib "mmsnt-t3.dll" () As Integer
    Public Declare Sub NTSetLastError Lib "mmsnt-t3.dll" (ByVal nError As Integer)
    'Public Declare Function NTGetErrorMessage Lib "mmsnt-t3.dll" (ByVal nError As Long) As String
    Public Declare Sub NTGetErrorMessagetoBuffer Lib "mmsnt-t3.dll" (ByVal nError As Short, ByRef strMessage As String)

    Public Const NT_MSG_VB As Short = 1

    Public Const NT_NOERROR As Integer = 0
    Public Const NT_ERROR As Integer = -1

    Public Const NTReqConMM As Short = -2
    Public Const NTReqGetMM As Short = -3
    Public Const NTReqConST As Short = -4
    Public Const NTReqGetST As Short = -5
    Public Const NTReqClose As Short = -9
    Public Const NTTerminate As Short = -99


    Public Const NT_CONNECTMACMAN As Short = 1
    Public Const NT_GETMACMAN As Short = 2
    Public Const NT_CONNECTSTATUS As Short = 3
    Public Const NT_GETSTATUS As Short = 4
    Public Const NT_CLOSE As Short = 5

    Public Const NTBASEERR As Integer = 20000
    Public Const NTEBADF As Integer = (NTBASEERR + 1) ' Bad file(NTHD) number
    Public Const NTENOMEM As Integer = (NTBASEERR + 2) ' Not enough memory
    Public Const NTEINVAL As Integer = (NTBASEERR + 3) ' Invalid argument
    Public Const NTEMFILE As Integer = (NTBASEERR + 4) ' Too many open files
    Public Const NTEALREADY As Integer = (NTBASEERR + 5) ' Operation already in progress
    Public Const NTEWOULDBLOCK As Integer = (NTBASEERR + 6) ' Operation would block
    Public Const NTENOTSUPPORTED As Integer = (NTBASEERR + 7) ' Cannot support this function
    Public Const NTECONNRESET As Integer = (NTBASEERR + 8) ' Connection reset by peer
    Public Const NTETIMEDOUT As Integer = (NTBASEERR + 9) ' Operation timed out
    Public Const NTERESINVAL As Integer = (NTBASEERR + 51) ' Invalid response
    Public Const NTEMNPRECVED As Integer = (NTBASEERR + 52) ' "M NP" received
    Public Const NTEMERRECVED As Integer = (NTBASEERR + 53) ' "M ER" received
    Public Const NTETBDRECVED As Integer = (NTBASEERR + 54) ' "T BD" received
End Module