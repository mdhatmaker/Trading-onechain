//#define DESIGNER
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI;
using EZAPI.Framework;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public partial class DPModifyMomentum
#if DESIGNER
        : DPModifyControlStub
#else
        : DPModifyControl
#endif
    {
        public event EventHandler SelectedValueChanged;

        public override string ControlName { get { return "ModifyMomentum"; } }

        private string selectedValue = null;
        private EZInstrument currentInstrument = null;
        
        private Rectangle rect = Rectangle.Empty;
        private APIMain api;
        private Graphics gfx;

        public DPModifyMomentum(KeyValueCollection propertyValues) : base(propertyValues)
        {
            InitializeComponent();

            api = APIMain.Instance;
            gfx = this.CreateGraphics();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return txtMarketDescription.Text;
            }
            set
            {
                txtMarketDescription.Text = value;
            }
        }

        private void btnMarketChooser_Click(object sender, EventArgs e)
        {
            if (api != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Cursor.Show();

                    EZInstrument instrument = api.ShowInstrumentDialog();
                    if (instrument != null)
                    {
                        api.UnsubscribeInstrument(currentInstrument);
                        currentInstrument = instrument;
                        api.OnInsideMarketUpdate += api_OnInsideMarketUpdate;
                        txtMarketDescription.Text = currentInstrument.Name;
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                    Cursor.Show();
                }
            }
        }

        void api_OnInsideMarketUpdate(EZInstrument instrument)
        {
            if (instrument == null || currentInstrument == null || instrument.Key != currentInstrument.Key)
                return;

            this.Invoke((MethodInvoker) delegate {
                lblBid1.Text = instrument.Bid.ToString();
                lblBidVol1.Text = instrument.BidQty.ToString();
                lblOffer1.Text = instrument.Ask.ToString();
                lblOfferVol1.Text = instrument.AskQty.ToString();
                lblLast1.Text = instrument.Last.ToString();
                lblLastVol1.Text = instrument.LastQty.ToString();
                lblTotalVol1.Text = instrument.Volume.ToString();
                lblNet1.Text = instrument.NetPos.ToString();
                lblBuys1.Text = instrument.NetBuys.ToString();
                lblSells1.Text = instrument.NetSells.ToString();
            });
        }

        private void lbl_Click(object sender, EventArgs e)
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
            pen = new Pen(Color.DarkBlue, borderWidth);
            gfx.DrawRectangle(pen, rect);

            lblSelected.Text = control.Tag.ToString();

            selectedValue = control.Tag.ToString().Trim();
            if (SelectedValueChanged != null) SelectedValueChanged(this, EventArgs.Empty);
        }

        public override void ParentFormClosing()
        {
            base.ParentFormClosing();
            api.OnInsideMarketUpdate -= api_OnInsideMarketUpdate;
        }

        public override void UpdateUIFromPropertyValues()
        {
        //string SelectedValue { get { return selectedValue; } }
        //EZInstrument SelectedInstrument { get { return currentInstrument; } }
        }

        public override void UpdatePropertyValuesFromUI()
        {
            //PropertyValues["QuoteItem"] = selectedValue;
            PropertyValues["InstrumentKey"] = APIFactory.StringFromInstrumentKey(currentInstrument.Key);
        }


    } // class
} // namespace
