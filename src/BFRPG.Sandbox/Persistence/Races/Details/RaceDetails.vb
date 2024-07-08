Friend Class RaceDetails
    Public Sub New(
                  raceId As Object,
                  raceName As Object)
        Me.RaceId = CInt(raceId)
        Me.RaceName = CStr(raceName)
    End Sub

    Public ReadOnly Property RaceId As Integer
    Public ReadOnly Property RaceName As String
    Public ReadOnly Property UniqueName As String
        Get
            Return $"{RaceName}(Id={RaceId})"
        End Get
    End Property
End Class
