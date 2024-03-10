Public Class HostConfig
    Implements IHostConfig

    Sub New()
        ViewScale = 2
        IsFullScreen = False
        IsMuted = False
        Volume = 1
    End Sub

    Public ReadOnly Property ViewWidth As Integer Implements IHostConfig.ViewWidth
        Get
            Return 640
        End Get
    End Property

    Public ReadOnly Property ViewHeight As Integer Implements IHostConfig.ViewHeight
        Get
            Return 360
        End Get
    End Property

    Public Property ViewScale As Integer Implements IHostConfig.ViewScale

    Public Property IsFullScreen As Boolean Implements IHostConfig.IsFullScreen

    Public Property Volume As Single Implements IHostConfig.Volume

    Public Property IsMuted As Boolean Implements IHostConfig.IsMuted
End Class
