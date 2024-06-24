Friend Module MainMenu

    Friend Sub RunMainMenu(connection As MySqlConnection)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MainMenuPrompt}
            prompt.AddChoice(NewPlayerText)
            prompt.AddChoice(ChoosePlayerText)
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case QuitText
                    done = True
                Case NewPlayerText
                    RunNewPlayer(connection)
                Case ChoosePlayerText
                    RunPickPlayer(connection)
            End Select
        End While
    End Sub

End Module
