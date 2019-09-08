#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZAPI.Framework.Chart.Indicators;
using EZAPI.Toolbox.DataStructures;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Framework.Chart
{
    public partial class UIControlIndicatorParameters : UserControl
    {
        const int MAX_PARAM = 6;

        public event Action ParameterChanged;
        public event Action<string> ParameterSelected;

        public Action ParameterChangedHandler
        {
            set
            {
                if (previousParameterChangedHandler != null) ParameterChanged -= previousParameterChangedHandler;
                ParameterChanged += value;
                previousParameterChangedHandler = value;
            }
        }

        public Action<string> ParameterSelectedHandler
        {
            set
            {
                if (previousParameterSelectedHandler != null) ParameterSelected -= previousParameterSelectedHandler;
                ParameterSelected += value;
                previousParameterSelectedHandler = value;
            }
        }

        public IndicatorParameterList Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;

                ResetParameterEditors();

                if (parameters != null)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        ShowParameterEditor(i);
                    }
                }
            }
        }

        IndicatorParameterList parameters;
        bool settingTextBox = false;
        Action previousParameterChangedHandler;
        Action<string> previousParameterSelectedHandler;
        bool leftButtonDown = false;
        int mouseX;
        int mouseY;
        TextBox activeTextBox;

        public UIControlIndicatorParameters()
        {
            InitializeComponent();

            previousParameterChangedHandler = null;
            previousParameterSelectedHandler = null;
        }

        void ShowParameterEditor(int index)
        {
            GetLabel(index).Visible = true;
            GetLabel(index).Text = parameters[index].Name;
            GetTextBox(index).Visible = true;
            DisplayValue(index, parameters[index]);
        }

        void DisplayValue(int index, IIndicatorParameter parameter)
        {
            // Store the parameter in the Tag parameter of the text box.
            GetTextBox(index).Tag = parameter;

            if (parameter.DataType == typeof(Color))
            {
                GetTextBox(index).ReadOnly = true;
                GetTextBox(index).Cursor = Cursors.Hand;
                GetTextBox(index).BackColor = (Color) parameter.Value;
            }
            else if (parameter.DataType == typeof(zChartLineStyle))
            {
                GetTextBox(index).ReadOnly = true;
                GetTextBox(index).Cursor = Cursors.Hand;
                string text = parameter.Value.ToString();
                settingTextBox = true;
                GetTextBox(index).Text = text;
                settingTextBox = false;
            }
            else
            {
                string text = parameter.Value.ToString();
                settingTextBox = true;
                GetTextBox(index).Text = text;
                settingTextBox = false;
            }
        }

        void ResetParameterEditors()
        {
            for (int i = 0; i < MAX_PARAM; i++)
            {
                GetLabel(i).Visible = false;
                GetTextBox(i).Visible = false;
                GetTextBox(i).ReadOnly = false;
                GetTextBox(i).Cursor = Cursors.IBeam;
            }
        }

        Label GetLabel(int index)
        {
            Label result = null;

            switch (index)
            {
                case 0:
                    result = lblParam1;
                    break;
                case 1:
                    result = lblParam2;
                    break;
                case 2:
                    result = lblParam3;
                    break;
                case 3:
                    result = lblParam4;
                    break;
                case 4:
                    result = lblParam5;
                    break;
                case 5:
                    result = lblParam6;
                    break;
            }

            return result;
        }

        TextBox GetTextBox(int index)
        {
            TextBox result = null;

            switch (index)
            {
                case 0:
                    result = txtParam1;
                    break;
                case 1:
                    result = txtParam2;
                    break;
                case 2:
                    result = txtParam3;
                    break;
                case 3:
                    result = txtParam4;
                    break;
                case 4:
                    result = txtParam5;
                    break;
                case 5:
                    result = txtParam6;
                    break;
            }

            return result;
        }

        void UpdateParameters()
        {
            // Catch any exceptions with the parameters (and ignore them since it is probably an invalid user entry).
            try
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (parameters[i].DataType == typeof(Color))
                    {
                        parameters[i].Value = GetTextBox(i).BackColor;
                    }
                    else if (parameters[i].DataType == typeof(zChartLineStyle))
                    {
                        parameters[i].Value = Parse.ParseEnum<zChartLineStyle>(GetTextBox(i).Text);
                    }
                    else
                    {
                        var text = GetTextBox(i).Text;
                        var value = Convert.ChangeType(text, parameters[i].DataType);
                        parameters[i].Value = value;
                    }
                }

                if (parameters.IsValid)
                {
                    if (ParameterChanged != null) ParameterChanged();
                }
            }
            catch (Exception ex)
            {
                Spy.Print("Exception updating parameters: " + ex.Message);
            }
        }

        private void txtParam_TextChanged(object sender, EventArgs e)
        {
            if (settingTextBox == true)
                return;

            UpdateParameters();
        }

        private void txtParam_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            var ip = tb.Tag as IIndicatorParameter;
            
            // If this is a Color parameter, display the ColorDialog
            if (ip.DataType == typeof(Color))            
            {
                DialogResult dialogResult = CodeProject.Dialog.DlgBox.ShowDialog(colorDialog1);
                /*DialogResult dialogResult;
                if (this.Parent == null)
                    dialogResult = colorDialog1.ShowDialog();
                else
                {
                    colorDialog1. = this.Parent.PointToScreen(new Point(this.Parent.Width / 2, this.Parent.Height / 2));
                    dialogResult = colorDialog1.ShowDialog(this.Parent);
                }*/

                if (dialogResult == DialogResult.OK)
                {
                    tb.BackColor = colorDialog1.Color;
                    UpdateParameters();
                }
            }
            else if (ip.DataType == typeof(zChartLineStyle))
            {
                // This is not great practice, but I am incrementing through the enums
                // (and resetting back to the first enum value when we reach the end (NotSet).
                zChartLineStyle lineStyle = (zChartLineStyle) ip.Value;
                ++lineStyle;
                if (lineStyle == zChartLineStyle.NotSet)
                    lineStyle = zChartLineStyle.Dash;
                tb.Text = lineStyle.ToString();
                UpdateParameters();
            }
        }

        private void txtParam_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            var ip = tb.Tag as IIndicatorParameter;
            
            string text = ip.Name + ": " + ip.Description;

            if (ParameterSelected != null) ParameterSelected(text);
        }

        private void txtParam_MouseDown(object sender, MouseEventArgs e)
        {
            activeTextBox = sender as TextBox;
            var ip = activeTextBox.Tag as IIndicatorParameter;

            if ((ip.DataType == typeof(int) || ip.DataType == typeof(double)) && e.Button == MouseButtons.Left)
            {
                leftButtonDown = true;
                mouseX = e.X;
                mouseY = e.Y;
                this.Cursor = Cursors.VSplit;
            }
        }

        private void txtParam_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && leftButtonDown == true)
            {
                leftButtonDown = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void txtParam_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftButtonDown == true)
            {
                var ip = activeTextBox.Tag as IIndicatorParameter;

                if (e.X > mouseX)
                {
                    if (ip.DataType == typeof(int))
                        Add(activeTextBox, 1);
                    else
                        Add(activeTextBox, 0.1);
                }
                else if (e.X < mouseX)
                {
                    if (ip.DataType == typeof(int))
                        Add(activeTextBox, -1);
                    else
                        Add(activeTextBox, -0.1);
                }

                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        void Add(TextBox tb, int increment)
        {
            int currentValue = Convert.ToInt32(tb.Text);
            currentValue += increment;
            tb.Text = currentValue.ToString();
        }

        void Add(TextBox tb, double increment)
        {
            double currentValue = Convert.ToDouble(tb.Text);
            currentValue += increment;
            tb.Text = currentValue.ToString();
        }
        
    } // class
} // namespace
