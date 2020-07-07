
using System;
using dotNetCoreExample.Examples;
using Newtonsoft.Json;

namespace dotNetCoreExample
{
    public class Program
    {

        public static void Main()
        {
            DisplayTheMenu();

            var quit = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter a number (or QUIT to exit):");

                var selection = Console.ReadLine();
                if (string.IsNullOrEmpty(selection))
                    continue;

                quit = selection.ToLower().Trim() == "quit";
                if (quit)
                    continue;

                var exampleClassName = GetExampleName(selection);
                if(exampleClassName == null)
                    continue;

                var type = Type.GetType(exampleClassName);
                var obj = (IExample)Activator.CreateInstance(type);
                Execute(obj);

            } while (quit == false);
        }

        private static void DisplayTheMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Please select from one of the following code examples:");
            Console.WriteLine("NOTE:  Please update the ExampleConfig.cs file with your credentials");
            Console.WriteLine();
            Console.WriteLine(" Basic Send Examples: ");
            Console.WriteLine("    1:  Basic Send ");
            Console.WriteLine("    2:  Basic Send From Html File ");
            Console.WriteLine("    3:  Basic Send With Api Template ");
            Console.WriteLine("    4:  Basic Send With Ascii Charset ");
            Console.WriteLine("    5:  Basic Send With Attachment ");
            Console.WriteLine("    6:  Basic Send With Custom-Headers ");
            Console.WriteLine("    7:  Basic Send With Embedded Image ");
            Console.WriteLine("    8:  Basic Send With Proxy ");
            Console.WriteLine("    9:  Basic Send Complex Example ");
            Console.WriteLine();
            Console.WriteLine(" Validation Error Handling Examples: ");
            Console.WriteLine("   10:  Basic Send With Invalid Attachment");
            Console.WriteLine("   11:  Basic Send With Invalid From ");
            Console.WriteLine("   12:  Basic Send With Invalid Recipients ");
            Console.WriteLine();
            Console.WriteLine(" Bulk Send Examples: ");
            Console.WriteLine("   13:  Bulk Send ");
            Console.WriteLine("   14:  Bulk Send With MergeData ");
            Console.WriteLine("   15:  Bulk Send With Ascii Charset And MergeData ");
            Console.WriteLine("   16:  Bulk Send From DataSource With MergeData ");
            Console.WriteLine("   17:  Bulk Send Complex Example (Everything including the Kitchen Sink) ");
            Console.WriteLine();
            Console.WriteLine(" Amp Examples: ");
            Console.WriteLine("   18:  Basic Send With Amp Body ");
            Console.WriteLine("   19:  Bulk Send With Amp Body ");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
        }

        private static string GetExampleName(string selection)
        {
            if (!int.TryParse(selection, out var menuItem))
            {
                Console.WriteLine("Invalid Input (Not an integer)");
                return null;
            }

            switch (menuItem)
            {
                case 1: return "dotNetCoreExample.Examples.Basic.BasicSend";
                case 2: return "dotNetCoreExample.Examples.Basic.BasicSendFromHtmlFile";
                case 3: return "dotNetCoreExample.Examples.Basic.BasicSendWithApiTemplate";
                case 4: return "dotNetCoreExample.Examples.Basic.BasicSendWithAsciiCharset";
                case 5: return "dotNetCoreExample.Examples.Basic.BasicSendWithAttachment";
                case 6: return "dotNetCoreExample.Examples.Basic.BasicSendWithCustomHeaders";
                case 7: return "dotNetCoreExample.Examples.Basic.BasicSendWithEmbeddedImage";
                case 8: return "dotNetCoreExample.Examples.Basic.BasicSendWithProxy";
                case 9: return "dotNetCoreExample.Examples.Basic.BasicComplexExample";
                case 10: return "dotNetCoreExample.Examples.Basic.Invalid.BasicSendWithInvalidAttachment";
                case 11: return "dotNetCoreExample.Examples.Basic.Invalid.BasicSendWithInvalidFrom";
                case 12: return "dotNetCoreExample.Examples.Basic.Invalid.BasicSendWithInvalidRecipients";
                case 13: return "dotNetCoreExample.Examples.Bulk.BulkSend";
                case 14: return "dotNetCoreExample.Examples.Bulk.BulkSendWithMergeData";
                case 15: return "dotNetCoreExample.Examples.Bulk.BulkSendWithAsciiCharsetMergeData";
                case 16: return "dotNetCoreExample.Examples.Bulk.BulkSendFromDataSourceWithMerge";
                case 17: return "dotNetCoreExample.Examples.Bulk.BulkSendComplexExample";
                case 18: return "dotNetCoreExample.Examples.Basic.BasicSendWithAmpBody";
                case 19: return "dotNetCoreExample.Examples.Bulk.BulkSendWithAmpBody";

                default:
                    Console.WriteLine("Invalid Input (Out of Range)");
                    return null;
            }
        }

        private static void Execute(IExample example)
        {
            try
            {
                var response = example.RunExample();
                Console.WriteLine(response);
                Console.WriteLine($"JSON: {JsonConvert.SerializeObject(response, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
