Public Class PixelBuffer_should
    <Fact>
    Sub have_width_and_height()
        Const Columns = 2
        Const Rows = 3

        Dim subject As IPixelBuffer(Of Integer) = New PixelBuffer(Of Integer)(Columns, Rows)

        subject.Size.Columns.ShouldBe(Columns)
        subject.Size.Rows.ShouldBe(Rows)
    End Sub

    <Fact>
    Sub read_pixels()
        Const Columns = 2
        Const Rows = 3

        Dim subject As IPixelBuffer(Of Integer) = New PixelBuffer(Of Integer)(Columns, Rows)
        Dim actual = subject.Read(0, 0)

        actual.ShouldBe(0)
    End Sub

    <Theory>
    <InlineData(2, 3, -1, 0)>
    <InlineData(2, 3, 0, -1)>
    <InlineData(2, 3, 2, 0)>
    <InlineData(2, 3, 0, 3)>
    Sub return_default_value_for_reading_out_of_bounds(columns As Integer, rows As Integer, column As Integer, row As Integer)
        Dim subject As IPixelBuffer(Of Integer) = New PixelBuffer(Of Integer)(columns, rows)
        Dim actual = subject.Read(column, row)
        Dim expected As Integer = Nothing
        actual.ShouldBe(expected)
    End Sub

    <Fact>
    Sub write_pixels()
        Const Columns = 2
        Const Rows = 3
        Const Pixel = 1

        Dim subject As IPixelBuffer(Of Integer) = New PixelBuffer(Of Integer)(Columns, Rows)
        subject.Write(0, 0, Pixel)
        Dim actual = subject.Read(0, 0)

        actual.ShouldBe(Pixel)
    End Sub

    <Theory>
    <InlineData(2, 3, -1, 0)>
    <InlineData(2, 3, 0, -1)>
    <InlineData(2, 3, 2, 0)>
    <InlineData(2, 3, 0, 3)>
    Sub not_throw_exceptions_when_writing_out_of_bounds(columns As Integer, rows As Integer, column As Integer, row As Integer)
        Dim subject As IPixelBuffer(Of Integer) = New PixelBuffer(Of Integer)(columns, rows)
        Should.NotThrow(
            Sub()
                subject.Write(column, row, 1)
            End Sub)
    End Sub
End Class

