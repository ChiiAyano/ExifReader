# ExifReader
JPEG 画像の EXIF データを簡易的に読み取り、必要な情報を出力するだけのツールです。SONY のミラーレス一眼 α6600 (ILCE-6600) でのみ確認しています。

何らかの写真の説明に使いたいとき、読ませればテキストで取得できるので、必要な情報の取捨選択が簡単にできると思います。

RAW データからは取得できません。


## 出力イメージ
```
File Path: F:\....\DSC06855.JPG
SONY ILCE-6600
E 24mm F1.8 ZA
24 mm (36 mm)
Mode Av f/1.8
1/40 sec
ISO 160 +1 EV
```

パソコンなどで RAW 現像した後の JPEG データでも上記のように出ることは確認していますが、もしかするとダメなパターンもあるかもしれません。

## 環境
- [.NET 7](https://dotnet.microsoft.com/ja-jp/download/dotnet/7.0)
