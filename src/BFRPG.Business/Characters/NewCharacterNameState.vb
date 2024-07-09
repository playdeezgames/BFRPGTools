Friend Class NewCharacterNameState
    Inherits BaseState
    Implements IState

    Private ReadOnly playerId As Integer

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, playerId As Integer)
        MyBase.New(data, ui, endState)
        Me.playerId = playerId
    End Sub

    Public Overrides Function Run() As IState
        Dim characterName = ui.Ask((Mood.Prompt, Prompts.NewCharacterName), String.Empty).Trim
        If String.IsNullOrWhiteSpace(characterName) Then
            Return endState
        End If
        If data.Characters.FindForPlayerAndName(playerId, characterName).HasValue Then
            ui.Message((Mood.Danger, Messages.DuplicateCharacterName))
            Return endState
        End If
        Return New NewCharacterRaceClassState(data, ui, endState, playerId, characterName)
    End Function
End Class
