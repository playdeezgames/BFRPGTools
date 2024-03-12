Imports System.Text.Json
Imports System.IO
Public Class HWAssets
    Private _font As Font = Nothing
    ReadOnly Property Font As Font
        Get
            If _font Is Nothing Then
                _font = New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("Content/CyFont4x6.json")))
            End If
            Return _font
        End Get
    End Property
End Class
