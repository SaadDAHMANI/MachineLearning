Public Class Range
    Public Sub New()
    End Sub

    Public Sub New(minValue As Double, maxValue As Double)

        If maxValue < minValue Then
            Throw New Exception("Interval.Max must be > interval.Min ")
        End If
        Min = minValue
        Max = maxValue
    End Sub

    Public Sub New(name As String, minValue As Double, maxValue As Double)
        Me.Name = name
        If maxValue < minValue Then
            Throw New Exception("Interval.Max must be > interval.Min ")
        End If
        Min = minValue
        Max = maxValue
    End Sub

    Public Min As Double
    Public Max As Double

    Public Property Name As String = "Search Space Interval"
End Class
