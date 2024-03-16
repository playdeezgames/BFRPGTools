Public Class DirectionExtensions_should
    <Theory>
    <InlineData(Direction.North, 1, 2, 1)>
    <InlineData(Direction.South, 1, 2, 1)>
    Sub step_in_x_direction(givenDirection As Direction, givenX As Integer, givenY As Integer, expectedX As Integer)
        Dim actual = givenDirection.StepX(givenX, givenY)
        actual.ShouldBe(expectedX)
    End Sub
End Class

