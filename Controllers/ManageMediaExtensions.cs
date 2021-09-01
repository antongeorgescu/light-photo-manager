using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Light_Photo_Manager.Helpers;
using Newtonsoft.Json;

namespace Light_Photo_Manager.Controllers
{
    internal class ManageMediaExtensions
    {
        RootConfigObject setts = null;

        public ManageMediaExtensions()
        {
            ReadConfiguration config = new ReadConfiguration();
            setts = config.GetPaths();
        }

        internal void QueryExtensions()
        {
            if (setts == null)
                return;

            // Change the root drive or folder if necessary 
            var srcdirs = setts.Paths.ToList<Light_Photo_Manager.Helpers.Path>().FindAll(x => x.Type == "src").Select(x => x.Dir).ToList();

            var exts = new List<string>();
            try
            {
                foreach (string d in srcdirs)
                {
                    // Take a snapshot of the file system.  
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(d);

                    // This method assumes that the application has discovery permissions  
                    // for all folders under the specified path.  
                    IEnumerable<System.IO.FileInfo> fileList = dir.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

                    // var can be used for convenience with groups.  
                    var queryExts =
                        from file in fileList
                        select file.Extension.ToUpper().Replace(".", string.Empty);
                    foreach (string ext in queryExts.Distinct())
                        if (!exts.Contains(ext))
                            exts.Add(ext);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
            }

            if (exts.Count > 0)
            {
                var distinctexts = exts.Distinct().OrderBy(x => x);
                var strexts = string.Empty;
                distinctexts.ToList().ForEach(x => strexts = strexts + x + ",");
                strexts = strexts.Remove(strexts.Length - 1, 1);

                Console.WriteLine($"[{DateTime.Now}] Following extensions found in the source folders: {strexts}");

                var mext = new ReadMediaExtensions();
                var appexts = mext.Extensions;
                var strappexts = string.Empty;

                var ordappexts = appexts.Extensions.ToList().OrderBy(x => x.Extension).Select(x => x).ToList();
                ordappexts.ForEach(x => strappexts = strappexts + x.Extension + ",");
                strappexts = strappexts.Remove(strappexts.Length - 1, 1);

                Console.WriteLine($"[{DateTime.Now}] Following extensions are registered in this application: {strappexts}");


            }

        }

        
    }
}
