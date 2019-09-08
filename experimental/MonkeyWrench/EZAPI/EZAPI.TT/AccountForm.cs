using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using EZAPI.Containers;
using TradingTechnologies.TTAPI;

namespace EZAPI
{
    public partial class AccountForm : Form
    {
        public AccountForm()
        {
            InitializeComponent();

            IResourceReader reader = null;
            Dictionary<string, string> loginResources = new Dictionary<string, string>();
            try
            {
                reader = new ResourceReader("Account.resources");
                IDictionaryEnumerator dict = reader.GetEnumerator();
                gridAccounts.Rows.Clear();
                while (dict.MoveNext())
                {
                    string key = dict.Key.ToString();
                    string valueString = dict.Value.ToString();
                    string[] values = valueString.Split(',');
                    gridAccounts.Rows.Add(key, values[0], values[1], values[2]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void AccountForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gridAccounts.CommitEdit(DataGridViewDataErrorContexts.Commit);
            gridAccounts.EndEdit();

            IResourceWriter writer = new ResourceWriter("Account.resources");
            foreach (DataGridViewRow row in gridAccounts.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string key = row.Cells[0].Value.ToString();
                    if (!key.Trim().Equals(""))
                    {
                        string value = string.Format("{0},{1},{2}", row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                        writer.AddResource(key, value);
                    }
                }
            }
            writer.Close();
        }

    } // class
} // namespace
