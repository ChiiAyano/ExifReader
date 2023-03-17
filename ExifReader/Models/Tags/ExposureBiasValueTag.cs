namespace ExifReader.Models.Tags;

/// <summary>
/// 露出補正値を表すタグ
/// </summary>
internal record ExposureBiasValueTag : TagBase
{
    /// <summary>
    /// 露出補正値
    /// </summary>
    public double ExposureBias { get; }

    public ExposureBiasValueTag(TagId tagId, RationalTag.Rational[] values)
    {
        this.TagId = tagId;
        var value = values.FirstOrDefault();

        this.ExposureBias = (double)value.Numerator / value.Denominator;

        if (this.ExposureBias > 0)
        {
            this.FormattedValue = $"+{this.ExposureBias:0.0} EV";
        }
        else if (this.ExposureBias < 0)
        {
            this.FormattedValue = $"{this.ExposureBias:0.0} EV";
        }
        else
        {
            this.FormattedValue = "±0.0 EV";
        }
    }
}
