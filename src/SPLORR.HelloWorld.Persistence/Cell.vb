Friend Class Cell
    Implements ICell

    Private ReadOnly data As WorldData

    Public Sub New(data As WorldData, column As Integer, row As Integer)
        Me.data = data
        Me.column = column
        Me.row = row
    End Sub

    Public ReadOnly Property Column As Integer Implements ICell.Column

    Public ReadOnly Property Row As Integer Implements ICell.Row

    Public ReadOnly Property IsLocked As Boolean Implements ICell.IsLocked
        Get
            Return BoardCellData.IsLocked
        End Get
    End Property

    Private Shared ReadOnly flagTable As IReadOnlyDictionary(Of Direction, Integer) =
        New Dictionary(Of Direction, Integer) From
        {
            {Direction.North, 1},
            {Direction.East, 2},
            {Direction.South, 4},
            {Direction.West, 8}
        }

    Public ReadOnly Property Value As Integer Implements ICell.Value
        Get
            Return BoardCellData.Connections.Select(Function(x) flagTable(x)).Sum()
        End Get
    End Property

    Protected ReadOnly Property BoardCellData As BoardCellData
        Get
            Return data.BoardColumn(Column).Cell(Row)
        End Get
    End Property

    Public ReadOnly Property IsLit As Boolean Implements ICell.IsLit
        Get
            Return BoardCellData.IsLit
        End Get
    End Property

    Public Sub SetConnection(direction As Direction) Implements ICell.SetConnection
        BoardCellData.Connections.Add(direction)
    End Sub

    Public Sub TurnRight() Implements ICell.TurnRight
        Dim directions = BoardCellData.Connections.ToList
        BoardCellData.Connections.Clear()
        For Each direction In directions
            SetConnection(direction.RightDirection())
        Next
    End Sub

    Public Sub TurnLeft() Implements ICell.TurnLeft
        Dim directions = BoardCellData.Connections.ToList
        BoardCellData.Connections.Clear()
        For Each direction In directions
            SetConnection(direction.LeftDirection())
        Next
    End Sub

    Public Sub ToggleLock() Implements ICell.ToggleLock
        BoardCellData.IsLocked = Not BoardCellData.IsLocked
    End Sub

    Public Sub Unlock() Implements ICell.Unlock
        BoardCellData.IsLocked = False
    End Sub

    Public Function HasConnection(direction As Direction) As Boolean Implements ICell.HasConnection
        Return BoardCellData.Connections.Contains(direction)
    End Function

    Public Sub Light() Implements ICell.Light
        BoardCellData.IsLit = True
    End Sub
End Class
