using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Program
{
    static void Main(string[] args)
    {
        string input = Console.ReadLine();
        DraftManager dm = new DraftManager();
        string command = "asdf";
        while (command != "Shutdown")
        {
            string[] tokens = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            List<string> argus = tokens.ToList();
            argus = argus.Skip(1).ToList();

            //dm.arguments = tokens.ToList();
            switch (tokens[0])
            {
                case "RegisterHarvester":
                    Console.WriteLine(dm.RegisterHarvester(argus)); 
                    break;
                case "RegisterProvider":
                    Console.WriteLine(dm.RegisterProvider(argus)); 
                    break;
                case "Day":
                    Console.WriteLine(dm.Day());
                    break;
                case "Mode":
                    Console.WriteLine(dm.Mode(argus));
                    break;
                case "Check":
                    Console.WriteLine(dm.Check(argus));
                    break;
                case "Shutdown":
                    Console.WriteLine(dm.ShutDown());
                    command = "Shutdown";
                    break;
                default:
                    break;
            }
            input = Console.ReadLine();
        }
    }
}


