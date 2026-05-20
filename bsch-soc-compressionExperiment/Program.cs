using System;
using System.IO;

namespace CompressionExperiment
{
    /// <summary>
    /// Entry point for the compression experiment application.
    /// Menu-driven interface; follow the prompts to run compression experiments.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nCompression Experiment");
            Console.WriteLine("======================");

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("  1. Compress a string");
                Console.WriteLine("  2. Compress a file");
                Console.WriteLine("  3. Exit");
                Console.Write("\nEnter choice: ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        HandleCompressString();
                        break;
                    case "2":
                        HandleCompressFile();
                        break;
                    case "3":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        break;
                }
            }
        }

        static void HandleCompressString()
        {
            Console.WriteLine("\n-- Compress a String --");

            Console.Write("Enter input string: ");
            string input = Console.ReadLine() ?? "";

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input cannot be empty.");
                return;
            }

            Console.Write("Enter a short description for this input (e.g. high-repetition-4char): ");
            string description = Console.ReadLine()?.Trim() ?? "manual-input";

            string resultsFile = PromptPath("Results file path (e.g. results/results.csv): ");
            Directory.CreateDirectory(Path.GetDirectoryName(resultsFile) ?? ".");

            Console.WriteLine();
            var runner = new ExperimentRunner();
            var results = runner.Run(input, description);

            var reporter = new ResultReporter(resultsFile);
            reporter.Write(results);

            Console.WriteLine($"\nResults written to: {resultsFile}");
        }

        static void HandleCompressFile()
        {
            Console.WriteLine("\n-- Compress a File --");

            string inputFile = PromptExistingFile("Input file path: ");

            Console.Write("Enter a short description for this input (e.g. natural-text-gutenberg): ");
            string description = Console.ReadLine()?.Trim() ?? "file-input";

            string resultsFile = PromptPath("Results file path (e.g. results/results.csv): ");
            Directory.CreateDirectory(Path.GetDirectoryName(resultsFile) ?? ".");

            Console.WriteLine();
            var runner = new ExperimentRunner();
            var results = runner.RunFromFile(inputFile, description);

            var reporter = new ResultReporter(resultsFile);
            reporter.Write(results);

            Console.WriteLine($"\nResults written to: {resultsFile}");
        }

        static string PromptPath(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                Console.WriteLine("Please enter a valid file path.");
            }
        }

        static string PromptExistingFile(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine()?.Trim();
                if (File.Exists(input))
                    return input;
                Console.WriteLine($"File not found: {input}. Please try again.");
            }
        }
    }
}