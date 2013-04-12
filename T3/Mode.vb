Option Strict On

Public Structure Mode

    Private _mode As MachineMode
    Private _status As MachineStatus
    Private _sequenceA As String
    Private _blockA As String
    Private _sequenceB As String
    Private _blockB As String

    Friend Sub New(ByVal data As String)
        If String.IsNullOrEmpty(data) Then Throw New ArgumentException("Mode data cannot be null or empty.", "data")

        'MD:08,03,     ,0,     ,0
        'mode (hex), status (hex), seq a, blk a, seq b, blk b
        If data.StartsWith("MD:") Then data = data.Substring(3)

        Dim tokens() As String = data.Split(","c)
        If Not tokens.Length = 6 Then Throw New ArgumentException("Mode data is not in the proper format.", "data")

        _mode = DirectCast(Integer.Parse(tokens(0), Globalization.NumberStyles.HexNumber), MachineMode)
        _status = DirectCast(Integer.Parse(tokens(1), Globalization.NumberStyles.HexNumber), MachineStatus)
        _sequenceA = tokens(2).Trim()
        _blockA = tokens(3).Trim()
        _sequenceB = tokens(4).Trim()
        _blockB = tokens(5).Trim()
    End Sub

    ReadOnly Property Mode() As MachineMode
        Get
            Return _mode
        End Get
    End Property

    ReadOnly Property Status() As MachineStatus
        Get
            Return _status
        End Get
    End Property

    ReadOnly Property SequenceA() As String
        Get
            Return _sequenceA
        End Get
    End Property

    ReadOnly Property BlockA() As String
        Get
            Return _blockA
        End Get
    End Property

    ReadOnly Property SequenceB() As String
        Get
            Return _sequenceB
        End Get
    End Property

    ReadOnly Property BlockB() As String
        Get
            Return _blockB
        End Get
    End Property

    ReadOnly Property Alarm() As Boolean
        Get
            Return (_status And MachineStatus.Alarm) = MachineStatus.Alarm
        End Get
    End Property

    ReadOnly Property Limit() As Boolean
        Get
            Return (_status And MachineStatus.Limit) = MachineStatus.Limit
        End Get
    End Property

    ReadOnly Property ProgramStop() As Boolean
        Get
            Return (_status And MachineStatus.ProgramStop) = MachineStatus.ProgramStop
        End Get
    End Property

    ReadOnly Property SlideHold() As Boolean
        Get
            Return (_status And MachineStatus.SlideHold) = MachineStatus.SlideHold
        End Get
    End Property

    ReadOnly Property STM() As Boolean
        Get
            Return (_status And MachineStatus.STM) = MachineStatus.STM
        End Get
    End Property

    ReadOnly Property Running() As Boolean
        Get
            Return (_status And MachineStatus.Running) = MachineStatus.Running
        End Get
    End Property

End Structure