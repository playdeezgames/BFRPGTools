Public Class CharacterAbilityDetails
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
    Private Shared ReadOnly modifierTable As IReadOnlyDictionary(Of Integer, Integer) =
        New Dictionary(Of Integer, Integer) From
        {
            {3, -3},
            {4, -2},
            {5, -2},
            {6, -1},
            {7, -1},
            {8, -1},
            {9, 0},
            {10, 0},
            {11, 0},
            {12, 0},
            {13, 1},
            {14, 1},
            {15, 1},
            {16, 2},
            {17, 2},
            {18, 3}
        }
    Public ReadOnly Property CharacterId As Integer
    Public ReadOnly Property CharacterName As String
    Public ReadOnly Property AbilityId As Integer
    Public ReadOnly Property AbilityName As String
    Public ReadOnly Property AbilityAbbreviation As String
    Public ReadOnly Property AbilityScore As Integer
    Public ReadOnly Property Modifier As Integer
        Get
            Return modifierTable(AbilityScore)
        End Get
    End Property
End Class
