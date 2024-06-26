Friend Module PlayerMenu
    'TODO: rename player?

    Friend Sub Run(context As DataContext, playerId As Integer)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim details = Players.ReadDetails(context.Connection, playerId)
            AnsiConsole.MarkupLine($"Player Id: {details.PlayerId}")
            AnsiConsole.MarkupLine($"Player Name: {details.PlayerName}")
            AnsiConsole.MarkupLine($"Character Count: {details.CharacterCount}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptPlayerMenu}
            prompt.AddChoice(ChoiceGoBack)
            If details.CharacterCount = 0 Then
                prompt.AddChoice(ChoiceDelete)
            End If
            prompt.AddChoice(ChoiceNewCharacter)
            prompt.AddChoice(ChoiceExistingCharacter)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChoiceGoBack
                    done = True
                Case ChoiceDelete
                    If Confirm.Run(ConfirmDeletePlayer) Then
                        Players.Delete(context.Connection, playerId)
                        done = True
                    End If
                Case ChoiceNewCharacter
                    NewCharacterName.Run(context, playerId)
                Case ChoiceExistingCharacter
                    PickCharacter.Run(context, playerId)
            End Select
        End While
    End Sub

End Module
