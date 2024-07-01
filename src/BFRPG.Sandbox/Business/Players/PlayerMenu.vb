Friend Module PlayerMenu
    'TODO: rename player?

    Friend Sub Run(context As DataContext, ui As IUIContext, playerId As Integer)
        Dim done = False
        While Not done
            ui.Clear()
            Dim details = Players.ReadDetails(context.Connection, playerId)
            ui.Write(
                (Mood.Info, $"Player Name: {details.UniqueName}"),
                (Mood.Info, $"Character Count: {details.CharacterCount}"))
            Dim menu As New List(Of String) From
                {
                    Choices.GoBack
                }
            If details.CharacterCount = 0 Then
                menu.Add(Choices.Delete)
            End If
            menu.Add(Choices.Rename)
            menu.Add(Choices.NewCharacter)
            Dim table As Dictionary(Of String, Integer) =
            Characters.AllForPlayer(context.Connection, playerId).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.CharacterId)
            menu.AddRange(table.Keys)
            Dim answer = ui.Choose((Mood.Prompt, Prompts.PlayerMenu), menu.ToArray)
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
                    NewCharacterName.Run(context, ui, playerId)
                Case Else
                    Dim characterId = table(answer)
                    CharacterMenu.Run(context, ui, characterId)
            End Select
        End While
    End Sub

End Module
