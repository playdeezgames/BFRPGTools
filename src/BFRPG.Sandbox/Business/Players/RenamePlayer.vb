Friend Module RenamePlayer
    Friend Sub Run(context As DataContext, ui As IUIContext, playerId As Integer)
        Dim playerName = Trim(ui.Ask((Mood.Prompt, Prompts.NewPlayerName), String.Empty))
        If String.IsNullOrWhiteSpace(playerName) Then
            Return
        End If
        If Players.FindForName(context.Connection, playerName).HasValue Then
            ui.Message((Mood.Danger, Messages.DuplicatePlayerName))
            Return
        End If
        Players.Rename(context.Connection, playerId, playerName)
    End Sub
End Module
