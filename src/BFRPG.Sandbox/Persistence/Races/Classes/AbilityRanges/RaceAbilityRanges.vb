Friend Class RaceClassAbilityRanges
    Implements IRaceClassAbilityRanges

    Private ReadOnly connection As MySqlConnection
    Private ReadOnly raceClassId As Integer

    Public Sub New(connection As MySqlConnection, raceClassId As Integer)
        Me.connection = connection
        Me.raceClassId = raceClassId
    End Sub

    Function Valid(
                         abilityId As Integer,
                         abilityScore As Integer) As Boolean Implements IRaceClassAbilityRanges.Valid
        Using command = connection.CreateCommand
            command.CommandText = $"
SELECT 
    COUNT(1) 
FROM 
    `{Views.RaceClassAbilityRanges}` 
WHERE 
    `{Columns.RaceClassId}`=@{Columns.RaceClassId} AND 
    `{Columns.AbilityId}`=@{Columns.AbilityId} AND 
    `{Columns.MaximumScore}`>=@{Columns.AbilityScore} AND 
    `{Columns.MinimumScore}`<=@{Columns.AbilityScore};"
            command.Parameters.AddWithValue(Columns.AbilityScore, abilityScore)
            command.Parameters.AddWithValue(Columns.AbilityId, abilityId)
            command.Parameters.AddWithValue(Columns.RaceClassId, raceClassId)
            Return CInt(command.ExecuteScalar) > 0
        End Using
    End Function
End Class
