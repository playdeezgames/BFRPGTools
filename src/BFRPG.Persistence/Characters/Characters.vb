Friend Class Characters
    Implements ICharacters
    Private ReadOnly store As IStore

    Sub New(store As IStore)
        Me.store = store
    End Sub

    Public Sub Delete(characterId As Integer) Implements ICharacters.Delete
        Abilities(characterId).DeleteForCharacter()
        HitDice(characterId).DeleteForCharacter()
        store.Delete(Tables.Characters, New Dictionary(Of String, Object) From {{Columns.CharacterId, characterId}})
    End Sub

    Public Sub Rename(characterId As Integer, characterName As String) Implements ICharacters.Rename
        store.Update(
            Tables.Characters,
            New Dictionary(Of String, Object) From
            {
                {Columns.CharacterName, characterName}
            },
            New Dictionary(Of String, Object) From
            {
                {Columns.CharacterId, characterId}
            })
    End Sub

    Public Sub Transfer(characterId As Integer, playerId As Integer) Implements ICharacters.Transfer
        store.Update(
            Tables.Characters,
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerId, playerId}
            },
            New Dictionary(Of String, Object) From
            {
                {Columns.CharacterId, characterId}
            })
    End Sub

    Public Sub AddXP(characterId As Integer, experiencePoints As Integer) Implements ICharacters.AddXP
        Dim current =
            store.Retrieve(
                {
                    Columns.ExperiencePoints
                },
                Tables.Characters,
                New Dictionary(Of String, Object) From
                {
                    {Columns.CharacterId, characterId}
                }).FirstOrDefault?(Columns.ExperiencePoints)
        If current IsNot Nothing Then
            store.Update(
                Tables.Characters,
                New Dictionary(Of String, Object) From
                {
                    {Columns.ExperiencePoints, CInt(current) + experiencePoints}
                },
                New Dictionary(Of String, Object) From
                {
                    {Columns.CharacterId, characterId}
                })
        End If
    End Sub

    Public Function Create(playerId As Integer, characterName As String, raceClassId As Integer, experiencePoints As Integer, characterDescription As String) As Integer? Implements ICharacters.Create
        Dim result = store.Create(
            Tables.Characters,
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerId, playerId},
                {Columns.CharacterName, characterName},
                {Columns.RaceClassId, raceClassId},
                {Columns.ExperiencePoints, experiencePoints},
                {Columns.CharacterDescription, characterDescription}
            },
            {
                Columns.CharacterId
            })
        If result Is Nothing Then
            Return Nothing
        End If
        Return CInt(result(Columns.CharacterId))
    End Function

    Public Function ReadDetails(characterId As Integer) As CharacterDetails Implements ICharacters.ReadDetails
        Return store.Retrieve(
        {
            Columns.CharacterId,
            Columns.CharacterName,
            Columns.RaceId,
            Columns.RaceName,
            Columns.PlayerId,
            Columns.PlayerName,
            Columns.ClassId,
            Columns.ClassName,
            Columns.ExperiencePoints,
            Columns.Level,
            Columns.HitPoints,
            Columns.CharacterDescription,
            Columns.AttackBonus
        },
        Views.CharacterDetails,
        New Dictionary(Of String, Object) From
        {{Columns.CharacterId, characterId}}).
        Select(Function(reader) New CharacterDetails(
                    reader(Columns.CharacterId),
                    reader(Columns.CharacterName),
                    reader(Columns.RaceId),
                    reader(Columns.RaceName),
                    reader(Columns.PlayerId),
                    reader(Columns.PlayerName),
                    reader(Columns.ClassId),
                    reader(Columns.ClassName),
                    reader(Columns.ExperiencePoints),
                    reader(Columns.Level),
                    reader(Columns.HitPoints),
                    reader(Columns.CharacterDescription),
                    reader(Columns.AttackBonus))).FirstOrDefault
    End Function

    Public Function AllForPlayer(playerId As Integer) As IEnumerable(Of CharacterDetails) Implements ICharacters.AllForPlayer
        Return store.Retrieve(
        {
            Columns.CharacterId,
            Columns.CharacterName,
            Columns.RaceId,
            Columns.RaceName,
            Columns.PlayerId,
            Columns.PlayerName,
            Columns.ClassId,
            Columns.ClassName,
            Columns.ExperiencePoints,
            Columns.Level,
            Columns.HitPoints,
            Columns.CharacterDescription,
            Columns.AttackBonus
        },
        Views.CharacterDetails,
        New Dictionary(Of String, Object) From
        {{Columns.PlayerId, playerId}}).
        Select(Function(x) New CharacterDetails(
                    x(Columns.CharacterId),
                    x(Columns.CharacterName),
                    x(Columns.RaceId),
                    x(Columns.RaceName),
                    x(Columns.PlayerId),
                    x(Columns.PlayerName),
                    x(Columns.ClassId),
                    x(Columns.ClassName),
                    x(Columns.ExperiencePoints),
                    x(Columns.Level),
                    x(Columns.HitPoints),
                    x(Columns.CharacterDescription),
                    x(Columns.AttackBonus)))
    End Function

    Public Function FindForPlayerAndName(playerId As Integer, characterName As String) As Integer? Implements ICharacters.FindForPlayerAndName
        Dim result = store.Retrieve(
            {
                Columns.CharacterId
            },
            Tables.Characters,
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerId, playerId},
                {Columns.CharacterName, characterName}
            }).FirstOrDefault
        If result Is Nothing Then
            Return Nothing
        End If
        Return CInt(result(Columns.CharacterId))
    End Function

    Public Function HitDice(characterId As Integer) As ICharacterHitDice Implements ICharacters.HitDice
        Return New CharacterHitDice(store, characterId)
    End Function

    Public Function Abilities(characterId As Integer) As ICharacterAbilities Implements ICharacters.Abilities
        Return New CharacterAbilities(store, characterId)
    End Function
End Class
