Friend Module MainMenu

    Friend Sub Run(connection As MySqlConnection)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptMainMenu}
            prompt.AddChoice(ChoiceNewPlayer)
            prompt.AddChoice(ChoiceChoosePlayer)
            prompt.AddChoice(ChoiceQuit)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChoiceQuit
                    done = Confirm.Run(ConfirmQuit)
                Case ChoiceNewPlayer
                    NewPlayer.Run(connection)
                Case ChoiceChoosePlayer
                    PickPlayer.Run(connection)
            End Select
        End While
    End Sub

End Module
