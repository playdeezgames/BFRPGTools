Friend Module NewPlayer

    Friend Sub Run(connection As MySqlConnection)
        Dim playerName = AnsiConsole.Ask(NewPlayerNamePrompt, String.Empty)
        If Not String.IsNullOrWhiteSpace(playerName) Then
            PlayerMenu.Run(connection, Create(connection, playerName))
        End If
    End Sub

End Module
