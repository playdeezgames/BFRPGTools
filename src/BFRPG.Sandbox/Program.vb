Module Program
    Sub Main(args As String())
        Console.Title = "BFRPG Sandbox"
        Using connection As New MySqlConnection(Environment.GetEnvironmentVariable("ConnectionString"))
            Try
                connection.Open()
                Dim state As IState = New MainMenuState(New DataContext(New Store(connection)), New UIContext)
                While state IsNot Nothing
                    state = state.Run()
                End While
            Catch ex As Exception
                AnsiConsole.WriteException(ex)
                Console.ReadLine()
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
End Module
