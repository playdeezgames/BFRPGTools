﻿Friend Module TransferCharacter
    Friend Function Run(context As DataContext, ui As IUIContext, characterId As Integer) As Boolean
        Dim menu = New List(Of String) From
            {
                Choices.GoBack
            }
        Dim table = Players.All(context.Connection).ToDictionary(Function(x) x.UniqueName, Function(x) x.PlayerId)
        Dim answer = ui.Choose((Mood.Prompt, Prompts.WhichPlayer), table.Keys.ToArray)
        Select Case answer
            Case GoBack
                Return False
            Case Else
                Dim playerId = table(answer)
                Dim characterName = Characters.ReadDetails(context.Connection, characterId).CharacterName
                If Characters.FindForPlayerAndName(context.Connection, playerId, characterName).HasValue Then
                    OkPrompt.Run(Messages.DuplicateCharacterName)
                    Return False
                End If
                Characters.Transfer(context.Connection, characterId, playerId)
                OkPrompt.Run(Messages.CharacterTransferSuccess)
                Return True
        End Select
    End Function
End Module
