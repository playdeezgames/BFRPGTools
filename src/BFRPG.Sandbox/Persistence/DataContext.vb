Friend Class DataContext
    Friend ReadOnly Property Connection As MySqlConnection
    Sub New(connection As MySqlConnection)
        Me.Connection = connection
    End Sub
End Class
