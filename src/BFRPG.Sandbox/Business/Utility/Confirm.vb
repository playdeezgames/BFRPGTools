Friend Module Confirm

    Friend Function Run(message As String) As Boolean
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[red]{message}[/]"}
        prompt.AddChoice(No)
        prompt.AddChoice(Yes)
        Select Case AnsiConsole.Prompt(prompt)
            Case Yes
                Return True
            Case Else
                Return False
        End Select
    End Function
End Module
