using ExifReader.Models.Enums;

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
