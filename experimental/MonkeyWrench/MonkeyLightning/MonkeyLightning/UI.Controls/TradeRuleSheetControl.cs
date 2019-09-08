#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonkeyLightning.Framework;
using MonkeyLightning.Framework.IO;
using EZAPI.Toolbox.Debug;

namespace MonkeyLightning.UI.Controls
{
    public partial class TradeRuleSheetControl : UserControl
    {
        public event Action<string> StatusMessageUpdate;
        public event Action<string> ErrorMessageUpdate;

        public List<TradeRule> Rules { get { return rules; } }

        BindingSource bindingSource;
        List<TradeRule> rules;
        object cellRenameOriginalName;
        object cellRenameUpdatedName;

        public TradeRuleSheetControl()
        {
            InitializeComponent();

            // Attach the grid to a List as the datasource.
            bindingSource = new BindingSource();
            rules = new List<TradeRule>();
            bindingSource.DataSource = rules;
            gridRules.DataSource = bindingSource;

            // Format the columns and column headers.
            gridRules.Columns[0].Width = 50;
            gridRules.Columns[0].HeaderText = "active";

            gridRules.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridRules.Columns[1].HeaderText = "Rule";
            gridRules.Columns[1].ReadOnly = true;

            // Columns after "active" and "Rule name" should be hidden.
            for (int i=2; i<gridRules.Columns.Count; i++)
                gridRules.Columns[i].Visible = false;

            cellRenameOriginalName = null;
            cellRenameUpdatedName = null;
        }

        public void AddRule(TradeRule rule, bool Active = true)
        {
            rule.Active = Active;
            bindingSource.Add(rule);
            gridRules.Refresh();
        }

        public void RemoveSelectedRule()
        {
            if (SelectedRule != null)
            {
                bindingSource.Remove(SelectedRule);
                gridRules.Refresh();
            }
        }

        public TradeRule SelectedRule
        {
            get
            {
                if (gridRules.SelectedRows.Count == 1)
                {
                    int index = gridRules.SelectedRows[0].Index;
                    return bindingSource[index] as TradeRule;
                }
                else
                {
                    return null;
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool ShowActiveColumn
        {
            get
            {
                return gridRules.Columns[0].Visible;
            }
            set
            {
                gridRules.Columns[0].Visible = value;
            }
        }

        private void gridRules_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (gridRules.Rows[e.RowIndex].Selected)
            {
                using (Pen pen = new Pen(Color.DarkBlue))
                {
                    int penWidth = 2;

                    pen.Width = penWidth;

                    int x = e.RowBounds.Left + (penWidth / 2);
                    int y = e.RowBounds.Top + (penWidth / 2);
                    int width = e.RowBounds.Width - penWidth;
                    int height = e.RowBounds.Height - penWidth;

                    e.Graphics.DrawRectangle(pen, x, y, width, height);
                }

            }
        }

        public void ParentFormClosing()
        {
            //gridRules.DataSource = null;
            //bindingSource.DataSource = null;
        }

        private void itemCancel_Click(object sender, EventArgs e)
        {
            // Do nothing - just clicking will make the context menu go away.
        }

        private void itemRename_Click(object sender, EventArgs e)
        {
            //gridRules.Rows[rightClickRowIndex].ReadOnly = false;
            gridRules.CurrentCell.ReadOnly = false;
            gridRules.BeginEdit(true);
        }

        private void gridRules_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex < gridRules.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < gridRules.Columns.Count)
            {
                gridRules.CurrentCell = gridRules.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cellRenameOriginalName = gridRules.CurrentCell.Value;

                itemRename.Text = "Rename rule '" + gridRules.CurrentCell.Value + "'";
                contextMenuStrip1.Show(MousePosition);
            }

        }

        private void gridRules_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gridRules.CurrentCell.ReadOnly = true;

            cellRenameUpdatedName = gridRules.CurrentCell.Value;
            
            Spy.Print("rule name original: '{0}'   updated: '{1}'", cellRenameOriginalName, cellRenameUpdatedName);

            // If the rename fails, then we will "roll back" the Rule name in the grid.
            if (MonkeyIO.RenameRule(cellRenameOriginalName.ToString(), cellRenameUpdatedName.ToString()) == false)
            {
                MonkeySound.PlayErrorSound();
                gridRules.CurrentCell.Value = cellRenameOriginalName;
                FireErrorMessage("An error occurred attempting to rename rule.");
            }
        }

        void FireStatusMessage(string msg)
        {
            if (StatusMessageUpdate != null) StatusMessageUpdate(msg);
        }

        void FireErrorMessage(string msg)
        {
            if (ErrorMessageUpdate != null) ErrorMessageUpdate(msg);
        }

        private void gridRules_SelectionChanged(object sender, EventArgs e)
        {
            // We don't want to fire off messages if the cell is the same as previously selected.
            Spy.Print("selection changed: {0} {1}", gridRules.CurrentCell.RowIndex, gridRules.CurrentCell.ColumnIndex);
            FireStatusMessage(gridRules.CurrentCell.Value.ToString());
        }

    } // class
} // namespace
