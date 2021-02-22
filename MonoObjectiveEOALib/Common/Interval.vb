Public Class Interval
    Public Sub New()
    End Sub

    Public Sub New(min As Double, max As Double)
        If max < min Then
            Throw New Exception("Interval.Max_Value must be > interval.Min_Value ")
        End If

        Min_Value = min
        Max_Value = max
    End Sub

    Public Sub New(name As String, min As Double, max As Double)
        Me.Name = name
        If max < min Then
            Throw New Exception("Interval.Max_Value must be > interval.Min_Value ")
        End If
        Min_Value = min
        Max_Value = max
    End Sub

    Public Min_Value As Double
    Public Max_Value As Double

    Public Property Name As String = "Search Space Interval"
End Class
