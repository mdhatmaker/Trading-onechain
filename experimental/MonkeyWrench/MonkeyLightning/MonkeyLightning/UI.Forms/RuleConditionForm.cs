using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using EZAPI.Toolbox;
using MonkeyLightning.DataProvider;
using MonkeyLightning.Framework;
using MonkeyLightning.Framework.Comparison;

namespace MonkeyLightning.UI.Forms
{
    public partial class RuleConditionForm : Form
    {
        Dictionary<string, Type> dataProviders;
        Dictionary<string, IRuleComparison> ruleComparisons;

        RuleValue ruleValue1;
        RuleValue ruleValue2;
        IRuleComparison ruleComparison;
        IDataProvider dataProvider1;
        IDataProvider dataProvider2;
        int previousComboValue1;
        int previousComboValue2;
        bool settingComboValue;

        public RuleCondition Condition
        {
            get { return condition; }
        }
        
        private RuleCondition condition;


        public RuleConditionForm()
        {
            InitializeComponent();

            dataProviders = new Dictionary<string, Type>();
            ruleComparisons = new Dictionary<string, IRuleComparison>();

            // Find all classes that implement IDataProvider.
            /*var dpInstances = from t in dpAssembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(IDataProvider))
                                && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IDataProvider;*/

            // Find all classes that implement IDataProvider.
            string location = Assembly.GetExecutingAssembly().Location;
            string projectFolder = @"MonkeyLightning\";
            int pathIndex = location.IndexOf(projectFolder);
#if DEBUG
            string assemblyPath = location.Substring(0, pathIndex + projectFolder.Length) + @"DataProvider.Providers.Basic\bin\Debug\DataProvider.Providers.Basic.dll";
#else
            string assemblyPath = location.Substring(0, pathIndex + projectFolder.Length) + @"DataProvider.Providers.Basic\bin\Release\DataProvider.Providers.Basic.dll";
#endif
            var dpAssembly = Assembly.LoadFrom(assemblyPath);
            //var dpAssembly = Assembly.GetAssembly(typeof(DataProviderUserInteger));

            var dpTypes = from t in dpAssembly.GetTypes()
                              where t.GetInterfaces().Contains(typeof(IDataProvider))
                                  && t.GetConstructor(Type.EmptyTypes) != null
                              select t;

            // Fill the combo boxes with the IDataProvider instances.
            comboValue1.Items.Clear();
            comboValue2.Items.Clear();
            foreach (Type type in dpTypes)
            {
                object dp = Activator.CreateInstance(type);
                //PropertyInfo info = type.GetProperty("Name", typeof(string));
                //info.GetValue(
                var dataProviderName = (string)type.GetProperty("Name").GetValue(dp, null);
                //string nameProperty = type.GetProperty("Name", typeof(string)).GetValue(   //.GetValue GetValue(null) as string;
                comboValue1.Items.Add(dataProviderName);
                comboValue2.Items.Add(dataProviderName);

                dataProviders[dataProviderName] = type;
            }

            // Find all classes that implement IRuleComparison.
            var comparisonInstances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(IRuleComparison))
                                && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IRuleComparison;

            // Fill the combo box with the IRuleComparison instances.
            comboCompare.Items.Clear();
            foreach (var instance in comparisonInstances)
            {
                string friendlyName = instance.Name + string.Format(" ( {0} )", instance.Symbol);
                comboCompare.Items.Add(friendlyName);

                ruleComparisons[friendlyName] = instance;
            }

            previousComboValue1 = -1;
            previousComboValue2 = -1;
            settingComboValue = false;

            //comboValue1.SelectedIndex = 0;
            //comboValue2.SelectedIndex = 0;

            //ruleValue1.ValueUpdated += ruleValue_ValueUpdated;
            //ruleValue2.ValueUpdated += ruleValue_ValueUpdated;
        }

        void ruleValue_ValueUpdated(object sender, ValueUpdateEventArgs e)
        {
            BuildCondition();
            ConditionCheck();
        }

        public RuleConditionForm(RuleCondition condition) : this()
        {
            this.condition = condition;

            RuleValue v1 = condition.Value1;
            RuleValue v2 = condition.Value2;
            IRuleComparison compare = condition.Comparison;

            throw new NotImplementedException();

            /*if (v1.IsNumeric)
            {
                comboValue1.SelectedItem = "USER_NUMBER";
                numericValue1.Value = v1;
            }
            else if (v1.ValueType == RuleValueType.TEXT)
            {
                comboValue1.SelectedItem = "USER_TEXT";
                textValue1.Text = v1;
            }

            if (v2.IsNumeric)
            {
                comboValue2.SelectedItem = "USER_NUMBER";
                numericValue2.Value = v2;
            }
            else if (v2.ValueType == RuleValueType.TEXT)
            {
                comboValue2.SelectedItem = "USER_TEXT";
                textValue2.Text = v2;
            }

            for (int i=0; i<comboCompare.Items.Count; i++)
            {
                if (comboCompare.Items[i].ToString().StartsWith(compare.Name))
                {
                    comboCompare.SelectedIndex = i;
                    break;
                }
            }*/
        }

