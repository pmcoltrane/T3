Option Strict On

''' <summary>
''' Alarm represents the machine's alarm state.
''' </summary>
''' <remarks></remarks>
Public Class Alarm
    Private _number As String
    Private _level As MachineAlarmLevel
    Private _message As String

    Friend Sub New(ByVal data As String)
        If data Is Nothing Then Throw New ArgumentException("Alarm data cannot be null.", "data")

        If data.StartsWith("AL:") Then data = data.Substring(3)

        'If there's no alarm, just set as such and return.
        If String.IsNullOrEmpty(data.Trim()) Then
            _number = ""
            _level = MachineAlarmLevel.None
            _message = ""
            Return
        End If

        'There's an alarm, so we need to parse it.
        Dim index As Integer = 0
        Dim nextSpace As Integer = data.IndexOf(" ")
        If nextSpace = -1 Then Throw New ArgumentException("Alarm data is invalid.", "data")
        _number = data.Substring(0, nextSpace)

        index = nextSpace + 1
        nextSpace = data.IndexOf(" ", index)
        If nextSpace = -1 Then Throw New ArgumentException("Alarm data is invalid.", "data")
        Select Case data.Substring(index, nextSpace - index).ToUpper()
            Case "ALARM-A"
                _level = MachineAlarmLevel.A
            Case "ALARM-B"
                _level = MachineAlarmLevel.B
            Case "ALARM-C"
                _level = MachineAlarmLevel.C
            Case "ALARM-D"
                _level = MachineAlarmLevel.D
            Case "ALARM-P"
                _level = MachineAlarmLevel.P
            Case Else
                _level = MachineAlarmLevel.None
        End Select
        index = nextSpace + 1

        _message = data.Substring(index).Trim()
    End Sub

    ''' <summary>
    ''' The alarm number
    ''' </summary>
    ''' <returns>The current alarm number, if an alarm is active.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Number() As String
        Get
            Return _number
        End Get
    End Property

    ''' <summary>
    ''' The alarm level e.g. Alarm A, B, C, D
    ''' </summary>
    ''' <returns>The alarm level, or MachineAlarmLevel.None if the alarm level could not be determined.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Level() As MachineAlarmLevel
        Get
            Return _level
        End Get
    End Property

    ''' <summary>
    ''' The alarm message
    ''' </summary>
    ''' <returns>A string containing the alarm message.</returns>
    ''' <remarks>For now, the message contains the alarm number, alarm level e.g. "ALARM_A", and the alarm message.</remarks>
    ReadOnly Property Message() As String
        Get
            Return _message
        End Get
    End Property

End Class