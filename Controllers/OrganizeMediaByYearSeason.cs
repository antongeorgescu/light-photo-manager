using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Light_Photo_Manager.Helpers;

namespace Light_Photo_Manager.Controllers
{
    internal class OrganizeMediaByYearSeason
    {
        internal void Execute()
        {
            ReadConfiguration config = new ReadConfiguration();
            var setts = config.GetPaths();

            if (setts == null)
                return;

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
                //Console.WriteLine("Press any key to close program.");
                //Console.ReadLine();
                return;
            }

            if (!existOneSource)
            {
                Console.WriteLine($"[{DateTime.Now}] Process aborted as there are no valid source folders...");
                //Console.WriteLine("Press any key to close program.");
                //Console.ReadLine();
                return;
            }

            // process the source paths
            Console.WriteLine($"[{DateTime.Now}] Light_Photo_Manager started to parse the source folders...");
            int photoCount = 0;
            foreach (var sett in setts.Paths)
            {
                if (!sett.Exists) continue;
                if (sett.Type == "src")
                {
                    try
                    {
                        string[] files = Directory.GetFiles(sett.Dir, "*", SearchOption.AllDirectories);

                        Console.WriteLine($"[{DateTime.Now}] {files.Length} files are getting processed in source folder {sett.Dir}.");
                        foreach (var file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            //var created = fi.CreationTime;
                            var captured = fi.LastWriteTime;

                            // get Year of file creation
                            var year = captured.Year;
                            var season = getSeason(captured.Month);
                            var ext = fi.Extension.Replace(".", "").ToUpper();

                            // copy the file
                            foreach (var path in destPaths)
                            {
                                // if destination directory does not exist, display error and exit  the for loop
                                if (!Directory.Exists($"{path}"))
                                {
                                    continue;
                                }

                                // check if Year existed as subfolder in destination folders; if not create it
                                if (!Directory.Exists($"{path}/{year}"))
                                    Directory.CreateDirectory($"{path}/{year}");
                                // check if Season existed as subfolder in destination folders; if not create it
                                if (!Directory.Exists($"{path}/{year}/{season}"))
                                    Directory.CreateDirectory($"{path}/{year}/{season}");
                                // check if Extension existed as subfolder in destination folders; if not create it
                                if (!Directory.Exists($"{path}/{year}/{season}/{getCaptureType(ext)}"))
                                    Directory.CreateDirectory($"{path}/{year}/{season}/{getCaptureType(ext)}");

                                // copy the file in respective subfolder of destination path
                                File.Copy(file, $"{path}//{year}//{season}//{getCaptureType(ext)}//{fi.Name}", true);
                                photoCount++;
                                if (photoCount % 500 == 0)
                                    Console.WriteLine($"[{DateTime.Now}] {photoCount} files processed so far...");

                            }
                        }
                        Console.WriteLine($"[{DateTime.Now}] Source folder {sett.Dir} has been fully processed.");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
                    }

                }
            }
            Console.WriteLine($"[{DateTime.Now}] {photoCount} files processed in total.");
            //Console.WriteLine("Press any key to close program.");
            //Console.ReadLine();

        }

        static string getSeason(int month)
        {
            string season = string.Empty; ;
            switch (month)
            {
                case 1:
                    season = "Winter";
                    break;
                case 2:
                    season = "Winter";
                    break;
                case 3:
                    season = "Spring";
                    break;
                case 4:
                    season = "Spring";
                    break;
                case 5:
                    season = "Spring";
                    break;
                case 6:
                    season = "Summer";
                    break;
                case 7:
                    season = "Summer";
                    break;
                case 8:
                    season = "Summer";
                    break;
                case 9:
                    season = "Fall";
                    break;
                case 10:
                    season = "Fall";
                    break;
                case 11:
                    season = "Fall";
                    break;
                case 12:
                    season = "Winter";
                    break;
                default:
                    season = "Unspecified";
                    break;
            }
            return season;
        }

        static string getCaptureType(string ext)
        {
            string[] VIDEOEXT = { "MOV" };
            string[] PHOTOEXT = { "JPEG", "JPG", "PNG" };
            string[] SPECIALEXT = { "HEIC" };

            string type = string.Empty;

            if (Array.IndexOf(VIDEOEXT, ext.ToUpper()) > -1)
                type = "Videos";
            else if (Array.IndexOf(PHOTOEXT, ext.ToUpper()) > -1)
                type = "Pictures";
            else if (Array.IndexOf(SPECIALEXT, ext.ToUpper()) > -1)
                type = "Special";

            return type;
        }
    }

}
