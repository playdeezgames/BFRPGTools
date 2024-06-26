Friend Module NewCharacterRace
    Friend Sub Run(context As DataContext, playerId As Integer, characterName As String)
        Dim allAbilities = Abilities.All(context.Connection)
        Dim abilityScores = allAbilities.ToDictionary(Function(x) x.AbilityId, Function(x) RNG.RollDice(3, 6))
        AnsiConsole.MarkupLine("Ability Scores:")
        For Each ability In allAbilities
            AnsiConsole.MarkupLine($"{ability.AbilityName}: {abilityScores(ability.AbilityId)}")
        Next
        Dim qualifiedRaces = Races.
            All(context.Connection).
            Where(Function(race) abilityScores.All(Function(x) RaceAbilityRanges.Valid(context.Connection, race.RaceId, x.Key, x.Value))).
            ToDictionary(Function(x) $"{x.RaceName}(Id={x.RaceId})", Function(x) x.RaceId)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Race?[/]"}
        prompt.AddChoices(qualifiedRaces.Keys)
        Dim raceId = qualifiedRaces(AnsiConsole.Prompt(prompt))
        Dim characterId = Characters.Create(context.Connection, playerId, characterName, raceId).Value
        For Each score In abilityScores
            CharacterAbilities.Write(context.Connection, characterId, score.Key, score.Value)
        Next
        CharacterMenu.Run(context, characterId)
    End Sub
End Module
