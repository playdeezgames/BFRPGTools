Friend Class CharacterSavingThrows
    Implements ICharacterSavingThrows

    Private ReadOnly store As IStore
    Private ReadOnly characterId As Integer

    Public Sub New(store As IStore, characterId As Integer)
        Me.store = store
        Me.characterId = characterId
    End Sub

    Public Function ReadAllDetailsForCharacter() As IEnumerable(Of CharacterSavingThrowDetails) Implements ICharacterSavingThrows.ReadAllDetailsForCharacter
        Return store.Retrieve(
            {
                Columns.CharacterId,
                Columns.CharacterName,
                Columns.SavingThrowId,
                Columns.SavingThrowName,
                Columns.SavingThrow,
                Columns.SavingThrowBonus
            },
            Views.CharacterSavingThrowDetails,
            New Dictionary(Of String, Object) From
            {
                {Columns.CharacterId, characterId}
            }).
            Select(Function(x) New CharacterSavingThrowDetails(
                x(Columns.CharacterId),
                x(Columns.CharacterName),
                x(Columns.SavingThrowId),
                x(Columns.SavingThrowName),
                x(Columns.SavingThrow),
                x(Columns.SavingThrowBonus)))
    End Function
End Class
