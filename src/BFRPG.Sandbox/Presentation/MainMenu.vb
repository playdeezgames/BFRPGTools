Friend Module MainMenu
    Friend Sub Run(context As DataContext)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.MainMenu}
            prompt.AddChoice(Choices.NewPlayer)
            prompt.AddChoice(ExistingPlayer)
            prompt.AddChoice(Quit)
            Select Case AnsiConsole.Prompt(prompt)
                Case Quit
                    done = Confirm.Run(ConfirmQuit)
                Case Choices.NewPlayer
                    NewPlayer.Run(context)
                Case ExistingPlayer
                    PickPlayer.Run(context)
            End Select
        End While
    End Sub
End Module
