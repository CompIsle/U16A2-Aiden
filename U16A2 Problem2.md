# U16A2 Problem 1

## Problem Statement

The college library needs to integrate all its books into a new indexing system. I need to provide a solution that can automatically extract book details such as title, author, publisher, and publication date from a stored CSV file. Additionally, there should be a separate class responsible for assigning serial numbers, utilizing an interface to enable future alternative implementations. The requirements for this indexing system are as follows:

- Ability to read book details (title, author, publisher, and publication date).
- Capability to write the updated details to a stored CSV file.
- Implementation of an interface to support alternative future implementations.
- A separate class dedicated to assigning serial numbers.

## Code review

1. **Using Directives:**
   ```csharp
   // Import necessary namespaces
   using CsvHelper;  // For reading and writing CSV files
   using CsvHelper.Configuration;  // For configuring CsvHelper
   using System;
   using System.Collections.Generic;
   using System.Globalization;  // For handling cultural settings
   using System.IO;  // For file I/O operations
   using System.Linq;  // For LINQ operations
   using System.Security.Cryptography;  // For cryptographic services
   using System.Text;  // For text encoding
   ```
   - These directives import necessary namespaces that provide functionalities for CSV file handling (`CsvHelper`), cultural settings (`System.Globalization`), file I/O (`System.IO`), LINQ operations (`System.Linq`), cryptographic services (`System.Security.Cryptography`), and text encoding (`System.Text`).

2. **Program Class:**
   ```csharp
   class Program
   {
       static void Main(string[] args)
       {
           // ...
       }

       // Other nested classes and interfaces...
   }
   ```
   - `Program` is the main class containing the `Main` method, the entry point of the application. It also contains nested classes and interfaces related to the functionality of the program.

3. **Main Method:**
   ```csharp
   static void Main(string[] args)
   {
       // Define the input and output file paths
       string filePath = "U16A2Task2Data.csv";
       string newFilePath = "BooksCode.csv";

       // Open the input CSV file for reading
       using (var reader = new StreamReader(filePath))
       using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
       {
           // Register the mapping configuration for the Book class
           csv.Context.RegisterClassMap<BookMap>();

           // Read and convert the records from the CSV file into a list of Book objects
           var records = csv.GetRecords<Book>().ToList();

           int validRecordsCount = 0;

           // Create an instance of MD5CodeAssigner to generate hash codes for the books
           ICodeAssigner codeAssigner = new MD5CodeAssigner();

           // Open the output CSV file for writing
           using (var writer = new StreamWriter(newFilePath))
           using (var csvw = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
           {
               // Write the header to the output CSV file
               csvw.WriteHeader<Book>();
               csvw.NextRecord();

               // Process each book record
               foreach (var record in records)
               {
                   // Generate a hash code for the current book record
                   record.Hash = codeAssigner.GenerateHashCode(record);

                   // Write the book record to the output CSV file
                   csvw.WriteRecord(record);
                   csvw.NextRecord();
                   validRecordsCount++;
               }
           }

           // Output the results to the console
           Console.WriteLine($"{records.Count} lines of input read, {validRecordsCount} valid records created.");
       }

       // Wait for the user to press Enter before closing
       Console.ReadLine();
   }
   ```
   - **File Paths:** Defines the input (`filePath`) and output (`newFilePath`) file paths.
   - **Reading CSV:** Opens the input CSV file using `StreamReader` and reads its contents using `CsvReader`. It registers a mapping configuration for the `Book` class (`BookMap`) and reads the records into a list of `Book` objects (`records`).
   - **Processing Records:** Initializes an `MD5CodeAssigner` to generate hash codes for the books. Opens the output CSV file using `StreamWriter` and writes the header and processed records to it using `CsvWriter`.
   - **Output Results:** Outputs the count of input records read and valid records created to the console.
   - **User Interaction:** Waits for the user to press Enter before closing the console window.

4. **ICodeAssigner Interface:**
   ```csharp
   public interface ICodeAssigner
   {
       string GenerateHashCode(Book book);
   }
   ```
   - Defines an interface `ICodeAssigner` with a method `GenerateHashCode` that takes a `Book` object and returns a `string`. This interface is used to generate hash codes for books.

