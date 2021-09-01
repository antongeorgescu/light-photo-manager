using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;

namespace Light_Photo_Manager.Helpers
{
    public class RootExtObject
    {
        public ExtensionObject[] Extensions { get; set; }
    }

    public class ExtensionObject
    {
        public string Type { get; set; }
        public string Extension { get; set; }
    }

    public class ReadMediaExtensions
    {
        public RootExtObject GetAppExtensions()
        {
            RootExtObject setts;
            try
            {
                using (StreamReader r = new StreamReader("media.extensions.json"))
                {
                    string json = r.ReadToEnd();
                    setts = JsonConvert.DeserializeObject<RootExtObject>(json);
                }
                return setts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Error reading media.extensions file: {ex.Message}.");
                Console.WriteLine("Press any key to close program.");
                Console.ReadLine();
                return null;
            }
        }
    }
}
