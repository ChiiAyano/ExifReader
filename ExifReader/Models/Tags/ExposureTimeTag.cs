namespace ExifReader.Models.Tags;

/// <summary>
/// 露出時間を表すタグ
/// </summary>
internal record ExposureTimeTag : TagBase
{
    public ExposureTimeTag(TagId tagId, int a, int b)
    {
        this.TagId = tagId;

        this.FormattedValue = $"{a}/{b} sec";
    }
}