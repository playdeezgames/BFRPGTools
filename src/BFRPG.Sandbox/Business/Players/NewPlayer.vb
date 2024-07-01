Friend Module NewPlayer
    Friend Sub Run(context As DataContext, ui As IUIContext)
        Dim playerName = Trim(ui.Ask((Mood.Prompt, Prompts.NewPlayerName), String.Empty))
        If Not String.IsNullOrWhiteSpace(playerName) Then
            Dim playerId = Players.Create(context.Connection, playerName)
            If Not playerId.HasValue Then
                ui.Message((Mood.Danger, Messages.DuplicatePlayerName))
                Return
            End If
            PlayerMenu.Run(context, ui, playerId.Value)
        End If
    End Sub
End Module
