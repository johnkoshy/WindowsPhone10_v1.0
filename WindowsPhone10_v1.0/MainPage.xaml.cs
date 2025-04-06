using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WindowsPhone10_v1._0
{
    public sealed partial class MainPage : Page
    {
        ObservableCollection<string> tasks = new ObservableCollection<string>();

        public MainPage()
        {
            this.InitializeComponent();
            TaskList.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                tasks.Add(TaskInput.Text);
                TaskInput.Text = "";
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var task = button?.Tag as string;
            if (task != null && tasks.Contains(task))
            {
                tasks.Remove(task);
            }
        }

        private void Task_Checked(object sender, RoutedEventArgs e)
        {
            // Optional: You can add completed logic here
        }
    }
}
