Option Strict On

''' <summary>
''' Axis represents an axis.
''' </summary>
''' <remarks></remarks>
Public Structure Axis
    Private _name As String
    Private _value As Double

    Friend Sub New(ByVal data As String)
        If String.IsNullOrEmpty(data) Then Throw New ArgumentException("Axis data cannot be null or empty.", "data")

        Dim nextSpace As Integer = data.IndexOf(" "c)
        If nextSpace = -1 Then Throw New ArgumentException("Axis data is not in the proper format.", "data")

        _name = data.Substring(0, nextSpace).Trim()
        Double.TryParse(data.Substring(nextSpace + 1).Trim(), _value)
    End Sub

    ''' <summary>
    ''' The axis name
    ''' </summary>
    ''' <returns>A string containing the axis name</returns>
    ''' <remarks></remarks>
    ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    ''' <summary>
    ''' The axis value
    ''' </summary>
    ''' <returns>A string containing the axis value</returns>
    ''' <remarks></remarks>
    ReadOnly Property Value() As Double
        Get
            Return _value
        End Get
    End Property

End Structure