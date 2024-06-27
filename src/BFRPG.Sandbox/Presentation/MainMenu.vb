Friend Module MainMenu
    Friend Sub Run(context As DataContext)
        Do
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.MainMenu}
            prompt.AddChoice(Choices.Players)
            prompt.AddChoice(Choices.Quit)
            Select Case AnsiConsole.Prompt(prompt)
                Case Choices.Quit
                    If Confirm.Run(Confirms.Quit) Then
                        Exit Do
                    End If
                Case Choices.Players
                    PlayersMenu.Run(context)
            End Select
        Loop
    End Sub
End Module
