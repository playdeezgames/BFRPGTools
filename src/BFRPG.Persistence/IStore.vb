Public Interface IStore
    Function ReadAll(columns As IEnumerable(Of String), viewName As String) As IEnumerable(Of IReadOnlyDictionary(Of String, Object))
    Sub Delete(viewName As String, forValues As IReadOnlyDictionary(Of String, Object))
End Interface
