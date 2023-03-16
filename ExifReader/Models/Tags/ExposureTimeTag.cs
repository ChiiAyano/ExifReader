namespace ExifReader.Models.Tags;

internal record ExposureTimeTag : TagBase
{
    public ExposureTimeTag(TagId tagId, int a, int b)
    {
        this.TagId = tagId;

        this.FormattedValue = $"{a}/{b} sec";
    }
}