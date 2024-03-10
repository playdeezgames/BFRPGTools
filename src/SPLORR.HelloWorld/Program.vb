Module Program
    Sub Main(args As String())
        Using host As New Host(Of Hue, Command, Sfx)(
            New GameController(New HostConfig()),
            New Renderer(),
            New InputManager(),
            New SfxManager())
            host.Run()
        End Using
    End Sub
End Module
