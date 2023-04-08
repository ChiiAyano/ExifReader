namespace ExifReader.Models.Tags;

/// <summary>
/// 露出時間を表すタグ
/// </summary>
internal record ExposureTimeTag : TagBase
{
    public ExposureTimeTag(TagId tagId, RationalTag.Rational[] values)
    {
        this.TagId = tagId;

        var (denominator, numerator) = values.FirstOrDefault();

        if (numerator >= denominator)
        {
            // 分子の方が大きい場合は実数表記に変更
            this.FormattedValue = $"{(double)numerator / denominator} sec";

            return;
        }

        this.FormattedValue = $"{numerator}/{denominator} sec";
    }
}
