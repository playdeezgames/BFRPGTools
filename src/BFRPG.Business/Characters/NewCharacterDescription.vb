﻿Friend Module NewCharacterDescription
    Sub Run(data As IDataContext, ui As IUIContext, playerId As Integer, characterName As String, raceClassId As Integer, abilityScores As IReadOnlyDictionary(Of Integer, Integer))
        Const startingExperiencePoints = 0
        Dim characterDescription = ui.Ask((Mood.Prompt, Prompts.CharacterDescription), String.Empty)
        Dim characterId = data.Characters.Create(
            playerId,
            characterName,
            raceClassId,
            startingExperiencePoints,
            characterDescription).Value
        For Each score In abilityScores
            data.Characters.Abilities(characterId).Write(score.Key, score.Value)
        Next
        Dim raceClassDetails = data.RaceClasses.ReadDetails(raceClassId)
        Dim hitDieSize = raceClassDetails.HitDieSize
        Dim hitDiceCount = raceClassDetails.MaximumHitDice
        For Each index In Enumerable.Range(1, hitDiceCount)
            data.Characters.HitDice(characterId).Write(index, RNG.RollDice(1, hitDieSize))
        Next
        CharacterMenu.Run(data, ui, characterId)
    End Sub
End Module