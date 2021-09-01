using System;
using System.Collections.Generic;
using System.Text;

namespace Light_Photo_Manager.Helpers
{
    public static class MediaExtensions
    {
        public static string[] VideoExts()
        {
            return new string[] {"MOV"};
        }

        public static string[] PhotoExts()
        {
            return new string[] { "JPEG", "JPG", "PNG" };
        }

        public static string[] SpecialExts()
        {
            return new string[] { "HEIC" };
        }

    }
}
