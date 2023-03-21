namespace ExifReader.Models.Enums;

/// <summary>
/// 露出プログラム定義
/// </summary>
internal enum ExposureProgram
{
    /// <summary>
    /// 未定義
    /// </summary>
    Undefined = 0,
    /// <summary>
    /// マニュアルモード
    /// </summary>
    Manual = 1,
    /// <summary>
    /// プログラムモード
    /// </summary>
    NormalProgram = 2,
    /// <summary>
    /// 絞り優先モード
    /// </summary>
    Aperture = 3,
    /// <summary>
    /// シャッター優先モード
    /// </summary>
    Shutter = 4,
    /// <summary>
    /// クリエイティブプログラムモード
    /// </summary>
    CreativeProgram = 5,
    /// <summary>
    /// アクションプログラムモード
    /// </summary>
    ActionProgram = 6,
    /// <summary>
    /// ポートレートモード
    /// </summary>
    Portrait = 7,
    /// <summary>
    /// ランドスケープモード
    /// </summary>
    Landscape = 8,
}
