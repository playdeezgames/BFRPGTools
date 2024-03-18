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
End Class

