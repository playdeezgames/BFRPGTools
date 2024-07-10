Friend Class NewCharacterDescriptionState
    Inherits BaseState

    Private ReadOnly playerId As Integer
    Private ReadOnly characterName As String
    Private ReadOnly raceClassId As Integer
    Private ReadOnly abilityScores As IReadOnlyDictionary(Of Integer, Integer)

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, playerId As Integer, characterName As String, raceClassId As Integer, abilityScores As IReadOnlyDictionary(Of Integer, Integer))
        MyBase.New(data, ui, endState)
        Me.playerId = playerId
        Me.characterName = characterName
        Me.raceClassId = raceClassId
        Me.abilityScores = abilityScores
    End Sub

    Public Overrides Function Run() As IState
        Const startingExperiencePoints = 0
        Dim characterDescription = ui.Ask((Mood.Prompt, Prompts.CharacterDescription), String.Empty)
        Dim characterId = data.Characters.Create(
            playerId,
            characterName,
            raceClassId,
            startingExperiencePoints,
            characterDescription,
            RNG.RollDice(3, 6) * 10).Value
        For Each score In abilityScores
            data.Characters.Abilities(characterId).Write(score.Key, score.Value)
        Next
        Dim raceClassDetails = data.RaceClasses.ReadDetails(RaceClassId)
        Dim hitDieSize = raceClassDetails.HitDieSize
        Dim hitDiceCount = raceClassDetails.MaximumHitDice
        For Each index In Enumerable.Range(1, hitDiceCount)
            data.Characters.HitDice(characterId).Write(index, RNG.RollDice(1, hitDieSize))
        Next
        Return New CharacterMenuState(data, ui, endState, characterId)
    End Function
End Class
