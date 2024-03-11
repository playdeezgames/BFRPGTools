Public Class BaseGameController(Of TState, TPixel As Structure, TCommand, TSfx, TModel)
    Implements IGameController(Of TPixel, TCommand, TSfx, TModel)

    Private ReadOnly sfxQueue As New List(Of TSfx)
    Private ReadOnly states As IReadOnlyDictionary(Of TState, Func(Of IUIContext(Of TPixel, TCommand, TSfx, TModel), TState))
    Private state As TState
    Private checkQuitRequested As Func(Of TState, Boolean)
    Sub New(
           config As IHostConfig,
           windowTitle As String,
           checkQuitRequested As Func(Of TState, Boolean),
           states As IReadOnlyDictionary(Of TState, Func(Of IUIContext(Of TPixel, TCommand, TSfx, TModel), TState)),
           state As TState,
           model As TModel)
        Me.Model = model
        Me.Config = config
        Me.WindowTitle = windowTitle
        Me.Display = New PixelBuffer(Of TPixel)(config.ViewWidth, config.ViewHeight)
        Me.Command = New CommandBuffer(Of TCommand)
        Me.checkQuitRequested = checkQuitRequested
        Me.states = states
        Me.state = state
    End Sub

    Public ReadOnly Property Display As IPixelBuffer(Of TPixel) Implements IGameController(Of TPixel, TCommand, TSfx, TModel).Display

    Public ReadOnly Property Command As ICommandBuffer(Of TCommand) Implements IGameController(Of TPixel, TCommand, TSfx, TModel).Command

    Public ReadOnly Property ViewWidth As Integer Implements IGameController(Of TPixel, TCommand, TSfx, TModel).ViewWidth
        Get
            Return Config.ViewWidth
        End Get
    End Property

    Public ReadOnly Property ViewHeight As Integer Implements IGameController(Of TPixel, TCommand, TSfx, TModel).ViewHeight
        Get
            Return Config.ViewHeight
        End Get
    End Property

    Public ReadOnly Property FrameWidth As Integer Implements IGameController(Of TPixel, TCommand, TSfx, TModel).FrameWidth
        Get
            Return Config.ViewWidth * Config.ViewScale
        End Get
    End Property

    Public ReadOnly Property FrameHeight As Integer Implements IGameController(Of TPixel, TCommand, TSfx, TModel).FrameHeight
        Get
            Return Config.ViewHeight * Config.ViewScale
        End Get
    End Property

    Public ReadOnly Property WindowTitle As String Implements IGameController(Of TPixel, TCommand, TSfx, TModel).WindowTitle

    Public ReadOnly Property IsFullScreen As Boolean Implements IGameController(Of TPixel, TCommand, TSfx, TModel).IsFullScreen
        Get
            Return Config.IsFullScreen
        End Get
    End Property

    Public ReadOnly Property IsQuitRequested As Boolean Implements IGameController(Of TPixel, TCommand, TSfx, TModel).IsQuitRequested
        Get
            Return checkQuitRequested(state)
        End Get
    End Property

    Public ReadOnly Property Volume As Single Implements IGameController(Of TPixel, TCommand, TSfx, TModel).Volume
        Get
            Return Config.Volume
        End Get
    End Property

    Public ReadOnly Property IsMuted As Boolean Implements IGameController(Of TPixel, TCommand, TSfx, TModel).IsMuted
        Get
            Return Config.IsMuted
        End Get
    End Property

    Public ReadOnly Property QueuedSfx As IEnumerable(Of TSfx) Implements IGameController(Of TPixel, TCommand, TSfx, TModel).QueuedSfx
        Get
            Return sfxQueue
        End Get
    End Property

    Public ReadOnly Property Config As IHostConfig Implements IUIContext(Of TPixel, TCommand, TSfx, TModel).Config

    Public ReadOnly Property Model As TModel Implements IUIContext(Of TPixel, TCommand, TSfx, TModel).Model

    Public Sub Update() Implements IGameController(Of TPixel, TCommand, TSfx, TModel).Update
        sfxQueue.Clear()
        state = states(state)(Me)
    End Sub

    Public Sub Play(sfx As TSfx) Implements IUIContext(Of TPixel, TCommand, TSfx, TModel).Play
        sfxQueue.Add(sfx)
    End Sub
End Class
