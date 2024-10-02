using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace SP1
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private int _updateInterval = 1000; 
        private bool _isUpdating = true;

        public MainWindow()
        {
            InitializeComponent(); 
            LoadProcesses(); 
            //SetTimer(); 
        }

        private void LoadProcesses()
        {
            var processes = Process.GetProcesses().Select(p => new
            {
                ProcessName = p.ProcessName,
                Id = p.Id,
            }).ToList();

            grid.ItemsSource = processes; 
        }

        private void SetTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(_updateInterval);
            _timer.Tick += (sender, args) =>
            {
                if (_isUpdating)
                    LoadProcesses();
            };
            _timer.Start();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem != null)
            {
                var selectedProcess = (dynamic)grid.SelectedItem;
                Process.GetProcessById(selectedProcess.Id).Kill();
                LoadProcesses();
            }
            else
            {
                MessageBox.Show("Please select a process to kill.");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem != null)
            {
                var selectedProcess = (dynamic)grid.SelectedItem;
                MessageBox.Show($"Process Name: {selectedProcess.ProcessName}\nPID: {selectedProcess.Id}");
            }
            else
            {
                MessageBox.Show("Please select a process to view information.");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                var processName = nameProcess.Text;
                Process.Start(processName);
                LoadProcesses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting process: " + ex.Message);
            }
        }

        private void timeCB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)timeCB.SelectedItem;
            //_updateInterval = int.Parse(selectedItem.Content.ToString()) * 1000;
            //_timer.Interval = TimeSpan.FromMilliseconds(_updateInterval);
        }

        private void check_Checked(object sender, RoutedEventArgs e)
        {
            //_isUpdating = check.IsChecked ?? false;
        }

        private void timeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }       
    }
}
