Friend Class CharacterPerquisites
    Implements ICharacterPerquisites

    Private ReadOnly store As IStore
    Private ReadOnly characterId As Integer

    Public Sub New(store As IStore, characterId As Integer)
        Me.store = store
        Me.characterId = characterId
    End Sub

    Public Function ReadAllDetailsForCharacter() As IEnumerable(Of CharacterPerquisiteDetails) Implements ICharacterPerquisites.ReadAllDetailsForCharacter
        Return store.Retrieve(
            {
                Columns.CharacterId,
                Columns.CharacterName,
                Columns.PerquisiteId,
                Columns.PerquisiteName,
                Columns.PerquisiteDescription
            },
            Views.CharacterPerquisiteDetails,
            New Dictionary(Of String, Object) From
            {
                {Columns.CharacterId, characterId}
            }).
            Select(Function(x) New CharacterPerquisiteDetails(
                x(Columns.CharacterId),
                x(Columns.CharacterName),
                x(Columns.PerquisiteId),
                x(Columns.PerquisiteName),
                x(Columns.PerquisiteDescription)))
    End Function
End Class
