Friend Module PlayerMenu
    'TODO: rename player?

    Friend Sub Run(context As DataContext, playerId As Integer)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim details = Players.ReadDetails(context.Connection, playerId)
            AnsiConsole.MarkupLine($"Player Name: {details.UniqueName}")
            AnsiConsole.MarkupLine($"Character Count: {details.CharacterCount}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.PlayerMenu}
            prompt.AddChoice(Choices.GoBack)
            If details.CharacterCount = 0 Then
                prompt.AddChoice(Choices.Delete)
            End If
            prompt.AddChoice(Choices.Rename)
            prompt.AddChoice(Choices.NewCharacter)
            Dim table As Dictionary(Of String, Integer) =
            Characters.AllForPlayer(context.Connection, playerId).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.CharacterId)
            prompt.AddChoices(table.Keys)
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case Choices.Rename
                    RenamePlayer.Run(context, playerId)
                Case Choices.GoBack
                    done = True
                Case Choices.Delete
                    If Confirm.Run(Confirms.DeletePlayer) Then
                        Players.Delete(context.Connection, playerId)
                        done = True
                    End If
                Case Choices.NewCharacter
                    NewCharacterName.Run(context, playerId)
                Case Else
                    Dim characterId = table(answer)
                    CharacterMenu.Run(context, characterId)
            End Select
        End While
    End Sub

End Module
