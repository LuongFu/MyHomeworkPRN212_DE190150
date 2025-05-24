// A utility to analyze text files and provide statistics
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");
            
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }
            
            string filePath = args[0];
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }
            
            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");
                
                // Read the file content
                string content = File.ReadAllText(filePath);

                // TODO: Implement analysis functionality
                // 1. Count words
                var words = Regex.Matches(content.ToLower(), @"\b[\w']+\b")
                                 .Cast<Match>()
                                 .Select(m => m.Value)
                                 .ToList();
                int wordCount = words.Count;

                // 2. Count characters
                int charCountWithSpaces = content.Length;
                int charCountWithoutSpaces = content.Count(c => !char.IsWhiteSpace(c));

                // 3. Count sentences
                int sentenceCount = Regex.Matches(content, @"[.!?]+").Count;

                // 4. Most common words
                var commonWords = words
                    .GroupBy(w => w)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => $"{g.Key} ({g.Count()})");

                // 5. Average word length
                double avgWordLength = wordCount > 0
                    ? words.Average(w => w.Length)
                    : 0;

                // Example implementation for counting lines:
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines: {lineCount}");

                // TODO: Additional analysis to be implemented

                // Output results
                Console.WriteLine($"Number of words: {wordCount}");
                Console.WriteLine($"Number of characters (with spaces): {charCountWithSpaces}");
                Console.WriteLine($"Number of characters (without spaces): {charCountWithoutSpaces}");
                Console.WriteLine($"Number of sentences: {sentenceCount}");
                Console.WriteLine($"Most common words: {string.Join(", ", commonWords)}");
                Console.WriteLine($"Average word length: {avgWordLength:F2} characters");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}