Public Class DirectionExtensions_should
    <Theory>
    <InlineData(Direction.North, 1, 2, 1)>
    <InlineData(Direction.South, 1, 2, 1)>
    <InlineData(Direction.East, 1, 2, 2)>
    <InlineData(Direction.West, 1, 2, 0)>
    Sub step_in_x_direction(givenDirection As Direction, givenX As Integer, givenY As Integer, expectedX As Integer)
        Dim actual = givenDirection.StepX(givenX, givenY)
        actual.ShouldBe(expectedX)
    End Sub

    <Theory>
    <InlineData(Direction.North, 1, 2, 1)>
    <InlineData(Direction.South, 1, 2, 3)>
    <InlineData(Direction.East, 1, 2, 2)>
    <InlineData(Direction.West, 1, 2, 2)>
    Sub step_in_y_direction(givenDirection As Direction, givenX As Integer, givenY As Integer, expectedY As Integer)
        Dim actual = givenDirection.StepY(givenX, givenY)
        actual.ShouldBe(expectedY)
    End Sub
End Class

