Friend Module MainMenu
    Friend Sub Run(context As DataContext)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptMainMenu}
            prompt.AddChoice(ChoiceNewPlayer)
            prompt.AddChoice(ChoiceExistingPlayer)
            prompt.AddChoice(ChoiceQuit)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChoiceQuit
                    done = Confirm.Run(ConfirmQuit)
                Case ChoiceNewPlayer
                    NewPlayer.Run(context)
                Case ChoiceExistingPlayer
                    PickPlayer.Run(context)
            End Select
        End While
    End Sub
End Module
