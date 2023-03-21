namespace ExifReader.Models.Tags;

internal record NumericArrayTag : TagBase
{
    public int[]? Data { get; }

    public NumericArrayTag(TagId tagId, int[]? data)
    {
        this.TagId = tagId;
        this.Data = data;
        this.FormattedValue = string.Join(", ", data?? Array.Empty<int>());
    }
}
