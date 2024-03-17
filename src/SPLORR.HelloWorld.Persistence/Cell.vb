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

    Protected ReadOnly Property BoardCellData As BoardCellData
        Get
            Return data.BoardColumn(Column).Cell(Row)
        End Get
    End Property

    Public Function HasConnection(direction As Direction) As Boolean Implements ICell.HasConnection
        Return BoardCellData.Connections.Contains(direction)
    End Function
End Class
