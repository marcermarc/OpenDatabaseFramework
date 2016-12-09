'Imports System
'Imports System.Data
'Namespace BaseObjects

'    Private MustInherit Class DatabaseObject

'        ''' <summary>
'        ''' connection object should be used by methods
'        ''' </summary>
'        Protected ReadOnly connection As Connection

'        ''' <summary>
'        ''' identifier for collections
'        ''' </summary>
'        ''' <returns>GUID</returns>
'        Public ReadOnly Property Object_ID As Guid
'            Get
'                Return myID
'            End Get
'        End Property
'        Private myID As Guid


'        Public Sub New()
'            myID = Guid.NewGuid
'        End Sub

'    End Class

'End Namespace