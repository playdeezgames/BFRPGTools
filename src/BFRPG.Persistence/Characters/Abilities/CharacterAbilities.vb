Friend Class CharacterAbilities
    Implements ICharacterAbilities

    Private ReadOnly characterId As Integer
    Private ReadOnly store As IStore

    Public Sub New(store As IStore, characterId As Integer)
        Me.characterId = characterId
        Me.store = store
    End Sub

    Public Sub Write(abilityId As Integer, abilityScore As Integer) Implements ICharacterAbilities.Write
        store.Insert(
            Tables.CharacterAbilities,
            New Dictionary(Of String, Object) From
            {
                {Columns.CharacterId, characterId},
                {Columns.AbilityId, abilityId},
                {Columns.CharacterId, characterId}
            },
            updateColumns:=
            {
                Columns.AbilityScore
            })
    End Sub

    Public Sub DeleteForCharacter() Implements ICharacterAbilities.DeleteForCharacter
        store.Delete(Tables.CharacterAbilities, New Dictionary(Of String, Object) From {{Columns.CharacterId, characterId}})
    End Sub

    Public Function ReadAllDetailsForCharacter() As IEnumerable(Of CharacterAbilityDetails) Implements ICharacterAbilities.ReadAllDetailsForCharacter
        Return store.ReadAll(
            {
                Columns.CharacterId,
                Columns.CharacterName,
                Columns.AbilityId,
                Columns.AbilityName,
                Columns.AbilityAbbreviation,
                Columns.AbilityScore
            },
            Views.CharacterAbilityDetails,
            New Dictionary(Of String, Object)).
            Select(Function(x) New CharacterAbilityDetails(
                x(Columns.CharacterId),
                x(Columns.CharacterName),
                x(Columns.AbilityId),
                x(Columns.AbilityName),
                x(Columns.AbilityAbbreviation),
                x(Columns.AbilityScore)))
    End Function
End Class
