Friend Module NewCharacterClass
    Friend Sub Run(
                  context As DataContext,
                  playerId As Integer,
                  characterName As String,
                  abilityScores As IReadOnlyDictionary(Of Integer, Integer),
                  raceId As Integer)

        Dim qualifiedClasses = Classes.
            All(context.Connection).
            Where(Function([class]) abilityScores.All(Function(x) ClassAbilityRanges.Valid(context.Connection, [class].ClassId, x.Key, x.Value))).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.ClassId)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Class?[/]"}
        prompt.AddChoices(qualifiedClasses.Keys)
        Dim classId = qualifiedClasses(AnsiConsole.Prompt(prompt))
        Dim characterId = Characters.Create(context.Connection, playerId, characterName, raceId, classId).Value
        For Each score In abilityScores
            CharacterAbilities.Write(context.Connection, characterId, score.Key, score.Value)
        Next
        CharacterMenu.Run(context, characterId)
    End Sub
End Module