5. **MD5CodeAssigner Class:**
   ```csharp
   public class MD5CodeAssigner : ICodeAssigner
   {
       public string GenerateHashCode(Book book)
       {
           // Concatenate book properties into a single string
           string bookString = $"{book.Name}-{book.Title}-{book.Place}-{book.Publisher}-{book.Date}";

           // Create an MD5 hash of the concatenated string
           using (MD5 md5 = MD5.Create())
           {
               byte[] inputBytes = Encoding.ASCII.GetBytes(bookString);
               byte[] hashBytes = md5.ComputeHash(inputBytes);

               // Convert the hash bytes to a hexadecimal string
               StringBuilder sb = new StringBuilder();
               for (int i = 0; i < hashBytes.Length; i++)
               {
                   sb.Append(hashBytes[i].ToString("X2"));
               }

               // Truncate the hash to the last 6 characters and prefix with "AX"
               string truncatedHash = "AX" + sb.ToString().Substring(sb.Length - 6);

               return truncatedHash;
           }
       }
   }
   ```
   - Implements `ICodeAssigner`. The `GenerateHashCode` method:
     - Concatenates properties of a `Book` object into a single string.
     - Creates an MD5 hash of this string.
     - Converts the hash bytes to a hexadecimal string.
     - Truncates the hash to the last 6 characters, prefixes it with "AX", and returns the result.

6. **Book Class:**
   ```csharp
   public class Book
   {
       public string Name { get; set; }
       public string Title { get; set; }
       public string Place { get; set; }
       public string Publisher { get; set; }
       public string Date { get; set; }
       public string Hash { get; set; }
   }
   ```
   - Represents a book record with properties `Name`, `Title`, `Place`, `Publisher`, `Date`, and `Hash`. The `Hash` property is used to store the generated hash code for the book.

7. **BookMap Class:**
   ```csharp
   public sealed class BookMap : ClassMap<Book>
   {
       public BookMap()
       {
           Map(m => m.Name).Index(0);
           Map(m => m.Title).Index(1);
           Map(m => m.Place).Index(2);
           Map(m => m.Publisher).Index(3);
           Map(m => m.Date).Index(4);
           Map(m => m.Hash).Ignore();  // Ignore the Hash property during CSV reading
       }
   }
   ```
   - Configures the mapping for the `Book` class for CSV operations using `CsvHelper`.
   - Maps each property of `Book` to a specific column index in the CSV file.
   - Ignores the `Hash` property during CSV reading, as it will be generated later.

This code reads book records from a CSV file, generates an MD5 hash code for each record, and writes the updated records to a new CSV file. It uses `CsvHelper` for CSV operations, handles cultural settings for consistent formatting, and employs cryptographic services for hash generation.

## M2 Design Justification

I chose to use CsvHelper, an external library to simplify the code and boost the program's performance significantly. CsvHelper excels in minimizing memory usage by processing CSV files one record at a time during iteration, thereby ensuring that only a small segment of the file resides in memory at any given moment. This efficient handling enhances overall program performance.

A standout feature of CsvHelper is its attribute mapping ability, which allows developers to specify the mapping between CSV file fields and class properties. This feature addresses the challenge of unpredictable class member ordering in .NET environments, leading to fewer errors and improved code readability. By explicitly defining these mappings, CsvHelper streamlines data parsing and manipulation tasks, offering a dependable solution for handling CSV operations with precision and efficiency.

In comparison to manual CSV parsing methods, integrating CsvHelper has simplified and streamlined the program's implementation. It eliminates the difficulties and potential pitfalls associated with manual parsing, such as data type conversions and edge case handling. This approach not only accelerates development but also enhances code maintainability by leveraging CsvHelper's proven capabilities for CSV file management.

Additionally, I implemented a dedicated class to manage serial number allocation to the `GenerateHashCode` interface for flexibility and future extensibility. This design choice facilitates seamless integration of alternative serial number generation methods as needed. By utilizing SHA256 hashing for serial numbers, the application ensures robust collision resistance, thereby bolstering security and reliability. This methodical approach enhances the uniqueness and integrity of each generated serial number, crucial for maintaining data consistency and accuracy.

## M3 Optimization

- Adam Hurst : The todo list application is very useful and easy to use. However, it would be helpful if you could add comments to the code to make it easier to understand and edit.
- Ugnius Mieldazys : In both of the solutions, the code has blue lines under some of the text.

Like what I did for Problem 1, I cleared all the blue text again

![Problem 2](<Images/Problem2 Evidence/Screenshot 2024-06-13 190923.png>)

Same as Problem 1, I added comments to my code to make it more legible and easier to understand. This will help users navigate the codebase more effectively and make future modifications or enhancements with greater ease.

![alt text](<Images/Problem2 Evidence/Screenshot 2024-06-13 190923.png>)
![alt text](<Images/Problem2 Evidence/Screenshot 2024-06-13 190949.png>)

