Option Strict On

Imports System.IO

Public Structure Status
    Private _alarm As Alarm
    Private _mode As Mode
    Private _program As Program
    Private _programDataA As String
    Private _programDataB As String
    Private _axis As Dictionary(Of String, Double)
    Private _distance As Dictionary(Of String, Double)

    Public Sub New(ByVal data As String)
        'TODO: check data format and initialize
        'length should be 1864
        'but something's not quite adding up for the sizes -- esp. for program
        'at the office check actual DNC-T3 output.

        _axis = New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)
        _distance = New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)

        Using reader As New StringReader(data)
            Do
                Dim line As String = reader.ReadLine
                If line Is Nothing Then Exit Do
                Select Case line.Substring(0, 3)
                    Case "AL:"
                        _alarm = New Alarm(line)
                    Case "MD:"
                        _mode = New Mode(line)
                    Case "PM:"
                        _program = New Program(line)
                    Case "PD1"
                        _programDataA = line.Substring(4).Trim()
                    Case "PD2"
                        _programDataB = line.Substring(4).Trim()
                    Case "PO1", "PO2", "PO4"
                        Dim tokens() As String = line.Substring(4).Split(","c)
                        For Each token As String In tokens
                            Dim axis As New Axis(token)
                            If Not String.IsNullOrEmpty(axis.Name) Then _axis(axis.Name) = axis.Value
                        Next token
                    Case "PO3"
                        Dim tokens() As String = line.Substring(4).Split(","c)
                        For Each token As String In tokens
                            Dim axis As New Axis(token)
                            If Not String.IsNullOrEmpty(axis.Name) Then _distance(axis.Name) = axis.Value
                        Next token
                    Case Else
                End Select
            Loop
        End Using
    End Sub

    ReadOnly Property Alarm() As Alarm
        Get
            Return _alarm
        End Get
    End Property

    ReadOnly Property Mode() As Mode
        Get
            Return _mode
        End Get
    End Property

    ReadOnly Property Program() As Program
        Get
            Return _program
        End Get
    End Property

    ReadOnly Property ProgramDataA() As String
        Get
            Return _programDataA
        End Get
    End Property

    ReadOnly Property ProgramDataB() As String
        Get
            Return _programDataB
        End Get
    End Property

    ReadOnly Property Axis() As IDictionary(Of String, Double)
        Get
            Return _axis
        End Get
    End Property

    ReadOnly Property Distance() As IDictionary(Of String, Double)
        Get
            Return _distance
        End Get
    End Property
End Structure