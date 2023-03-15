using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifReader.Models
{
    internal class ExifIfd
    {
        //public static ExifIfd Parse(Span<byte> data, bool isBigEndian, int initIndex, int startIndex)
        //{
        //    var instance = new ExifIfd();

        //    var index = initIndex + startIndex;

        //    var tagLength = isBigEndian ?
        //        BinaryPrimitives.ReadInt16BigEndian(data[index..(index += 2)]) :
        //        BinaryPrimitives.ReadInt16LittleEndian(data[index..(index += 2)]);

        //    var tagInfos = new List<TagInformation>(tagLength);
        //    var tagDataList = new List<TagData>(tagInfos.Count);

        //    for (var i = 0; i < tagLength; i++)
        //    {
        //        var tag = isBigEndian ?
        //            BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += 2)]) :
        //            BinaryPrimitives.ReadUInt16LittleEndian(data[index..(index += 2)]);

        //        var valueType = isBigEndian ?
        //            BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += 2)]) :
        //            BinaryPrimitives.ReadUInt16LittleEndian(data[index..(index += 2)]);

        //        var valueCount = isBigEndian ?
        //            BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += 4)]) :
        //            BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += 4)]);

        //        var offset = isBigEndian ?
        //            BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += 4)]) :
        //            BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += 4)]);

        //        var info = new TagInformation
        //        {
        //            Tag = tag,
        //            ValueType = valueType,
        //            ValueCount = valueCount,
        //            Offset = offset
        //        };

        //        tagInfos.Add(info);

        //        if (info.ValueLength < 5)
        //        {
        //            var d = new TagData { Tag = tag, Data = offset.ToString() };
        //            tagDataList.Add(d);
        //        }
        //    }

        //    foreach (var item in tagInfos.Where(w => tagDataList.All(a => a.Tag != w.Tag)))
        //    {
        //        switch (item.DataType)
        //        {
        //            case DataType.Ascii:
        //                index = startIndex + (int)item.Offset;
        //                var d = data[index..(index += item.ValueLength)];
        //                if (!isBigEndian)
        //                {
        //                    d.Reverse();
        //                }
        //                var ascii = Encoding.ASCII.GetString(d);
        //                tagDataList.Add(new TagData { Tag = item.Tag, Data = ascii });
        //                break;
        //            case DataType.Byte:
        //                index = startIndex + (int)item.Offset;
        //                var bValue = isBigEndian ?
        //                    BinaryPrimitives.ReadInt16BigEndian(data[index..(index += item.ValueLength)]) :
        //                    BinaryPrimitives.ReadInt16LittleEndian(data[index..(index += item.ValueLength)]);
        //                tagDataList.Add(new TagData { Tag = item.Tag, Data = bValue.ToString() });

        //                break;
        //            case DataType.Short:
        //                index = startIndex + (int)item.Offset;
        //                var sValue = isBigEndian ?
        //                    BinaryPrimitives.ReadUInt16BigEndian(data[index..(index += item.ValueLength)]) :
        //                    BinaryPrimitives.ReadUInt16LittleEndian(data[index..(index += item.ValueLength)]);
        //                tagDataList.Add(new TagData { Tag = item.Tag, Data = sValue.ToString() });
        //                break;
        //            case DataType.Long:
        //                index = startIndex + (int)item.Offset;
        //                var lValue = isBigEndian ?
        //                    BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += item.ValueLength)]) :
        //                    BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += item.ValueLength)]);
        //                tagDataList.Add(new TagData { Tag = item.Tag, Data = lValue.ToString() });
        //                break;
        //            case DataType.Rational:
        //                index = startIndex + (int)item.Offset;
        //                var rA = isBigEndian ?
        //                    BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += item.ValueLength / 2)]) :
        //                    BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += item.ValueLength / 2)]);
        //                var rB = isBigEndian ?
        //                    BinaryPrimitives.ReadUInt32BigEndian(data[index..(index += item.ValueLength / 2)]) :
        //                    BinaryPrimitives.ReadUInt32LittleEndian(data[index..(index += item.ValueLength / 2)]);
        //                tagDataList.Add(new TagData { Tag = item.Tag, Data = $"{rA}/{rB}" });
        //                break;
        //            case DataType.SByte:
        //                index = startIndex + (int)item.Offset;
        //                var sbValue = isBigEndian ?
        //                    BinaryPrimitives.ReadInt16BigEndian(data[index..(index += item.ValueLength)]) :
        //                    BinaryPrimitives.ReadInt16LittleEndian(data[index..(index += item.ValueLength)]);
        //                tagDataList.Add(new TagData { Tag = item.Tag, Data = sbValue.ToString() });
        //                break;
        //            case DataType.SLong:
        //                index = startIndex + (int)item.Offset;
        //                var slValue = isBigEndian ?
        //                    BinaryPrimitives.ReadInt32BigEndian(data[index..(index += item.ValueLength)]) :
        //                    BinaryPrimitives.ReadInt32LittleEndian(data[index..(index += item.ValueLength)]);
        //                tagDataList.Add(new TagData { Tag = item.Tag, Data = slValue.ToString() });
        //                break;
        //            case DataType.SRational:
        //                index = startIndex + (int)item.Offset;
        //                var srA = isBigEndian ?
        //                    BinaryPrimitives.ReadInt32BigEndian(data[index..(index += item.ValueLength / 2)]) :
        //                    BinaryPrimitives.ReadInt32LittleEndian(data[index..(index += item.ValueLength / 2)]);
        //                var srB = isBigEndian ?
        //                    BinaryPrimitives.ReadInt32BigEndian(data[index..(index += item.ValueLength / 2)]) :
        //                    BinaryPrimitives.ReadInt32LittleEndian(data[index..(index += item.ValueLength / 2)]);
        //                tagDataList.Add(new TagData { Tag = item.Tag, Data = $"{srA}/{srB}" });
        //                break;
        //        }
        //    }

        //    return instance;
        //}
    }
}
