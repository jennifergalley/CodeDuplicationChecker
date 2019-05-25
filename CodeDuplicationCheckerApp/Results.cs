using CodeDuplicationChecker;
using CountMatrixCloneDetection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeDuplicationCheckerApp
{
    public partial class Results : Form
    {
        /// <summary>
        /// Gets or sets the current session reuslts
        /// </summary>
        private List<CMCDDuplicateResult> currentSessionResults = null;
        
        /// <summary>
        /// The constructor
        /// </summary>
        public Results()
        {
            InitializeComponent();
            cdcPath.Text = Path.GetFullPath(".");
            dataGridView1.Visible = false;
            dataGridView1.DataBindingComplete += DataGridView1_DataBindingComplete;
        }

        /// <summary>
        /// Event handlers for te file picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = cdcFolder.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = cdcFolder.SelectedPath;
                cdcPath.Text = file;
            }
        }

        /// <summary>
        /// Evbent handler for the CMCD run
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdcRun_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            // Start the CMCD
            if (!string.IsNullOrEmpty(cdcPath.Text) && (Directory.Exists(cdcPath.Text) || File.Exists(cdcPath.Text)))
            {
                currentSessionResults = CMCD.Run(cdcPath.Text);
                List<CMCDResults> list = new List<CMCDResults>(); 
                foreach(var result in currentSessionResults)
                {
                    list.Add(new CMCDResults()
                    {
                        Method1 = result.MethodA.MethodName,
                        File1 = result.MethodA.FileName,
                        Method2 = result.MethodB.MethodName,
                        File2 = result.MethodB.FileName,
                        Score = result.Score
                    });
                }
                dataGridView1.DataSource = list;
                dataGridView1.Visible = true;
                dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            }
            else
            {
                MessageBox.Show("Invalid directory or file path.", "Error");
            }
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Event handler for the cell content click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (e.ColumnIndex == 4)
            {
                var cmcdResult = currentSessionResults[e.RowIndex];
                var results = new List<DuplicateInstance>()
                {
                    new DuplicateInstance(cmcdResult.MethodA),
                    new DuplicateInstance(cmcdResult.MethodB),
                };
                VisualizeDiffs.TryGenerateResultsFile(results, out var resultsFilePath, false);
                Process.Start(resultsFilePath);
            }
            Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Event handler for the data binding complete 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Cells["Score"] = new DataGridViewLinkCell();
                DataGridViewLinkCell c = r.Cells["Score"] as DataGridViewLinkCell;
                c.LinkColor = Color.Blue;
                c.LinkColor = Color.Blue;
                c.TrackVisitedState = true;
                c.VisitedLinkColor = Color.YellowGreen;
            }
            Cursor = Cursors.Arrow;
        }

    }

    /// <summary>
    /// Data source class for the grid view
    /// </summary>
    public class CMCDResults
    {
        /// <summary>
        /// Gets or sets Method 1 Name 
        /// </summary>
        public string Method1 { get; set; }

        /// <summary>
        /// Gets or sets File 1 Name 
        /// </summary>
        public string File1 { get; set; }

        /// <summary>
        /// Gets or sets Method 2 Name 
        /// </summary>
        public string Method2 { get; set; }

        /// <summary>
        /// Gets or sets File 2 Name 
        /// </summary>
        public string File2 { get; set; }

        /// <summary>
        /// Gets or sets the score
        /// </summary>
        public double Score { get; set; }
    }
}
