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
    <InlineData(2, 3, 0, 1, 0, 2)>
    <InlineData(2, 3, 0, 2, 0, 2)>
    Sub move_down(givenColumns As Integer, givenRows As Integer, initialColumn As Integer, initialRow As Integer, expectedColumn As Integer, expectedRow As Integer)
        Dim data As New WorldData(givenColumns, givenRows) With
            {
                .SelectedColumn = initialColumn,
                .SelectedRow = initialRow
            }
        Dim subject As IWorld = New World(data)

        subject.MoveDown()

        subject.SelectedColumn.ShouldBe(expectedColumn)
        subject.SelectedRow.ShouldBe(expectedRow)
    End Sub

    <Theory>
    <InlineData(2, 3, 0, 1, 0, 0)>
    <InlineData(2, 3, 0, 0, 0, 0)>
    <InlineData(2, 3, 0, 2, 0, 1)>
    Sub move_up(givenColumns As Integer, givenRows As Integer, initialColumn As Integer, initialRow As Integer, expectedColumn As Integer, expectedRow As Integer)
        Dim data As New WorldData(givenColumns, givenRows) With
            {
                .SelectedColumn = initialColumn,
                .SelectedRow = initialRow
            }
        Dim subject As IWorld = New World(data)

        subject.MoveUp()

        subject.SelectedColumn.ShouldBe(expectedColumn)
        subject.SelectedRow.ShouldBe(expectedRow)
    End Sub

    <Theory>
    <InlineData(2, 3, 1, 0, 0, 0)>
    <InlineData(2, 3, 0, 0, 0, 0)>
    <InlineData(2, 3, 2, 0, 1, 0)>
    Sub move_left(givenColumns As Integer, givenRows As Integer, initialColumn As Integer, initialRow As Integer, expectedColumn As Integer, expectedRow As Integer)
        Dim data As New WorldData(givenColumns, givenRows) With
            {
                .SelectedColumn = initialColumn,
                .SelectedRow = initialRow
            }
        Dim subject As IWorld = New World(data)

        subject.MoveLeft()

        subject.SelectedColumn.ShouldBe(expectedColumn)
        subject.SelectedRow.ShouldBe(expectedRow)
    End Sub

    <Theory>
    <InlineData(2, 3, 0, 0, 1, 0)>
    <InlineData(2, 3, 1, 0, 1, 0)>
    <InlineData(2, 3, 2, 0, 1, 0)>
    Sub move_right(givenColumns As Integer, givenRows As Integer, initialColumn As Integer, initialRow As Integer, expectedColumn As Integer, expectedRow As Integer)
        Dim data As New WorldData(givenColumns, givenRows) With
            {
                .SelectedColumn = initialColumn,
                .SelectedRow = initialRow
            }
        Dim subject As IWorld = New World(data)

        subject.MoveRight()

        subject.SelectedColumn.ShouldBe(expectedColumn)
        subject.SelectedRow.ShouldBe(expectedRow)
    End Sub

    <Theory>
    <InlineData(Direction.North, Direction.East)>
    <InlineData(Direction.East, Direction.South)>
    <InlineData(Direction.South, Direction.West)>
    <InlineData(Direction.West, Direction.North)>
    Sub turning_right(givenDirection As Direction, expectedDirection As Direction)
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim subject As IWorld = New World(data)
        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).SetConnection(givenDirection)

        subject.TurnRight()
        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).HasConnection(givenDirection).ShouldBeFalse()
        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).HasConnection(expectedDirection).ShouldBeTrue()
    End Sub

    <Theory>
    <InlineData(Direction.North, Direction.West)>
    <InlineData(Direction.East, Direction.North)>
    <InlineData(Direction.South, Direction.East)>
    <InlineData(Direction.West, Direction.South)>
    Sub turning_left(givenDirection As Direction, expectedDirection As Direction)
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim subject As IWorld = New World(data)
        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).SetConnection(givenDirection)

        subject.TurnLeft()
        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).HasConnection(givenDirection).ShouldBeFalse()
        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).HasConnection(expectedDirection).ShouldBeTrue()
    End Sub

    <Fact>
    Sub toggle_lock()
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim subject As IWorld = New World(data)

        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).IsLocked.ShouldBeFalse
        subject.ToggleLock()
        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).IsLocked.ShouldBeTrue
        subject.ToggleLock()
        subject.GetCell(subject.SelectedColumn, subject.SelectedRow).IsLocked.ShouldBeFalse
    End Sub

    <Fact>
    Sub light_only_center_when_disconnected()
        Const GivenColumns = 3
        Const GivenRows = 5
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim subject As IWorld = New World(data)
        subject.Light()

        For Each column In Enumerable.Range(0, GivenColumns)
            For Each row In Enumerable.Range(0, GivenRows)
                subject.GetCell(column, row).IsLit.ShouldBe(column = subject.CenterColumn AndAlso row = subject.CenterRow)
            Next
        Next
    End Sub

    <Fact>
    Sub light_all_when_connected()
        Const GivenColumns = 3
        Const GivenRows = 5
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim subject As IWorld = New World(data)
        For Each column In Enumerable.Range(0, GivenColumns)
            For Each row In Enumerable.Range(0, GivenRows)
                Dim cell = subject.GetCell(column, row)
                cell.SetConnection(Direction.North)
                cell.SetConnection(Direction.East)
                cell.SetConnection(Direction.South)
                cell.SetConnection(Direction.West)
            Next
        Next
        subject.Light()
        For Each column In Enumerable.Range(0, GivenColumns)
            For Each row In Enumerable.Range(0, GivenRows)
                subject.GetCell(column, row).IsLit.ShouldBeTrue()
            Next
        Next
        subject.Darken()
        For Each column In Enumerable.Range(0, GivenColumns)
            For Each row In Enumerable.Range(0, GivenRows)
                subject.GetCell(column, row).IsLit.ShouldBeFalse()
            Next
        Next
    End Sub

    <Fact>
    Sub have_center_column_and_row()
        Const GivenColumns = 3
        Const GivenRows = 5
        Const ExpectedColumn = 1
        Const ExpectedRow = 2
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim subject As IWorld = New World(data)
        subject.CenterColumn.ShouldBe(ExpectedColumn)
        subject.CenterRow.ShouldBe(ExpectedRow)
    End Sub
End Class

