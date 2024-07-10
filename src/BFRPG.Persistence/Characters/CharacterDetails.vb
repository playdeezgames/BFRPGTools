Public Class CharacterDetails
    Public ReadOnly Property CharacterId As Integer
    Public ReadOnly Property CharacterName As String
    Public ReadOnly Property RaceId As Integer
    Public ReadOnly Property RaceName As String
    Public ReadOnly Property PlayerId As Integer
    Public ReadOnly Property PlayerName As String
    Public ReadOnly Property ClassId As Integer
    Public ReadOnly Property ClassName As String
    Public ReadOnly Property Level As Integer
    Public ReadOnly Property ExperiencePoints As Integer
    Public ReadOnly Property HitPoints As Integer
    Public ReadOnly Property CharacterDescription As String
    Public ReadOnly Property AttackBonus As Integer
    Public ReadOnly Property Money As Decimal
    Public ReadOnly Property UniqueName As String
        Get
            Return $"{CharacterName}(Id={CharacterId})"
        End Get
    End Property
    Sub New(
           characterId As Object,
           characterName As Object,
           raceId As Object,
           raceName As Object,
           playerId As Object,
           playerName As Object,
           classId As Object,
           className As Object,
           experiencePoints As Object,
           level As Object,
           hitPoints As Object,
           characterDescription As Object,
           attackBonus As Object,
           money As Object)
        Me.CharacterId = CInt(characterId)
        Me.CharacterName = CStr(characterName)
        Me.RaceId = CInt(raceId)
        Me.RaceName = CStr(raceName)
        Me.PlayerId = CInt(playerId)
        Me.PlayerName = CStr(playerName)
        Me.ClassId = CInt(classId)
        Me.ClassName = CStr(className)
        Me.ExperiencePoints = CInt(experiencePoints)
        Me.Level = CInt(level)
        Me.HitPoints = CInt(hitPoints)
        Me.CharacterDescription = If(characterDescription IsNot Nothing, CStr(characterDescription), Nothing)
        Me.AttackBonus = CInt(attackBonus)
        Me.Money = CDec(money)
    End Sub

    Friend Shared Function FromRecord(record As IReadOnlyDictionary(Of String, Object)) As CharacterDetails
        Return New CharacterDetails(
                            record(Columns.CharacterId),
                            record(Columns.CharacterName),
                            record(Columns.RaceId),
                            record(Columns.RaceName),
                            record(Columns.PlayerId),
                            record(Columns.PlayerName),
                            record(Columns.ClassId),
                            record(Columns.ClassName),
                            record(Columns.ExperiencePoints),
                            record(Columns.Level),
                            record(Columns.HitPoints),
                            record(Columns.CharacterDescription),
                            record(Columns.AttackBonus),
                            record(Columns.Money))
    End Function
End Class
