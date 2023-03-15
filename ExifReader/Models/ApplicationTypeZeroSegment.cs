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

        public ApplicationTypeZeroSegment(Span<byte> data)
        {
            MarkerIdentity = data[0];
            AppZeroMarker = data[1];
            FieldLength = BinaryPrimitives.ReadUInt16BigEndian(data[2..4]);
            JfifIdentity = data[4..9].ToArray();
            Version = BinaryPrimitives.ReadUInt16BigEndian(data[9..11]);
            PixelUnit = data[11];
            PixelWidth = BinaryPrimitives.ReadUInt16BigEndian(data[12..14]);
            PixelHeight = BinaryPrimitives.ReadUInt16BigEndian(data[14..16]);
            ThumbnailWidth = data[16];
            ThumbnailHeight = data[17];

            if (FieldLength - 16 > 0)
            {
                var length = FieldLength + 2 - 16;
                Image = data[18..length].ToArray();
            }
        }
    }
}
