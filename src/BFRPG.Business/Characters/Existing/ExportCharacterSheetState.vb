Imports System.IO
Imports System.Text
Imports System.Web

Friend Class ExportCharacterSheetState
    Inherits BaseState

    Private ReadOnly characterId As Integer

    Public Sub New(data As IDataContext, ui As IUIContext, endState As IState, characterId As Integer)
        MyBase.New(data, ui, endState)
        Me.characterId = characterId
    End Sub

    Private Function TD(
                       text As String,
                       Optional colSpan As Integer = 1,
                       Optional rowSpan As Integer = 1,
                       Optional htmlEncode As Boolean = True,
                       Optional header As Boolean = False) As String
        Return $"<{If(header, "th", "td")} colspan=""{colSpan}"" rowspan=""{rowSpan}"">{If(htmlEncode, HttpUtility.HtmlEncode(text), text)}</{If(header, "th", "td")}>"
    End Function
    Private Function TR(ParamArray lines As String()) As String
        Return $"<tr>{String.Join(String.Empty, lines)}</tr>"
    End Function
    Private Function TABLE(border As Boolean, ParamArray lines As String()) As String
        Return $"<table{If(border," border=""border""","")}>{String.Join(String.Empty, lines)}</table>"
    End Function
    Private Function BODY(ParamArray lines As String()) As String
        Return $"<body>{String.Join(String.Empty, lines)}</body>"
    End Function
    Private Function HTML(ParamArray lines As String()) As String
        Return $"<html>{String.Join(String.Empty, lines)}</html>"
    End Function
    Private Function TITLE(text As String, Optional htmlEncode As Boolean = True) As String
        Return $"<title>{If(htmlEncode, HttpUtility.HtmlEncode(text), text)}</title>"
    End Function
    Private Function HEAD(ParamArray lines As String()) As String
        Return $"<head>{String.Join(String.Empty, lines)}</head>"
    End Function

    Public Overrides Function Run() As IState
        Dim details = data.Characters.ReadDetails(characterId)
        Dim generatedOn = DateTimeOffset.Now
        Dim filename = $"{details.CharacterName} - {details.RaceName} - {details.ClassName} - {details.Level} - {generatedOn:yyyyMMddHHmmss}.html"
        Dim builder As New StringBuilder
        Dim abilityScoresTable As New List(Of String) From
                {
                    TR(
                        TD("Ability", header:=True),
                        TD("Score", header:=True),
                        TD("Modifier", header:=True))
                }
        abilityScoresTable.AddRange(
                data.Characters.Abilities(characterId).ReadAllDetailsForCharacter().Select(Function(abilityDetail) TR(
                        TD($"{abilityDetail.AbilityAbbreviation}"),
                        TD($"{abilityDetail.AbilityScore}"),
                        TD($"{abilityDetail.Modifier}"))))
        File.WriteAllText(
            filename,
            HTML(
                HEAD(
                    TITLE($"{details.CharacterName} - {details.RaceName} - {details.ClassName} - {details.Level}")),
                BODY(
                    TABLE(
                        True,
                        TR(
                            TD($"Name: {details.CharacterName}", colSpan:=2),
                            TD($"Player: {details.PlayerName}")),
                        TR(
                            TD($"Race: {details.RaceName}"),
                            TD($"XP: {details.ExperiencePoints}"),
                            TD($"Desc: {details.CharacterDescription}", rowSpan:=2)),
                        TR(
                            TD($"Class: {details.ClassName}"),
                            TD($"Level: {details.Level}")),
                        TR(
                            TD(
                                TABLE(
                                    True,
                                    abilityScoresTable.ToArray), htmlEncode:=False),
                            TD(
                                TABLE(
                                    False,
                                    TR(TD($"AC: TODO")),
                                    TR(TD($"HP: {details.HitPoints}")),
                                    TR(TD($"AB: {details.AttackBonus}"))), htmlEncode:=False),
                            TD(
                                TABLE(
                                    False,
                                    TR(
                                        TD($"Movement: TODO")),
                                    TR(
                                        TD($"Money: {details.Money}"))), htmlEncode:=False)),
                        TR(
                            TD(
                                TABLE(
                                    False,
                                    TR(
                                        TD("Spells/Abilities:", header:=True))), htmlEncode:=False, rowSpan:=2),
                            TD(
                                TABLE(
                                    False,
                                    TR(
                                        TD("Saving Throws:", header:=True))), colSpan:=2, htmlEncode:=False)),
                        TR(
                            TD(
                                TABLE(
                                    False,
                                    TR(
                                        TD("Weapon", header:=True),
                                        TD("Damage", header:=True),
                                        TD("Range", header:=True))), htmlEncode:=False, colSpan:=2)),
                        TR(
                            TD(
                                TABLE(
                                    False,
                                    TR(TD("Equipment:", header:=True))), htmlEncode:=False),
                            TD(
                                TABLE(
                                    False,
                                    TR(TD("Notes:", header:=True))), htmlEncode:=False, colSpan:=2))))))
        Dim p As New Process() With
            {
                .StartInfo = New ProcessStartInfo(filename) With
                {
                    .UseShellExecute = True
                }
            }
        p.Start()
        Return endState
    End Function
End Class
