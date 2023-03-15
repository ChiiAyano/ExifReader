using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifReader.Models
{
    internal readonly record struct TagData
    {
        public ushort Tag { get; init; }
        public string Data { get; init; }

        public TagData()
        {
            
        }
    }
}
