Public Class GraphicBuffer_should
    <Fact>
    Sub have_width_and_height()
        Const Columns = 2
        Const Rows = 3

        Dim subject As IGraphicBuffer(Of Integer) = New GraphicBuffer(Of Integer)(Columns, Rows)

        subject.Columns.ShouldBe(Columns)
        subject.Rows.ShouldBe(Rows)
    End Sub

    <Fact>
    Sub read_pixels()
        Const Columns = 2
        Const Rows = 3

        Dim subject As IGraphicBuffer(Of Integer) = New GraphicBuffer(Of Integer)(Columns, Rows)
        Dim actual = subject.Read(0, 0)

        actual.ShouldBe(0)
    End Sub

    <Fact>
    Sub write_pixels()
        Const Columns = 2
        Const Rows = 3
        Const Pixel = 1

        Dim subject As IGraphicBuffer(Of Integer) = New GraphicBuffer(Of Integer)(Columns, Rows)
        subject.Write(0, 0, Pixel)
        Dim actual = subject.Read(0, 0)

        actual.ShouldBe(Pixel)
    End Sub
End Class

