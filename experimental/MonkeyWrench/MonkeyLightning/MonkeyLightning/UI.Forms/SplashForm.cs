using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Toolbox;
using EZAPI.Toolbox.Serialization;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.UI.Forms
{
    public partial class SplashForm : Form
    {
        public string Status
        {
            get { return lblStatus.Text; }
            set
            {
                lblStatus.Text = value;
                Application.DoEvents();
            }
        }

        public SplashForm()
        {
            InitializeComponent();
        }

    } // class
} // namespace
