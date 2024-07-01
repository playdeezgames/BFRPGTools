Friend Class UIContext
    Implements IUIContext

    Public Sub Clear() Implements IUIContext.Clear
        AnsiConsole.Clear()
    End Sub

    Public Function Choose(Of TResult)(prompt As (Mood As Mood, Text As String), ParamArray choices() As (Text As String, Value As TResult)) As TResult Implements IUIContext.Choose
        Dim table = choices.ToDictionary(Function(x) x.Text, Function(x) x.Value)
        Dim selector As New SelectionPrompt(Of String) With {.Title = $"[{prompt.Mood.ColorName}]{prompt.Text}[/]"}
        selector.AddChoices(choices.Select(Function(x) x.Text))
        Return table(AnsiConsole.Prompt(selector))
    End Function
End Class
