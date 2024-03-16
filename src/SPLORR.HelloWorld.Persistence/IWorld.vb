Public Interface IWorld
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Function GetCell(column As Integer, row As Integer) As ICell
    Sub MoveDown()
    Sub MoveLeft()
    Sub MoveRight()
    Sub MoveUp()
    ReadOnly Property SelectedColumn As Integer
    ReadOnly Property SelectedRow As Integer
End Interface
