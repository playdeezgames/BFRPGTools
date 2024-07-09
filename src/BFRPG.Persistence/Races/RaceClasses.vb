Friend Class RaceClasses
    Implements IRaceClasses

    Private ReadOnly store As IStore

    Public Sub New(store As IStore)
        Me.store = store
    End Sub

    Function ReadDetails(raceClassId As Integer) As RaceClassDetails Implements IRaceClasses.ReadDetails
        Return store.Retrieve(
        {
    Columns.RaceClassId,
    Columns.RaceId,
    Columns.RaceName,
    Columns.ClassId,
    Columns.ClassName,
    Columns.HitDieSize,
    Columns.MaximumHitDice
        },
        Views.RaceClassDetails,
        New Dictionary(Of String, Object) From
        {
        {Columns.RaceClassId, raceClassId}
        }).
        Select(Function(reader) New RaceClassDetails(
                            reader(Columns.RaceClassId),
                            reader(Columns.RaceId),
                            reader(Columns.RaceName),
                            reader(Columns.ClassId),
                            reader(Columns.ClassName),
                            reader(Columns.HitDieSize),
                            reader(Columns.MaximumHitDice))).FirstOrDefault
    End Function
    Function All() As IEnumerable(Of RaceClassDetails) Implements IRaceClasses.All
        Return store.Retrieve(
        {
    Columns.RaceClassId,
    Columns.RaceId,
    Columns.RaceName,
    Columns.ClassId,
    Columns.ClassName,
    Columns.HitDieSize,
    Columns.MaximumHitDice
        },
        Views.RaceClassDetails).
        Select(Function(reader) New RaceClassDetails(
                            reader(Columns.RaceClassId),
                            reader(Columns.RaceId),
                            reader(Columns.RaceName),
                            reader(Columns.ClassId),
                            reader(Columns.ClassName),
                            reader(Columns.HitDieSize),
                            reader(Columns.MaximumHitDice)))
    End Function

    Public Function AbilityRanges(raceClassId As Integer) As IRaceClassAbilityRanges Implements IRaceClasses.AbilityRanges
        Return New RaceClassAbilityRanges(store, raceClassId)
    End Function
End Class
