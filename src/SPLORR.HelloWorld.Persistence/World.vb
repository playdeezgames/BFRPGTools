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

    Private Sub SetSelectedRow(newRow As Integer)
        data.SelectedRow = Math.Clamp(newRow, 0, Rows - 1)
    End Sub

    Private Sub SetSelectedColumn(newColumn As Integer)
        data.SelectedColumn = Math.Clamp(newColumn, 0, Columns - 1)
    End Sub

    Public Sub MoveDown() Implements IWorld.MoveDown
        SetSelectedRow(SelectedRow + 1)
    End Sub

    Public Sub MoveLeft() Implements IWorld.MoveLeft
        SetSelectedColumn(SelectedColumn - 1)
    End Sub

    Public Sub MoveRight() Implements IWorld.MoveRight
        SetSelectedColumn(SelectedColumn + 1)
    End Sub

    Public Sub MoveUp() Implements IWorld.MoveUp
        SetSelectedRow(SelectedRow - 1)
    End Sub

    Public Function GetCell(column As Integer, row As Integer) As ICell Implements IWorld.GetCell
        If column < 0 OrElse row < 0 OrElse column >= Columns OrElse row >= Rows Then
            Return Nothing
        End If
        Return New Cell(data, column, row)
    End Function

    Public Sub TurnRight() Implements IWorld.TurnRight
        GetCell(SelectedColumn, SelectedRow).TurnRight()
    End Sub

    Public Sub TurnLeft() Implements IWorld.TurnLeft
        GetCell(SelectedColumn, SelectedRow).TurnLeft()
    End Sub

    Public Sub Lock() Implements IWorld.Lock
        Throw New NotImplementedException()
    End Sub
End Class
