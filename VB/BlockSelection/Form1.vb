Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList

Namespace BlockSelection
	Partial Public Class Form1
		Inherits Form
		Private selector As BlockSelector
		Public Sub New()
			InitializeComponent()
			selector = New BlockSelector(treeList1)
		End Sub

		Private Sub treeList1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles treeList1.KeyDown
			Dim list As TreeList = CType(sender, TreeList)
			If e.Control AndAlso (e.KeyCode = Keys.C OrElse e.KeyCode = Keys.Insert) Then
				If selector IsNot Nothing AndAlso selector.List Is list AndAlso (Not selector.Block.IsEmpty()) Then
					Clipboard.SetDataObject(selector.GetSelectedValues())
					e.Handled = True
				End If
			End If
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'treeListDataBaseDataSet.Table1' table. You can move, or remove it, as needed.
			Me.table1TableAdapter1.Fill(Me.treeListDataBaseDataSet.Table1)

		End Sub
	End Class
End Namespace
