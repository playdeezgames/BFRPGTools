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
            Case Direction.East, Direction.West
                Return y
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Public Function RightDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.East
                Return Direction.South
            Case Direction.North
                Return Direction.East
            Case Direction.South
                Return Direction.West
            Case Direction.West
                Return Direction.North
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    <Extension>
    Public Function LeftDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.East
                Return Direction.North
            Case Direction.North
                Return Direction.West
            Case Direction.South
                Return Direction.East
            Case Direction.West
                Return Direction.South
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
