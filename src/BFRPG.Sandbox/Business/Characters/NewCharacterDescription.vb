Friend Module NewCharacterDescription
    Sub Run(context As DataContext, playerId As Integer, characterName As String, raceClassId As Integer, abilityScores As IReadOnlyDictionary(Of Integer, Integer))
        Const startingExperiencePoints = 0
        Dim characterDescription = AnsiConsole.Ask(Prompts.CharacterDescription, String.Empty)
        Dim characterId = Characters.Create(
            context.Connection,
            playerId,
            characterName,
            raceClassId,
            startingExperiencePoints,
            characterDescription).Value
        For Each score In abilityScores
            CharacterAbilities.Write(context.Connection, characterId, score.Key, score.Value)
        Next
        Dim raceClassDetails = RaceClasses.ReadDetails(context.Connection, raceClassId)
        Dim hitDieSize = raceClassDetails.HitDieSize
        Dim hitDiceCount = raceClassDetails.MaximumHitDice
        For Each index In Enumerable.Range(1, hitDiceCount)
            CharacterHitDice.Write(context.Connection, characterId, index, RNG.RollDice(1, hitDieSize))
        Next
        CharacterMenu.Run(context, characterId)
    End Sub
End Module
