Public Class RaceClassDetails
    Public Sub New(
                  raceClassId As Object,
                  raceId As Object,
                  raceName As Object,
                  classId As Object,
                  className As Object,
                  hitDieSize As Object,
                  maximumHitDice As Object)
        Me.RaceClassId = CInt(raceClassId)
        Me.RaceId = CInt(raceId)
        Me.RaceName = CStr(raceName)
        Me.ClassId = CInt(classId)
        Me.ClassName = CStr(className)
        Me.HitDieSize = CInt(hitDieSize)
        Me.MaximumHitDice = CInt(maximumHitDice)
    End Sub

    Public ReadOnly Property RaceClassId As Integer
    Public ReadOnly Property RaceId As Integer
    Public ReadOnly Property RaceName As String
    Public ReadOnly Property ClassId As Integer
    Public ReadOnly Property ClassName As String
    Public ReadOnly Property HitDieSize As Integer
    Public ReadOnly Property MaximumHitDice As Integer
    Public ReadOnly Property UniqueName As String
        Get
            Return $"{RaceName}, {ClassName}(Id={RaceClassId})"
        End Get
    End Property

End Class
