Public Class MainMenuState
    Inherits BaseState
    Implements IState
    Sub New(data As IDataContext, ui As IUIContext)
        MyBase.New(data, ui, Nothing)
    End Sub
    Public Overrides Function Run() As IState Implements IState.Run
        ui.Clear()
        Select Case ui.Choose(
                (Mood.Prompt, Prompts.MainMenu),
                Choices.Players,
                Choices.Quit)
            Case Choices.Quit
                If ui.Confirm((Mood.Danger, Confirms.Quit)) Then
                    Return endState
                End If
            Case Choices.Players
                Return New PlayersMenuState(data, ui, Me)
        End Select
        Return Me
    End Function
End Class
