namespace ExifReader.Models;

internal enum TagId
{
    // GPS IFD
    /// <summary>
    /// GPS タグバージョン
    /// </summary>
    GpsVersionId = 0,
    /// <summary>
    /// 北緯・南緯
    /// </summary>
    GpsLatitudeRef = 1,
    /// <summary>
    /// 緯度
    /// </summary>
    GpsLatitude = 2,
    /// <summary>
    /// 東経・西経
    /// </summary>
    GpsLongitudeRef = 3,
    /// <summary>
    /// 経度
    /// </summary>
    GpsLongitude = 4,
    /// <summary>
    /// 高度の基準
    /// </summary>
    GpsAltitudeRef = 5,
    /// <summary>
    /// 高度
    /// </summary>
    GpsAltitude = 6,
    /// <summary>
    /// GPS 時間
    /// </summary>
    GpsTimeStamp = 7,
    /// <summary>
    /// GPS 日付
    /// </summary>
    GpsDateStamp = 29,

    // 0thIFD
    /// <summary>
    /// 画像タイトル
    /// </summary>
    ImageDescription = 270,
    /// <summary>
    /// メーカー
    /// </summary>
    Make = 271,
    /// <summary>
    /// カメラのモデル
    /// </summary>
    Model = 272,
    /// <summary>
    /// ソフトウェア名
    /// </summary>
    Software = 305,
    /// <summary>
    /// ファイル変更日時
    /// </summary>
    DateTime = 306,

    // EXIF
    /// <summary>
    /// 露出時間
    /// </summary>
    ExposureTime = 33434,
    /// <summary>
    /// F値
    /// </summary>
    FNumber = 33437,
    /// <summary>
    /// Exif タグのポインター
    /// </summary>
    ExifIfdPointer = 34665,
    /// <summary>
    /// 露出プログラム
    /// </summary>
    ExposureProgram = 34850,
    /// <summary>
    /// GPS タグへのポインター
    /// </summary>
    GpsInfoIfdPointer = 34853,
    /// <summary>
    /// 撮影感度
    /// </summary>
    PhotographicSensitivity = 34855,
    /// <summary>
    /// 感度種別
    /// </summary>
    SensitivityType = 34864,
    /// <summary>
    /// 標準出力感度
    /// </summary>
    StandardOutputSensitivity = 34866,
    /// <summary>
    /// 原画像データの生成日時
    /// </summary>
    DateTimeOriginal = 36867,
    /// <summary>
    /// 露出補正値
    /// </summary>
    ExposureBiasValue = 37380,
    /// <summary>
    /// 最小F値
    /// </summary>
    MaxApertureValue = 37381,
    /// <summary>
    /// 焦点距離
    /// </summary>
    FocalLength = 37386,
    /// <summary>
    /// ホワイトバランス
    /// </summary>
    WhiteBalance = 41987,
    /// <summary>
    /// 35mm 換算レンズ焦点距離
    /// </summary>
    FocalLengthIn35MmFilm = 41989,
    /// <summary>
    /// レンズ名
    /// </summary>
    LensModel = 42036,
}