Imports SPLORR.HelloWorld.Persistence

Public Class HWModel
    Private worldData As WorldData = Nothing
    ReadOnly Property World As IWorld
        Get
            Return If(HasWorld, New World(worldData), Nothing)
        End Get
    End Property
    Public Sub New()
    End Sub
    ReadOnly Property HasWorld As Boolean
        Get
            Return worldData IsNot Nothing
        End Get
    End Property
    Const BoardColumnCount = 15
    Const BoardRowCount = 15
    Sub CreateWorld()
        worldData = New WorldData(BoardColumnCount, BoardRowCount)
        Dim maze As New Maze(Of Direction)(
            BoardColumnCount,
            BoardRowCount,
            New Dictionary(Of Direction, MazeDirection(Of Direction)) From
            {
                {Direction.North, New MazeDirection(Of Direction)(Direction.South, 0, -1)},
                {Direction.East, New MazeDirection(Of Direction)(Direction.West, 1, 0)},
                {Direction.South, New MazeDirection(Of Direction)(Direction.North, 0, 1)},
                {Direction.West, New MazeDirection(Of Direction)(Direction.East, -1, 0)}
            })
        maze.Generate()
        For Each x In Enumerable.Range(0, BoardColumnCount)
            For Each y In Enumerable.Range(0, BoardRowCount)
                Dim mazeCell = maze.GetCell(x, y)
                Dim worldCell = World.GetCell(x, y)
                For Each direction In mazeCell.Directions
                    If mazeCell.GetDoor(direction).Open Then
                        worldCell.SetConnection(direction)
                    End If
                Next
                Select Case worldCell.Value
                    Case 15
                        worldCell.ToggleLock()
                    Case 5, 10
                        worldCell.TurnLeft()
                    Case Else
                        Dim turnCount = RNG.FromRange(1, 3)
                        While turnCount > 0
                            worldCell.TurnLeft()
                            turnCount -= 1
                        End While
                End Select
            Next
        Next
    End Sub

    Public Sub AbandonWorld()
        worldData = Nothing
    End Sub
End Class
