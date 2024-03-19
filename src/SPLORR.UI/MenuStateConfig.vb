Public Class MenuStateConfig(Of TPixel, TCommand, TAssets)
    Public Property BackgroundHue As TPixel
    Public Property HeaderHue As TPixel
    Public Property HiliteHue As TPixel
    Public Property FooterHue As TPixel
    Public Property GetFont As Func(Of TAssets, Font)
    Public Property FooterText As String
    Public Property NextItemCommand As Func(Of TCommand, Boolean)
    Public Property PreviousItemCommand As Func(Of TCommand, Boolean)
    Public Property ChooseCommand As Func(Of TCommand, Boolean)
    Public Property CancelCommand As Func(Of TCommand, Boolean)
End Class
