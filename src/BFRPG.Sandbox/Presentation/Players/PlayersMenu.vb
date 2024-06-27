Friend Module PlayersMenu
    Friend Sub Run(context As DataContext)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.PlayersMenu}
            prompt.AddChoice(Choices.GoBack)
            prompt.AddChoice(Choices.NewPlayer)
            prompt.AddChoice(Choices.ExistingPlayer)
            Select Case AnsiConsole.Prompt(prompt)
                Case Choices.GoBack
                    Exit Do
                Case Choices.NewPlayer
                    NewPlayer.Run(context)
                Case Choices.ExistingPlayer
                    PickPlayer.Run(context)
            End Select
        Loop
    End Sub
End Module
