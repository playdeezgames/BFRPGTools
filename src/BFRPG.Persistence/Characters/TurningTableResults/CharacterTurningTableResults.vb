Friend Class CharacterTurningTableResults
    Implements ICharacterTurningTableResults

    Public Sub New(store As IStore, characterId As Integer)
        Me.Store = store
        Me.CharacterId = characterId
    End Sub

    ReadOnly Property store As IStore
    ReadOnly Property characterId As Integer

    Public Function ReadAllDetailsForCharacter() As IEnumerable(Of CharacterTurningTableResultDetails) Implements ICharacterTurningTableResults.ReadAllDetailsForCharacter
        Return store.Retrieve(
            {
                Columns.CharacterId,
                Columns.CharacterName,
                Columns.TurningTableHitDice,
                Columns.TurningTableHitDieName,
                Columns.TurningTableIndicator
            },
            Views.CharacterTurningTableResults,
            New Dictionary(Of String, Object) From
            {
                {Columns.CharacterId, characterId}
            }).
            Select(Function(x) New CharacterTurningTableResultDetails(
                x(Columns.CharacterId),
                x(Columns.CharacterName),
                x(Columns.TurningTableHitDice),
                x(Columns.TurningTableHitDieName),
                x(Columns.TurningTableIndicator)))
    End Function
End Class
