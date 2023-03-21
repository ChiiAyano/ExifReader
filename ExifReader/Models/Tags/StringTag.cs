namespace ExifReader.Models.Tags;

/// <summary>
/// 文字列を表すタグ
/// </summary>
internal record StringTag : TagBase
{
    public StringTag(TagId tagId, string? data)
    {
        this.TagId = tagId;
        this.FormattedValue = data?.Trim('\0');
    }
}
