Friend Class RenameCharacterState
    Inherits BaseState

    Private ReadOnly characterId As Integer

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, characterId As Integer)
        MyBase.New(data, ui, endState)
        Me.characterId = characterId
    End Sub

    Public Overrides Function Run() As IState
        Dim characterName = ui.Ask((Mood.Prompt, Prompts.NewCharacterName), String.Empty).Trim
        If String.IsNullOrWhiteSpace(characterName) Then
            Return endState
        End If
        Dim playerId = data.Characters.ReadDetails(characterId).PlayerId
        If data.Characters.FindForPlayerAndName(playerId, characterName).HasValue Then
            ui.Message((Mood.Danger, Messages.DuplicateCharacterName))
            Return endState
        End If
        data.Characters.Rename(characterId, characterName)
        Return endState
    End Function
End Class
