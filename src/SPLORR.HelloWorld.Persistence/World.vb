Public Class World
    Implements IWorld

    Private ReadOnly data As WorldData

    Public Sub New(data As WorldData)
        Me.data = data
    End Sub

    Public ReadOnly Property Columns As Integer Implements IWorld.Columns
        Get
            Return data.BoardColumn.Count
        End Get
    End Property

    Public ReadOnly Property Rows As Integer Implements IWorld.Rows
        Get
            Return data.BoardColumn(0).Cell.Count
        End Get
    End Property

    Public ReadOnly Property SelectedColumn As Integer Implements IWorld.SelectedColumn
        Get
            Return data.SelectedColumn
        End Get
    End Property

    Public ReadOnly Property SelectedRow As Integer Implements IWorld.SelectedRow
        Get
            Return data.SelectedRow
        End Get
    End Property

    Public Sub MoveDown() Implements IWorld.MoveDown
        data.SelectedRow = Math.Min(data.SelectedRow + 1, Rows - 1)
    End Sub

    Public Sub MoveLeft() Implements IWorld.MoveLeft
        data.SelectedColumn = Math.Max(data.SelectedColumn - 1, 0)
    End Sub

    Public Sub MoveRight() Implements IWorld.MoveRight
        Throw New NotImplementedException()
    End Sub

    Public Sub MoveUp() Implements IWorld.MoveUp
        data.SelectedRow = Math.Max(data.SelectedRow - 1, 0)
    End Sub

    Public Function GetCell(column As Integer, row As Integer) As ICell Implements IWorld.GetCell
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Return Nothing
        End If
        Return New Cell(data, column, row)
    End Function
End Class
