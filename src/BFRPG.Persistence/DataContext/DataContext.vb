Public Class DataContext
    Implements IDataContext
    Private ReadOnly store As IStore

    Public ReadOnly Property Abilities As IAbilities Implements IDataContext.Abilities
        Get
            Return New Abilities(store)
        End Get
    End Property

    Public ReadOnly Property Characters As ICharacters Implements IDataContext.Characters
        Get
            Return New Characters(store)
        End Get
    End Property

    Public ReadOnly Property Players As IPlayers Implements IDataContext.Players
        Get
            Return New Players(store)
        End Get
    End Property

    Public ReadOnly Property Races As IRaces Implements IDataContext.Races
        Get
            Return New Races(store)
        End Get
    End Property

    Public ReadOnly Property RaceClasses As IRaceClasses Implements IDataContext.RaceClasses
        Get
            Return New RaceClasses(store)
        End Get
    End Property

    Sub New(store As IStore)
        Me.store = store
    End Sub
End Class
