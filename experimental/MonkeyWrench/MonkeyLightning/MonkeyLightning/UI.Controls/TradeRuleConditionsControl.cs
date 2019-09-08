using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MonkeyLightning.Framework;

namespace MonkeyLightning.UI.Controls
{
    public partial class TradeRuleConditionsControl : UserControl
    {
        public event EventHandler SelectionChanged;

        BindingSource bindingSource;
        List<RuleCondition> rules;

        public TradeRuleConditionsControl()
        {
            InitializeComponent();

            // Attach the grid to a List as the datasource.
            bindingSource = new BindingSource();
            rules = new List<RuleCondition>();
            bindingSource.DataSource = rules;
            gridConditions.DataSource = bindingSource;

            // Format the columns and column headers.
            gridConditions.Columns[0].Width = 50;
            gridConditions.Columns[0].HeaderText = "active";

            gridConditions.Columns[1].Width = 50;
            gridConditions.Columns[1].HeaderText = "result";

            gridConditions.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridConditions.Columns[2].HeaderText = "Value1";
            gridConditions.Columns[2].ReadOnly = true;

            gridConditions.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridConditions.Columns[3].HeaderText = "Comparison";
            gridConditions.Columns[3].ReadOnly = true;

            gridConditions.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridConditions.Columns[4].HeaderText = "Value2";
            gridConditions.Columns[4].ReadOnly = true;

            // Columns after "active" and "Rule name" should be hidden.
            for (int i = 5; i < gridConditions.Columns.Count; i++)
                gridConditions.Columns[i].Visible = false;
        }

        public void AddCondition(RuleCondition condition, bool Active = true)
        {
            bindingSource.Add(condition);
            condition.ValueChanged += condition_ValueChanged;
            gridConditions.Refresh();
        }

        void condition_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    gridConditions.Refresh();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION (TradeRuleConditionsControl): " + ex.Message);
            }
        }

        public void UpdateCondition(RuleCondition original, RuleCondition updated)
        {
            bindingSource.Remove(original);
            bindingSource.Add(updated);
            gridConditions.Refresh();
        }

        public void DeleteCondition(RuleCondition condition)
        {
            bindingSource.Remove(condition);
            gridConditions.Refresh();
        }

        public void RemoveSelectedCondition()
        {
            if (SelectedCondition != null)
            {
                bindingSource.Remove(SelectedCondition);
                gridConditions.Refresh();
            }
        }

        public IEnumerable<RuleCondition> Conditions
        {
            get
            {
                foreach (RuleCondition condition in bindingSource)
                {
                    yield return condition;
                }
            }
        }

        public RuleCondition SelectedCondition
        {
            get
            {
                if (gridConditions.SelectedRows.Count == 1)
                {
                    int index = gridConditions.SelectedRows[0].Index;
                    return bindingSource[index] as RuleCondition;
                }
                else
                {
                    return null;
                }
            }
        }

        private void gridConditions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender;
            if (e.ColumnIndex == 1)
            {
                if ((bool)e.Value == true)
                    e.CellStyle.BackColor = Color.Green;
                else
                    e.CellStyle.BackColor = Color.Red;
            }

            // Make the selected cell the same color.
            e.CellStyle.SelectionBackColor = e.CellStyle.BackColor;
            e.CellStyle.SelectionForeColor = e.CellStyle.ForeColor;
        }

        private void gridConditions_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

        }

        private void gridConditions_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (gridConditions.Rows[e.RowIndex].Selected)
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

        private void gridConditions_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        public void ParentFormClosing()
        {
            foreach (RuleCondition condition in bindingSource.List)
            {
                condition.ValueChanged -= condition_ValueChanged;
            }
            //gridConditions.DataSource = null;
            //bindingSource.DataSource = null;
        }


    } // class
} // namespace
