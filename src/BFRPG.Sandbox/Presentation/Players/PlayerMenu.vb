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
            Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.PlayerMenu}
            prompt.AddChoice(Choices.GoBack)
            If details.CharacterCount = 0 Then
                prompt.AddChoice(Choices.Delete)
            End If
            prompt.AddChoice(Choices.RenamePlayer)
            prompt.AddChoice(Choices.NewCharacter)
            prompt.AddChoice(Choices.ExistingCharacter)
            Select Case AnsiConsole.Prompt(prompt)
                Case Choices.RenamePlayer
                    RenamePlayer.Run(context, playerId)
                Case Choices.GoBack
                    done = True
                Case Choices.Delete
                    If Confirm.Run(Confirms.DeletePlayer) Then
                        Players.Delete(context.Connection, playerId)
                        done = True
                    End If
                Case NewCharacter
                    NewCharacterName.Run(context, playerId)
                Case ExistingCharacter
                    PickCharacter.Run(context, playerId)
            End Select
        End While
    End Sub

End Module
