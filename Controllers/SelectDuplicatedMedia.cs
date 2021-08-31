using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Light_Photo_Manager.Helpers;
using Newtonsoft.Json;

namespace Light_Photo_Manager.Controllers
{
    internal class SelectDuplicatedMedia
    {
        RootObject setts = null;

        public SelectDuplicatedMedia()
        {
            ReadConfiguration config = new ReadConfiguration();
            setts = config.GetPaths();
        }

        internal void QueryDuplicates()
        {
            if (setts == null)
                return;

            // Change the root drive or folder if necessary 
            var rootFolder = setts.Paths.ToList<Light_Photo_Manager.Helpers.Path>().Find(x => x.Type == "dedupe");
            string startFolder = rootFolder.Dir; // @"c:\program files\Microsoft Visual Studio 9.0\";

            // Take a snapshot of the file system.  
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);

            // This method assumes that the application has discovery permissions  
            // for all folders under the specified path.  
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            // used in WriteLine to keep the lines shorter  
            int charsToSkip = startFolder.Length;

            // var can be used for convenience with groups.  
            var queryDupNames =
                from file in fileList
                group file.FullName.Substring(charsToSkip) by file.Name into fileGroup
                where fileGroup.Count() > 1
                select fileGroup;

            // Pass the query to a method that will  
            // output one page at a time.  
            pageOutput<string, string>(queryDupNames);
        }

        internal void QueryDuplicates2(bool showFiles = true)
        {
            if (setts == null)
                return;

            Console.WriteLine($"[{DateTime.Now}] Started the dupes identification process...");

            // Change the root drive or folder if necessary 
            var rootFolder = setts.Paths.ToList<Light_Photo_Manager.Helpers.Path>().Find(x => x.Type == "dedupe");
            string startFolder = rootFolder.Dir; // @"c:\program files\Microsoft Visual Studio 9.0\";

            // Make the lines shorter for the console display  
            int charsToSkip = startFolder.Length;

            // Take a snapshot of the file system.  
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(startFolder);
            IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            Console.WriteLine($"[{DateTime.Now}] {fileList.Count()} files will be searched for duplications.");

            // Note the use of a compound key. Files that match  
            // all three properties belong to the same group.  
            // A named type is used to enable the query to be  
            // passed to another method. Anonymous types can also be used  
            // for composite keys but cannot be passed across method boundaries  
            //
            var queryDupFiles =
                from file in fileList
                group file.FullName.Substring(charsToSkip) by
                    new PortableKey { Name = file.Name, LastWriteTime = file.LastWriteTime, Length = file.Length } into fileGroup
                where fileGroup.Count() > 1
                select fileGroup;

            var list = queryDupFiles.ToList();

            int i = queryDupFiles.Count();

            Console.WriteLine($"[{DateTime.Now}] {i} duplicates found.");

            if (showFiles)
                pageOutput<PortableKey, string>(queryDupFiles);

            Console.WriteLine("Press any key to close the program...");
            ConsoleKey key = Console.ReadKey().Key;
        }

        

        // A generic method to page the output of the QueryDuplications methods  
        // Here the type of the group must be specified explicitly. "var" cannot  
        // be used in method signatures. This method does not display more than one  
        // group per page.  
        private static void pageOutput<K, V>(IEnumerable<System.Linq.IGrouping<K, V>> groupByExtList)
        {
            // Flag to break out of paging loop.  
            bool goAgain = true;

            // "3" = 1 line for extension + 1 for "Press any key" + 1 for input cursor.  
            int numLines = Console.WindowHeight - 3;

            int fileCount = 0;

            // Iterate through the outer collection of groups.  
            foreach (var filegroup in groupByExtList)
            {
                // Start a new extension at the top of a page.  
                int currentLine = 0;

                // Output only as many lines of the current group as will fit in the window.  
                do
                {
                    //Console.Clear();
                    Console.WriteLine("Filename = {0}", filegroup.Key.ToString() == String.Empty ? "[none]" : filegroup.Key.ToString());

                    // Get 'numLines' number of items starting at number 'currentLine'.  
                    var resultPage = filegroup.Skip(currentLine).Take(numLines);

                    //Execute the resultPage query  
                    foreach (var fileName in resultPage)
                    {
                        Console.WriteLine("\t{0}", fileName);
                    }

                    // Increment the line counter.  
                    currentLine += numLines;

                    fileCount++;
                    if (fileCount % 500 == 0)
                    {
                        Console.WriteLine($"[{DateTime.Now}] {fileCount} duplicates found so far...");
                    }

                } while (currentLine < filegroup.Count());

                if (goAgain == false)
                    break;
            }

            
        }
    }
}
