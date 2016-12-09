Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data

'Comments comming soon

Public Class DatabaseObjectList(Of T As {DatabaseObject, New})
    Implements IList(Of T), ICollection(Of T), IEnumerable(Of T), IList, ICollection, IEnumerable, IDisposable

    Private m_Datatable As DataTable

#Region "IList(Of T)"
    Private Property Item(index As Integer) As T Implements IList(Of T).Item
        Get
            Return New T With {.Row = m_Datatable.Rows(index)}
        End Get
        Set(value As T)
            Throw New NotImplementedException()
        End Set
    End Property

    Private Sub Insert(index As Integer, item As T) Implements IList(Of T).Insert
        m_Datatable.Rows.InsertAt(item.Row, index)
    End Sub

    Private Sub RemoveAt(index As Integer) Implements IList(Of T).RemoveAt
        m_Datatable.Rows(index).Delete()
    End Sub

    Private Function IndexOf(item As T) As Integer Implements IList(Of T).IndexOf
        Throw New NotImplementedException()
    End Function
#End Region

#Region "ICollection(Of T)"
    Private ReadOnly Property Count As Integer Implements ICollection(Of T).Count
        Get
            Return m_Datatable.Rows.Count ' ToDo Substract Deletete Rows
        End Get
    End Property

    Private ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of T).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Private Sub Add(item As T) Implements ICollection(Of T).Add
        m_Datatable.Rows.Add(item.Row)
    End Sub

    Private Sub Clear() Implements ICollection(Of T).Clear
        m_Datatable.Rows.Clear()
    End Sub

    Private Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
        For i As Integer = 0 To m_Datatable.Rows.Count - 1

        Next
    End Sub

    Private Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
        Throw New NotImplementedException()
    End Function

    Private Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
        Throw New NotImplementedException()
    End Function

    Private Sub CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo
        Throw New NotImplementedException()
    End Sub
#End Region

#Region "IEnumerable(Of T)"
    Private Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Throw New NotImplementedException()
    End Function
#End Region

#Region "IList"
    Private Property IList_Item(index As Integer) As Object Implements IList.Item
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Object)
            Throw New NotImplementedException()
        End Set
    End Property

    Private ReadOnly Property IList_IsReadOnly As Boolean Implements IList.IsReadOnly
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Private ReadOnly Property IsFixedSize As Boolean Implements IList.IsFixedSize
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Private Function Add(value As Object) As Integer Implements IList.Add
        Throw New NotImplementedException()
    End Function

    Private Function Contains(value As Object) As Boolean Implements IList.Contains
        Throw New NotImplementedException()
    End Function

    Private Sub IList_Clear() Implements IList.Clear
        Throw New NotImplementedException()
    End Sub

    Private Function IndexOf(value As Object) As Integer Implements IList.IndexOf
        Throw New NotImplementedException()
    End Function

    Private Sub Insert(index As Integer, value As Object) Implements IList.Insert
        Throw New NotImplementedException()
    End Sub

    Private Sub Remove(value As Object) Implements IList.Remove
        Throw New NotImplementedException()
    End Sub

    Private Sub IList_RemoveAt(index As Integer) Implements IList.RemoveAt
        Throw New NotImplementedException()
    End Sub
#End Region

#Region "ICollection"
    Private ReadOnly Property ICollection_Count As Integer Implements ICollection.Count
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Private ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Private ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
        Get
            Throw New NotImplementedException()
        End Get
    End Property
#End Region

#Region "IEnumerable"
    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Throw New NotImplementedException()
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Dient zur Erkennung redundanter Aufrufe.

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
            End If

            ' TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalize() weiter unten überschreiben.
            ' TODO: große Felder auf Null setzen.
        End If
        disposedValue = True
    End Sub

    ' TODO: Finalize() nur überschreiben, wenn Dispose(disposing As Boolean) weiter oben Code zur Bereinigung nicht verwalteter Ressourcen enthält.
    'Protected Overrides Sub Finalize()
    '    ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.
        Dispose(True)
        ' TODO: Auskommentierung der folgenden Zeile aufheben, wenn Finalize() oben überschrieben wird.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

