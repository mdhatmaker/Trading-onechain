#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI.Toolbox;
using EZAPI.Toolbox.Debug;
using MonkeyLightning.Framework;

namespace MonkeyLightning.UI.Controls
{
    public partial class TradeRowStepControl : UserControl
    {
        public TradeStep TradeStep
        {
            get { return tradeStep; }
            set
            {
                if (value != null)
                {
                    if (this.Parent != null)
                        ColorInactive = this.Parent.BackColor;

                    tradeStep = value;
                    tradeStep_ValueChanged(this, EventArgs.Empty);
                    tradeStep.StateChanged += tradeStep_StateChanged;
                }
            }
        }

        public Color ColorInactive { get; set; }
        public Color ColorActive { get; set; }
        public Color ColorWorking { get; set; }
        public Color ColorComplete { get; set; }
        public Color ColorAbort { get; set; }
        //public Color OutlineColor { get; set; }
        //public int OutlineThickness { get; set; }

        private TradeStep tradeStep;

        public TradeRowStepControl()
        {
            InitializeComponent();

            ColorInactive = this.BackColor;
            ColorActive = Color.Yellow;
            ColorWorking = Color.Blue;
            ColorComplete = Color.Green;
            ColorAbort = Color.Purple;
            //OutlineColor = Color.Purple;
            //OutlineThickness = 5;
        }

        void tradeStep_StateChanged(object sender, EventArgs e)
        {
            ColorTradeStep();
        }

        void tradeStep_ActiveChanged(object sender, EventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.Refresh();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void ColorTradeStep()
        {
            try
            {
                if (tradeStep.State == TradeStepState.COMPLETED)
                {                    
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.BackColor = ColorComplete;
                    });
                }
                else if (tradeStep.State == TradeStepState.ACTIVE)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.BackColor = ColorActive;
                    });
                }
                else if (tradeStep.State == TradeStepState.WORKING)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.BackColor = ColorWorking;
                    });
                }
                else if (tradeStep.State == TradeStepState.INACTIVE)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.BackColor = ColorInactive;
                    });
                }
                else if (tradeStep.State == TradeStepState.ABORTED)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.BackColor = ColorAbort;
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void TradeRowStepControl_Paint(object sender, PaintEventArgs e)
        {
            /*
            Graphics gfx = e.Graphics;

            Rectangle rect = this.ClientRectangle;
            
            OutlineTradeStep(gfx, rect);
            */
        }

        void OutlineTradeStep(Graphics gfx, Rectangle rect)
        {
            /*if (tradeStep.Active == true)
            {
                // Draw an outline border.
                DrawOutline(gfx, rect, OutlineColor, OutlineThickness);
            }
            else
            {
                // Remove the outline border.
                DrawOutline(gfx, rect, this.Parent.BackColor, OutlineThickness);
                DrawOutline(gfx, rect, this.ForeColor, 1);
            }*/
        }

        void DrawOutline(Graphics gfx, Rectangle rect, Color outlineColor, int outlineThickness)
        {
            Pen pen = new Pen(outlineColor, outlineThickness);
            gfx.DrawRectangle(pen, rect);
            pen.Dispose();
        }

        void tradeStep_ValueChanged(object sender, EventArgs e)
        {
            /*if (tradeStep.Evaluate() == true)
                WinForms.OutlineControlRect(this, Color.Green, 3);
            else
                WinForms.OutlineControlRect(this, Color.Red, 3);*/

            Spy.Print("{0} => {1} : {2}", lblTitle.Text, tradeStep.State, tradeStep.Evaluate());

            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lblTitle.Text = tradeStep.State.ToString() + ":" + tradeStep.Evaluate().ToString();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION (TradeStepControl): " + ex.Message);
            }

            ColorTradeStep();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        
    } // class
} // namespace
