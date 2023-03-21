using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace ExifReader.Models
{
    internal class ApplicationTypeZeroSegment
    {
        public byte MarkerIdentity { get; set; }
        public byte AppZeroMarker { get; set; }
        public ushort FieldLength { get; set; }
        public byte[] JfifIdentity { get; set; }
        public ushort Version { get; set; }
        public byte PixelUnit { get; set; }
        public ushort PixelWidth { get; set; }
        public ushort PixelHeight { get; set; }
        public byte ThumbnailWidth { get; set; }
        public byte ThumbnailHeight { get; set; }
        public byte[] Image { get; set; }

        public int Parse(Span<byte> data, int startIndex)
        {
            var index = startIndex;
            MarkerIdentity = data[index];
            AppZeroMarker = data[index += 1];
            FieldLength = BinaryPrimitives.ReadUInt16BigEndian(data[(index += 1)..(index += 2)]);
            JfifIdentity = data[index..(index += 5)].ToArray();
            Version = BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += 2)]);
            PixelUnit = data[index];
            PixelWidth = BinaryPrimitives.ReadUInt16BigEndian(data[(index += 1)..(index += 2)]);
            PixelHeight = BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += 2)]);
            ThumbnailWidth = data[index += 1];
            ThumbnailHeight = data[index += 1];

            if (FieldLength - 16 > 0)
            {
                var length = FieldLength - 16;
                Image = data[index..(index += length)].ToArray();
            }

            return index;
        }
    }
}
