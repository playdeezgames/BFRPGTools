Friend Module TitleState
    Function Update(context As IUIContext(Of Hue, Command, Sfx, Model.Model), elapsedTime As TimeSpan) As GameState
        While context.Command.HasCommand
            context.Command.ReadCommand()
        End While
        Return GameState.Title
    End Function
End Module
