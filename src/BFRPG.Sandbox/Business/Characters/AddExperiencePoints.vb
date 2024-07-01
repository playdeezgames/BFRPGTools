Friend Module AddExperiencePoints
    Friend Sub Run(context As DataContext, characterId As Integer)
        Dim xp As Integer = AnsiConsole.Ask(Of Integer)("[olive]How many XP?[/]")
        If xp < 0 Then
            Return
        End If
        Characters.AddXP(context.Connection, characterId, xp)
    End Sub
End Module
