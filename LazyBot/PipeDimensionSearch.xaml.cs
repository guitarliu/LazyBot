using System;
using System.Collections.Generic;
using System.Linq;
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
    /// PipeDimensionSearch.xaml 的交互逻辑
    /// </summary>
    public partial class PipeDimensionSearch : Window
    {
        readonly Dictionary<double, List<double>> PipeDiameterDict = new Dictionary<double, List<double>>()
        {
            { 50, new List<double> { 57, 4} },
            { 70, new List<double> { 73, 4} },
            { 80, new List<double> { 89, 4} },
            { 100, new List<double> { 108, 4} },
            { 125, new List<double> { 133, 4 } },
            { 150, new List<double>{ 159, 4} },
            { 200, new List<double>{ 219, 6} },
            { 250, new List<double>{ 273, 8} },
            { 300, new List<double>{ 325, 8} },
            { 350, new List<double>{ 377, 8} },
            { 400, new List<double>{ 426, 8} },
            { 450, new List<double>{ 480, 8} },
            { 500, new List<double>{ 530, 8} },
            { 600, new List<double>{ 630, 8} },
            { 700, new List<double>{ 720, 8} },
            { 800, new List<double>{ 820, 10} },
            { 900, new List<double>{ 920, 10} },
            { 1000, new List<double>{ 1020, 10} },
            { 1200, new List<double>{ 1220, 12 } },
            { 1400, new List<double>{ 1420, 12} },
            { 1600, new List<double>{ 1620, 14} },
            { 1800, new List<double>{ 1820, 16} },
            { 2000, new List<double>{ 2020, 18 } },
            { 2200, new List<double>{ 2220, 20} },
            { 2400, new List<double>{ 2420, 20} },
            { 2600, new List<double>{ 2620, 20} }
        };
        public PipeDimensionSearch()
        {
            InitializeComponent();
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

        private void Tbx_Pipe_DN_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Tbx_Pipe_D.Text = PipeDiameterDict[double.Parse(Tbx_Pipe_DN.Text)][0].ToString();
                Tbx_Pipe_WT.Text = PipeDiameterDict[double.Parse(Tbx_Pipe_DN.Text)][1].ToString();
            }
            catch 
            {
                Tbx_Pipe_D.Text = "0";
                Tbx_Pipe_WT.Text = "0";
            }
            Tbx_Chg_DxWT.Text = $"D{Tbx_Pipe_D.Text}x{Tbx_Pipe_WT.Text}";
        }
    }
}
