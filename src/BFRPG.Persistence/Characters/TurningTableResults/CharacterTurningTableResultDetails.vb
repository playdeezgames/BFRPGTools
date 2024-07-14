Public Class CharacterTurningTableResultDetails
    ReadOnly Property CharacterId As Integer
    ReadOnly Property CharacterName As String
    ReadOnly Property TurningTableHitDice As Integer
    ReadOnly Property TurningTableHitDieName As String
    ReadOnly Property TurningTableIndicator As String
    Sub New(
           characterId As Object,
           characterName As Object,
           turningTableHitDice As Object,
           turningTableHitDieName As Object,
           turningTableIndicator As Object)
        Me.CharacterId = CInt(characterId)
        Me.CharacterName = CStr(characterName)
        Me.TurningTableHitDice = CInt(turningTableHitDice)
        Me.TurningTableHitDieName = CStr(turningTableHitDieName)
        Me.TurningTableIndicator = CStr(turningTableIndicator)
    End Sub
End Class
