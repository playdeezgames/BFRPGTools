Friend Module OkPrompt
    Friend Sub Run(Optional title As String = "")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = title}
        prompt.AddChoice(Ok)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
