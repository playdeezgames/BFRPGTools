Imports System.IO

Module Program

    Sub Main(args As String())
        Using connection As New MySqlConnection(File.ReadAllText(ConnectionStringFilename))
            Try
                connection.Open()
                RunMainMenu(connection)
            Catch ex As Exception
                AnsiConsole.WriteException(ex)
                Console.ReadLine()
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
End Module
