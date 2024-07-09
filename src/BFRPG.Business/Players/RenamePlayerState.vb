Friend Class RenamePlayerState
    Inherits BaseState
    Implements IState
    Private ReadOnly playerId As Integer

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, playerId As Integer)
        MyBase.New(data, ui, endState)
        Me.playerId = playerId
    End Sub

    Public Overrides Function Run() As IState
        Dim playerName = ui.Ask((Mood.Prompt, Prompts.NewPlayerName), String.Empty).Trim
        If String.IsNullOrWhiteSpace(playerName) Then
            Return endState
        End If
        If data.Players.FindForName(playerName).HasValue Then
            ui.Message((Mood.Danger, Messages.DuplicatePlayerName))
            Return endState
        End If
        data.Players.Rename(playerId, playerName)
        Return endState
    End Function
End Class
