Public Class Model
    Public X As Integer
    Public Y As Integer
    Public Hue As Hue

    Public Sub New(config As IHostConfig)
        X = config.ViewWidth \ 2
        Y = config.ViewHeight \ 2
        Hue = Hue.White
    End Sub
End Class
