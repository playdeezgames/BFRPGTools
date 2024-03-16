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

    <Theory>
    <InlineData(2, 3, 0, 0, 0, 1)>
    Sub move_down(givenColumns As Integer, givenRows As Integer, initialColumn As Integer, initialRow As Integer, expectedColumn As Integer, expectedRow As Integer)
        Dim data As New WorldData(givenColumns, givenRows) With
            {
                .SelectedColumn = initialColumn,
                .SelectedRow = initialRow
            }
        Dim subject As IWorld = New World(data)

        subject.MoveDown()

        subject.SelectedColumn.ShouldBe(ExpectedColumn)
        subject.SelectedRow.ShouldBe(ExpectedRow)
    End Sub
End Class

