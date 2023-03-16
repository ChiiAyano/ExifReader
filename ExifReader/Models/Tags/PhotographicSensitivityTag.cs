namespace ExifReader.Models.Tags;

/// <summary>
/// 撮影感度 (ISO感度) を表すタグ
/// </summary>
internal record PhotographicSensitivityTag : TagBase
{
    /// <summary>
    /// ISO 感度の値
    /// </summary>
    public int Value { get; }

    public PhotographicSensitivityTag(TagId tagId, int data)
    {
        this.TagId = tagId;
        this.Value = data;
        this.FormattedValue = $"ISO {data}";
    }
}