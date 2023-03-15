using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ExifReader.Extensions;
using ExifReader.Models;

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

            if (!soi.Valid())
            {
                ErrorMessage("JPEGヘッダーが違います。");
                Environment.Exit(1);
                return;
            }

            while (true)
            {
                var headerData = data.AsSpan(index);

                var marker = headerData[..2];

                switch (marker[1])
                {
                    case 0xE0:
                        {
                            var app0 = new ApplicationTypeZeroSegment(headerData);

                            if (!app0.Valid())
                            {
                                ErrorMessage("アプリケーション セグメント タイプ 0 がないか、正しくなさそうです。");
                                Environment.Exit(2);
                                return;
                            }

                            index += app0.FieldLength + 2;
                        }
                        break;
                    case 0xE1:
                        {
                            var app1 = new ApplicationTypeOneSegment();
                            var tags = app1.Parse(data);

                            index += app1.FieldLength + 2;
                        }
                        break;
                }


            }
        }

        private static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}