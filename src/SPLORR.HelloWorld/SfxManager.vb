Imports Microsoft.Xna.Framework.Audio

Public Class SfxManager
    Implements ISfxManager(Of Sfx)
    Private ReadOnly fileNames As IReadOnlyDictionary(Of Sfx, String)
    Private ReadOnly soundEffects As New Dictionary(Of Sfx, SoundEffect)
    Sub New()
        fileNames = New Dictionary(Of Sfx, String) From
            {
                {Sfx.Ok, "Content/WooHoo.wav"}
            }
    End Sub

    Public Sub Play(sfx As Sfx, volume As Single) Implements ISfxManager(Of Sfx).Play
        If fileNames.ContainsKey(sfx) Then
            If Not soundEffects.ContainsKey(sfx) Then
                soundEffects(sfx) = SoundEffect.FromFile(fileNames(sfx))
            End If
            soundEffects(sfx).Play(volume, 0.0F, 0.0F)
        End If
    End Sub
End Class
