using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Light_Photo_Manager
{
    
    public class RootObject
    {
        public Path[] Paths { get; set; }
    }

    public class Path
    {
        public string Type { get; set; }
        public string Dir { get; set; }
        public bool Exists { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RootObject setts;
            using (StreamReader r = new StreamReader("configuration.json"))
            {
                string json = r.ReadToEnd();
                setts = JsonConvert.DeserializeObject<RootObject>(json);
            }

            // extract destination paths
            List<string> destPaths = new List<string>();
            bool existOneSource = false;
            bool existOneDest = false;
            foreach (var sett in setts.Paths)
            {
                // check if directory exists
                if (!Directory.Exists($"{sett.Dir}"))
                {
                    if (sett.Type == "src")
                        Console.WriteLine($"[{DateTime.Now}] Source directory {sett.Dir} does not exist and will be skipped. ");
                    else
                        Console.WriteLine($"[{DateTime.Now}] Destination directory {sett.Dir} does not exist and will be skipped. ");
                    sett.Exists = false;
                }
                else
                {
                    sett.Exists = true;
                    if (sett.Type == "src") existOneSource = true;
                    if (sett.Type == "dest") existOneDest = true;
                }

                if (sett.Type == "dest")
                    destPaths.Add(sett.Dir);
            }

            if (!existOneDest)
                {
                Console.WriteLine($"[{DateTime.Now}] Process aborted as there are no valid destination folders...");
                Console.WriteLine("Press any key to close program.");
                Console.ReadLine();
                return;
            }

            if (!existOneSource)
            {
                Console.WriteLine($"[{DateTime.Now}] Process aborted as there are no valid source folders...");
                Console.WriteLine("Press any key to close program.");
                Console.ReadLine();
                return;
            }

            // process the source paths
            Console.WriteLine($"[{DateTime.Now}] Light_Photo_Manager started to parse the source folders...");
            foreach (var sett in setts.Paths)
            {
                if (!sett.Exists) continue;
                if (sett.Type == "src") {
                    try
                    {
                        string[] files = Directory.GetFiles(sett.Dir, "*", SearchOption.AllDirectories);
                        
                        Console.WriteLine($"[{DateTime.Now}] {files.Length} files are getting processed in source folder {sett.Dir}...");
                        foreach (var file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            var created = fi.CreationTime;
                            var lastmodified = fi.LastWriteTime;

                            // get Year of file creation
                            var year = created.Year;

                            // copy the file
                            foreach (var path in destPaths) {
                                // if destination directory does not exist, display error and exit  the for loop
                                if (!Directory.Exists($"{path}"))
                                {
                                    continue;
                                }

                                // check if Year existed as subfolder in destination folders; if not create it
                                if (!Directory.Exists($"{path}/{year}"))
                                    Directory.CreateDirectory($"{path}/{year}");
                                // copy the file in respective subfolder of destination path
                                File.Copy(file, $"{path}//{year}//{fi.Name}",true);
                            }
                        }
                        Console.WriteLine($"[{DateTime.Now}] Source folder {sett.Dir} has been fully processed.");
                        
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
                    }

                }
            }

            Console.WriteLine("Press any key to close program.");
            Console.ReadLine();

        }
    }
}
