using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExifReader.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct StartOfImageSegment
    {
        public byte MarkerIdentity { get; set; }
        public byte SoiMarker { get; set; }
    }
}
