Friend Interface IDataContext
    ReadOnly Property Connection As MySqlConnection
    ReadOnly Property Abilities As IAbilities
    ReadOnly Property Characters As ICharacters
    ReadOnly Property Players As IPlayers
End Interface
