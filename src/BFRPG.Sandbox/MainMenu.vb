Friend Module MainMenu

    Friend Sub Run(connection As MySqlConnection)
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
                    NewPlayer.Run(connection)
                Case ChoosePlayerText
                    PickPlayer.Run(connection)
            End Select
        End While
    End Sub

End Module
