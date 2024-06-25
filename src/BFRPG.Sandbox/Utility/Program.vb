Imports System.IO

Module Program

    Sub Main(args As String())
        Using connection As New MySqlConnection(File.ReadAllText(FilenameConnectionString))
            Try
                connection.Open()
                MainMenu.Run(connection)
            Catch ex As Exception
                AnsiConsole.WriteException(ex)
                Console.ReadLine()
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
End Module
