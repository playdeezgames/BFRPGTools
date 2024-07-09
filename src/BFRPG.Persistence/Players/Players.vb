Friend Class Players
    Implements IPlayers

    Public Sub New(store As IStore)
        Me.store = store
    End Sub

    Private ReadOnly store As IStore

    Public Sub Delete(playerId As Integer) Implements IPlayers.Delete
        store.Delete(Tables.Players, New Dictionary(Of String, Object) From {{Columns.PlayerId, playerId}})
    End Sub

    Public Sub Rename(playerId As Integer, playerName As String) Implements IPlayers.Rename
        store.Update(
            Tables.Players,
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerName, playerName}
            },
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerId, playerId}
            })
    End Sub

    Public Function Create(playerName As String) As Integer? Implements IPlayers.Create
        Dim result = store.Create(
            Tables.Players,
            New Dictionary(Of String, Object) From
            {
                {Columns.PlayerName, playerName}
            },
            {
                Columns.PlayerId
            })
        If result Is Nothing Then
            Return Nothing
        End If
        Return CInt(result(Columns.PlayerId))
    End Function

    Public Function ReadDetails(playerId As Integer) As PlayerDetails Implements IPlayers.ReadDetails
        Return store.Retrieve(
        {
    Columns.PlayerId,
    Columns.PlayerName,
    Columns.CharacterCount
        },
        Views.PlayerDetails,
        New Dictionary(Of String, Object) From
        {
            {Columns.PlayerId, playerId}
        }).Select(Function(x) New PlayerDetails(
                    x(Columns.PlayerId),
                    x(Columns.PlayerName),
                    x(Columns.CharacterCount))).FirstOrDefault
    End Function

    Public Function All() As IEnumerable(Of PlayerDetails) Implements IPlayers.All
        Return store.Retrieve(
        {
            Columns.PlayerId,
            Columns.PlayerName,
            Columns.CharacterCount
        },
        Views.PlayerDetails).
        Select(Function(x) New PlayerDetails(
                    x(Columns.PlayerId),
                    x(Columns.PlayerName),
                    x(Columns.CharacterCount)))
    End Function

    Public Function FindForName(playerName As String) As Integer? Implements IPlayers.FindForName
        Dim result = store.Retrieve({Columns.PlayerId}, Tables.Players, New Dictionary(Of String, Object) From {{Columns.PlayerName, playerName}}).FirstOrDefault
        If result IsNot Nothing Then
            Return CInt(result(Columns.PlayerId))
        End If
        Return Nothing
    End Function
End Class
