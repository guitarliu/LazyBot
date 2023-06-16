using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LazyBot
{
    /// <summary>
    /// PipeFittingWeightSearch.xaml 的交互逻辑
    /// </summary>
    public partial class PipeFittingWeightSearch : Window
    {
        public PipeFittingWeightSearch()
        {
            InitializeComponent();
            DataContext = this;

            // Add Details to DataGrid
            Assembly assm = Assembly.GetExecutingAssembly();
            Stream lineStr = assm.GetManifestResourceStream("LazyBot.Database.PipeDiameter.db");
            StreamReader lineSr = new StreamReader(lineStr);
            string[] lines = lineSr.ReadToEnd().Split('\n');

            List<string> cmbxContent = new List<string>();

            foreach (string line in lines)
            {
                if (line.Trim() == string.Empty)
                {
                    continue;
                }
                cmbxContent.Add(line.Split('\t')[0]);

                PipeStandard pstData = new PipeStandard
                {
                    pStName = line.Split('\t')[0],
                    pStDN = line.Split('\t')[1],
                    pStDN1 = line.Split('\t')[2],
                    pStD = line.Split('\t')[3],
                    pStD1 = line.Split('\t')[4],
                    pStWt = line.Split('\t')[5],
                    pStWt1 = line.Split('\t')[6],
                    pStWeight = line.Split('\t')[7],
                    pStPage = line.Split('\t')[8].Trim('\n', '\r')
                };

                xPipeStandard.Items.Add(pstData);
            }

            string[] newCmbxContent = cmbxContent.Distinct().ToList().ToArray();
            CmbxTypeName.ItemsSource = newCmbxContent;
        }

        public class PipeStandard
        {
            public string pStName { get; set; }
            public string pStDN { get; set; }
            public string pStDN1 { get; set; }
            public string pStD { get; set; }
            public string pStD1 { get; set; }
            public string pStWt { get; set; }
            public string pStWt1 { get; set; }
            public string pStWeight { get; set; }
            public string pStPage { get; set; }
        }

        public void DataGrid_Update()
        {
            // Clean DataGrid to Zero
            xPipeStandard.Items.Clear();

            // Refresh DataGrid 
            xPipeStandard.Items.Refresh();

            // Add Details to DataGrid
            Assembly assm = Assembly.GetExecutingAssembly();
            Stream lineStr = assm.GetManifestResourceStream("LazyBot.Database.PipeDiameter.db");
            StreamReader lineSr = new StreamReader(lineStr);
            string[] lines = lineSr.ReadToEnd().Split('\n');

            foreach (string line in lines)
            {
                if (line.Trim() == string.Empty)
                {
                    continue;
                }
                if ((TbPipeDiameter.Text != "") && (CmbxTypeName.SelectedIndex != -1))
                {
                    if ((TbPipeDiameter.Text == line.Split('\t')[1]) && (CmbxTypeName.SelectedItem.ToString() == line.Split('\t')[0]))
                    {
                        // Refresh DataGrid with Selected Database
                        PipeStandard pstData_New = new PipeStandard
                        {
                            pStName = line.Split('\t')[0],
                            pStDN = line.Split('\t')[1],
                            pStDN1 = line.Split('\t')[2],
                            pStD = line.Split('\t')[3],
                            pStD1 = line.Split('\t')[4],
                            pStWt = line.Split('\t')[5],
                            pStWt1 = line.Split('\t')[6],
                            pStWeight = line.Split('\t')[7],
                            pStPage = line.Split('\t')[8].Trim('\n', '\r'),
                        };

                        xPipeStandard.Items.Add(pstData_New);
                    }
                }
                else if ((TbPipeDiameter.Text == "") && (CmbxTypeName.SelectedIndex != -1))
                {
                    if (CmbxTypeName.SelectedItem.ToString() == line.Split('\t')[0])
                    {
                        // Refresh DataGrid with Selected Database
                        PipeStandard pstData_New = new PipeStandard
                        {
                            pStName = line.Split('\t')[0],
                            pStDN = line.Split('\t')[1],
                            pStDN1 = line.Split('\t')[2],
                            pStD = line.Split('\t')[3],
                            pStD1 = line.Split('\t')[4],
                            pStWt = line.Split('\t')[5],
                            pStWt1 = line.Split('\t')[6],
                            pStWeight = line.Split('\t')[7],
                            pStPage = line.Split('\t')[8].Trim('\n', '\r')
                        };

                        xPipeStandard.Items.Add(pstData_New);
                    }
                }
                else if ((TbPipeDiameter.Text != "") && (CmbxTypeName.SelectedIndex == -1))
                {
                    if (TbPipeDiameter.Text == line.Split('\t')[1])
                    {
                        // Refresh DataGrid with Selected Database
                        PipeStandard pstData_New = new PipeStandard
                        {
                            pStName = line.Split('\t')[0],
                            pStDN = line.Split('\t')[1],
                            pStDN1 = line.Split('\t')[2],
                            pStD = line.Split('\t')[3],
                            pStD1 = line.Split('\t')[4],
                            pStWt = line.Split('\t')[5],
                            pStWt1 = line.Split('\t')[6],
                            pStWeight = line.Split('\t')[7],
                            pStPage = line.Split('\t')[8].Trim('\n', '\r')
                        };

                        xPipeStandard.Items.Add(pstData_New);
                    }
                }
                else
                {
                    // Refresh DataGrid with Whole Database
                    PipeStandard pstData_New = new PipeStandard
                    {
                        pStName = line.Split('\t')[0],
                        pStDN = line.Split('\t')[1],
                        pStDN1 = line.Split('\t')[2],
                        pStD = line.Split('\t')[3],
                        pStD1 = line.Split('\t')[4],
                        pStWt = line.Split('\t')[5],
                        pStWt1 = line.Split('\t')[6],
                        pStWeight = line.Split('\t')[7],
                        pStPage = line.Split('\t')[8].Trim('\n', '\r')
                    };
                    xPipeStandard.Items.Add(pstData_New);
                }
            }
        }

        private void xPipeStandard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = xPipeStandard.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                string[] content = {
                    selectedRow["pStName"].ToString(),
                    selectedRow["pStDN"].ToString(),
                    selectedRow["pStDN1"].ToString(),
                    selectedRow["pStD"].ToString(),
                    selectedRow["pStD1"].ToString(),
                    selectedRow["pStWt"].ToString(),
                    selectedRow["pStWt1"].ToString(),
                    selectedRow["pStWeight"].ToString(),
                    selectedRow["pStPage"].ToString()
                };

                string selectedContent = string.Join(",", content);
                // Class1.(selectedContent);
            }
        }

        private void TbPipeDiameter_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGrid_Update();
        }

        private void CmbxTypeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid_Update();
        }
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                // throw;
            }
        }
        private void MinMumBt_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
