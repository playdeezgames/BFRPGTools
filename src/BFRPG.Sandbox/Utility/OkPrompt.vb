Friend Module OkPrompt
    Friend Sub Run(Optional title As String = "")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = title}
        prompt.AddChoice(ChoiceOk)
        AnsiConsole.Prompt(prompt)
    End Sub
End Module
