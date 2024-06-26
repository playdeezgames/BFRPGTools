Friend Class RaceDetails
    Public Sub New(raceId As Integer, raceName As String)
        Me.RaceId = raceId
        Me.RaceName = raceName
    End Sub

    Public ReadOnly Property RaceId As Integer
    Public ReadOnly Property RaceName As String
End Class
