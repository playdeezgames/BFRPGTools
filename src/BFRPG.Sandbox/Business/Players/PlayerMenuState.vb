Friend Class PlayerMenuState
    Inherits BaseState
    Implements IState
    Private ReadOnly playerId As Integer

    Public Sub New(data As DataContext, ui As IUIContext, endState As IState, playerId As Integer)
        MyBase.New(data, ui, endState)
        Me.playerId = playerId
    End Sub

    Public Overrides Function Run() As IState
        ui.Clear()
        Dim details = Players.ReadDetails(data.Connection, playerId)
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
            Characters.AllForPlayer(data.Connection, playerId).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.CharacterId)
        menu.AddRange(table.Keys)
        Dim answer = ui.Choose((Mood.Prompt, Prompts.PlayerMenu), menu.ToArray)
        Select Case answer
            Case Choices.Rename
                RenamePlayer.Run(data, ui, playerId)
            Case Choices.GoBack
                Return endState
            Case Choices.Delete
                If ui.Confirm((Mood.Danger, Confirms.DeletePlayer)) Then
                    Players.Delete(data.Connection, playerId)
                    Return endState
                End If
            Case Choices.NewCharacter
                NewCharacterName.Run(data, ui, playerId)
            Case Else
                Dim characterId = table(answer)
                CharacterMenu.Run(data, ui, characterId)
        End Select
        Return Me
    End Function
End Class
