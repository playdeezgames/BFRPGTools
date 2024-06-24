Imports System.IO

Module Program
    Private Const ConnectionStringFilename As String = "ConnectionString.txt"

    Private Const NewPlayerText As String = "New Player... " & ChrW(8)
    Private Const ChoosePlayerText As String = "Choose Player... " & ChrW(8)
    Private Const QuitText As String = "Quit " & ChrW(8)
    Private Const GoBackText = "Go Back " & ChrW(8)

    Private Const MainMenuPrompt As String = "[olive]Main Menu[/]"
    Private Const NewPlayerNamePrompt As String = "[olive]New Player Name:[/]"
    Private Const PlayerMenuPrompt As String = "[olive]Player Menu:[/]"

    Private Const TablePlayers = "players"

    Private Const ColumnPlayerName = "player_name"
    Private Const ColumnPlayerId = "player_id"

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
            Dim prompt As New SelectionPrompt(Of String) With {.Title = MainMenuPrompt}
            prompt.AddChoice(NewPlayerText)
            prompt.AddChoice(ChoosePlayerText)
            prompt.AddChoice(QuitText)
            Select Case AnsiConsole.Prompt(prompt)
                Case QuitText
                    done = True
                Case NewPlayerText
                    CreatePlayer(connection)
                Case ChoosePlayerText
                    PickPlayer(connection)
            End Select
        End While
    End Sub

    Private Sub PickPlayer(connection As MySqlConnection)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Which Player?[/]"}
        prompt.AddChoice(GoBackText)
        Dim table As New Dictionary(Of String, Integer)
        Using command = connection.CreateCommand
            command.CommandText = $"SELECT {ColumnPlayerId},{ColumnPlayerName} FROM {TablePlayers} ORDER BY {ColumnPlayerName};"
            Using reader = command.ExecuteReader
                While reader.Read
                    Dim playerId = reader.GetInt32(0)
                    Dim playerName = reader.GetString(1)
                    table(playerName) = playerId
                    prompt.AddChoice(playerName)
                End While
            End Using
        End Using
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
            Dim playerName As String
            Using command = connection.CreateCommand
                command.CommandText = $"SELECT {ColumnPlayerName} FROM {TablePlayers} WHERE {ColumnPlayerId}=@{ColumnPlayerId};"
                command.Parameters.AddWithValue(ColumnPlayerId, playerId)
                Using reader = command.ExecuteReader
                    If Not reader.Read Then
                        Return
                    End If
                    playerName = reader.GetString(0)
                End Using
            End Using
            AnsiConsole.MarkupLine($"Player Id: {playerId}")
            AnsiConsole.MarkupLine($"Player Name: {playerName}")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = PlayerMenuPrompt}
            prompt.AddChoice(GoBackText)
            Select Case AnsiConsole.Prompt(prompt)
                Case GoBackText
                    done = True
            End Select
        End While
    End Sub

    Private Sub CreatePlayer(connection As MySqlConnection)
        Dim playerName = AnsiConsole.Ask(NewPlayerNamePrompt, String.Empty)
        If Not String.IsNullOrWhiteSpace(playerName) Then
            Using command = connection.CreateCommand
                command.CommandText = $"INSERT INTO {TablePlayers}({ColumnPlayerName}) values(@{ColumnPlayerName});"
                command.Parameters.AddWithValue(ColumnPlayerName, Trim(playerName))
                command.ExecuteNonQuery()
            End Using
        End If
    End Sub
End Module
