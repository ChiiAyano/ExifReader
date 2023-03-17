namespace ExifReader.Models.Tags;

/// <summary>
/// 位置情報における分数表記のタグ情報
/// </summary>
internal record GpsRationalTag : TagBase
{
    /// <summary>
    /// 度分秒表記の「度」
    /// </summary>
    public int Degree { get; }

    /// <summary>
    /// 度分秒表記の「分」
    /// </summary>
    public double Minute { get; }

    /// <summary>
    /// 度分秒表記の「秒」
    /// </summary>
    public double Second { get; }

    /// <summary>
    /// 度数表記
    /// </summary>
    public double Point => this.Degree + (this.Minute / 60d) + (this.Second / 3600d);

    public GpsRationalTag(TagId tagId, RationalTag.Rational[] values)
    {
        this.TagId = tagId;

        // 度分秒表記の場合は、3つの分数表記の分子はすべて0以上の数字がいるはず
        // 度分表記の場合は、3つ目の分子は0になっているはず

        if (values.Length < 3)
        {
            return;
        }

        this.Degree = (int)((double)values[0].Numerator / values[0].Denominator);
        this.Minute = (double)values[1].Numerator / values[1].Denominator;

        if (values[2].Numerator > 0)
        {
            this.Second = (double)values[2].Numerator / values[2].Denominator;
        }

        this.FormattedValue = this.Point.ToString("0.0000");
    }
}
