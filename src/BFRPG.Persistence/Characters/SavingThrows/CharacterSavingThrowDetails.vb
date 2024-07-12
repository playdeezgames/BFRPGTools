Public Class CharacterSavingThrowDetails
    ReadOnly Property CharacterId As Integer
    ReadOnly Property CharacterName As String
    ReadOnly Property SavingThrowId As Integer
    ReadOnly Property SavingThrowName As String
    ReadOnly Property SavingThrowBonus As Integer
    ReadOnly Property SavingThrow As Integer
    Sub New(
           characterId As Object,
           characterName As Object,
           savingThrowId As Object,
           savingThrowName As Object,
           savingThrow As Object,
           savingThrowBonus As Object)
        Me.CharacterId = CInt(characterId)
        Me.CharacterName = CStr(characterName)
        Me.SavingThrowId = CInt(savingThrowId)
        Me.SavingThrowName = CStr(savingThrowName)
        Me.SavingThrow = CInt(savingThrow)
        Me.SavingThrowBonus = CInt(savingThrowBonus)
    End Sub
End Class
