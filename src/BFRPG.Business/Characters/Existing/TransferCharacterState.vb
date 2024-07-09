Friend Class TransferCharacterState
    Inherits BaseState

    Private ReadOnly characterId As Integer
    Private ReadOnly cancelState As IState

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, cancelState As IState, characterId As Integer)
        MyBase.New(data, ui, endState)
        Me.characterId = characterId
        Me.cancelState = cancelState
    End Sub

    Public Overrides Function Run() As IState
        Dim menu = New List(Of String) From
            {
                Choices.GoBack
            }
        Dim table = data.Players.All().ToDictionary(Function(x) x.UniqueName, Function(x) x.PlayerId)
        Dim answer = ui.Choose((Mood.Prompt, Prompts.WhichPlayer), table.Keys.ToArray)
        Select Case answer
            Case GoBack
                Return cancelState
            Case Else
                Dim playerId = table(answer)
                Dim characterName = data.Characters.ReadDetails(characterId).CharacterName
                If data.Characters.FindForPlayerAndName(playerId, characterName).HasValue Then
                    ui.Message((Mood.Danger, Messages.DuplicateCharacterName))
                    Return cancelState
                End If
                data.Characters.Transfer(characterId, playerId)
                ui.Message((Mood.Success, Messages.CharacterTransferSuccess))
                Return endState
        End Select
    End Function
End Class
