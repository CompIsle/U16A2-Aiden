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

namespace U16A2Library
{
    class Program
    {
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

        // Interface for code assigners
        public interface ICodeAssigner
        {
            string GenerateHashCode(Book book);
        }

        // Implementation of ICodeAssigner that generates an MD5 hash code
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

        // Book class representing the structure of a book record
        public class Book
        {
            public string Name { get; set; }
            public string Title { get; set; }
            public string Place { get; set; }
            public string Publisher { get; set; }
            public string Date { get; set; }
            public string Hash { get; set; }
        }

        // Mapping configuration for the Book class
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
    }
}