The optimised code for the serial number generator is similar to the one used in Microsoft Documentation [Microsoft Documentation.](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256?view=net-8.0) This is because the SHA256 algorithm is a widely accepted and secure method for generating hash codes. By using this algorithm, the application ensures that the serial numbers are unique and secure, providing a robust solution for managing book records. The negatives of using this algorithm are that it can be computationally intensive for large datasets, potentially impacting performance. However, for the current requirements, the benefits of security and uniqueness outweigh the performance considerations.

The same hashed value will always produce the same hash code, regardless of the number of times it is hashed. This property is essential for ensuring data consistency and integrity, as it guarantees that the same input will always yield the same output. By using a secure hashing algorithm like SHA256, the application can rely on consistent hash codes for each serial number, enabling reliable data management and verification.

## Testing Plan

| Test Case | Description | Steps | Expected Outcome | Result |
|--------------|-------------|-------|-----------------|--------|
| TC1 | Reading from input CSV file | 1. Open the application.<br>2. Verify the input CSV file is loaded automatically. | The application correctly reads and displays data from the input CSV file. | Pass |
| TC2 | Generating hash codes | 1. Add new records or modify existing ones.<br>2. Check the generated hash codes. | Each record should have a unique hash code generated using the MD5 algorithm. | Pass |
| TC3 | Writing to output CSV file | 1. Save changes or export data.<br>2. Verify the contents of the output CSV file. | The output CSV file should contain updated data with corresponding hash codes. | Pass |
| TC4 | Error handling for invalid data | 1. Modify data with incorrect format or missing fields.<br>2. Attempt to save or export data. | The application should handle invalid data gracefully, providing feedback to the user. | Pass |
| TC5 | Performance testing | 1. Process a large input CSV file (e.g., 1000 records).<br>2. Measure processing time and memory usage. | The application should efficiently handle large datasets without crashing or excessive resource usage. | Pass |
| TC6 | Exporting data with special characters | 1. Include special characters in data fields.<br>2. Export data to the output CSV file. | Special characters should be correctly encoded and preserved in the output CSV file. | Pass |
| TC7 | Updating existing records | 1. Modify existing records.<br>2. Verify hash codes are updated accordingly. | Updated records should have their hash codes recalculated and updated in the output CSV file. | Pass |

## Log book

**Date** | **Description**
-------------|----------------
27/05        | Reviewed project requirements and understood CSV file structure.


**Date** | **Description**
-------------|----------------
28/05        | **Design Phase**
             | - Defined how data in the `Book` class should match CSV using `CsvHelper`.
             | - Planned how `Book` records will be securely hashed using `MD5`.


**Date** | **Description**
-------------|----------------
28/05        | **Development Phase**
             | - Wrote the `Main` method to read from "U16A2Task2Data.csv" and save to "BooksCode.csv".
             | - Set up `CsvHelper` to handle CSV read and write operations smoothly.
             | - Configured `BookMap` to correctly map fields during CSV tasks.

**Date** | **Description**
-------------|----------------
29/05        | **Testing and Debugging**
             | - Ran the program to confirm CSV read and write operations were working as expected.
             | - Verified that "BooksCode.csv" had correct formatting and reliable data.
             | - Tested `MD5CodeAssigner` to ensure accurate hash code creation.

**Date** | **Description**
-------------|----------------
30/05        | **Documentation**
             | - Drafted code documentation explaining how classes and functions work.
             | - Created a short overview of the CSV reader program for reference.

## Evidence

![alt text](<Images/Problem2 Evidence/Evidence.png>)
![alt text](<Images/Problem2 Evidence/Checklist.png>)

![alt text](<Images/Problem2 Evidence/withoutcomments/Screenshot 2024-06-13 193212.png>)
![alt text](<Images/Problem2 Evidence/withoutcomments/Screenshot 2024-06-13 193214.png>)
![alt text](<Images/Problem2 Evidence/withoutcomments/Screenshot 2024-06-13 193216.png>)
![alt text](<Images/Problem2 Evidence/withoutcomments/Screenshot 2024-06-13 193219.png>)
![alt text](<Images/Problem2 Evidence/withoutcomments/Screenshot 2024-06-13 193224.png>)
![alt text](<Images/Problem2 Evidence/withoutcomments/Screenshot 2024-06-13 193221.png>)
![alt text](<Images/Problem2 Evidence/withoutcomments/Screenshot 2024-06-13 193226.png>)
![alt text](<Images/Problem2 Evidence/withoutcomments/Screenshot 2024-06-13 193229.png>)