using System;
using System.Linq;

namespace Document_processing_system
{
    // Design Pattern: Template Method Pattern
    // Defines the algorithm structure in base class, lets subclasses override specific steps

    public abstract class DocumentProcessor
    {
        // Template Method: defines the algorithm steps
        public void Process(string filePath)
        {
            OpenFile(filePath);
            ValidateFormat();
            ProcessContent();  // Override in subclasses
            SaveResult();
            LogCompletion();
        }

        // Abstract method - must be implemented by subclasses
        protected abstract void ProcessContent();

        private void OpenFile(string filePath)
        {
            Console.WriteLine($" Opening file: {filePath}");
        }

        private void ValidateFormat()
        {
            Console.WriteLine(" Format validation completed");
        }

        private void SaveResult()
        {
            Console.WriteLine(" Result saved successfully");
        }

        private void LogCompletion()
        {
            Console.WriteLine(" Operation logged\n");
        }
    }

    public class PdfProcessor : DocumentProcessor
    {
        protected override void ProcessContent()
        {
            Console.WriteLine(" PDF Processing: Extracting text content...");
        }
    }

    public class ExcelProcessor : DocumentProcessor
    {
        protected override void ProcessContent()
        {
            Console.WriteLine(" Excel Processing: Summing numbers and calculating average...");
            double[] numbers = { 10, 20, 30, 40, 50 };
            double sum = numbers.Sum();
            double average = numbers.Average();
            Console.WriteLine($"   Sum: {sum}, Average: {average}");
        }
    }

    public class WordProcessor : DocumentProcessor
    {
        protected override void ProcessContent()
        {
            Console.WriteLine(" Word Processing: Counting words...");
            string text = "This is a sample document with multiple words";
            int wordCount = text.Split(' ').Length;
            Console.WriteLine($"   Word count: {wordCount}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Document Processing System ===\n");

            Console.WriteLine("--- Processing PDF ---");
            DocumentProcessor pdfProcessor = new PdfProcessor();
            pdfProcessor.Process("document.pdf");

            Console.WriteLine("--- Processing Excel ---");
            DocumentProcessor excelProcessor = new ExcelProcessor();
            excelProcessor.Process("spreadsheet.xlsx");

            Console.WriteLine("--- Processing Word ---");
            DocumentProcessor wordProcessor = new WordProcessor();
            wordProcessor.Process("document.docx");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
