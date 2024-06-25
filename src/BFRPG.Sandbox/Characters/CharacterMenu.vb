Friend Module CharacterMenu
    Friend Sub Run(connection As MySqlConnection, characterId As Integer)
        Do
            AnsiConsole.Clear()
            AnsiConsole.MarkupLine($"Character Id: {characterId}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Character Menu:[/]"}
            prompt.AddChoice(ChoiceGoBack)
            Select Case AnsiConsole.Prompt(prompt)
                Case ChoiceGoBack
                    Exit Do
            End Select
        Loop
    End Sub
End Module
