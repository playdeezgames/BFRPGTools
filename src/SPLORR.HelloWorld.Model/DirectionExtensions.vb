Imports System.Runtime.CompilerServices

Public Module DirectionExtensions
    <Extension>
    Public Function StepX(direction As Direction, x As Integer, y As Integer) As Integer
        Select Case direction
            Case Direction.North
                Return x
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
