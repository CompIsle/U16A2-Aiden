using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TaskEditor
{
    public partial class MainWindow : Window
    {
        // Collection of TaskItem objects that will be displayed in the UI
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent(); // Initializes the UI components
            Tasks = new ObservableCollection<TaskItem>(); // Initializes the Tasks collection
            TaskList.ItemsSource = Tasks; // Binds the Tasks collection to the TaskList UI element
        }

        // Event handler for adding a new task
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            // Creates a new TaskItem with input values
            var newTask = new TaskItem
            {
                Title = TitleBox.Text,
                Description = DescriptionBox.Text,
                DueDate = DueDatePicker.SelectedDate ?? DateTime.Now,
                IsCompleted = false
            };
            Tasks.Add(newTask); // Adds the new task to the Tasks collection
            ClearInputFields(); // Clears the input fields for the next entry
        }

        // Event handler for deleting a selected task
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem selectedTask) // Checks if a task is selected
            {
                Tasks.Remove(selectedTask); // Removes the selected task from the Tasks collection
            }
        }

        // Event handler for marking a selected task as completed
        private void MarkAsComplete_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem selectedTask) // Checks if a task is selected
            {
                selectedTask.IsCompleted = true; // Marks the task as completed
                TaskList.Items.Refresh(); // Refreshes the TaskList UI to reflect changes
            }
        }

        // Event handler for showing or hiding completed tasks
        private void ShowCompletedTasks_Checked(object sender, RoutedEventArgs e)
        {
            if (ShowCompletedTasksCheckBox.IsChecked == true) // Checks if the checkbox is checked
            {
                TaskList.ItemsSource = Tasks; // Shows all tasks
            }
            else
            {
                TaskList.ItemsSource = Tasks.Where(t => !t.IsCompleted); // Shows only incomplete tasks
            }
        }

        // Clears the input fields in the UI
        private void ClearInputFields()
        {
            TitleBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;
            DueDatePicker.SelectedDate = null;
        }
    }

    // Represents a task item with properties for title, description, due date, and completion status
    public class TaskItem
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        // Returns a string representation of the TaskItem
        public override string ToString()
        {
            return $"{Title} - {Description} (Due: {DueDate.ToShortDateString()}) - {(IsCompleted ? "Completed" : "Incomplete")}";
        }
    }
}
