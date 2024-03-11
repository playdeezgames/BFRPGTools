Imports Microsoft.Xna.Framework.Input

Module Program
    Sub Main(args As String())
        Using host As New Host(Of Hue, Command, Sfx, Model.Model)(
            New GameController(New HostConfig()),
            New Renderer(),
            New BaseInputManager(Of Command)(New Dictionary(Of Command, Func(Of KeyboardState, GamePadState, Boolean)) From
            {
                {Command.A, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Space) OrElse keyboard.IsKeyDown(Keys.Enter) OrElse gamePad.IsButtonDown(Buttons.A)},
                {Command.B, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Escape) OrElse keyboard.IsKeyDown(Keys.NumPad0) OrElse gamePad.IsButtonDown(Buttons.B)},
                {Command.Up, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Up) OrElse keyboard.IsKeyDown(Keys.NumPad8) OrElse gamePad.DPad.Up = ButtonState.Pressed},
                {Command.Down, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Down) OrElse keyboard.IsKeyDown(Keys.NumPad2) OrElse gamePad.DPad.Down = ButtonState.Pressed},
                {Command.Left, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Left) OrElse keyboard.IsKeyDown(Keys.NumPad4) OrElse gamePad.DPad.Left = ButtonState.Pressed},
                {Command.Right, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Right) OrElse keyboard.IsKeyDown(Keys.NumPad6) OrElse gamePad.DPad.Right = ButtonState.Pressed},
                {Command.[Select], Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Tab) OrElse gamePad.IsButtonDown(Buttons.Back)},
                {Command.Start, Function(keyboard, gamePad) keyboard.IsKeyDown(Keys.Enter) OrElse gamePad.IsButtonDown(Buttons.Start)}
            }),
            New SfxManager())
            host.Run()
        End Using
    End Sub
End Module
