Friend Class Races
    Implements IRaces

    Private ReadOnly store As IStore

    Public Sub New(store As IStore)
        Me.store = store
    End Sub

    Function All() As IEnumerable(Of RaceDetails) Implements IRaces.All
        Return store.Retrieve(
            {
                Columns.RaceId,
                Columns.RaceName
            },
            Tables.Races).
            Select(Function(x) New RaceDetails(
                x(Columns.RaceId),
                x(Columns.RaceName)))
    End Function
End Class
