Public Class BaseGameController(Of TState, TPixel As Structure, TCommand, TSfx)
    Implements IGameController(Of TPixel, TCommand, TSfx)

    Private ReadOnly sfxQueue As New List(Of TSfx)
    Private ReadOnly states As IReadOnlyDictionary(Of TState, Func(Of IUIContext(Of TPixel, TCommand, TSfx), TState))
    Private state As TState
    Private checkQuitRequested As Func(Of TState, Boolean)
    Sub New(
           config As IHostConfig,
           windowTitle As String,
           checkQuitRequested As Func(Of TState, Boolean),
           states As IReadOnlyDictionary(Of TState, Func(Of IUIContext(Of TPixel, TCommand, TSfx), TState)),
           state As TState)
        Me.Config = config
        Me.WindowTitle = windowTitle
        Me.Display = New PixelBuffer(Of TPixel)(config.ViewWidth, config.ViewHeight)
        Me.Command = New CommandBuffer(Of TCommand)
        Me.checkQuitRequested = checkQuitRequested
        Me.states = states
        Me.state = state
    End Sub

    Public ReadOnly Property Display As IPixelBuffer(Of TPixel) Implements IGameController(Of TPixel, TCommand, TSfx).Display

    Public ReadOnly Property Command As ICommandBuffer(Of TCommand) Implements IGameController(Of TPixel, TCommand, TSfx).Command

    Public ReadOnly Property ViewWidth As Integer Implements IGameController(Of TPixel, TCommand, TSfx).ViewWidth
        Get
            Return config.ViewWidth
        End Get
    End Property

    Public ReadOnly Property ViewHeight As Integer Implements IGameController(Of TPixel, TCommand, TSfx).ViewHeight
        Get
            Return config.ViewHeight
        End Get
    End Property

    Public ReadOnly Property FrameWidth As Integer Implements IGameController(Of TPixel, TCommand, TSfx).FrameWidth
        Get
            Return config.ViewWidth * config.ViewScale
        End Get
    End Property

    Public ReadOnly Property FrameHeight As Integer Implements IGameController(Of TPixel, TCommand, TSfx).FrameHeight
        Get
            Return config.ViewHeight * config.ViewScale
        End Get
    End Property

    Public ReadOnly Property WindowTitle As String Implements IGameController(Of TPixel, TCommand, TSfx).WindowTitle

    Public ReadOnly Property IsFullScreen As Boolean Implements IGameController(Of TPixel, TCommand, TSfx).IsFullScreen
        Get
            Return config.IsFullScreen
        End Get
    End Property

    Public ReadOnly Property IsQuitRequested As Boolean Implements IGameController(Of TPixel, TCommand, TSfx).IsQuitRequested
        Get
            Return checkQuitRequested(state)
        End Get
    End Property

    Public ReadOnly Property Volume As Single Implements IGameController(Of TPixel, TCommand, TSfx).Volume
        Get
            Return config.Volume
        End Get
    End Property

    Public ReadOnly Property IsMuted As Boolean Implements IGameController(Of TPixel, TCommand, TSfx).IsMuted
        Get
            Return config.IsMuted
        End Get
    End Property

    Public ReadOnly Property QueuedSfx As IEnumerable(Of TSfx) Implements IGameController(Of TPixel, TCommand, TSfx).QueuedSfx
        Get
            Return sfxQueue
        End Get
    End Property

    Public ReadOnly Property Config As IHostConfig Implements IUIContext(Of TPixel, TCommand, TSfx).Config

    Public Sub Update() Implements IGameController(Of TPixel, TCommand, TSfx).Update
        state = states(state)(Me)
        sfxQueue.Clear()
    End Sub

    Public Sub Play(sfx As TSfx) Implements IUIContext(Of TPixel, TCommand, TSfx).Play
        sfxQueue.Add(sfx)
    End Sub
End Class
