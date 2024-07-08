Friend Class DataContext
    Implements IDataContext
    Private ReadOnly connection As MySqlConnection
    Private ReadOnly store As IStore

    Public ReadOnly Property Abilities As IAbilities Implements IDataContext.Abilities
        Get
            Return New Abilities(Connection)
        End Get
    End Property

    Public ReadOnly Property Characters As ICharacters Implements IDataContext.Characters
        Get
            Return New Characters(Connection)
        End Get
    End Property

    Public ReadOnly Property Players As IPlayers Implements IDataContext.Players
        Get
            Return New Players(Connection)
        End Get
    End Property

    Public ReadOnly Property Races As IRaces Implements IDataContext.Races
        Get
            Return New Races(Connection)
        End Get
    End Property

    Public ReadOnly Property RaceClasses As IRaceClasses Implements IDataContext.RaceClasses
        Get
            Return New RaceClasses(Connection)
        End Get
    End Property

    Sub New(connection As MySqlConnection, store As IStore)
        Me.connection = connection
        Me.store = store
    End Sub
End Class
