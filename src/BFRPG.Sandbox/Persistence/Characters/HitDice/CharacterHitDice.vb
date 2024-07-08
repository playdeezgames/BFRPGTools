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
        Using command = connection.CreateCommand
            command.CommandText = $"
INSERT IGNORE INTO `{Tables.CharacterHitDice}`
(
    `{Columns.CharacterId}`,
    `{Columns.Die}`,
    `{Columns.DieRoll}`
)
VALUES
(
    @{Columns.CharacterId},
    @{Columns.Die},
    @{Columns.DieRoll}
);"
            command.Parameters.AddWithValue(Columns.CharacterId, characterId)
            command.Parameters.AddWithValue(Columns.Die, die)
            command.Parameters.AddWithValue(Columns.DieRoll, dieRoll)
            command.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub DeleteForCharacter() Implements ICharacterHitDice.DeleteForCharacter
        Store.Delete(Tables.CharacterHitDice, New Dictionary(Of String, Object) From {{Columns.CharacterId, characterId}})
    End Sub
End Class
