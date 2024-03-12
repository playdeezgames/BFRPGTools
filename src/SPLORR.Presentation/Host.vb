Public Class Host(Of TPixel As Structure, TCommand, TSfx, TModel, TAssets)
    Inherits Game

    Private ReadOnly graphicsDeviceManager As GraphicsDeviceManager
    Private ReadOnly controller As IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets)
    Private buffer As Color()
    Private pixelBuffer As IPixelBuffer(Of Color)
    Private texture As Texture2D
    Private spriteBatch As SpriteBatch
    Private ReadOnly sfxManager As ISfxManager(Of TSfx)
    Private ReadOnly renderer As IRenderer(Of TPixel, Color)
    Private ReadOnly inputManager As IInputManager(Of TCommand)

    Sub New(
           controller As IGameController(Of TPixel, TCommand, TSfx, TModel, TAssets),
           renderer As IRenderer(Of TPixel, Color),
           inputManager As IInputManager(Of TCommand),
           sfxManager As ISfxManager(Of TSfx))
        Me.graphicsDeviceManager = New GraphicsDeviceManager(Me)
        Me.controller = controller
        Me.sfxManager = sfxManager
        Me.renderer = renderer
        Me.inputManager = inputManager
        Content.RootDirectory = "Content"
    End Sub

    Protected Overrides Sub LoadContent()
        Me.spriteBatch = New SpriteBatch(GraphicsDevice)
        Me.texture = New Texture2D(GraphicsDevice, controller.ViewWidth, controller.ViewHeight)
        ReDim Me.buffer(controller.ViewWidth * controller.ViewHeight - 1)
        Me.pixelBuffer = New PixelBuffer(Of Color)(controller.ViewWidth, controller.ViewHeight, buffer)
        MyBase.LoadContent()
    End Sub

    Protected Overrides Sub Update(gameTime As GameTime)
        CheckForQuit()
        UpdateWindowTitle()
        UpdateWindowSize()
        PlayQueuedSfx()
        HandleInput()
        controller.Update(gameTime.ElapsedGameTime)
        renderer.Render(controller.Display, pixelBuffer)
        texture.SetData(buffer)
        MyBase.Update(gameTime)
    End Sub

    Private Sub HandleInput()
        For Each cmd In inputManager.GetCommands(Keyboard.GetState(), GamePad.GetState(PlayerIndex.One))
            controller.Command.WriteCommand(cmd)
        Next
    End Sub

    Private Sub PlayQueuedSfx()
        If Not controller.IsMuted Then
            For Each sfx In controller.QueuedSfx
                sfxManager.Play(sfx, controller.Volume)
            Next
        End If
    End Sub

    Private Sub CheckForQuit()
        If controller.IsQuitRequested Then
            Me.Exit()
        End If
    End Sub

    Private Sub UpdateWindowSize()
        If graphicsDeviceManager.PreferredBackBufferWidth <> controller.FrameWidth OrElse graphicsDeviceManager.PreferredBackBufferHeight <> controller.FrameHeight OrElse graphicsDeviceManager.IsFullScreen <> controller.IsFullScreen Then
            graphicsDeviceManager.PreferredBackBufferWidth = controller.FrameWidth
            graphicsDeviceManager.PreferredBackBufferHeight = controller.FrameHeight
            graphicsDeviceManager.ApplyChanges()
        End If
    End Sub

    Private Sub UpdateWindowTitle()
        If Window.Title <> controller.WindowTitle Then
            Window.Title = controller.WindowTitle
        End If
    End Sub

    Protected Overrides Sub Draw(gameTime As GameTime)
        graphicsDeviceManager.GraphicsDevice.Clear(Color.Black)
        spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        spriteBatch.Draw(
            texture,
            New Rectangle(
                0,
                0,
                controller.FrameWidth,
                controller.FrameHeight),
            Nothing,
            Color.White)
        spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
End Class
