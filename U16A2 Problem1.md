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

## M2 Design Justifications

![alt text](<Images/Problem1 Evidence/Gui.png>)

The GUI features a simplistic yet clear and aesthetically pleasing design, with prominently sized buttons guiding user interactions and a well-organized task list layout. It successfully meets client requirements for adding, deleting, and filtering tasks. However, a potential area for improvement is the lack of task editing functionality, which could enhance user flexibility in managing tasks post-creation.

### Data Binding for Enhanced Usability

Utilizing data binding in the GUI implementation was crucial. It ensures that changes to task data are automatically reflected in the user interface, reducing manual updates and enhancing overall usability. This approach simplifies interactions by keeping the interface synchronized with underlying data, improving efficiency and user satisfaction.

### Client Enhancements

To further streamline user experience and meet client expectations effectively:

- **Understanding Client Needs**: Continuously gathering feedback on how users interact with the application can provide insights into additional functionalities they might find useful, such as task editing capabilities. This proactive approach ensures that future updates align closely with user expectations and needs.

- **Enhancing Usability**: Implementing intuitive controls and workflows can significantly improve user productivity. For example, incorporating drag-and-drop functionality for task reordering or providing customizable task views could enhance usability and cater to diverse user preferences.

- **Supporting Scalability**: As the application evolves, maintaining scalability is essential. Ensuring that new features integrate seamlessly with existing functionalities minimizes disruption for users and supports long-term usability.

The clear and user-friendly GUI, combined with robust functionality, ensures that the application is both intuitive and dependable. By leveraging interfaces, the application benefits from enhanced flexibility, customization options, and scalability, aligning with evolving user needs and future enhancements. This approach not only meets current client requirements effectively but also lays a foundation for continued growth and adaptability.

In summary, integrating data binding has streamlined user interactions, while a client-centric approach ensures ongoing alignment with user needs and preferences. By focusing on usability enhancements and scalability, the application remains responsive to changes in user requirements, maintaining a positive user experience over time.

## M3 Optimization

- Adam Hurst : The todo list application is very useful and easy to use. However, it would be helpful if you could add comments to the code to make it easier to understand and edit.
- Ugnius Mieldazys : In both of the solutions, the code has blue lines under some of the text.

To optimize the code, I have taken in consideration 2 peers feedback and used their suggestions to improve the code. The first feedback was to add comments to the code to make it easier to understand and edit. The second feedback was to address the warnings in the code.

I addressed the first problem, which was adding comments to my code, it made the code easier to understand and edit.

![alt text](<Images/Problem1 Evidence/Screenshot 2024-06-13 153624.png>)
![alt text](<Images/Problem1 Evidence/Screenshot 2024-06-13 153613.png>)

![Problem 1](<Images/Problem1 Evidence/Screenshot 2024-06-13 153624.png>)

To remove the blue lining text, I added those specific keywords to userspace settings in Visual Studio Code. This will remove the blue lining text from the code.

![without blue text](<Images/blue comment.png>)

Why did I use data binding? Data binding is a powerful feature in WPF that simplifies the process of synchronizing data between the UI and the underlying data source. By using data binding, you can establish a connection between UI elements and data properties, ensuring that changes in one are automatically reflected in the other. This approach enhances the usability of the application by providing real-time updates and reducing the need for manual data manipulation. Negative of data binding is that it can sometimes lead to performance issues, especially when dealing with large data sets or complex bindings. In such cases, it's essential to optimize data binding operations to maintain application responsiveness.

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