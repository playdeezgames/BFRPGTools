Friend Class CharacterAbilityDetails
    Public Sub New(
                  characterId As Integer,
                  characterName As String,
                  abilityId As Integer,
                  abilityName As String,
                  abilityAbbreviation As String,
                  abilityScore As Integer)
        Me.CharacterId = characterId
        Me.CharacterName = characterName
        Me.AbilityId = abilityId
        Me.AbilityName = abilityName
        Me.AbilityAbbreviation = abilityAbbreviation
        Me.AbilityScore = abilityScore
    End Sub

    Public ReadOnly Property CharacterId As Integer
    Public ReadOnly Property CharacterName As String
    Public ReadOnly Property AbilityId As Integer
    Public ReadOnly Property AbilityName As String
    Public ReadOnly Property AbilityAbbreviation As String
    Public ReadOnly Property AbilityScore As Integer
End Class
