Public Interface ICell
    ReadOnly Property Column As Integer
    ReadOnly Property Row As Integer
    Function HasConnection(direction As Direction) As Boolean
End Interface
