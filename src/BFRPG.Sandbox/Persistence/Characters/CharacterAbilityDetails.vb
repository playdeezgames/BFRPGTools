Friend Class CharacterAbilityDetails
    Public Sub New(
                  characterId As Object,
                  characterName As Object,
                  abilityId As Object,
                  abilityName As Object,
                  abilityAbbreviation As Object,
                  abilityScore As Object)
        Me.CharacterId = CInt(characterId)
        Me.CharacterName = CStr(characterName)
        Me.AbilityId = CInt(abilityId)
        Me.AbilityName = CStr(abilityName)
        Me.AbilityAbbreviation = CStr(abilityAbbreviation)
        Me.AbilityScore = CInt(abilityScore)
    End Sub

    Public ReadOnly Property CharacterId As Integer
    Public ReadOnly Property CharacterName As String
    Public ReadOnly Property AbilityId As Integer
    Public ReadOnly Property AbilityName As String
    Public ReadOnly Property AbilityAbbreviation As String
    Public ReadOnly Property AbilityScore As Integer
End Class
