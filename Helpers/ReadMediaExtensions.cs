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
        RootExtObject setts;

        public ReadMediaExtensions()
        {
            
            try
            {
                using (StreamReader r = new StreamReader("media.extensions.json"))
                {
                    string json = r.ReadToEnd();
                    this.setts = JsonConvert.DeserializeObject<RootExtObject>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}] Error reading media.extensions file: {ex.Message}.");
                Console.WriteLine("Press any key to close program.");
                Console.ReadLine();
                this.setts = null;
            }
        }

        public RootExtObject Extensions
        {
            get
            {
                return setts;
            }
        }
        
        public string GetCaptureType(string ext)
        {
            string rettype = string.Empty;
            foreach (var e in this.setts.Extensions)
                if (e.Extension.ToUpper() == ext.ToUpper())
                {
                    rettype = e.Type;
                    break;
                }

            return rettype;
        }
    }
}
