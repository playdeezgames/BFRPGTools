Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input

Module Program
    Sub Main(args As String())
        Using host As New Host(Of Hue, Command, Sfx, Model.Model)(
            New GameController(New HostConfig()),
            New PaletteRenderer(Of Hue, Color)(New Dictionary(Of Hue, Color) From
            {
                {Hue.Black, New Color(0, 0, 0)},
                {Hue.Blue, New Color(0, 0, 170)},
                {Hue.Green, New Color(0, 170, 0)},
                {Hue.Cyan, New Color(0, 170, 170)},
                {Hue.Red, New Color(170, 0, 0)},
                {Hue.Magenta, New Color(170, 0, 170)},
                {Hue.Orange, New Color(170, 85, 0)},
                {Hue.LightGray, New Color(170, 170, 170)},
                {Hue.DarkGray, New Color(85, 85, 85)},
                {Hue.LightBlue, New Color(85, 85, 255)},
                {Hue.LightGreen, New Color(85, 255, 85)},
                {Hue.LightCyan, New Color(85, 255, 255)},
                {Hue.LightRed, New Color(255, 85, 85)},
                {Hue.LightMagenta, New Color(255, 85, 255)},
                {Hue.Yellow, New Color(255, 255, 85)},
                {Hue.White, New Color(255, 255, 255)}
            }),
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
