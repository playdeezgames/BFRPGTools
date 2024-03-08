Public Class GraphicBuffer_should
    <Fact>
    Sub have_width_and_height()
        Const Columns = 2
        Const Rows = 3
        Dim subject As IGraphicBuffer = New GraphicBuffer(Columns, Rows)
        subject.Columns.ShouldBe(Columns)
        subject.Rows.ShouldBe(Rows)
    End Sub
End Class

