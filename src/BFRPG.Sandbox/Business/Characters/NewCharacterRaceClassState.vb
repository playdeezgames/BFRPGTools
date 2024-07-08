Friend Class NewCharacterRaceClassState
    Inherits BaseState
    Implements IState

    Private ReadOnly playerId As Integer
    Private ReadOnly characterName As String

    Public Sub New(
                  data As DataContext,
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
        Dim qualifiedRaceClasses = RaceClasses.
            All(data.Connection).
            Where(Function(raceClass) abilityScores.All(Function(x) RaceClassAbilityRanges.Valid(data.Connection, raceClass.RaceClassId, x.Key, x.Value))).
            ToDictionary(Function(x) x.UniqueName, Function(x) x.RaceClassId)
        If Not qualifiedRaceClasses.Any Then
            ui.Message((Mood.Danger, Messages.NoQualifiedRaceClassesAvailable))
            Return endState
        End If
        Dim raceClassId = qualifiedRaceClasses(ui.Choose((Mood.Prompt, Prompts.WhichRaceClass), qualifiedRaceClasses.Keys.ToArray))
        NewCharacterDescription.Run(data, ui, playerId, characterName, raceClassId, abilityScores)
        Return endState
    End Function
End Class
