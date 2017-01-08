Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data

'Comments comming soon

Public Class DatabaseObjectList(Of T As {DatabaseObject, New})
    Implements IList(Of T), ICollection(Of T), IEnumerable(Of T), IList, ICollection, IEnumerable, IDisposable

    Private m_Datatable As DataTable
    Private m_List As List(Of T)
    Private m_version As Integer

    Sub New(in_Datatable As DataTable)
        m_Datatable = in_Datatable
        m_List = New List(Of T)(in_Datatable.Rows.Count)
    End Sub

    Sub DeleteAt(index As Integer)
        'ToDo Remove from list, but forward delete to database so remove not from datatable
        Throw New NotImplementedException
    End Sub

    Sub Delete(item As T)
        'ToDo Remove from list, but forward delete to database so remove not from datatable
        Throw New NotImplementedException
    End Sub

#Region "IList(Of T)"
    Default Property Item(index As Integer) As T Implements IList(Of T).Item
        Get
            If m_List(index) Is Nothing Then
                m_List(index) = New T With {.Row = m_Datatable.Rows(index)}
            End If

            Return m_List(index)
        End Get
        Set(value As T)
            If value.Row.Equals(Item(index).Row) Then
                Return
            End If
            'ToDo Set old index deletet and create a new row
        End Set
    End Property

    Sub Insert(index As Integer, item As T) Implements IList(Of T).Insert
        m_Datatable.Rows.InsertAt(item.Row, index)
        m_List.Insert(index, item)
    End Sub

    Sub RemoveAt(index As Integer) Implements IList(Of T).RemoveAt, IList.RemoveAt
        m_Datatable.Rows.RemoveAt(index)
        m_List.RemoveAt(index)
    End Sub

    Private Function IndexOf(item As T) As Integer Implements IList(Of T).IndexOf
        Throw New Exceptions.NotSupportedException
    End Function
#End Region

#Region "ICollection(Of T)"
    ReadOnly Property Count As Integer Implements ICollection(Of T).Count, ICollection.Count
        Get
            Return m_List.Count
        End Get
    End Property

    Private ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of T).IsReadOnly, IList.IsReadOnly
        Get
            Return False
        End Get
    End Property

    Sub Add(item As T) Implements ICollection(Of T).Add
        m_Datatable.Rows.Add(item.Row)
        m_List.Add(item)
    End Sub

    Private Sub Clear() Implements ICollection(Of T).Clear, IList.Clear
        'Private because it is not usefull
        m_Datatable.Rows.Clear()
        m_List.Clear()
    End Sub

    Private Sub CopyTo(array() As T, arrayIndex As Integer) Implements ICollection(Of T).CopyTo
        For i As Integer = 0 To m_List.Count - 1
            array(i + arrayIndex) = Item(i)
        Next
    End Sub

    Function Contains(item As T) As Boolean Implements ICollection(Of T).Contains
        Dim i As Integer = m_Datatable.Rows.IndexOf(item.Row)
        Return i <> -1
    End Function

    Function Remove(item As T) As Boolean Implements ICollection(Of T).Remove
        Dim i As Integer = m_Datatable.Rows.IndexOf(item.Row)
        If i = -1 Then
            Return False
        Else
            RemoveAt(i)
            Return True
        End If
    End Function

    Private Sub CopyTo(array As Array, index As Integer) Implements ICollection.CopyTo
        For i As Integer = 0 To m_List.Count - 1
            array.SetValue(Item(i), i + index)
        Next
    End Sub
#End Region

#Region "IEnumerable(Of T)"
    Private Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Throw New NotImplementedException() 'ToDO
    End Function
#End Region

#Region "IList"
    Private Property IList_Item(index As Integer) As Object Implements IList.Item
        Get
            Return Item(index)
        End Get
        Set(value As Object)
            Item(index) = CType(value, T)
        End Set
    End Property

    'IList.IsReadOnly -> ICollection(Of T).IsReadOnly

    Private ReadOnly Property IList_IsFixedSize As Boolean Implements IList.IsFixedSize
        Get
            Return False
        End Get
    End Property

    Private Function IList_Add(value As Object) As Integer Implements IList.Add
        Add(CType(value, T))
        Return m_List.Count - 1
    End Function

    Private Function IList_Contains(value As Object) As Boolean Implements IList.Contains
        Return Contains(CType(value, T))
    End Function

    'IList.Clear -> ICollection(Of T).Clear

    Private Function IList_IndexOf(value As Object) As Integer Implements IList.IndexOf
        Return IndexOf(CType(value, T))
    End Function

    Private Sub IList_Insert(index As Integer, value As Object) Implements IList.Insert
        Insert(index, CType(value, T))
    End Sub

    Private Sub IList_Remove(value As Object) Implements IList.Remove
        Remove(CType(value, T))
    End Sub

    'IList.RemoveAt -> IList(Of T).RemoveAt
#End Region

#Region "ICollection"
    'ICollection.Count

    Private ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
        Get
            Dim icol As ICollection = m_List
            Return icol.SyncRoot
        End Get
    End Property

    Private ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
        Get
            Return False
        End Get
    End Property
#End Region

#Region "IEnumerable"
    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Throw New NotImplementedException() 'ToDo
    End Function
#End Region

#Region "Enumerator"
    Public Structure Enumerator
        Implements IEnumerator(Of T), IEnumerator

        Private m_liste As DatabaseObjectList(Of T)
        Private m_index As Integer
        Private m_version As Integer

        Friend Sub New(in_liste As DatabaseObjectList(Of T))
            m_liste = in_liste
            m_index = 0
            m_version = in_liste.m_version
        End Sub

        Public ReadOnly Property Current As T Implements IEnumerator(Of T).Current
            Get
                If m_version <> m_liste.m_version Then
                    Throw New Exceptions.InvalidOperationException("List has been changed.")
                ElseIf m_index = -1 Then
                    Throw New Exceptions.InvalidOperationException("Enumerator is at the end.")
                End If
                Return m_liste(m_index)
            End Get
        End Property

        Private ReadOnly Property IEnumerator_Current As Object Implements IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            m_liste = Nothing
        End Sub

        Public Sub Reset() Implements IEnumerator.Reset
            If m_version <> m_liste.m_version Then
                Throw New Exceptions.InvalidOperationException("List has been changed.")
            End If
            m_index = 0
        End Sub

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            If m_version <> m_liste.m_version Then
                Throw New Exceptions.InvalidOperationException("List has been changed.")
            ElseIf m_index >= m_liste.Count - 1 Then
                m_index = -1
                Return False
            End If
            m_index += 1
            Return True
        End Function
    End Structure
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO
            End If

            ' TODO
            ' TODO
        End If
        disposedValue = True
    End Sub

    ' TODO
    'Protected Overrides Sub Finalize()
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        ' TODO
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class

