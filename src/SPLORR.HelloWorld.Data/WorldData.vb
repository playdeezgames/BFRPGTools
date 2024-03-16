Public Class WorldData
    Property BoardColumn As List(Of BoardColumnData)
    Sub New(columns As Integer, rows As Integer)
        BoardColumn = Enumerable.Range(0, columns).Select(Function(x) New BoardColumnData(rows)).ToList
    End Sub
End Class
