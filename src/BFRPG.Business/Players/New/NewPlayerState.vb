Friend Class NewPlayerState
    Inherits BaseState
    Implements IState

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState)
        MyBase.New(data, ui, endState)
    End Sub

    Public Overrides Function Run() As IState
        Dim playerName = ui.Ask((Mood.Prompt, Prompts.NewPlayerName), String.Empty).Trim
        If Not String.IsNullOrWhiteSpace(playerName) Then
            Dim playerId = data.Players.Create(playerName)
            If Not playerId.HasValue Then
                ui.Message((Mood.Danger, Messages.DuplicatePlayerName))
                Return endState
            End If
            Return New PlayerMenuState(data, ui, endState, playerId.Value)
        End If
        Return endState
    End Function
End Class
