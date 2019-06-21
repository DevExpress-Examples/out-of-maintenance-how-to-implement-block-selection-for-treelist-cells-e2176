Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraTreeList
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.Nodes

Namespace BlockSelection
	Friend Class BlockSelector
		Private fList As TreeList
		Private fBlock As Block

		Public Sub New(ByVal list As TreeList)
			If list Is Nothing Then
				Throw New Exception("A valid TreeList instance must be passed to the constructor")
			End If

			fList = list
			fBlock = New Block()

			AddHandler fList.FocusedColumnChanged, AddressOf fList_FocusedColumnChanged
			AddHandler fList.FocusedNodeChanged, AddressOf fList_FocusedNodeChanged
			AddHandler fList.MouseMove, AddressOf fList_MouseMove
			AddHandler fList.MouseDown, AddressOf fList_MouseDown
			AddHandler fList.NodeCellStyle, AddressOf fList_NodeCellStyle
		End Sub

		Public ReadOnly Property Block() As Block
			Get
				Return fBlock
			End Get
		End Property

		Public ReadOnly Property List() As TreeList
			Get
				Return fList
			End Get
		End Property

		Private Sub fList_NodeCellStyle(ByVal sender As Object, ByVal e As GetCustomNodeCellStyleEventArgs)
			If fBlock.Contains(e.Column.VisibleIndex, e.Node.Id) Then
				e.Appearance.BackColor = SystemColors.Highlight
			End If
		End Sub

		Private Delegate Sub SelectEntireNodeDlg(ByVal node As TreeListNode)

		Private Sub SelectEntireNode(ByVal node As TreeListNode)
			fBlock.X1 = 0
			fBlock.X2 = fList.VisibleColumns.Count - 1
			fBlock.Y2 = node.Id
			If fBlock.Modified AndAlso Not fBlock.IsEmpty() Then
				InvalidateBlock()
			End If
		End Sub

		Private Sub fList_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
			If e.Button = MouseButtons.Left Then
				Dim hInfo As TreeListHitInfo = fList.CalcHitInfo(e.Location)
				If hInfo.HitInfoType = HitInfoType.RowIndicator Then
					fList.BeginInvoke(New SelectEntireNodeDlg(AddressOf SelectEntireNode), hInfo.Node)
				End If
			End If
		End Sub

		Private Sub fList_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
			If e.Button = MouseButtons.Left Then
				Dim hInfo As TreeListHitInfo = fList.CalcHitInfo(e.Location)
				If hInfo.HitInfoType = HitInfoType.Cell Then
					fBlock.X2 = hInfo.Column.VisibleIndex
				End If
				If hInfo.HitInfoType = HitInfoType.Cell OrElse hInfo.HitInfoType = HitInfoType.RowIndicator Then
					fBlock.Y2 = hInfo.Node.Id
					If fBlock.Modified AndAlso Not fBlock.IsEmpty() Then
						InvalidateBlock()
					End If
				End If
			End If
		End Sub

		Public Sub InvalidateBlock()
			fList.LayoutChanged()
		End Sub

		Private Sub fList_FocusedNodeChanged(ByVal sender As Object, ByVal e As FocusedNodeChangedEventArgs)
			If fList.FocusedColumn IsNot Nothing Then
				UpdateBlock()
			End If
		End Sub

		Private Sub UpdateBlock()
			If Control.ModifierKeys = Keys.Shift Then
				fBlock.X2 = fList.FocusedColumn.VisibleIndex
				fBlock.Y2 = fList.FocusedNode.Id
			Else
				fBlock.X1 = fList.FocusedColumn.VisibleIndex
				fBlock.Y1 = fList.FocusedNode.Id
				fBlock.X2 = fBlock.X1
				fBlock.Y2 = fBlock.Y1
			End If
			If fBlock.Modified Then
				InvalidateBlock()
			End If
		End Sub

		Private Sub fList_FocusedColumnChanged(ByVal sender As Object, ByVal e As FocusedColumnChangedEventArgs)
			UpdateBlock()
		End Sub

		Public Function GetSelectedValues() As String
			Dim result As String = String.Empty
			If Not fBlock.IsEmpty() Then
				Dim operation As New SelectionOperation(fBlock)
				fList.NodesIterator.DoOperation(operation)
				result = operation.Result
			End If
			Return result
		End Function
	End Class

	Public Class Block
		Private FX1 As Integer
		Private FX2 As Integer
		Private FY1 As Integer
		Private FY2 As Integer
		Private FModified As Boolean

		Public Function Between(ByVal a As Integer, ByVal b As Integer, ByVal c As Integer) As Boolean
			If a > b Then
				Dim temp As Integer = a
				a = b
				b = temp
			End If
			Return (a <= c) AndAlso (c <= b)
		End Function

		Public Function Contains(ByVal x As Integer, ByVal y As Integer) As Boolean
			Return Between(X1, X2, x) AndAlso Between(Y1, Y2, y)
		End Function
		Public Function IsEmpty() As Boolean
			Return (X1 = X2 AndAlso Y1 = Y2)
		End Function

		#Region "Coordinates"
		Public Property X1() As Integer
			Get
				Return FX1
			End Get
			Set(ByVal value As Integer)
				If FX1 <> value Then
					FX1 = value
					FModified = True
				End If
			End Set
		End Property
		Public Property X2() As Integer
			Get
				Return FX2
			End Get
			Set(ByVal value As Integer)
				If FX2 <> value Then
					FX2 = value
					FModified = True
				End If
			End Set
		End Property
		Public Property Y1() As Integer
			Get
				Return FY1
			End Get
			Set(ByVal value As Integer)
				If FY1 <> value Then
					FY1 = value
					FModified = True
				End If
			End Set
		End Property
		Public Property Y2() As Integer
			Get
				Return FY2
			End Get
			Set(ByVal value As Integer)
				If FY2 <> value Then
					FY2 = value
					FModified = True
				End If
			End Set
		End Property
		#End Region

		Public ReadOnly Property Modified() As Boolean
			Get
				Return FModified
			End Get
		End Property
	End Class

	Public Class SelectionOperation
		Inherits TreeListOperation

		Private block As Block
'INSTANT VB NOTE: The field result was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private result_Renamed As String = String.Empty
		Private Const CellDelimeter As String = vbTab
		Private Const LineDelimeter As String = vbCrLf

		Public Sub New(ByVal block As Block)
			Me.block = block
		End Sub

		Public ReadOnly Property Result() As String
			Get
				Return result_Renamed
			End Get
		End Property

		Public Overrides Sub Execute(ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode)
			If Not block.Between(block.Y1, block.Y2, node.Id) Then
				Return
			End If
			For Each column As TreeListColumn In node.TreeList.Columns
				If block.Contains(column.VisibleIndex, node.Id) Then
					result_Renamed &= node.GetDisplayText(column)
					result_Renamed &= CellDelimeter
				End If
			Next column
			result_Renamed &= LineDelimeter
		End Sub
	End Class

End Namespace