        private void comboValue1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (settingComboValue == true)
                return;

            ShowTrueFalseBox(false);

            string dataProviderName = comboValue1.Text;
            dataProvider1 = (IDataProvider)Activator.CreateInstance(dataProviders[dataProviderName]);

            txtDescription.Text = dataProvider1.Name + ": " + dataProvider1.Description;

            DialogResult dialogResult = System.Windows.Forms.DialogResult.Cancel;

            // See if we need to display a UI for this data provider.
            if (dataProvider1.GetModifyUserInterface() != null)
            {
                DPModifyControl modifyControl = dataProvider1.GetModifyUserInterface();
                Size controlSize = modifyControl.Size;
                var dpModifyForm = new DPControlContainerModifyForm();
                dpModifyForm.Width = controlSize.Width + 16;
                dpModifyForm.Height = controlSize.Height + 112;
                Panel panel = dpModifyForm.Controls["panelControl"] as Panel;
                panel.Controls.Add(modifyControl);
                modifyControl.Dock = DockStyle.Fill;
                //dpModifyForm.OKButton.Enabled = false;

                WinForms.SetFormRelativePosition(dpModifyForm, this, FormRelativePosition.LEFT);
                dialogResult = dpModifyForm.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                    dataProvider1.UpdateProviderFromPropertyValues();
                else
                    ResetComboValue1();
            }

