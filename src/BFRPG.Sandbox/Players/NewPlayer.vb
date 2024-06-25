﻿Friend Module NewPlayer
    Friend Sub Run(connection As MySqlConnection)
        Dim playerName = Trim(AnsiConsole.Ask(PromptNewPlayerName, String.Empty))
        If Not String.IsNullOrWhiteSpace(playerName) Then
            Dim playerId = Create(connection, playerName)
            If Not playerId.HasValue Then
                OkPrompt.Run(MessageDuplicatePlayerName)
                Return
            End If
            PlayerMenu.Run(connection, playerId.Value)
        End If
    End Sub
End Module
