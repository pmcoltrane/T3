Option Strict On

''' <summary>
''' Program represents the loaded part program. If present/available, this will include filename, program, subprogram, and schedule program.
''' </summary>
''' <remarks></remarks>
Public Structure Program

    Private _filename As String
    Private _program As String
    Private _subprogram As String
    Private _schedule As String

    Friend Sub New(ByVal data As String)
        If String.IsNullOrEmpty(data) Then Throw New ArgumentException("Program data cannot be null or empty.", "data")
        If data.StartsWith("PM:") Then data = data.Substring(3)

        Dim tokens() As String = data.Split(","c)
        If Not tokens.Length = 4 Then Throw New ArgumentException("Program data is not in the proper format.", "data")

        _filename = tokens(0).Trim()
        _program = tokens(1).Trim()
        _subprogram = tokens(2).Trim()
        _schedule = tokens(3).Trim()
    End Sub

    ''' <summary>
    ''' The filename of the program.
    ''' </summary>
    ''' <returns>A string containing the filename.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Filename() As String
        Get
            Return _filename
        End Get
    End Property

    ''' <summary>
    ''' The program name.
    ''' </summary>
    ''' <returns>The name of the program.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Program() As String
        Get
            Return _program
        End Get
    End Property

    ''' <summary>
    ''' The subprogram name.
    ''' </summary>
    ''' <returns>The name of the subprogram.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Subprogram() As String
        Get
            Return _subprogram
        End Get
    End Property

    ''' <summary>
    ''' The schedule program name.
    ''' </summary>
    ''' <returns>The name of the schedule program.</returns>
    ''' <remarks></remarks>
    ReadOnly Property Schedule() As String
        Get
            Return _schedule
        End Get
    End Property

End Structure