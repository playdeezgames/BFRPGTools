Friend Class PlayerMenuState
    Inherits BaseState
    Implements IState
    Private ReadOnly playerId As Integer

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, playerId As Integer)
        MyBase.New(data, ui, endState)
        Me.playerId = playerId
    End Sub

    Public Overrides Function Run() As IState
        ui.Clear()
        Dim details = data.Players.ReadDetails(playerId)
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
            data.Characters.AllForPlayer(playerId).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.CharacterId)
        menu.AddRange(table.Keys)
        Dim answer = ui.Choose((Mood.Prompt, Prompts.PlayerMenu), menu.ToArray)
        Select Case answer
            Case Choices.Rename
                Return New RenamePlayerState(data, ui, Me, playerId)
            Case Choices.GoBack
                Return endState
            Case Choices.Delete
                If ui.Confirm((Mood.Danger, Confirms.DeletePlayer)) Then
                    data.Players.Delete(playerId)
                    Return endState
                End If
            Case Choices.NewCharacter
                Return New NewCharacterNameState(data, ui, Me, playerId)
            Case Else
                Dim characterId = table(answer)
                Return New CharacterMenuState(data, ui, Me, characterId)
        End Select
        Return Me
    End Function
End Class
