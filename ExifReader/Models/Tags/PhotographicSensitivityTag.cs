namespace ExifReader.Models.Tags;

internal record PhotographicSensitivityTag : TagBase
{
    /// <summary>
    /// 値
    /// </summary>
    public int Value { get; }

    public PhotographicSensitivityTag(TagId tagId, int data)
    {
        this.TagId = tagId;
        this.Value = data;
        this.FormattedValue = $"ISO {data}";
    }
}