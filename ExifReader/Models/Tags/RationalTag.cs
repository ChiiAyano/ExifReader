namespace ExifReader.Models.Tags;

/// <summary>
/// 分母・分子で表される汎用タグ情報
/// </summary>
internal record RationalTag : TagBase
{
    /// <summary>
    /// 分母
    /// </summary>
    public int Denominator { get; }
    /// <summary>
    /// 分子
    /// </summary>
    public int Numerator { get; }
    /// <summary>
    /// 分母と分子を単純に割り算した値
    /// </summary>
    public double Value { get; }

    public RationalTag(TagId tagId, int a, int b)
    {
        this.TagId = tagId;
        this.Numerator = a;
        this.Denominator = b;

        this.Value = (double)this.Numerator / this.Denominator;
        this.FormattedValue = $"{this.Numerator} / {this.Denominator}";
    }
}