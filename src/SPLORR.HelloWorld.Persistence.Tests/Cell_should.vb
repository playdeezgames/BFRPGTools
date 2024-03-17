Public Class Cell_should
    <Fact>
    Sub have_column_and_row()
        Const Columns = 2
        Const Rows = 3
        Const Column = 0
        Const Row = 1
        Dim data As New WorldData(Columns, Rows)
        Dim world As New World(data)
        Dim subject = world.GetCell(Column, Row)

        subject.Column.ShouldBe(Column)
        subject.Row.ShouldBe(Row)
    End Sub

    <Theory>
    <InlineData(Direction.North)>
    <InlineData(Direction.East)>
    <InlineData(Direction.South)>
    <InlineData(Direction.West)>
    Sub have_no_connections(givenDirection As Direction)
        Const GivenColumns = 2
        Const GivenRows = 3
        Const GivenColumn = 0
        Const GivenRow = 1
        Const ExpectedConnection = False
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim world As New World(data)
        Dim subject = world.GetCell(GivenColumn, GivenRow)

        Dim actual = subject.HasConnection(givenDirection)
        actual.ShouldBe(ExpectedConnection)
    End Sub
End Class
