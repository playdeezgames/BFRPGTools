Friend Class InPlayState
    Inherits BaseGameState(Of GameState, Hue, Command, Sfx, HWModel, HWAssets)

    Private Function HandleInput(commandBuffer As ICommandBuffer(Of Command), model As HWModel) As GameState?
        While commandBuffer.HasCommand
            Select Case commandBuffer.ReadCommand()
                Case Command.Start
                    Return GameState.GameMenu
                Case Command.Down
                    model.World.MoveDown()
                Case Command.Left
                    model.World.MoveLeft()
                Case Command.Right
                    model.World.MoveRight()
                Case Command.Up
                    model.World.MoveUp()
                Case Command.A
                    model.World.TurnRight()
                Case Command.B
                    model.World.TurnLeft()
                Case Command.Select
                    model.World.ToggleLock()
            End Select
        End While
        Return Nothing
    End Function

    Public Overrides Function Update(context As IUIContext(Of Hue, Command, Sfx, HWModel, HWAssets), elapsedTime As TimeSpan) As GameState
        Dim result = HandleInput(context.Command, context.Model)
        If result.HasValue Then
            Return result.Value
        End If
        context.Model.World.Light()
        Draw(context.Display, context.Assets.Font, context.Assets.Pipes, context.Model)
        context.Model.World.Darken()
        Return GameState.InPlay
    End Function

    Private Sub Draw(display As IPixelBuffer(Of Hue), font As Font, pipes As Font, model As HWModel)
        display.WriteAll(Hue.Black)
        Dim columnWidth = pipes.TextWidth(ChrW(0))
        Dim rowHeight = pipes.Height
        Dim x = 0
        Dim world = model.World
        For Each column In Enumerable.Range(0, world.Columns)
            Dim y = 0
            For Each row In Enumerable.Range(0, world.Rows)
                Dim cell = world.GetCell(column, row)
                pipes.WriteText(display, (x, y), ChrW(cell.Value), DetermineHue(cell))
                If column = world.SelectedColumn AndAlso row = world.SelectedRow Then
                    pipes.WriteText(display, (x, y), ChrW(16), Hue.Yellow)
                End If
                y += rowHeight
            Next
            x += columnWidth
        Next
    End Sub

    Private Shared Function DetermineHue(cell As Persistence.ICell) As Hue
        Dim hue As Hue = Hue.Red
        If cell.IsLit Then
            If cell.IsLocked Then
                hue = Hue.LightGreen
            Else
                hue = Hue.Green
            End If
        Else
            If cell.IsLocked Then
                hue = Hue.LightRed
            End If
        End If
        Return hue
    End Function
End Class
