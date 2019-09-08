using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace EZAPI.Toolbox
{
    public partial class PropertyForm : Form
    {
        public delegate void PropertyFormResult(DialogResult dlgResult);
        public event PropertyFormResult OnPropertyFormClose;

        static string _appPath = Path.GetDirectoryName(Application.ExecutablePath);
        
        string _settingsFile = null; 
        
        public string SettingsFile { get { return _settingsFile; } }
        public string AppPath { get { return _appPath; } }

        #region CONSTRUCTORS
        // If no settings filename is passed, use 'Settings.txt' in application directory.
        public PropertyForm() : this(_appPath + "\\SETTINGS.TXT")
        {
        }

        // Pass a settings filename to specify the filename explicitly.
        public PropertyForm(string settingsFile)
        {
            InitializeComponent();

            _settingsFile = settingsFile;

            ReadSettings();
        }

        void PropertyForm_OnPropertyFormClose(DialogResult dlgResult)
        {
            throw new NotImplementedException();
        }

        public PropertyForm(string windowTitle, string instructions, string settingsFile)
        {
            InitializeComponent();

            this.Text = windowTitle;
            lblInstructions.Text = instructions;
            _settingsFile = settingsFile;

            ReadSettings();
        }

        public PropertyForm(string windowTitle, string instructions)
        {
            InitializeComponent();

            this.Text = windowTitle;
            lblInstructions.Text = instructions;
            _settingsFile = _appPath + "\\Settings.txt";

            ReadSettings();
        }
        #endregion

        // Retrieve a single setting (given the property name).
        public string GetSetting(string settingName)
        {
            string result = null;

            Dictionary<string, string> settings = GetSettings();

            if (settings.ContainsKey(settingName))
                result = settings[settingName];

            return result;
        }

        // Retrieve ALL of the settings as a dictionary.
        public Dictionary<string,string> GetSettings()
        {
            Dictionary<string,string> settings = new Dictionary<string,string>();

            DataGridViewRowCollection rows = propertyGrid.Rows;
            foreach (DataGridViewRow row in rows)
            {
                string name = row.Cells[0].Value as string;
                string value = row.Cells[1].Value as string;

                if (name != null && value != null)
                    settings.Add(name.Trim(), value.Trim());
            }

            return settings;
        }

        // Read the settings file into the grid on this form.
        private void ReadSettings()
        {
            // Start with an empty grid
            propertyGrid.Rows.Clear();

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(_settingsFile))
                {
                    String line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] settings = line.Split(',');
                        propertyGrid.Rows.Add(settings);
                        //Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        // Write the settings from the grid on this form to a file.
        private void WriteSettings()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamWriter sw = new StreamWriter(_settingsFile))
                {
                    // Write each grid row to the settings file.
                    DataGridViewRowCollection rows = propertyGrid.Rows;
                    foreach (DataGridViewRow row in rows)
                    {
                        string st1 = row.Cells[0].Value.ToString().Trim();
                        string st2 = row.Cells[1].Value.ToString().Trim();
                        string line = st1 + "," + st2;
                        if (!(st1.Equals("") && st2.Equals("")))
                        {
                            sw.WriteLine(line);
                            Console.WriteLine("WRITE: " + line);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be written:");
                Console.WriteLine(e.Message);
            }
        }

        private void PropertyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReadSettings();
            this.Hide();
            e.Cancel = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            WriteSettings();
            this.Hide();
            PropertyFormClose(DialogResult.OK);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReadSettings();
            this.Hide();
            PropertyFormClose(DialogResult.Cancel);
        }

        private void btnClearRows_Click(object sender, EventArgs e)
        {
            DataGridViewRowCollection rows = propertyGrid.Rows;
            rows.Clear();
        }

        // By Default, create an OnXXXX Method, to call the Event
        protected void PropertyFormClose(DialogResult dlgResult)
        {
            if (OnPropertyFormClose != null)
            {
                OnPropertyFormClose(dlgResult);
            }
        }

        // Double-clicking in a Property Value cell will bring up a file dialog so the user
        // can select a filename (and if a .WAV file is selected the sound will be played).
        private void propertyGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DialogResult result = openFileDialog1.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    string filename = openFileDialog1.FileName;
                    DataGridViewRow row = propertyGrid.Rows[e.RowIndex];
                    row.Cells[1].Value = filename;
                    propertyGrid.EndEdit();

                    // If it is a sound file, we will preview it for the user
                    if (filename.ToUpper().Trim().EndsWith(".WAV"))
                    {
                        //Stream str = Properties.Resources.mySoundFile;
                        SoundPlayer snd = new SoundPlayer(filename);
                        snd.Play();
                    }
                }
            }
        }


    } // class
} // namespace
