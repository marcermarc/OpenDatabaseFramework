Imports System.Collections.Generic
Imports System.Data
Imports System.Text

'Comments comming soon

Public MustInherit Class Command
    Private m_lock As Object 'For Synclock

    Protected Friend m_Command As IDbCommand

    Property Timeout As Integer
        Get
            Return m_Command.CommandTimeout
        End Get
        Set(value As Integer)
            m_Command.CommandTimeout = value
        End Set
    End Property

#Region "Public"
    Public Function Execute(statement As SqlStatement) As DataTable
        Using ds As DataSet = Execute({statement}) 'ToDo Test, maybe tables are disposed too.
            Return ds.Tables(0)
        End Using
    End Function


#End Region

#Region "Private"
    Private Function Execute(statements As IList(Of SqlStatement)) As DataSet
        Dim ds As New DataSet
        Execute(statements, ds)
        Return ds
    End Function

    Private Sub Execute(statements As IList(Of SqlStatement), ByRef ds As DataSet)
        Dim sb As New StringBuilder

        For i As Integer = 0 To statements.Count - 1
            For Each item As KeyValuePair(Of String, Object) In statements(i).Items
                Dim key As String = "@" & i & item.Key.TrimStart("@"c)
                Dim value As Object = EditVariable(item.Value)
                statements(i).Statement = statements(i).Statement.Replace(item.Key, key)

                Dim param As IDbDataParameter = m_Command.CreateParameter
                param.ParameterName = key
                param.Value = value
                m_Command.Parameters.Add(param)
            Next

            sb.Append(statements(i).Statement.Trim(";"c) & ";")
        Next

        m_Command.Prepare()

        Dim adapter As IDbDataAdapter = CreateDataAdapter()
        adapter.Fill(ds)
    End Sub

#End Region

#Region "MustOverride"
    Protected MustOverride Sub InitCommand()
    Protected MustOverride Function EditVariable(value As Object) As Object
    Protected MustOverride Function CreateDataAdapter() As IDbDataAdapter
#End Region
End Class
