Public Interface ICell
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    Sub SetConnection(direction As Direction)
    Sub TurnRight()
    Sub TurnLeft()
    Function HasConnection(direction As Direction) As Boolean
    Sub ToggleLock()
    Sub Unlock()
    ReadOnly Property IsLocked As Boolean
    ReadOnly Property Value As Integer
    ReadOnly Property IsLit As Boolean
    Sub Light()
End Interface
