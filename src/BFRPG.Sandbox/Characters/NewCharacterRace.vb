Friend Module NewCharacterRace
    Friend Sub Run(connection As MySqlConnection, playerId As Integer, characterName As String)
        Dim allAbilities = Abilities.All(connection)
        Dim abilityScores = allAbilities.ToDictionary(Function(x) x.AbilityId, Function(x) RNG.RollDice(3, 6))
        AnsiConsole.MarkupLine("Ability Scores:")
        For Each ability In allAbilities
            AnsiConsole.MarkupLine($"{ability.AbilityName}: {abilityScores(ability.AbilityId)}")
        Next
        Dim qualifiedRaces = Races.
            All(connection).
            Where(Function(race) abilityScores.All(Function(x) RaceAbilityRanges.Valid(connection, race.RaceId, x.Key, x.Value))).
            ToDictionary(Function(x) $"{x.RaceName}(Id={x.RaceId})", Function(x) x.RaceId)
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "[olive]Race?[/]"}
        prompt.AddChoices(qualifiedRaces.Keys)
        Dim raceId = qualifiedRaces(AnsiConsole.Prompt(prompt))
        Dim characterId = Characters.Create(connection, playerId, characterName, raceId).Value
        For Each abilityScore In abilityScores
            CharacterAbilities.Write(connection, characterId, abilityScore.Key, abilityScore.Value)
        Next
        CharacterMenu.Run(connection, characterId)
    End Sub
End Module
