using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Text;


namespace WindowsPhone10_v1._0
{
    public sealed partial class MainPage : Page
    {
        ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>(); // ✅ CORRECT TYPE


        public MainPage()
        {
            this.InitializeComponent();
            TaskList.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                tasks.Add(new TaskItem { Name = TaskInput.Text, IsCompleted = false });
                TaskInput.Text = "";
            }
        }


        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var task = button?.Tag as TaskItem;
            if (task != null && tasks.Contains(task))
            {
                tasks.Remove(task);
            }
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var container = (ListViewItem)TaskList.ContainerFromItem(checkbox.Tag);
            if (container != null)
            {
                var textBlock = FindTextBlock(container);
                if (textBlock != null)
                {
                    textBlock.TextDecorations = Windows.UI.Text.TextDecorations.Strikethrough;
                    textBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Gray);
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var container = (ListViewItem)TaskList.ContainerFromItem(checkbox.Tag);
            if (container != null)
            {
                var textBlock = FindTextBlock(container);
                if (textBlock != null)
                {
                    textBlock.TextDecorations = Windows.UI.Text.TextDecorations.None;
                    textBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                }
            }
        }



        private TextBlock FindTextBlock(DependencyObject parent)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is TextBlock tb && tb.Name == "TaskText")
                    return tb;

                var result = FindTextBlock(child);
                if (result != null)
                    return result;
            }
            return null;
        }


        private void Task_Checked(object sender, RoutedEventArgs e)
        {
            // Optional: You can add completed logic here
        }
    }
}
