namespace ExifReader.Models.Tags;

/// <summary>
/// F値を表すタグ
/// </summary>
internal record FNumberTag : TagBase
{
    /// <summary>
    /// F値
    /// </summary>
    public double Number { get; }

    public FNumberTag(TagId tagId, RationalTag.Rational[] values)
    {
        this.TagId = tagId;

        var value = values.FirstOrDefault();
        this.Number = (double)value.Numerator / value.Denominator;
        this.FormattedValue = $"f/{this.Number:0.0}";
    }
}
