Friend Module PlayerMenu

    Friend Sub Run(connection As MySqlConnection, playerId As Integer)
        Dim done = False
        While Not done
            AnsiConsole.Clear()
            Dim playerName As String = Nothing
            Dim characterCount As Integer = Nothing
            ReadDetails(connection, playerId, playerName, characterCount)
            AnsiConsole.MarkupLine($"Player Id: {playerId}")
            AnsiConsole.MarkupLine($"Player Name: {playerName}")
            AnsiConsole.MarkupLine($"Character Count: {characterCount}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PromptPlayerMenu}
            prompt.AddChoice(ChoiceGoBack)
            If characterCount = 0 Then
                prompt.AddChoice(ChoiceDelete)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case ChoiceGoBack
                    done = True
                Case ChoiceDelete
                    If Confirm.Run(ConfirmDeletePlayer) Then
                        Delete(connection, playerId)
                        done = True
                    End If
            End Select
        End While
    End Sub

End Module
