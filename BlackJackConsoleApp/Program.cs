using Interactors.Providers;
using System;
using System.Text;
using System.Collections.Generic;

namespace BlackJackConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> PlayerNames = new List<string>() { "Jeff", "Bryan" };
            //Console.WriteLine("Max of 4 names.");
            //Console.WriteLine("Enter Player Names:");
            //Console.Write("1");
            //string input;
            //for(int i = 0; i < 4; i++) {
            //    input = Console.ReadLine();
            //    if (string.IsNullOrWhiteSpace(input))
            //    {
            //        PlayerNames.Add(input);
            //        Console.Write(i + 1);
            //    }
            //    else break;
            //}

            //foreach (var name in PlayerNames)
            //{
            //    Console.WriteLine(name);
            //}
            //Console.ReadKey();
            
        }

        //public void GetPlayerNames()
        //{
        //    string name = string.Empty;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Console.WriteLine("Enter Player Name:");
        //        name = Console.ReadLine();
        //        if (!string.IsNullOrWhiteSpace(name))
        //        {
        //            break;
        //        }
        //        else PlayerNames.Add(name);
        //    }
        //}

        public string CreateNewBlackJackGame() => new GuidBasedIdentifierProvider().Generate();
    }
}
