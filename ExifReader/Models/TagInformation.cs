namespace ExifReader.Models;

internal readonly record struct TagInformation
{
    public TagId Tag { get; init; }
    public ushort ValueType { get; init; }
    public DataType DataType => (DataType)this.ValueType;
    public int TypeUnit => DataLengthPreset[this.DataType];
    public uint ValueCount { get; init; }
    public int ValueLength => (int)(this.TypeUnit * this.ValueCount);
    public uint Offset { get; init; }

    public static int CalculateValueLength(DataType type, int count) => DataLengthPreset[type] * count;

    private static Dictionary<DataType, byte> DataLengthPreset { get; } = new Dictionary<DataType, byte> 
    {
        { DataType.Byte, 1 },
        { DataType.Ascii, 1 },
        { DataType.Short, 2 },
        { DataType.Long, 4 },
        { DataType.Rational, 8 },
        { DataType.SByte, 1 },
        { DataType.Undefined, 1 },
        { DataType.SShort, 2 },
        { DataType.SLong, 4 },
        { DataType.SRational, 8 },
        { DataType.Float, 4 },
        { DataType.DFloat, 8 },
    };

    public TagInformation()
    {
    }
}

internal enum DataType
{
    Byte = 0x01,
    Ascii = 0x02,
    Short = 0x03,
    Long = 0x04,
    Rational = 0x05,
    SByte = 0x06,
    Undefined = 0x07,
    SShort = 0x08,
    SLong = 0x09,
    SRational = 0x0A,
    Float = 0x0B,
    DFloat = 0x0C
}