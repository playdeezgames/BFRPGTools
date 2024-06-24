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
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PlayerMenuPrompt}
            prompt.AddChoice(GoBackText)
            If characterCount = 0 Then
                prompt.AddChoice(DeleteText)
            End If
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
                Case DeleteText
                    Delete(connection, playerId)
                    done = True
            End Select
        End While
    End Sub

End Module
