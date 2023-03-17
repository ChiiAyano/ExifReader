namespace ExifReader.Models.Tags;

/// <summary>
/// 露出時間を表すタグ
/// </summary>
internal record ExposureTimeTag : TagBase
{
    public ExposureTimeTag(TagId tagId, RationalTag.Rational[] values)
    {
        this.TagId = tagId;

        var value = values.FirstOrDefault();
        this.FormattedValue = $"{value.Numerator}/{value.Denominator} sec";
    }
}
