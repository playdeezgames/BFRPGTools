Friend Class Cell
    Implements ICell

    Private ReadOnly data As WorldData
    Private ReadOnly column As Integer
    Private ReadOnly row As Integer

    Public Sub New(data As WorldData, column As Integer, row As Integer)
        Me.data = data
        Me.column = column
        Me.row = row
    End Sub

    Public Function HasConnection(direction As Direction) As Boolean Implements ICell.HasConnection
        Return False
    End Function
End Class
