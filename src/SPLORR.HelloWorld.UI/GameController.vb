Public Class GameController(Of TPixel As Structure, TCommand, TSfx)
    Implements IGameController(Of TPixel, TCommand, TSfx)

    Sub New()
        Me.ViewWidth = 640
        Me.ViewHeight = 360
        Me.Display = New PixelBuffer(Of TPixel)(ViewWidth, ViewHeight)
        Me.Command = New CommandBuffer(Of TCommand)
        Me.ViewScale = 2
        Me.IsFullScreen = False
    End Sub

    Public Sub Update() Implements IGameController(Of TPixel, TCommand, TSfx).Update
    End Sub

    Public ReadOnly Property ViewWidth As Integer Implements IGameController(Of TPixel, TCommand, TSfx).ViewWidth

    Public ReadOnly Property ViewHeight As Integer Implements IGameController(Of TPixel, TCommand, TSfx).ViewHeight

    Private ReadOnly Property ViewScale As Integer

    Public ReadOnly Property FrameWidth As Integer Implements IGameController(Of TPixel, TCommand, TSfx).FrameWidth
        Get
            Return ViewWidth * ViewScale
        End Get
    End Property

    Public ReadOnly Property FrameHeight As Integer Implements IGameController(Of TPixel, TCommand, TSfx).FrameHeight
        Get
            Return ViewHeight * ViewScale
        End Get
    End Property

    Public ReadOnly Property WindowTitle As String Implements IGameController(Of TPixel, TCommand, TSfx).WindowTitle
        Get
            Return "SPLORR!!"
        End Get
    End Property

    Public ReadOnly Property IsFullScreen As Boolean Implements IGameController(Of TPixel, TCommand, TSfx).IsFullScreen

    Public ReadOnly Property IsQuitRequested As Boolean Implements IGameController(Of TPixel, TCommand, TSfx).IsQuitRequested
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property QueuedSfx As IEnumerable(Of TSfx) Implements IGameController(Of TPixel, TCommand, TSfx).QueuedSfx
        Get
            Return Array.Empty(Of TSfx)
        End Get
    End Property

    Public ReadOnly Property Display As IPixelBuffer(Of TPixel) Implements IGameController(Of TPixel, TCommand, TSfx).Display

    Public ReadOnly Property Command As ICommandBuffer(Of TCommand) Implements IGameController(Of TPixel, TCommand, TSfx).Command

    Public ReadOnly Property Volume As Single Implements IGameController(Of TPixel, TCommand, TSfx).Volume
        Get
            Return 1.0F
        End Get
    End Property

    Public ReadOnly Property IsMuted As Boolean Implements IGameController(Of TPixel, TCommand, TSfx).IsMuted
        Get
            Return False
        End Get
    End Property
End Class
