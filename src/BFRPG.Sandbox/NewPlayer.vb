Friend Module NewPlayer

    Friend Sub RunNewPlayer(connection As MySqlConnection)
        Dim playerName = AnsiConsole.Ask(NewPlayerNamePrompt, String.Empty)
        If Not String.IsNullOrWhiteSpace(playerName) Then
            RunPlayerMenu(connection, Create(connection, playerName))
        End If
    End Sub

End Module
