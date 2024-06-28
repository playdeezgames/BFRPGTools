﻿Friend Class CharacterDetails
    Friend ReadOnly Property CharacterId As Integer
    Friend ReadOnly Property CharacterName As String
    Friend ReadOnly Property RaceId As Integer
    Friend ReadOnly Property RaceName As String
    Friend ReadOnly Property PlayerId As Integer
    Friend ReadOnly Property PlayerName As String
    Friend ReadOnly Property ClassId As Integer
    Friend ReadOnly Property ClassName As String
    Friend ReadOnly Property UniqueName As String
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
           className As Object)
        Me.CharacterId = CInt(characterId)
        Me.CharacterName = CStr(characterName)
        Me.RaceId = CInt(raceId)
        Me.RaceName = CStr(raceName)
        Me.PlayerId = CInt(playerId)
        Me.PlayerName = CStr(playerName)
        Me.ClassId = CInt(classId)
        Me.ClassName = CStr(className)
    End Sub
End Class
