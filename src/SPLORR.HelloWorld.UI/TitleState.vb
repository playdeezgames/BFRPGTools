Friend Module TitleState
    Function Update(context As IUIContext(Of Hue, Command, Sfx, Model)) As GameState
        While context.Command.HasCommand
            Select Case context.Command.ReadCommand
                Case Command.Down
                    context.Model.Y += 1
                Case Command.Left
                    context.Model.X -= 1
                Case Command.Right
                    context.Model.X += 1
                Case Command.Up
                    context.Model.Y -= 1
                Case Command.A
                    context.Model.Hue = Hue.White
                Case Command.B
                    context.Model.Hue = Hue.Black
                Case Command.Start
                    context.Display.WriteAll(context.Model.Hue)
            End Select
        End While
        context.Display.Write(context.Model.X, context.Model.Y, context.Model.Hue)
        Return GameState.Title
    End Function
End Module
