using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExifReader.Models.Tags;

namespace ExifReader.Models
{
    internal class ApplicationTypeOneSegment
    {
        const int StartIndex = 30;

        public byte MarkerIdentity { get; set; }
        public byte AppZeroMarker { get; set; }

        public ushort FieldLength { get; set; }

        public string ByteOrder { get; set; }

        public TagBase?[] Parse(Span<byte> data)
        {
            var index = StartIndex;
            var isBigEndian = false;
            var result = new List<TagBase?>();

            MarkerIdentity = data[0];
            AppZeroMarker = data[1];
            FieldLength = BinaryPrimitives.ReadUInt16BigEndian(data[2..4]);

            // Exif は読み飛ばす 4..10
            ByteOrder = Encoding.ASCII.GetString(data[index..(index += 2)]);

            if (ByteOrder == "MM")
            {
                // ビッグエンディアン
                isBigEndian = true;
                var tiff = BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += 2)]);
                var nextPointer = BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += 4)]);

                index = StartIndex + (int)nextPointer;
            }
            else if (ByteOrder == "II")
            {
                // リトルエンディアン
                var tiff = BinaryPrimitives.ReadUInt16LittleEndian(data[index..(index += 2)]);
                var nextPointer = BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += 4)]);

                index = StartIndex + (int)nextPointer;
            }

            var tags = Parse(data, index, isBigEndian);
            result.AddRange(tags);

            var exifPointer = Array.Find(tags, f => f?.TagId == TagId.ExifIfdPointer) as NumericTag;
            index = exifPointer?.Value ?? 0;
            //var exif = ExifIfd.Parse(data, isBigEndian, index, StartIndex);

            var exifs = Parse(data, index + StartIndex, isBigEndian);
            result.AddRange(exifs);

            return result.Where(w => w != null).ToArray();
        }

        private TagBase?[] Parse(Span<byte> data, int index, bool isBigEndian)
        {
            var tagLength = isBigEndian ?
                BinaryPrimitives.ReadInt16BigEndian(data[index..(index += 2)]) :
                BinaryPrimitives.ReadInt16LittleEndian(data[index..(index += 2)]);

            var tagInfos = new List<TagInformation>(tagLength);
            var tagDataList = new List<TagBase?>(tagInfos.Count);

            for (var i = 0; i < tagLength; i++)
            {
                var tag = isBigEndian ?
                    BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += 2)]) :
                    BinaryPrimitives.ReadUInt16LittleEndian(data[index..(index += 2)]);

                var valueType = isBigEndian ?
                    BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += 2)]) :
                    BinaryPrimitives.ReadUInt16LittleEndian(data[index..(index += 2)]);

                var valueCount = isBigEndian ?
                    BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += 4)]) :
                    BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += 4)]);

                if (TagInformation.CalculateValueLength((DataType)valueType, (int)valueCount) < 5)
                {
                    switch ((DataType)valueType)
                    {
                        case DataType.Byte:
                        case DataType.Short:
                        case DataType.Long:
                        case DataType.SShort:
                        case DataType.SLong:
                            var v = isBigEndian ?
                                BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += 4)]) :
                                BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += 4)]);
                            tagDataList.Add(ParseData((TagId)tag, valueA: (int)v));
                            break;
                        case DataType.Ascii:
                            var d = data[index..(index += 4)];
                            if (!isBigEndian)
                            {
                                d.Reverse();
                            }
                            var ascii = Encoding.ASCII.GetString(d);
                            tagDataList.Add(ParseData((TagId)tag, strValue: ascii));
                            break;
                        default:
                            // 4バイト分捨てる
                            index += 4;
                            break;
                    }
                }
                else
                {
                    var offset = isBigEndian ?
                        BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += 4)]) :
                        BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += 4)]);

                    var info = new TagInformation
                    {
                        Tag = (TagId)tag,
                        ValueType = valueType,
                        ValueCount = valueCount,
                        Offset = offset
                    };

                    tagInfos.Add(info);
                }
            }

            foreach (var item in tagInfos.Where(w => tagDataList.All(a => a?.TagId != w.Tag)))
            {
                tagDataList.Add(ParseTag(item, isBigEndian, data));
            }

            return tagDataList.ToArray();
        }

        private TagBase? ParseTag(TagInformation info, bool isBigEndian, Span<byte> data)
        {
            var index = 0;

            switch (info.DataType)
            {
                case DataType.Ascii:
                    index = StartIndex + (int)info.Offset;
                    var d = data[index..(index += info.ValueLength)];
                    if (!isBigEndian)
                    {
                        d.Reverse();
                    }
                    var ascii = Encoding.ASCII.GetString(d);
                    return ParseData(info.Tag, strValue: ascii);

                case DataType.Byte:
                    index = StartIndex + (int)info.Offset;
                    var bValue = isBigEndian ?
                    BinaryPrimitives.ReadInt16BigEndian(data[index..(index += info.ValueLength)]) :
                        BinaryPrimitives.ReadInt16LittleEndian(data[index..(index += info.ValueLength)]);
                    return ParseData(info.Tag, valueA: bValue);

                case DataType.Short:
                    index = StartIndex + (int)info.Offset;
                    var sValue = isBigEndian ?
                    BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += info.ValueLength)]) :
                        BinaryPrimitives.ReadUInt16LittleEndian(data[index..(index += info.ValueLength)]);
                    return ParseData(info.Tag, valueA: sValue);

                case DataType.Long:
                    index = StartIndex + (int)info.Offset;
                    var lValue = isBigEndian ?
                    BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += info.ValueLength)]) :
                        BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += info.ValueLength)]);
                    return ParseData(info.Tag, valueA: (int)lValue);

                case DataType.Rational:
                    index = StartIndex + (int)info.Offset;
                    var rA = isBigEndian ?
                    BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += info.ValueLength / 2)]) :
                    BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += info.ValueLength / 2)]);
                    var rB = isBigEndian ?
                    BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += info.ValueLength / 2)]) :
                        BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += info.ValueLength / 2)]);
                    return ParseData(info.Tag, valueA: (int)rA, valueB: (int)rB);

                case DataType.SByte:
                    index = StartIndex + (int)info.Offset;
                    var sbValue = isBigEndian ?
                        BinaryPrimitives.ReadInt16BigEndian(data[index..(index += info.ValueLength)]) :
                        BinaryPrimitives.ReadInt16LittleEndian(data[index..(index += info.ValueLength)]);
                    return ParseData(info.Tag, valueA: sbValue);

                case DataType.SLong:
                    index = StartIndex + (int)info.Offset;
                    var slValue = isBigEndian ?
                        BinaryPrimitives.ReadInt32BigEndian(data[index..(index += info.ValueLength)]) :
                        BinaryPrimitives.ReadInt32LittleEndian(data[index..(index += info.ValueLength)]);
                    return ParseData(info.Tag, valueA: slValue);

                case DataType.SRational:
                    index = StartIndex + (int)info.Offset;
                    var srA = isBigEndian ?
                        BinaryPrimitives.ReadInt32BigEndian(data[index..(index += info.ValueLength / 2)]) :
                        BinaryPrimitives.ReadInt32LittleEndian(data[index..(index += info.ValueLength / 2)]);
                    var srB = isBigEndian ?
                        BinaryPrimitives.ReadInt32BigEndian(data[index..(index += info.ValueLength / 2)]) :
                        BinaryPrimitives.ReadInt32LittleEndian(data[index..(index += info.ValueLength / 2)]);
                    return ParseData(info.Tag, valueA: srA, valueB: srB);

                default:
                    return null;
            }
        }

        TagBase? ParseData(TagId tagId, string? strValue = null, int? valueA = null, int? valueB = null)
        {
            switch (tagId)
            {
                case TagId.Make:
                    return new StringTag(tagId, strValue);
                case TagId.Model:
                    return new StringTag(tagId, strValue);
                case TagId.Software:
                    return new StringTag(tagId, strValue);
                case TagId.DateTime:
                    return new DateTimeTag(tagId, strValue);
                case TagId.ExposureTime:
                    return new RationalTag(tagId, valueA ?? 0, valueB ?? 0);
                case TagId.FNumber:
                    return new FNumberTag(tagId, valueA ?? 0, valueB ?? 0);
                case TagId.ExifIfdPointer:
                    return new NumericTag(tagId, valueA ?? 0);
                case TagId.SensitivityType:
                    return new NumericTag(tagId, valueA ?? 0);
                case TagId.StandardOutputSensitivity:
                    return new NumericTag(tagId, valueA ?? 0);
                case TagId.DateTimeOriginal:
                    return new DateTimeTag(tagId, strValue);
                case TagId.ExposureBiasValue:
                    return new ExposureBiasValueTag(tagId, valueA ?? 0, valueB ?? 0);
                case TagId.MaxApertureValue:
                    return new RationalTag(tagId, valueA ?? 0, valueB ?? 0);
                case TagId.FocalLength:
                    return new FocalLengthTag(tagId, valueA ?? 0, valueB ?? 0);
                case TagId.WhiteBalance:
                    return new NumericTag(tagId, valueA ?? 0);
                case TagId.FocalLengthIn35MmFilm:
                    return new NumericTag(tagId, valueA ?? 0);
                case TagId.LensModel:
                    return new StringTag(tagId, strValue);
                default:
                    return null;
            }
        }
    }
}
