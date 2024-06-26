Friend Class CharacterDetails
    Friend ReadOnly Property CharacterId As Integer
    Friend ReadOnly Property CharacterName As String
    Friend ReadOnly Property RaceId As Integer
    Friend ReadOnly Property RaceName As String
    Sub New(
           characterId As Integer,
           characterName As String,
           raceId As Integer,
           raceName As String)
        Me.CharacterId = characterId
        Me.CharacterName = characterName
        Me.RaceId = raceId
        Me.RaceName = raceName
    End Sub
End Class
