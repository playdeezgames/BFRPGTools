﻿Friend Class PlayerDetails
    Public Sub New(
                  playerId As Object,
                  playerName As Object,
                  characterCount As Object)
        Me.PlayerId = CInt(playerId)
        Me.PlayerName = CStr(playerName)
        Me.CharacterCount = CInt(characterCount)
    End Sub

    Public ReadOnly Property PlayerId As Integer
    Public ReadOnly Property PlayerName As String
    Public ReadOnly Property CharacterCount As Integer
End Class
