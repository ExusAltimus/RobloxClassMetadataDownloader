using RobloxClassMetadataDownloader.ApiDumpClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobloxClassMetadataDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnDownloadMetadata_Click(object sender, EventArgs e)
        {
            try
            {
                RobloxMetadataParser robloxMetadataParser = new RobloxMetadataParser();
                IList<RobloxClass> classes = await robloxMetadataParser.DownloadApiDump();
                txtResult.Text = robloxMetadataParser.RobloxClassListToLuaLookupTable(classes);
                if (chkFormatTable.Checked)
                {
                    FormatTable();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
        }

        private void FormatTable()
        {
            int selectStart = this.txtResult.SelectionStart;


            MatchCollection cyanMatches = Regex.Matches(txtResult.Text, @"(\d+)");

            foreach (Match match in cyanMatches)
            {
                this.txtResult.Select(match.Index, match.Length);
                this.txtResult.SelectionColor = Color.FromArgb(0, 127, 127);
            }


            MatchCollection purpleMatches = Regex.Matches(txtResult.Text, @"('\w*')");

            foreach (Match match in purpleMatches)
            {
                this.txtResult.Select(match.Index, match.Length);
                this.txtResult.SelectionColor = Color.FromArgb(127, 0, 127);
            }
            MatchCollection blueMatches = Regex.Matches(txtResult.Text, "(true|false)");

            foreach (Match match in blueMatches)
            {
                this.txtResult.Select(match.Index, match.Length);
                this.txtResult.SelectionColor = Color.FromArgb(0, 0, 127);
            }

            MatchCollection yellowMatches = Regex.Matches(txtResult.Text, "[,={}]");

            foreach (Match match in yellowMatches)
            {
                this.txtResult.Select(match.Index, match.Length);
                this.txtResult.SelectionColor = Color.FromArgb(127, 127, 0);

            }

        }
    }
}
