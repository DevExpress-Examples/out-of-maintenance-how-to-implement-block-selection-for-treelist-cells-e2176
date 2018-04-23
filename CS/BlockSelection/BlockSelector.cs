using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraTreeList;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;

namespace BlockSelection
{
    class BlockSelector
    {
        private TreeList fList;
        private Block fBlock;

        public BlockSelector(TreeList list)
        {
            if (list == null)
                throw new Exception("A valid TreeList instance must be passed to the constructor");

            fList = list;
            fBlock = new Block();

            fList.FocusedColumnChanged += new FocusedColumnChangedEventHandler(fList_FocusedColumnChanged);
            fList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(fList_FocusedNodeChanged);
            fList.MouseMove += new System.Windows.Forms.MouseEventHandler(fList_MouseMove);
            fList.MouseDown += new MouseEventHandler(fList_MouseDown);
            fList.NodeCellStyle += new GetCustomNodeCellStyleEventHandler(fList_NodeCellStyle);
        }

        public Block Block { get { return fBlock; } }

        public TreeList List { get { return fList; } }

        private void fList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (fBlock.Contains(e.Column.VisibleIndex, e.Node.Id))
                e.Appearance.BackColor = SystemColors.Highlight;
        }

        private delegate void SelectEntireNodeDlg(TreeListNode node);

        private void SelectEntireNode(TreeListNode node)
        {
            fBlock.X1 = 0;
            fBlock.X2 = fList.VisibleColumns.Count - 1;
            fBlock.Y2 = node.Id;
            if (fBlock.Modified && !fBlock.IsEmpty()) InvalidateBlock();
        }

        void fList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo hInfo = fList.CalcHitInfo(e.Location);
                if (hInfo.HitInfoType == HitInfoType.RowIndicator)
                {
                    fList.BeginInvoke(new SelectEntireNodeDlg(SelectEntireNode), hInfo.Node);
                }
            }
        }

        private void fList_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo hInfo = fList.CalcHitInfo(e.Location);
                if (hInfo.HitInfoType == HitInfoType.Cell)
                    fBlock.X2 = hInfo.Column.VisibleIndex;
                if (hInfo.HitInfoType == HitInfoType.Cell || hInfo.HitInfoType == HitInfoType.RowIndicator)
                {
                    fBlock.Y2 = hInfo.Node.Id;
                    if (fBlock.Modified && !fBlock.IsEmpty()) InvalidateBlock();
                }
            }
        }

        public void InvalidateBlock() { fList.LayoutChanged(); }

        private void fList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e) { if (fList.FocusedColumn != null) UpdateBlock(); }

        private void UpdateBlock()
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                fBlock.X2 = fList.FocusedColumn.VisibleIndex;
                fBlock.Y2 = fList.FocusedNode.Id;
            }
            else
            {
                fBlock.X1 = fList.FocusedColumn.VisibleIndex;
                fBlock.Y1 = fList.FocusedNode.Id;
                fBlock.X2 = fBlock.X1;
                fBlock.Y2 = fBlock.Y1;
            }
            if (fBlock.Modified) InvalidateBlock();
        }

        void fList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e) { UpdateBlock(); }

        public string GetSelectedValues()
        {
            string result = string.Empty;
            if (!fBlock.IsEmpty())
            {
                SelectionOperation operation = new SelectionOperation(fBlock);
                fList.NodesIterator.DoOperation(operation);
                result = operation.Result;
            }
            return result;
        }
    }

    public class Block
    {
        private int FX1;
        private int FX2;
        private int FY1;
        private int FY2;
        private bool FModified;

        public bool Between(int a, int b, int c)
        {
            if (a > b)
            {
                int temp = a;
                a = b;
                b = temp;
            }
            return (a <= c) && (c <= b);
        }

        public bool Contains(int x, int y)
        {
            return Between(X1, X2, x) && Between(Y1, Y2, y);
        }
        public bool IsEmpty()
        {
            return (X1 == X2 && Y1 == Y2);
        }

        #region Coordinates
        public int X1
        {
            get
            {
                return FX1;
            }
            set
            {
                if (FX1 != value)
                {
                    FX1 = value;
                    FModified = true;
                }
            }
        }
        public int X2
        {
            get
            {
                return FX2;
            }
            set
            {
                if (FX2 != value)
                {
                    FX2 = value;
                    FModified = true;
                }
            }
        }
        public int Y1
        {
            get
            {
                return FY1;
            }
            set
            {
                if (FY1 != value)
                {
                    FY1 = value;
                    FModified = true;
                }
            }
        }
        public int Y2
        {
            get
            {
                return FY2;
            }
            set
            {
                if (FY2 != value)
                {
                    FY2 = value;
                    FModified = true;
                }
            }
        }
        #endregion

        public bool Modified
        {
            get
            {
                return FModified;
            }
        }
    }

    public class SelectionOperation : TreeListOperation
    {
        private Block block;
        private string result = string.Empty;
        private const string CellDelimeter = "\t";
        private const string LineDelimeter = "\r\n";

        public SelectionOperation(Block block) { this.block = block; }

        public string Result { get { return result; } }

        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            if (!block.Between(block.Y1, block.Y2, node.Id)) return;
            foreach (TreeListColumn column in node.TreeList.Columns)
                if (block.Contains(column.VisibleIndex, node.Id))
                {
                    result += node.GetDisplayText(column);
                    result += CellDelimeter;
                }
            result += LineDelimeter;
        }
    }

}
