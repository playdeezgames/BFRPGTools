Imports System
Imports Xunit

Public Class DirectionExtensions_should
    <Fact>
    Sub step_in_x_direction()
        Const givenDirection = Direction.North
        Const givenX = 1
        Const givenY = 2
        Const expectedX = 1

        Dim actual = givenDirection.StepX(givenX, givenY)
        actual.ShouldBe(expectedX)
    End Sub
End Class

