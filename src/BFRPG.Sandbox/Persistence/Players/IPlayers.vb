Friend Interface IPlayers
    Sub Delete(
            playerId As Integer)
    Sub Rename(
              playerId As Integer,
              playerName As String)
    Function Create(
                playerName As String) As Integer?
    Function ReadDetails(
                        playerId As Integer) As PlayerDetails
    Function All() As IEnumerable(Of PlayerDetails)
    Function FindForName(playerName As String) As Integer?
End Interface
