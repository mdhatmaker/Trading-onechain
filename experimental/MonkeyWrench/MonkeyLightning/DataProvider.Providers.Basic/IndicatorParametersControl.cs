using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZAPI.Framework.Chart;
using EZAPI.Framework.Chart.Indicators;
using EZAPI.Toolbox.DataStructures;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public partial class IndicatorParametersControl : UserControl
    {
        const int MAX_PARAM = 6;

        public event Action ParameterChanged;
        public event Action<string> ParameterSelected;

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

        public IndicatorParametersControl()
        {
            InitializeComponent();
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

        private void txtParam_TextChanged(object sender, EventArgs e)
        {
            if (settingTextBox == true)
                return;

            UpdateParameters();
        }

        private void txtParam_Click(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            
            // If this is a Color parameter, display the ColorDialog
            if (tb.ReadOnly == true)
            {
                DialogResult dialogResult = colorDialog1.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    tb.BackColor = colorDialog1.Color;
                    UpdateParameters();
                }
            }
        }

        private void txtParam_Enter(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            var ip = tb.Tag as IIndicatorParameter;
            
            string text = ip.Name + ": " + ip.Description;

            if (ParameterSelected != null) ParameterSelected(text);
        }

    } // class
} // namespace
