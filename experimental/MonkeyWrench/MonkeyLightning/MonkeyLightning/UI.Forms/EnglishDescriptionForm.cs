using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI.Toolbox;
using MonkeyLightning.DataProvider;
using MonkeyLightning.Framework;

namespace MonkeyLightning.UI.Forms
{
    public partial class EnglishDescriptionForm : Form
    {
        private Dictionary<string, RuleValue> links;
        private int linkCount;
        private Trade trade;

        public EnglishDescriptionForm(Trade trade)
        {
            InitializeComponent();

            this.trade = trade;
            DialogResult = DialogResult.Abort;

            linkCount = 0;

            DisplayTradeDescription();
        }

        void DisplayTradeDescription()
        {
            links = new Dictionary<string, RuleValue>();

            StringBuilder sb = new StringBuilder();
            
            sb.Append("<!DOCTYPE html>\n");
            sb.Append("<html lang=\"en\">\n");
            sb.Append("<head>\n");
            sb.Append("<title>" + trade.Name + "</title>\n");
            sb.Append("<meta charset=\"utf-8\" />\n");
            sb.Append("<style>\n");

            sb.Append("a:link {text-decoration:none; color:blue;}\n");
            sb.Append("a:visited {text-decoration:none; color:blue;}\n");
            sb.Append("a:hover {text-decoration:underline;}\n");
            sb.Append("a:active {text-decoration:none;}\n");
            sb.Append("h1 {color:#666666;}\n");
            sb.Append("h2 {margin-bottom: 0px;}\n");
            sb.Append("h3 {font-weight:bold; color:green; margin-top: 5px; margin-bottom: 0px;}\n");

            sb.Append("</style>\n");
            sb.Append("</head>\n");
            sb.Append("<body>\n");
            sb.Append("<h1>" + trade.Name + "</h1>\n");
            sb.Append("<hr>\n");
            sb.Append("<h2><u>All</u> of the following PRECONDITIONS must be met before this trade is active:</h2>\n");
            AppendRules(sb, trade.Steps[TradeStepType.PRECONDITIONS].Rules);
            sb.Append("<h2><u>Any</u> of the following ENTRY RULES must be met before this trade is entered:</h2>\n");
            AppendRules(sb, trade.Steps[TradeStepType.ENTRY].Rules);
            sb.Append("<h2>This trade will exit if <u>any</u> of the following EXIT RULES are met:</h2>\n");
            AppendRules(sb, trade.Steps[TradeStepType.EXIT].Rules);
            sb.Append("<h2>This trade will stop out if <u>any</u> of the following STOP RULES are met:</h2>\n");
            AppendRules(sb, trade.Steps[TradeStepType.STOP].Rules);
            sb.Append("</body>\n");
            sb.Append("</html>\n");

            string html = sb.ToString();

            browser.DocumentText = html;
            
            FileStream stream = File.Create(Application.StartupPath + "\\test.html");
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(html);
            writer.Flush();
            writer.Close();
        }

        void AppendRules(StringBuilder sb, List<TradeRule> rules)
        {
            if (rules.Count == 0)
            {
                sb.Append("(there are no rules for this step)\n");
                return;
            }

            foreach (TradeRule rule in rules)
            {
                sb.Append("<h3>" + rule.Name + "</h3>\n");
                foreach (RuleCondition condition in rule.Conditions)
                {
                    ++linkCount;
                    string linkID1 = "#link" + Convert.ToString(linkCount) + "A";
                    string linkID2 = "#link" + Convert.ToString(linkCount) + "B";

                    // VALUE1
                    if (condition.Value1.DataProvider.GetModifyUserInterface() == null)
                    {
                        sb.Append("<b>" + condition.Value1.DataProvider.EnglishDescription + "</b>");
                    }
                    else
                    {
                        sb.Append("<a href=\"" + linkID1 + "\">" + condition.Value1.DataProvider.EnglishDescription + "</a>");
                        links[linkID1] = condition.Value1;
                    }

                    // CONDITION
                    sb.Append("  " + condition.Comparison.EnglishDescription + "  ");

                    // VALUE2
                    if (condition.Value2.DataProvider.GetModifyUserInterface() == null)
                    {
                        sb.Append("<b>" + condition.Value2.DataProvider.EnglishDescription + "</b><br>\n");
                    }
                    else
                    {
                        sb.Append("<a href=\"" + linkID2 + "\">" + condition.Value2.DataProvider.EnglishDescription + "</a><br>\n");
                        links[linkID2] = condition.Value2;
                    }
                }
            }
        }

        private void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //e.Cancel = true;
            string urlString = e.Url.ToString();
            int i = urlString.IndexOf("#link");
            if (i >= 0)
            {
                string link = urlString.Substring(i);
                Console.WriteLine(link);
                e.Cancel = true;

                RuleValue rv = links[link];
                DPModifyControl modifyControl = rv.DataProvider.GetModifyUserInterface();
                var form = DPControlContainerModifyForm.CreateForm(modifyControl);

                WinForms.SetFormRelativePosition(form, this, FormRelativePosition.LEFT);
                DialogResult dialogResult = form.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    rv.DataProvider.UpdateProviderFromPropertyValues();
                    DisplayTradeDescription();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

    } // class
} // namespace


/*
<!DOCTYPE html> 
<html lang="en"> 
<head> 
	<title> _title_ </title> 
	<meta charset="utf-8" /> 
	<!--[if lt IE 9]>
  	<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
  	<![endif]--> 
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js"></script>
	<script>
 
	$("document").ready( function() {
 
		// code
 
	});
	</script>
	<style>
	
		// css
	
	</style>
</head>
<body>
 
</body>
</html>
*/