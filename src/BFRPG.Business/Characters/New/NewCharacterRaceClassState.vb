Friend Class NewCharacterRaceClassState
    Inherits BaseState
    Implements IState

    Private ReadOnly playerId As Integer
    Private ReadOnly characterName As String

    Public Sub New(
                  data As IDataContext,
                  ui As IUIContext,
                  endState As IState,
                  playerId As Integer,
                  characterName As String)
        MyBase.New(data, ui, endState)
        Me.playerId = playerId
        Me.characterName = characterName
    End Sub

    Public Overrides Function Run() As IState
        Dim allAbilities = Me.data.Abilities.All()
        Dim abilityScores As IReadOnlyDictionary(Of Integer, Integer) = allAbilities.ToDictionary(Function(x) x.AbilityId, Function(x) RNG.RollDice(3, 6))
        ui.Write((Mood.Info, "Ability Scores:"))
        For Each ability In allAbilities
            ui.Write((Mood.Info, $"{ability.AbilityName}: {abilityScores(ability.AbilityId)}"))
        Next
        Dim qualifiedRaceClasses = data.RaceClasses.
            All().
            Where(Function(raceClass) abilityScores.All(Function(x) data.RaceClasses.AbilityRanges(raceClass.RaceClassId).Valid(x.Key, x.Value))).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.RaceClassId)
        If Not qualifiedRaceClasses.Any Then
            ui.Message((Mood.Danger, Messages.NoQualifiedRaceClassesAvailable))
            Return endState
        End If
        Dim raceClassId = qualifiedRaceClasses(ui.Choose((Mood.Prompt, Prompts.WhichRaceClass), qualifiedRaceClasses.Keys.ToArray))
        Return New NewCharacterDescriptionState(data, ui, endState, playerId, characterName, raceClassId, abilityScores)
    End Function
End Class
