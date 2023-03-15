namespace ExifReader.Models.Tags;

/// <summary>
/// 数字に関する汎用タグ情報
/// </summary>
internal record NumericTag : TagBase
{
    /// <summary>
    /// 値
    /// </summary>
    public int Value { get; }

    public NumericTag(TagId tagId, int data)
    {
        this.TagId = tagId;
        this.Value = data;
        this.FormattedValue = data.ToString();
    }
}