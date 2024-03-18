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

    <Fact>
    Sub set_connection()
        Const GivenColumns = 2
        Const GivenRows = 3
        Const GivenColumn = 0
        Const GivenRow = 1
        Const GivenDirection = Direction.North
        Const ExpectedConnection = True
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim world As New World(data)
        Dim subject = world.GetCell(GivenColumn, GivenRow)

        subject.SetConnection(GivenDirection)

        Dim actual = subject.HasConnection(GivenDirection)
        actual.ShouldBe(ExpectedConnection)
    End Sub

    <Theory>
    <InlineData(Direction.North, Direction.East)>
    <InlineData(Direction.East, Direction.South)>
    <InlineData(Direction.South, Direction.West)>
    <InlineData(Direction.West, Direction.North)>
    Sub turn_right(givenDirection As Direction, expectedDirection As Direction)
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim world As New World(data)
        Dim subject = world.GetCell(world.SelectedColumn, world.SelectedRow)
        subject.SetConnection(givenDirection)

        subject.TurnRight()
        subject.HasConnection(givenDirection).ShouldBeFalse
        subject.HasConnection(expectedDirection).ShouldBeTrue
    End Sub

    <Theory>
    <InlineData(Direction.North, Direction.West)>
    <InlineData(Direction.East, Direction.North)>
    <InlineData(Direction.South, Direction.East)>
    <InlineData(Direction.West, Direction.South)>
    Sub turn_left(givenDirection As Direction, expectedDirection As Direction)
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim world As New World(data)
        Dim subject = world.GetCell(world.SelectedColumn, world.SelectedRow)
        subject.SetConnection(givenDirection)

        subject.TurnLeft()
        subject.HasConnection(givenDirection).ShouldBeFalse
        subject.HasConnection(expectedDirection).ShouldBeTrue
    End Sub

    <Fact>
    Sub initially_be_unlocked()
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim world As New World(data)
        Dim subject = world.GetCell(world.SelectedColumn, world.SelectedRow)

        subject.IsLocked.ShouldBeFalse()
    End Sub

    <Fact>
    Sub lock()
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim world As New World(data)
        Dim subject = world.GetCell(world.SelectedColumn, world.SelectedRow)
        subject.ToggleLock()

        subject.IsLocked.ShouldBeTrue()
    End Sub

    <Fact>
    Sub unlock()
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim world As New World(data)
        Dim subject = world.GetCell(world.SelectedColumn, world.SelectedRow)
        subject.ToggleLock()
        subject.Unlock()

        subject.IsLocked.ShouldBeFalse()
    End Sub

    <Theory>
    <InlineData({Direction.North}, 1)>
    <InlineData({Direction.East}, 2)>
    <InlineData({Direction.South}, 4)>
    <InlineData({Direction.West}, 8)>
    <InlineData({Direction.North, Direction.East}, 3)>
    <InlineData({Direction.East, Direction.South}, 6)>
    <InlineData({Direction.South, Direction.West}, 12)>
    <InlineData({Direction.West, Direction.North}, 9)>
    <InlineData({Direction.North, Direction.South}, 5)>
    <InlineData({Direction.East, Direction.West}, 10)>
    Sub have_value(givenDirections As Direction(), expectedValue As Integer)
        Const GivenColumns = 2
        Const GivenRows = 3
        Dim data As New WorldData(GivenColumns, GivenRows)
        Dim world As New World(data)
        Dim subject = world.GetCell(world.SelectedColumn, world.SelectedRow)
        For Each direction In givenDirections
            subject.SetConnection(direction)
        Next

        subject.Value.ShouldBe(expectedValue)
    End Sub
End Class
