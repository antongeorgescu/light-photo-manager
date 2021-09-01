using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Light_Photo_Manager.Controllers;

namespace Light_Photo_Manager
{
    

    class Program
    {
        static void Main(string[] args)
        {
            switch (args[0].ToUpper())
            {
                case "O":
                    var orgmedia = new OrganizeMediaByYearSeason();
                    orgmedia.Execute();
                    break;
                case "D":
                    var dups = new SelectDuplicatedMedia();
                    dups.QueryDuplicates();
                    break;
                case "D2":
                    var dups2 = new SelectDuplicatedMedia();
                    string[] exts = new string[] { }; 
                    if (args.Length == 1)
                        dups2.QueryDuplicates2(ref exts, false);
                    if ((args.Length > 1) && (args[1] == "show"))
                        dups2.QueryDuplicates2(ref exts);
                    break;
                case "X":
                    var manx = new ManageMediaExtensions();
                    manx.QueryExtensions();
                    break;
            }
            Console.WriteLine("Press any key to close the program...");
            ConsoleKey key = Console.ReadKey().Key;

        }
    }
}
