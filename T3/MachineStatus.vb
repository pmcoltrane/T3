Option Strict On

''' <summary>
''' MachineStatus represents a machine status, including alarm, limit, program stop, slide hold, STM, and running state.
''' </summary>
''' <remarks></remarks>
<Flags()> Public Enum MachineStatus
    ''' <summary>
    ''' Alarm status
    ''' </summary>
    ''' <remarks></remarks>
    Alarm = 1
    ''' <summary>
    ''' Limit status
    ''' </summary>
    ''' <remarks></remarks>
    Limit = 2
    ''' <summary>
    ''' Program stop status
    ''' </summary>
    ''' <remarks></remarks>
    ProgramStop = 4
    ''' <summary>
    ''' Slide hold status
    ''' </summary>
    ''' <remarks></remarks>
    SlideHold = 8
    ''' <summary>
    ''' STM status
    ''' </summary>
    ''' <remarks></remarks>
    STM = 16
    ''' <summary>
    ''' Running status
    ''' </summary>
    ''' <remarks></remarks>
    Running = 32
End Enum