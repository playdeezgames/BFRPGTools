Friend Class Abilities
    Implements IAbilities
    Private ReadOnly store As IStore

    Sub New(store As IStore)
        Me.store = store
    End Sub

    Public Function All() As IEnumerable(Of AbilityDetails) Implements IAbilities.All
        Return store.ReadAll(
            {
                Columns.AbilityId,
                Columns.AbilityName,
                Columns.AbilityAbbreviation
            },
            Tables.Abilities).
                Select(Function(x) New AbilityDetails(
                    x(Columns.AbilityId),
                    x(Columns.AbilityName),
                    x(Columns.AbilityAbbreviation)))
    End Function
End Class
