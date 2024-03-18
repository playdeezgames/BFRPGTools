Imports System.Runtime.CompilerServices

Public Module DirectionExtensions
    ReadOnly stepXTable As IReadOnlyDictionary(Of Direction, Integer) =
        New Dictionary(Of Direction, Integer) From
        {
            {Direction.North, 0},
            {Direction.East, 1},
            {Direction.South, 0},
            {Direction.West, -1}
        }
    <Extension>
    Public Function StepX(direction As Direction, x As Integer, y As Integer) As Integer
        Return stepXTable(direction) + x
    End Function

    ReadOnly stepYTable As IReadOnlyDictionary(Of Direction, Integer) =
        New Dictionary(Of Direction, Integer) From
        {
            {Direction.North, -1},
            {Direction.East, 0},
            {Direction.South, 1},
            {Direction.West, 0}
        }
    <Extension>
    Public Function StepY(direction As Direction, x As Integer, y As Integer) As Integer
        Return stepYTable(direction) + y
    End Function

    ReadOnly rightDirectionTable As IReadOnlyDictionary(Of Direction, Direction) =
        New Dictionary(Of Direction, Direction) From
        {
            {Direction.North, Direction.East},
            {Direction.East, Direction.South},
            {Direction.South, Direction.West},
            {Direction.West, Direction.North}
        }
    <Extension>
    Public Function RightDirection(direction As Direction) As Direction
        Return rightDirectionTable(direction)
    End Function

    ReadOnly leftDirectionTable As IReadOnlyDictionary(Of Direction, Direction) =
        New Dictionary(Of Direction, Direction) From
        {
            {Direction.North, Direction.West},
            {Direction.East, Direction.North},
            {Direction.South, Direction.East},
            {Direction.West, Direction.South}
        }
    <Extension>
    Public Function LeftDirection(direction As Direction) As Direction
        Return leftDirectionTable(direction)
    End Function

    ReadOnly oppositeDirectionTable As IReadOnlyDictionary(Of Direction, Direction) =
        New Dictionary(Of Direction, Direction) From
        {
            {Direction.North, Direction.South},
            {Direction.East, Direction.West},
            {Direction.South, Direction.North},
            {Direction.West, Direction.East}
        }
    <Extension>
    Public Function OppositeDirection(direction As Direction) As Direction
        Return oppositeDirectionTable(direction)
    End Function
End Module
