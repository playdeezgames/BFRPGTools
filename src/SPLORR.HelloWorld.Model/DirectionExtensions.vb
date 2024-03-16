Imports System.Runtime.CompilerServices

Public Module DirectionExtensions
    <Extension>
    Public Function StepX(direction As Direction, x As Integer, y As Integer) As Integer
        Select Case direction
            Case Direction.North, Direction.South
                Return x
            Case Direction.East
                Return x + 1
            Case Direction.West
                Return x - 1
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Public Function StepY(direction As Direction, x As Integer, y As Integer) As Integer
        Select Case direction
            Case Direction.North
                Return y - 1
            Case Direction.South
                Return y + 1
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
