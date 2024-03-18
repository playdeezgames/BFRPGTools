﻿Public Interface ICell
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    Sub SetConnection(direction As Direction)
    Sub TurnRight()
    Function HasConnection(direction As Direction) As Boolean
End Interface
