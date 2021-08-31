using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;

namespace Light_Photo_Manager.Helpers
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

    public class ReadConfiguration
    {
        public RootObject GetPaths()
        {
            RootObject setts;
            try
            {
                using (StreamReader r = new StreamReader("configuration.json"))
                {
                    string json = r.ReadToEnd();
                    setts = JsonConvert.DeserializeObject<RootObject>(json);
                }
                return setts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Error reading configuration file: {ex.Message}.");
                Console.WriteLine("Press any key to close program.");
                Console.ReadLine();
                return null;
            }
        }
    }
}
