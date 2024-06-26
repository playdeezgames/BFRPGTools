Friend Module NewPlayer
    Friend Sub Run(context As DataContext)
        Dim playerName = Trim(AnsiConsole.Ask(PromptNewPlayerName, String.Empty))
        If Not String.IsNullOrWhiteSpace(playerName) Then
            Dim playerId = Players.Create(context.Connection, playerName)
            If Not playerId.HasValue Then
                OkPrompt.Run(MessageDuplicatePlayerName)
                Return
            End If
            PlayerMenu.Run(context, playerId.Value)
        End If
    End Sub
End Module
