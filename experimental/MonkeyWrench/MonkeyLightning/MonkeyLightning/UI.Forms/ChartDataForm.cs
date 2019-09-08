#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;
using EZAPI.Toolbox.Debug;
using EZAPI.Toolbox;

namespace MonkeyLightning.UI.Forms
{
    public partial class ChartDataForm : Form
    {
        IChartDataProvider provider;

        public ChartDataForm(IChartDataProvider provider)
        {
            this.provider = provider;

            InitializeComponent();

            chart1.SetChartDataProvider(provider);
            provider.DataLoadComplete += provider_DataLoadComplete;
            provider.DataSeriesUpdated += provider_DataSeriesUpdated;

            chart1.LoadingNewChart += chart1_LoadingNewChart;
        }

        void chart1_LoadingNewChart(IChartDataProvider updatedProvider)
        {
            provider.DataLoadComplete += provider_DataLoadComplete;
            provider.DataSeriesUpdated += provider_DataSeriesUpdated;

            this.Text = "*** LOADING NEW CHART ***";

            provider = updatedProvider;
            provider.DataLoadComplete += provider_DataLoadComplete;
            provider.DataSeriesUpdated += provider_DataSeriesUpdated;
        }

        void provider_DataSeriesUpdated(object sender, ezDataSeriesUpdatedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void provider_DataLoadComplete(object sender, ezDataLoadCompleteEventArgs e)
        {
            WinForms.SetWaitCursor(false);

            this.Invoke((MethodInvoker)delegate
            {
                this.Text = provider.Name;
            });
        }


    
    } // class
} // namespace
