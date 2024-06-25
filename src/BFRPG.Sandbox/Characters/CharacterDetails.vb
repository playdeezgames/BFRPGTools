Friend Class CharacterDetails
    Friend ReadOnly Property CharacterId As Integer
    Friend ReadOnly Property CharacterName As String
    Sub New(characterId As Integer, characterName As String)
        Me.CharacterId = characterId
        Me.CharacterName = characterName
    End Sub
End Class
