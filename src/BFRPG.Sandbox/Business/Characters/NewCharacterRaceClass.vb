Friend Module NewCharacterRaceClass
    Friend Sub Run(context As DataContext, ui As IUIContext, playerId As Integer, characterName As String)
        Dim allAbilities = Abilities.All(context.Connection)
        Dim abilityScores As IReadOnlyDictionary(Of Integer, Integer) = allAbilities.ToDictionary(Function(x) x.AbilityId, Function(x) RNG.RollDice(3, 6))
        ui.Write((Mood.Info, "Ability Scores:"))
        For Each ability In allAbilities
            ui.Write((Mood.Info, $"{ability.AbilityName}: {abilityScores(ability.AbilityId)}"))
        Next
        Dim qualifiedRaceClasses = RaceClasses.
            All(context.Connection).
            Where(Function(raceClass) abilityScores.All(Function(x) RaceClassAbilityRanges.Valid(context.Connection, raceClass.RaceClassId, x.Key, x.Value))).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.RaceClassId)
        Dim raceClassId = qualifiedRaceClasses(ui.Choose((Mood.Prompt, Prompts.WhichRaceClass), qualifiedRaceClasses.Keys.ToArray))
        NewCharacterDescription.Run(context, ui, playerId, characterName, raceClassId, abilityScores)
    End Sub
End Module
