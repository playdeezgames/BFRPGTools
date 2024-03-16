﻿Public Interface IWorld
    ReadOnly Property Columns As Integer
    ReadOnly Property Rows As Integer
    Function GetCell(column As Integer, row As Integer) As ICell
    ReadOnly Property SelectedColumn As Integer
    ReadOnly Property SelectedRow As Integer
End Interface
