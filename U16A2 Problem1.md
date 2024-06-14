# U16A2 Problem 1

## Problem Statement

This problem involves developing a todo list program primarily aimed at showcasing GUI functionality. The GUI can be implemented either as a desktop native app using WPF or through HTML/CSS in the presentation layer. The requirements for this todo list are:

- Creating and deleting tasks.
- Tracking task completion status.
- Supporting editable fields for title, description, and due date.
- Displaying a list of tasks.
- Ability to toggle between displaying all tasks or only incomplete tasks.

## Code review

1. **Using Directives:**
   ```csharp
   using System;
   using System.Collections.ObjectModel;
   using System.Diagnostics;
   using System.Linq;
   using System.Windows;
   using System.Windows.Controls;
   ```
   - These are C# `using` directives that import namespaces into the current file. They allow you to use classes and types defined in these namespaces without fully qualifying their names.

2. **Namespace Declaration:**
   ```csharp
   namespace TaskEditor
   {
       // ...
   }
   ```
   - Defines a namespace `TaskEditor` to encapsulate the classes and types within this file. Namespaces help organize and avoid naming conflicts in large projects.

3. **MainWindow Class:**
   ```csharp
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
       
       // ...
   }
   ```
   - `MainWindow` is a partial class that represents the main window of the application. It inherits from `Window`, which is a standard class in WPF applications for creating windows.
   - `Tasks` is a public property of type `ObservableCollection<TaskItem>`, which stores and manages a collection of `TaskItem` objects. It's bound to the UI element `TaskList` using `TaskList.ItemsSource`.
   - `InitializeComponent()` initializes the UI components defined in the XAML file associated with this window.

4. **Event Handlers:**
   - **AddTask_Click:**
     - Adds a new task to the `Tasks` collection when the user clicks on a button (`AddTask_Click`). It creates a new `TaskItem` object with values from input fields (`TitleBox.Text`, `DescriptionBox.Text`, `DueDatePicker.SelectedDate`), sets `IsCompleted` to `false`, adds it to `Tasks`, and clears input fields.
   
   - **DeleteTask_Click:**
     - Deletes the selected task from `Tasks` when the user clicks a button (`DeleteTask_Click`). It removes the selected `TaskItem` from `Tasks` if `TaskList.SelectedItem` is of type `TaskItem`.

   - **MarkAsComplete_Click:**
     - Marks the selected task as completed when the user clicks a button (`MarkAsComplete_Click`). It sets `IsCompleted` of the selected `TaskItem` to `true` and refreshes the UI (`TaskList.Items.Refresh()`) to reflect the change.

   - **ShowCompletedTasks_Checked:**
     - Shows or hides completed tasks based on the state of a checkbox (`ShowCompletedTasks_Checked`). If the checkbox is checked (`ShowCompletedTasksCheckBox.IsChecked == true`), it shows all tasks; otherwise, it filters `Tasks` to show only incomplete tasks (`Tasks.Where(t => !t.IsCompleted)`).

   - **ClearInputFields:**
     - Clears input fields (`TitleBox`, `DescriptionBox`, `DueDatePicker`) in the UI (`ClearInputFields`). Sets their values to empty (`string.Empty`) or `null`.

5. **TaskItem Class:**
   ```csharp
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
   ```
   - `TaskItem` is a simple class representing a task item with properties for `Title`, `Description`, `DueDate`, and `IsCompleted`.
   - `ToString()` method overrides the default `ToString()` to provide a custom string representation of `TaskItem`, showing its `Title`, `Description`, `DueDate`, and completion status (`Completed` or `Incomplete`).

This code explans a basic implementation of a todo list application using WPF in C#. It includes functionality for adding, deleting, marking tasks as completed, filtering tasks, and managing UI interactions effectively.

## GUI Design + Justifications

![alt text](<Images/Problem1 Evidence/Gui.png>)

Using Affinity Designer, I created a very simple and basic GUI for the application. The GUI consists of a name, description, date, and time. The name and description are text fields, while the date and time are date and time pickers respectively. The GUI is very simple and easy to use, with the date and time pickers being very intuitive. The GUI is also very clean and easy to read, with the text fields being large and easy to read. The GUI is also very responsive, with the text fields and pickers being easy to use on desktop.

