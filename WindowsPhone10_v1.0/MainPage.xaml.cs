using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.IO;
using Newtonsoft.Json;
using Windows.UI.Xaml.Navigation;

namespace WindowsPhone10_v1._0
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
        private readonly string fileName = "tasks.json";

        public MainPage()
        {
            this.InitializeComponent();
            TaskList.ItemsSource = tasks;
            LoadTasksFromLocal();

        }

        private async void SaveTasksAsync()
        {
            var json = JsonConvert.SerializeObject(tasks);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("tasks.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);
        }

        private async void LoadTasksFromLocal()
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("tasks.json");
                var json = await FileIO.ReadTextAsync(file);
                var loadedTasks = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(json);

                if (loadedTasks != null)
                {
                    tasks = loadedTasks;
                    TaskList.ItemsSource = tasks;
                }
            }
            catch (FileNotFoundException)
            {
                // No saved tasks yet — ignore
            }
        }


        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                tasks.Add(new TaskItem { Name = TaskInput.Text.Trim(), IsCompleted = false });
                TaskInput.Text = string.Empty;
                SaveTasksAsync();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.Tag is TaskItem task)
            {
                tasks.Remove(task);
                SaveTasksAsync();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SaveTasksAsync();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SaveTasksAsync();
        }
    }
}
