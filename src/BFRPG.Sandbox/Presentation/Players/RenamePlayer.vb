Friend Module RenamePlayer
    Friend Sub Run(context As DataContext, playerId As Integer)
        Dim playerName = Trim(AnsiConsole.Ask(Prompts.NewPlayerName, String.Empty))
        If String.IsNullOrWhiteSpace(playerName) Then
            Return
        End If
        If Players.FindForName(context.Connection, playerName).HasValue Then
            OkPrompt.Run(Messages.DuplicatePlayerName)
            Return
        End If
        Players.Rename(context.Connection, playerId, playerName)
    End Sub
End Module
