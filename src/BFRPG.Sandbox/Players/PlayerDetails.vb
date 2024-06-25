Friend Class PlayerDetails
    Public Sub New(playerId As Integer, playerName As String, characterCount As Integer)
        Me.PlayerId = playerId
        Me.PlayerName = playerName
        Me.CharacterCount = characterCount
    End Sub

    Public ReadOnly Property PlayerId As Integer
    Public ReadOnly Property PlayerName As String
    Public ReadOnly Property CharacterCount As Integer
End Class
