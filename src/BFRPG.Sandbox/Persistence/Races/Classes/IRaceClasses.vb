Friend Interface IRaceClasses
    Function ReadDetails(raceClassId As Integer) As RaceClassDetails
    Function All() As IEnumerable(Of RaceClassDetails)
End Interface
