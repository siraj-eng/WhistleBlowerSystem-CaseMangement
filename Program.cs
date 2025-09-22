using System;
using System.Collections.Generic;

namespace whistleblowerConsoleApp
{
    class Program
    {
        static List<Report> Reports = new List<Report>();
        static int ReportId = 0;

        class Report
        {
            public int Code {  get; set; }
            public string Details { get; set; }
             
        }

        static void Main(string[] args)
        {
            // Welcoming message
            Console.WriteLine("====================================");
            Console.WriteLine("   Welcome to the Whistleblower App  ");
            Console.WriteLine("====================================");
            Console.WriteLine("Report unethical practices securely!");
            Console.WriteLine();

            submitReport(); 
            ViewReport();
        }

        // Submit the report
        static void submitReport()
        {
            Console.WriteLine("\n------ Report Submission ---------");
            Console.Write("\nExplain in detail what happened: ");
            string report = Console.ReadLine();

            Random rnd = new Random();
            string code = "REP" + rnd.Next(1000, 9999);
            
            Report NewReport = new Report();
            NewReport.Code = code;
            NewReport.Details = report;

            Reports.Add(NewReport);

            Console.WriteLine($"You have successfully submitted a report and your unique code is{code}.");
        }

        //View Report
        static void ViewReport()
        {
            if (Reports.Count > 0)
                Console.WriteLine("No reports yet");
            else
            {
                for (var i = 0; i < Reports.Count; i++)
                {
                    Console.WriteLine("\n-------All Reports---------");
                    Console.WriteLine($"Reports {i + 1} {Reports[i]}");
                }
            }
        }
    }
}
