Public Interface IWorld
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Function GetCell(column As Integer, row As Integer) As ICell
    Sub MoveDown()
    Sub MoveLeft()
    Sub MoveRight()
    Sub MoveUp()
    Sub TurnRight()
    Sub TurnLeft()
    Sub ToggleLock()
    ReadOnly Property SelectedColumn As Integer
    ReadOnly Property SelectedRow As Integer
    Sub Light()
    Sub Darken()
    ReadOnly Property CenterColumn As Integer
    ReadOnly Property CenterRow As Integer
End Interface
