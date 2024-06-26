Friend Module Confirm

    Friend Function Run(message As String) As Boolean
        Dim prompt As New SelectionPrompt(Of String) With {.Title = $"[red]{message}[/]"}
        prompt.AddChoice(ChoiceNo)
        prompt.AddChoice(ChoiceYes)
        Select Case AnsiConsole.Prompt(prompt)
            Case ChoiceYes
                Return True
            Case Else
                Return False
        End Select
    End Function
End Module
