Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Text

'Comments comming soon

Public MustInherit Class Command
    Implements IDisposable

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
        Using ds As New DataSet  'ToDo Test, maybe tables are disposed too.
            Execute({statement}, ds)

            Return ds.Tables(0)
        End Using
    End Function

    Public Function ExecuteWithoutResult(statement As SqlStatement) As Integer
        Dim result As Integer
        ExecuteWithoutResult({statement}, result)
        Return result
    End Function
#End Region

#Region "Friend"
    Friend Function Execute(statements As IList(Of SqlStatement), ByRef ds As DataSet) As Integer
        Dim done As Integer = PrepareCommand(statements)

        Dim adapter As IDbDataAdapter = CreateDataAdapter()
        adapter.Fill(ds)

        Return done
    End Function

    Friend Function ExecuteWithoutResult(statements As IList(Of SqlStatement), ByRef result As Integer) As Integer
        Dim done As Integer = PrepareCommand(statements)

        result = m_Command.ExecuteNonQuery

        Return done
    End Function
#End Region

#Region "Private"
    Private Function PrepareCommand(statements As IList(Of SqlStatement)) As Integer
        Dim sb As New StringBuilder

        Dim i As Integer
        For i = 0 To statements.Count - 1
            Dim params As New List(Of IDbDataParameter)

            For Each item As KeyValuePair(Of String, Object) In statements(i).Items
                Dim param As IDbDataParameter = m_Command.CreateParameter
                param.ParameterName = "@" & i & item.Key.TrimStart("@"c)
                param.Value = EditVariable(item.Value)
                params.Add(param)

                statements(i).Statement = statements(i).Statement.Replace(item.Key, param.ParameterName)
            Next

            If sb.Length + statements(i).Statement.Length < MaxCommandLength AndAlso m_Command.Parameters.Count + params.Count < MaxParameterCount Then
                sb.Append(statements(i).Statement.Trim(";"c) & ";")
                For Each param As IDbDataParameter In params
                    m_Command.Parameters.Add(param)
                Next
            Else
                Exit For
            End If
        Next

        m_Command.CommandText = sb.ToString

        m_Command.Prepare()

        Return i + 1
    End Function
#End Region

#Region "MustOverride"
    Protected MustOverride Sub InitCommand()
    Protected MustOverride Function EditVariable(value As Object) As Object
    Protected MustOverride Function CreateDataAdapter() As IDbDataAdapter
    Protected MustOverride ReadOnly Property MaxCommandLength As Integer
    Protected MustOverride ReadOnly Property MaxParameterCount As Integer
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                m_Command.Dispose()
                m_Command = Nothing
            End If
        End If
        disposedValue = True
    End Sub

    'Protected Overrides Sub Finalize()
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
