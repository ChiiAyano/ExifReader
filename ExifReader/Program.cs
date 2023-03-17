using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ExifReader.Extensions;
using ExifReader.Models;
using ExifReader.Models.Tags;

namespace ExifReader
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var item in args)
                {
                    var data = await ReadAsync(item);
                    Analyze(data);
                }
            }
            else
            {
                Console.Write("File Path: ");
                var filePath = Console.ReadLine();

                var data = await ReadAsync(filePath);
                Analyze(data);
            }
        }

        private static async Task<byte[]> ReadAsync(string? filePath)
        {
            var path = filePath?.Trim('"');

            if (File.Exists(path))
            {
                return await File.ReadAllBytesAsync(path);
            }

            ErrorMessage("指定されたファイルパスが見つかりません。");

            return Array.Empty<byte>();
        }

        private static void Analyze(byte[] data)
        {
            if (!data.Any())
            {
                return;
            }

            var index = 0;
            var soiHeader = data.AsSpan(0, index += Unsafe.SizeOf<StartOfImageSegment>());
            var soi = MemoryMarshal.Cast<byte, StartOfImageSegment>(soiHeader)[0];
            TagBase?[]? tagData = null;

            if (!soi.Valid())
            {
                ErrorMessage("JPEGヘッダーが違います。");
                Environment.Exit(1);
                return;
            }

            while (index < data.Length)
            {
                var headerData = data.AsSpan(index);

                var marker = headerData[..2];

                switch (marker[1])
                {
                    case 0xE0:
                        {
                            var app0 = new ApplicationTypeZeroSegment();
                            index = app0.Parse(data, index);
                        }
                        break;
                    case 0xE1:
                        {
                            var app1 = new SegmentParser();
                            var (tags, i) = app1.Parse(data, index);

                            tagData = tags;
                            index = i;
                        }
                        break;
                    default:
                        index++;
                        break;
                }

                if (tagData is not null)
                {
                    break;
                }
            }

            if (tagData is null)
            {
                ErrorMessage("EXIF データが見つかりませんでした。");
                Environment.Exit(2);
                return;
            }

            var maker = tagData.FirstOrDefault(f => f?.TagId == TagId.Make) as StringTag;
            var model = tagData.FirstOrDefault(f => f?.TagId == TagId.Model) as StringTag;
            var lensModel = tagData.FirstOrDefault(f => f?.TagId == TagId.LensModel) as StringTag;
            var focalLength = tagData.FirstOrDefault(f => f?.TagId == TagId.FocalLength) as FocalLengthTag;
            var focal35Length = tagData.FirstOrDefault(f => f?.TagId == TagId.FocalLengthIn35MmFilm) as NumericTag;
            var iso = tagData.FirstOrDefault(f => f?.TagId == TagId.PhotographicSensitivity);
            var exp = tagData.FirstOrDefault(f => f?.TagId == TagId.ExposureBiasValue);
            var shutter = tagData.FirstOrDefault(f => f?.TagId == TagId.ExposureTime);
            var fNumber = tagData.FirstOrDefault(f => f?.TagId == TagId.FNumber);
            var programMode = tagData.FirstOrDefault(f => f?.TagId == TagId.ExposureProgram) as ExposureProgramTag;

            var gpsLatitudeRef = tagData.FirstOrDefault(f => f?.TagId == TagId.GpsLatitudeRef);
            var gpsLatitude = tagData.FirstOrDefault(f => f?.TagId == TagId.GpsLatitude) as GpsRationalTag;
            var gpsLongitudeRef = tagData.FirstOrDefault(f => f?.TagId == TagId.GpsLongitudeRef);
            var gpsLongitude = tagData.FirstOrDefault(f => f?.TagId == TagId.GpsLongitude) as GpsRationalTag;

            var lensModelValue = lensModel is null ? "" : lensModel.FormattedValue + "\n";

            string? focalValue;
            if (focalLength?.Focal == focal35Length?.Value || focal35Length is null)
            {
                focalValue = focalLength?.FormattedValue;
            }
            else
            {
                focalValue = $"{focalLength?.FormattedValue} ({focal35Length?.FormattedValue} mm)";
            }

            var modeValue = programMode is null ? "" : $"Mode {programMode.ProgramValue} ";

            Console.WriteLine($"{maker?.FormattedValue} {model?.FormattedValue}\n" +
                $"{lensModelValue}" +
                $"{focalValue}\n" +
                $"{modeValue}{fNumber?.FormattedValue}\n" +
                $"{shutter?.FormattedValue}\n" +
                $"{iso?.FormattedValue} {exp?.FormattedValue}");

            if (gpsLatitudeRef is not null &&
                gpsLatitude is not null && 
                gpsLongitudeRef is not null && 
                gpsLongitude is not null)
            {
                Console.WriteLine();
                Console.WriteLine($"{gpsLatitudeRef.FormattedValue}{gpsLatitude.FormattedValue}, {gpsLongitudeRef.FormattedValue}{gpsLongitude.FormattedValue}");
            }

            Console.WriteLine();
            Console.WriteLine("出力しました。何かキーを押すと終了します。");
            Console.ReadKey();
        }

        private static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
