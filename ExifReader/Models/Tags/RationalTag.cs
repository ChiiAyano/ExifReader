namespace ExifReader.Models.Tags;

/// <summary>
/// 分母・分子で表される汎用タグ情報
/// </summary>
internal record RationalTag : TagBase
{
    /// <summary>
    /// 分母・分子を表すペアの配列
    /// </summary>
    public Rational[] Rationals { get; }

    public RationalTag(TagId tagId, Rational[] values)
    {
        this.TagId = tagId;
        this.Rationals = values;
        this.FormattedValue = string.Join(", ", this.Rationals.Select(s => $"{s.Numerator}/{s.Denominator}"));
    }

    /// <summary>
    /// 分母・分子を表すペア
    /// </summary>
    /// <param name="Denominator">分母</param>
    /// <param name="Numerator">分子</param>
    public record struct Rational(int Denominator, int Numerator);
}
