Friend MustInherit Class BaseState
    Implements IState
    Protected ReadOnly data As DataContext
    Protected ReadOnly ui As IUIContext
    Protected ReadOnly endState As IState
    Sub New(data As DataContext, ui As IUIContext, endState As IState)
        Me.data = data
        Me.ui = ui
        Me.endState = endState
    End Sub

    Public MustOverride Function Run() As IState Implements IState.Run
End Class
