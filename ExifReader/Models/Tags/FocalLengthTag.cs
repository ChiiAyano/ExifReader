namespace ExifReader.Models.Tags;

/// <summary>
/// 焦点距離を表すタグ
/// </summary>
internal record FocalLengthTag : TagBase
{
    /// <summary>
    /// 焦点距離
    /// </summary>
    public double Focal { get; }

    public FocalLengthTag(TagId tagId, RationalTag.Rational[] values)
    {
        this.TagId = tagId;

        var value = values.FirstOrDefault();

        this.Focal = (double)value.Numerator / value.Denominator;
        this.FormattedValue = $"{this.Focal:0.#} mm";
    }
}