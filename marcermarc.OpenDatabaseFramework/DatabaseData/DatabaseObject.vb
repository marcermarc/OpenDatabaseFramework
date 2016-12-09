Imports System.Data

'Comments comming soon

Public Class DatabaseObject
    Private m_Row As DataRow
    Friend Property Row As DataRow
        Get
            Return m_Row
        End Get
        Set(value As DataRow)
            m_Row = value
        End Set
    End Property

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj.GetType Is Me.GetType Then
            Dim databaseObject As DatabaseObject = obj
            Return m_Row.Equals(databaseObject.Row)
        End If
        Return False
    End Function
End Class