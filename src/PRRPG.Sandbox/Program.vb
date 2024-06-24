Imports System.IO
Imports MySqlConnector

Module Program
    Sub Main(args As String())
        Using connection = New MySqlConnection(File.ReadAllText("ConnectionString.txt"))
            connection.Open()
            Using command = connection.CreateCommand()
                command.CommandText = "SELECT skill_name FROM skills;"
                Using reader = command.ExecuteReader
                    While reader.Read
                        Console.WriteLine(reader.GetString(0))
                    End While
                End Using
            End Using
            connection.Close()
        End Using
        Console.ReadLine()
    End Sub
End Module
