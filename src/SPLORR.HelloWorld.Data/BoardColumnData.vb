Public Class BoardColumnData
    Property Cell As List(Of BoardCellData)
    Sub New(rows As Integer)
        Cell = Enumerable.Range(0, rows).Select(Function(x) New BoardCellData).ToList
    End Sub
End Class
