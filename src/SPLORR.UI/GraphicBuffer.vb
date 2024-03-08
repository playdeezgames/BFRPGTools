Public Class GraphicBuffer
    Implements IGraphicBuffer

    Sub New(columns As Integer, rows As Integer)
        Me.Columns = columns
        Me.Rows = rows
    End Sub

    Public ReadOnly Property Columns As Integer Implements IGraphicBuffer.Columns

    Public ReadOnly Property Rows As Integer Implements IGraphicBuffer.Rows
End Class