            if (dataProvider1.GetModifyUserInterface() == null || dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                previousComboValue1 = comboValue1.SelectedIndex;
                ruleValue1 = new RuleValue(dataProvider1);
                ruleValue1.ValueUpdated += ruleValue_ValueUpdated;
                DPDisplayControl displayControl = ruleValue1.DataProvider.GetDisplayUserInterface();
                panelValue1UI.Controls.Clear();
                panelValue1UI.Controls.Add(displayControl);
                displayControl.Dock = DockStyle.Fill;
                
                dataProvider1.UpdateProviderFromPropertyValues();

                if (dataProvider1.GetModifyUserInterface() == null)
                    displayControl.Cursor = Cursors.Default;
                else
                {
                    displayControl.Cursor = Cursors.Hand;
                    displayControl.Click += displayControl1_Click;
                }

                CheckConditionEnableTest();
            }
        }

        private void comboValue2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (settingComboValue == true)
                return;

            ShowTrueFalseBox(false);

            string dataProviderName = comboValue2.Text;
            dataProvider2 = (IDataProvider)Activator.CreateInstance(dataProviders[dataProviderName]);

            txtDescription.Text = dataProvider2.Name + ": " + dataProvider2.Description;

            DialogResult dialogResult = System.Windows.Forms.DialogResult.Cancel;

            // See if we need to display a UI for this data provider.
            if (dataProvider2.GetModifyUserInterface() != null)
            {
                DPModifyControl modifyControl = dataProvider2.GetModifyUserInterface();
                Size controlSize = modifyControl.Size;
                var dpModifyForm = new DPControlContainerModifyForm();
                dpModifyForm.Width = controlSize.Width + 16;
                dpModifyForm.Height = controlSize.Height + 112;
                Panel panel = dpModifyForm.Controls["panelControl"] as Panel;
                panel.Controls.Add(modifyControl);
                modifyControl.Dock = DockStyle.Fill;

                WinForms.SetFormRelativePosition(dpModifyForm, this, FormRelativePosition.RIGHT);
                dialogResult = dpModifyForm.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                    dataProvider2.UpdateProviderFromPropertyValues();
                else
                    ResetComboValue2();
            }

            if (dataProvider2.GetModifyUserInterface() == null || dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                previousComboValue2 = comboValue2.SelectedIndex;
                ruleValue2 = new RuleValue(dataProvider2);
                ruleValue2.ValueUpdated += ruleValue_ValueUpdated;
                DPDisplayControl displayControl = ruleValue2.DataProvider.GetDisplayUserInterface();
                panelValue2UI.Controls.Clear();
                panelValue2UI.Controls.Add(displayControl);
                displayControl.Dock = DockStyle.Fill;

                dataProvider2.UpdateProviderFromPropertyValues();

                if (dataProvider2.GetModifyUserInterface() == null)
                    displayControl.Cursor = Cursors.Default;
                else
                {
                    displayControl.Cursor = Cursors.Hand;
                    displayControl.Click += displayControl2_Click;
                }

                CheckConditionEnableTest();
            }
        }

        void ResetComboValue1()
        {
            settingComboValue = true;
            comboValue1.SelectedIndex = previousComboValue1;
            settingComboValue = false;
        }

        void ResetComboValue2()
        {
            settingComboValue = true;
            comboValue2.SelectedIndex = previousComboValue2;
            settingComboValue = false;
        }

        void displayControl1_Click(object sender, EventArgs e)
        {
            if (dataProvider1.GetModifyUserInterface() != null)
            {
                Control modifyControl = dataProvider1.GetModifyUserInterface();
                Size controlSize = modifyControl.Size;
                var dpModifyForm = new DPControlContainerModifyForm();
                dpModifyForm.Width = controlSize.Width + 16;
                dpModifyForm.Height = controlSize.Height + 112;
                Panel panel = dpModifyForm.Controls["panelControl"] as Panel;
                panel.Controls.Add(modifyControl);
                modifyControl.Dock = DockStyle.Fill;
                //dpModifyForm.OKButton.Enabled = false;

                DialogResult dialogResult = dpModifyForm.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    dataProvider1.UpdateProviderFromPropertyValues();
                    dataProvider1.GetDisplayUserInterface().InitializeControl();
                }

            }
        }

        void displayControl2_Click(object sender, EventArgs e)
        {
            if (dataProvider2.GetModifyUserInterface() != null)
            {
                Control modifyControl = dataProvider2.GetModifyUserInterface();
                Size controlSize = modifyControl.Size;
                var dpModifyForm = new DPControlContainerModifyForm();
                dpModifyForm.Width = controlSize.Width + 16;
                dpModifyForm.Height = controlSize.Height + 112;
                Panel panel = dpModifyForm.Controls["panelControl"] as Panel;
                panel.Controls.Add(modifyControl);
                modifyControl.Dock = DockStyle.Fill;

                DialogResult dialogResult = dpModifyForm.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    dataProvider2.UpdateProviderFromPropertyValues();
                    dataProvider2.GetDisplayUserInterface().InitializeControl();
                }
            }
        }

        private void comboCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTrueFalseBox(false);

            var comparisonName = comboCompare.Text;
            ruleComparison = ruleComparisons[comparisonName];

            CheckConditionEnableTest();
        }

        private void CheckConditionEnableTest()
        {
            if (ruleValue1 != null && ruleValue2 != null && ruleComparison != null && ruleComparison.IsValid(ruleValue1, ruleValue2))
            {
                btnOK.Enabled = true;
                BuildCondition();
                ConditionCheck();
            }
            else
            {
                btnOK.Enabled = false;
            }
        }

        private void BuildCondition()
        {
            if (ruleValue1 != null && ruleValue2 != null && ruleComparison != null)
            {
                condition = new RuleCondition(ruleValue1, ruleComparison, ruleValue2);
            }
        }

        private void ShowTrueFalseBox(bool visible)
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblTrueFalse.Visible = visible;
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
        }

        private void ConditionCheck()
        {
            if (condition == null || !condition.IsValid)
            {
                btnOK.Enabled = false;
                return;
            }

            btnOK.Enabled = true;
            ShowTrueFalseBox(true);

            try
            {
                if (condition.Value == true)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            lblTrueFalse.BackColor = Color.Green;
                            lblTrueFalse.Text = "True";
                        });
                    }
                }
                else
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            lblTrueFalse.BackColor = Color.Red;
                            lblTrueFalse.Text = "False";
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION (ConditionCheck): " + ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            BuildCondition();

            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void DisconnectEvents()
        {
            if (ruleValue1 != null) ruleValue1.ValueUpdated -= ruleValue_ValueUpdated;
            if (ruleValue2 != null) ruleValue2.ValueUpdated -= ruleValue_ValueUpdated;
        }

        private void value_Changed(object sender, EventArgs e)
        {
            ShowTrueFalseBox(false);
        }

        private void RuleConditionForm_Load(object sender, EventArgs e)
        {
            //Win32Helper.FadeInForm(this, 250);            
        }

        private void RuleConditionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisconnectEvents();
            if (dataProvider1 != null) dataProvider1.ClosingDisplayUI();
            if (dataProvider2 != null) dataProvider2.ClosingDisplayUI();

            
        }

    } // class
} // namespace
