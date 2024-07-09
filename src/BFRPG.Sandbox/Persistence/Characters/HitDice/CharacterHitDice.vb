Friend Class CharacterHitDice
    Implements ICharacterHitDice

    Private ReadOnly connection As MySqlConnection
    Private ReadOnly characterId As Integer
    Private ReadOnly store As IStore

    Public Sub New(connection As MySqlConnection, store As IStore, characterId As Integer)
        Me.connection = connection
        Me.characterId = characterId
        Me.store = store
    End Sub

    Public Sub Write(die As Integer, dieRoll As Integer) Implements ICharacterHitDice.Write
        store.InsertReturning(
            Tables.CharacterHitDice,
            New Dictionary(Of String, Object) From
            {
                {Columns.CharacterId, characterId},
                {Columns.Die, die},
                {Columns.DieRoll, dieRoll}
            })
    End Sub

    Public Sub DeleteForCharacter() Implements ICharacterHitDice.DeleteForCharacter
        Store.Delete(Tables.CharacterHitDice, New Dictionary(Of String, Object) From {{Columns.CharacterId, characterId}})
    End Sub
End Class
