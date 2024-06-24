﻿Friend Module PickPlayer

    Friend Sub RunPickPlayer(connection As MySqlConnection)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Player?[/]"}
        prompt.AddChoice(GoBackText)
        Dim table As Dictionary(Of String, Integer) = All(connection)
        prompt.AddChoices(table.Keys)
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case GoBackText
                Return
            Case Else
                Dim playerId = table(answer)
                RunPlayerMenu(connection, playerId)
        End Select
    End Sub

End Module
