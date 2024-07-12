Public Class CharacterPerquisiteDetails
    ReadOnly Property CharacterId As Integer
    ReadOnly Property CharacterName As String
    ReadOnly Property PerquisiteId As Integer
    ReadOnly Property PerquisiteName As String
    ReadOnly Property PerquisiteDescription As String
    Sub New(
           characterId As Object,
           characterName As Object,
           perquisiteId As Object,
           perquisiteName As Object,
           perquisiteDescription As Object)
        Me.CharacterId = CInt(characterId)
        Me.CharacterName = CStr(characterName)
        Me.PerquisiteId = CInt(perquisiteId)
        Me.PerquisiteName = CStr(perquisiteName)
        Me.PerquisiteDescription = CStr(perquisiteDescription)
    End Sub
End Class
