Friend Class UIContext
    Implements IUIContext

    Public Sub Clear() Implements IUIContext.Clear
        AnsiConsole.Clear()
    End Sub

    Public Sub Message(prompt As (Mood As Mood, Text As String)) Implements IUIContext.Message
        Choose(prompt, "Ok")
    End Sub

    Public Function Choose(Of TResult)(prompt As (Mood As Mood, Text As String), ParamArray choices() As (Text As String, Value As TResult)) As TResult Implements IUIContext.Choose
        Dim table = choices.ToDictionary(Function(x) x.Text, Function(x) x.Value)
        Dim selector As New SelectionPrompt(Of String) With {.Title = $"[{prompt.Mood.ColorName}]{prompt.Text}[/]"}
        selector.AddChoices(choices.Select(Function(x) x.Text))
        Return table(AnsiConsole.Prompt(selector))
    End Function

    Public Function Choose(prompt As (Mood As Mood, Text As String), ParamArray choices() As String) As String Implements IUIContext.Choose
        Return Choose(Of String)(prompt, choices.Select(Function(x) (x, x)).ToArray)
    End Function

    Public Function Confirm(prompt As (Mood As Mood, Text As String)) As Boolean Implements IUIContext.Confirm
        Const Yes = "Yes"
        Const No = "No"
        Return Choose(prompt, No, Yes) = Yes
    End Function

    Public Function Ask(Of TResult)(prompt As (Mood As Mood, Text As String), defaultResult As TResult) As TResult Implements IUIContext.Ask
        Return AnsiConsole.Ask($"[{prompt.Mood.ColorName}]{prompt.Text}[/]", defaultResult)
    End Function
End Class
