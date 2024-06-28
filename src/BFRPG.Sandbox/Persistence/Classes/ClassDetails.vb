Friend Class ClassDetails
    Public Sub New(classId As Object, className As Object)
        Me.ClassId = CInt(classId)
        Me.ClassName = CStr(className)
    End Sub

    Public ReadOnly Property ClassId As Integer
    Public ReadOnly Property ClassName As String
    Public ReadOnly Property UniqueName As String
        Get
            Return $"{ClassName}(Id={ClassId})"
        End Get
    End Property
End Class
