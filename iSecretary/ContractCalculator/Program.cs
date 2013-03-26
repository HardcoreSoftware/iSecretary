using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using ContractStatisticsAnalyser;
using Data;
using EmailDataMiner;
using UserInterface.ContractRelated;
using UserInterface.InvoiceRelated;

namespace UserInterface
{
    class Program
    {
        private static void Main(string[] args)
        {
            var repo = new Repository();

            if (args.ToList().Contains("-weekly"))
            {
                InvoiceCreationUi.RunAutomatedWeeklyInvoice(repo);
            }
            else
            {
                RunInteractive(repo);
            }
        }

        private static void RunInteractive(Repository repo)
        {
            const string version = "v3.0";

            SetupConsole(version);


            var runAgain = true;

            while (runAgain)
            {
                ShowIntro(version, false);

                var option = InputReceiver.GetOption("Select a task", new List<string>
                    {
                        "View new contract details",
                        "View existing contract details\n",
                        "Create custom invoice",
                        "Create weekly invoice",
                        "Create invoice from XML",
                        "Add invoiceable client\n",
                        "Update agent email address list\n",
                        "View folder: Invoices",
                        "View folder: Mineable Data",
                        "Change Folders\n",
                        "Exit"
                    });

                switch (option)
                {
                    case 0: ContractVisualiser.Visualise(); break;

                    case 1: ContractVisualiser.VisualiseExistingContract(repo); break;

                    case 2: InvoiceCreationUi.CreateCustomInvoiceFromInput(repo); break;

                    case 3: InvoiceCreationUi.CreateWeeklyInvoice(repo); break;

                    case 4: InvoiceCreationUi.CreatedInvoiceFromXml(repo); break;

                    case 5: ClientCreatorUi.AddClient(repo); break;

                    case 6:
                        var badFiles = new List<string>();
                        Extractor.GetEmailAddresses(repo.StorageWrapper.Data.EmailExportDirectory, out badFiles); break;

                    case 7: OperatingDirectoriesUi.ViewInvoiceDirectory(repo); break;

                    case 8: OperatingDirectoriesUi.ViewEmailExports(repo); break;

                    case 9: OperatingDirectoriesUi.SetStorageDirectories(repo); break;

                    case 10: Environment.Exit(0); return; break;
                }

                runAgain = InputReceiver.GetBool("View main menu?");

                Console.WriteLine("");
                Console.WriteLine("");
            }
        }

        private static void ShowIntro(string version, bool showLogo)
        {
            if (showLogo)
            {
                Console.WriteLine(@" _  _   __   ____  ____   ___  __  ____  ____  ");
                Console.WriteLine(@"/ )( \ / _\ (  _ \(    \ / __)/  \(  _ \(  __) ");
                Console.WriteLine(@") __ (/    \ )   / ) D (( (__(  O ))   / ) _)  ");
                Console.WriteLine(@"\_)(_/\_/\_/(__\_)(____/ \___)\__/(__\_)(____) ");
                Console.WriteLine(@" ____   __  ____  ____  _  _   __   ____  ____ ");
                Console.WriteLine(@"/ ___) /  \(  __)(_  _)/ )( \ / _\ (  _ \(  __)");
                Console.WriteLine(@"\___ \(  O )) _)   )(  \ /\ //    \ )   / ) _) ");
                Console.WriteLine(@"(____/ \__/(__)   (__) (_/\_)\_/\_/(__\_)(____)");
                Console.WriteLine("");
            }

            Console.WriteLine("Hello and welcome to iSecretary " + version);
            Console.WriteLine("");
            Console.WriteLine("If you are a software contractor - please get in touch for possible");
            Console.WriteLine("business oppurtunites. Visit: www.hardcoresoftware.co.uk");
            Console.WriteLine("");
        }

        private static void SetupConsole(string version)
        {
            Console.BufferHeight = 1000;
            Console.Title = "Contract Analyser " + version;
            Console.WindowHeight = 32;
        }
    }
}