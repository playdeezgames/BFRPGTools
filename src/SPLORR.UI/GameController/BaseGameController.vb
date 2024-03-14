Public Class BaseGameController(Of TState, TPixel As Structure, TCommand, TSfx, TModel, TAssets)
    Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets)

    Private ReadOnly sfxQueue As New List(Of TSfx)
    Private ReadOnly states As IReadOnlyDictionary(Of TState, BaseGameState(Of TState, TPixel, TCommand, TSfx, TModel, TAssets))
    Private state As TState
    Private checkQuitRequested As Func(Of TState, Boolean)
    Sub New(
           config As IHostConfig,
           windowTitle As String,
           checkQuitRequested As Func(Of TState, Boolean),
           states As IReadOnlyDictionary(Of TState, BaseGameState(Of TState, TPixel, TCommand, TSfx, TModel, TAssets)),
           state As TState,
           model As TModel,
           assets As TAssets)
        Me.Model = model
        Me.Assets = assets
        Me.Config = config
        Me.WindowTitle = windowTitle
        Me.Display = New PixelBuffer(Of TPixel)(config.ViewWidth, config.ViewHeight)
        Me.Command = New CommandBuffer(Of TCommand)
        Me.checkQuitRequested = checkQuitRequested
        Me.states = states
        Me.state = state
    End Sub

    Public ReadOnly Property Display As IPixelBuffer(Of TPixel) Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).Display

    Public ReadOnly Property Command As ICommandBuffer(Of TCommand) Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).Command

    Public ReadOnly Property ViewWidth As Integer Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).ViewWidth
        Get
            Return Config.ViewWidth
        End Get
    End Property

    Public ReadOnly Property ViewHeight As Integer Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).ViewHeight
        Get
            Return Config.ViewHeight
        End Get
    End Property

    Public ReadOnly Property FrameWidth As Integer Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).FrameWidth
        Get
            Return Config.ViewWidth * Config.ViewScale
        End Get
    End Property

    Public ReadOnly Property FrameHeight As Integer Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).FrameHeight
        Get
            Return Config.ViewHeight * Config.ViewScale
        End Get
    End Property

    Public ReadOnly Property WindowTitle As String Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).WindowTitle

    Public ReadOnly Property IsFullScreen As Boolean Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).IsFullScreen
        Get
            Return Config.IsFullScreen
        End Get
    End Property

    Public ReadOnly Property IsQuitRequested As Boolean Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).IsQuitRequested
        Get
            Return checkQuitRequested(state)
        End Get
    End Property

    Public ReadOnly Property Volume As Single Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).Volume
        Get
            Return Config.Volume
        End Get
    End Property

    Public ReadOnly Property IsMuted As Boolean Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).IsMuted
        Get
            Return Config.Volume <= 0.0F
        End Get
    End Property

    Public ReadOnly Property QueuedSfx As IEnumerable(Of TSfx) Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).QueuedSfx
        Get
            Return sfxQueue
        End Get
    End Property

    Public ReadOnly Property Config As IHostConfig Implements IUIContext(Of TPixel, TCommand, TSfx, TModel, TAssets).Config

    Public ReadOnly Property Model As TModel Implements IUIContext(Of TPixel, TCommand, TSfx, TModel, TAssets).Model

    Public ReadOnly Property Assets As TAssets Implements IUIContext(Of TPixel, TCommand, TSfx, TModel, TAssets).Assets

    Public Sub Update(elapsedTime As TimeSpan) Implements IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets).Update
        sfxQueue.Clear()
        state = states(state).Update(Me, elapsedTime)
    End Sub

    Public Sub Play(sfx As TSfx) Implements IUIContext(Of TPixel, TCommand, TSfx, TModel, TAssets).Play
        sfxQueue.Add(sfx)
    End Sub
End Class
