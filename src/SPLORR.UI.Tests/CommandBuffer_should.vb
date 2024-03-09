Public Class CommandBuffer_should
    <Fact>
    Sub initially_have_no_command_pending()
        Dim subject As ICommandBuffer(Of String) = New CommandBuffer(Of String)
        subject.HasCommand.ShouldBe(False)
    End Sub
End Class
