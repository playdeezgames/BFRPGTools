Public Class MazeDirection(Of TDirection)
    ReadOnly Property Opposite As TDirection
    ReadOnly Property DeltaX As Integer
    ReadOnly Property DeltaY As Integer
    Sub New(opposite As TDirection, deltaX As Integer, deltaY As Integer)
        Me.Opposite = opposite
        Me.DeltaX = deltaX
        Me.DeltaY = deltaY
    End Sub
End Class
