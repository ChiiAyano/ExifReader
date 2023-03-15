using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ExifReader.Models;

namespace ExifReader.Extensions
{
    internal static class ModelExtensions
    {
        public static bool Valid(this StartOfImageSegment data)
        {
            return data is { MarkerIdentity: 0xFF, SoiMarker: 0xD8 };
        }

        public static bool Valid(this ApplicationTypeZeroSegment data)
        {
            return data is
            {
                MarkerIdentity: 0xFF,
                AppZeroMarker: 0xE0
            };
        }
    }
}
