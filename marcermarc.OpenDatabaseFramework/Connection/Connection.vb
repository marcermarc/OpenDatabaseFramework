Imports System
Imports System.Collections.Generic
Imports System.Data

'Comments comming soon

Public MustInherit Class Connection
    Implements IDisposable

    Private m_lock As Object 'For Synclock

    Protected Friend m_Connection As IDbConnection

    Protected m_ConnectionStirng As String

    Private m_DefaultTransaction As IUnique(Of Transaction)

    Private ReadOnly m_ThreadUniqueCommands As Boolean

    Protected Sub New(threadUniqueTransactions As Boolean, threadUniqueCommands As Boolean)
        If threadUniqueTransactions Then
            m_DefaultTransaction = New ThreadUnique(Of Transaction)
        Else
            m_DefaultTransaction = New Unique(Of Transaction)
        End If
        m_ThreadUniqueCommands = threadUniqueCommands
    End Sub

#Region "Public"
    ''' <summary>
    ''' Gives the default transaction back
    ''' </summary>
    ''' <returns>the default transaction</returns>
    ''' <remarks>The default transaction is used by the <see cref="Execute(IList(Of SqlStatement)"/>, <see cref="Execute(SqlStatement)"/>, <see cref="ExecuteWithoutResult(IList(Of SqlStatement))"/> and <see cref="ExecuteWithoutResult(SqlStatement)"/>.</remarks>
    ReadOnly Property DefaultTransaction As Transaction
        Get
            If m_DefaultTransaction.Value Is Nothing Then
                m_DefaultTransaction.Value = NewTransaction()
            End If

            Return m_DefaultTransaction.Value
        End Get
    End Property

    ''' <summary>
    ''' Executes the statement in the default transaction.
    ''' </summary>
    Function Execute(statement As SqlStatement) As DataTable
        Return DefaultTransaction.Execute(statement)
    End Function

    ''' <summary>
    ''' Executes the statements in the default transaction.
    ''' </summary>
    Function Execute(statements As IList(Of SqlStatement)) As DataSet
        Return m_DefaultTransaction.Value.Execute(statements)
    End Function

    ''' <summary>
    ''' Executes the statement in the default transaction.
    ''' </summary>
    Function ExecuteWithoutResult(statement As SqlStatement) As Integer
        Return DefaultTransaction.ExecuteWithoutResult(statement)
    End Function

    ''' <summary>
    ''' Executes the statements in the default transaction.
    ''' </summary>
    Function ExecuteWithoutResult(statements As IList(Of SqlStatement)) As Integer
        Return m_DefaultTransaction.Value.ExecuteWithoutResult(statements)
    End Function

    ''' <summary>
    ''' Returns the name of the Database
    ''' </summary>
    Public Overrides Function ToString() As String
        Return m_Connection.Database
    End Function
#End Region

#Region "MustOverride"
    Protected MustOverride Sub InitConnection()
    Protected MustOverride Function NewTransaction() As Transaction
    Protected MustOverride Function NewCommand() As Command
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                m_DefaultTransaction.Dispose()
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
