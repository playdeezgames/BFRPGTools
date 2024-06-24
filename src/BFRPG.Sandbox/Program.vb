Imports System.IO

Module Program

    Sub Main(args As String())
        Using connection As New MySqlConnection(File.ReadAllText(ConnectionStringFilename))
            Try
                connection.Open()
                MainMenu(connection)
            Catch ex As Exception
                AnsiConsole.WriteException(ex)
                Console.ReadLine()
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub MainMenu(connection As MySqlConnection)
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
                    NewPlayer(connection)
                Case ChoosePlayerText
                    PickPlayer(connection)
            End Select
        End While
    End Sub

    Private Sub PickPlayer(connection As MySqlConnection)
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
                PlayerMenu(connection, playerId)
        End Select
    End Sub

    Private Sub PlayerMenu(connection As MySqlConnection, playerId As Integer)
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

    Private Sub NewPlayer(connection As MySqlConnection)
        Dim playerName = AnsiConsole.Ask(NewPlayerNamePrompt, String.Empty)
        If Not String.IsNullOrWhiteSpace(playerName) Then
            PlayerMenu(connection, Create(connection, playerName))
        End If
    End Sub
End Module
