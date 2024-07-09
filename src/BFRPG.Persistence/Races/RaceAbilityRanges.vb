Friend Class RaceClassAbilityRanges
    Implements IRaceClassAbilityRanges

    Private ReadOnly raceClassId As Integer
    Private ReadOnly store As IStore

    Public Sub New(store As IStore, raceClassId As Integer)
        Me.raceClassId = raceClassId
        Me.store = store
    End Sub

    Function Valid(
                abilityId As Integer,
                abilityScore As Integer) As Boolean Implements IRaceClassAbilityRanges.Valid
        Dim result = store.ReadAll(
            {
                Columns.MaximumScore,
                Columns.MinimumScore
            },
            Views.RaceClassAbilityRanges,
            New Dictionary(Of String, Object) From
            {
                {Columns.RaceClassId, raceClassId},
                {Columns.AbilityId, abilityId}
            }).FirstOrDefault
        If result IsNot Nothing Then
            Return abilityScore >= CInt(result(Columns.MinimumScore)) AndAlso abilityScore <= CInt(result(Columns.MaximumScore))
        End If
        Return False
    End Function
End Class
