//#define DESIGNER
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public partial class DPModifyTradeDetail
#if DESIGNER
        : DPModifyControlStub
#else
        : DPModifyControl
#endif
    {
        public event EventHandler SelectedValueChanged;

        static private Color RectangleColor = Color.DarkBlue;

        public override string ControlName { get { return "ModifyTradeDetail"; } }

        private Graphics gfx;
        private Rectangle rect;
        private string selectedValue;

        public DPModifyTradeDetail(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();

            gfx = this.CreateGraphics();
            rect = Rectangle.Empty;
        }

        private void lblChoice_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            int borderWidth = 4;
            Pen pen;

            // Erase previous rect.
            if (rect != Rectangle.Empty)
            {
                pen = new Pen(Color.FromKnownColor(KnownColor.Control), borderWidth);
                gfx.DrawRectangle(pen, rect);
            }

            // Draw new rect.
            rect = control.Bounds;
            pen = new Pen(RectangleColor, borderWidth);
            gfx.DrawRectangle(pen, rect);

            //lblSelected.Text = control.Tag.ToString();

            selectedValue = control.Tag.ToString().Trim();
            PropertyValues["TradeDetailTag"] = selectedValue;
            if (SelectedValueChanged != null) SelectedValueChanged(this, EventArgs.Empty);
        }


        public override void UpdateUIFromPropertyValues()
        {
            //string SelectedValue { get { return selectedValue; } }
            //EZInstrument SelectedInstrument { get { return currentInstrument; } }
        }

        public override void UpdatePropertyValuesFromUI()
        {
            PropertyValues["TradeDetailTag"] = selectedValue;

        }

    } // class
} // namespace
