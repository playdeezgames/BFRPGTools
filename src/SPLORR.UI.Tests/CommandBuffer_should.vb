Imports System.Windows.Input

Public Class CommandBuffer_should
    <Fact>
    Sub initially_have_no_command_pending()
        Dim subject As ICommandBuffer(Of String) = New CommandBuffer(Of String)
        subject.HasCommand.ShouldBe(False)
    End Sub

    <Fact>
    Sub accept_commands_to_add()
        Const command = "command"
        Dim subject As ICommandBuffer(Of String) = New CommandBuffer(Of String)
        subject.WriteCommand(command)
        subject.HasCommand.ShouldBe(True)
        Dim actual = subject.ReadCommand()
        actual.ShouldBe(command)
    End Sub

    <Fact>
    Sub throws_exception_when_reading_from_empty_queue()
        Dim subject As ICommandBuffer(Of String) = New CommandBuffer(Of String)
        Should.Throw(Of InvalidOperationException)(Sub()
                                                       subject.ReadCommand()
                                                   End Sub)
    End Sub
End Class
