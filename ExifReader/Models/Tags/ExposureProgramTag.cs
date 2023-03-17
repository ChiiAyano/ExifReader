namespace ExifReader.Models.Tags;

/// <summary>
/// 露出プログラムを表すタグ
/// </summary>
internal record ExposureProgramTag : TagBase
{
    /// <summary>
    /// 露出プログラム
    /// </summary>
    public ExposureProgram Program { get; }

    /// <summary>
    /// 露出プログラムの説明
    /// </summary>
    public string ProgramValue => this.Program switch
    {
        ExposureProgram.Undefined => "Undefined",
        ExposureProgram.Manual => "M",
        ExposureProgram.NormalProgram => "P",
        ExposureProgram.Aperture => "Av",
        ExposureProgram.Shutter => "Tv",
        _ => "Other"
    };

    public ExposureProgramTag(TagId tagId, int data)
    {
        this.TagId = tagId;
        this.Program = (ExposureProgram)data;
    }
}

/// <summary>
/// 露出プログラム定義
/// </summary>
public enum ExposureProgram
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
