Friend Class CharacterDetails
    Friend ReadOnly Property CharacterId As Integer
    Friend ReadOnly Property CharacterName As String
    Friend ReadOnly Property RaceId As Integer
    Friend ReadOnly Property RaceName As String
    Friend ReadOnly Property PlayerId As Integer
    Friend ReadOnly Property PlayerName As String
    Sub New(
           characterId As Integer,
           characterName As String,
           raceId As Integer,
           raceName As String,
           playerId As Integer,
           playerName As String)
        Me.CharacterId = characterId
        Me.CharacterName = characterName
        Me.RaceId = raceId
        Me.RaceName = raceName
        Me.PlayerId = playerId
        Me.PlayerName = playerName
    End Sub
End Class
