Friend Interface IRaceClasses
    Function ReadDetails(raceClassId As Integer) As RaceClassDetails
    Function All() As IEnumerable(Of RaceClassDetails)
    Function AbilityRanges(raceClassId As Integer) As IRaceClassAbilityRanges
End Interface
