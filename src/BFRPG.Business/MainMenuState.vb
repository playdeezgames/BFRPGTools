Public Class MainMenuState
    Inherits BaseState
    Implements IState
    Private Sub New(data As IDataContext, ui As IUIContext)
        MyBase.New(data, ui, Nothing)
    End Sub
    Public Overrides Function Run() As IState Implements IState.Run
        ui.Clear()
        ui.WriteFiglet((Mood.Title, "BFRPG Tools"))
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

    Public Shared Sub Start(data As IDataContext, ui As IUIContext)
        Dim state As IState = New MainMenuState(data, ui)
        While state IsNot Nothing
            state = state.Run()
        End While
    End Sub
End Class
