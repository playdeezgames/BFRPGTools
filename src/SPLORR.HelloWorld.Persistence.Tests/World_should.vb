Imports System
Imports Xunit
Public Class World_should
    <Fact>
    Sub have_properties()
        Const Columns = 2
        Const Rows = 3
        Dim data As New WorldData(Columns, Rows)

        Dim subject As IWorld = New World(data)

        subject.Columns.ShouldBe(Columns)
        subject.Rows.ShouldBe(Rows)
        data.BoardColumn.Count.ShouldBe(Columns)
        data.BoardColumn.All(Function(x) x.Cell.Count = Rows).ShouldBeTrue
        subject.SelectedColumn.ShouldBe(0)
        subject.SelectedRow.ShouldBe(0)
    End Sub

    <Fact>
    Sub move_down()
        Const GivenColumns = 2
        Const GivenRows = 3
        Const InitialColumn = 0
        Const InitialRow = 0
        Const ExpectedColumn = 0
        Const ExpectedRow = 1
        Dim data As New WorldData(GivenColumns, GivenRows) With
            {
                .SelectedColumn = InitialColumn,
                .SelectedRow = InitialRow
            }
        Dim subject As IWorld = New World(data)

        subject.MoveDown()

        subject.SelectedColumn.ShouldBe(expectedColumn)
        subject.SelectedRow.ShouldBe(expectedRow)
    End Sub
End Class

