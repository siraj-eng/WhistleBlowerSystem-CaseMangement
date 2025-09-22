using System;
using System.Collections.Generic;

namespace whistleblowerConsoleApp
{
    class Program
    {
        static List<Report> Reports = new List<Report>();

        // User-defined Report class
        class Report
        {
            public string Code { get; set; }    // Unique code (string so we can prefix REP)
            public string Details { get; set; } // Report text
        }

        static void Main(string[] args)
        {
            // Welcoming message
            Console.WriteLine("====================================");
            Console.WriteLine("   Welcome to the Whistleblower App  ");
            Console.WriteLine("====================================");
            Console.WriteLine("Report unethical practices securely!");
            Console.WriteLine();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Submit a Report");
                Console.WriteLine("2. View Submitted Reports");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option (1-3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        submitReport();
                        break;
                    case "2":
                        viewReports();
                        break;
                    case "3":
                        running = false;
                        Console.WriteLine("\nExiting the app. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice, please try again.");
                        break;
                }
            }
        }

        // Submit the report
        static void submitReport()
        {
            Console.WriteLine("\n------ Report Submission ---------");
            Console.Write("Explain in detail what happened: ");
            string report = Console.ReadLine();

            // Generate unique code
            Random rnd = new Random();
            string code = "REP" + rnd.Next(1000, 9999);

            Report newReport = new Report
            {
                Code = code,
                Details = report
            };

            Reports.Add(newReport);

            Console.WriteLine($"\n✅ Report submitted successfully! Your unique code is {code}.");
        }

        // View reports
        static void viewReports()
        {
            Console.WriteLine("\n------ Submitted Reports ---------");

            if (Reports.Count == 0)
            {
                Console.WriteLine("No reports have been submitted yet.");
            }
            else
            {
                for (int i = 0; i < Reports.Count; i++)
                {
                    Console.WriteLine($"\nReport #{i + 1}");
                    Console.WriteLine($"Details: {Reports[i].Details}");
                }
            }
        }
    }
}
