Friend Module TitleState
    Function Update(context As IUIContext(Of Hue, Command, Sfx, Model.Model)) As GameState
        While context.Command.HasCommand
            context.Command.ReadCommand()
        End While
        Return GameState.Title
    End Function
End Module
