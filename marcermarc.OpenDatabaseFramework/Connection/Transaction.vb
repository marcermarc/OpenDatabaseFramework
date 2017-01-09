Imports System
Imports System.Collections.Generic
Imports System.Data

'Comments comming soon

Public MustInherit Class Transaction
    Implements IDisposable

    Private m_lock As Object 'For Synclock

    Protected Friend m_Transaction As IDbTransaction

#Region "Public"
    Function Execute(statement As SqlStatement) As DataTable
        Using command As Command = NewCommand()
            Return command.Execute(statement)
        End Using
    End Function

    Function Execute(statements As IList(Of SqlStatement)) As DataSet
        Dim result As New DataSet
        Dim done As Integer = 0

        Using command As Command = NewCommand()
            done = command.Execute(statements, result)
        End Using

        For i As Integer = 0 To done - 1
            statements.RemoveAt(i)
        Next

        While statements.Count > 0
            Using ds As New DataSet
                Using command As Command = NewCommand()
                    done = command.Execute(statements, ds)
                End Using

                For Each dt As DataTable In ds.Tables
                    result.Tables.Add(dt.Copy)
                Next
            End Using

            For i As Integer = 0 To done - 1
                statements.RemoveAt(i)
            Next
        End While

        Return result
    End Function

    Function ExecuteWithoutResult(statement As SqlStatement) As Integer
        Using command As Command = NewCommand()
            Return command.ExecuteWithoutResult(statement)
        End Using
    End Function

    Function ExecuteWithoutResult(statements As IList(Of SqlStatement)) As Integer
        Dim result As Integer
        Dim done As Integer
        Using command As Command = NewCommand()
            done = command.ExecuteWithoutResult(statements, result)
        End Using

        For i As Integer = 0 To done - 1
            statements.RemoveAt(i)
        Next

        While statements.Count > 0
            Dim nextResult As Integer
            Using command As Command = NewCommand()
                done = command.ExecuteWithoutResult(statements, nextResult)
            End Using

            For i As Integer = 0 To done - 1
                statements.RemoveAt(i)
            Next

            result += nextResult
        End While

        Return result
    End Function
#End Region

#Region "Private"

#End Region

#Region "MustOverride"
    Protected MustOverride Sub InitTransaction()
    Protected MustOverride Function NewCommand() As Command
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                m_Transaction.Dispose()
                m_Transaction = Nothing
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
