Friend Module PlayerMenu
    'TODO: rename player?

    Friend Sub Run(connection As MySqlConnection, playerId As Integer)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim details = Players.ReadDetails(connection, playerId)
            AnsiConsole.MarkupLine($"Player Id: {details.PlayerId}")
            AnsiConsole.MarkupLine($"Player Name: {details.PlayerName}")
            AnsiConsole.MarkupLine($"Character Count: {details.CharacterCount}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptPlayerMenu}
            prompt.AddChoice(ChoiceGoBack)
            If details.CharacterCount = 0 Then
                prompt.AddChoice(ChoiceDelete)
            End If
            prompt.AddChoice(ChoiceNewCharacter)
            prompt.AddChoice(ChoicePickCharacter)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChoiceGoBack
                    done = True
                Case ChoiceDelete
                    If Confirm.Run(ConfirmDeletePlayer) Then
                        Players.Delete(connection, playerId)
                        done = True
                    End If
                Case ChoiceNewCharacter
                    NewCharacterName.Run(connection, playerId)
                Case ChoicePickCharacter
                    PickCharacter.Run(connection, playerId)
            End Select
        End While
    End Sub

End Module
