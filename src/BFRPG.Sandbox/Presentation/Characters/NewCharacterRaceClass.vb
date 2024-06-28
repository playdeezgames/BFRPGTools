Friend Module NewCharacterRaceClass
    Friend Sub Run(context As DataContext, playerId As Integer, characterName As String)
        Dim allAbilities = Abilities.All(context.Connection)
        Dim abilityScores As IReadOnlyDictionary(Of Integer, Integer) = allAbilities.ToDictionary(Function(x) x.AbilityId, Function(x) RNG.RollDice(3, 6))
        AnsiConsole.MarkupLine("Ability Scores:")
        For Each ability In allAbilities
            AnsiConsole.MarkupLine($"{ability.AbilityName}: {abilityScores(ability.AbilityId)}")
        Next
        Dim qualifiedRaceClasses = RaceClasses.
            All(context.Connection).
            Where(Function(raceClass) abilityScores.All(Function(x) RaceClassAbilityRanges.Valid(context.Connection, raceClass.RaceClassId, x.Key, x.Value))).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.RaceClassId)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = Prompts.WhichRaceClass}
        prompt.AddChoices(qualifiedRaceClasses.Keys)
        Dim raceClassId = qualifiedRaceClasses(AnsiConsole.Prompt(prompt))
        Dim characterId = Characters.Create(context.Connection, playerId, characterName, raceClassId).Value
        For Each score In abilityScores
            CharacterAbilities.Write(context.Connection, characterId, score.Key, score.Value)
        Next
        CharacterMenu.Run(context, characterId)
    End Sub

End Module