I chose to use WPF for the todo list because of its extensive design options for creating the GUI. This enables me to create a more visually appealing interface. Additionally, programming visuals in WPF is simpler to grasp compared to HTML/CSS, which enhances both the design and functionality of the GUI. This approach aims to ensure the todo list is user-friendly, emphasizing excellent usability whilst maintaining performance.

## Test Plan

| Test Case | Description | Steps | Expected Outcome | Result |
|--------------|-------------|-------|-----------------|--------|
| TC1 | Adding a new task | 1. Click on the "Add Task" button.<br>2. Enter valid values in the task fields.<br>3. Click "Add". | The task should appear in the task list displayed in the UI. | Pass |
| TC2 | Deleting a task | 1. Select a task from the task list.<br>2. Click on the "Delete Task" button. | The selected task should be removed from the task list. | Pass |
| TC3 | Marking a task as complete | 1. Select an incomplete task from the task list.<br>2. Click on the "Mark as Complete" button. | The task's status should change to "Completed". | Pass |
| TC4 | Filtering completed tasks | 1. Check the "Show Completed Tasks" checkbox.<br>2. Verify that completed tasks are displayed in the task list. | Only tasks marked as completed should be visible in the task list. | Pass |
| TC5 | Error handling for task deletion | 1. Click on the "Delete Task" button without selecting any task.<br>2. Check for an error message or prompt. | An error message or prompt should inform the user to select a task before deletion. | Pass |
| TC6 | UI update on task completion | 1. Mark a task as complete.<br>2. Observe the task list UI. | The task list should refresh automatically to reflect the updated completion status of the task. | Pass |
| TC7 | Adding a task with no due date | 1. Enter task details without selecting a due date.<br>2. Click "Add". | The task should be added successfully with the current date as the due date. | Pass |
| TC8 | Performance testing | 1. Add a large number of tasks (e.g., 100 tasks).<br>2. Monitor application responsiveness. | The application should remain responsive without significant lag or slowdown. | Pass |

## Log book

**Date** | **Description**
-------------|----------------
02/06        | Reviewed project requirements.


**Date** | **Description**
-------------|----------------
03/06        | **Design Phase**
             | - Defined the problem statement and clarified project requirements.
             | - Created a use case diagram and outlined data structures needed.
             | - Developed initial sketches for the graphical user interface (GUI).

**Date:** 05/06/2024

**Date** | **Description**
-------------|----------------
04/06        | **Development Phase**
             | - Implemented the `MainWindow` class in C# to manage application logic.
             | - Programmed the `AddTask_Click` event handler for adding tasks.
             | - Developed the `DeleteTask_Click` event handler for removing tasks.
             | - Created unit tests to validate basic functionality of the application.


**Date** | **Description**
-------------|----------------
06/06        | **Testing and Debugging**
             | - Executed the test plan to ensure tasks could be added correctly.
             | - Verified input functionality for task names and descriptions.
             | - Tested the selection and deletion of tasks based on due dates.
             | - Addressed and resolved any issues identified during testing.


**Date** | **Description**
-------------|----------------
08/06        | Reviewed test results and debugged any issues found during testing.


**Date** | **Description**
-------------|----------------
11/06        | **Documentation**
             | - Created a comprehensive test plan using Markdown format for future reference.
             | - Summarized the overall functionality of the code in the project documentation.


**Date** | **Description**
-------------|----------------
12/06        | Reviewed and finalized all project documentation to ensure accuracy and completeness.

## Evidence

![alt text](<Images/Problem1 Evidence/withoutcomments/Screenshot 2024-06-13 193427.png>)
![alt text](<Images/Problem1 Evidence/withoutcomments/Screenshot 2024-06-13 193424.png>)
![alt text](<Images/Problem1 Evidence/withoutcomments/Screenshot 2024-06-13 193419.png>)
![alt text](<Images/Problem1 Evidence/withoutcomments/Screenshot 2024-06-13 193415.png>)
![alt text](<Images/Problem1 Evidence/withoutcomments/Screenshot 2024-06-13 193412.png>)
![alt text](<Images/Problem1 Evidence/withoutcomments/Screenshot 2024-06-13 193408.png>)
![alt text](<Images/Problem1 Evidence/withoutcomments/Screenshot 2024-06-13 193404.png>)
![alt text](<Images/Problem1 Evidence/withoutcomments/Screenshot 2024-06-13 193357.png>)