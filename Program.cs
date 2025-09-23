using System.ComponentModel;
using System.Text.Json;

namespace whistleblowerConsoleApp
{
    class Program
    {
        static List<Report> Reports = new List<Report>();

        static string filepath = "reports.json";

        // User-defined Report class
        class Report
        {
            public string Code { get; set; }    // Unique code (string so we can prefix REP)
            public string Details { get; set; } // Report text
            public string Assignedto { get; set; } //Person investigating the report
            public string Status { get; set; } // Investigator to update the status
            public string Notes { get; set; } //Notes from Hr and audit officer
        }

        static void Main(string[] args)
        {
            //Load Existing Reports
            LoadReports();

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
                Console.WriteLine("3. Check your reports and track");
                Console.WriteLine("4.Compliance team");
                Console.WriteLine("5. Exit");
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
                        CheckMyreport();
                        break;
                    case "4":
                        ComplianceTeam();
                        break;
                    case "5":
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
            SaveReports();

            Console.WriteLine($"\n✅ Report submitted successfully! Your unique code is {code}.");
        }

        // View reports
        static void viewReports()
        {
            Console.WriteLine("\n------ Submitted Reports ---------");

            string password = "admin";

            Console.Write("Enter the password: ");
            string input = Console.ReadLine();

            if (input == password)
            {
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
                        Console.WriteLine($"Assigned to: {Reports[i].Assignedto}");
                        SaveReports();
                    }
                }
            }
            else
            {
                return;
            }
        }

        static void SaveReports()
        {
            string json = JsonSerializer.Serialize(Reports, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filepath, json);
        }

        static void LoadReports()
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                Reports = JsonSerializer.Deserialize<List<Report>>(json) ?? new List<Report>();
            }
        }

        static void CheckMyreport()
        {
            Console.Write("\nEnter the code to view your report: ");
            string entry = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entry))
                return;

            // Trim spaces and ignore case
            entry = entry.Trim();

            var report = Reports.Find(r => string.Equals(r.Code?.Trim(), entry, StringComparison.OrdinalIgnoreCase));

            if (report != null)
            {
                Console.WriteLine("\nHere is your Report:");
                Console.WriteLine($"Code: {report.Code}, Details: {report.Details}, Assigned To: {report.Assignedto}");
            }
            else
            {
                Console.WriteLine("No such Report exists!!!!!!");
            }

        }

        static void ComplianceTeam()
        {
            var password = "Compliance-Team";

            Console.Write("Enter your password: ");
            string input = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(input))
                return;

            if (password == input)
            {
                Console.WriteLine("\n----Welcome to the Compliance team------");
                Console.WriteLine("Select your role");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Compliance officer");
                Console.WriteLine("3.Investigator");
                Console.WriteLine("4.HR/Legal");

                Console.Write("Enter your choice(1-4) ");
                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\n Logged in as Admin. You have full system access");
                        viewReports();
                        break;
                    case "2":
                        Console.WriteLine("\n Logged in as a Compliance officer. You can manage cases.");
                        ShowReportsToCompliance();
                        AssignReports();
                        break;
                    case "3":
                        Console.WriteLine("\n Logged in as investigator. You can investigate assigned reports");
                        UpdateReportstatus();
                        break;
                    case "4":
                        Console.WriteLine("\n Logged in as HR/Legal. You can handle employee-related issues.");
                        HrAudit();
                        break;
                    default:
                        Console.WriteLine("\n Invalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\n Access denied. Wrong password.");

            }
        }

        static void ShowReportsToCompliance()
        {
            Console.WriteLine("----- Reports for Compliance Team -----");

            for (int i = 0; i < Reports.Count; i++)
            {
                var report = Reports[i];
                Console.WriteLine($"{i + 1}. {report.Details} " +
                                  $"{(string.IsNullOrWhiteSpace(report.Assignedto) ? "(Unassigned)" : $"Assigned to: {report.Assignedto}")}");
                SaveReports();
            }

            Console.WriteLine("--------------------------------------");
        }

        static void AssignReports()
        {
            for (int i = 0; i < Reports.Count; i++)
            {
                var reports = Reports[i];

                if (String.IsNullOrWhiteSpace(reports.Assignedto))
                {
                    Console.WriteLine($"Enter the investigators name for the Report {reports.Details}");
                    reports.Assignedto = Console.ReadLine();
                    SaveReports();
                }
            }
        }

        static void UpdateReportstatus()
        {

            Console.Write("\nEnter your Password: ");
            string input = Console.ReadLine();

            var password = "investigator";

            if (String.IsNullOrWhiteSpace(input))
                return;

            if (password == input)
            {

                Console.Write("\n-------Hey...!Welcome You can now successfully update the report status-------");

                for (int i = 0; i < Reports.Count; i++)
                {
                    var report = Reports[i];

                    Console.WriteLine($"\nReport Code: {report.Code}");
                    Console.WriteLine($"Details: {report.Details}");
                    Console.WriteLine($"Current Status: {(String.IsNullOrWhiteSpace(report.Status) ? "No status yet" : report.Status)}");

                    Console.WriteLine("Do you want to update this status (y/n)");
                    string choice = Console.ReadLine()?.Trim().ToLower();

                    if(choice == "y")
                    {
                        Console.Write("\n Plz update the status: ");
                        report.Status = Console.ReadLine();
                        Console.WriteLine($"Status updated to: {report.Status}");
                    }
                    else
                    {
                        Console.WriteLine("Skipped updating this report.");
                    }
                }
                SaveReports();
            }
            else
            {
                Console.WriteLine("Wrong Password Access Denied!!!");
            }
        }

        static void HrAudit()
        {
            Console.WriteLine("\nEnter your password!");
            string input = Console.ReadLine();

            var password = "hr";

            if (String.IsNullOrWhiteSpace(input))
                return;

            if (password == input)
            {
                Console.WriteLine("\n-----Reports available for HR review------");

                for (int i = 0; i < Reports.Count; i++)
                {
                    var report = Reports[i];

                    Console.WriteLine($"\n Report Code   : {report.Code}");
                    Console.WriteLine($" Report Details: {report.Details}");
                    Console.WriteLine($" Report Status : {report.Status}");
                    Console.WriteLine($" Current Notes : {(String.IsNullOrWhiteSpace(report.Notes) ? "No notes added yet" : report.Notes)}");

                    Console.Write("\nDo you wish to add or update notes? (y/n): ");
                    string choice = Console.ReadLine().Trim().ToLower();

                    if (choice == "y")
                    {
                        Console.WriteLine("Enter your final remarks:");
                        string newNotes = Console.ReadLine();

                        // If HR already had notes, append instead of overwriting
                        if (!String.IsNullOrWhiteSpace(report.Notes))
                            report.Notes += "\n[Update]: " + newNotes;
                        else
                            report.Notes = newNotes;

                        Console.WriteLine($"✔ You have successfully updated report {report.Code}");
                    }
                    else
                    {
                        Console.WriteLine(">>> Skipped notes update for this report");
                    }
                }
            }
            else
            {
                Console.WriteLine("❌ Wrong Password. Access denied!");
            }

            SaveReports();
        }
    }
}
