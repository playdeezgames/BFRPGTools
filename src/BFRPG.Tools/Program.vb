Imports BFRPG.Business
Imports BFRPG.Persistence
Imports BFRPG.Persistence.MySql
Imports BFRPG.Presentation
Imports BFRPG.Presentation.Spectre
Imports MySqlConnector

Module Program
    Private Const ConnectionString As String = "ConnectionString"
    Private Const Title As String = "BFRPG Tools"

    Sub Main(args As String())
        Console.Title = Title
        Dim ui As IUIContext = New UIContext
        Using connection As New MySqlConnection(Environment.GetEnvironmentVariable(ConnectionString))
            Try
                connection.Open()
                Dim state As IState = New MainMenuState(New DataContext(New Store(connection)), ui)
                While state IsNot Nothing
                    state = state.Run()
                End While
            Catch ex As Exception
                ui.WriteException(ex)
                ui.Message((Mood.Info, String.Empty))
            Finally
                connection.Close()
            End Try
        End Using
    End Sub
End Module
